
using StokedSportsGroupService.Data.Models;
using StokedSportsGroupService.Data.Repositories;
using System;
using System.Collections.Generic;

using System.Threading.Tasks;

namespace StokedSportsGroupService
{
    public class GroupService: IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        public GroupService(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<Group> CreateGroup(Group group)
        {
            var createdGroup = await _groupRepository.CreateGroup(group);
            return createdGroup;
        }

        public async Task<Group> GetGroup(Guid groupId)
        {
            return await _groupRepository.GetGroup(groupId);
        }

        public async Task<IEnumerable<Group>> GetAllGroups()
        {
            return await _groupRepository.GetAllGroups();
        }
    }
}

