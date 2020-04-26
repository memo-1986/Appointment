using System;
using System.Collections.Generic;
//جدول الحجز
namespace appoimntlastlq.Models.DB
{
    public partial class AppTable
    {
        public int ReNo { get; set; }//رقم الحجز
        public string ReNid { get; set; }//الرقم الوطني
        public DateTime? ReDate { get; set; }//تاريخ الحجز
        public int? ReArea { get; set; }//الموقع
        public int? CompanyId { get; set; }//الشركة
        public string RePersonId { get; set; }//
        public string ReQuidNo { get; set; }//رقم القيد
    }
}
