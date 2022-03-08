using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StokedSportsSpotService.Data.Models
{
    public class Spot
    {
        [Key]
        public Guid SpotId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Decimal Longitude { get; set; }
        public Decimal Latitude { get; set; }
        public int StarRating { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Directions { get; set; }

    }
}
