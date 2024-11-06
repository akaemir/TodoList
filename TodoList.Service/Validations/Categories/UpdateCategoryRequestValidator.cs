using FluentValidation;
using TodoList.Models.Dtos.Categories.Requests;

namespace TodoList.Service.Validations;

public class UpdateCategoryRequestValidator : AbstractValidator<UpdateCategoryRequest>
{
    public UpdateCategoryRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Kategori ismi boş olamaz!");
        RuleFor(x => x.Id).NotEmpty().WithMessage("Kategori Id'si boş olamaz!")
            .GreaterThan(0).WithMessage("Id 0'dan büyük olmalıdır!");
    }
}