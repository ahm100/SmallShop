using FluentValidation;
using Orders.Dtos;

namespace Orders.Validators
{
    public class CreateOrderDtoValidator : AbstractValidator<CreateOrderDto>
    {
        public CreateOrderDtoValidator()
        {
            RuleFor(dto => dto.CustomerName).NotEmpty().WithMessage("Customer name is required");
            RuleFor(dto => dto.CreateTime).NotEmpty().WithMessage("Order date is required");
            RuleFor(dto => dto.TotalAmount).GreaterThan(0).WithMessage("Total amount must be greater than 0");
        }
    }
}
