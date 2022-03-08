using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StokedSportsIdenityService;
using StokedSportsIdenityService.Configuration;
using StokedSportsIdenityService.Data.DTOs.Requests;
using StokedSportsSpotService;
using StokedSportsSpotService.Data.Models;

namespace StokedWebApi.Controllers
{
    [Route("api/[controller]")]
   // [Authorize]
    [ApiController]
    public class SpotController : ControllerBase
    {
        private readonly ISpotService _spotService;

        public SpotController(
           ISpotService spotService)
        {
            _spotService = spotService;
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _spotService.GetAllSpots());
        }
        [HttpGet]
        public IActionResult Get(Guid spotId)
        {
            return Ok(_spotService.GetSpot(spotId));
        }
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Register(Spot spot)
        {
            var newSpot = await _spotService.CreateSpot(spot);
            return Ok(newSpot);
        }

    }
}