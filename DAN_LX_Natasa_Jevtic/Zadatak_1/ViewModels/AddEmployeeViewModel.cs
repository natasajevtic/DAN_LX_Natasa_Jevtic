using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Commands;
using Zadatak_1.Models;
using Zadatak_1.Views;

namespace Zadatak_1.ViewModels
{
    class AddEmployeeViewModel : BaseViewModel
    {
        AddEmployeeView addEmployeeView;      
        Employees employees = new Employees();
        Sectors sectors = new Sectors();
        Genders genders = new Genders();
        Locations locations = new Locations();

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

        public AddEmployeeViewModel(AddEmployeeView addEmployeeView)
        {
            this.addEmployeeView = addEmployeeView;
            GenderList = genders.GetAllGenders();
            LocationList = locations.GetAllLocations();
            ManagerList = employees.GetAllEmployees();
            Employee = new vwEmployee();
        }
        /// <summary>
        /// This method invokes a methods for adding employee and checks if sector of employee exists. If not exist, invokes a method for adding sector.
        /// </summary>       
        public void SaveExecute()
        { 
            if (String.IsNullOrEmpty(Employee.NameAndSurname) || String.IsNullOrEmpty(Employee.NumberOfIdentityCard) || String.IsNullOrEmpty(Employee.JMBG) || Gender == null
               || String.IsNullOrEmpty(Employee.PhoneNumber) || String.IsNullOrEmpty(Sector) || Location==null)
            {
                MessageBox.Show("Please fill all fields.", "Notification");
            }
            else
            {
                try
                {
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to save the employee?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
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
                        bool isCreated = employees.AddEmployee(Employee);
                        if (isCreated)
                        {
                            MessageBox.Show("Employee is created.", "Notification", MessageBoxButton.OK);
                            addEmployeeView.Close();
                        }
                        else
                        {
                            MessageBox.Show("Employee cannot be created.", "Notification", MessageBoxButton.OK);
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
        /// This method checks if user input is valid.
        /// </summary>
        /// <returns>True if data is valid, false if not.</returns>  
        public bool CanSaveExecute()
        {
            return true;
        }
        /// <summary>
        /// This method invokes method for closing window of adding employee.
        /// </summary>
        public void CancelExecute()
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to cancel creating the employee?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    addEmployeeView.Close();
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
