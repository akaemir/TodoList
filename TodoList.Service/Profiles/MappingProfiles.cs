using AutoMapper;
using TodoList.Models.Dtos.ToDos.Requests;
using TodoList.Models.Dtos.ToDos.Responses;
using TodoList.Models.Entities;

namespace TodoList.Service.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateTodoRequest,Todo>();
        CreateMap<Todo,TodoResponseDto>();
        CreateMap<UpdateTodoRequest,Todo>();
    }
}