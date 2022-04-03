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
        public MainWindow()
        {
            InitializeComponent();
            btnProperties.IsChecked = true;
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

    }
}
