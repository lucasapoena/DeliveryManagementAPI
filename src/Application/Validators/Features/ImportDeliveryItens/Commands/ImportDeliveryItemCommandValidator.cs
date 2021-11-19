using Application.Features.ImportDeliveryItens.Commands;
using FluentValidation;
using System;

namespace Application.Validators.Features.ImportDeliveryItens.Commands
{
    public class ImportDeliveryItemCommandValidator : AbstractValidator<ImportDeliveryItemCommand>
    {
        public ImportDeliveryItemCommandValidator()
        {
            RuleFor(request => request.DeliveryDate)
                .Must(x => x.Date >= DateTime.Today)
                .WithMessage("Data de entrega é obrigatória!");

            RuleFor(request => request.ProductName)
              .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("Nome do produto é obrigatório!")
              .MaximumLength(50).WithMessage("Nome do produto deve possuir no máximo 50 caracters!");

            RuleFor(request => request.ProductQty)
             .NotEmpty()
             .WithMessage("A quantidade não pode ser igual a 0!");

            RuleFor(request => request.ProductPrice)
             .NotEmpty()
             .WithMessage("O preço não pode ser igual a 0!");
        }

    }    
}