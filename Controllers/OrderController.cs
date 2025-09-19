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
public class OrderController : Controller
{
    private ApplicationContext _context;
    private IPaginationService<OrderResponse> _paginationService;
    private IBaseService<Order> _orderService;
    private IBaseService<OrderProduct> _orderProductService; 
    private IMapper _mapper;
    
    public OrderController(
        ApplicationContext context, IBaseService<Order> orderService, IBaseService<OrderProduct> orderProductService,
        IPaginationService<OrderResponse> paginationService, IMapper mapper
        )
    {
        _context = context;
        _paginationService = paginationService;
        _orderService = orderService;
        _orderProductService = orderProductService;
        _mapper = mapper;
    }
    

    [HttpGet]
    public async Task<IActionResult> GetOrdersList(int pageNumber = 1, int pageSize = 100)
    {
        IQueryable<OrderResponse> ordersQuery = _context.Orders.OrderByDescending(
            order => order.Id
            ).ProjectTo<OrderResponse>(_mapper.ConfigurationProvider).AsQueryable();
        PagedResult<OrderResponse> result = await _paginationService.GetPagedAsync(ordersQuery, pageNumber, pageSize);
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrder(int id)
    {
        OrderResponse? orderResponse = await _context.Orders.ProjectTo<OrderResponse>(
            _mapper.ConfigurationProvider
            ).FirstOrDefaultAsync(o => o.Id == id);
        if (orderResponse is null)
        {
            return NotFound();
        }
        return Ok(orderResponse);
    }

    [Authorize]
    [HttpPost("create")]
    public async Task<IActionResult> CreateOrder()
    {
        Order order = new Order();

        try
        {
            await _orderService.AddAsync(order);
        }
        catch (UserUnauthorizedException)
        {
            return Unauthorized();
        }
        
        if (order.Id > 0)
        {
            return Created($"/api/product/{order.Id}", await GetOrder(order.Id));
        }
        return BadRequest();
    }
    
    [Authorize]
    [HttpPost("{id}/add-product")]
    public async Task<IActionResult> AddProductInOrder(int id, int productId, int quantity, int? orderProductId)
    {
        Order? order = await _context.Orders.FindAsync(id);
        if (order is null)
        {
            return NotFound();
        }
        if (order.IsPaid)
        {
            return BadRequest("Заказ уже оплачен");
        }

        OrderProduct? orderProduct;
        if (orderProductId is not null)
        {
            orderProduct = await _context.OrderProducts.FindAsync(orderProductId);
        }
        else
        {
            orderProduct = await _context.OrderProducts.Where(op => op.OrderId == order.Id && op.ProductId == productId).FirstOrDefaultAsync();
        }

        if (orderProduct is null)
        {
            return BadRequest("Не найден для какого товара заказ");
        }
        OrderProduct orderProductForUpdate = new OrderProduct();
        orderProductForUpdate.Quantity = quantity;
        await _orderProductService.UpdateAsync(orderProduct, orderProductForUpdate);
        _orderProductService.SetContextValue("order", order);
        return Ok(await GetOrder(order.Id));
    }
    
    [Authorize]
    [HttpPost("{id}/paid")]
    public async Task<IActionResult> PaidOrder(int id)
    {
        Order? order = await _context.Orders.FindAsync(id);
        if (order is null)
        {
            return NotFound();
        }

        if (order.IsPaid)
        {
            return BadRequest("Заказ уже оплачен");
        }
        Order orderForUpdate = new Order();
        orderForUpdate.IsPaid = true;
        await _orderService.UpdateAsync(order, orderForUpdate);
        return await GetOrder(order.Id);
    }
    
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrder(int id)
    {
        Order? order = await _context.Orders.FindAsync(id);
        if (order is null)
        {
            return NotFound();
        }

        if (order.IsPaid)
        {
            return BadRequest("Заказ уже оплачен");
        }
        Order orderForUpdate = new Order();
        await _orderService.UpdateAsync(order, orderForUpdate);
        return await GetOrder(order.Id);
    }
    
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        Order? order = await _context.Orders.FindAsync(id);
        if (order is not null)
        {
            await _orderService.DeleteAsync(order);
            return Ok();
        }
        return NotFound();
    }
}