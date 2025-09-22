using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniShop.Models;
using MiniShop.Models.Responses;
using MiniShop.Services;

namespace MiniShop.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : Controller
{
    
    private ApplicationContext _context;
    private IBaseService<User> _userService;
    private IMapper _mapper;
    
    public UserController(
        ApplicationContext context, IBaseService<User> userService, IMapper mapper
    )
    {
        _context = context;
        _userService = userService;
        _mapper = mapper;
    }
    
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetUser()
    {
        User? currentUser = HttpContext.Items["CurrentUser"] as User;
        if (currentUser == null)
        {
            return Unauthorized();
        }
        return Ok(_mapper.Map<User, UserResponse>(currentUser));
    }
}