using Microsoft.EntityFrameworkCore;
using StokedSportsSessionService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StokedSportsSessionService.Data.DBContext
{
   public class StokedSessionContext : DbContext
    {
        public DbSet<Session> Sessions { get; set; }
        public StokedSessionContext(DbContextOptions<StokedSessionContext> options
            ):base(options)
        {
                
        }
    }
}
