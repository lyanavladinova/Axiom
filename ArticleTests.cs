using Data;
using Data.Entities;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Service;
using Service.Contracts;
using System.Collections.Generic;
using System.Linq;
using Label = Data.Entities.Category;

namespace Axiom.Tests
{
    public class PostTests
    {
        private AxiomDbContext context;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<AxiomDbContext>()
                .UseInMemoryDatabase("TestDb").Options;

            this.context = new AxiomDbContext(options);
        }

        [TearDown]
        public void TearDown()
        {
            this.context.Database.EnsureDeleted();
        }

        [Test]
        public void TestInMemoryDatabaseWorks()
        {
            Mock<ICategoryService> categoryService = new Mock<ICategoryService>();
            ArticleService articleService = new ArticleService(this.context, categoryService.Object);

            this.context.Articles.Add(new Article() { Id = 2 });
            this.context.Articles.Add(new Article() { Id = 12 });
            this.context.SaveChanges();

            ArticleViewModel articleViewModel = articleService.GetById(2);

            Assert.IsNotNull(articleViewModel);
            Assert.AreEqual(2, this.context.Articles.ToList().Count);
        }

        [Test]
        public void TestGetAllShouldReturnCorrectData()
        {
            Mock<ICategoryService> categoryService = new Mock<ICategoryService>();
            ArticleService articleService = new ArticleService(this.context, categoryService.Object);

            this.context.Articles.Add(new Article() { Id = 2 });
            this.context.Articles.Add(new Article() { Id = 12 });
            this.context.Articles.Add(new Article() { Id = 13 });
            this.context.Articles.Add(new Article() { Id = 14 });
            this.context.SaveChanges();

            List<ArticleViewModel> articleViewModels = articleService.GetAll();

            Assert.IsNotNull(articleViewModels);
            Assert.AreEqual(4, articleViewModels.Count);
        }

        [Test]
        public void CreateArticleShouldReturnCorrectData()
        {
            Category category= new Category();
            category.Id = 1;
            category.Name = "TestCategory";

            Mock<ICategoryService> categoryService = new Mock<ICategoryService>();
            categoryService
                .Setup(x => x.ExistsByName(It.IsAny<string>()))
                .Returns(false);

            categoryService
                .Setup(x => x.Create(It.IsAny<CategoryDTO>()))
                .Callback(() => {
                    this.context.Categories.Add(category);
                    this.context.SaveChanges();
                });

            categoryService
                .Setup(x => x.GetAllCategories(It.IsAny<List<string>>()))
                .Returns(new List<Label>() { category });

            User user = new User() { Id = 1, UserName = "test" };
            this.context.Users.Add(user);
            this.context.SaveChanges();

            ArticleService articleService = new ArticleService(this.context, categoryService.Object);
            articleService.Create(new ArticleDTO() { Topic = "Vocab", Content = "Sent", Categories = "TestLabel" }, user);

            Assert.AreEqual(1, this.context.Articles.ToList().Count);
            Assert.AreEqual("Con", this.context.Articles.First().Content);
        }
    }
}