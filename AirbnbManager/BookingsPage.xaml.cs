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
    /// Interaction logic for BookingsPage.xaml
    /// </summary>
    public partial class BookingsPage : UserControl
    {
        DatabaseEntitiesModel ctx = new DatabaseEntitiesModel();
        CollectionViewSource bookingVSource;


        public BookingsPage()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            bookingVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("bookingViewSource")));
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

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            ctx.Bookings.Load();
        }

        private void btnDeleteBooking_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                Booking booking = null;
                booking = (Booking)bookingDataGrid.SelectedItem;
                ctx.Bookings.Remove(booking);
                ctx.SaveChanges();
                bookingVSource.View.Refresh();
            }
            catch (Exception)
            {
                MessageBox.Show("Please select the booking you want to delete!");
            }

        }
    }
}
