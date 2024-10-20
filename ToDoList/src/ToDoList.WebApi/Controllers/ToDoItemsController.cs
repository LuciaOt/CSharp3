namespace ToDoList.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;

[ApiController]
[Route("api/[controller]")]
public class ToDoItemsController : ControllerBase
{
    private static readonly List<ToDoItem> items = [];

    [HttpPost]
    public IActionResult Create(ToDoItemCreateRequestDto request)
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
        //return Ok();
        //return Ok(ToDoItemGetResponseDto.FromDomain(item));
        /*
        Nazov parametru musi byt toDoItemId, ako je nazov parametru v ReadById. Inak ti request pada s hlaskou:
        System.InvalidOperationException: No route matches the supplied values.
        Item vlozi, ale nevie vytvorit cestu pre ReadById, lebo nepozna taku, kde by bol parameter /{id:int}, ale /{toDoItemId:int}
        */
        return CreatedAtAction(nameof(ReadById), new { toDoItemId = item.ToDoItemId }, ToDoItemGetResponseDto.FromDomain(item));
    }

    [HttpGet]
    public IActionResult Read()
    {
        try
        {
            if (items == null || items.Count == 0)
            {
                return NotFound();
            }

            var response = items.Select(ToDoItemGetResponseDto.FromDomain).ToList();
            return Ok(response);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{toDoItemId:int}")]
    public IActionResult ReadById(int toDoItemId)
    {
        try
        {
            var item = items.Find(o => o.ToDoItemId == toDoItemId);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(ToDoItemGetResponseDto.FromDomain(item));
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut("{toDoItemId:int}")]
    public IActionResult UpdateById(int toDoItemId, [FromBody] ToDoItemUpdateRequestDto request)
    {
        if (request == null)
        {
            return BadRequest("Invalid request body.");
        }

        try
        {
            var indexOfOldInstance = items.FindIndex(item => item.ToDoItemId == toDoItemId);

            if (indexOfOldInstance == -1)
            {
                return NotFound();
            }

            /*
            Tu chces updatovat cely item tym, co ti prislo v request, nielen IsCompleted
            Malo by to byt teda nejak podobne:
            */
            var item = request.ToDomain();
            item.ToDoItemId = toDoItemId; // ak by request neobsahoval to ID ale bolo by iba v ceste
            items[indexOfOldInstance] = item;
            // Na konci pri uspesnej aktualizacii vratit NoContent() podla zadania
            return NoContent();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete("{toDoItemId:int}")]
    public IActionResult DeleteById(int toDoItemId)
    {
        try
        {
            var indexOfItem = items.FindIndex(item => item.ToDoItemId == toDoItemId);
            if (indexOfItem == -1)
            {
                return NotFound();
            }
            items.RemoveAt(indexOfItem);
            return NoContent();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }
}
