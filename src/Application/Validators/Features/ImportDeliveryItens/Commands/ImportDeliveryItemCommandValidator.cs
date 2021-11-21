using Application.Features.ImportDeliveryItens.Commands;
using FluentValidation;
using System;

namespace Application.Validators.Features.ImportDeliveryItens.Commands
{
    public class ImportDeliveryItemCommandValidator : AbstractValidator<ImportDeliveryItemCommand>
    {
        public ImportDeliveryItemCommandValidator()
        {
            RuleFor(x => x.DeliveryDate).NotNull().WithMessage("Delivery Date is required");
            RuleFor(x => x.DeliveryDate).Must(x => x.Date >= DateTime.Today)
                .WithMessage("Delivery date must be greater than or equal to the current date!");
            
            RuleFor(x => x.ProductName)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("Product name is required!")
                .MaximumLength(50).WithMessage("Product name must have a maximum of 50 characters!");

            RuleFor(x => x.ProductQty)
                .NotNull()
                .NotEmpty()
                .WithMessage("Quantity cannot equal 0!");

            RuleFor(x => x.ProductPrice)
                .NotNull()
                .NotEmpty()
                .WithMessage("Price cannot be equal to 0!");
        }

    }    
}