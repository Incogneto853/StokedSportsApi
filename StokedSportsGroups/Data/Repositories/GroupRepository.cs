using Microsoft.EntityFrameworkCore;
using StokedSportsGroupService.Data.DBContext;
using StokedSportsGroupService.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StokedSportsGroupService.Data.Repositories
{
   
    public class GroupRepository : IGroupRepository
    {
        private readonly StokedGroupContext _context;
        public GroupRepository(StokedGroupContext context)
        {
            _context = context;
        }

        public async Task<Group> CreateGroup(Group group)
        {
            await _context.Groups.AddAsync(group);
            await _context.SaveChangesAsync();
            return group;

        }

        public async Task<List<Group>> GetAllGroups()
        {
            var allGroupsInDb = await _context.Groups.ToListAsync();
            return allGroupsInDb;
        }

        public async Task<Group> GetGroup(Guid groupId)
        {
            var groupInDb = await _context.Groups.SingleOrDefaultAsync(g => g.GroupId == groupId);
            return groupInDb;
        }
    }
}
