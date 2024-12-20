using System;
using AutoMapper;
using backend.DTOs;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.CategoryService
{
	public class ProductCategoryService : IProductCategoryService
	{
        private readonly AlexContext _context;
        private readonly IMapper _mapper;

        public ProductCategoryService(AlexContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<ServiceResponse<List<ProductCategory>>> GetCategories()
        {
            var serviceResponse = new ServiceResponse<List<ProductCategory>>();
            // serviceResponse.Data = await _context.ProductCategories.Include(cat => cat.Products).ToListAsync();
            serviceResponse.Data = await _context.ProductCategories.ToListAsync();

            return (serviceResponse);
        }

        public async Task<ServiceResponse<ProductCategory>> GetCategoryById(int id)
        {
            var serviceResponse = new ServiceResponse<ProductCategory>();
            serviceResponse.Data = await _context.ProductCategories.FindAsync(id);

            return (serviceResponse);
        }

        


        public async Task<ServiceResponse<PostProductCategoryDto>> AddCategory(PostProductCategoryDto category)
        {
            var serviceResponse = new ServiceResponse<PostProductCategoryDto>();
            await _context.ProductCategories.AddAsync(_mapper.Map<ProductCategory>(category));
            await _context.SaveChangesAsync();
            
            serviceResponse.Data = category;
            return serviceResponse;
        }
    }
}

