using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Data.Repo;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityRepository cityRepository;

        public CityController( ICityRepository cityRepository)
        {
            this.cityRepository = cityRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cities=await cityRepository.GetCitiesAsync();
            return Ok(cities);
        }
        //Post api/city
        [HttpPost("post")]
        public async Task<IActionResult> AddCity(City city)
        {
             cityRepository.AddCity(city);
            await cityRepository.SaveAsync();
            return StatusCode(201);
        }

        //Delete api/city/delete/id
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            cityRepository.DeleteCity(id);
            await cityRepository.SaveAsync();
            return Ok(id);
        }
        //[HttpGet("{id}")]
        //public string  Get(int id)
        //{
        //    return "Atlanta";
        //}
    }
}
