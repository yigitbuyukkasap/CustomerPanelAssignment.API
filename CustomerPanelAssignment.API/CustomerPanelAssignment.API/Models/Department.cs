using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerPanelAssignment.API.Models
{
    public class Department
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        //Navigation Prop
        public Customer Customer { get; set; }
    }
}
