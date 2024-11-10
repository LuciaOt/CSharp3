namespace ToDoList.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.Persistence;
using ToDoList.Persistence.Repositories;

[ApiController]
[Route("api/[controller]")]
public class ToDoItemsController : ControllerBase
{
    //nahradit zavislot na db contextu repozitarom
    //public readonly List<ToDoItem> items = []; //static
    // private readonly ToDoItemsContext context;
    private readonly IRepository<ToDoItem> repository;
    // private IRepository<ToDoItem> repositoryMock;

    public ToDoItemsController(IRepository<ToDoItem> repository)//po migracii tu bude len repository ToDoItemsContext context,
    {
        // this.context = context;
        this.repository = repository;
    }

    // public ToDoItemsController(IRepository<ToDoItem> repositoryMock) => this.repositoryMock = repositoryMock;

    // public ToDoItemsController(IRepository<ToDoItem> repository)
    // {
    //     this.repository = repository;
    // }

    [HttpPost]
    public ActionResult<ToDoItemGetResponseDto> Create(ToDoItemCreateRequestDto request)
    {
        var item = request.ToDomain();
        try
        {
            repository.Create(item);
            // context.ToDoItems.Add(item); //will be removed after migration
            // context.SaveChanges(); //will be removed after migration
            // item.ToDoItemId = items.Count == 0 ? 1 : items.Max(o => o.ToDoItemId) + 1;
            // items.Add(item);
        }

        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
        return CreatedAtAction(nameof(ReadById),
                                 new { toDoItemId = item.ToDoItemId },
                                 ToDoItemGetResponseDto.FromDomain(item));
    }

    [HttpGet]
    public ActionResult<IEnumerable<ToDoItemGetResponseDto>> Read()
    {
        IEnumerable<ToDoItem> itemsToGet;
        try
        {
            itemsToGet = repository.ReadAll();
            // var items = repository.ReadAll();
            // var itemsToGet = items.ToList();
            // var itemsToGet = context.ToDoItems.ToList();
            // if (itemsToGet.Count == 0) //will be removed
            // {
            //     itemsToGet = context.ToDoItems.ToList();
            // }
        }
        catch (Exception ex)
        {
            return Problem($"Error accessing the database: {ex.Message}", null, StatusCodes.Status500InternalServerError);
        }

        return (itemsToGet == null || !itemsToGet.Any())
        ? NotFound("No items found in the database.") //400
        : Ok(itemsToGet.Select(ToDoItemGetResponseDto.FromDomain)); //200
    }

    [HttpGet("{toDoItemId:int}")]
    public ActionResult<ToDoItemGetResponseDto> ReadById(int toDoItemId)
    {
        ToDoItem? itemToGet;
        try
        {
            itemToGet = repository.Read(toDoItemId);
            // if (itemToGet == null) // will be removed
            // {
            //     itemToGet = context.ToDoItems.Find(toDoItemId);
            // }
            // itemToGet = context.ToDoItems.Find(toDoItemId);
        }
        catch (Exception ex)
        {
            return Problem($"Error accessing the database: {ex.Message}", null, StatusCodes.Status500InternalServerError);
            // return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
        return (itemToGet is null)
            ? NotFound() //404
            : Ok(ToDoItemGetResponseDto.FromDomain(itemToGet));
    }

    [HttpPut("{toDoItemId:int}")]
    public IActionResult UpdateById(int toDoItemId, [FromBody] ToDoItemUpdateRequestDto request)
    {
        var updatedItem = request.ToDomain();
        updatedItem.ToDoItemId = toDoItemId;
        try
        {
            var existingItem = repository.Read(toDoItemId);
            // var existingItem = context.ToDoItems.Find(toDoItemId);
            if (existingItem == null) // will be removed
            {
                return NotFound();
            }

            repository.Update(updatedItem);
            // context.Entry(existingItem).CurrentValues.SetValues(updatedItem); //will be removed
            // context.SaveChanges(); //will be removed
        }
        catch (Exception ex)
        {
            return Problem($"Error accessing the database: {ex.Message}", null, StatusCodes.Status500InternalServerError);
            // return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
        return NoContent(); //204
    }

    [HttpDelete("{toDoItemId:int}")]
    public IActionResult DeleteById(int toDoItemId)
    {
        try
        {
            var itemToDelete = repository.Read(toDoItemId);
            // if (itemToDelete == null)//will be removed
            // {
            //     itemToDelete = context.ToDoItems.FirstOrDefault(i => i.ToDoItemId == toDoItemId);
            // }

            // var itemToDelete = context.ToDoItems.FirstOrDefault(i => i.ToDoItemId == toDoItemId);
            if (itemToDelete is null)
            {
                return NotFound();
            }
            repository.Delete(toDoItemId);
            // context.ToDoItems.Remove(itemToDelete); //will be removed
            // context.SaveChanges();
        }
        catch (Exception ex)
        {
            return Problem($"Error accessing the database: {ex.Message}", null, StatusCodes.Status500InternalServerError);
            // return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
        return NoContent(); //204
    }
}
