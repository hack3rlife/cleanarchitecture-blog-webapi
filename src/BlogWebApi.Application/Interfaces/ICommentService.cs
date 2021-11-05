using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlogWebApi.Domain;

namespace BlogWebApi.Application.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<Comment>> GetAll(int skip, int take);
        Task<Comment> GetBy(Guid commentId);
        Task<Comment> Add(Comment comment);
        Task Update(Comment comment);
        Task Delete(Guid commentId);
    }
}