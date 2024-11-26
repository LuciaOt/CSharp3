namespace ToDoList.Test.UnitTests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi.Controllers;

public class GetByIDUnitTests
{
    [Fact]
    public async Task Get_ReadByIdWhenSomeItemAvailable_ReturnsOk()
    {

        //arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        var testId = 0;

        var sampleItem = new ToDoItem
        {
            ToDoItemId = testId,
            Name = "Sample Name",
            Description = "Sample Description",
            IsCompleted = true,
            Category = "test"

        };
        // repositoryMock.ReadById(testId).Returns(Task.FromResult(sampleItem));
        repositoryMock.ReadById(testId).Returns(sampleItem);

        //act
        var result = await controller.ReadById(testId);

        //assert
        Assert.IsType<OkObjectResult>(result.Result);
        Assert.NotNull(result.Result);
        // Assert.NotNull(result?.Value);
        repositoryMock.Received(1).ReadById(testId);

    }

    [Fact]
    public async Task Get_ReadByIdWhenItemIsNull_ReturnsNotFound()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        var testId = 0;
        // repositoryMock.ReadById(testId).Returns((ToDoItem)null);
        repositoryMock.ReadById(testId).Returns(Task.FromResult<ToDoItem?>(null));


        // Act
        var result = await controller.ReadById(testId);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
        repositoryMock.Received(1).ReadById(testId);
    }

    [Fact]
    public async Task Get_ReadByIdUnhandledException_ReturnsInternalServerError()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        var testId = 1;
        repositoryMock.ReadById(testId).Throws(new Exception("test exception"));

        // Act
        var result = await controller.ReadById(testId);

        // Assert
        Assert.IsType<ObjectResult>(result.Result);
        Assert.Equivalent(new StatusCodeResult(StatusCodes.Status500InternalServerError), result.Result);
        repositoryMock.Received(1).ReadById(testId);

    }
}






