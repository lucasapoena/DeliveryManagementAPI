using Application.Features.ImportDeliveries.Commands;
using FluentValidation;

namespace Application.Validators.Features.ImportDeliveries.Commands
{
    public class InsertImportDeliveryCommandValidator : AbstractValidator<InsertImportDeliveryCommand>
    {
        public InsertImportDeliveryCommandValidator()
        {
            RuleFor(request => request.UploadRequest).NotNull().WithMessage("UploadRequest is required!");                     
        }
    }
}