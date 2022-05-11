using DatabaseModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace AirbnbManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DatabaseEntitiesModel ctx = new DatabaseEntitiesModel();

        public MainWindow()
        {
            InitializeComponent();
            btnProperties.IsChecked = true;
          
                ctx.PasswordTable.Load();
   
        }

        private void btnProperties_Checked(object sender, RoutedEventArgs e)
        {
            SetActiveUserControl(properties);
        }

        private void btnCustomers_Checked(object sender, RoutedEventArgs e)
        {
            SetActiveUserControl(customers);

        }

        private void btnCleaning_Checked(object sender, RoutedEventArgs e)
        {
            SetActiveUserControl(cleaning);
        }
        private void btnBookings_Checked(object sender, RoutedEventArgs e)
        {
            SetActiveUserControl(bookings);
        }

        private void btnPaymentHistory_Checked(object sender, RoutedEventArgs e)
        {
            SetActiveUserControl(paymentHistory);
            properties.Visibility = Visibility.Collapsed;
        }

        public void SetActiveUserControl(UserControl control)
        {
            properties.Visibility = Visibility.Collapsed;
            customers.Visibility = Visibility.Collapsed;
            cleaning.Visibility = Visibility.Collapsed;
            bookings.Visibility = Visibility.Collapsed;
            paymentHistory.Visibility = Visibility.Collapsed;

            control.Visibility = Visibility.Visible;
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            changePasswordGrid.Visibility = Visibility.Hidden;
            if (loginTextBox.Password == ctx.PasswordTable.Find(1).Password)
            {
                loginGrid.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("    Incorrect Password    ");
                loginTextBox.Password = "Password";
                
            }
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            loginTextBox.Password = "Password";
            loginGrid.Visibility = Visibility.Visible;
            
        }

        private void loginTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (loginTextBox.Password == "Password")
            {
                loginTextBox.Password = "";
            }
            
        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            if (changePasswordGrid.Visibility == Visibility.Hidden)
                changePasswordGrid.Visibility = Visibility.Visible;
            else changePasswordGrid.Visibility = Visibility.Hidden;
            currentPassword.Password = "";
            newPassword.Password = "";
        }

        private void Image_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void btnSavePassword_Click(object sender, RoutedEventArgs e)
        {
            if (currentPassword.Password == ctx.PasswordTable.Find(1).Password)
            {
                ctx.PasswordTable.Find(1).Password = newPassword.Password;
                ctx.SaveChanges();
                MessageBox.Show("  The new password was saved.  ");
                currentPassword.Password = "";
                newPassword.Password = "";
            }
            else MessageBox.Show("    Incorrect Password    ");
        }

        private void loginTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (loginTextBox.IsFocused && e.Key == Key.Enter)
            {
                changePasswordGrid.Visibility = Visibility.Hidden;
                if (loginTextBox.Password == ctx.PasswordTable.Find(1).Password)
                {
                    loginGrid.Visibility = Visibility.Collapsed;
                }
                else
                {
                    MessageBox.Show("    Incorrect Password    ");
                    loginTextBox.Password = "Password";

                }
            }

        }
    }
}
