namespace TodoList.Models.Dtos.ToDos.Requests;

public sealed record CreateTodoRequest(
    string Title,
    string Description,
    DateTime StartDate,
    DateTime EndDate,
    int priorityId,
    int categoryId
    );