using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class ArticleViewModel
    {
        public int Id { get; set; }

        public string Topic { get; set; }

        public string Content { get; set; }

        public int UserId { get; set; }

        public List<CategoryViewModel> Categories { get; set; }
    }
}
