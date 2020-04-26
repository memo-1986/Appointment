using System;
using System.Collections.Generic;

namespace appoimntlastlq.Models.DB
{
    public partial class Control
    {
        public int ControlId { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public int CompanyId { get; set; }
        public int Qouta { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
