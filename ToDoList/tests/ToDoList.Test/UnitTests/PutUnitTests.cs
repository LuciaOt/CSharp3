namespace ToDoList.Test.UnitTests;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi.Controllers;
public class PutUnitTests
{
    [Fact]
    public void Put_UpdateByIdWhenIdNotFound_ReturnsNotFound()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        var testId = 1;
        var updatedItem = new ToDoItem
        {
            Name = "Updated Name",
            Description = "Updated Description",
            IsCompleted = true
        };
        repositoryMock.ReadById(testId).Returns((ToDoItem)null);

        // Act
        var result = controller.UpdateById(testId, updatedItem);


        // Assert
        result.Should().BeOfType<NotFoundResult>("the item does not exist in the repository");
        repositoryMock.Received(1).ReadById(testId);
        repositoryMock.DidNotReceive().Update(Arg.Any<ToDoItem>());
    }

    [Fact]
    public void Put_UpdateByIdWhenItemUpdated_ReturnsNoContent()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        var testId = 1;
        var existingItem = new ToDoItem
        {
            ToDoItemId = testId,
            Name = "Existing Item",
            Description = "This item exists.",
            IsCompleted = false
        };
        var updatedItem = new ToDoItem
        {
            ToDoItemId = testId,
            Name = "Updated Name",
            Description = "Updated Description",
            IsCompleted = true
        };

        repositoryMock.ReadById(testId).Returns(existingItem);


        // Act
        var result = controller.UpdateById(testId, updatedItem);

        // Assert
        result.Should().BeOfType<NoContentResult>();
        repositoryMock.Received(1).ReadById(testId);
        repositoryMock.Received(1).Update(Arg.Is<ToDoItem>(item =>
            item.ToDoItemId == updatedItem.ToDoItemId &&
            item.Name == updatedItem.Name &&
            item.Description == updatedItem.Description &&
            item.IsCompleted == updatedItem.IsCompleted));
    }

    [Fact]
    public void Put_UpdateByIdUnhandledException_ReturnsInternalServerError()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        var testId = 1;
        var updatedItem = new ToDoItem
        {
            ToDoItemId = testId,
            Name = "Updated Name",
            Description = "Updated Description",
            IsCompleted = true
        };
        repositoryMock.ReadById(testId).Returns(updatedItem);
        repositoryMock
                .When(repo => repo.Update(Arg.Any<ToDoItem>()))
                .Do(_ => throw new Exception("Database error"));
        // Act
        var result = controller.UpdateById(testId, updatedItem);

        // Assert
        var objectResult = result as ObjectResult;
        objectResult.Should().NotBeNull();
        objectResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
    }
}






