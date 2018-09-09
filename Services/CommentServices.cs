using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SkinHubApp.Data;
using SkinHubApp.DTOs;
using SkinHubApp.Models;

namespace SkinHubApp.Services
{
    public class CommentServices : ICommentServices
    {
        
         #region Fields
            private readonly SkinHubAppDbContext _skinHubAppDbContext;
        
        #endregion


        #region Ctor
        public CommentServices(SkinHubAppDbContext skinHubAppDbContext)
        {
            _skinHubAppDbContext = skinHubAppDbContext;
    
        }
        #endregion


        #region Methods
        public async Task<long> CreateComment(CreateCommentDto model)
        {
           if(model != null)
           {
                var createComment = new Comment
                {
                    CommentBody = model.CommentBody,
                    CreatedOn = model.CreatedOn,
                    Author =  model.Author,
                    PostID = model.PostID
                };
                await _skinHubAppDbContext.AddAsync(createComment);
                await _skinHubAppDbContext.SaveChangesAsync();
                return createComment.ID;
           }
           return 0;
        }

        public async Task<bool> DeleteComment(long Id)
        {
            var CommentToDelete = await _skinHubAppDbContext.Comment.FindAsync(Id);
            if(CommentToDelete != null)
            {
                 _skinHubAppDbContext.Comment.Remove(CommentToDelete);
                 await _skinHubAppDbContext.SaveChangesAsync();
                 return true;
            }
            return false;
        }

        public async Task<IEnumerable<CommentDto>> GetAllComments()
        {
            var allComment = await _skinHubAppDbContext.Comment.Include(c => c.Post).ToListAsync();
            if(allComment.Count() > 0)
            {
                var model = new List<CommentDto>();
                model.AddRange(allComment.OrderBy(x => x.CreatedOn).Select(m => new CommentDto()
                {
                    ID = m.ID,
                    CommentBody = m.CommentBody,
                    CreatedOn = m.CreatedOn,
                    Author = m.Author,
                    PostID = m.PostID,
                    Post = m.Post.Title,
                }));
                return model;
            }
            return null;
        }

        public async Task<IEnumerable<CommentDto>> GetAllCommentByAuthor(string author)
        {
            var allComment = await _skinHubAppDbContext.Comment.Include(c => c.Post).Where(c => c.Author == author).ToListAsync();
            if(allComment.Count() > 0)
            {
                var model = new List<CommentDto>();
                model.AddRange(allComment.OrderBy(x => x.CreatedOn).Select(m => new CommentDto()
                {
                    ID = m.ID,
                    CommentBody = m.CommentBody,
                    CreatedOn = m.CreatedOn,
                    Author = m.Author,
                    PostID = m.PostID,
                    Post = m.Post.Title,
                }));
                return model;
            }
            return null;
        }

        public async Task<CommentDto> GetCommentByID(long id)
        {
           var comment = await _skinHubAppDbContext.Comment.Include(p => p.Post).Where(p => p.ID == id).FirstOrDefaultAsync();
           if(comment != null)
           {
               var model = new CommentDto
               {
                    ID = comment.ID,
                    CommentBody = comment.CommentBody,
                    CreatedOn = comment.CreatedOn,
                    Author = comment.Author,
                    PostID = comment.PostID,
                    Post = comment.Post.Title,
               };
               return model;
           }
           return null;
        }

        public async Task<IEnumerable<CommentDto>> GetCommentByPostID(long id)
        {
            var allComment = await _skinHubAppDbContext.Comment.Include(c => c.Post).Where(c => c.PostID == id).ToListAsync();
            if(allComment.Count() > 0)
            {
                var model = new List<CommentDto>();
                model.AddRange(allComment.OrderBy(x => x.CreatedOn).Select(m => new CommentDto()
                {
                    ID = m.ID,
                    CommentBody = m.CommentBody,
                    CreatedOn = m.CreatedOn,
                    Author = m.Author,
                    PostID = m.PostID,
                    Post = m.Post.Title,
                }));
                return model;
            }
            return null;
        }
        public async Task<long> UpdateComment(CommentDto model)
        {
            var CommentToUpdate = await _skinHubAppDbContext.Comment.FindAsync(model.ID);
            if(CommentToUpdate != null)
            {
                 _skinHubAppDbContext.Entry(CommentToUpdate).State = EntityState.Modified;
                 await _skinHubAppDbContext.SaveChangesAsync();
                 return model.ID;
            }
            return 0;
        }

        #endregion

        #region Validation   
        public async Task<bool> IsNameExist(string name, long id)
        {
            if(await _skinHubAppDbContext.Comment.AnyAsync( a => a.CommentBody == name && a.PostID == id))
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
        // ~CommentServices() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }

}