using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkinHubApp.Data;
using SkinHubApp.DTOs;
using SkinHubApp.Models;

namespace SkinHubApp.Services
{
    public sealed class PostServices : IPostServices
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
            if (model == null) return 0;
            var createPost = new Post
            {
                Title = model.Title,
                Body = model.Body,
                CreatedOn = DateTime.UtcNow,
                Author = model.Author,
                ProductListTypeID = model.ProductListTypeID
            };
            await _skinHubAppDbContext.AddAsync(createPost);
            await _skinHubAppDbContext.SaveChangesAsync();
            return createPost.ID;
        }

        public async Task<bool> DeletePost(long id)
        {
            var postToDelete = await _skinHubAppDbContext.Post.FindAsync(id);
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
            if (!allPost.Any()) return null;
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

        public async Task<PostDto> GetPostById(long id)
        {
           var post = await _skinHubAppDbContext.Post.Include(x =>x.ProductListType).FirstOrDefaultAsync(x =>x.ID == id);
            if (post == null) return null;
            var newPostDto = new PostDto()
            {
                Author = post.Author,
                Title = post.Title,
                Body = post.Body,
                CreatedOn = post.CreatedOn,
                ID = post.ID,
                ProductListTypeID = post.ProductListTypeID,
                ProductListType = post.ProductListType.Name
            };
            return newPostDto;


        }

        public async Task<IEnumerable<PostDto>> GetPostByProductListTypeId(int id)
        {
            var allPost = await _skinHubAppDbContext.Post.Include(c => c.ProductListType).Where(c => c.ProductListTypeID == id).ToListAsync();
            if (!allPost.Any()) return null;
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
        public async Task<long> UpdatePost(PostDto model)
        {
            var postToUpdate = await _skinHubAppDbContext.Post.FindAsync(model.ID);
            if (postToUpdate == null) return 0;
            _skinHubAppDbContext.Entry(postToUpdate).State = EntityState.Modified;
            await _skinHubAppDbContext.SaveChangesAsync();
            return model.ID;
        }

        #endregion

        #region Validation   
        public async Task<bool> IsNameExist(string name, int id)
        {
            return await _skinHubAppDbContext.Post.AnyAsync( a => a.Title == name && a.ProductListTypeID == id);
        }

        public async Task<IEnumerable<PostDto>> GetAllRelatedPosts(long id)
        {
            var postCollection = await _skinHubAppDbContext.Post.Where(p =>p.ID == id).ToListAsync();
            var postDto = new List<PostDto>();
            if (!postCollection.Any()) return null;
            postDto.AddRange(postCollection.GroupBy(c => c.ProductListTypeID).Select(x => new PostDto()
            {
                ID = id,
                ProductListTypeID = x.Key,
                Title = x.ToLookup(a => a.Title).ToString()
            }));
            return postDto;

        }

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