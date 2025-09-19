namespace MiniShop.Services;

public interface IBaseService<T> where T : class
{
    
    Task AddAsync(T entity);
    Task AfterAddAsync(T entity);
    void AddRange(IEnumerable<T> entities);

    Task UpdateAsync(T entityFromDb, T updatedEntity);
    Task AfterUpdateAsync(T entity);
    void UpdateRange(IEnumerable<T> entities);

    Task DeleteAsync(T entity);
    Task AfterDeleteAsync(T entity);
    void DeleteRange(IEnumerable<T> entities);

    void Save();
    
    Task SaveAsync();

    void SetContextValue(string key, object? value);

    TVal? GetContextValue<TVal>(string key);

    TVal? PopContextValue<TVal>(string key);
    
}