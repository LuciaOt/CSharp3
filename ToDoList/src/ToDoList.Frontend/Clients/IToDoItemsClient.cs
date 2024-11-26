namespace ToDoList.Frontend.Clients;
using ToDoList.Frontend.Views;

public interface IToDoItemsClient
{
    // public Task<List<ToDoItemView>> ReadItemsAsync();
    public List<ToDoItemView> ReadItems();
}
