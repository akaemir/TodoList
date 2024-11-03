using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.Models.Dtos.ToDos.Requests;
using TodoList.Service.Abstracts;

namespace TodoList.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoController(ITodoService _todoService) : Controller
{
    [HttpGet("getall")]
    [Authorize(Roles = "Admin")]
    public IActionResult GetAll()
    {
        var result = _todoService.GetAll();
        return Ok(result);
    }

    [HttpPost("add")]
    public IActionResult Add([FromBody]CreateTodoRequest dto)
    {
        string authorId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
        var result = _todoService.Add(dto,authorId);
        return Ok(result);
    }

    [HttpGet("getbyid/{id}")]
    public IActionResult GetById([FromRoute]Guid id)
    {

        var result = _todoService.GetById(id);
        return Ok(result);
    }

    [HttpPut("Update")]
    public IActionResult Update([FromBody] UpdateTodoRequest dto)
    {
        var result = _todoService.Update(dto);
        return Ok(result);
    }

    [HttpGet("getallbycategoryid")]
    public IActionResult GetAllByCategoryId(int id)
    {
        var result = _todoService.GetAllByCategoryId(id);
        return Ok(result);
    }

    [HttpGet("getallbyauthorid")]
    public IActionResult GetAllByAuthorId(string id)
    {
    
        var result = _todoService.GetAllByAuthorId(id);
        return Ok(result);
    }

    [HttpGet("getallbytitlecontains")]
    public IActionResult GetAllByTitleContains(string text)
    {
        var result = _todoService.GetAllByTitleContains(text);
        return Ok(result);
    }
}