using System;
using System.Collections.Generic;

namespace appoimntlastlq.Models.DB
{
    public partial class Cities
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
