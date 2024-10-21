namespace ToDoList.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;

[ApiController]
[Route("api/[controller]")]
public class ToDoItemsController : ControllerBase
{
    public static readonly List<ToDoItem> items = [];

    //    public static readonly List<ToDoItem> items = new List<ToDoItem> // []; testovaci data do itemov
    //     {
    //         new ToDoItem
    //             {
    //                 ToDoItemId = 1,
    //                 Name = "Opravit pracku",
    //                 Description = "Skontrolovat elektriku, objednat novu",
    //                 IsCompleted = false
    //             },
    //             new ToDoItem
    //             {
    //                 ToDoItemId = 2,
    //                 Name = "Vymalovat stenu",
    //                 Description = "Kupit tmel a bielu farbu",
    //                 IsCompleted = false
    //             }
    //     };

    [HttpPost]
    public ActionResult<ToDoItemGetResponseDto> Create(ToDoItemCreateRequestDto request)
    {
        var item = request.ToDomain();

        try
        {
            item.ToDoItemId = items.Count == 0 ? 1 : items.Max(o => o.ToDoItemId) + 1;
            items.Add(item);
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
        List<ToDoItem> itemsToGet;
        try
        {
            itemsToGet = items;
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }

        if (itemsToGet == null || itemsToGet.Count == 0)
        {
            return NotFound(); // 404
        }
        return Ok(itemsToGet.Select(ToDoItemGetResponseDto.FromDomain)); // 200

        //respond to client - OLD
        // return (itemsToGet is null)
        //     ? NotFound() //404
        //     : Ok(itemsToGet.Select(ToDoItemGetResponseDto.FromDomain)); //200
    }

    [HttpGet("{toDoItemId:int}")]
    public ActionResult<ToDoItemGetResponseDto> ReadById(int toDoItemId)
    {
        ToDoItem? itemToGet;

        try
        {
            itemToGet = items.Find(i => i.ToDoItemId == toDoItemId);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
        return (itemToGet is null)
            ? NotFound() //404
            : Ok(ToDoItemGetResponseDto.FromDomain(itemToGet));
    }

    [HttpPut("{toDoItemId:int}")]
    public IActionResult UpdateById(int toDoItemId, [FromBody] ToDoItemUpdateRequestDto request)
    {
        var updatedItem = request.ToDomain();
        try
        {
            var itemIndexToUpdate = items.FindIndex(i => i.ToDoItemId == toDoItemId);

            if (itemIndexToUpdate == -1)
            {
                return NotFound();
            }

            updatedItem.ToDoItemId = toDoItemId;
            items[itemIndexToUpdate] = updatedItem;
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
        return NoContent(); //204
    }

    [HttpDelete("{toDoItemId:int}")]
    public IActionResult DeleteById(int toDoItemId)
    {
        try
        {
            var itemToDelete = items.Find(i => i.ToDoItemId == toDoItemId);
            if (itemToDelete is null)
            {
                return NotFound();
            }
            items.Remove(itemToDelete);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
        return NoContent(); //204
    }
}
