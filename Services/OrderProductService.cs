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

    public override async Task AfterAddAsync(OrderProduct orderProduct)
    {
        Order? order = PopContextValue<Order>("order");
        if (order is not null)
        {
            await OrderService.UpdateAsync(order, order);
        }
        await base.AfterAddAsync(orderProduct);
    }

    public override async Task UpdateAsync(OrderProduct entity, OrderProduct updatedEntity)
    {
        entity.Quantity = updatedEntity.Quantity;
        SetContextValue("IgnoreMap", true);
        await base.UpdateAsync(entity, updatedEntity);
    }
    
    public override async Task AfterUpdateAsync(OrderProduct entity)
    {
        Order? order = PopContextValue<Order>("order");
        if (order is not null)
        {
            await OrderService.UpdateAsync(order, order);
        }
        await base.AfterUpdateAsync(entity);
    }
}