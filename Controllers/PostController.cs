using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SkinHubApp.DTOs;
using SkinHubApp.Services;

namespace SkinHubApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : Controller
    {
         #region Fields
        private readonly IPostServices _postServices;
        #endregion

        #region Ctor
        
        public PostController(IPostServices postServices)
        {
            _postServices = postServices;
        }
        #endregion

        #region ActionMethods

        #region POST and PUT

        /// <summary>
        /// Creates a Post
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        [Produces(typeof(PostDto))]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> Create([FromBody] CreatePostDto model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var checkName = await _postServices.IsNameExist(model.Title, model.ProductListTypeID);
                    if(checkName == true)
                    {
                        return BadRequest("Sorry!, This name already exists on our database. Choose another name");
                    }

                    var createPost = await _postServices.CreatePost(model);
                    return StatusCode(201, $"{model.Title} created Successfully.");
                }
                return BadRequest("Sorry! Your task cannot be completed");
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}, Error! Your task failed, Please try again");
            }
        }
        
        /// <summary>
        /// Updates a post
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        [Produces(typeof(PostDto))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] PostDto model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Update failed, Please try again");
                var update = await _postServices.GetPostById(model.ID);
                if (update == null) return BadRequest("Update failed, Please try again");
                await _postServices.UpdatePost(model);
                return Ok($"{model.Title} updated Successfully");

            }
            catch (Exception ex)
            {
               return BadRequest($"{ex.Message}, Error! Your task failed, Please try again");
            }
        }

        #endregion

        #region GET

        /// <summary>
        /// Gets all posts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        [Produces(typeof(IEnumerable<PostDto>))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllPosts()
        {
            try
            {
                var allPosts = await _postServices.GetAllPosts();
                if(allPosts != null)
                {
                    return Ok(allPosts);
                }
                return BadRequest("Sorry!, No Data was fetched, Please try again");

            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}, Error! Your task failed, Please try again");
            }
        }

        /// <summary>
        /// Gets all posts by productlist Identity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        [Produces(typeof(IEnumerable<PostDto>))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPostByProductListTypeId(int id)
        {
            try
            {
                var allPosts = await _postServices.GetPostByProductListTypeId(id);
                if(allPosts != null)
                {
                    return Ok(allPosts);
                }
                return BadRequest("Sorry!, No Data was fetched, Please try again");

            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}, Error! Your task failed, Please try again");
            }
        }

        /// <summary>
        /// Gets all post by an author
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Route("[action]")]
        [Produces(typeof(IEnumerable<PostDto>))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllPostsByAuthor(string author)
        {
            try
            {
                var allPosts = await _postServices.GetAllPostsByAuthor(author);
                if(allPosts != null)
                {
                    return Ok(allPosts);
                }
                return BadRequest("Sorry!, No Data was fetched, Please try again");

            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}, Error! Your task failed, Please try again");
            }
        }

        /// <summary>
        /// Gets a post by post identity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("[action]")]
        [Produces(typeof(PostDto))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPostById(long id)
        {
            try
            {
                var color = await _postServices.GetPostById(id);
                if(color != null)
                {
                    return Ok(color);
                }
                return BadRequest($"Sorry!, No Data with Id: {id} found, Please try again");

            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}, Error! Your task failed, Please try again");
            }
        }
        
        /// <summary>
        /// Gets a post by post identity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("[action]")]
        [Produces(typeof(IEnumerable<PostDto>))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllRelatedPosts(long id)
        {
            try
            {
                var post = await _postServices.GetAllRelatedPosts(id);
                if(post != null)
                {
                    return Ok(post);
                }
                return BadRequest($"Sorry!, No Data with Id: {id} found, Please try again");

            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}, Error! Your task failed, Please try again");
            }
        }


        #endregion

        #endregion
    }   
        
}