using Core.Tokens.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.Models.Dtos.Users.Requests;
using TodoList.Service.Abstracts;

namespace TodoList.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")] // User ile ilgili işlemleri sadece admin gerçekleştirebilir.
public class UserController(IUserService _userService, IAuthenticationService _authenticationService) : Controller
{
    [HttpGet("getbyemail")]
    public async Task<IActionResult> GetByEmail([FromQuery] string email)
    {
        var result = await _userService.GetByEmailAsync(email);
        return Ok(result);
    }
    
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromQuery] string id)
    {
        var result = await _userService.DeleteAsync(id);
        return Ok(result);
    }


    [HttpPut("update")]
    public async Task<IActionResult> Update([FromQuery] string id, [FromBody] UserUpdateRequestDto dto)
    {
        var result = await _userService.UpdateAsync(id, dto);
        return Ok(result);
    }

    [HttpPut("changepassword")]
    public async Task<IActionResult> ChangePassword(string id, ChangePasswordRequestDto dto)
    {
        var result = await _userService.ChangePasswordAsync(id, dto);
        return Ok(result);
    }
}