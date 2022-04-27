﻿using CustomerPanelAssignment.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Customer> Category { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<User> User{ get; set; }
        public DbSet<Role> Role { get; set; }
    }
}
