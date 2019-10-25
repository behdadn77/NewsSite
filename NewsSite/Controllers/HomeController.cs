using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using newsSite.Areas.Identity.Data;
using newsSite.Models;

namespace newsSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly DBNews db;
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager;

        public HomeController(DBNews db, UserManager<ApplicationUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }

        public IActionResult Index(string category, int page = 1, int pageSize = 5)
        {
            ViewData["pageNum"] = page;
            ViewData["category"] = category;
            if (category != null && db.Categories.Where(x => x.Name.ToUpper() == category.ToUpper()).FirstOrDefault() != null)
            {
                ViewData["totalPages"] = Math.Ceiling(db.Articles.Where(x => x.Category.Name.ToUpper() == category.ToUpper())
                    .Count() / (double)pageSize);
                return View(db.Articles
                    .Where(x => x.Category.Name.ToUpper() == category.ToUpper()).Skip((page - 1) * pageSize).Take(pageSize)
                    .Include(x => x.Comments).Include(x => x.Author).Include(x => x.Category).ToList());
            }
            ViewData["totalPages"] = Math.Ceiling(db.Articles.Count() / (double)pageSize);
            return View(db.Articles.Skip((page - 1) * pageSize).Take(pageSize).Include(x => x.Comments)
                .Include(x => x.Author).Include(x => x.Category).ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        [Route("article/{id}")]
        public IActionResult Article(int id)
        {
            //return View(db.Articles.Include(x => x.Comments).Include(x => x.Author).Single(x => x.Id == id)); //returend null Comment.User
            var article = db.Articles.Include(x => x.Comments).Include(x => x.Author).Single(x => x.Id == id);
            if (article != null)
            {
                article.Comments.ToList().ForEach(async x => x.User = await userManager.FindByIdAsync(x.UserId));
                ++article.TotalViews;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {

                }
                return View(article);
            }
            TempData["GlobalError"] = "Error";
            return View();
        }
        public List<Category> GetCategoreis()
        {
            return db.Categories.ToList();
        }
        public IActionResult CategoriesCombo()
        {
            return PartialView(db.Categories.ToList());
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
