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
using Stripe;
using Stripe.Infrastructure;
using System.Net.Mail;

namespace AirbnbManager
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class PropertiesPage : UserControl
    {
        DatabaseEntitiesModel ctx = new DatabaseEntitiesModel();
        CollectionViewSource propertyVSource;
        CollectionViewSource cleaningVSource;
        CollectionViewSource paymentHistoryVSource;



        public PropertiesPage()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            propertyVSource= ((System.Windows.Data.CollectionViewSource)(this.FindResource("propertyViewSource")));
            propertyVSource.Source = ctx.Properties.Local;
            ctx.Properties.Load();
            cleaningVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("cleaningViewSource")));
            cleaningVSource.Source = ctx.Cleanings.Local;
            ctx.Cleanings.Load();
            paymentHistoryVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("paymentHistoryViewSource")));
            paymentHistoryVSource.Source = ctx.PaymentHistories.Local;
            ctx.PaymentHistories.Load();
            paymentGrid.Visibility = Visibility.Collapsed;

            

            // Do not load your data at design time.
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//Load your data here and assign the result to the CollectionViewSource.
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }
        }

        private void btnAddProperty_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
                Property property = null;
                var idExists = ctx.Properties.Find(propertyCodeTextBox.Text.Trim());
                if (idExists != null) MessageBox.Show("PropertyCode already exists!");
                else
                {
                    property = new Property()
                    {
                        PropertyCode = propertyCodeTextBox.Text.Trim(),
                        Address = addressTextBox.Text.Trim(),
                        Rooms = Int32.Parse(roomsTextBox.Text.Trim()),
                        Price = Decimal.Parse(priceTextBox.Text.Trim())
                    };

                    ctx.Properties.Add(property);
                    ctx.SaveChanges();
                    propertyVSource.View.Refresh();

                }
            }
           
            catch (Exception)
            {
                MessageBox.Show("Empty fields detected or wrong values!");
            }

        }

        private void btnEditProperty_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Property property = null;
                if (propertyDataGrid.SelectedItem != null)
                {
                    string idChange = propertyCodeTextBox.Text.Trim();
                    property = (Property)propertyDataGrid.SelectedItem;
                    if (property.PropertyCode == idChange)
                    {
                        property.Address = addressTextBox.Text.Trim();
                        property.Rooms = Int32.Parse(roomsTextBox.Text.Trim());
                        property.Price = Decimal.Parse(priceTextBox.Text.Trim());

                        ctx.SaveChanges();
                        propertyVSource.View.Refresh();
                    }
                    else
                    {
                        MessageBox.Show("You cannot change the PropertyCode!");
                        propertyCodeTextBox.Text = property.PropertyCode.Trim();
                    }
                }
            }
            catch (Exception)
            {
                  MessageBox.Show("Please select the row you want to edit!");
            }
        }

        private void btnDeleteProperty_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Property property = null;
                property = (Property)propertyDataGrid.SelectedItem;
                ctx.Properties.Remove(property);
                ctx.SaveChanges();

                propertyVSource.View.Refresh();
               
            }
            catch (Exception)
            {
                MessageBox.Show("Please select the row you want to delete!");
            }
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            DatabaseEntitiesModel ctx = new DatabaseEntitiesModel();
            cleaningVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("cleaningViewSource")));
            cleaningVSource.Source = ctx.Cleanings.Local;
            ctx.Cleanings.Load();
        }

        private void btnPayServices_Click(object sender, RoutedEventArgs e)
        {
            if (propertyDataGrid.SelectedCells.Count > 0)
            {
                if (cleaningListBox.SelectedIndex >= 0)
                {
                    if (cleaningDate.SelectedDate.HasValue)
                    {
                        if (cleaningDate.SelectedDate.Value>=DateTime.Today)
                        {
                            paymentGrid.Visibility = Visibility.Visible;
                            priceToPay.Text = String.Format("{0} RON", ((Cleaning)cleaningListBox.SelectedItem).Price); 
                        }
                        else MessageBox.Show("Please select a future date!");
                    }
                    else MessageBox.Show("Please select a Cleaning Date.");
                }
                else MessageBox.Show("Please select a Cleaning Company from the list.");
            }
            else MessageBox.Show("Please select a Property from the table.");


        }

        private void btnCancelPayment_Click(object sender, RoutedEventArgs e)
        { 
            paymentGrid.Visibility = Visibility.Collapsed;
            cifreCard1.Text = "0000";
            cifreCard2.Text = "0000";
            cifreCard3.Text = "0000";
            cifreCard4.Text = "0000";
        }

        private void cifreCard1_LostFocus(object sender, RoutedEventArgs e)
        {
            if (cifreCard1.Text.Length!=4)
            {
                cifreCard1.Text = "0000";
            }
        }

        private void cifreCard1_GotFocus(object sender, RoutedEventArgs e)
        {
            if(cifreCard1.Text=="0000")
            {
                cifreCard1.Text = "";
            }
        }

        private void cifreCard2_LostFocus(object sender, RoutedEventArgs e)
        {
            if (cifreCard2.Text.Length != 4)
            {
                cifreCard2.Text = "0000";
            }
        }

        private void cifreCard2_GotFocus(object sender, RoutedEventArgs e)
        {
            if (cifreCard2.Text == "0000")
            {
                cifreCard2.Text = "";
            }
        }

        private void cifreCard3_LostFocus(object sender, RoutedEventArgs e)
        {
            if (cifreCard3.Text.Length != 4)
            {
                cifreCard3.Text = "0000";
            }
        }

        private void cifreCard3_GotFocus(object sender, RoutedEventArgs e)
        {
            if (cifreCard3.Text == "0000")
            {
                cifreCard3.Text = "";
            }
        }

        private void cifreCard4_LostFocus(object sender, RoutedEventArgs e)
        {
            if (cifreCard4.Text.Length != 4)
            {
                cifreCard4.Text = "0000";
            }
        }

        private void cifreCard4_GotFocus(object sender, RoutedEventArgs e)
        {
            if (cifreCard4.Text == "0000")
            {
                cifreCard4.Text = "";
            }
        }





        private void monthTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (monthTextBox.Text == "MONTH")
                monthTextBox.Text = "";
        }

        private void monthTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (monthTextBox.Text == "")
                monthTextBox.Text = "MONTH";
        }
        private void yearTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (yearTextBox.Text == "YEAR")
                yearTextBox.Text = "";
        }

        private void yearTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (yearTextBox.Text == "")
                yearTextBox.Text = "YEAR";
        }
        private async void btnPayNow_Click(object sender, RoutedEventArgs e)
        {
           
                StripeConfiguration.ApiKey = ((Cleaning)cleaningListBox.SelectedItem).StripeKey;

                var optionstoken = new TokenCreateOptions
                {
                   
                    Card = new TokenCardOptions
                    {
                        Number = cifreCard1.Text + cifreCard2.Text + cifreCard3.Text + cifreCard4.Text,
                        ExpMonth = Int64.Parse(monthTextBox.Text),
                        ExpYear = Int64.Parse(yearTextBox.Text),
                        Cvc = cvcTextBox.Password,
                       
                    },
                };
                var servicetoken = new TokenService();
                Token stripetoken = servicetoken.Create(optionstoken);


                var options = new ChargeCreateOptions
                {
                    Amount = (long?)(((Cleaning)cleaningListBox.SelectedItem).Price * 100),
                    Currency = "ron",
                    Source = stripetoken.Id,
                    Description="...",
                    
                };
                var service = new ChargeService();
                await service.CreateAsync(options);


                
                PaymentHistory payment = new PaymentHistory()
                {
                    CompanyId = ((Cleaning)cleaningListBox.SelectedItem).CompanyId,
                    PropertyCode = ((Property)propertyDataGrid.SelectedItem).PropertyCode,
                    CleaningDate = cleaningDate.SelectedDate,
                    Price = ((Cleaning)cleaningListBox.SelectedItem).Price,
                    PaymentDate = DateTime.Today
                };
                ctx.PaymentHistories.Add(payment);
                ctx.SaveChanges();


                // Email
                MailMessage mail = new MailMessage();
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("TestLicenta111@gmail.com");
                mail.To.Add(((Cleaning)cleaningListBox.SelectedItem).Email);
                mail.Subject = "Payment successful";
                mail.Body = "A Cleaning Service was successfully paid." + Environment.NewLine + "Property Address: " + ((Property)propertyDataGrid.SelectedItem).Address + "."
                + Environment.NewLine + "Cleaning Date: " + cleaningDate.SelectedDate + "." + Environment.NewLine + "Price: " + ((Cleaning)cleaningListBox.SelectedItem).Price + ".";

                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential("TestLicenta111@gmail.com", "testlicenta111");
                smtp.EnableSsl = true;

                smtp.Send(mail);


               
                MessageBox.Show("Payment successful. Check it in the Payment History. An email with all the details was sent to "+ ((Cleaning)cleaningListBox.SelectedItem).Email+".");

          
            
        }

        private void cifreCard1_KeyUp(object sender, KeyEventArgs e)
        {
            if (cifreCard1.Text.Length == 4)
                cifreCard2.Focus();
        }

        private void cifreCard2_KeyUp(object sender, KeyEventArgs e)
        {
            if (cifreCard2.Text.Length == 4)
                cifreCard3.Focus();
        }

        private void cifreCard3_KeyUp(object sender, KeyEventArgs e)
        {
            if (cifreCard3.Text.Length == 4)
                cifreCard4.Focus();
        }

        private void monthTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (monthTextBox.Text.Length == 2)
                yearTextBox.Focus();
        }

        
    }
}
