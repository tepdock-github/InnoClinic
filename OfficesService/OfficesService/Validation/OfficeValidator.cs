using FluentValidation;
using OfficesService.Domain.DataTransferObjects;

namespace OfficesService.Validation
{
    public class OfficeValidator : AbstractValidator<OfficeForManipulationDto>
    {
        public OfficeValidator()
        {
            RuleFor(o => o.Address)
                .NotEmpty().WithMessage("Address should not be empty");

        }
    }
}
