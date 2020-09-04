using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Commands;
using Zadatak_1.Models;
using Zadatak_1.Views;

namespace Zadatak_1.ViewModels
{
    class EditEmployeeViewModel : BaseViewModel
    {
        EditEmployeeView editEmployeeView;
        Employees employees = new Employees();
        Sectors sectors = new Sectors();
        Genders genders = new Genders();
        Locations locations = new Locations();

        public vwEmployee OldEmployee { get; set; }

        private vwEmployee employee;

        public vwEmployee Employee
        {
            get
            {
                return employee;
            }
            set
            {
                employee = value;
                OnPropertyChanged("Employee");
            }
        }
        private vwEmployee manager;

        public vwEmployee Manager
        {
            get
            {
                return manager;
            }
            set
            {
                manager = value;
                OnPropertyChanged("Manager");
            }
        }

        private List<vwEmployee> managerList;

        public List<vwEmployee> ManagerList
        {
            get
            {
                return managerList;
            }
            set
            {
                managerList = value;
                OnPropertyChanged("EmployeeList");
            }
        }

        private vwLocation location;

        public vwLocation Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
                OnPropertyChanged("Location");
            }
        }
        private List<vwLocation> locationList;

        public List<vwLocation> LocationList
        {
            get
            {
                return locationList;
            }
            set
            {
                locationList = value;
                OnPropertyChanged("LocationList");
            }
        }

        private vwGender gender;
        public vwGender Gender
        {
            get
            {
                return gender;
            }
            set
            {
                gender = value;
                OnPropertyChanged("Gender");
            }
        }

        private List<vwGender> genderList;
        public List<vwGender> GenderList
        {
            get
            {
                return genderList;
            }
            set
            {
                genderList = value;
                OnPropertyChanged("GenderList");
            }
        }

        private string sector;

        public string Sector
        {
            get
            {
                return sector;
            }
            set
            {
                sector = value;
                OnPropertyChanged("Sector");
            }
        }

        private ICommand save;

        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SaveExecute(), param => CanSaveExecute());
                }
                return save;
            }
        }

        private ICommand cancel;

        public ICommand Cancel
        {
            get
            {
                if (cancel == null)
                {
                    cancel = new RelayCommand(param => CancelExecute(), param => CanCancelExecute());
                }
                return cancel;
            }
        }
        public EditEmployeeViewModel(EditEmployeeView editEmployeeView, vwEmployee employeeToEdit)
        {
            this.editEmployeeView = editEmployeeView;
            Employee = employeeToEdit;
            Sector = employeeToEdit.SectorName;
            GenderList = genders.GetAllGenders();
            LocationList = locations.GetAllLocations();
            ManagerList = employees.GetAllManagers(employee);
            //gets user initial values before editing
            OldEmployee = new vwEmployee
            {
                NameAndSurname = employeeToEdit.NameAndSurname,
                NumberOfIdentityCard = employeeToEdit.NumberOfIdentityCard,
                PhoneNumber = employeeToEdit.PhoneNumber,
                JMBG = employeeToEdit.JMBG,
                Gender = employeeToEdit.Gender,
                Sector = employeeToEdit.Sector,
                SectorName = employeeToEdit.SectorName,
                Location = employeeToEdit.Location,
                Manager = employeeToEdit.Manager
            };
        }
        /// <summary>
        /// This method invokes a methods for editing employee achecks if sector of employee exists. If not exist, invokes a method for adding sector.
        /// </summary>
        public void SaveExecute()
        {
            if (String.IsNullOrEmpty(Employee.NameAndSurname) || String.IsNullOrEmpty(Employee.NumberOfIdentityCard) || String.IsNullOrEmpty(Employee.JMBG) || Gender == null
               || String.IsNullOrEmpty(Employee.PhoneNumber) || String.IsNullOrEmpty(Sector) || Location == null)
            {
                MessageBox.Show("Please fill all fields.", "Notification");
            }
            else
            {
                try
                {
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to save the changes?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        if (sectors.IsSectorExists(Sector) == false)
                        {
                            sectors.AddSector(sector);
                        }
                        //invoking method to find sector and setting that sector to employee sector
                        Employee.Sector = sectors.FindSector(sector);
                        Employee.LocationId = Convert.ToInt32(Location.LocationId);
                        Employee.Gender = Convert.ToInt32(Gender.GenderId);
                        if (Manager != null)
                        {
                            Employee.Manager = Convert.ToInt32(Manager.EmployeeId);
                        }
                        bool isEdited = employees.EditEmployee(Employee);
                        if (isEdited)
                        {
                            MessageBox.Show("Employee is edited.", "Notification", MessageBoxButton.OK);
                            sectors.DeleteSector(OldEmployee.SectorName);
                            editEmployeeView.Close();
                        }
                        else
                        {
                            MessageBox.Show("Employee cannot be edited.", "Notification", MessageBoxButton.OK);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        /// <summary>
        /// This method checks if data about employee is changed.
        /// </summary>
        /// <returns>True if can save data, false if not.</returns>
        public bool CanSaveExecute()
        {
            if (Manager != null)
            {

                if (Employee.NameAndSurname != OldEmployee.NameAndSurname || Employee.NumberOfIdentityCard != OldEmployee.NumberOfIdentityCard ||
                    Employee.JMBG != OldEmployee.JMBG || Employee.Gender != OldEmployee.Gender || Employee.PhoneNumber != OldEmployee.PhoneNumber
                   || Sector != OldEmployee.SectorName || Employee.Location != OldEmployee.Location || Manager.EmployeeId != OldEmployee.Manager)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if(Employee.NameAndSurname != OldEmployee.NameAndSurname || Employee.NumberOfIdentityCard != OldEmployee.NumberOfIdentityCard ||
                    Employee.JMBG != OldEmployee.JMBG || Employee.Gender != OldEmployee.Gender || Employee.PhoneNumber != OldEmployee.PhoneNumber
                   || Sector != OldEmployee.SectorName || Employee.Location != OldEmployee.Location)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// This method invokes method for closing window of editing employee.
        /// </summary>
        public void CancelExecute()
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel editing the employee?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    editEmployeeView.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public bool CanCancelExecute()
        {
            return true;
        }
    }
}
