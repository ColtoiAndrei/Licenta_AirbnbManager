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
    /// Interaction logic for PaymentHistoryPage.xaml
    /// </summary>
    public partial class PaymentHistoryPage : UserControl
    {
        DatabaseEntitiesModel ctx = new DatabaseEntitiesModel();
        CollectionViewSource paymentHistoryVSource;


        public PaymentHistoryPage()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            paymentHistoryVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("paymentHistoryViewSource")));
            paymentHistoryVSource.Source = ctx.PaymentHistories.Local;
            ctx.PaymentHistories.Load();

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
         
            ctx.PaymentHistories.Load();
        }

        private void btnDeletePayment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PaymentHistory payment = null;
                payment = (PaymentHistory)paymentHistoryDataGrid.SelectedItem;
                ctx.PaymentHistories.Remove(payment);
                ctx.SaveChanges();
                paymentHistoryVSource.View.Refresh();
            }
            catch (Exception)
            {

                MessageBox.Show("Please select the payment you want to delete!");
            }
        }
    }
}
