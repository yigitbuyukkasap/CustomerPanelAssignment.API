using AutoMapper;
using Models.DomainModels;
using System;
using DataModels = CustomerPanelAssignment.API.Models;

namespace CustomerPanelAssignment.API.Profiles
{
    public class AddCustomerRequestAfterMap : IMappingAction<AddCustomerRequest, DataModels.Customer>
    {
        public void Process(AddCustomerRequest source, DataModels.Customer destination, ResolutionContext context)
        {
            destination.Id = Guid.NewGuid();
            destination.Address = new DataModels.Address()
            {
                Id = Guid.NewGuid(),
                PhysicalAddress = source.PhysicalAddress,
                PostalAddress = source.PostalAddress
            };
        }
    }
}
