using System;

namespace CustomerPanelAssignment.API.Models.DomainModels
{
    public class UpdateCustomerRequest
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }

        public string PhysicalAddress { get; set; }
        public string PostalAddress { get; set; }
    }
}
