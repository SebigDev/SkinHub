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
    public class GenderTypeController : Controller
    {
        #region Fields
        private readonly IGenderTypeServices _genderTypeServices;
        #endregion

        #region Ctor
        
        public GenderTypeController(IGenderTypeServices genderTypeServices)
        {
            _genderTypeServices = genderTypeServices;
        }
        #endregion

        #region ActionMethods

        #region POST and PUT

        /// <summary>
        /// Creates new gender
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        [Produces(typeof(GenderTypeDto))]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> Create([FromBody] CreateGenderTypeDto model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var checkName = await _genderTypeServices.IsNameExist(model.Name);
                    if(checkName == true)
                    {
                        return BadRequest("Sorry!, This name already exists on our database. Choose another name");
                    }

                    var createGender = await _genderTypeServices.CreateGenderType(model);
                    return StatusCode(201, $"{model.Name} created Successfully.");
                }
                return BadRequest("Sorry! Your task cannot be completed");
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}, Error! Your task failed, Please try again");
            }
        }
        
        /// <summary>
        /// Updates a gender
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        [Produces(typeof(GenderTypeDto))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] GenderTypeDto model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var update = await _genderTypeServices.GetGenderTypeByID(model.ID);
                    if(update != null)
                    {
                        await _genderTypeServices.UpdateGenderType(model);
                        return Ok($"{model.Name} updated Successfully");
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

        /// <summary>
        /// Gets all the gender
        /// </summary>
        /// <returns></returns>
        #region GET
        [HttpGet]
        [Route("[action]")]
        [Produces(typeof(IEnumerable<GenderTypeDto>))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllGenderTypes()
        {
            try
            {
                var allGender = await _genderTypeServices.GetAllGenderTypes();
                if(allGender != null)
                {
                    return Ok(allGender);
                }
                return BadRequest("Sorry!, No Data was fetched, Please try again");

            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}, Error! Your task failed, Please try again");
            }
        }

        /// <summary>
        /// Gets a gender by gender Identity
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("[action]")]
        [Produces(typeof(GenderTypeDto))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetGenderTypeByID(int Id)
        {
            try
            {
                var gender = await _genderTypeServices.GetGenderTypeByID(Id);
                if(gender != null)
                {
                    return Ok(gender);
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