using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StokedSportsGroupService.Data.Models;

namespace StokedSportsGroupService.Data.DBContext
{
    public class StokedGroupContext: DbContext
    {
        public DbSet<Group> Groups { get; set; }
       

        public StokedGroupContext(DbContextOptions<StokedGroupContext> options)
            : base(options)
        {

        }
    }
}
