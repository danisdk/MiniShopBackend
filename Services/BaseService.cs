using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiniShop;
using MiniShop.Models;

namespace MiniShop.Services;

public abstract class BaseService<T> : IBaseService<T> where T : Base
{
    protected readonly Dictionary<string, object?> ContextData = new();
    
    protected readonly ApplicationContext Context;
    protected readonly IHttpContextAccessor HttpContextAccessor;
    protected readonly IMapper Mapper;
    protected readonly ILogger Logger;
    
    protected HttpContext? HttpContext => HttpContextAccessor.HttpContext;
    protected readonly DbSet<T> DbSet;
    protected readonly List<Func<Task>> AfterSaveActions = new();
    protected User? CurrentUser;

    public BaseService(
        ApplicationContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper, ILogger logger
        )
    {
        Context = context;
        DbSet = Context.Set<T>();
        HttpContextAccessor = httpContextAccessor;
        Mapper = mapper;
        Logger = logger;
        SetUser();
    }

    private void SetUser()
    {
        if (HttpContext is not null && HttpContext.Items.ContainsKey("CurrentUser"))
        {
            CurrentUser = HttpContext.Items["CurrentUser"] as User;
        }
    }

    public virtual async Task AddAsync(T entity)
    {
        DbSet.Add(entity);
        AfterSaveActions.Add(() => AfterAddAsync(entity));
        await SaveAsync();
        ContextData.Add("CreatedEntity", entity);
    }

    public virtual Task AfterAddAsync(T entity)
    {
        return Task.CompletedTask;
    }

    public virtual void AddRange(IEnumerable<T> entities) => DbSet.AddRange(entities);

    public virtual async Task UpdateAsync(T entityFromDb, T updatedEntity)
    {
        Mapper.Map(updatedEntity, entityFromDb);
        AfterSaveActions.Add(() => AfterUpdateAsync(entityFromDb));
        await SaveAsync();
        ContextData.Add("UpdatedEntity", entityFromDb);
    }

    public virtual Task AfterUpdateAsync(T entity)
    {
        return Task.CompletedTask;
    }

    public virtual void UpdateRange(IEnumerable<T> entities) => DbSet.UpdateRange(entities);

    public virtual async Task DeleteAsync(T entity) {
        DbSet.Remove(entity);
        AfterSaveActions.Add(() => AfterDeleteAsync(entity));
        await SaveAsync();
    }

    public virtual Task AfterDeleteAsync(T entity)
    {
        return Task.CompletedTask;
    }

    public virtual void DeleteRange(IEnumerable<T> entities) => DbSet.RemoveRange(entities);

    public virtual void Save() => Context.SaveChanges();
    
    public virtual async Task SaveAsync() {
        await Context.SaveChangesAsync();
        foreach (Func<Task> afterSaveAction in AfterSaveActions)
        {
            try
            {
                await afterSaveAction();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Ошибка выполнения AfterSave action для {EntityType}", typeof(T).Name);
            }
        }
        AfterSaveActions.Clear();
    }
    
    public void SetContextValue(string key, object? value)
    {
        ContextData[key] = value;
    }
    
    public TVal? GetContextValue<TVal>(string key)
    {
        if (ContextData.TryGetValue(key, out var val) && val is TVal typed)
            return typed;
        return default;
    }
    
    public TVal? PopContextValue<TVal>(string key)
    {
        if (ContextData.TryGetValue(key, out var val))
        {
            ContextData.Remove(key);
            if (val is TVal typed)
                return typed;
        }
        return default;
    }
    
}