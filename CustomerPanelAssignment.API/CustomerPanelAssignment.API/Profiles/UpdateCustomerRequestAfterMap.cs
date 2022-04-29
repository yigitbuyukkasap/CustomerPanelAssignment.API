using AutoMapper;
using CustomerPanelAssignment.API.Models.DomainModels;
using DataModels = CustomerPanelAssignment.API.Models;

namespace CustomerPanelAssignment.API.Profiles
{
    public class UpdateCustomerRequestAfterMap : IMappingAction<UpdateCustomerRequest, DataModels.Customer>
    {
        public void Process(UpdateCustomerRequest source, DataModels.Customer destination, ResolutionContext context)
        {
            destination.Address = new DataModels.Address()
            {
                PhysicalAddress = source.PhysicalAddress,
                PostalAddress = source.PostalAddress
            };
        }
    }
}
