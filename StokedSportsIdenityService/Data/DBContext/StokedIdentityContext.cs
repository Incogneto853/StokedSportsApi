using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace StokedSportsIdenityService.Data.DBContext
{
    public class StokedIdentityContext : IdentityDbContext
    {
        public StokedIdentityContext(DbContextOptions<StokedIdentityContext> options)
            : base(options)
        {
            
        }
    }
}