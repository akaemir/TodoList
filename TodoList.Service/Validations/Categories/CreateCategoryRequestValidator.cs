using FluentValidation;
using TodoList.Models.Dtos.Categories.Requests;

namespace TodoList.Service.Validations;

public class CreateCategoryRequestValidator : AbstractValidator<CreateCategoryRequest>
{
    public CreateCategoryRequestValidator()
    {
        RuleFor(x=> x.Name).NotEmpty().WithMessage("Kategori ismi boş olamaz!")
            .Length(2,20).WithMessage("Kategori ismi 2 ile 20 karakter arasında olmalıdır!");
    }
}