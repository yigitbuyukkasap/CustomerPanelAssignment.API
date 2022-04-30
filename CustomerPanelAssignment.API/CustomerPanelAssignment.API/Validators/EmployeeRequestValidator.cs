using DataAccess.Repositories.IRepository;
using FluentValidation;
using Models.DomainModels;

namespace CustomerPanelAssignment.API.Validators
{
    public class EmployeeRequestValidator : AbstractValidator<EmployeeRequest>
    {
        public EmployeeRequestValidator(ICustomerRepository customerRepository)
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.CustomerId).NotEmpty().Must(id =>
            {
                var customer = customerRepository.FirstOrDefault(x => x.Id == id);
                if (customer != null)
                {
                    return true;
                }
                return false;
            }).WithMessage("Duzgun Musteri Seciniz");

        }
    }
}
