using StokedSportsSpotService.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StokedSportsSpotService.Data.Repositories
{
    public interface ISpotRepository
    {
        Task<Spot> GetSpot(Guid spotId);     
        Task<List<Spot>> GetAllSpots();
        Task<Spot> CreateSpot(Spot spot);

    }
}
