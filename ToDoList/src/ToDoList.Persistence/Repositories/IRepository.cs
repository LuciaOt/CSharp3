namespace ToDoList.Persistence.Repositories;

public interface IRepository<T> where T : class
{
    void Create(T item);

    // Read a single item by its ID
    T? Read(int id);

    // Read all items
    IEnumerable<T> ReadAll();

    // Update an existing item
    void Update(T item);

    // Delete an item by its ID
    void Delete(int id);
}
