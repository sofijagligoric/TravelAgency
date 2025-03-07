using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TravelAgency.DataAccess;
using TravelAgency.Models;
using TravelAgency.Util;
using TravelAgency.Views;

namespace TravelAgency.ViewModels
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Employee> Employees { get; set; }
        private Employee _selectedEmployee;

        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                if (_selectedEmployee != value)
                {
                    _selectedEmployee = value;
                    OnPropertyChanged(nameof(SelectedEmployee));
                }
            }
        }


        public ICommand AddEmployeeCommand { get; }
        public ICommand DeleteEmployeeCommand { get; }
        public ICommand UpdateEmployeeCommand { get; }
        public ICommand SearchEmployeesCommand { get; }
        public ICommand AllEmployeesCommand { get; }

        public EmployeeViewModel()
        {
            var hotels = EmployeeDataAccess.GetAllEmployees();

            if (hotels == null)
            {
                Console.WriteLine("Employees list is null! Check database connection.");
                hotels = new List<Employee>();
            }

            Employees = new ObservableCollection<Employee>(hotels);

            AddEmployeeCommand = new RelayCommand(AddEmployee);
            DeleteEmployeeCommand = new RelayCommand(DeleteEmployee);
            UpdateEmployeeCommand = new RelayCommand(UpdateEmployee);
            SearchEmployeesCommand = new RelayCommand(param => SearchEmployees(param.ToString()));
            AllEmployeesCommand = new RelayCommand(AllEmployees);

        }



        private void AddEmployee()
        {
            AddEmployeeWindow dialog = new AddEmployeeWindow();

            bool? dialogResult = dialog.ShowDialog();

            if ((bool)dialogResult)
            {
                Employee pom = dialog.Employee;
                string message2 = (string)Application.Current.Resources["ConfirmAdd"] + ": " + pom + "?";
                MessageDialog dialog2 = new MessageDialog(message2);
                bool? dialogResult2 = dialog2.ShowDialog();
                if ((bool)dialogResult2)
                {
                    if (EmployeeDataAccess.AddEmployee(pom, dialog.Employee.PhoneNumber))
                    {
                        Employees.Add(pom);
                        OnPropertyChanged(nameof(Employees));
                        string message = (string)Application.Current.Resources["SuccessfullyAdded"];
                        MessageWithoutOptionDialog dialog3 = new MessageWithoutOptionDialog(message);
                        dialog3.ShowDialog();
                    }
                    else
                    {
                        string message3 = (string)Application.Current.Resources["FailedAdd"];
                        MessageWithoutOptionDialog dialog3 = new MessageWithoutOptionDialog(message3);
                        dialog3.ShowDialog();
                    }
                }

            }
        }

        private void AllEmployees()
        {
            var hotels = EmployeeDataAccess.GetAllEmployees();
            if (hotels == null)
            {
                Console.WriteLine("Employees list is null! Check database connection.");
                hotels = new List<Employee>();
            }
            else
            {
                Employees = new ObservableCollection<Employee>(hotels);
                OnPropertyChanged(nameof(Employees));
            }
        }

        private void SearchEmployees(string destName)
        {
            if (destName.Trim().Length == 0)

            {
                this.AllEmployees();
                return;
            }
            var foundEmployees = EmployeeDataAccess.GetEmployeesByFirstOrLastName(destName);
            if (foundEmployees == null)
            {
                Console.WriteLine("Search is null.");
            }
            else
            {
                Employees = new ObservableCollection<Employee>(foundEmployees);
                OnPropertyChanged(nameof(Employees));
            }
        }


        private void DeleteEmployee(object obj)
        {
            if (SelectedEmployee == null)
            {
                string message = (string)Application.Current.Resources["RowNotSelected"];
                MessageWithoutOptionDialog dialog = new MessageWithoutOptionDialog(message);
                dialog.ShowDialog();

                return;
            }

            if (SelectedEmployee.RoleType.Equals("admin"))
            {
                string message = (string)Application.Current.Resources["CantDeleteAdmin"];
                MessageWithoutOptionDialog dialog = new MessageWithoutOptionDialog(message);
                dialog.ShowDialog();

                return;
            }


            string message2 = (string)Application.Current.Resources["ConfirmDelete"] + " " + SelectedEmployee.FullName + "?";
            MessageDialog dialog2 = new MessageDialog(message2);
            bool? dialogResult2 = dialog2.ShowDialog();


            if ((bool)dialogResult2)
            {
                if (EmployeeDataAccess.DeleteEmployee(SelectedEmployee.Jmb))
                {
                    Employees.Remove(SelectedEmployee);
                    OnPropertyChanged(nameof(Employees));
                    string message = (string)Application.Current.Resources["SuccessfulDelete"];
                    MessageWithoutOptionDialog dialog = new MessageWithoutOptionDialog(message);
                    dialog.ShowDialog();

                }
                else
                {

                    string message = (string)Application.Current.Resources["FailedDelete"];
                    MessageWithoutOptionDialog dialog = new MessageWithoutOptionDialog(message);
                    dialog.ShowDialog();
                }
            }
        }


        private void UpdateEmployee()
        {

            if (SelectedEmployee != null)
            {
                UpdateEmployeeWindow dialog = new UpdateEmployeeWindow(SelectedEmployee);

                bool? dialogResult = dialog.ShowDialog();

                if ((bool)dialogResult)
                {
                    Employee pom = dialog.Employee;
                    string message2 = (string)Application.Current.Resources["ConfirmUpdate"] + ": " + pom + "?";
                    MessageDialog dialog2 = new MessageDialog(message2);
                    bool? dialogResult2 = dialog2.ShowDialog();
                    if ((bool)dialogResult2)
                    {
                        if (EmployeeDataAccess.UpdateEmployee(pom, SelectedEmployee.Jmb))
                        {
                            SelectedEmployee.Jmb = pom.Jmb;
                            SelectedEmployee.FirstName = pom.FirstName;
                            SelectedEmployee.Address = pom.Address;
                            SelectedEmployee.Email = pom.Email;
                            SelectedEmployee.LastName = pom.LastName;
                            SelectedEmployee.DateOfBirth = pom.DateOfBirth;
                            SelectedEmployee.Username = pom.Username;
                            SelectedEmployee.Salary = pom.Salary;

                            OnPropertyChanged(nameof(SelectedEmployee));
                            OnPropertyChanged(nameof(Employees));
                            string message3 = (string)Application.Current.Resources["SuccessfulUpdate"];
                            MessageWithoutOptionDialog dialog3 = new MessageWithoutOptionDialog(message3);
                            dialog3.ShowDialog();
                        }
                        else
                        {
                            string message3 = (string)Application.Current.Resources["FailedUpdate"];
                            MessageWithoutOptionDialog dialog3 = new MessageWithoutOptionDialog(message3);
                            dialog3.ShowDialog();
                        }
                    }

                }
            }
            else
            {
                string message = (string)Application.Current.Resources["RowNotSelected"];
                MessageWithoutOptionDialog dialog = new MessageWithoutOptionDialog(message);
                dialog.ShowDialog();
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
