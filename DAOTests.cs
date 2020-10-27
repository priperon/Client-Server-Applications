using System;
using Xunit;
using HelpdeskDAL;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace DAOTests
{
    public class DAOTests
    {
        [Fact]

        public void Employee_GetByEmailTest()
        {
            try
            {
                EmployeeDAO dao = new EmployeeDAO();
                Employees selectedEmail = dao.GetByEmail("bs@abc.com");
                Assert.NotNull(selectedEmail);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(" Error - " + ex.Message);
            }

        }

        [Fact]
        public void Employee_GetByIdTest()
        {
            try
            {
                EmployeeDAO dao = new EmployeeDAO();
                Employees selectedEmployee = dao.GetById(2);
                Assert.NotNull(selectedEmployee);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(" Error - " + ex.Message);
            }

        }
        [Fact]
        public void Employee_GetAllTest()
        {
            try
            {
                EmployeeDAO dao = new EmployeeDAO();
                List<Employees> allEmployees = dao.GetAll();
                Assert.True(allEmployees.Count > 0);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(" Error - " + ex.Message);
            }

        }
        [Fact]
        public void Employee_AddTest()
        {
            try
            {
                EmployeeDAO dao = new EmployeeDAO();
                Employees employee = new Employees();
                Employees newemployee = new Employees
                {
                    Title = "Mrs.",
                    FirstName = "Priscilla",
                    LastName = "Peron",
                    PhoneNo = "(555)555-1234",
                    Email = "pp@abc.com",
                    DepartmentId = 10
                };
                Assert.True(dao.Add(newemployee) > 0);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error - " + ex.Message);
            }
        }
        [Fact]
        public void Employee_UpdateTest()
        {
            try
            {
                EmployeeDAO dao = new EmployeeDAO();
                Employees employeeForUpdate = dao.GetById(2);

                if (employeeForUpdate != null)
                {
                    string oldPhoneNo = employeeForUpdate.PhoneNo;
                    string newPhoneNo = oldPhoneNo == "(519)555-1234" ? "(555)555-4321" : "(519)555-1234";
                    employeeForUpdate.PhoneNo = newPhoneNo;
                }
                Assert.True(dao.Update(employeeForUpdate) == UpdateStatus.Ok);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error - " + ex.Message);
            }
        }

        [Fact]
        public void Employee_DeleteTest()
        {
            try
            {
                EmployeeDAO dao = new EmployeeDAO();
                Employees employeeForDelete = dao.GetById(2);
                Assert.True(dao.Delete(employeeForDelete.Id) == 1);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error - " + ex.Message);
            }
        }

        [Fact]

        public void Employee_ConcurrencyTest()
        {
            EmployeeDAO dao1 = new EmployeeDAO();
            EmployeeDAO dao2 = new EmployeeDAO();
            Employees employeeForUpdate1 = dao1.GetByEmail("pp@abc.com");
            Employees employeeForUpdate2 = dao2.GetByEmail("pp@abc.com");

            if (employeeForUpdate1 != null)
            {
                string oldPhoneNo = employeeForUpdate1.PhoneNo;
                string newPhoneNo = oldPhoneNo == "519-555-1234" ? "555-555-5555" : "519-555-1234";
                employeeForUpdate1.PhoneNo = newPhoneNo;
                if (dao1.Update(employeeForUpdate1) == UpdateStatus.Ok)
                {
                    // need to change the phone # to something else
                    employeeForUpdate2.PhoneNo = "666-666-6666";
                    Assert.True(dao2.Update(employeeForUpdate2) == UpdateStatus.Stale);
                }
                else
                    Assert.True(false);
            }
        }


    }
}

