using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StokedSportsSpotService.Data.Models;

namespace StokedSportsSpotService.Data.DBContext
{
    public class StokedSpotContext: DbContext
    {
        public DbSet<Spot> Spots { get; set; }
        public DbSet<SpotMedia> SpotMedia { get; set; }

        public StokedSpotContext(DbContextOptions<StokedSpotContext> options)
            : base(options)
        {

        }
    }
}
