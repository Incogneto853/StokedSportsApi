using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StokedSportsIdenityService.Data.DTOs.Responses
{
    public class AppUser
    {
        public string UserName { get; set; }
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
