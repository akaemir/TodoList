using Core.Exceptions;
using TodoList.DataAccess.Abstracts;
using TodoList.Models.Entities;
using TodoList.Service.Constants;

namespace TodoList.Service.Rules;

public class TodoBusinessRules(IToDoRepository _toDoRepository)
{
    public virtual bool TodoIsPresent(Guid id)
    {
        var todo = _toDoRepository.GetById(id);
        if (todo is null)
        {
            throw new NotFoundException(Messages.TodoIsNotPresentMessage(id));
        }

        return true;
    }
}