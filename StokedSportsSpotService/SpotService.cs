using StokedSportsSpotService.Data.Models;
using StokedSportsSpotService.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StokedSportsSpotService
{
    public class SpotService: ISpotService
    {
        private readonly ISpotRepository _spotRepository;
        public SpotService(ISpotRepository spotRepository)
        {
            _spotRepository = spotRepository;

        }

        public async Task<Spot> CreateSpot(Spot spot)
        {
            if (spot == null )
            {
                throw new ArgumentNullException("information to create a spot was empty");
            }
            if (String.IsNullOrWhiteSpace(spot.Name)) ;
            {
                throw new ArgumentNullException("spot name is required");
            }
            if (String.IsNullOrWhiteSpace(spot.Description)) ;
            {
                throw new ArgumentNullException("spot description is required");
            }
            if (spot.Latitude == 0 || spot.Longitude == 0) ;
            {
                throw new ArgumentNullException("no location was provided");
            }
            return  await  _spotRepository.CreateSpot(spot);
        }

        public async Task<IEnumerable<Spot>> GetAllSpots()
        {
           var result = await _spotRepository.GetAllSpots();
            if (!result.Any())
            {
                throw new ArgumentException("no spots found!");
            }
            else
            {
                return result;
            }
        }

        public async Task<Spot> GetSpot(Guid spotId)
        {
            return await _spotRepository.GetSpot(spotId);
        }

        public Task<IEnumerable<Spot>> GetSpots(string spotName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Spot>> GetSpots(string cityName, string state)
        {
            throw new NotImplementedException();
        }
    }
}

