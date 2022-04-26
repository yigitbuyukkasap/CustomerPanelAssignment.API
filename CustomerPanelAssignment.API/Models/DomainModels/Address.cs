using System;

namespace CustomerPanelAssignment.API.Models.DomainModels
{
    public class Address
    {
        public Guid Id { get; set; }
        public string PhysicalAddress { get; set; }
        public string PostalAddress { get; set; }

        // Navigation Prop
        public Guid CustomerId { get; set; }
    }
}
