using System;
using System.Collections.Generic;

namespace appoimntlastlq.Models.DB
{
    public partial class Company
    {
        public int CompanyId { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ComapanyName { get; set; }
        public string CreatedBy { get; set; }
    }
}
