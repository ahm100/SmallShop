using FluentValidation;
using Orders.Dtos;

namespace Orders.Validators
{
    public class UpdateOrderDtoValidator : AbstractValidator<UpdateOrderDto>
    {
        public UpdateOrderDtoValidator()
        {
            // Define validation rules for UpdateOrderDto
            RuleFor(dto => dto.Id).NotEmpty().WithMessage("Order ID is required");
            RuleFor(dto => dto.TotalAmount).GreaterThan(0).WithMessage("Total amount must be greater than 0");
        }
    }
}
