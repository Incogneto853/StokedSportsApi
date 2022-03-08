using StokedSportsGroupService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StokedSportsGroupService
{
    public interface IGroupService
    {
        Task<Group> GetGroup(Guid groupId);
        Task<IEnumerable<Group>> GetAllGroups();
        Task<Group> CreateGroup(Group group);

    }
}
