using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.DTOs;
using backend.Models;
using backend.Services;
using backend.Services.CategoryService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly IProductCategoryService _productCategoryService;

        public CategoryController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        // GET: api/values
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetProductCategoryDTO>>>> GetCategories()
        { 
            return Ok(await _productCategoryService.GetCategories());
        }

        // GET api/values/5
        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<ProductCategory>>> Get(int id)
        {
            return Ok(await _productCategoryService.GetCategoryById(id));
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<ProductCategory>>> PostCategory([FromBody]PostProductCategoryDto category)
        { 
            return Ok(await _productCategoryService.AddCategory(category));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

