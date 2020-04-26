using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using appoimntlastlq.Models.DB;
using appoimntlastlq.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace appoimntlastlq.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppTableController : ControllerBase
    {
        // GET: api/AppTable
        private readonly appointmentxContext _context;
        public AppTableController(appointmentxContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<int> Get()
        {
            var data = new ReqData();
            var dd = new DateTime(2020, 01, 08);
            data.ReDate = dd;
            data.ReNid = "119850540652";
            data.CompanyId = 1;
            var a = new MAppTable(_context);
            var res = a.SrchAppTableData(data);
            return new int[] { res };
        }

        // GET: api/AppTable/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(ReqData data)
        {


            return "value";
        }

        // POST: api/AppTable
        [HttpPost]
        public void Post([FromBody]ReqData data)
        {

        }

        // PUT: api/AppTable/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
