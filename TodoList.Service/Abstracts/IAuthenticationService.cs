using TodoList.Models.Dtos.Tokens.Responses;
using TodoList.Models.Dtos.Users.Requests;

namespace TodoList.Service.Abstracts;

public interface IAuthenticationService
{
    Task<TokenResponseDto> RegisterByTokenAsync(RegisterRequestDto dto);
    Task<TokenResponseDto> LoginByTokenAsync(LoginRequestDto dto);
}