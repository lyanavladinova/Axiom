using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    public class Category
    {
        public Category()
        {
            this. Articles = new List<ArticlesCategories>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public List<ArticlesCategories> Articles { get; set; }
    }
}