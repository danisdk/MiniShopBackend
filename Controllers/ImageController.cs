using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniShop.Models;
using MiniShop.Models.Requests;
using MiniShop.Models.Responses;
using MiniShop.Services;
using MiniShop.Services.Structures;
using FileNotFoundException = MiniShop.Services.FileNotFoundException;

namespace MiniShop.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImageController : Controller
{
    private ApplicationContext _context;
    private IPaginationService<ImageResponse> _paginationService;
    private IBaseService<Image> _imageService;
    private IMapper _mapper;
    
    public ImageController(
        ApplicationContext context, IBaseService<Image> imageService, 
        IPaginationService<ImageResponse> paginationService, IMapper mapper
        )
    {
        _context = context;
        _paginationService = paginationService;
        _imageService = imageService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetImagesList(int pageNumber = 1, int pageSize = 100)
    {
        IQueryable<ImageResponse> imagesQuery = _context.Images.OrderByDescending(
            image => image.Id
            ).ProjectTo<ImageResponse>(_mapper.ConfigurationProvider).AsQueryable();
        PagedResult<ImageResponse> result = await _paginationService.GetPagedAsync(imagesQuery, pageNumber, pageSize);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetImage(int id)
    {
        ImageResponse? imageResponse = await _context.Images.ProjectTo<ImageResponse>(
            _mapper.ConfigurationProvider
            ).FirstOrDefaultAsync(i => i.Id == id);
        if (imageResponse is null)
        {
            return NotFound();
        }
        return Ok(imageResponse);
    }

    [Authorize]
    [HttpPost("create")]
    public async Task<IActionResult> CreateImage([FromForm] ImageRequest imageRequest)
    {
        _imageService.SetContextValue("Image", imageRequest.File);
        Image image = _mapper.Map<ImageRequest, Image>(imageRequest);
        try
        {
            await _imageService.AddAsync(image);
        }
        catch (FileNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (FileTooLargeException ex)
        {
            return StatusCode(413, ex.Message);
        }
        
        if (image.Id > 0)
        {
            return Created($"/api/image/{image.Id}", await GetImage(image.Id));
        }
        return BadRequest();
    }
    
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateImage(int id, [FromForm] ImageRequest imageRequest)
    {
        Image? image = await _context.Images.FindAsync(id);
        if (image is null)
        {
            return NotFound();
        }
        _imageService.SetContextValue("Image", imageRequest.File);
        Image imageForUpdate = _mapper.Map<ImageRequest, Image>(imageRequest);
        try
        {
            await _imageService.UpdateAsync(image, imageForUpdate);
        }
        catch (FileNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (FileTooLargeException ex)
        {
            return StatusCode(413, ex.Message);
        }
        return Ok(await GetImage(image.Id));
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteImage(int id)
    {
        Image? image = await _context.Images.FindAsync(id);
        if (image is not null)
        {
            await _imageService.DeleteAsync(image);
            return Ok();
        }
        return NotFound();
    }
}