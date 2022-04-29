using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DomainModels
{
    public class AddCustomerRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public string PhysicalAddress { get; set; }
        public string PostalAddress { get; set; }
    }
}
