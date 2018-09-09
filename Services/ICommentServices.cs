using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SkinHubApp.DTOs;

namespace SkinHubApp.Services
{
    public interface ICommentServices : IDisposable
    {
         
         Task<long> CreateComment(CreateCommentDto model);

        Task<long> UpdateComment(CommentDto model);

        Task<IEnumerable<CommentDto>> GetAllComments();

        Task<CommentDto> GetCommentByID(long id);

        Task<IEnumerable<CommentDto>> GetCommentByPostID(long Id);

        Task<IEnumerable<CommentDto>> GetAllCommentByAuthor(string author);

        Task<bool> DeleteComment(long Id);

        Task<bool> IsNameExist(string name, long id);
    }
}