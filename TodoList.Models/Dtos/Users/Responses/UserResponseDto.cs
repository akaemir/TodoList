namespace TodoList.Models.Dtos.Users.Responses;

public sealed record UserResponseDto
{
    public string UserId { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    
};