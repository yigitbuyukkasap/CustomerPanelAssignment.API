using CustomerPanelAssignment.API.Models.DomainModels;
using DataAccess.Repositories.IRepository;
using FluentValidation;
using Models.DomainModels;

namespace CustomerPanelAssignment.API.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator(ICustomerRepository customerRepository)
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
            

        }
    }
}
