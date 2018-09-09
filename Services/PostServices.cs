using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkinHubApp.Data;
using SkinHubApp.DTOs;

namespace SkinHubApp.Services
{
    public class PostServices : IPostServices
    {
         #region Fields
            private readonly SkinHubAppDbContext _skinHubAppDbContext;
            private readonly ILogger<PostServices> _logger;
        #endregion


        #region Ctor
        public PostServices(SkinHubAppDbContext skinHubAppDbContext, ILogger<PostServices> logger)
        {
            _skinHubAppDbContext = skinHubAppDbContext;
            _logger = logger;
        }
        #endregion


        #region Methods
        public async Task<long> CreatePost(CreatePostDto model)
        {
           if(model != null)
           {
                var createPost = new PostDto
                {
                    Title = model.Title,
                    Body = model.Body,
                    CreatedOn = DateTime.UtcNow,
                    Author = "Annonymous",
                };
                await _skinHubAppDbContext.AddAsync(createPost);
                await _skinHubAppDbContext.SaveChangesAsync();
                return createPost.ID;
           }
           return 0;
        }

        public async Task<bool> DeletePost(long Id)
        {
            var postToDelete = await _skinHubAppDbContext.Post.FindAsync(Id);
            if(postToDelete != null)
            {
                 _skinHubAppDbContext.Post.Remove(postToDelete);
                 await _skinHubAppDbContext.SaveChangesAsync();
                 return true;
            }
            return false;
        }

        public async Task<IEnumerable<PostDto>> GetAllPosts()
        {
            var allPost = await _skinHubAppDbContext.Post.Include(c => c.ProductListType).ToListAsync();
            if(allPost.Count() > 0)
            {
                var model = new List<PostDto>();
                model.AddRange(allPost.OrderBy(x => x.CreatedOn).Select(m => new PostDto()
                {
                    ID = m.ID,
                    Title = m.Title,
                    Body = m.Body,
                    CreatedOn = m.CreatedOn,
                    Author = m.Author,
                    ProductListTypeID = m.ProductListTypeID,
                    ProductListType = m.ProductListType.Name,
                }));
                return model;
            }
            return null;
        }

        public async Task<IEnumerable<PostDto>> GetAllPostsByAuthor(string author)
        {
            var allPost = await _skinHubAppDbContext.Post.Include(c => c.ProductListType).Where(c => c.Author == author).ToListAsync();
            if(allPost.Count() > 0)
            {
                var model = new List<PostDto>();
                model.AddRange(allPost.OrderBy(x => x.CreatedOn).Select(m => new PostDto()
                {
                    ID = m.ID,
                    Title = m.Title,
                    Body = m.Body,
                    CreatedOn = m.CreatedOn,
                    Author = m.Author,
                    ProductListTypeID = m.ProductListTypeID,
                    ProductListType = m.ProductListType.Name,
                }));
                return model;
            }
            return null;
        }

        public async Task<PostDto> GetPostByID(long id)
        {
           var post = await _skinHubAppDbContext.Post.Where(p => p.ID == id).FirstOrDefaultAsync();
           if(post != null)
           {
               var model = new PostDto
               {
                    ID = post.ID,
                    Title = post.Title,
                    Body = post.Body,
                    CreatedOn = post.CreatedOn,
                    Author = post.Author,
                    ProductListTypeID = post.ProductListTypeID,
                    ProductListType = post.ProductListType.Name, 
               };
               return model;
           }
           return null;
        }

        public async Task<IEnumerable<PostDto>> GetPostByProductListTypeID(int id)
        {
            var allPost = await _skinHubAppDbContext.Post.Include(c => c.ProductListType).Where(c => c.ProductListTypeID == id).ToListAsync();
            if(allPost.Count() > 0)
            {
                var model = new List<PostDto>();
                model.AddRange(allPost.OrderBy(x => x.CreatedOn).Select(m => new PostDto()
                {
                    ID = m.ID,
                    Title = m.Title,
                    Body = m.Body,
                    CreatedOn = m.CreatedOn,
                    Author = m.Author,
                    ProductListTypeID = m.ProductListTypeID,
                    ProductListType = m.ProductListType.Name,
                }));
                return model;
            }
            return null;
        }

        public async Task<IEnumerable<PostDto>> GetPostsByDateCreated(DateTime date)
        {
            var allPost = await _skinHubAppDbContext.Post.Include(c => c.ProductListType).Where(c => c.CreatedOn == date).ToListAsync();
            if(allPost.Count() > 0)
            {
                var model = new List<PostDto>();
                model.AddRange(allPost.OrderBy(x => x.CreatedOn).Select(m => new PostDto()
                {
                    ID = m.ID,
                    Title = m.Title,
                    Body = m.Body,
                    CreatedOn = m.CreatedOn,
                    Author = m.Author,
                    ProductListTypeID = m.ProductListTypeID,
                    ProductListType = m.ProductListType.Name,
                }));
                return model;
            }
            return null;
        }
        public async Task<long> UpdatePost(PostDto model)
        {
            var postToUpdate = await _skinHubAppDbContext.Post.FindAsync(model.ID);
            if(postToUpdate != null)
            {
                 _skinHubAppDbContext.Entry(postToUpdate).State = EntityState.Modified;
                 await _skinHubAppDbContext.SaveChangesAsync();
                 return model.ID;
            }
            return 0;
        }

        #endregion

        #region Validation   
        public async Task<bool> IsNameExist(string name, int id)
        {
            if(await _skinHubAppDbContext.Post.AnyAsync( a => a.Title == name && a.ProductListTypeID == id))
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
        // ~PostServices() {
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