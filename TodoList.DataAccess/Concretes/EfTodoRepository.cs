using Core.Repositories;
using TodoList.DataAccess.Abstracts;
using TodoList.DataAccess.Contexts;
using TodoList.Models.Entities;

namespace TodoList.DataAccess.Concretes;

public class EfTodoRepository : EfRepositoryBase<BaseDbContext, Todo, Guid>, IToDoRepository
{
    public EfTodoRepository(BaseDbContext context) : base(context)
    {
    }
}