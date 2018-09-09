using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SkinHubApp.Data;
using SkinHubApp.DTOs;
using SkinHubApp.Models;

namespace SkinHubApp.Services
{
    public class ReplyServices : IReplyServices
    {
        #region Fields
            private readonly SkinHubAppDbContext _skinHubAppDbContext;
        
        #endregion


        #region Ctor
        public ReplyServices(SkinHubAppDbContext skinHubAppDbContext)
        {
            _skinHubAppDbContext = skinHubAppDbContext;
    
        }
        #endregion


        #region Methods
        public async Task<long> CreateReply(CreateReplyDto model)
        {
           if(model != null)
           {
                var createReply = new Reply
                {
                    ReplyBody = model.ReplyBody,
                    CreatedOn = model.CreatedOn,
                    Author =  model.Author,
                    CommentID = model.CommentID
                };
                await _skinHubAppDbContext.AddAsync(createReply);
                await _skinHubAppDbContext.SaveChangesAsync();
                return createReply.ID;
           }
           return 0;
        }

        public async Task<bool> DeleteReply(long Id)
        {
            var replyToDelete = await _skinHubAppDbContext.Reply.FindAsync(Id);
            if(replyToDelete != null)
            {
                 _skinHubAppDbContext.Reply.Remove(replyToDelete);
                 await _skinHubAppDbContext.SaveChangesAsync();
                 return true;
            }
            return false;
        }

        public async Task<IEnumerable<ReplyDto>> GetAllReplies()
        {
            var allReply = await _skinHubAppDbContext.Reply.Include(c => c.Comment).ToListAsync();
            if(allReply.Count() > 0)
            {
                var model = new List<ReplyDto>();
                model.AddRange(allReply.OrderBy(x => x.CreatedOn).Select(m => new ReplyDto()
                {
                    ID = m.ID,
                    ReplyBody = m.ReplyBody,
                    CreatedOn = m.CreatedOn,
                    Author = m.Author,
                    CommentID = m.CommentID,
                    Comment = m.Comment.CommentBody,
                }));
                return model;
            }
            return null;
        }

        public async Task<IEnumerable<ReplyDto>> GetAllRepliesByAuthor(string author)
        {
            var allReply = await _skinHubAppDbContext.Reply.Include(c => c.Comment).Where(c => c.Author == author).ToListAsync();
            if(allReply.Count() > 0)
            {
                var model = new List<ReplyDto>();
                model.AddRange(allReply.OrderBy(x => x.CreatedOn).Select(m => new ReplyDto()
                {
                     ID = m.ID,
                    ReplyBody = m.ReplyBody,
                    CreatedOn = m.CreatedOn,
                    Author = m.Author,
                    CommentID = m.CommentID,
                    Comment = m.Comment.CommentBody,
                }));
                return model;
            }
            return null;
        }

        public async Task<ReplyDto> GetReplyByID(long id)
        {
           var reply = await _skinHubAppDbContext.Reply.Include(p => p.Comment).Where(p => p.ID == id).FirstOrDefaultAsync();
           if(reply != null)
           {
               var model = new ReplyDto
               {
                    ID = reply.ID,
                    ReplyBody = reply.ReplyBody,
                    CreatedOn = reply.CreatedOn,
                    Author = reply.Author,
                    CommentID = reply.CommentID,
                    Comment = reply.Comment.CommentBody,
               };
               return model;
           }
           return null;
        }

        public async Task<IEnumerable<ReplyDto>> GetReplyByCommentID(long id)
        {
            var allReply = await _skinHubAppDbContext.Reply.Include(c => c.Comment).Where(c => c.CommentID == id).ToListAsync();
            if(allReply.Count() > 0)
            {
                var model = new List<ReplyDto>();
                model.AddRange(allReply.OrderBy(x => x.CreatedOn).Select(m => new ReplyDto()
                {
                    ID = m.ID,
                    ReplyBody = m.ReplyBody,
                    CreatedOn = m.CreatedOn,
                    Author = m.Author,
                    CommentID = m.CommentID,
                    Comment = m.Comment.CommentBody,
                }));
                return model;
            }
            return null;
        }
        public async Task<long> UpdateReply(ReplyDto model)
        {
            var replyToUpdate = await _skinHubAppDbContext.Reply.FindAsync(model.ID);
            if(replyToUpdate != null)
            {
                 _skinHubAppDbContext.Entry(replyToUpdate).State = EntityState.Modified;
                 await _skinHubAppDbContext.SaveChangesAsync();
                 return model.ID;
            }
            return 0;
        }

        #endregion

        #region Validation   
        public async Task<bool> IsNameExist(string name, long id)
        {
            if(await _skinHubAppDbContext.Reply.AnyAsync( a => a.ReplyBody == name && a.CommentID == id))
                return true;
            return false;
        }

        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ReplyServices() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: unReply the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }

}