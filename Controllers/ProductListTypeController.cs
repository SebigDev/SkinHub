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
    public class ProductListTypeController : Controller
    {
         #region Fields
        private readonly IProductListTypeServices _productTypeServices;
        #endregion

        #region Ctor
        
        public ProductListTypeController(IProductListTypeServices productTypeServices)
        {
            _productTypeServices = productTypeServices;
        }
        #endregion

        #region ActionMethods

        #region POST and PUT

        [HttpPost]
        [Route("[action]")]
        [Produces(typeof(ProductListTypeDto))]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> Create([FromBody] CreateProductListTypeDto model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var checkName = await _productTypeServices.IsNameExist(model.Name, model.ProductTypeID);
                    if(checkName == true)
                    {
                        return BadRequest("Sorry!, This name already exists on our database. Choose another name");
                    }

                    var createColor = await _productTypeServices.CreateProductListType(model);
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
        [Produces(typeof(ProductListTypeDto))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] ProductListTypeDto model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var update = await _productTypeServices.GetProductListTypeByID(model.ID);
                    if(update != null)
                    {
                        await _productTypeServices.UpdateProductListType(model);
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
        [Produces(typeof(IEnumerable<ProductListTypeDto>))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllProductListTypes()
        {
            try
            {
                var allProduct = await _productTypeServices.GetAllProductListTypes();
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
        [Produces(typeof(IEnumerable<ProductListTypeDto>))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProductListTypesByProductTypeID(int Id)
        {
            try
            {
                var allProduct = await _productTypeServices.GetProductListTypesByProductTypeID(Id);
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
        [Produces(typeof(ProductListTypeDto))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProductListTypeByID(int Id)
        {
            try
            {
                var product = await _productTypeServices.GetProductListTypeByID(Id);
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