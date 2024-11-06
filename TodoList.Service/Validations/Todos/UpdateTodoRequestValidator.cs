using FluentValidation;
using TodoList.Models.Dtos.ToDos.Requests;

namespace TodoList.Service.Validations;

public class UpdateTodoRequestValidator : AbstractValidator<UpdateTodoRequest>
{
    public UpdateTodoRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id cannot be empty");
        RuleFor(x=> x.Description).NotEmpty().WithMessage("Description cannot be empty")
            .Length(5,150).WithMessage("Description must be between 2 and 50 characters");
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title cannot be empty")
            .Length(2,50).WithMessage("Title must be between 2 and 50 characters");
        RuleFor(x => x.EndDate).NotEmpty().WithMessage("End date cannot be empty");
    }
}