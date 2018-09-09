using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SkinHubApp.DTOs;

namespace SkinHubApp.Services
{
    public interface IPostServices : IDisposable
    {
         Task<long> CreatePost(CreatePostDto model);

        Task<long> UpdatePost(PostDto model);

        Task<IEnumerable<PostDto>> GetAllPosts();

        Task<PostDto> GetPostByID(long id);

        Task<IEnumerable<PostDto>> GetPostByProductListTypeID(int id);

        Task<IEnumerable<PostDto>> GetAllPostsByAuthor(string author);

        Task<bool> DeletePost(long Id);

        Task<bool> IsNameExist(string name, int id);
    }
}