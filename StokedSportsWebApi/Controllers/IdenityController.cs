using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StokedSportsIdenityService;
using StokedSportsIdenityService.Configuration;
using StokedSportsIdenityService.Data.DTOs.Requests;

namespace StokedWebApi.Controllers
{
    [Route("api/[controller]")] // api/authManagement
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public IdentityController(
            UserManager<IdentityUser> userManager,
            IOptionsMonitor<JwtConfig> optionsMonitor,
            IIdentityService identityService)
        {
            _identityService = identityService;
        }
        [HttpGet]
        [Authorize]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var users = _identityService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet]
        public async Task<IActionResult> Get(string email)
        {
            var user = await _identityService.GetUser(email);
            return Ok(user);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto user)
        {
            var jwtToken = await _identityService.CreateAsync(user, ModelState);
            var userInDb = await _identityService.GetUser(user.Email);
            return Ok(new { JwtToken = jwtToken, User = userInDb, IsAuthenticated = true });
        }

        [HttpPost]
        [Route("GetToken")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest user)
        {
            var userInDb = await _identityService.GetUser(user.Email);
            var jwtToken = await _identityService.GetUserToken(user, ModelState);
            return Ok(new { JwtToken = jwtToken, User = userInDb, IsAuthenticated = true });
        }
    }
}