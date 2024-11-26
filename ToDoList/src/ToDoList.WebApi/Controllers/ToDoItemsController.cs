namespace ToDoList.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repositories;

[ApiController]
[Route("api/[controller]")]
public class ToDoItemsController : ControllerBase
{
    private readonly IRepositoryAsync<ToDoItem> repository;

    public ToDoItemsController(IRepositoryAsync<ToDoItem> repository)
    {
        // this.context = context;
        this.repository = repository;
    }

    [HttpPost]
    public async Task<ActionResult<ToDoItemGetResponseDto>> Create(ToDoItemCreateRequestDto request)
    {
        var item = request.ToDomain();
        try
        {
            await repository.Create(item);
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
    public async Task<ActionResult<IEnumerable<ToDoItemGetResponseDto>>> Read()
    {
        IEnumerable<ToDoItem> itemsToGet;
        try
        {
            itemsToGet = await repository.ReadAll();
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
    public async Task<ActionResult<ToDoItemGetResponseDto>> ReadById(int toDoItemId)
    {
        ToDoItem? itemToGet;
        try
        {
            itemToGet = await repository.ReadById(toDoItemId);
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
    public async Task<IActionResult> UpdateById(int toDoItemId, [FromBody] ToDoItem updatedItem) //ToDoItemUpdateRequestDto request
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

            await repository.Update(updatedItem);
        }
        catch (Exception ex)
        {
            return Problem($"Error accessing the database: {ex.Message}", null, StatusCodes.Status500InternalServerError);
        }
        return NoContent(); //204
    }

    [HttpDelete("{toDoItemId:int}")]
    public async Task<IActionResult> DeleteById(int toDoItemId)
    {
        try
        {
            var itemToDelete = await repository.ReadById(toDoItemId);
            if (itemToDelete is null)
            {
                return NotFound();
            }
            await repository.Delete(itemToDelete);
        }
        catch (Exception ex)
        {
            return Problem($"Error accessing the database: {ex.Message}", null, StatusCodes.Status500InternalServerError);
        }
        return NoContent(); //204
    }

    // public object UpdateById(int invalidId, ToDoItemUpdateRequestDto request) => throw new NotImplementedException();
}
