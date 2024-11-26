namespace ToDoList.Persistence.Repositories;

public interface IRepositoryAsync<T> where T : class
{
    Task Create(T item);
    Task<IEnumerable<T>> ReadAll();
    Task<T?> ReadById(int id);
    Task Update(T item);
    Task Delete(T item);
}

