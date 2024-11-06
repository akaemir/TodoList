using Microsoft.AspNetCore.Mvc;
using TodoList.Models.Dtos.Users.Requests;
using TodoList.Service.Abstracts;

namespace TodoList.API.Controllers;


[Route("api/[controller]")]
[ApiController]
public class AuthenticationController(IAuthenticationService _authenticationService) : Controller
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
    {
        var result = await _authenticationService.LoginByTokenAsync(dto);
        return Ok(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> CreateUser([FromBody] RegisterRequestDto dto)
    {
        var result = await _authenticationService.RegisterByTokenAsync(dto);

        return Ok(result);
    }
    
}