using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Zadatak_1.Helper;

namespace Zadatak_1.Models
{
    class Employees
    {
        /// <summary>
        /// This method adds employee to DbSet and then save changes to database.
        /// </summary>
        /// <param name="employeeToAdd">Employee to be added.</param>
        /// <returns>True if added, false if not.</returns>
        public bool AddEmployee(vwEmployee employeeToAdd)
        {
            try
            {
                using (Employee_DataEntities context = new Employee_DataEntities())
                {
                    tblEmployee employee = new tblEmployee
                    {
                        NameAndSurname = employeeToAdd.NameAndSurname,
                        DateOfBirth = CalculateDateOfBirth.Calculate(employeeToAdd.JMBG),
                        NumberOfIdentityCard = employeeToAdd.NumberOfIdentityCard,
                        JMBG = employeeToAdd.JMBG,
                        Gender = employeeToAdd.Gender,
                        PhoneNumber = employeeToAdd.PhoneNumber,
                        Sector = employeeToAdd.Sector,
                        LocationId = employeeToAdd.LocationId,
                        Manager = employeeToAdd.Manager                        
                    };
                    context.tblEmployees.Add(employee);
                    context.SaveChanges();
                    employeeToAdd.EmployeeId = employee.EmployeeId;
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return false;
            }
        }
        /// <summary>
        /// This method creates a list of data from view of employees.
        /// </summary>
        /// <returns>List of employees.</returns>
        public List<vwEmployee> GetAllEmployees()
        {
            try
            {
                using (Employee_DataEntities context = new Employee_DataEntities())
                {
                    List<vwEmployee> employees = new List<vwEmployee>();
                    employees = (from x in context.vwEmployees select x).ToList();
                    return employees;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
        /// <summary>
        /// This method edits employee in DbSet and then saves changes to database.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>True if edited, false if not.</returns>
        public bool EditEmployee(vwEmployee employee)
        {
            try
            {
                using (Employee_DataEntities context = new Employee_DataEntities())
                {
                    tblEmployee employeeToEdit = context.tblEmployees.Where(x => x.EmployeeId == employee.EmployeeId).FirstOrDefault();
                    employeeToEdit.NameAndSurname = employee.NameAndSurname;
                    employeeToEdit.DateOfBirth = employee.DateOfBirth;
                    employeeToEdit.JMBG = employee.JMBG;
                    employeeToEdit.NumberOfIdentityCard = employee.NumberOfIdentityCard;
                    employeeToEdit.Gender = employee.Gender;
                    employeeToEdit.PhoneNumber = employee.PhoneNumber;
                    employeeToEdit.Sector = employee.Sector;
                    employeeToEdit.LocationId = employee.LocationId;
                    employeeToEdit.Manager = employee.Manager;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return false;
            }
        }
        /// <summary>
        /// This method deletes employee from DbSet and then saves changes to database.
        /// </summary>
        /// <param name="employeeID">Employee id.</param>
        /// <returns>True if deleted, false if not.</returns>
        public bool DeleteEmployee(int employeeID)
        {
            try
            {
                using (Employee_DataEntities context = new Employee_DataEntities())
                {
                    //creating a list of employees for this manager
                    var employeeOfThisManager = context.tblEmployees.Where(x => x.Manager == employeeID).ToList();
                    //if the list is not empty, setting manager id to null for every employee in that list
                    if (employeeOfThisManager.Count() > 0)
                    {
                        foreach (var employee in employeeOfThisManager)
                        {
                            employee.Manager = null;
                        }
                    }
                    //finding employee with forwarded id
                    tblEmployee employeeToDelete = context.tblEmployees.Where(x => x.EmployeeId == employeeID).FirstOrDefault();
                    //if that employee was the only in the sector, deleting sector
                    var peopleInSector = context.tblEmployees.Where(x => x.Sector == employeeToDelete.Sector).ToList();                    
                    if (peopleInSector.Count() == 1)
                    {
                        var sector = context.tblSectors.Where(x => x.SectorId == employeeToDelete.Sector).FirstOrDefault();
                        context.tblSectors.Remove(sector);
                        context.SaveChanges();
                    }
                    //removing employee from DbSet and saving changes to database
                    context.tblEmployees.Remove(employeeToDelete);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return false;
            }
        }
        /// <summary>
        /// This method creates a list of possible managers of employee.
        /// </summary>
        /// <param name="employee">Employee.</param>
        /// <returns>List of possible managers.</returns>
        public List<vwEmployee> GetAllManagers(vwEmployee employee)
        {
            try
            {
                using (Employee_DataEntities context = new Employee_DataEntities())
                {
                    List<vwEmployee> employees = new List<vwEmployee>();
                    //inserting into list all employees except forwarded employee (finding him based on jmbg)
                    employees = context.vwEmployees.Where(x => x.JMBG != employee.JMBG).ToList();
                    return employees;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
        /// <summary>
        /// This method creates a list of data from view of all employees (including managers).
        /// </summary>
        /// <returns>List of employees.</returns>
        public List<tblEmployee> GetEmployees()
        {
            try
            {
                using (Employee_DataEntities context = new Employee_DataEntities())
                {
                    List<tblEmployee> employees = new List<tblEmployee>();
                    employees = (from x in context.tblEmployees select x).ToList();
                    return employees;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }        
    }
}
