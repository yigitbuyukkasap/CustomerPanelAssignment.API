using AutoMapper;
using CustomerPanelAssignment.API.Models.DomainModels;
using Models.DomainModels;
using DataModel = CustomerPanelAssignment.API.Models;

namespace CustomerPanelAssignment.API.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DataModel.Customer, Customer>().ReverseMap();
            CreateMap<DataModel.Employee, Employee>().ReverseMap();
            CreateMap<DataModel.Address, Address>().ReverseMap();
            CreateMap<UpdateCustomerRequest, DataModel.Customer>().AfterMap<UpdateCustomerRequestAfterMap>();
            CreateMap<AddCustomerRequest, DataModel.Customer>().AfterMap<AddCustomerRequestAfterMap>();
            CreateMap<EmployeeRequest, DataModel.Employee>().AfterMap<AddEmployeeRequestAfterMap>();
        }

    }
}
