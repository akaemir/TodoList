using FluentValidation;
using TodoList.Models.Dtos.ToDos.Requests;

namespace TodoList.Service.Validations;

public class CreateTodoRequestValidator : AbstractValidator<CreateTodoRequest>
{
    public CreateTodoRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Görev başlığı boş olamaz!")
            .Length(2, 50).WithMessage("Görev bağlığı minimum 2 karakter 50 karakter olmalıdır!");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Görev açıklaması boş olamaz!")
            .Length(5,100).WithMessage("Görev açıklaması minimum 5 karakter ve maksimum 100 karakter 100 karakter olmalıdır!");
        RuleFor(x=> x.StartDate).NotEmpty().WithMessage("Başlangıç tarihi boş olamaz!")
            .LessThan(DateTime.Now.AddMinutes(-1)).WithMessage("Geçmiş zaman tarihi olamaz!");
    }
}