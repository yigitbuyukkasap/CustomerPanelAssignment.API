using CustomerPanelAssignment.API.Models;
using System;

namespace CustomerPanelAssignment.API.Models.DomainModels
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
