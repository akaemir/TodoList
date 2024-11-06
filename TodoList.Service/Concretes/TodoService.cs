using AutoMapper;
using Core.Responses;
using TodoList.DataAccess.Abstracts;
using TodoList.Models.Dtos.ToDos.Requests;
using TodoList.Models.Dtos.ToDos.Responses;
using TodoList.Models.Entities;
using TodoList.Service.Abstracts;
using TodoList.Service.Constants;
using TodoList.Service.Rules;

namespace TodoList.Service.Concretes;

public sealed class TodoService : ITodoService
{
    private readonly IToDoRepository _toDoRepository;
    private readonly IMapper _mapper;
    private readonly TodoBusinessRules _businessRules;

    public TodoService(IToDoRepository toDoRepository, IMapper mapper, TodoBusinessRules businessRules)
    {
        _toDoRepository = toDoRepository;
        _mapper = mapper;
        _businessRules = businessRules;
    }

    public Task<ReturnModel<TodoResponseDto>> Add(CreateTodoRequest dto, string userId)
    {
        Todo createdTodo = _mapper.Map<Todo>(dto);
        createdTodo.Id = Guid.NewGuid();
        createdTodo.UserId = userId;
        Todo todo = _toDoRepository.Add(createdTodo);
        TodoResponseDto response = _mapper.Map<TodoResponseDto>(todo);

        return Task.FromResult(new ReturnModel<TodoResponseDto>
        {
            Data = response,
            Message = Messages.TodoAddedMessage,
            StatusCode = 201,
            Success = true
        });
    }

    public ReturnModel<List<TodoResponseDto>> GetAll()
    {
        var Todos = _toDoRepository.GetAll();
        List<TodoResponseDto> responses = _mapper.Map<List<TodoResponseDto>>(Todos);
        return new ReturnModel<List<TodoResponseDto>>
        {
            Data = responses,
            Message = string.Empty,
            StatusCode = 200,
            Success = true
        };
    }

    public ReturnModel<TodoResponseDto> GetById(Guid id)
    {
        try
        {
            _businessRules.TodoIsPresent(id);

            var Todo = _toDoRepository.GetById(id);
            var response = _mapper.Map<TodoResponseDto>(Todo);
            return new ReturnModel<TodoResponseDto>
            {
                Data = response,
                Message = "İlgili Todo gösterildi",
                StatusCode = 200,
                Success = true
            };
        }
        catch (Exception ex)
        {
            return ExceptionHandler<TodoResponseDto>.HandleException(ex);
        }
    }

    public ReturnModel<TodoResponseDto> Update(UpdateTodoRequest dto)
    {
        _businessRules.TodoIsPresent(dto.Id);

        Todo todo = _toDoRepository.GetById(dto.Id);

        todo.Title = dto.Title;
        todo.Description = dto.Description;

        _toDoRepository.Update(todo);

        TodoResponseDto response = _mapper.Map<TodoResponseDto>(todo);

        return new ReturnModel<TodoResponseDto>
        {
            Data = response,
            Message = Messages.TodoUpdatedMessage,
            StatusCode = 200,
            Success = true
        };
    }

    public ReturnModel<string> Delete(Guid id)
    {
        _businessRules.TodoIsPresent(id);
        Todo? todo = _toDoRepository.GetById(id);
        Todo deletedTodo = _toDoRepository.Remove(todo);

        return new ReturnModel<string>
        {
            Data = $"Görev Başlığı : {deletedTodo.Title}",
            Message = Messages.TodoDeletedMessage,
            StatusCode = 200,
            Success = true
        };
    }

    public ReturnModel<List<TodoResponseDto>> GetAllByCategoryId(int id)
    {
        List<Todo> todos = _toDoRepository.GetAll(x => x.CategoryId == id);
        List<TodoResponseDto> responses = _mapper.Map<List<TodoResponseDto>>(todos);
        return new ReturnModel<List<TodoResponseDto>>
        {
            Data = responses,
            Message = $"Kategori Id sine göre Görevler listelendi, Kategori Id: {id}",
            StatusCode = 200,
            Success = true
        };
    }

    public ReturnModel<List<TodoResponseDto>> GetAllByAuthorId(string authorId)
    {
        List<Todo> todos = _toDoRepository.GetAll(p => p.UserId == authorId);
        List<TodoResponseDto> responses = _mapper.Map<List<TodoResponseDto>>(todos);

        return new ReturnModel<List<TodoResponseDto>>
        {
            Data = responses,
            Message = $"Kullanıcı Id'sine göre Görevler listelendi! Id: {authorId}",
            StatusCode = 200,
            Success = true
        };
    }

    public ReturnModel<List<TodoResponseDto>> GetAllByTitleContains(string text)
    {
        var posts = _toDoRepository.GetAll(x => x.Title.Contains(text));
        var responses = _mapper.Map<List<TodoResponseDto>>(posts);
        return new ReturnModel<List<TodoResponseDto>>
        {
            Data = responses,
            Message = string.Empty,
            StatusCode = 200,
            Success = true
        };
    }
}