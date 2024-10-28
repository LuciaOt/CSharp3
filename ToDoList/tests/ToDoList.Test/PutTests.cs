namespace ToDoList.Test;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.WebApi.Controllers;

[Collection("Tests")]
public class PutTests
{
    public PutTests()
    {
        ToDoItemsController.items.Clear();

        ToDoItemsController.items.Add(new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Initial Task",
            Description = "Initial Description",
            IsCompleted = false
        });
    }

    [Fact]
    public void Put_ValidId_UpdatesItemOrNoContent()
    {
        //arrange
        var controller = new ToDoItemsController();
        var request = new ToDoItemUpdateRequestDto("Updated Task", "Updated Description", true);
        int IdToUpdate = 1;

        //act
        var result = controller.UpdateById(IdToUpdate, request);

        //assert
        Assert.IsType<NoContentResult>(result);
        var updatedItem = ToDoItemsController.items.FirstOrDefault(i => i.ToDoItemId == IdToUpdate);
        Assert.NotNull(updatedItem);
        Assert.Equal(request.Name, updatedItem.Name);
        Assert.Equal(request.Description, updatedItem.Description);
        Assert.Equal(request.IsCompleted, updatedItem.IsCompleted);
    }

    // Tu ti chybala tretia cast nazvu testu, a to ze co sa ma stat (vrati sa NotFound)
    [Fact]
    public void Put_InvalidId_ReturnsNotFound()
    {
        // arrange
        var controller = new ToDoItemsController();
        var request = new ToDoItemUpdateRequestDto("Non-existent Task", "This task does not exist", false);
        int nonExistentId = 42; // Assuming this ID does not exist

        // act
        var result = controller.UpdateById(nonExistentId, request);

        // assert
        Assert.IsType<NotFoundResult>(result);
    }
}
