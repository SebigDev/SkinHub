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
    public class CommentController : Controller
    {
         #region Fields
        private readonly ICommentServices _CommentServices;
        #endregion

        #region Ctor
        
        public CommentController(ICommentServices CommentServices)
        {
            _CommentServices = CommentServices;
        }
        #endregion

        #region ActionMethods

        #region Post and PUT

        /// <summary>
        /// Creates a new comment
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        [Produces(typeof(CommentDto))]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> Create([FromBody] CreateCommentDto model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var checkName = await _CommentServices.IsNameExist(model.CommentBody, model.PostID);
                    if(checkName == true)
                    {
                        return BadRequest("Sorry!, This name already exists on our database. Choose another name");
                    }

                    var createComment = await _CommentServices.CreateComment(model);
                    return StatusCode(201, $"Comment created Successfully.");
                }
                return BadRequest("Sorry! Your task cannot be completed");
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}, Error! Your task failed, Please try again");
            }
        }
        
        /// <summary>
        /// updates a comment
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        [Produces(typeof(CommentDto))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] CommentDto model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var update = await _CommentServices.GetCommentByID(model.ID);
                    if(update != null)
                    {
                        await _CommentServices.UpdateComment(model);
                        return Ok($"Comment updated Successfully");
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
        /// Gets all comments
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        [Produces(typeof(IEnumerable<CommentDto>))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllComments()
        {
            try
            {
                var allComments = await _CommentServices.GetAllComments();
                if(allComments != null)
                {
                    return Ok(allComments);
                }
                return BadRequest("Sorry!, No Data was fetched, Please try again");

            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}, Error! Your task failed, Please try again");
            }
        }

        /// <summary>
        /// Gets all comments by Post Identity
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        [Produces(typeof(IEnumerable<CommentDto>))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCommentByPostID(long Id)
        {
            try
            {
                var allComments = await _CommentServices.GetCommentByPostID(Id);
                if(allComments != null)
                {
                    return Ok(allComments);
                }
                return BadRequest("Sorry!, No Data was fetched, Please try again");

            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}, Error! Your task failed, Please try again");
            }
        }

        /// <summary>
        /// Gets all comment by author
        /// </summary>
        /// <param name="author"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("[action]")]
        [Produces(typeof(IEnumerable<CommentDto>))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllCommentByAuthor(string author)
        {
            try
            {
                var allComments = await _CommentServices.GetAllCommentByAuthor(author);
                if(allComments != null)
                {
                    return Ok(allComments);
                }
                return BadRequest("Sorry!, No Data was fetched, Please try again");

            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}, Error! Your task failed, Please try again");
            }
        }

        /// <summary>
        /// Gets comment by comment Identity
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        [Produces(typeof(CommentDto))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCommentByID(long Id)
        {
            try
            {
                var color = await _CommentServices.GetCommentByID(Id);
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