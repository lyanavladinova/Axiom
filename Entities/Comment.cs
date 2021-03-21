using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class Comment
    {
        public Comment()
        {
            this.Published = DateTime.UtcNow;
        }
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime Published { get; set; }

        [ForeignKey("Article")]
        public int? ArticleId { get; set; }

        public Article Article { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}