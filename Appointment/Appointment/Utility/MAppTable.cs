using appoimntlastlq.Models.DB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
namespace appoimntlastlq.Utility
{
    public class MAppTable
    {
        private appointmentxContext _context;
        public MAppTable(appointmentxContext context)
        {
            _context = context;
        }

        //   التاكد من ان  الزبون  ليس  لديه حجز مسبق  ام لا  في نفس التاريخ  او  نفس  الشركة 
        public int SrchAppTableData(ReqData dt)
        {
            var res = _context.AppTable.Where(s => s.ReNid == dt.ReNid && s.CompanyId == dt.CompanyId || s.ReNid == dt.ReNid && s.ReDate == dt.ReDate);
            if (res == null)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }
}

