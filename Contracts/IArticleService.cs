using Data.Entities;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Contracts
{
    public interface IArticleService
    {
        ArticleViewModel GetById(int? id);

        void Create(ArticleDTO article, User user);

        List<ArticleViewModel> GetAll();

        void Edit(ArticleDTO updateArticle);

        void Delete(int? id);

        ArticleDetailsViewModel GetByIdForDetails(int? id);
    }
}
