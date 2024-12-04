namespace ToDoList.Persistence.Repositories;

using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Models;
public class ToDoItemsRepository : IRepositoryAsync<ToDoItem>
{
    private readonly ToDoItemsContext context;

    public ToDoItemsRepository(ToDoItemsContext context) => this.context = context;

    public async Task Create(ToDoItem item)
    {
        await context.ToDoItems.AddAsync(item);
        await context.SaveChangesAsync();

    }

    public async Task<IEnumerable<ToDoItem>> ReadAll() => await context.ToDoItems.ToListAsync();

    public async Task<ToDoItem?> ReadById(int id) => await context.ToDoItems.FindAsync(id);

    public async Task Update(ToDoItem item)
    {
        var foundItem = context.ToDoItems.Find(item.ToDoItemId) ?? throw new ArgumentOutOfRangeException($"ToDo item with ID {item.ToDoItemId} not found.");
        context.Entry(item).CurrentValues.SetValues(item);
        await context.SaveChangesAsync();
    }

    // public void DeleteById(ToDoItem item)
    // {
    //     context.ToDoItems.Remove(item);
    //     context.SaveChanges();
    // }


    public async Task Delete(ToDoItem item)
    {
        context.ToDoItems.Remove(item);
        await context.SaveChangesAsync();
    }
}
