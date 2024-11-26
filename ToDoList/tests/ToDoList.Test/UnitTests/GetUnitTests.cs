namespace ToDoList.Test.UnitTests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi.Controllers;

public class GetUnitTests
{
    [Fact]
    public async Task Get_ReadWhenSomeItemAvailable_ReturnsOk()
    {

        //arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);

        repositoryMock.ReadAll().Returns(
                    [
                        new ToDoItem{
                            Name = "testName",
                            Description = "testDescription",
                            IsCompleted = false,
                            Category = "test"
                        }
                    ]
                    );

        //act
        var result = await controller.Read();
        var resultResult = result.Result;
        // var value = result.GetValue();

        //assert
        Assert.IsType<OkObjectResult>(resultResult);
        repositoryMock.Received(1).ReadAll();

    }

    [Fact]
    public async Task Get_ReadWhenNoItemAvailable_ReturnsNotFound()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        // repositoryMock.ReadAll().ReturnsNull();
        repositoryMock.ReadAll().Returns(Task.FromResult<IEnumerable<ToDoItem>>(null));

        // Act
        var result = await controller.Read();
        var resultResult = result.Result;

        // Assert
        Assert.IsType<NotFoundObjectResult>(resultResult);
        repositoryMock.Received(1).ReadAll();
    }

    [Fact]
    public async Task Get_ReadUnhandledException_ReturnsInternalServerError()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        repositoryMock.ReadAll().Throws(new Exception());

        // Act
        var result = await controller.Read();
        var resultResult = result.Result;

        // Assert
        Assert.IsType<ObjectResult>(resultResult);
        Assert.Equivalent(new StatusCodeResult(StatusCodes.Status500InternalServerError), resultResult);
        repositoryMock.Received(1).ReadAll();
    }
}






