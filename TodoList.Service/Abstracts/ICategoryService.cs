using Core.Responses;
using TodoList.Models.Dtos.Categories.Requests;
using TodoList.Models.Dtos.Categories.Responses;

namespace TodoList.Service.Abstracts;

public interface ICategoryService
{
    Task<ReturnModel<CategoryResponseDto>> Add(CreateCategoryRequest create);
    ReturnModel<List<CategoryResponseDto>> GetAll();
    ReturnModel<CategoryResponseDto?> GetById(int id);
    ReturnModel<CategoryResponseDto> Update(UpdateCategoryRequest updateCategory);
    ReturnModel<CategoryResponseDto> Remove(int id);
}