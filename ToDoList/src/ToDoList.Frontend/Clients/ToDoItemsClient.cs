namespace ToDoList.Frontend.Clients;
using ToDoList.Domain.DTOs;
using ToDoList.Frontend.Views;

// public class ToDoItemsClient(HttpClient httpClient) : IToDoItemsClient
// {
//     public async Task<List<ToDoItemView>> ReadItemsAsync()
//     {
//         var toDoItemsView = new List<ToDoItemView>();
//         var response = await httpClient.GetFromJsonAsync<List<ToDoItemGetResponseDto>>("api/ToDoItems");
//         toDoItemsView = response.Select(dto => new ToDoItemView
//         {
//             ToDoItemId = dto.Id,
//             Name = dto.Name,
//             Description = dto.Description,
//             IsCompleted = dto.IsCompleted
//         }).ToList();
//         return toDoItemsView;
//     }
// }

public class ToDoItemsClient(HttpClient httpClient) : IToDoItemsClient
{
    private readonly HttpClient httpClient = httpClient;

    public List<ToDoItemView> ReadItems()
    {
        var toDoItemsView = new List<ToDoItemView>();
        var response = httpClient.GetFromJsonAsync<List<ToDoItemGetResponseDto>>("api/ToDoItems");
        toDoItemsView = response.Result.Select(dto => new ToDoItemView
        {
            ToDoItemId = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
            IsCompleted = dto.IsCompleted,
            Category = dto.Category
        }).ToList();
        return toDoItemsView;
    }
}
