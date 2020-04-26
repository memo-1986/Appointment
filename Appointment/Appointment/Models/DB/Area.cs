using System;
using System.Collections.Generic;

namespace appoimntlastlq.Models.DB
{
    public partial class Area
    {
        public int AreaId { get; set; }
        public string AreaName { get; set; }
        public int? CityId { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
