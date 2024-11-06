using AutoMapper;
using TodoList.Models.Dtos.Categories.Requests;
using TodoList.Models.Dtos.Categories.Responses;
using TodoList.Models.Dtos.ToDos.Requests;
using TodoList.Models.Dtos.ToDos.Responses;
using TodoList.Models.Entities;

namespace TodoList.Service.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateTodoRequest, Todo>();
        CreateMap<Todo, TodoResponseDto>()
            .ForMember(x => x.CategoryName, cfg => cfg.MapFrom(t => t.Category.Name));
        CreateMap<UpdateTodoRequest, Todo>();

        CreateMap<CreateCategoryRequest, Category>();
        CreateMap<Category, CategoryResponseDto>();
        CreateMap<UpdateCategoryRequest, Category>();
    }
}