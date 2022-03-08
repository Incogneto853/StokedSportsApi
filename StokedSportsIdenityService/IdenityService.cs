using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StokedSportsIdenityService;
using StokedSportsIdenityService.Configuration;
using StokedSportsIdenityService.Data.DTOs.Requests;
using StokedSportsIdenityService.Data.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StokedSportsIdenityService
{
    public class IdentityService: IIdentityService
    {
        private readonly IIdentityRepository _userRepository;
        private readonly JwtConfig _jwtConfig;
        public IdentityService(IIdentityRepository userRepository, IOptionsMonitor<JwtConfig> optionsMonitor)
        {
            _userRepository = userRepository;
            _jwtConfig = optionsMonitor.CurrentValue;
        }
        public async Task<IdentityUser> GetUser(string email)
        {
            var existingUser = await _userRepository.GetUser(email);
            if (existingUser == null)
            {
                throw new KeyNotFoundException($"The provided email { email } returned no results");
            }
            return existingUser;
        }
        async Task<string> IIdentityService.GetUserToken(UserLoginRequest user, Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary modelState)
        {
            if (user == null || !modelState.IsValid)
            {
                throw new ArgumentException("User info is missing cannot login");
            }
            var existingUser = await GetUser(user.Email);
            if (existingUser == null)
            {
                throw new KeyNotFoundException("A user with the entered credentials does not exist ");
            }
            var hasCorrectCredentials = await _userRepository.IsValidLogin(existingUser, user);
            if (!hasCorrectCredentials)
            {
                throw new KeyNotFoundException("A user with the entered credentials does not exist");
            }
            var jwtToken = GenerateJwtToken(existingUser);
            return jwtToken;
        }
        public async Task<string> CreateAsync(UserRegistrationDto user, Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary modelState)
        {
            if (user == null || !modelState.IsValid)
            {
                throw new ArgumentException("User info must be sent to create user");
            }
            if (String.IsNullOrWhiteSpace(user.Email))
            {
                throw new ArgumentNullException("Email must be sent to create user.");
            }
            // We can utilise the model
            var existingUser = await _userRepository.GetUser(user.Email);
            if (existingUser != null)
            {
                throw new ArgumentException($"'{user.Email}' Email is already in use!");
            }
            var newUser = new IdentityUser() { Email = user.Email, UserName = user.Username };
            var isCreated = await _userRepository.CreateAsync(newUser.Email, user.Password);
            if (isCreated.identityResult.Succeeded)
            {
                var jwtToken = GenerateJwtToken(newUser);
                return jwtToken;
            }
            else
            {
                var errorList = isCreated.identityResult.Errors.Select(x => x.Description).ToList();
                throw new Exception(string.Join(", ", errorList));
            }
        }

        string GenerateJwtToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }

        public List<IdentityUser> GetAllUsers()
        {
            var users = _userRepository.GetAllUsers();
            return users;
        }
    }
}

