using TodoList.Models.Dtos.Users.Requests;

namespace TodoList.Service.Abstracts;

public interface IRoleService
{
    Task<string> AddRoleToUser(RoleAddToUserRequestDto dto);

    Task<List<string>> GetAllRolesByUserId(string userId);

    Task<string> AddRoleAsync(string name);
}