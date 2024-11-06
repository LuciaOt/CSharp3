namespace ToDoList.Test;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.Models;
using ToDoList.Persistence;
using ToDoList.WebApi.Controllers;

public class GetTests
{
    [Fact]
    public void Get_AllItems_ReturnsAllItems()
    {
        //arrange
        var controller = new ToDoItemsController();
        var toDoItem = new ToDoItem()
        {
            ToDoItemId = 1,
            Name = "Test name",
            Description = "Test description",
            IsCompleted = false
        };

        ToDoItemsController.items.Add(toDoItem);

        //act
        var result = controller.Read();
        var value = result.GetValue();

        var resultResult = result.Result;

        //assert
        Assert.IsType<OkObjectResult>(resultResult);
        Assert.NotNull(value);

        var firstItem = value.First();
        Assert.Equal(toDoItem.ToDoItemId, firstItem.Id);
        Assert.Equal(toDoItem.Description, firstItem.Description);
        Assert.Equal(toDoItem.IsCompleted, firstItem.IsCompleted);
        Assert.Equal(toDoItem.Name, firstItem.Name);

    }

    [Fact]
    public void Get_AllItems_ReturnsNotFound()
    {
        //arrange
        var controller = new ToDoItemsController();
        //ToDoItemsController.items.Clear();

        //act
        var result = controller.Read();
        var resultResult = result.Result;

        //assert
        Assert.IsType<NotFoundResult>(resultResult);
    }
}






