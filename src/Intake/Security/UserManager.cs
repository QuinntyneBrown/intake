﻿using Intake.Data.Model;
using System.Threading.Tasks;
using System.Security.Principal;
using Intake.Data;
using System.Data.Entity;

namespace Intake.Security
{
    public interface IUserManager
    {
        Task<User> GetUserAsync(IPrincipal user);
    }

    public class UserManager : IUserManager
    {
        public async Task<User> GetUserAsync(IPrincipal user) => await _dataContext.Users.SingleAsync(x => x.Username == user.Identity.Name);

        protected readonly IDataContext _dataContext;
    }
}