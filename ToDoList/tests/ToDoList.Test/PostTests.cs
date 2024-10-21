namespace ToDoList.Test;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.WebApi.Controllers;

public class PostTests
{
    [Fact]
    public void Post_One_Item_ReturnsCreated()
    {
        //arrange
        var controller = new ToDoItemsController();
        var request = new ToDoItemCreateRequestDto("New Task", "Test description", false);

        //act
        var result = controller.Create(request);
        var resultResult = result.Result;

        //assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(resultResult);
        Assert.NotNull(createdResult);
        Assert.Equal("ReadById", createdResult.ActionName);
    }
}






