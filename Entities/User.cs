using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    public class User : IdentityUser<int>
    {
        public User()
        {
            this.Articles = new List<Article>();
            this.Comments = new List<Comment>();
        }

        public User(string firstName, string lastName, List<Article> articles, List<Comment> comments)
        {
            FirstName = firstName;
            LastName = lastName;
            Articles = articles;
            Comments = comments;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<Article> Articles { get; set; }
        public List<Comment> Comments { get; set; }
    }    
}
