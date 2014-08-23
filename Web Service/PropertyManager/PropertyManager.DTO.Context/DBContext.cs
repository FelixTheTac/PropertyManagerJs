using System;
using System.Data.Entity;
using System.Linq;
using PropertyManager.DTO.Models;

#if Context
namespace PropertyManager.DTO.Context

#endif
#if Migrations
namespace PropertyManager.Migrations
#endif
{
    public class DBContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }

        public DbSet<Property> Properties { get; set; }

        public DbSet<User> Users { get; set; }

       


    }
}
