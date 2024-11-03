using TodoList.Models.Dtos.Tokens.Responses;
using TodoList.Models.Entities;

namespace TodoList.Service.Abstracts;

public interface IJwtService
{
    Task<TokenResponseDto> CreateToken(User user);
}