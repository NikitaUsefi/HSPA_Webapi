using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Models;

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
        public async Task<IActionResult> Get()
        {
            var cities=await dataContext.Cities.ToListAsync();
            return Ok(cities);
        }
        //Post api/city
        [HttpPost("add")]
        [HttpPost("add/{cityname}")]
        public async Task<IActionResult> AddCity(string cityName)
        {
            City city = new City();
            city.Name = cityName;
            await dataContext.Cities.AddAsync(city);
            await dataContext.SaveChangesAsync();
            return Ok(city);
        }
        //Post api/city
        [HttpPost("post")]
        public async Task<IActionResult> AddCity(City city)
        {
            await dataContext.Cities.AddAsync(city);
            await dataContext.SaveChangesAsync();
            return Ok(city);
        }

        //Delete api/city/delete/id
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            var city = await dataContext.Cities.FindAsync(id);
            dataContext.Cities.Remove(city);
            await dataContext.SaveChangesAsync();
            return Ok(id);
        }
        [HttpGet("{id}")]
        public string  Get(int id)
        {
            return "Atlanta";
        }
    }
}
