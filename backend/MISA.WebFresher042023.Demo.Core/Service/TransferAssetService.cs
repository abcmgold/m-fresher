using AutoMapper;
using Microsoft.AspNetCore.Http.Features;
using MISA.WebFresher042023.Demo.Core.Dto.Document;
using MISA.WebFresher042023.Demo.Core.Dto.PropertyTransfer;
using MISA.WebFresher042023.Demo.Core.Entities;
using MISA.WebFresher042023.Demo.Core.Enum;
using MISA.WebFresher042023.Demo.Core.HandleException;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;
using MISA.WebFresher042023.Demo.Core.Interface.Service;
using MISA.WebFresher042023.Demo.Core.Manager;
using MISA.WebFresher042023.Demo.Infrastructure.Interface;
using System.Linq.Expressions;

namespace MISA.WebFresher042023.Demo.Core.Service
{
    public class TransferAssetService : BaseService<TransferAsset, TransferAssetDto, TransferAssetUpdateDto, TransferAssetCreateDto>, ITransferAssetService
    {
        private readonly ITransferAssetRepository _transferAssetRepository;
        private readonly ITransferAssetDetailRepository _transferAssetDetailRepository;
        private readonly TransferAssetManager transferAssetManager;
        private readonly TransferAssetDetailManager transferAssetDetailManager;

        public TransferAssetService(ITransferAssetRepository transferAssetRepository, IMapper mapper, IUnitOfWork unitOfWork, ITransferAssetDetailRepository transferAssetDetailRepository) : base(transferAssetRepository, mapper, unitOfWork)
        {
            _transferAssetRepository = transferAssetRepository;
            _transferAssetDetailRepository = transferAssetDetailRepository;
            transferAssetManager = new TransferAssetManager(_transferAssetDetailRepository, _transferAssetRepository);
            transferAssetDetailManager = new TransferAssetDetailManager(_transferAssetDetailRepository, _transferAssetRepository);
        }

        public async Task<object> GetByPaging(int pageNumber, int pageSize)
        {
            var result = await _transferAssetRepository.GetByPaging(pageNumber, pageSize);

            return result;
        }

        public async Task<int> AddDocumentAsync(TransferAssetCreateDto transferAssetCreateDto)
        {
            // Check nghiệp vụ ở đây này 
            if (transferAssetManager.ValidateTransferDate(transferAssetCreateDto.TransferDate, transferAssetCreateDto.TransactionDate) && transferAssetManager.ValidateNewDepartment(transferAssetCreateDto.TransferAssetDetailCreateList))
            {
                TransferAsset transferAsset = _mapper.Map<TransferAsset>(transferAssetCreateDto);

                List<TransferAsset> transferAssetList = new List<TransferAsset>();

                transferAssetList.Add(transferAsset);

                List<TransferAssetDetail> transferAssetDetails = _mapper.Map<List<TransferAssetDetail>>(transferAssetCreateDto.TransferAssetDetailCreateList);

                List<Receiver> receivers = _mapper.Map<List<Receiver>>(transferAssetCreateDto.ReceiverCreateList);

                Guid idTransfer = transferAsset.TransferAssetId;

                foreach (var pt in transferAssetDetails)
                {
                    pt.TransferAssetId = idTransfer; // Gán giá trị idDocument trong PropertyTransfer
                }

                foreach (var receiver in receivers)
                {
                    receiver.TransferAssetId = idTransfer; // Gán giá trị idDocument trong PropertyTransfer
                }

                try
                {
                    _unitOfWork.BeginTransaction();

                    await _transferAssetRepository.InsertAsync(transferAssetList);

                    await _transferAssetDetailRepository.InsertAsync(transferAssetDetails);

                    _unitOfWork.Commit();

                    return 1;

                }
                catch (Exception)
                {
                    _unitOfWork.Rollback();
                    throw;
                }
            }

            return 0;
        }

        public async Task<int> UpdateDocumentAsync(TransferAssetUpdateDto transferAssetUpdateDto)
        {
            // phân loại các tài sản trong chứng từ vào 3 loại: thêm mới, update, xóa
            List<TransferAssetDetailUpdateDto> transferAssetDetailUpdatesChange = new List<TransferAssetDetailUpdateDto>();

            List<TransferAssetDetailUpdateDto> transferAssetDetailUpdatesDelete = new List<TransferAssetDetailUpdateDto>();

            List<TransferAssetDetailUpdateDto> transferAssetDetailUpdatesInsert = new List<TransferAssetDetailUpdateDto>();



            foreach (TransferAssetDetailUpdateDto transfer in transferAssetUpdateDto.TransferAssetDetailUpdateList)
            {
                switch (transfer.StatusRerord)
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

            // tạo danh sách id các chứng từ bị xóa
            List<Guid> propertyIds = transferAssetDetailUpdatesDelete.Select(p => p.TransferAssetDetailId).ToList();

            string concatenatedIds = string.Join(", ", propertyIds);

            // map các entityDto sang entity
            TransferAsset transferAsset = _mapper.Map<TransferAsset>(transferAssetUpdateDto);

            var transferAssetList = new List<TransferAsset>();

            transferAssetList.Add(transferAsset);

            List<TransferAssetDetail> transferAssetDetailChange = _mapper.Map<List<TransferAssetDetail>>(transferAssetDetailUpdatesChange);

            List<TransferAssetDetail> transferAssetDetailInsert = _mapper.Map<List<TransferAssetDetail>>(transferAssetDetailUpdatesInsert);

            List<Receiver> receivers = _mapper.Map<List<Receiver>>(transferAssetUpdateDto.ReceiverUpdateList);

            // Check tài sản trong chứng từ có được phép xóa hay không
            foreach (var transferAssetDelete in transferAssetDetailUpdatesDelete)
            {
                var res = await transferAssetDetailManager.CheckTransferAssetDetail(transferAssetDelete.TransferAssetDetailId, transferAsset.TransferAssetId);
                if (res.Count > 0)
                {
                    throw new UserException("Tài sản đã phát sinh chứng từ, không thể xóa!", 400);
                }
            }

            try
            {
                _unitOfWork.BeginTransaction();

                await _transferAssetRepository.UpdateAsync(transferAssetList);

                await _transferAssetDetailRepository.UpdateAsync(transferAssetDetailChange);

                await _transferAssetDetailRepository.InsertAsync(transferAssetDetailInsert);

                await _transferAssetDetailRepository.DeleteAsync(concatenatedIds);

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
        /// <param name="transferAssetIdList">danh sách id của các chứng từ điều chuyển tài sản</param>
        /// <returns></returns>
        /// <exception cref="UserException"></exception>
        public override async Task<int> DeleteAsync(List<Guid> transferAssetIdList)
        {
            // check xem trong các chứng từ có tài sản nào có chứng từ mới hơn không
            foreach(var id in transferAssetIdList)
            {
                await transferAssetManager.CheckTransferAsset(id);
            }

            // check ok, thực hiện xóa nhiều

            string concatenatedIds = string.Join(", ", transferAssetIdList);

            try
            {
                _unitOfWork.BeginTransaction();

                await _transferAssetRepository.DeleteAsync(concatenatedIds);

                return 1;
            }
            catch(Exception)
            {
                _unitOfWork.Rollback();

                throw;
            }
        }
    }
}
