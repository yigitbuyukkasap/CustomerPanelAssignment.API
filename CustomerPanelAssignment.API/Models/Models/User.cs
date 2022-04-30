﻿using CustomerPanelAssignment.API.Models;
using System;

namespace CustomerPanelAssignment.API.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

    }
}
