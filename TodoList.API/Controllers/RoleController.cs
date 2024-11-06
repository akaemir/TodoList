using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.Models.Dtos.Users.Requests;
using TodoList.Service.Abstracts;

namespace TodoList.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize (Roles = "Admin")]
public class RoleController(IRoleService roleService) : Controller
{
    [HttpPost("addroletouser")]
    public async Task<IActionResult> AddRoleToUser([FromBody]RoleAddToUserRequestDto dto)
    {
        var result = await roleService.AddRoleToUser(dto);
        return Ok(result);
    }

    [HttpGet("getallrolesbyid")]
    public async Task<IActionResult> GetAllRolesByUserId([FromQuery]string userId)
    {
        var result = await roleService.GetAllRolesByUserId(userId);
        return Ok(result);
    }

    [HttpPost("addrole")]
    public async Task<IActionResult> AddRoleAsync([FromQuery]string Name)
    {
        var result = await roleService.AddRoleAsync(Name);
        return Ok(result);
    }
}