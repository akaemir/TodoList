using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.Models.Dtos.Categories.Requests;
using TodoList.Models.Dtos.ToDos.Requests;
using TodoList.Service.Abstracts;

namespace TodoList.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController(ICategoryService _categoryService) : Controller
{
    [HttpGet("getall")]
    [Authorize(Roles = "User,Admin")]
    public IActionResult GetAll()
    {
        var result = _categoryService.GetAll();
        return Ok(result);
    }
    [Authorize(Roles = "Admin")]
    [HttpPost("add")]
    public IActionResult Add([FromBody] CreateCategoryRequest dto)
    {
        var result = _categoryService.Add(dto);
        return Ok(result);
    }
    [Authorize(Roles = "User,Admin")]
    [HttpGet("getbyid/{id}")]
    public IActionResult GetById([FromRoute] int id)
    {
        var result = _categoryService.GetById(id);
        return Ok(result);
    }
    [Authorize(Roles = "Admin")]
    [HttpPut("Update")]
    public IActionResult Update([FromBody] UpdateCategoryRequest dto)
    {
        var result = _categoryService.Update(dto);
        return Ok(result);
    }
}