using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiniShop.Models;

namespace MiniShop.Services;

public class OrderService : FrozenService<Order>
{
    public OrderService(
        ApplicationContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper, ILogger<OrderService> logger
    ) : base(context, httpContextAccessor, mapper, logger){}

    public override async Task AddAsync(Order entity)
    {
        if (CurrentUser is null)
        {
            throw new UserUnauthorizedException("Пользователь не авторизован");
        }
        if (entity.IsPaid)
        {
            return;
        }
        entity.UserId = CurrentUser.Id;
        await base.AddAsync(entity);
    }

    public override async Task UpdateAsync(Order entity, Order entityToUpdate)
    {
        if (entity.IsPaid)
        {
            return;
        }
        decimal total = await DbSet.Where(
                o => o.Id == entity.Id
                ).Select(
                o => o.OrderProducts.Sum(op => op.Quantity * op.Product.Price)
                ).FirstOrDefaultAsync();
        entity.Total = total;
        entity.IsPaid = entityToUpdate.IsPaid;
        if (entityToUpdate.OrderDateTime is not null)
        {
            entity.OrderDateTime = entityToUpdate.OrderDateTime;
        }
        SetContextValue("IgnoreMap", true);
        await base.UpdateAsync(entity, entityToUpdate);
    }
    
}
