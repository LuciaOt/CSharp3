namespace ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;

public record ToDoItemUpdateRequestDto(int Id, string Name, string Description, bool IsCompleted)
{
    public ToDoItem ToDomain() => new()
    {
        ToDoItemId = Id,
        Name = Name,
        Description = Description,
        IsCompleted = IsCompleted
    };

}

