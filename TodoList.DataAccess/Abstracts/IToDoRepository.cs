using Core.Repositories;
using TodoList.Models.Entities;

namespace TodoList.DataAccess.Abstracts;

public interface IToDoRepository : IRepository<Todo,Guid>
{
    
}