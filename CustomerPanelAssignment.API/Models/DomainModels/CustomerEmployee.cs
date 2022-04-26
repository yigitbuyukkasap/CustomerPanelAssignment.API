using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerPanelAssignment.API.Models.DomainModels
{
    public class CustomerEmployee
    {
        public Guid Id { get; set; }

        //Navigation Prop
        public Customer Customer { get; set; }
        public Employee Employee { get; set; }
    }
}
