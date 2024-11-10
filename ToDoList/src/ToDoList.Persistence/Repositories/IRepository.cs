namespace ToDoList.Persistence.Repositories;

public interface IRepository<T> where T : class
{
    public void Create(T item);
    public T? Read(int id);
    public IEnumerable<T> ReadAll();
    public void Update(T item);
    public void Delete(int id);
}
