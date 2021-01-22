using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUnitOfWork unitofWork;

        public AccountController(IUnitOfWork unitofWork)
        {
            this.unitofWork = unitofWork;
        }
        //Post api/Account/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginReqDto loginReq)
        {
            var user = await unitofWork.UserRepository.Authenticate(loginReq.Username, loginReq.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            var loginRes = new LoginResDto();
            loginRes.Username = user.Username;
            loginRes.Token = "Token to be generated";
            return Ok(loginRes);
        }
    }
}
