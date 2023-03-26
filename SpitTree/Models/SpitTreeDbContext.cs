using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SpitTree.Models
{
      public class SpitTreeDbContext : IdentityDbContext<User>
      {
            public DbSet<Post> Posts { get; set; }
            public DbSet<Category> Categories { get; set; }


            public SpitTreeDbContext()
                    : base("SpittreeConnection3", throwIfV1Schema: false)
                {
                    Database.SetInitializer(new DatabaseInitializer());
                }

                public static SpitTreeDbContext Create()
                {
                    return new SpitTreeDbContext();
                }
        }

    
}