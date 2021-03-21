using Data.Entities;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AXIOM.Controllers
{
    public class ArticleController : Controller
    {
        private IArticleService articleService;
        private UserManager<User> userManager;

        public ArticleController(IArticleService articleService, UserManager<User> userManager)
        {
            this.articleService = articleService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            int userId = 0;

            if (this.User.Identity.IsAuthenticated) 
            {
                User user = await this.userManager.GetUserAsync(this.User);

                userId = user.Id;
            }

            ViewData.Add("ownerId", userId);
            List<ArticleViewModel> articles = this.articleService.GetAll();

            return View(articles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (!this.User.Identity.IsAuthenticated) 
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(ArticleDTO article)
        {
            User user = await this.userManager.GetUserAsync(this.User);

            this.articleService.Create(article, user);

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) 
            {
                return RedirectToAction("Index");
            }

            ArticleViewModel viewModel = this.articleService.GetById(id);

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(ArticleDTO updatedArticle)
        {
            this.articleService.Edit(updatedArticle);

            return RedirectToAction("Details");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) 
            {
                return RedirectToAction("Index");
            }

            ArticleViewModel viewModel = this.articleService.GetById(id);

            return View(viewModel);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteArticle(int? id)
        {
            if (id == null) 
            {
                return RedirectToAction("Index");
            }

            this.articleService.Delete(id);

            return RedirectToAction("Index");
        }

        public IActionResult Details(int? id)
        {
            if (id == null) 
            {
                return RedirectToAction("Index");
            }

            ArticleDetailsViewModel viewModel = this.articleService.GetByIdForDetails(id);

            return View(viewModel);
        }
    }
}
