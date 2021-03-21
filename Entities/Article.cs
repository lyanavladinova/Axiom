using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities
{
    public class Article
    {
        public Article()
        {
            this.Published = DateTime.UtcNow;
            this.Categories = new List<ArticlesCategories>();
            this.Comments = new List<Comment>();
        }
        public int Id { get; set; }

        public DateTime Published { get; set; } = DateTime.UtcNow;

        public string Topic { get; set; }

        public string Content { get; set; }


        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        public List<ArticlesCategories> Categories { get; set; }

        public List<Comment> Comments { get; set; }
    }
}