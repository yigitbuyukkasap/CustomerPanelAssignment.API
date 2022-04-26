using System;

namespace CustomerPanelAssignment.API.Models.DomainModels
{
    public class Customer
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public string ImageUrl { get; set; }

        // Navigation Prop
        public Address Address { get; set; }
    }
}
