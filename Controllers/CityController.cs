using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly DataContext dataContext;

        public CityController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var cities=dataContext.Cities.ToList();
            return Ok(cities);
        }
        [HttpGet("{id}")]
        public string  Get(int id)
        {
            return "Atlanta";
        }
    }
}
