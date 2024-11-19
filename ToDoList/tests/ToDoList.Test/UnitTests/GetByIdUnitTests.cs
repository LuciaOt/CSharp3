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
    public void Get_ReadByIdWhenSomeItemAvailable_ReturnsOk()
    {

        //arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        var testId = 0;

        var sampleItem = new ToDoItem
        {
            ToDoItemId = testId,
            Name = "Sample Name",
            Description = "Sample Description",
            IsCompleted = true
        };

        repositoryMock.ReadById(testId).Returns(sampleItem);

        //act
        var result = controller.ReadById(testId);
        // var resultResult = result.Result as OkObjectResult;

        //assert
        Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(result?.Value);
        repositoryMock.Received(1).ReadById(testId);

    }

    [Fact]
    public void Get_ReadByIdWhenItemIsNull_ReturnsNotFound()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        var testId = 0;
        repositoryMock.ReadById(testId).Returns((ToDoItem)null);

        // Act
        var result = controller.ReadById(testId);
        // var resultResult = result.Result as NotFoundResult;

        // Assert
        Assert.IsType<NotFoundResult>(result);
        repositoryMock.Received(1).ReadById(testId);
    }

    [Fact]
    public void Get_ReadByIdUnhandledException_ReturnsInternalServerError()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        var testId = 1;
        repositoryMock.ReadById(testId).Throws(new Exception("test exception"));

        // Act
        var result = controller.ReadById(testId);
        // var resultResult = result.Result as ObjectResult;

        // Assert
        Assert.IsType<ObjectResult>(result);
        Assert.Equivalent(new StatusCodeResult(StatusCodes.Status500InternalServerError), result);
        repositoryMock.Received(1).ReadById(testId);

    }
}






