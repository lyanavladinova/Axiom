using Data;
using Data.Entities;
using Data.Models;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Service
{
    public class CommentService : ICommentService
    {
        private AxiomDbContext context;

        public CommentService(AxiomDbContext context)
        {
            this.context = context;
        }

        public void Create(CommentDTO comment)
        {
            Comment newComment = new Comment();
            newComment.Content = comment.Content;
            newComment.ArticleId = comment.ArticleId;

            this.context.Add(newComment);
            this.context.SaveChanges();
        }

        public void Delete(int id)
        {
            Comment comment = this.context.Comments.FirstOrDefault(x => x.Id == id);

            if (comment != null) 
            {
                this.context.Remove(comment);
                this.context.SaveChanges();
            }
        }

        public List<Comment> GetAllByArticleId(int articleId)
        {
            List<Comment> comments = this.context.Comments
                .Where(x => x.ArticleId == articleId)
                .ToList();

            return comments;
        }
    }
}
