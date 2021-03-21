using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Models
{
    public class CommentDTO
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public int ArticleId { get; set; }
    }
}
