using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SpitTree.Models
{
    public class DatabaseInitializer : DropCreateDatabaseAlways<SpitTreeDbContext>
    {
        protected override void Seed(SpitTreeDbContext context)
        {
            if (!context.Users.Any())
            {
                RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                if (!roleManager.RoleExists("Admin"))
                {
                    roleManager.Create(new IdentityRole("Admin"));
                }

                if (!roleManager.RoleExists("Member"))
                {
                    roleManager.Create(new IdentityRole("Member"));
                }

                context.SaveChanges();

                UserManager<User> userManager = new UserManager<User>(new UserStore<User>(context));

                if (userManager.FindByName("admin@spittree.com") == null)
                {
                    userManager.PasswordValidator = new PasswordValidator
                    {
                        RequiredLength = 1,
                        RequireNonLetterOrDigit = false,
                        RequireDigit = false,
                        RequireLowercase = false,
                        RequireUppercase = false,
                    };

                    var admin = new User
                    {
                        UserName = "admin@spittree.com",
                        Email = "admin@spittree.com",
                        FirstName = "Jim",
                        LastName = "Smith",
                        Street = "56 High Street",
                        City = "Glasgow",
                        PostCode = "G1 67AD",
                        EmailConfirmed = true,
                        PhoneNumber = "08847362732"
                    };

                    userManager.Create(admin, "123456");

                    userManager.AddToRole(admin.Id, "Admin");


                    var member1 = new User()
                    {
                        UserName = "member1@gmail.com",
                        Email = "member1@gmail.com",
                        FirstName = "Paul",
                        LastName = "Goat",
                        Street = "123 Jane Street",
                        City = "Glasgow",
                        PostCode = "G3 5VV",
                        EmailConfirmed = true,
                        PhoneNumber = "063546362732"
                    };

                    if (userManager.FindByName("member1@gmail.com") == null)
                    {
                        userManager.Create(member1, "123456");
                        userManager.AddToRole(member1.Id, "Member");
                    }

                    var member2 = new User()
                    {
                        UserName = "member2@gmail.com",
                        Email = "member2@gmail.com",
                        FirstName = "Andy",
                        LastName = "Robertson",
                        Street = "1 Clyde Road",
                        City = "Glasgow",
                        PostCode = "G11 6DV",
                        EmailConfirmed = true,
                        PhoneNumber = "03466362732"
                    };

                    if (userManager.FindByName("member2@gmail.com") == null)
                    {
                        userManager.Create(member2, "123456");
                        userManager.AddToRole(member2.Id, "Member");
                    }

                    context.SaveChanges();

                    //Adding Categories
                    var cat1 = new Category() { Name = "Motors" };
                    var cat2 = new Category() { Name = "Property" };
                    var cat3 = new Category() { Name = "Jobs" };
                    var cat4 = new Category() { Name = "Services" };
                    var cat5 = new Category() { Name = "Pets" };
                    var cat6 = new Category() { Name = "For Sale" };

                    //Saving Categories to the context
                    context.Categories.Add(cat1);
                    context.Categories.Add(cat2);
                    context.Categories.Add(cat3);
                    context.Categories.Add(cat4);
                    context.Categories.Add(cat5);
                    context.Categories.Add(cat6);
                    //Saving Categories to the database
                    context.SaveChanges();

                    var post1 = new Post()
                    {
                        Title = "House For Sale",
                        Description = "Detached 5 bed",
                        Location = "Glasgow",
                        Price = 145000m,
                        DatePosted = new DateTime(2019, 1, 1, 8, 0, 15),
                        DateExpired = new DateTime(2019, 1, 1, 8, 0, 15).AddDays(14),
                        User = member2,
                        Category = cat2
                    };

                    context.Posts.Add(post1);

                    var post2 = new Post()
                    {
                        Title = "Hyundai Tuscon",
                        Description = "4dr 2016 Hyundai Tuscon",
                        Location = "Aberdeen",
                        Price = 14000m,
                        DatePosted = new DateTime(2019, 5, 25, 8, 0, 15),
                        DateExpired = new DateTime(2019, 1, 1, 8, 0, 15).AddDays(14),
                        User = member2,
                        Category = cat1
                    };

                    context.Posts.Add(post2);

                    var post3 = new Post()
                    {
                        Title = "Audi Q5",
                        Description = "Brand new 2020 Audi",
                        Location = "Edinburgh",
                        Price = 56000m,
                        DatePosted = new DateTime(2019, 1, 25, 6, 0, 15),
                        DateExpired = new DateTime(2019, 1, 25, 6, 0, 15).AddDays(14),
                        User = member1,
                        Category = cat2
                    };

                    context.Posts.Add(post3);

                    var post4 = new Post()
                    {
                        Title = "Lhasso Apso",
                        Description = "Beautiful 2 year old Lhasso Apso",
                        Location = "Galsgow",
                        Price = 500m,
                        DatePosted = new DateTime(2019, 3, 5, 8, 0, 15),
                        DateExpired = new DateTime(2019, 3, 5, 8, 0, 15).AddDays(14),
                        User = member2,
                        Category = cat5
                    };
                    context.Posts.Add(post4);

                    var post5 = new Post()
                    {
                        Title = "Mercedez Benz C220d",
                        Description = "2016 Model C clas 2.2l diesel",
                        Location = "Edinburgh",
                        Price = 34000m,
                        DatePosted = new DateTime(2019, 4, 5, 5, 0, 15),
                        DateExpired = new DateTime(2019, 4, 5, 5, 0, 15).AddDays(14),
                        User = member1,
                        Category = cat1
                    };
                    context.Posts.Add(post5);

                    var post6 = new Post()
                    {
                        Title = "1997 Nissan R32 GTR",
                        Description = "128000 Miles, runs great, fantastic condition",
                        Location = "Glasgow",
                        Price = 22000m,
                        DatePosted = new DateTime(2019, 4, 2, 8, 0, 15),
                        DateExpired = new DateTime(2019, 4, 2, 8, 0, 15).AddDays(14),
                        User = member2,
                        Category = cat1
                    };
                    context.Posts.Add(post6);

                    context.SaveChanges();

                    base.Seed(context);
                }

            }
        }


    }
}