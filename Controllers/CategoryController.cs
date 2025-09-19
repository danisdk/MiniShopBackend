using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniShop.Models;
using MiniShop.Models.Requests;
using MiniShop.Models.Responses;
using MiniShop.Services;

namespace MiniShop.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : Controller
{
    private ApplicationContext _context;
    private IPaginationService<CategoryResponse> _paginationService;
    private IBaseService<Category> _categoryService;
    private IMapper _mapper;
    
    public CategoryController(
        ApplicationContext context, IBaseService<Category> categoryService, 
        IPaginationService<CategoryResponse> paginationService, IMapper mapper
        )
    {
        _context = context;
        _paginationService = paginationService;
        _categoryService = categoryService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategoryList(int pageNumber = 1, int pageSize = 100, string? filter = "")
    {
        IQueryable<Category> categoriesQuery = _context.Categories.OrderByDescending(c => c.Id);
        if (!string.IsNullOrEmpty(filter))
        {
            categoriesQuery = categoriesQuery.Where(category => category.Name.Contains(filter));
        }
        IQueryable<CategoryResponse> categoriesResponseQuery = 
            categoriesQuery.ProjectTo<CategoryResponse>(_mapper.ConfigurationProvider).AsQueryable();
        PagedResult<CategoryResponse> result = await _paginationService.GetPagedAsync(categoriesResponseQuery, pageNumber, pageSize);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategory(int id)
    {
        CategoryResponse? categoryResponse = await _context.Categories.ProjectTo<CategoryResponse>(
            _mapper.ConfigurationProvider
            ).FirstOrDefaultAsync(c => c.Id == id);
        if (categoryResponse is null)
        {
            return NotFound();
        }
        return Ok(categoryResponse);
    }

    [Authorize]
    [HttpPost("create")]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryRequest categoryRequest)
    {
        Category category = _mapper.Map<CategoryRequest, Category>(categoryRequest);
        await _categoryService.AddAsync(category);
        if (category.Id > 0)
        {
            return Created($"/api/category/{category.Id}", await GetCategory(category.Id));
        }
        return BadRequest();
    }
    
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryRequest categoryRequest)
    {
        Category? category = await _context.Categories.FindAsync(id);
        if (category is null)
        {
            return NotFound();
        }
        Category categoryForUpdate = _mapper.Map<CategoryRequest, Category>(categoryRequest);
        await _categoryService.UpdateAsync(category, categoryForUpdate);
        return await GetCategory(category.Id);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        Category? category = await _context.Categories.FindAsync(id);
        if (category is not null)
        {
            await _categoryService.DeleteAsync(category);
            return Ok();
        }
        return NotFound();
    }
}