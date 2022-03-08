using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StokedSportsSessionService;
using StokedSportsSessionService.Data.Models;

namespace StokedWebApi.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;

        public SessionController(
           ISessionService sessionService)
        {
            _sessionService = sessionService;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get(Guid sessionId)
        {
            var retVal = await _sessionService.GetSession(sessionId);
            return Ok(retVal);
        }
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(Session session)
        {
            var newGroup = await _sessionService.CreateSession(session);
            return Ok(newGroup);
        }

    }
}