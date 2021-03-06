﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Data.Repo
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext dataContext;

        public UserRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public async Task<User> Authenticate(string userName, string password)
        {
            return await dataContext.Users.FirstOrDefaultAsync(x => x.Username.ToLower().Equals(userName) &&
            x.Password.Equals(password));
        }
    }
}
