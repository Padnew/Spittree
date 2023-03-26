using SpitTree.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace SpitTree.Controllers
{
    public class HomeController : Controller
    {
        public SpitTreeDbContext context = new SpitTreeDbContext();

        public ActionResult Index()
        {
            var posts = context.Posts.Include(p => p.Category).Include(p => p.User).OrderByDescending(p => p.DatePosted);

            ViewBag.Categories = context.Categories.ToList();

            return View(posts.ToList());
        }

        public ActionResult Details(int id)
        {
            Post post = context.Posts.Find(id);

            var user = context.Users.Find(post.UserId);

            var category = context.Categories.Find(post.CategoryId);

            post.User = user;

            post.Category = category;


            return View(post);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpPost]
        public ViewResult Index(string SearchString)
        {
            var posts = context.Posts.Include(p => p.Category).Include(p => p.User).Where(p => p.Category.Name.Equals(SearchString.Trim())).OrderByDescending(p => p.DatePosted);

            return View(posts.ToList());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}