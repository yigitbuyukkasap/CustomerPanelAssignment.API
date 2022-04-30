using AutoMapper;
using Models.DomainModels;
using System;
using DataModels = CustomerPanelAssignment.API.Models;

namespace CustomerPanelAssignment.API.Profiles
{
    public class AddEmployeeRequestAfterMap : IMappingAction<EmployeeRequest, DataModels.Employee>
    {
        public void Process(EmployeeRequest source, DataModels.Employee destination, ResolutionContext context)
        {
            destination.Id = Guid.NewGuid();
        }
    }
}
