using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using System.Linq;

namespace AXIOM.Controllers
{
    public class CommentController : Controller
    {
        private ICommentService commentService;

        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        [HttpGet]
        [Route("/Comment/Create/{postId}")]
        public IActionResult Create(int articleId)
        {
            ViewData.Add("articleId", articleId);
            
            return View();
        }

        [HttpPost]
        public IActionResult Create(CommentDTO comment)
        {
            this.commentService.Create(comment);
            
            return RedirectToAction("Index", "Article");
        }

        public IActionResult Delete(int id)
        {
            this.commentService.Delete(id);

            return RedirectToAction("Index", "Article");
        }
    }
}
