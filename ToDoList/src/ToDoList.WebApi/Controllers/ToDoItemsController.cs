namespace ToDoList.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repositories;

[ApiController]
[Route("api/[controller]")]
public class ToDoItemsController : ControllerBase
{
    private readonly IRepository<ToDoItem> repository;

    public ToDoItemsController(IRepository<ToDoItem> repository)
    {
        // this.context = context;
        this.repository = repository;
    }

    [HttpPost]
    public ActionResult<ToDoItemGetResponseDto> Create(ToDoItemCreateRequestDto request)
    {
        var item = request.ToDomain();
        try
        {
            repository.Create(item);
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
            itemToGet = repository.ReadById(toDoItemId);
        }
        catch (Exception ex)
        {
            return Problem($"Error accessing the database: {ex.Message}", null, StatusCodes.Status500InternalServerError);
        }
        return (itemToGet is null)
            ? NotFound() //404
            : Ok(ToDoItemGetResponseDto.FromDomain(itemToGet));
    }

    [HttpPut("{toDoItemId:int}")]
    public IActionResult UpdateById(int toDoItemId, [FromBody] ToDoItem updatedItem) //ToDoItemUpdateRequestDto request
    {
        // var updatedItem = request.ToDomain();
        updatedItem.ToDoItemId = toDoItemId;
        try
        {
            var existingItem = repository.ReadById(toDoItemId);
            if (existingItem is null)
            {
                return NotFound();
            }

            repository.Update(updatedItem);
        }
        catch (Exception ex)
        {
            return Problem($"Error accessing the database: {ex.Message}", null, StatusCodes.Status500InternalServerError);
        }
        return NoContent(); //204
    }

    [HttpDelete("{toDoItemId:int}")]
    public IActionResult DeleteById(int toDoItemId)
    {
        try
        {
            var itemToDelete = repository.ReadById(toDoItemId);
            if (itemToDelete is null)
            {
                return NotFound();
            }
            repository.Delete(itemToDelete);
        }
        catch (Exception ex)
        {
            return Problem($"Error accessing the database: {ex.Message}", null, StatusCodes.Status500InternalServerError);
        }
        return NoContent(); //204
    }

    // public object UpdateById(int invalidId, ToDoItemUpdateRequestDto request) => throw new NotImplementedException();
}
