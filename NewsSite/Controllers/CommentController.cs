using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using newsSite.Areas.Identity.Data;
using newsSite.Models;
using newsSite.Models.ViewModels;

namespace newsSite.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly DBNews db;

        public CommentController(UserManager<ApplicationUser> userManager,DBNews db)
        {
            this.userManager = userManager;
            this.db = db;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);
        public async Task<IActionResult> PostCommentAsync(CommentViewModel commentViewModel)
        {
            if (db.Articles.Find(commentViewModel.PostId) != null)
            {
                db.Comments.Add(new Comment()
                {
                    DateTime = DateTime.Now,
                    Text = commentViewModel.Text,
                    UserId = (await GetCurrentUserAsync()).Id,
                    ArticleId = commentViewModel.PostId
                });
                if (db.SaveChanges() != 0)
                {
                    return Redirect($"/article/{commentViewModel.PostId}");
                }
            }
            TempData["GlobalError"] = "Error";
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> DeleteCommentAsync(int id)
        {
            var comment = db.Comments.Find(id);
            var user = await GetCurrentUserAsync();
            if (comment.UserId== user.Id||await userManager.IsInRoleAsync(user,"bloggers"))
            {
                db.Remove(comment);
                if (db.SaveChanges()!=0)
                {
                    return Redirect($"/article/{comment.ArticleId}");
                }
            }
            TempData["GlobalError"] = "Error";
            return RedirectToAction("Index", "Home");
        }
    }
}