namespace ToDoList.Domain.Models;

public class ToDoItem
{
    public int ToDoItemId { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public bool IsCompleted { get; set; }
}
