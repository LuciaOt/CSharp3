namespace ToDoList.Test;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi.Controllers;



public class PostUnitTests
{
    [Fact]
    public void Post_ValidRequest_ReturnsNewItem()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(null, repositoryMock); // Docasny hack, nez z controlleru odstranime context.
        var request = new ToDoItemCreateRequestDto(
            Name: "Jmeno",
            Description: "Popis",
            IsCompleted: false
        );

        // Act
        var result = controller.Create(request);
        var resultResult = result.Result;
        var value = result.GetValue();

        // Assert
        Assert.IsType<ObjectResult>(resultResult);
        Assert.NotNull(value);
        Assert.Equal(request.Description, value.Description);
        Assert.Equal(request.IsCompleted, value.IsCompleted);
        Assert.Equal(request.Name, value.Name);
    }

    [Fact]
    public void Post_UnhandledException_Returns500()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(null, repositoryMock); // Docasny hack, nez z controlleru odstranime context.
        var request = new ToDoItemCreateRequestDto(
            Name: "Jmeno",
            Description: "Popis",
            IsCompleted: false
        );
        repositoryMock.When(r => r.Create(Arg.Any<ToDoItem>())).Do(r => throw new Exception());

        // Act
        var result = controller.Create(request);
        var resultResult = result.Result;

        // Assert
        Assert.IsType<ObjectResult>(resultResult);
        Assert.Equivalent(new StatusCodeResult(StatusCodes.Status500InternalServerError), resultResult);
    }
}

// public class PostUnitTests
// {
//     [Fact]
//     public void Post_One_Item_ReturnsCreated()
//     {
//         //arrange
//         var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
//         var controller = new ToDoItemsController(repositoryMock);
//         // var controller = new ToDoItemsController();
//         var request = new ToDoItemCreateRequestDto("New Task", "Test description", false);
//         repositoryMock.When(r => r.Create(Arg.Any<ToDoItem>())).Do(r => throw new Exception());

//         //act
//         var result = controller.Create(request);
//         var resultResult = result.Result;

//         //assert
//         var createdResult = Assert.IsType<CreatedAtActionResult>(resultResult);
//         Assert.NotNull(createdResult);
//         Assert.Equal("ReadById", createdResult.ActionName);
//     }
// }






