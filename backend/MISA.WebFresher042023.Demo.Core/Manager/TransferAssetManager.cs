using Microsoft.AspNetCore.Http.Features;
using MISA.WebFresher042023.Demo.Core.Dto.PropertyTransfer;
using MISA.WebFresher042023.Demo.Core.Entities;
using MISA.WebFresher042023.Demo.Core.HandleException;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Manager
{
    public class TransferAssetManager
    {
        private readonly ITransferAssetDetailRepository _transferAssetDetailRepository;
        private readonly ITransferAssetRepository _transferAssetRepository;
        public TransferAssetManager(ITransferAssetDetailRepository transferAssetDetailRepository, ITransferAssetRepository transferAssetRepository)
        {
            _transferAssetDetailRepository = transferAssetDetailRepository;
            _transferAssetRepository = transferAssetRepository;
        }

        /// <summary>
        /// Check chứng từ có được phép xóa hay không
        /// </summary>
        /// <param name="DocumentId">Mã id của chứng từ điều chuyển</param>
        /// <returns>True: được xóa, nếu không gửi về exception</returns>
        public async Task<Boolean> CheckTransferAsset(Guid transferAssetId)
        {
            // kiểm tra xem transferAssetId có phải là id của 1 chứng từ điều chuyển nào không
            var transferAsset = await _transferAssetRepository.GetByIdAsync(transferAssetId);

            if (transferAsset != null)
            {
                var transferAssetDetailList = await _transferAssetDetailRepository.GetByTransferAssetId(transferAssetId);

                // 2. Kiểm tra từng tài sản, nếu tài sản nào có tồn tại trong 1 chứng từ khác
                // mà có thời gian tạo lớn hơn thời gian tạo của tài sản hiện tại
                // thì trả về false(không được xóa), nếu không trả về true (được xóa)

                foreach (var transferAssetDetail in transferAssetDetailList)
                {
                    // cái procedure này chưa sửa đâu
                    var res = await _transferAssetRepository.CheckExist(transferAssetDetail.PropertyId, transferAsset.TransactionDate);

                    var transferAssetCurrent = res.FirstOrDefault(t => t.TransferAssetId == transferAssetId);

                    res.RemoveAll(element => element.TransferAssetId ==  transferAssetId);

                    if (res.Count > 0)
                    {
                        var infoTransferAssets = new List<string>();

                        foreach (var transfer in res)
                        {
                            string formattedDate = transfer.TransactionDate.ToString("dd/MM/yyyy");

                            infoTransferAssets.Add($"- Chứng từ điều chuyển <strong>{transfer.TransferAssetCode}</strong> <strong>({formattedDate})</strong>");
                        }
                        throw new UserException($"Không thể xóa chứng từ <strong>{transferAssetCurrent.TransferAssetCode}</strong> do tài sản <strong>{transferAssetDetail.PropertyCode}</strong> nằm trong chứng từ này cũng thuộc chứng từ khác có thời gian tạo mới hơn !", 400, infoTransferAssets);
                    }
                }

                return true;
            }

            else throw new NotFoundException();
        }
        /// <summary>
        /// kiểm tra ngày chứng từ có nhỏ hơn ngày điều chuyển hay không
        /// </summary>
        /// <param name="transferDate">Ngày điều chuyển</param>
        /// <param name="transactionDate">Ngày chứng từ</param>
        /// <returns>true: nếu hợp lệ</returns>
        /// <exception cref="UserException">Exception nếu không hợp lệ</exception>
        public Boolean ValidateTransferDate(DateTime transferDate, DateTime transactionDate)
        {
            if (transferDate < transactionDate)
            {
                throw new UserException("Ngày điều chuyển phải lớn hơn hoặc bằng ngày chứng từ", 400, "transferDateInput");
            }
            else return true;
        }

        public Boolean ValidateNewDepartment(List<TransferAssetDetailCreateDto> transferAssetDetailCreateDtos)
        {
            foreach (var transferAsset in transferAssetDetailCreateDtos)
            {
                if (transferAsset.DepartmentId == transferAsset.DepartmentTransferId)
                {
                    throw new UserException("Bộ phận sử dụng mới không được trùng với bộ phận sử dụng cũ", 400);
                }
            }
            return true;
        } 
    }
}
