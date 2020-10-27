using HelpdeskDAL;
using HelpdeskViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CaseStudyTests
{
    public class EmployeeViewModelTests
    {
        [Fact]
        public void Employee_GetByEmailTest()
        {
            EmployeeViewModel vm = new EmployeeViewModel { Email = "td@abc.com" };
            vm.GetByEmail();
            Assert.NotNull(vm.Firstname);
        }
        [Fact]
        public void Employee_GetByIdTest()
        {
            EmployeeViewModel vm = new EmployeeViewModel { Email = "bs@abc.com" };
            vm.GetByEmail(); // Retrieve Student just added should populate Id
            vm.GetById();
            Assert.NotNull(vm.Firstname);
        }
        [Fact]
        public void Employee_GetAlltest()
        {
            EmployeeViewModel vm = new EmployeeViewModel();
            List<EmployeeViewModel> allStudentVms = vm.GetAll();
            Assert.True(allStudentVms.Count > 0);
        }
        [Fact]
        public void Employee_AddTest()
        {
            EmployeeViewModel vm = new EmployeeViewModel
            {
                Title = "Mrs.",
                Firstname = "Priscilla",
                Lastname = "Peron",
                Phoneno = "(555)555-5551",
                Email = "pp@abc.com",
                IsTech = true,
                DepartmentId = 100
            };
            vm.Add();
            Assert.True(vm.Id > 0);
        }
        [Fact]
        public void Employee_UpdateTest()
        {
            EmployeeViewModel vm = new EmployeeViewModel { Email = "pp@abc.com" };
            vm.GetByEmail();
            vm.Phoneno = vm.Phoneno == "(555)555-5551" ? "(555)555-5552" : "(555)555-5551";
            UpdateStatus studentsUpdated = vm.Update();
            Assert.True(studentsUpdated > 0);
        }
        [Fact]
        public void Employee_DeleteTest()
        {
            EmployeeViewModel vm = new EmployeeViewModel { Email = "pp@abc.com" };
            vm.GetByEmail();
            int employeesDeleted = vm.Delete();
            Assert.True(employeesDeleted == 1);
        }
    }
}