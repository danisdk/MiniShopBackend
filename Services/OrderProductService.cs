using AutoMapper;
using MiniShop.Models;

namespace MiniShop.Services;

public class OrderProductService : FrozenService<OrderProduct>
{
    
    protected IBaseService<Order> OrderService;

    public OrderProductService(
        ApplicationContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper,
        ILogger<OrderProductService> logger, IBaseService<Order> orderService
    ) : base(context, httpContextAccessor, mapper, logger)
    {
        OrderService = orderService;
    }

    public override async Task AfterUpdateAsync(OrderProduct entity)
    {
        Order? order = PopContextValue<Order>("order");
        if (order is not null)
        {
            Order orderForUpdate = new Order();
            await OrderService.UpdateAsync(order, orderForUpdate);
        }
        await base.AfterUpdateAsync(entity);
    }
}