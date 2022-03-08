using StokedSportsSpotService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StokedSportsSpotService
{
    public interface ISpotService
    {
        Task<Spot> GetSpot(Guid spotId);
        Task<IEnumerable<Spot>> GetSpots(string spotName);
        Task<IEnumerable<Spot>>GetSpots(string cityName, string state);
        Task<IEnumerable<Spot>> GetAllSpots();
        Task<Spot> CreateSpot(Spot spot);

    }
}
