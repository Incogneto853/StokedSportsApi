using Microsoft.AspNetCore.Identity;
using StokedSportsIdenityService.Data.DTOs.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StokedSportsIdenityService
{
    public interface IIdentityService
    {
        Task<string> CreateAsync(UserRegistrationDto user, Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary modelState);
        Task<IdentityUser> GetUser(string email);
        List<IdentityUser> GetAllUsers();
        Task<string> GetUserToken(UserLoginRequest user, Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary modelState);
    }
}
