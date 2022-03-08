using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StokedSportsSpotService.Data.Models
{
    public class SpotMedia
    {
        [Key]
        public Guid SpotMediaId { get; set; }
        public Guid SpotId { get; set; }
        public Spot Spot { get; set; }
        public Int16 MediaType { get; set; }
        public Guid SubmittedByUserId { get; set; }
    }
}
