using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DatabaseModel;
using System.Data.Entity;
using System.Data;

namespace AirbnbManager
{
    /// <summary>
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class CustomersPage : UserControl
    {
        DatabaseEntitiesModel ctx = new DatabaseEntitiesModel();
        
        CollectionViewSource customerVSource;
        CollectionViewSource propertyVSource;
        CollectionViewSource bookingVSource;
        public CustomersPage()
        {
            DataContext = this;
            InitializeComponent();
           

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            customerVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("customerViewSource")));
            customerVSource.Source = ctx.Customers.Local;
            ctx.Customers.Load();
            propertyVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("propertyViewSource")));
            propertyVSource.Source = ctx.Properties.Local;
            ctx.Properties.Load();
            bookingVSource= ((System.Windows.Data.CollectionViewSource)(this.FindResource("bookingViewSource")));
            bookingVSource.Source = ctx.Bookings.Local;
            ctx.Bookings.Load();



            // Do not load your data at design time.
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//Load your data here and assign the result to the CollectionViewSource.
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }
        }




        private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                Customer customer = null;
                var idExists = ctx.Customers.Find(Int32.Parse(customerIdTextBox.Text.Trim()));
                if (idExists != null) MessageBox.Show("Customer Id already exists!");
                else
                {


                    customer = new Customer()
                    {
                        
                        CustomerId = Int32.Parse(customerIdTextBox.Text.Trim()),
                        FirstName = firstNameTextBox.Text.Trim(),
                        LastName = lastNameTextBox.Text.Trim(),
                        Age = Int32.Parse(ageTextBox.Text.Trim()),
                        CNP = UInt64.Parse(cNPTextBox.Text.Trim())
                    };

                    ctx.Customers.Add(customer);
                    ctx.SaveChanges();
                    customerVSource.View.Refresh();


                }
            }
            catch (Exception)
            {

                MessageBox.Show("Empty fields detected or wrong values!");
            }
            
         
        }
        private void btnEditCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Customer customer = null;
                if(customerDataGrid.SelectedItem != null)
                {
                    int idChange = Int32.Parse(customerIdTextBox.Text.Trim());
                    customer = (Customer)customerDataGrid.SelectedItem;
                    if(customer.CustomerId == idChange)
                    {  
                        customer.FirstName = firstNameTextBox.Text.Trim();
                        customer.LastName = lastNameTextBox.Text.Trim();
                        customer.Age = Int32.Parse(ageTextBox.Text.Trim());
                        customer.CNP = UInt64.Parse(cNPTextBox.Text.Trim());

                        ctx.SaveChanges();
                        customerVSource.View.Refresh();
                    }
                    else
                    {
                        MessageBox.Show("You cannot change the Customer Id!");
                        customerIdTextBox.Text = customer.CustomerId.ToString();
                    }
                }

            }
            catch (Exception)
            {

                MessageBox.Show("Please select the row you want to edit!");
            }
        }

        private void btnDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Customer customer = null;
                customer = (Customer)customerDataGrid.SelectedItem;
                ctx.Customers.Remove(customer);
                ctx.SaveChanges();

                customerVSource.View.Refresh();


            }

            catch (Exception)
            {
                MessageBox.Show("Please select the row you want to delete!");
            }
        }


        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            DatabaseEntitiesModel ctx = new DatabaseEntitiesModel();
            propertyVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("propertyViewSource")));
            propertyVSource.Source = ctx.Properties.Local;
            ctx.Properties.Load();
        }

        private void btnBook_Click(object sender, RoutedEventArgs e)
        {

            if (customerDataGrid.SelectedCells.Count > 0)
            {
                if (propertyListBox.SelectedIndex >= 0)
                {
                    if (checkIn.SelectedDate.HasValue)
                    {
                        if (checkOut.SelectedDate.HasValue)
                        {
                            if (checkIn.SelectedDate < checkOut.SelectedDate)
                            {
                                Property properyGetPrice = ctx.Properties.Find(propertyListBox.SelectedValue.ToString());
                                //diferenta de zile dintre check-out si check-in
                                DateTime start = checkIn.SelectedDate.Value;
                                DateTime finish = checkOut.SelectedDate.Value;
                                TimeSpan difference = finish.Subtract(start);
                                Booking booking = new Booking()
                                {
                                    PropertyCode = propertyListBox.SelectedValue.ToString(),
                                    CustomerId = ((Customer)customerDataGrid.SelectedItem).CustomerId,
                                    CheckIn = checkIn.SelectedDate,
                                    CheckOut = checkOut.SelectedDate,
                                    Price = properyGetPrice.Price * Int32.Parse(difference.TotalDays.ToString())
                                };
                                ctx.Bookings.Add(booking);
                                ctx.SaveChanges();
                                MessageBox.Show("Successfully booked! Check the Bookings page.");  
                            }
                            else MessageBox.Show("Check-in must be earlier than Check-out");
                        }
                        else MessageBox.Show("Please select a check-out date.");
                    }
                    else MessageBox.Show("Please select a check-in date.");
                }
                else MessageBox.Show("Please select a Property Code from the list.");
            }
            else MessageBox.Show("Please select a customer from the table.");







        }

        private void propertyListBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }
    }
}


