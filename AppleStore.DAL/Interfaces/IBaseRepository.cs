using AppleStore.Domain.Entity;

namespace AppleStore.DAL.Interfaces;

public interface IBaseRepository<T>
{
    Task<bool> Create(T entity);
    
    Task<Device?> GetById(int? id);

    Task<List<Device?>> Select();

    Task<bool> Delete(T entity);

    Task<T> Update(T entity);
}