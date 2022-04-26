using System;

namespace CustomerPanelAssignment.API.Models.DataModels
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
