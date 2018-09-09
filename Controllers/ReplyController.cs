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
    public class ReplyController : Controller
    {
         #region Fields
        private readonly IReplyServices _replyServices;
        #endregion

        #region Ctor
        
        public ReplyController(IReplyServices replyServices)
        {
            _replyServices = replyServices;
        }
        #endregion

        #region ActionMethods

        #region Reply and PUT

        [HttpPost]
        [Route("[action]")]
        [Produces(typeof(ReplyDto))]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> Create([FromBody] CreateReplyDto model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var checkName = await _replyServices.IsNameExist(model.ReplyBody, model.CommentID);
                    if(checkName == true)
                    {
                        return BadRequest("Sorry!, This name already exists on our database. Choose another name");
                    }

                    var createReply = await _replyServices.CreateReply(model);
                    return StatusCode(201, $"Reply created Successfully.");
                }
                return BadRequest("Sorry! Your task cannot be completed");
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}, Error! Your task failed, Please try again");
            }
        }
        
        [HttpPut]
        [Route("[action]")]
        [Produces(typeof(ReplyDto))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] ReplyDto model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var update = await _replyServices.GetReplyByID(model.ID);
                    if(update != null)
                    {
                        await _replyServices.UpdateReply(model);
                        return Ok($"Reply updated Successfully");
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
        [HttpGet]
        [Route("[action]")]
        [Produces(typeof(IEnumerable<ReplyDto>))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllReplies()
        {
            try
            {
                var allReplys = await _replyServices.GetAllReplies();
                if(allReplys != null)
                {
                    return Ok(allReplys);
                }
                return BadRequest("Sorry!, No Data was fetched, Please try again");

            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}, Error! Your task failed, Please try again");
            }
        }

        [HttpGet]
        [Route("[action]")]
        [Produces(typeof(IEnumerable<ReplyDto>))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetReplyByCommentID(long Id)
        {
            try
            {
                var allReplys = await _replyServices.GetReplyByCommentID(Id);
                if(allReplys != null)
                {
                    return Ok(allReplys);
                }
                return BadRequest("Sorry!, No Data was fetched, Please try again");

            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}, Error! Your task failed, Please try again");
            }
        }

        

        [HttpGet]
        [Route("[action]")]
        [Produces(typeof(IEnumerable<ReplyDto>))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllRepliesByAuthor(string author)
        {
            try
            {
                var allReplys = await _replyServices.GetAllRepliesByAuthor(author);
                if(allReplys != null)
                {
                    return Ok(allReplys);
                }
                return BadRequest("Sorry!, No Data was fetched, Please try again");

            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}, Error! Your task failed, Please try again");
            }
        }


        [HttpGet]
        [Route("[action]")]
        [Produces(typeof(ReplyDto))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetReplyByID(long Id)
        {
            try
            {
                var reply = await _replyServices.GetReplyByID(Id);
                if(reply != null)
                {
                    return Ok(reply);
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