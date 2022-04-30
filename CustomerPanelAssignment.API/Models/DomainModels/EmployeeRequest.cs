using System;

namespace Models.DomainModels
{
    public class EmployeeRequest
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        // Navigation Prop
        public Guid CustomerId { get; set; }
    }
}
