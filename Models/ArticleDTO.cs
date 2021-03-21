using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class ArticleDTO
    {
        public int Id { get; set; }

        public string Topic { get; set; }

        public string Content { get; set; }
        
        public string Categories { get; set; }
    }
}
