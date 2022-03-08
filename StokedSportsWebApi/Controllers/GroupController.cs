using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StokedSportsGroupService;
using StokedSportsGroupService.Data.Models;

namespace StokedWebApi.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(
           IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _groupService.GetAllGroups());
        }
        [HttpGet]
        public async Task<IActionResult> Get(Guid groupId)
        {
            var retVal = await _groupService.GetGroup(groupId);
            return Ok(retVal);
        }
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Register(Group group)
        {
            var newGroup = await _groupService.CreateGroup(group);
            return Ok(newGroup);
        }

    }
}