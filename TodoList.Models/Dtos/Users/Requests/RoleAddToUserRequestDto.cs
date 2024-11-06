namespace TodoList.Models.Dtos.Users.Requests;

public sealed record RoleAddToUserRequestDto(string UserId, string RoleName);