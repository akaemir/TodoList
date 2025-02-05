using System.Linq.Expressions;
using AutoMapper;
using Core.Responses;
using TodoList.DataAccess.Abstracts;
using TodoList.Models.Dtos.Categories.Requests;
using TodoList.Models.Dtos.Categories.Responses;
using TodoList.Models.Entities;
using TodoList.Service.Abstracts;
using TodoList.Service.Constants;
using TodoList.Service.Rules;

namespace TodoList.Service.Concretes;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly CategoryBusinessRules _categoryBusinessRules;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper, CategoryBusinessRules categoryBusinessRules)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _categoryBusinessRules = categoryBusinessRules;
    }

    public Task<ReturnModel<CategoryResponseDto>> Add(CreateCategoryRequest create)
    {
        Category created = _mapper.Map<Category>(create);
        _categoryRepository.Add(created);
        CategoryResponseDto response = _mapper.Map<CategoryResponseDto>(created);
        return Task.FromResult(new ReturnModel<CategoryResponseDto>
        {
            Data = response,
            Message = Messages.CategoryAddedMessage,
            StatusCode = 200,
            Success = true
        });
    }

    public ReturnModel<List<CategoryResponseDto>> GetAll()
    {
        List<Category> categories = _categoryRepository.GetAll();
        List<CategoryResponseDto> responses = _mapper.Map<List<CategoryResponseDto>>(categories);

        return new ReturnModel<List<CategoryResponseDto>>
        {
            Data = responses,
            Message = "Listelendi",
            Success = true,
            StatusCode = 200
        };
    }

    public ReturnModel<CategoryResponseDto?> GetById(int id)
    {
        _categoryBusinessRules.CategoryIsPresent(id);
        Category Category = _categoryRepository.GetById(id);
        CategoryResponseDto response = _mapper.Map<CategoryResponseDto>(Category);
        return new ReturnModel<CategoryResponseDto?>
        {
            Data = response,
            StatusCode = 200,
            Message = "Id'ye gore getirildi.",
            Success = true,
        };
    }

    public ReturnModel<CategoryResponseDto> Update(UpdateCategoryRequest updateCategory)
    {
        _categoryBusinessRules.CategoryIsPresent(updateCategory.Id);
        Category category = _categoryRepository.GetById(updateCategory.Id);
        Category update = new Category
        {
            Id = updateCategory.Id,
            Name = updateCategory.Name,
        };
        Category updatedCategory = _categoryRepository.Update(update);
        CategoryResponseDto dto = _mapper.Map<CategoryResponseDto>(updatedCategory);
        return new ReturnModel<CategoryResponseDto>
        {
            Data = dto,
            Message = Messages.CategoryUpdatedMessage,
            StatusCode = 200,
            Success = true,
        };
    }

    public ReturnModel<CategoryResponseDto> Remove(int id)
    {
        _categoryBusinessRules.CategoryIsPresent(id);
        Category category = _categoryRepository.GetById(id);
        Category deletedCategory = _categoryRepository.Remove(category);
        CategoryResponseDto response = _mapper.Map<CategoryResponseDto>(deletedCategory);
        return new ReturnModel<CategoryResponseDto>
        {
            Data = response,
            Message = Messages.CategoryDeletedMessage,
            StatusCode = 200,
            Success = true,
        };
    }
}