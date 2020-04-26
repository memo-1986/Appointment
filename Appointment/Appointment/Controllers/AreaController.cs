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
    public class AreaController : ControllerBase
    {
        //api/Area
        private readonly appointmentxContext _context;
        public AreaController(appointmentxContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IEnumerable<Area> Get()

        {
          var a=  new MAppTable(_context);
            //a.BringData("119850540652");
            using (var db = new appointmentxContext())
            {
                return db.Area.ToList();
            }
        }
        //api/Area/1
        [HttpGet("{id}")]
        public IEnumerable<Area> Get(int id)

        {
            using (var db = new appointmentxContext())
            {
                return db.Area.Where(x => x.AreaId == id & x.IsDeleted != true).ToList();
            }
        }
        [HttpPost]
        public int Post([FromBody] Area a)
        {
            using (var db = new appointmentxContext())
            {
                var cntxt = db.Area.Where(x => x.AreaName == a.AreaName && x.CityId==a.CityId).ToList();
                if (cntxt.Count== 1)
                    return 0;
                else

                {
                    Area area = new Area();
                    area.AreaName = a.AreaName;
                    area.CityId = a.CityId;
                    area.CreatedBy = "saleh add area";
                    db.Area.Add(area);
                    db.SaveChanges();
                    return 1;
                }
           
            }
        }

    }
}