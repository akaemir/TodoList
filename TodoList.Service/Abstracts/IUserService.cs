using TodoList.Models.Dtos.Users.Requests;
using TodoList.Models.Entities;

namespace TodoList.Service.Abstracts;

public interface IUserService
{
    Task<User> RegisterAsync(RegisterRequestDto dto);
    Task<User> GetByEmailAsync(string email);
    Task<User> LoginAsync(LoginRequestDto dto);
    Task<User> UpdateAsync(string id,UserUpdateRequestDto dto);
    Task<User> ChangePasswordAsync(string id,ChangePasswordRequestDto requestDto);
    Task<string> DeleteAsync(string id);
}