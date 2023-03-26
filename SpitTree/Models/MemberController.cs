using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpitTree.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using System.Net;

namespace SpitTree.Models
{
    public class MemberController : Controller
    {
        public SpitTreeDbContext db = new SpitTreeDbContext();
        // GET: Member
        [Authorize(Roles ="Member")]
        public ActionResult Index()
        {
            var posts = db.Posts.Include(p => p.Category).Include(p => p.User);

            var userId = User.Identity.GetUserId();

            posts = posts.Where(p => p.UserId == userId);

            return View(posts.ToList());
        }

        // GET: Member/Details/5
        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Post post = db.Posts.Find(id);

            if(post == null)
            {
                return HttpNotFound();
            }    
            
            return View(post);
        }

        // GET: Member/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name");

            return View();
        }

        // POST: Member/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PostId, Title, Description, Location, Price, CategoryId")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.DatePosted = DateTime.Now;

                post.DateExpired = DateTime.Now.AddDays(14);

                post.UserId = User.Identity.GetUserId();

                db.Posts.Add(post);

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", post.CategoryId);

            return View(post);
        }

        // GET: Member/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Post post = db.Posts.Find(id);

            if (post == null)
            {
                return HttpNotFound();
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", post.CategoryId);

            return View(post);
        }

        // POST: Member/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include ="PostId, Title, Description, Location, Price, CategoryId")] Post post)
        {
            if(ModelState.IsValid)
            {
                post.DatePosted = DateTime.Now;

                post.DateExpired = DateTime.Now.AddDays(14);

                post.UserId = User.Identity.GetUserId();

                db.Entry(post).State = EntityState.Modified;

                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Name", post.CategoryId);

            return View(post);

        }

        // GET: Member/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Post post = db.Posts.Find(id);

            var category = db.Categories.Find(post.CategoryId);

            post.Category = category;

            if (post == null)
            {
                return HttpNotFound();
            }


            return View(post);
        }

        // POST: Member/Delete/5
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection collection)
        {
            Post post = db.Posts.Find(id);

            db.Posts.Remove(post);

            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
