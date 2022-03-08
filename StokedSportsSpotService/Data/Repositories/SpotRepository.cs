using Microsoft.EntityFrameworkCore;
using StokedSportsSpotService.Data.DBContext;
using StokedSportsSpotService.Data.Models;
using System;
using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StokedSportsSpotService.Data.Repositories
{
   
    public class SpotRepository : ISpotRepository
    {
        private readonly StokedSpotContext _context;
        public SpotRepository(StokedSpotContext context)
        {
            _context = context;
        }
        public async Task<Spot> CreateSpot(Spot spot)
        {
            await _context.Spots.AddAsync(spot);
            await _context.SaveChangesAsync();
            return spot;
        }

        public async Task<List<Spot>> GetAllSpots()
        {
           return await _context.Spots.ToListAsync();
        }

        public async Task<Spot> GetSpot(Guid spotId)
        {
           return await _context.Spots.SingleOrDefaultAsync(spot => spot.SpotId.Equals(spotId));
        }
    }
}
