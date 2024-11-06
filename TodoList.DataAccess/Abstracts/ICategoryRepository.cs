using Core.Repositories;
using Core.Responses;
using TodoList.Models.Dtos.Categories.Requests;
using TodoList.Models.Dtos.Categories.Responses;
using TodoList.Models.Entities;

namespace TodoList.DataAccess.Abstracts;

public interface ICategoryRepository : IRepository<Category, int>
{
    
}