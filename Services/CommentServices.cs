using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SkinHubApp.Data;
using SkinHubApp.DTOs;
using SkinHubApp.Models;

namespace SkinHubApp.Services
{
    public sealed class CommentServices : ICommentServices
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
            if (model == null) return 0;
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

        public async Task<bool> DeleteComment(long id)
        {
            var commentToDelete = await _skinHubAppDbContext.Comment.FindAsync(id);
            if(commentToDelete == null)  return false;
            {
                 _skinHubAppDbContext.Comment.Remove(commentToDelete);
                 await _skinHubAppDbContext.SaveChangesAsync();
                 return true;
            }
           
        }

        public async Task<IEnumerable<CommentDto>> GetAllComments()
        {
            var allComment = await _skinHubAppDbContext.Comment.Include(c => c.Post).ToListAsync();
            if (allComment.Any()) return null;
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

        public async Task<IEnumerable<CommentDto>> GetAllCommentByAuthor(string author)
        {
            var allComment = await _skinHubAppDbContext.Comment.Include(c => c.Post).Where(c => c.Author == author).ToListAsync();
            if (!allComment.Any()) return null;
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

        public async Task<CommentDto> GetCommentByID(long id)
        {
           var comment = await _skinHubAppDbContext.Comment.Include(p => p.Post).Where(p => p.ID == id).FirstOrDefaultAsync();
            if (comment == null) return null;
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

        public async Task<IEnumerable<CommentDto>> GetCommentByPostID(long id)
        {
            var allComment = await _skinHubAppDbContext.Comment.Include(c => c.Post).Where(c => c.PostID == id).ToListAsync();
            if (!allComment.Any()) return null;
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
        public async Task<long> UpdateComment(CommentDto model)
        {
            var commentToUpdate = await _skinHubAppDbContext.Comment.FindAsync(model.ID);
            if(commentToUpdate == null)  return 0;
            {
                 _skinHubAppDbContext.Entry(commentToUpdate).State = EntityState.Modified;
                 await _skinHubAppDbContext.SaveChangesAsync();
                 return model.ID;
            }
           
        }

        #endregion

        #region Validation   
//        public async Task<bool> IsNameExist(string name, long id)
//        {
//            return await _skinHubAppDbContext.Comment.AnyAsync( a => a.PostID == id);
//        }

        #endregion

        #region IDisposable Support
        private bool _disposedValue = false; // To detect redundant calls

        private void Dispose(bool disposing)
        {
            if (_disposedValue) return;
            if (disposing)
            {
                // TODO: dispose managed state (managed objects).
            }

            // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
            // TODO: set large fields to null.

            _disposedValue = true;
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