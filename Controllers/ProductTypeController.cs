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
    public class ProductTypeController : Controller
    {
         #region Fields
        private readonly IProductTypeServices _productTypeServices;
        #endregion

        #region Ctor
        
        public ProductTypeController(IProductTypeServices productTypeServices)
        {
            _productTypeServices = productTypeServices;
        }
        #endregion

        #region ActionMethods

        #region POST and PUT

        [HttpPost]
        [Route("[action]")]
        [Produces(typeof(ProductTypeDto))]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> Create([FromBody] CreateProductTypeDto model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var checkName = await _productTypeServices.IsNameExist(model.Name, model.ColorTypeID);
                    if(checkName == true)
                    {
                        return BadRequest("Sorry!, This name already exists on our database. Choose another name");
                    }

                    var createColor = await _productTypeServices.CreateProductType(model);
                    return StatusCode(201, $"{model.Name} created Successfully.");
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
        [Produces(typeof(ProductTypeDto))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] ProductTypeDto model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var update = await _productTypeServices.GetProductTypeByID(model.ID);
                    if(update != null)
                    {
                        await _productTypeServices.UpdateProductType(model);
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

        #region GET
        [HttpGet]
        [Route("[action]")]
        [Produces(typeof(IEnumerable<ProductTypeDto>))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllProductTypes()
        {
            try
            {
                var allProduct = await _productTypeServices.GetAllProductTypes();
                if(allProduct != null)
                {
                    return Ok(allProduct);
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
        [Produces(typeof(IEnumerable<ProductTypeDto>))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllProductTypesByColorID(int Id)
        {
            try
            {
                var allProduct = await _productTypeServices.GetProductTypesByColorTypeID(Id);
                if(allProduct != null)
                {
                    return Ok(allProduct);
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
        [Produces(typeof(ProductTypeDto))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProductTypeByID(int Id)
        {
            try
            {
                var product = await _productTypeServices.GetProductTypeByID(Id);
                if(product != null)
                {
                    return Ok(product);
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