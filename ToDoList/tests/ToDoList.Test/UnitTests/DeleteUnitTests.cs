namespace ToDoList.Test.UnitTests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi.Controllers;
public class DeleteUnitTests
{
    [Fact]
    public void Delete_DeleteByIdValidItemId_ReturnsNoContent()
    {

        //arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        var testId = 1;
        var sampleItem = new ToDoItem
        {
            ToDoItemId = testId,
            Name = "Sample Name",
            Description = "Sample Description",
            IsCompleted = true,
            Category = "test"
        };

        repositoryMock.ReadById(testId).Returns(sampleItem);
        repositoryMock.When(r => r.Delete(Arg.Any<ToDoItem>())).Do(x => { /* nothing*/ });

        //act
        var result = controller.DeleteById(testId);
        // var resultResult = result as NoContentResult;

        //assert
        Assert.IsType<NoContentResult>(result);
        repositoryMock.Received(1).ReadById(testId);
        repositoryMock.Received(1).Delete(Arg.Any<ToDoItem>());
    }

    [Fact]
    public void Delete_DeleteByIdInvalidItemId_ReturnsNotFound()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        var testId = 1;
        repositoryMock.ReadById(testId).Returns((ToDoItem)null);

        // Act
        var result = controller.DeleteById(testId);

        // Assert
        Assert.IsType<NotFoundResult>(result);
        repositoryMock.Received(1).ReadById(testId);
        repositoryMock.DidNotReceive().Delete(Arg.Any<ToDoItem>());
    }

    [Fact]
    public void Delete_DeleteByIdUnhandledException_ReturnsInternalServerError()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        var testId = 1;
        var sampleItem = new ToDoItem
        {
            ToDoItemId = testId,
            Name = "Sample Name",
            Description = "Sample Description",
            IsCompleted = true,
            Category = "test"

        };
        repositoryMock.ReadById(testId).Returns(sampleItem);
        repositoryMock.When(r => r.Delete(Arg.Any<ToDoItem>())).Do(r => throw new Exception());

        // Act
        var result = controller.DeleteById(testId);

        // Assert
        Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
        repositoryMock.Received(1).ReadById(testId);
        repositoryMock.Received(1).Delete(Arg.Any<ToDoItem>());
    }
}







