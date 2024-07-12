using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Logic_Layer.Entity
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User>  User { get; set; }
        public DbSet<UserDetail> UserDetail { get; set; }

        public DbSet<MissionDto> Missions { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<Skill> Skills { get; set; }    
    }
}
