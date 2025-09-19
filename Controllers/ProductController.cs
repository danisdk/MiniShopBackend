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
public class ProductController : Controller
{
    
    private ApplicationContext _context;
    private IPaginationService<ProductResponse> _paginationService;
    private IBaseService<Product> _productService;
    private IBaseService<Order> _orderService;
    private IBaseService<OrderProduct> _orderProductService;
    private IMapper _mapper;
    
    public ProductController(
        ApplicationContext context, IBaseService<Product> productService, IBaseService<Order> orderService,
        IBaseService<OrderProduct> orderProductService, IPaginationService<ProductResponse> paginationService, 
        IMapper mapper
        )
    {
        _context = context;
        _paginationService = paginationService;
        _productService = productService;
        _orderService = orderService;
        _orderProductService = orderProductService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetProductsList(int pageNumber = 1, int pageSize = 100)
    {
        IQueryable<ProductResponse> productsQuery = _context.Products.OrderByDescending(
            product => product.Id
            ).ProjectTo<ProductResponse>(_mapper.ConfigurationProvider).AsQueryable();
        PagedResult<ProductResponse> result = await _paginationService.GetPagedAsync(productsQuery, pageNumber, pageSize);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        ProductResponse? productResponse = await _context.Products.ProjectTo<ProductResponse>(
            _mapper.ConfigurationProvider
            ).FirstOrDefaultAsync(p => p.Id == id);
        if (productResponse is null)
        {
            return NotFound();
        }
        return Ok(productResponse);
    }

    [Authorize]
    [HttpPost("create")]
    public async Task<IActionResult> CreateProduct([FromBody] ProductRequest productRequest)
    {
        Product product = _mapper.Map<ProductRequest, Product>(productRequest);
        await _productService.AddAsync(product);
        if (product.Id > 0)
        {
            return Created($"/api/product/{product.Id}", await GetProduct(product.Id));
        }
        return BadRequest();
    }
    
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductRequest productRequest)
    {
        Product? product = await _context.Products.FindAsync(id);
        if (product is null)
        {
            return NotFound();
        }
        Product productForUpdate = _mapper.Map<ProductRequest, Product>(productRequest);
        await _productService.UpdateAsync(product, productForUpdate);
        return await GetProduct(product.Id);
    }

    [Authorize]
    [HttpPost("add-product-in-order")]
    public async Task<IActionResult> AddProductInOrder([FromBody] int productId)
    {
        User? currentUser = HttpContext.Items["CurrentUser"] as User;
        if (currentUser is null)
        {
            return Unauthorized();
        }
        Order? order = await _context.Orders.Where(o => o.UserId == currentUser.Id && o.IsPaid == false).FirstOrDefaultAsync();
        if (order is null)
        {
            order = new Order();
            try
            {
                await _orderService.AddAsync(order);
            }
            catch (UserUnauthorizedException)
            {
                return Unauthorized();
            }
        }

        OrderProduct? orderProduct = await _context.OrderProducts.Where(op => op.OrderId == order.Id && op.ProductId == productId).FirstOrDefaultAsync();
        if (orderProduct is null)
        {
            orderProduct = new OrderProduct();
            orderProduct.OrderId = order.Id;
            orderProduct.ProductId = productId;
            orderProduct.Quantity = 1;
            await _orderProductService.AddAsync(orderProduct);
        }
        else
        {
            OrderProduct orderProductForUpdate = new OrderProduct();
            orderProductForUpdate.Quantity = orderProduct.Quantity + 1;
            await _orderProductService.UpdateAsync(orderProduct, orderProductForUpdate);
        }

        _orderProductService.SetContextValue("order", order);
        return Ok("Товар успешно добавлен в корзину");
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        Product? product = await _context.Products.FindAsync(id);
        if (product is not null)
        {
            await _productService.DeleteAsync(product);
            return Ok();
        }
        return NotFound();
    }
}