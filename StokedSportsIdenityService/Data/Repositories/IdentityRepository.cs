using Microsoft.AspNetCore.Identity;
//using Microsoft.IdentityModel.Tokens;
using StokedSportsIdenityService.Data.DTOs.Requests;
using StokedSportsIdenityService.Data.DTOs.Responses;
using StokedSportsIdenityService.Data.Repositories;
using System;
using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StokedSportsIdenityService.Data.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly UserManager<IdentityUser> _userManager;

        public IdentityRepository(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IdentityUser> GetUser(string email)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);
            return existingUser;

        }
        public async Task<IdentityUserResult> CreateAsync(string userId, string password)
        {
            var newUser = new IdentityUser() { Email = userId, UserName = userId };
            var isCreated = await _userManager.CreateAsync(newUser, password);
            return new IdentityUserResult() { 
                 IdentityUser = newUser,
                 identityResult = isCreated
            };
        }
        public async Task<bool> IsValidLogin(IdentityUser existingUser, UserLoginRequest user)
        {
          var isCorrect = await _userManager.CheckPasswordAsync(existingUser, user.Password);
          return isCorrect;
        }
        public List<IdentityUser> GetAllUsers()
        {  
            var users = _userManager.Users.ToList();
            return users;
        }
    }
}
