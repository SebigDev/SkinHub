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
    public class ColorTypeController : Controller
    {
         #region Fields
        private readonly IColorTypeServices _colorTypeServices;
        #endregion

        #region Ctor
        
        public ColorTypeController(IColorTypeServices colorTypeServices)
        {
            _colorTypeServices = colorTypeServices;
        }
        #endregion

        #region ActionMethods

        #region POST and PUT

        /// <summary>
        /// Create a new color
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        [Produces(typeof(ColorTypeDto))]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> Create([FromBody] CreateColorTypeDto model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var checkName = await _colorTypeServices.IsNameExist(model.Name, model.GenderTypeID);
                    if(checkName == true)
                    {
                        return BadRequest("Sorry!, This name already exists on our database. Choose another name");
                    }

                    var createColor = await _colorTypeServices.CreateColorType(model);
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
        /// Updates a color
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("[action]")]
        [Produces(typeof(ColorTypeDto))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] ColorTypeDto model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var update = await _colorTypeServices.GetColorTypeByID(model.ID);
                    if(update != null)
                    {
                        await _colorTypeServices.UpdateColorType(model);
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
        /// Gets all colors
        /// </summary>
        /// <returns></returns>
        #region GET
        [HttpGet]
        [Route("[action]")]
        [Produces(typeof(IEnumerable<ColorTypeDto>))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllColorTypes()
        {
            try
            {
                var allColor = await _colorTypeServices.GetAllColorTypes();
                if(allColor != null)
                {
                    return Ok(allColor);
                }
                return BadRequest("Sorry!, No Data was fetched, Please try again");

            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}, Error! Your task failed, Please try again");
            }
        }

        /// <summary>
        /// Gets color by gender identity
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        [Produces(typeof(IEnumerable<ColorTypeDto>))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllColorTypesByGenderID(int Id)
        {
            try
            {
                var allColor = await _colorTypeServices.GetColorTypesByGenderTypeID(Id);
                if(allColor != null)
                {
                    return Ok(allColor);
                }
                return BadRequest("Sorry!, No Data was fetched, Please try again");

            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}, Error! Your task failed, Please try again");
            }
        }

        /// <summary>
        /// Gets color by color idenity
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[action]")]
        [Produces(typeof(ColorTypeDto))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetColorTypeByID(int Id)
        {
            try
            {
                var color = await _colorTypeServices.GetColorTypeByID(Id);
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