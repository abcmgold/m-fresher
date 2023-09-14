using AutoMapper;
using MISA.WebFresher042023.Demo.Core.Dto.Document;
using MISA.WebFresher042023.Demo.Core.Dto.Hanover;
using MISA.WebFresher042023.Demo.Core.Dto.PropertyTransfer;
using MISA.WebFresher042023.Demo.Core.DtoReadonly;
using MISA.WebFresher042023.Demo.Core.Entities;
using MISA.WebFresher042023.Demo.Core.Enum;
using MISA.WebFresher042023.Demo.Core.HandleException;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;
using MISA.WebFresher042023.Demo.Core.Interface.Service;
using MISA.WebFresher042023.Demo.Core.Manager;
using MISA.WebFresher042023.Demo.Infrastructure.Interface;
using System.Text.RegularExpressions;

namespace MISA.WebFresher042023.Demo.Core.Service
{
    public class TransferAssetService : BaseService<TransferAsset, TransferAssetDto, TransferAssetUpdateDto, TransferAssetCreateDto>, ITransferAssetService
    {
        private readonly ITransferAssetRepository _transferAssetRepository;
        private readonly ITransferAssetDetailRepository _transferAssetDetailRepository;
        private readonly IReceiverRepository _receiverRepository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly TransferAssetManager transferAssetManager;
        private readonly TransferAssetDetailManager transferAssetDetailManager;
        private readonly PropertyManager propertyManager;

        public TransferAssetService(ITransferAssetRepository transferAssetRepository, IMapper mapper, IUnitOfWork unitOfWork, ITransferAssetDetailRepository transferAssetDetailRepository, IReceiverRepository receiverRepository, IPropertyRepository propertyRepository) : base(transferAssetRepository, mapper, unitOfWork)
        {
            _transferAssetRepository = transferAssetRepository;
            _transferAssetDetailRepository = transferAssetDetailRepository;
            _receiverRepository = receiverRepository;
            _propertyRepository = propertyRepository;
            transferAssetManager = new TransferAssetManager(_transferAssetDetailRepository, _transferAssetRepository, _propertyRepository);
            transferAssetDetailManager = new TransferAssetDetailManager(_transferAssetDetailRepository, _transferAssetRepository);
            propertyManager = new PropertyManager(_propertyRepository, _transferAssetRepository);
        }

        /// <summary>
        /// Phân trang danh sách chứng từ điều chuyển
        /// </summary>
        /// <param name="pageNumber">Trang hiện tại</param>
        /// <param name="pageSize">Số bản ghi trong một trang</param>
        /// <returns>Danh sách bản ghi</returns>
        /// CreatedBy: BATUAN (30/08/2023)
        public async Task<object> GetByPaging(int pageNumber, int pageSize)
        {
            var result = await _transferAssetRepository.GetByPaging(pageNumber, pageSize);

            return result;
        }

