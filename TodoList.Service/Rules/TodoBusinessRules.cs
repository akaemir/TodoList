
using Core.Exceptions;
using TodoList.Models.Entities;

namespace TodoList.Service.Rules;

public class TodoBusinessRules
{
    public virtual void PostIsNullCheck(Todo todo)
    {
        if(todo is null)
        {
            throw new NotFoundException("İlgili görev bulunamadı.");
        }
    }
}
