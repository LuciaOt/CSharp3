namespace ToDoList.Test;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi.Controllers;


public class GetUnitTests
{
    [Fact]
    public void Get_AllItems_ReturnsAllItems()
    {

        //arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(null, repositoryMock);

        // var controller = new ToDoItemsController();
        var items = new List<ToDoItem>
        {
                new ToDoItem { ToDoItemId = 1, Name = "Task 1", Description = "Description 1", IsCompleted = false },
                new ToDoItem { ToDoItemId = 2, Name = "Task 2", Description = "Description 2", IsCompleted = true }
        };
        // var toDoItem = new ToDoItem()
        // {
        //     ToDoItemId = 1,
        //     Name = "Test name",
        //     Description = "Test description",
        //     IsCompleted = false
        // };

        // ToDoItemsController.items.Add(toDoItem);
        repositoryMock.ReadAll().Returns(items);


        //act
        var result = controller.Read();
        var value = result.GetValue();
        var resultResult = result.Result;

        //assert
        Assert.IsType<OkObjectResult>(resultResult);
        Assert.NotNull(value);
        // var firstItem = value.First();
        // Assert.NotNull(value);
        Assert.Equal(2, value.Count());
        Assert.Equal("Task 1", value.ElementAt(0).Name);
        Assert.Equal("Task 2", value.ElementAt(1).Name);

        // Assert.Equal(toDoItem.ToDoItemId, firstItem.Id);
        // Assert.Equal(toDoItem.Description, firstItem.Description);
        // Assert.Equal(toDoItem.IsCompleted, firstItem.IsCompleted);
        // Assert.Equal(toDoItem.Name, firstItem.Name);

    }

    // [Fact]
    // public void Get_AllItems_ReturnsNotFound()
    // {
    //     //arrange
    //     var controller = new ToDoItemsController();
    //     //ToDoItemsController.items.Clear();

    //     //act
    //     var result = controller.Read();
    //     var resultResult = result.Result;

    //     //assert
    //     Assert.IsType<NotFoundResult>(resultResult);
    // }
}






