using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class ArticleDetailsViewModel
    {
        public int Id { get; set; }

        public string Topic { get; set; }

        public string Content { get; set; }

        public List<CategoryViewModel> Categories { get; set; }

        public List<CommentViewModel> Comments { get; set; }
    }
}