        /// <summary>
        /// Tạo mới một chứng từ
        /// </summary>
        /// <param name="documentCreateDto">Chứng từ được tạo mới</param>
        /// <returns>1: Thành công || Exception: Thất bại</returns>
        /// CreatedBy: BATUAN (30/08/2023)
        public async Task<int> AddDocumentAsync(TransferAssetCreateDto transferAssetCreateDto)
        {
            // Map các entityDto về entity
            TransferAsset transferAsset = _mapper.Map<TransferAsset>(transferAssetCreateDto);

            List<TransferAsset> transferAssetList = new()
                {
                    transferAsset
                };

            List<TransferAssetDetail> transferAssetDetails = _mapper.Map<List<TransferAssetDetail>>(transferAssetCreateDto.TransferAssetDetailList);

            List<Receiver> receivers = _mapper.Map<List<Receiver>>(transferAssetCreateDto.ReceiverList);

            // Kiểm tra nghiệp vụ 

            // 1. Check trùng code
            await transferAssetManager.CheckDuplicateCode(transferAsset);

            // 2. Check các chi tiết chứng từ có cái nào trùng bộ phận hiện tại và chuyển đi không
            transferAssetDetailManager.CheckListDeparment(transferAssetDetails);

            // 3. Check ngày chứng từ nhỏ hơn ngày điều chuyển không
            transferAssetManager.ValidateTransferDate(transferAssetCreateDto.TransferDate, transferAssetCreateDto.TransactionDate);

            // 4. Check các chi tiết chứng từ có được thêm hay không
            var listIdProperty = transferAssetDetails.Select(p => p.PropertyId).ToList();
            await transferAssetManager.CheckTransferAssetUpdateOrInsert(listIdProperty, transferAssetCreateDto.TransferDate);

            // Thực hiện gán các giá trị mặc định
            var idTransfer = transferAsset.TransferAssetId;

            foreach (var pt in transferAssetDetails)
            {
                // Gán giá trị IdDocument cho chi tiết chứng từ điều chuyển
                pt.TransferAssetId = idTransfer;
                // Tạo mới created date
                pt.CreatedDate = DateTime.Now;
            }

            foreach (var receiver in receivers)
            {
                // Gán giá trị IdDocument cho bản ghi người nhận
                receiver.TransferAssetId = idTransfer; 
                // Tạo mới Id người nhận
                receiver.ReceiverId = Guid.NewGuid();
                // Tạo mới Created Date
                receiver.CreatedDate = DateTime.Now;
            }

            transferAssetList[0].CreatedDate = DateTime.Now;

            // Mở transaction thực hiện insert
            try
            {
                _unitOfWork.BeginTransaction();

                await _transferAssetRepository.InsertAsync(transferAssetList);

                await _transferAssetDetailRepository.InsertAsync(transferAssetDetails);

                await _receiverRepository.InsertAsync(receivers);

                _unitOfWork.Commit();

                return 1;

            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Cập nhật một chứng từ điều chuyển
        /// </summary>
        /// <param name="documentUpdateDto">Chứng từ được cập nhật</param>
        /// <returns>1: Thành công || Exception: Thất bại</returns>
        /// CreatedBy: BATUAN (30/08/2023)
        public async Task<int> UpdateDocumentAsync(TransferAssetUpdateDto transferAssetUpdateDto)
        {
            // Map các entityDto về dto
            List<TransferAssetDetail> transferAssetDetails = _mapper.Map<List<TransferAssetDetail>>(source: transferAssetUpdateDto.TransferAssetDetailList);

            var transferAsset = _mapper.Map<TransferAsset>(transferAssetUpdateDto);
            var transferAssetList = new List<TransferAsset>
            {
                transferAsset
            };

            // Phân loại các tài sản trong chứng từ vào 3 loại: thêm mới, chỉnh sửa, xóa
            var transferAssetDetailUpdatesChange = new List<TransferAssetDetailUpdateDto>();

            var transferAssetDetailUpdatesDelete = new List<TransferAssetDetailUpdateDto>();

            var transferAssetDetailUpdatesInsert = new List<TransferAssetDetailUpdateDto>();

            foreach (var transfer in transferAssetUpdateDto.TransferAssetDetailList)
            {
                ProcessList(transferAssetDetailUpdatesChange, transferAssetDetailUpdatesInsert,transferAssetDetailUpdatesDelete,  transfer.StatusRecord, transfer);

            }

            // Phân loại danh sách người nhận vào 3 loại: thêm mới, chỉnh sửa, xóa

            var receverChange = new List<ReceiverUpdateDto>();

            var receverDelete = new List<ReceiverUpdateDto>();

            var receverInsert = new List<ReceiverUpdateDto>();

            if (transferAssetUpdateDto.ReceiverList != null)
            {
                foreach (ReceiverUpdateDto receiver in transferAssetUpdateDto.ReceiverList)
                {
                    ProcessList(receverChange, receverInsert, receverDelete, receiver.StatusRecord, receiver);
                }
            }

            // Map lại các danh sách vừa được phân loại sang entity

            var transferAssetDetailChange = _mapper.Map<List<TransferAssetDetail>>(transferAssetDetailUpdatesChange);

            var transferAssetDetailInsert = _mapper.Map<List<TransferAssetDetail>>(transferAssetDetailUpdatesInsert);

            var receiverChange = _mapper.Map<List<Receiver>>(receverChange);

            var receiverInsert = _mapper.Map<List<Receiver>>(receverInsert);

            // Check các nghiệp vụ bài toán

            // 1. Check trùng code
            await transferAssetManager.CheckDuplicateCode(transferAsset);

            // 2. Check chứng từ có tồn tại hay không
            var transferAssetCheck = await _transferAssetRepository.GetByIdAsync(transferAssetUpdateDto.TransferAssetId);
            if (transferAssetCheck == null)
            {
                throw new NotFoundException();
            }

            // 3. Nếu thời gian điều chuyển thay đổi thay đổi thực hiện kiểm tra được phép chỉnh sửa hay không
            if (transferAssetCheck.TransferDate != transferAssetUpdateDto.TransferDate)
            {
                if (transferAssetCheck.TransferDate < transferAssetUpdateDto.TransferDate)
                {
                    await transferAssetManager.CheckTransferAssetChangeTransferDate(transferAssetUpdateDto.TransferAssetDetailList, transferAssetCheck.TransferDate, transferAssetUpdateDto.TransferDate);
                }
                else
                {
                    await transferAssetManager.CheckTransferAssetChangeTransferDate(transferAssetUpdateDto.TransferAssetDetailList, transferAssetUpdateDto.TransferDate, transferAssetCheck.TransferDate);
                }

            }

            // 4. Check các tài sản trong chứng từ được chỉnh sửa hay xóa hay thêm không
            var listIdProperty = transferAssetUpdateDto.TransferAssetDetailList.Where(p => p.StatusRecord != StatusRecord.NoChange).Select(p => p.PropertyId).ToList();

            await transferAssetManager.CheckTransferAssetUpdateOrInsert(listIdProperty, transferAssetUpdateDto.TransferDate);

            // 5. Kiểm tra danh sách chi tiết chứng từ có cái nào mà phòng ban chuyển đi giống ban đầu không
            transferAssetDetailManager.CheckListDeparment(transferAssetDetails);

            // 6. Check các chi tiết chứng từ xóa với sửa có Id nằm trong database hay không
            await transferAssetDetailManager.CheckExistTransferAssetDetail(transferAssetDetailUpdatesChange, transferAssetDetailUpdatesDelete);

            // Tạo danh sách id các chi tiết chứng từ bị xóa
            var propertyIds = transferAssetDetailUpdatesDelete.Select(p => p.TransferAssetDetailId).ToList();
            var transferAssetDetailDeleteIds = string.Join(", ", propertyIds);

            // Tạo danh sách id người nhận bị xóa
            var receiverDeleteIds = receverDelete.Select(p => p.ReceiverId).ToList();
            var receiverConcateDeleteIds = string.Join(", ", receiverDeleteIds);

            // Thêm các giá trị mặc định

            transferAsset.ModifiedDate = DateTime.Now;

            foreach(var transferAssetDetail in transferAssetDetailChange)
            {
                transferAssetDetail.ModifiedDate = DateTime.Now;
            }

            foreach (var transferAssetDetail in transferAssetDetailInsert)
            {
                transferAssetDetail.TransferAssetDetailId = Guid.NewGuid();
                transferAssetDetail.CreatedDate = DateTime.Now;
            }

            foreach (var receiver in receiverChange)
            {
                receiver.ModifiedDate = DateTime.Now;
            }

            foreach (var receiver in receiverInsert)
            {
                receiver.ReceiverId = Guid.NewGuid();
                receiver.TransferAssetId = transferAsset.TransferAssetId;
                receiver.CreatedDate = DateTime.Now;
            }

            try
            {
                _unitOfWork.BeginTransaction();

                await _transferAssetRepository.UpdateAsync(entity: transferAssetList);

                await _transferAssetDetailRepository.UpdateAsync(entity: transferAssetDetailChange);

                await _transferAssetDetailRepository.InsertAsync(entity: transferAssetDetailInsert);

                await _receiverRepository.UpdateAsync(entity: receiverChange);

                await _receiverRepository.InsertAsync(entity: receiverInsert);

                if (transferAssetDetailDeleteIds.Length > 0)
                {
                    await _transferAssetDetailRepository.DeleteAsync(transferAssetDetailDeleteIds);
                }

                if (receiverConcateDeleteIds.Length > 0)
                {
                    await _receiverRepository.DeleteAsync(receiverConcateDeleteIds);

                }

                _unitOfWork.Commit();

                return 1;

            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Phân loại entity vào 1 trong 3 loại: Thêm, chỉnh sửa, xóa
        /// </summary>
        /// <typeparam name="T">Generic type đại diện chung cho loại entity truyền vào </typeparam>
        /// <param name="listChange">Danh sách chỉnh sửa</param>
        /// <param name="listInsert">Danh sách thêm mới</param>
        /// <param name="listDelete">Danh sách xóa</param>
        /// <param name="statusRecord">Biến dùng để phân loại</param>
        /// <param name="entity">Thực thể được phân loại</param>
        /// CreatedBy: BATUAN (11/09/2023)
        public void ProcessList<T>(List<T> listChange, List<T> listInsert, List<T> listDelete, Enum.StatusRecord statusRecord, T entity)
        {

            switch (statusRecord)
            {
                case StatusRecord.Update:
                    listChange.Add(entity);
                    break;

                case StatusRecord.Insert:
                    listInsert.Add(entity);
                    break;

                case StatusRecord.Delete:
                    listDelete.Add(entity);
                    break;

                default:
                    break;

            }
        }
        /// <summary>
        /// Xóa nhiều chứng từ
        /// </summary>
        /// <param name="transferAssetIdList">Danh sách id của các chứng từ điều chuyển tài sản</param>
        /// <returns></returns>
        /// <exception cref="UserException">Trả về exception thông báo cho người dùng</exception>
        public override async Task<int> DeleteAsync(List<Guid> transferAssetIdList)
        {
            await transferAssetManager.CheckTransferAssetAllowDelete(transferAssetIdList);

            var concatenatedIds = string.Join(", ", transferAssetIdList);

            try
            {
                _unitOfWork.BeginTransaction();

                await _transferAssetRepository.DeleteAsync(concatenatedIds);

                _unitOfWork.Commit();

                return 1;
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();

                throw;
            }
        }

        /// <summary>
        /// Lấy thông tin của chứng từ điều chuyển theo Id
        /// </summary>
        /// <param name="transferAssetId">Id chứng từ</param>
        /// <returns>Thông tin chứng từ kèm với danh sách tài sản điều chuyển và người nhận</returns>
        /// CreatedBy: BATUAN (30/08/2023)
        public async Task<TransferAssetReadonly> GetInfoTransferAsset(Guid transferAssetId)
        {
            var transferAsset = await _transferAssetRepository.GetByIdAsync(transferAssetId);

            var listDetail = await _transferAssetDetailRepository.GetByTransferAssetId(transferAssetId);

            var listReceiver = await _receiverRepository.GetByTransferAssetId(transferAssetId);

            var listDetailDto = _mapper.Map<List<TransferAssetDetailDto>>(listDetail);

            var listReceiverDto = _mapper.Map<List<ReceiverDto>>(listReceiver);

            var transferAssetContainer = new TransferAssetReadonly
            {
                TransferAssetId = transferAssetId,
                TransferAssetCode = transferAsset.TransferAssetCode,
                TransferDate = transferAsset.TransferDate,
                TransactionDate = transferAsset.TransactionDate,
                OriginalPrice = transferAsset.OriginalPrice,
                Note = transferAsset.Note,
                TransferAssetDetailList = listDetailDto,
                ReceiverList = listReceiverDto
            };

            return transferAssetContainer;
        }

        /// <summary>
        /// Hàm check tài sản điều chuyển có được xóa ở form chỉnh sửa hay không
        /// </summary>
        /// <returns>Tài sản không được xóa và thông tin</returns>
        /// CreatedBy: BATUAN (30/08/2023)
        public async Task CheckDeleteOrNot(Guid transferAssetId, List<Guid> propertyId)
        {
            await propertyManager.CheckGreaterTransferDate(transferAssetId, propertyId);
        }

        /// <summary>
        /// Sinh mã code tự động
        /// </summary>
        /// <returns>Chuỗi mã code của chứng từ</returns>
        /// CreatedBy: BATUAN (30/08/2023)
        public async Task<string> GetAutoTransferAssetCode()
        {
            var code = await _transferAssetRepository.GetGreatestCode();

            if (code == null) return "CT000001";

            string numberPart = Regex.Match(input: code, @"\d+$").Value;

            int lastIndex = code.LastIndexOf(numberPart);

            string textPart = code.Substring(startIndex: 0, lastIndex);


            if (string.IsNullOrEmpty(numberPart))
            {
                code = textPart + 1;
            }
            else
            {
                int number = int.Parse(numberPart) + 1;

                string countedNumber = number.ToString("D" + numberPart.Length);

                code = textPart + countedNumber;
            }

            return code;
        }
    }
}
