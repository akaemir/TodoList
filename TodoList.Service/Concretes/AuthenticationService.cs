using TodoList.Models.Dtos.Tokens.Responses;
using TodoList.Models.Dtos.Users.Requests;
using TodoList.Service.Abstracts;

namespace TodoList.Service.Concretes;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserService _userService;
    private readonly IJwtService _jwtService;

    public AuthenticationService(IUserService userService, IJwtService jwtService)
    {
        _userService = userService;
        _jwtService = jwtService;
    }

    public async Task<TokenResponseDto> LoginByTokenAsync(LoginRequestDto dto)
    {
        var loginResponse = await _userService.LoginAsync(dto);
        var tokenResponse = await _jwtService.CreateToken(loginResponse);
        return tokenResponse;
    }

    public async Task<TokenResponseDto> RegisterByTokenAsync(RegisterRequestDto dto)
    {
        var registerResponse = await _userService.RegisterAsync(dto);
        var tokenResponse = await _jwtService.CreateToken(registerResponse);
        return tokenResponse;
    }
}