using Core.Exceptions;
using TodoList.DataAccess.Abstracts;
using TodoList.Service.Abstracts;
using TodoList.Service.Constants;

namespace TodoList.Service.Rules;

public class CategoryBusinessRules(ICategoryRepository _repository)
{
    public virtual bool CategoryIsPresent(int id)
    {
        var categories = _repository.GetById(id);
        if (categories is null)
        {
            throw new NotFoundException(Messages.CategoryIsNotPresentMessage(id));
        }
        return true;
    }
}