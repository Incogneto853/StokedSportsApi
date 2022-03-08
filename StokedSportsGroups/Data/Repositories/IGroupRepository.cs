using StokedSportsGroupService.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StokedSportsGroupService.Data.Repositories
{
    public interface IGroupRepository
    {
        Task<Group> GetGroup(Guid groupId);     
        Task<List<Group>> GetAllGroups();
        Task<Group> CreateGroup(Group group);

    }
}
