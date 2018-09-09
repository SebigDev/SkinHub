using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SkinHubApp.DTOs;

namespace SkinHubApp.Services
{
    public interface IReplyServices : IDisposable
    {
        Task<long> CreateReply(CreateReplyDto model);

        Task<long> UpdateReply(ReplyDto model);

        Task<IEnumerable<ReplyDto>> GetAllReplies();

        Task<ReplyDto> GetReplyByID(long id);

        Task<IEnumerable<ReplyDto>> GetReplyByCommentID(long Id);

        Task<IEnumerable<ReplyDto>> GetAllRepliesByAuthor(string author);

        Task<bool> DeleteReply(long Id);

        Task<bool> IsNameExist(string name, long id);
    }
}