using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StokedSportsSessionService.Data.Models
{
   public class Session
    {
        [Key]
        public Guid SessionId { get; set; }
        public string Name  { get; set; }
        public Guid SpotId  { get; set; }
        public DateTime SessionDateTime{ get; set; }

    }
}
