namespace ToDoList.Test.UnitTests;
using NSubstitute;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.WebApi.Controllers;
using ToDoList.Persistence.Repositories;
using ToDoList.Domain.Models;
using Microsoft.AspNetCore.Http;

public class PostUnitTests
{
    [Fact]
    public async Task Post_CreateValidRequest_ReturnsCreatedAtAction()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        var request = new ToDoItemCreateRequestDto(
            Name: "Jmeno",
            Description: "Popis",
            IsCompleted: false,
            Category: "test"
        );

        var createdItem = new ToDoItem
        {
            ToDoItemId = 1,
            Name = request.Name,
            Description = request.Description,
            IsCompleted = request.IsCompleted,
            Category = request.Category
        };

        repositoryMock.Create(Arg.Any<ToDoItem>()).Returns(Task.CompletedTask);


        // Act
        var result = await controller.Create(request);
        var resultResult = result.Result as CreatedAtActionResult;
        var value = resultResult?.Value as ToDoItemGetResponseDto;


        // Assert
        Assert.IsType<CreatedAtActionResult>(resultResult);
        Assert.NotNull(value);
        Assert.Equal(request.Description, value.Description);
        Assert.Equal(request.IsCompleted, value.IsCompleted);
        Assert.Equal(request.Name, value.Name);
        repositoryMock.Received(1).Create(Arg.Any<ToDoItem>());

    }

    [Fact]
    public async Task Post_CreateUnhandledException_ReturnsInternalServerError()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        var request = new ToDoItemCreateRequestDto(
            Name: "Jmeno",
            Description: "Popis",
            IsCompleted: false,
            Category: "test"
        );
        repositoryMock.When(r => r.Create(Arg.Any<ToDoItem>())).Do(r => throw new Exception());

        // Act
        var result = await controller.Create(request);
        var resultResult = result.Result;

        // Assert
        Assert.IsType<ObjectResult>(resultResult);
        Assert.Equivalent(new StatusCodeResult(StatusCodes.Status500InternalServerError), resultResult);
    }
}
