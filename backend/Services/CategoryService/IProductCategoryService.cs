using System;
using backend.DTOs;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Services.CategoryService
{
	public interface IProductCategoryService
	{
        Task<ServiceResponse<List<ProductCategory>>> GetCategories();
        Task<ServiceResponse<ProductCategory>> GetCategoryById(int id);
        Task<ServiceResponse<PostProductCategoryDto>> AddCategory(PostProductCategoryDto productCategory);
    }
}

