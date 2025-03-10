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
using TravelAgency.Views;

namespace TravelAgency.ViewModels
{
    public class CustomerViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Customer> Customers { get; set; }
        private Customer _selectedCustomer;

        public Customer SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                if (_selectedCustomer != value)
                {
                    _selectedCustomer = value;
                    OnPropertyChanged(nameof(SelectedCustomer));
                }
            }
        }


        public ICommand AddCustomerCommand { get; }
        public ICommand DeleteCustomerCommand { get; }
        public ICommand UpdateCustomerCommand { get; }
        public ICommand SearchCustomersCommand { get; }
        public ICommand AllCustomersCommand { get; }

        public CustomerViewModel()
        {
            var hotels = CustomerDataAccess.GetAllCustomers();

            if (hotels == null)
            {
                Console.WriteLine("Customers list is null! Check database connection.");
                hotels = new List<Customer>();
            }

            Customers = new ObservableCollection<Customer>(hotels);

            AddCustomerCommand = new RelayCommand(AddCustomer);
            DeleteCustomerCommand = new RelayCommand(DeleteCustomer);
            UpdateCustomerCommand = new RelayCommand(UpdateCustomer);
            SearchCustomersCommand = new RelayCommand(param => SearchCustomers(param.ToString()));
            AllCustomersCommand = new RelayCommand(AllCustomers);
        }



        private void AddCustomer()
        {
            AddCustomerWindow dialog = new AddCustomerWindow();

            bool? dialogResult = dialog.ShowDialog();

            if ((bool)dialogResult)
            {
                Customer pom = dialog.Customer;
                string message2 = (string)Application.Current.Resources["ConfirmAdd"] + ": " + pom + "?";
                MessageDialog dialog2 = new MessageDialog(message2);
                bool? dialogResult2 = dialog2.ShowDialog();
                if ((bool)dialogResult2)
                {
                    if (CustomerDataAccess.AddCustomer(pom, dialog.Customer.PhoneNumber))
                    {
                        Customers.Add(pom);
                        OnPropertyChanged(nameof(Customers));
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
                else
                {
                    string message3 = (string)Application.Current.Resources["ActionCanceled"];
                    MessageWithoutOptionDialog dialog3 = new MessageWithoutOptionDialog(message3);
                    dialog3.ShowDialog();
                }

            }
        }

        private void AllCustomers()
        {
            var hotels = CustomerDataAccess.GetAllCustomers();
            if (hotels == null)
            {
                Console.WriteLine("Customers list is null! Check database connection.");
                hotels = new List<Customer>();
            }
            else
            {
                Customers = new ObservableCollection<Customer>(hotels);
                OnPropertyChanged(nameof(Customers));
            }
        }

        private void SearchCustomers(string destName)
        {
            if (destName.Trim().Length == 0)

            {
                this.AllCustomers();
                return;
            }
            var foundCustomers = CustomerDataAccess.GetCustomersByFirstOrLastName(destName);
            if (foundCustomers == null)
            {
                Console.WriteLine("Search is null.");
            }
            else
            {
                Customers = new ObservableCollection<Customer>(foundCustomers);
                OnPropertyChanged(nameof(Customers));
            }
        }

        private void DeleteCustomer(object obj)
        {
            if (SelectedCustomer == null)
            {
                string message = (string)Application.Current.Resources["RowNotSelected"];
                MessageWithoutOptionDialog dialog = new MessageWithoutOptionDialog(message);
                dialog.ShowDialog();

                return;
            }


            string message2 = (string)Application.Current.Resources["ConfirmDelete"] + " " + SelectedCustomer.FullName + "?";
            MessageDialog dialog2 = new MessageDialog(message2);
            bool? dialogResult2 = dialog2.ShowDialog();


            if ((bool)dialogResult2)
            {
                if (CustomerDataAccess.DeleteCustomer(SelectedCustomer.Jmb))
                {
                    Customers.Remove(SelectedCustomer);
                    OnPropertyChanged(nameof(Customers));
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
            else
            {
                string message3 = (string)Application.Current.Resources["ActionCanceled"];
                MessageWithoutOptionDialog dialog3 = new MessageWithoutOptionDialog(message3);
                dialog3.ShowDialog();
            }
        }


        private void UpdateCustomer()
        {

            if (SelectedCustomer != null)
            {
                UpdateCustomerWindow dialog = new UpdateCustomerWindow(SelectedCustomer);

                bool? dialogResult = dialog.ShowDialog();

                if ((bool)dialogResult)
                {
                    Customer pom = dialog.Customer;
                    string message2 = (string)Application.Current.Resources["ConfirmUpdate"] + ": " + pom + "?";
                    MessageDialog dialog2 = new MessageDialog(message2);
                    bool? dialogResult2 = dialog2.ShowDialog();
                    if ((bool)dialogResult2)
                    {
                        if (CustomerDataAccess.UpdateCustomer(pom, SelectedCustomer.Jmb))
                        {
                            SelectedCustomer.Jmb = pom.Jmb;
                            SelectedCustomer.FirstName = pom.FirstName;
                            SelectedCustomer.Address = pom.Address;
                            SelectedCustomer.Email = pom.Email;
                            SelectedCustomer.LastName = pom.LastName;
                            SelectedCustomer.DateOfBirth = pom.DateOfBirth;


                            OnPropertyChanged(nameof(SelectedCustomer));
                            OnPropertyChanged(nameof(Customers));
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
                    else
                    {
                        string message3 = (string)Application.Current.Resources["ActionCanceled"];
                        MessageWithoutOptionDialog dialog3 = new MessageWithoutOptionDialog(message3);
                        dialog3.ShowDialog();
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
