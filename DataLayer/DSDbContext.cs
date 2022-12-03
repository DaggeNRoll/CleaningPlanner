using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class DSDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<CleaningSpace> CleaningSpaces { get; set; }
        public DbSet<LoginInformation> LoginInformations { get; set; }

        public DSDbContext(DbContextOptions<DSDbContext> options) : base(options) { }

        public class DSDbContextFactory : IDesignTimeDbContextFactory<DSDbContext>
        {
            public DSDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<DSDbContext>();
                optionsBuilder.UseMySql("server=std-mysql.ist.mospolytech.ru;database=std_1981_dorm_schedule;user=std_1981_dorm_schedule;password=Vhhvze05042002",
                    new MySqlServerVersion(new Version(8, 0, 15)), b => b.MigrationsAssembly(nameof(DataLayer)));

                return new DSDbContext(optionsBuilder.Options);
            }
        }


    }
}
