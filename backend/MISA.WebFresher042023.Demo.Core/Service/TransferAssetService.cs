using AutoMapper;
using Microsoft.AspNetCore.Http.Features;
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
using System.Linq.Expressions;
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

        public async Task<object> GetByPaging(int pageNumber, int pageSize)
        {
            var result = await _transferAssetRepository.GetByPaging(pageNumber, pageSize);

            return result;
        }

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

            var idTransfer = transferAsset.TransferAssetId;

            // Kiểm tra nghiệp vụ 
            await transferAssetManager.CheckDuplicateTransferAssetInsertCodeAsync(transferAssetCreateDto);

            transferAssetDetailManager.CheckListDeparment(transferAssetDetails);

            transferAssetManager.ValidateTransferDate(transferAssetCreateDto.TransferDate, transferAssetCreateDto.TransactionDate);

            await transferAssetManager.CheckTransferAssetUpdateOrInsert(transferAssetDetails, transferAssetCreateDto.TransferDate);

            // Thực hiện gán các giá trị id
            foreach (var pt in transferAssetDetails)
            {
                pt.TransferAssetId = idTransfer; // Gán giá trị idDocument trong PropertyTransfer
            }

            foreach (var receiver in receivers)
            {
                receiver.TransferAssetId = idTransfer; // Gán giá trị idDocument trong PropertyTransfer
                receiver.ReceiverId = Guid.NewGuid();
                receiver.CreatedDate = DateTime.Now;
            }

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

        public async Task<int> UpdateDocumentAsync(TransferAssetUpdateDto transferAssetUpdateDto)
        {
            // phân loại các tài sản trong chứng từ vào 3 loại: thêm mới, update, xóa
            List<TransferAssetDetailUpdateDto> transferAssetDetailUpdatesChange = new List<TransferAssetDetailUpdateDto>();

            List<TransferAssetDetailUpdateDto> transferAssetDetailUpdatesDelete = new();

            List<TransferAssetDetailUpdateDto> transferAssetDetailUpdatesInsert = new();

            foreach (TransferAssetDetailUpdateDto transfer in transferAssetUpdateDto.TransferAssetDetailList)
            {
                switch (transfer.StatusRecord)
                {
                    case StatusRecord.Update:
                        transferAssetDetailUpdatesChange.Add(transfer);
                        break;

                    case StatusRecord.Insert:
                        transferAssetDetailUpdatesInsert.Add(transfer);
                        break;

                    case StatusRecord.Delete:
                        transferAssetDetailUpdatesDelete.Add(transfer);
                        break;
                    default:
                        break;
                }
            }

            // Check chứng từ có tồn tại hay không
            var transferAssetCheck = await _transferAssetRepository.GetByIdAsync(transferAssetUpdateDto.TransferAssetId);
            if (transferAssetCheck == null)
            {
                throw new NotFoundException();
            }


            // Nếu thời gian thay đổi thực hiện check
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

            // Check các tài sản trong chứng từ được chỉnh sửa hay không
            transferAssetUpdateDto.TransferAssetDetailList.RemoveAll(detail => detail.StatusRecord == StatusRecord.NoChange);

            // Map entityDto sang entity
            List<TransferAssetDetail> transferAssetDetails = _mapper.Map<List<TransferAssetDetail>>(source: transferAssetUpdateDto.TransferAssetDetailList);

            await transferAssetManager.CheckTransferAssetUpdateOrInsert(transferAssetDetails, transferAssetUpdateDto.TransferDate);


            // Kiểm tra danh sách chi tiết chứng từ có cái nào mà phòng ban chuyển đi giống ban đầu không
            transferAssetDetailManager.CheckListDeparment(transferAssetDetails);

            // Check trùng code
            await transferAssetManager.CheckDuplicateTransferAssetUpdateCode(transferAssetUpdateDto);

            // Check các chi tiết chứng từ xóa với sửa có id nằm trong database hay không
            await transferAssetDetailManager.CheckExistTransferAssetDetail(transferAssetDetailUpdatesChange, transferAssetDetailUpdatesDelete);

            // phân loại danh sách người nhận vào 3 loại: thêm mới, update, xóa

            List<ReceiverUpdateDto> receverChange = new();

            List<ReceiverUpdateDto> receverDelete = new();

            List<ReceiverUpdateDto> receverInsert = new();


            if (transferAssetUpdateDto.ReceiverList != null)
            {
                foreach (ReceiverUpdateDto receiver in transferAssetUpdateDto.ReceiverList)
                {
                    switch (receiver.StatusRecord)
                    {
                        case StatusRecord.Update:
                            receverChange.Add(receiver);
                            break;

                        case StatusRecord.Insert:
                            receverInsert.Add(receiver);
                            break;

                        case StatusRecord.Delete:
                            receverDelete.Add(receiver);
                            break;
                        default:
                            break;
                    }
                }
            }

            // Tạo danh sách id các chi tiết chứng từ bị xóa
            List<Guid> propertyIds = transferAssetDetailUpdatesDelete.Select(p => p.TransferAssetDetailId).ToList();

            string transferAssetDetailDeleteIds = string.Join(", ", propertyIds);

            // Tạo danh sách id người nhận bị xóa
            List<Guid> receiverDeleteIds = receverDelete.Select(p => p.ReceiverId).ToList();

            string receiverConcateDeleteIds = string.Join(", ", receiverDeleteIds);

            // map các entityDto sang entity
            TransferAsset transferAsset = _mapper.Map<TransferAsset>(transferAssetUpdateDto);

            var transferAssetList = new List<TransferAsset>
            {
                transferAsset
            };

            List<TransferAssetDetail> transferAssetDetailChange = _mapper.Map<List<TransferAssetDetail>>(transferAssetDetailUpdatesChange);

            List<TransferAssetDetail> transferAssetDetailInsert = _mapper.Map<List<TransferAssetDetail>>(transferAssetDetailUpdatesInsert);

            List<Receiver> receiverChange = _mapper.Map<List<Receiver>>(receverChange);

            List<Receiver> receiverInsert = _mapper.Map<List<Receiver>>(receverInsert);

            foreach (var transferAssetDetail in transferAssetDetailInsert)
            {
                transferAssetDetail.TransferAssetDetailId = Guid.NewGuid();
            }

            foreach (var receiver in receiverInsert)
            {
                receiver.ReceiverId = Guid.NewGuid();
                receiver.TransferAssetId = transferAsset.TransferAssetId;
            }

            //// Check tài sản trong chứng từ có được phép xóa hay không
            //foreach (var transferAssetDelete in transferAssetDetailUpdatesDelete)
            //{
            //    await propertyManager.CheckGreaterTransferDate(transferAsset.TransferAssetId, transferAssetDelete.PropertyId);
            //}

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
        /// Xóa nhiều chứng từ
        /// </summary>
        /// <param name="transferAssetIdList">Danh sách id của các chứng từ điều chuyển tài sản</param>
        /// <returns></returns>
        /// <exception cref="UserException">Trả về exception thông báo cho người dùng</exception>
        public override async Task<int> DeleteAsync(List<Guid> transferAssetIdList)
        {
            // Check xem trong các chứng từ có tài sản nào có chứng từ mới hơn không
            foreach (var id in transferAssetIdList)
            {
                await transferAssetManager.CheckTransferAsset(id);
            }

            // Check ok, thực hiện xóa nhiều

            string concatenatedIds = string.Join(", ", transferAssetIdList);

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

        public async Task CheckDeleteOrNot(Guid transferAssetId, Guid propertyId)
        {
            await propertyManager.CheckGreaterTransferDate(transferAssetId, propertyId);
        }

        public async Task CheckDeleteMultiOrNot(Guid transferAssetId, List<Guid> transferAssetIds)
        {
            foreach (var id in transferAssetIds)
            {
                await CheckDeleteOrNot(transferAssetId, id);
            }
        }

        public async Task<string> GetAutoTransferAssetCode()
        {
            var code = await _transferAssetRepository.GetGreatestCode();

            if (code == null) return "DC0001";

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
