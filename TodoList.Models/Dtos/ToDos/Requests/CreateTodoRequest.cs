using TodoList.Models.Entities;

namespace TodoList.Models.Dtos.ToDos.Requests;

public sealed record CreateTodoRequest(
    string Title,
    string Description,
    DateTime StartDate,
    DateTime EndDate,
    Priority Priority,
    int CategoryId
    );