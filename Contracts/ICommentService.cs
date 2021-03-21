using Data.Entities;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Contracts
{
    public interface ICommentService
    {
        List<Comment> GetAllByArticleId(int articleId);

        void Create(CommentDTO comment);

        void Delete(int id);
    }
}
