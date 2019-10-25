using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using newsSite.Areas.Identity.Data;
using newsSite.Models;
using newsSite.Models.ViewModels;

namespace newsSite.Areas.Bloggers.Controllers
{
    //[Authorize(Roles ="bloggers")]
    [Authorize("BloggersPolicy")]
    [Area("Bloggers")]
    [Route("Bloggers/[controller]/[action]")]
    public class PostController : Controller
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager;
        private readonly DBNews db;

        public PostController(UserManager<ApplicationUser> userManager,DBNews db)
        {
            this.userManager = userManager;
            this.db = db;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddArticle()
        {
            return View();
        }

        public async Task<IActionResult> AddArticleConfirmAsync(ArticleViewModel model)
        {
            Article article = new Article()
            {
                Title = model.Title,
                Body = model.Body,
                DateTime = DateTime.Now,
                CategoryId = model.CategoryId,
                AuthorId = (await GetCurrentUserAsync()).Id
            };
            if (model.Image != null)
            {
                byte[] b = new byte[model.Image.Length];
                model.Image.OpenReadStream().Read(b, 0, b.Length);
                article.Image = b;
                article.ImageThumbnail = ImageThumbnailMaker.CreateThumbNail(Image.FromStream(new MemoryStream(b)));
            }
            db.Add(article);
            if (db.SaveChanges()!=0)
            {
                return RedirectToAction("Index", "Home");
            }
            TempData["GlobalError"] = "Error";
            return RedirectToAction("Index", "Home");

        }
        [Route("{id}")]
        public IActionResult EditArticle(int id)
        {
            Article article = db.Articles.Find(id);
            return View(new ArticleViewModel() {
                Id = article.Id,
                Title = article.Title,
                Body = article.Body,
                CategoryId=article.CategoryId
            });
        }

        public async Task<IActionResult> EditArticleConfirmAsync(ArticleViewModel model)
        {
            Article article = db.Articles.Find(model.Id);
            article.Title = model.Title;
            article.Body = model.Body;
            article.CategoryId = model.CategoryId;
            if (model.Image != null)
            {
                byte[] b = new byte[model.Image.Length];
                model.Image.OpenReadStream().Read(b, 0, b.Length);
                article.Image = b;
                article.ImageThumbnail = ImageThumbnailMaker.CreateThumbNail(Image.FromStream(new MemoryStream(b)));
            }
            if (db.SaveChanges() != 0)
            {
                return RedirectToAction("Index", "Home");
            }
            TempData["GlobalError"] = "Error";
            return RedirectToAction("Index", "Home");
        }

        [Route("{id}")]
        public IActionResult DeleteArticle(int id)
        {
            db.Articles.Remove(db.Articles.Find(id));
            if (db.SaveChanges()!=0)
            {
                return RedirectToAction("Index","Home");
            }
            TempData["GlobalError"] = "Error";
            return RedirectToAction("Index", "Home");
        }
    }
}