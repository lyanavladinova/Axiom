using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    public class ArticlesCategories
    {
        public ArticlesCategories()
        {

        }
        public ArticlesCategories(Article article, Category category)
        {
            Article = article;
            Category = category;
        }

        public int ArticleId { get; set; }
        public Article Article { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}