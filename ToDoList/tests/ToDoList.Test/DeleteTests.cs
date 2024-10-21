namespace ToDoList.Test;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.Models;
using ToDoList.WebApi.Controllers;
public class DeleteTests
{
    public DeleteTests()
    {
        ToDoItemsController.items.Clear();
        ToDoItemsController.items.Add(new ToDoItem
        {
            ToDoItemId = 1,
            Name = "Test task",
            Description = "Test description",
            IsCompleted = false

        }
        );
    }

    [Fact]
    public void Delete_ValidID_Reuturns_No_Content()
    {
        //arrange
        var controller = new ToDoItemsController();
        int itemIdToDelete = 1;

        //act
        var result = controller.DeleteById(itemIdToDelete);

        //assert
        Assert.IsType<NoContentResult>(result);
        var deletedItem = ToDoItemsController.items.FirstOrDefault(i => i.ToDoItemId == itemIdToDelete);
        Assert.Null(deletedItem);
    }

    [Fact]

    public void Delete_InvalidID_ReturnsNotFound()
    {
        //arrange
        var controller = new ToDoItemsController();
        int nonExistentId = 2;

        //act
        var result = controller.DeleteById(nonExistentId);

        //assert
        Assert.IsType<NotFoundResult>(result);
    }
}
