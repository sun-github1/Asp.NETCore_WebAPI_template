using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebAPIDemo.Model.Enums;
using WebAPIDemo.Model.Models;

namespace WebAPIDemo.DAL.DBContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>().HasData(new Employee()
            {
                EmployeeId=1,
                EmployeeName="Employee1",
                Age=45,
                DepartmentId=1,
                Email="asd.@xyz.com",
                Gender = Gender.Male,
            });
            modelBuilder.Entity<Employee>().HasData(new Employee()
            {
                EmployeeId = 2,
                EmployeeName = "Employee2",
                Age = 35,
                DepartmentId = 1,
                Email = "qwe.@rty.com",
                Gender = Gender.Female,
            });


            modelBuilder.Entity<Department>().HasData(new Department()
            {
                DepartmentId=1,
                DepartmentName="IT"
            });
            modelBuilder.Entity<Department>().HasData(new Department()
            {
                DepartmentId = 2,
                DepartmentName = "Admin"
            });
        }
    }
}
