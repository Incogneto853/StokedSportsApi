using Microsoft.AspNetCore.Identity;
using StokedSportsIdenityService.Data.DTOs.Requests;
using StokedSportsIdenityService.Data.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StokedSportsIdenityService
{
    public interface IIdentityRepository
    {
        Task<IdentityUser> GetUser(string email);
        Task<IdentityUserResult> CreateAsync(string userId, string password);
        Task<bool> IsValidLogin(IdentityUser existingUser, UserLoginRequest user);
        List<IdentityUser> GetAllUsers();

    }
}
