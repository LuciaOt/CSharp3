namespace ToDoList.Test.UnitTests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi.Controllers;
public class DeleteByIdUnitTests
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
            IsCompleted = true
        };

        repositoryMock.ReadById(testId).Returns(sampleItem);
        repositoryMock.When(r => r.DeleteById(testId)).Do(x => { /* nothing*/ });

        //act
        var result = controller.DeleteById(testId);
        var resultResult = result as NoContentResult;

        //assert
        Assert.IsType<NoContentResult>(resultResult);
        repositoryMock.Received(1).ReadById(testId);
        repositoryMock.Received(1).DeleteById(testId);
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
        var resultResult = result as NotFoundResult;

        // Assert
        Assert.IsType<NotFoundResult>(resultResult);
        repositoryMock.Received(1).ReadById(testId);
        repositoryMock.DidNotReceive().DeleteById(testId);
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
            IsCompleted = true
        };
        repositoryMock.ReadById(testId).Returns(sampleItem);
        repositoryMock.When(r => r.DeleteById(testId)).Throw(new Exception("An error occurred"));

        // Act
        var result = controller.DeleteById(testId);
        var resultResult = result as ObjectResult;

        // Assert
        Assert.IsType<ObjectResult>(resultResult);
        Assert.Equal(StatusCodes.Status500InternalServerError, resultResult.StatusCode);
        repositoryMock.Received(1).ReadById(testId);
        repositoryMock.Received(1).DeleteById(testId);
    }
}







