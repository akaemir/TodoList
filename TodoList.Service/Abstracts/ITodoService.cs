using Core.Repositories;
using Core.Responses;
using TodoList.Models.Dtos.ToDos.Requests;
using TodoList.Models.Dtos.ToDos.Responses;
using TodoList.Models.Entities;

namespace TodoList.Service.Abstracts;

public interface ITodoService
{
    Task<ReturnModel<TodoResponseDto>> Add(CreateTodoRequest dto, string userId);
    ReturnModel<List<TodoResponseDto>> GetAll();
    ReturnModel<TodoResponseDto> GetById(Guid id);
    ReturnModel<TodoResponseDto> Update(UpdateTodoRequest dto);
    ReturnModel<string> Delete(Guid id);
    ReturnModel<List<TodoResponseDto>> GetAllByCategoryId(int id);
    ReturnModel<List<TodoResponseDto>> GetAllByAuthorId(string authorId);
    ReturnModel<List<TodoResponseDto>> GetAllByTitleContains(string text);
}