namespace ToDoList.Test;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.WebApi.Controllers;

public class GetByIdTests
{
    public GetByIdTests()
    {
        ToDoItemsController.items.Clear();
        ToDoItemsController.items.Add(new ToDoItem
        {
            ToDoItemId = 1,
            Name = "test task",
            Description = "test description",
            IsCompleted = false
        });
    }

    [Fact]
    public void GetById_ValidId_ReturnsOkResult()
    {
        //arrange
        var controller = new ToDoItemsController();
        int existingId = 1;

        //act
        var result = controller.ReadById(existingId);
        //assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<ToDoItemGetResponseDto>(okResult.Value);
        Assert.Equal(existingId, returnValue.Id);
        Assert.Equal("test task", returnValue.Name);
        Assert.Equal("test description", returnValue.Description);
        Assert.False(returnValue.IsCompleted);
    }

    [Fact]
    public void GetById_InvalidId_ReturnsNotFound()
    {
        var controller = new ToDoItemsController();
        int nonExistentId = 465;
        var result = controller.ReadById(nonExistentId);
        Assert.IsType<NotFoundResult>(result.Result);
    }
}
