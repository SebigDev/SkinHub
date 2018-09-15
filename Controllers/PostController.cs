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
                if(ModelState.IsValid)
                {
                    var update = await _postServices.GetPostByID(model.ID);
                    if(update != null)
                    {
                        await _postServices.UpdatePost(model);
                        return Ok($"{model.Title} updated Successfully");
                    }
                }
                return BadRequest("Update failed, Please try again");

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
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        [Produces(typeof(IEnumerable<PostDto>))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPostByProductListTypeID(int Id)
        {
            try
            {
                var allPosts = await _postServices.GetPostByProductListTypeID(Id);
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
        /// <param name="Id"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("[action]")]
        [Produces(typeof(PostDto))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPostByID(long Id)
        {
            try
            {
                var color = await _postServices.GetPostByID(Id);
                if(color != null)
                {
                    return Ok(color);
                }
                return BadRequest($"Sorry!, No Data with Id: {Id} found, Please try again");

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