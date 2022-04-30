using CustomerPanelAssignment.API.Models.DomainModels;
using FluentValidation;
using Models.DomainModels;

namespace CustomerPanelAssignment.API.Validators
{
    public class AddCustomerRequestValidator : AbstractValidator<UpdateCustomerRequest>
    {
        public AddCustomerRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.PhoneNumber).NotEmpty();
        }
    }
}
