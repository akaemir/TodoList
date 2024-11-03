namespace TodoList.Models.Dtos.ToDos.Requests;

public sealed record UpdateTodoRequest(Guid Id,string Title,string Description,DateTime EndDate);