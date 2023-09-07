using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.DtoReadonly
{
    public class PropertyReadonly
    {
        /// <summary>
        /// Id tài sản
        /// </summary>
        public Guid PropertyId { get; set; }
        /// <summary>
        /// Mã tài sản
        /// </summary>
        public string PropertyCode { get; set; }
        /// <summary>
        /// Tên tài sản
        /// </summary>
        public string PropertyName { get; set; }
        /// <summary>
        /// Id bộ phận sử dụng
        /// </summary>
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// Tên bộ phận sử dụng
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// Id bộ phận sử dụng mới
        /// </summary>
        public Guid DepartmentTransferId { get; set; }
        /// <summary>
        /// Tên bộ phận sử dụng mới
        /// </summary>
        public string DepartmentTransferName { get; set; }
        /// <summary>
        /// Nguyên giá
        /// </summary>
        public decimal OriginalPrice { get; set; }
        /// <summary>
        /// Giá trị hao mòn năm
        /// </summary>
        public decimal WearRateValue { get; set; }
        /// <summary>
        /// Năm theo dõi
        /// </summary>
        public int? FollowYear { get; set; }
        /// <summary>
        /// Giá trị còn lại
        /// </summary>
        public decimal ResidualPrice { get; set; }
        /// <summary>
        /// Ngày bắt đầu sử dụng
        /// </summary>
        public DateTime FollowDate { get; set; }
        /// <summary>
        /// Tổng số bản ghi
        /// </summary>
        public int TotalRecordCount { get; set; }
        /// <summary>
        /// Tổng nguyên giá của tất cả các bản ghi
        /// </summary>
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// Tổng giá trị còn lại
        /// </summary>
        public decimal TotalResidualPrice { get; set; }
    }
}
