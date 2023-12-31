﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Demo.Core.Entities
{
    public class Document : BaseEntity
    {
        public Document()
        {
            DocumentId = Guid.NewGuid();
        }
        public Guid DocumentId { get; set; }
        public string DocumentCode { get; set; }
        public DateTime TransferDate { get; set; }
        public DateTime DocumentDate { get; set; }
        public decimal OriginalPrice { get; set; }
        public string? Note { get; set; }
    }
}
