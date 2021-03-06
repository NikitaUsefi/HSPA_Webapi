﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Data.Repo;
using WebAPI.Dtos;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Authorize]
    public class CityController : BaseController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CityController( IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            var cities=await unitOfWork.CityRepository.GetCitiesAsync();
            IEnumerable<CityDto> cityDtos = mapper.Map<IEnumerable<CityDto>>(cities);
            return Ok(cityDtos);
        }
        //Post api/city
        [HttpPost("post")]
        public async Task<IActionResult> AddCity(CityDto cityDto)
        {
            var city = mapper.Map<City>(cityDto);
            city.LastUpdatedBy = 1;
            city.LastUpdatedOn = DateTime.Now;
            unitOfWork.CityRepository.AddCity(city);
            await unitOfWork.SaveAsync();
            return StatusCode(201);
        }
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCity(int id,CityDto cityDto)
        {
            throw new Exception("Unknown error");
                var cityFromDb = await unitOfWork.CityRepository.FindCity(id);
                if (cityFromDb == null)
                    return BadRequest("update not allowed");
                cityFromDb.LastUpdatedBy = 1;
                cityFromDb.LastUpdatedOn = DateTime.Now;
                mapper.Map(cityDto, cityFromDb);
                await unitOfWork.SaveAsync();
                return StatusCode(200);
           

        }
        [HttpPut("updatecityname/{id}")]
        public async Task<IActionResult> UpdateCity(int id, CityUpdateDto cityDto)
        {
            var cityFromDb = await unitOfWork.CityRepository.FindCity(id);
            cityFromDb.LastUpdatedBy = 1;
            cityFromDb.LastUpdatedOn = DateTime.Now;
            mapper.Map(cityDto, cityFromDb);
            await unitOfWork.SaveAsync();
            return StatusCode(200);

        }

        //Delete api/city/delete/id
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            unitOfWork.CityRepository.DeleteCity(id);
            await unitOfWork.SaveAsync();
            return Ok(id);
        }
        //[HttpGet("{id}")]
        //public string  Get(int id)
        //{
        //    return "Atlanta";
        //}
    }
}
