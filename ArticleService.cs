using Data;
using Data.Entities;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using Service.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service
{
    public class ArticleService : IArticleService
    {
        private AxiomDbContext context;
        private ICategoryService categoryService;

        public ICategoryService Object { get; set; }

        public ArticleService(AxiomDbContext context, ICategoryService categoryService)
        {
            this.context = context;
            this.categoryService = categoryService;
        }

        public List<ArticleViewModel> GetAll()
        {
            List<Article> articles = this.context.Articles
                 .Include(x => x.Categories)
                 .ToList();

            this.context.Categories.ToList();

            List<ArticleViewModel> result = articles
                .Select(this.ToViewModel)
                .ToList();

            return result;
        }

        public void Create(ArticleDTO article, User user)
        {
            List<string> categoryNames = article.Categories
                .Split(", ")
                .ToList();

            List<CategoryDTO> newCategoryNames = categoryNames
                .Where(x => !this.categoryService.ExistsByName(x))
                .Select(x => new CategoryDTO() { Name = x })
                .ToList();

            this.categoryService.Create(newCategoryNames);
            List<Category> categories = this.categoryService.GetAllCategories(categoryNames);

            Article newArticle = this.ToEntity(article);
            newArticle.User = user;

            foreach (var category in categories) 
            {
                context.ArticlesCategories.Add(new ArticlesCategories(newArticle, category));
            }

            this.context.SaveChanges();
        }

        public ArticleViewModel GetById(int? id)
        {
            Article article = this.context.Articles.FirstOrDefault(x => x.Id == id);

            if (article == null) 
            {
                return null;
            }

            ArticleViewModel articleView = this.ToViewModel(article);

            return articleView;
        }

        public void Edit(ArticleDTO updatedArticle)
        {
            Article article = ToEntity(updatedArticle);
            this.context.Update(article);
            this.context.SaveChanges();
        }

        public void Delete(int? id)
        {
            Article article = this.context.Articles
                .Include(x => x.Comments)
                .Include(x => x.Categories)
                .FirstOrDefault(x => x.Id == id);

            //this.context.Categories.ToList();

            this.context.ArticlesCategories.RemoveRange(article.Categories);
            this.context.Comments.RemoveRange(article.Comments);
            this.context.Articles.RemoveRange(article);
            this.context.SaveChanges();
        }

        public ArticleDetailsViewModel GetByIdForDetails(int? id)
        {
            //throw new NotImplementedException();

            Article article = this.context.Articles
                .Include(x => x.Categories)
                .Include(x => x.Comments)
                .FirstOrDefault(x => x.Id == id);

            this.context.Categories.ToList();

            ArticleDetailsViewModel articleDetailsViewModel = new ArticleDetailsViewModel();

            articleDetailsViewModel.Id = article.Id;
            articleDetailsViewModel.Topic = article.Topic;
            articleDetailsViewModel.Content = article.Content;

            articleDetailsViewModel.Categories = article.Categories
                .Select(x => {
                    CategoryViewModel categoryViewModel = new CategoryViewModel();
                    categoryViewModel.Id = x.Article.Id;
                    categoryViewModel.Name = x.Category.Name;

                    return categoryViewModel;
                }).ToList();

            articleDetailsViewModel.Comments = article.Comments
                .Select(x => {
                    CommentViewModel commentViewModel = new CommentViewModel();
                    commentViewModel.Id = x.Id;
                    commentViewModel.Content = x.Content;

                    return commentViewModel;
                }).ToList();

            return articleDetailsViewModel;
        }

        private ArticleViewModel ToViewModel (Article article)
        {
            ArticleViewModel result = new ArticleViewModel();
            result.Id = article.Id;
            result.Topic = article.Topic;
            result.Content = article.Content;
            result.UserId = article.UserId;

            result.Categories = article.Categories
                .Select(y => {

                    CategoryViewModel category = new CategoryViewModel();
                    category.Id = y.Category.Id;
                    category.Name = y.Category.Name;

                    return category;
                }).ToList();

            return result;
        }

        private Article ToEntity(ArticleDTO article)
        {
            Article newArticle = new Article();
            newArticle.Id = article.Id;
            newArticle.Topic = article.Topic;
            newArticle.Content = article.Content;

            return newArticle; 
        }
    }
}
