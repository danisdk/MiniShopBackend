using AutoMapper;
using MiniShop.Models;

namespace MiniShop.Services;

public abstract class FrozenService<T> : BaseService<T> where T : Frozen
{
    public FrozenService(
        ApplicationContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper, 
        ILogger logger
        ) : base(context, httpContextAccessor, mapper, logger){}
    
    public override async Task AddAsync(T entity)
    {
        entity.DateTimeCreate = DateTime.UtcNow;
        entity.DateTimeUpdate = DateTime.UtcNow;
        if (CurrentUser is not null)
        {
            entity.AuthorCreatedId = CurrentUser.Id;
            entity.AuthorUpdatedId = CurrentUser.Id;
        }
        await base.AddAsync(entity);
    }
    public override async Task UpdateAsync(T entityFromDb, T updatedEntity)
    {
        entityFromDb.DateTimeUpdate = DateTime.UtcNow;
        if (CurrentUser is not null)
        {
            entityFromDb.AuthorCreatedId = CurrentUser.Id;
        }
        await base.UpdateAsync(entityFromDb, updatedEntity);
    }
}