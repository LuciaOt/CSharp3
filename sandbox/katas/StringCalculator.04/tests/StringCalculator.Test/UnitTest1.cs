namespace StringCalculator.Test;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {

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
}



