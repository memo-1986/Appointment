using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using appoimntlastlq.Models.DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace appoimntlastlq.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        //api/Cities

        [HttpGet]
        public IEnumerable<Cities> Get()

        {
            using (var db = new appointmentxContext())
            {
                return db.Cities.ToList();
            }
        }
        //api/Cities/1
        [HttpGet("{id}")]
        public IEnumerable<Area> Get(int id)

        {
            using (var db = new appointmentxContext())
            {
                return db.Area.Where(x => x.CityId == id & x.IsDeleted != true).ToList();
            }
        }
        [HttpPost]
        public int Post([FromBody] Cities c)
        {
            using (var db = new appointmentxContext())
            {
                var cntxt = db.Cities.Where(x => x.CityName == c.CityName).ToList();
                if (cntxt.Count == 1)
                    return 0;
                else

                {
                    Cities cities = new Cities();
                    cities.CityName=c.CityName;
                    cities.CreatedBy = "saleh add cities";
                    db.Cities.Add(cities);
                    db.SaveChanges();
                    return 1;
                }

            }
        }
    }
}