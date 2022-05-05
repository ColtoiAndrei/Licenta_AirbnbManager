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
    /// Interaction logic for Page3.xaml
    /// </summary>
    public partial class CleaningPage : UserControl
    {

        DatabaseEntitiesModel ctx = new DatabaseEntitiesModel();
        CollectionViewSource cleaningVSource;
        public CleaningPage()
        {
            DataContext = this;
            InitializeComponent();

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            cleaningVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("cleaningViewSource")));
            cleaningVSource.Source = ctx.Cleanings.Local;
            ctx.Cleanings.Load();

            // Do not load your data at design time.
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//Load your data here and assign the result to the CollectionViewSource.
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }
        }

        private void btnAddCleaning_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cleaning cleaning = null;
                var idExists = ctx.Cleanings.Find(Int32.Parse(companyIdTextBox.Text.Trim()));
                if (idExists != null) MessageBox.Show("Company Id Id already exists!");
                else
                {
                    cleaning = new Cleaning()
                    {
                        CompanyId = Int32.Parse(companyIdTextBox.Text.Trim()),
                        Name = nameTextBox.Text.Trim(),
                        Email = emailTextBox.Text.Trim(),
                        Price = Decimal.Parse(priceTextBox.Text.Trim()),
                        StripeKey = stripeKeyTextBox.Text.Trim()
                    };

                    ctx.Cleanings.Add(cleaning);
                    ctx.SaveChanges();
                    cleaningVSource.View.Refresh();

                }
            }
            catch (Exception)
            {

                MessageBox.Show("Empty fields detected or wrong values!");
            }
        }

        private void btnEditCleaning_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cleaning cleaning = null;
                if (cleaningDataGrid.SelectedItem != null)
                {
                    int idChange = Int32.Parse(companyIdTextBox.Text.Trim());
                    cleaning = (Cleaning)cleaningDataGrid.SelectedItem;
                    if (cleaning.CompanyId == idChange)
                    {
                        cleaning.Name = nameTextBox.Text.Trim();
                        cleaning.Email = emailTextBox.Text.Trim();
                        cleaning.Price = Decimal.Parse(priceTextBox.Text.Trim());
                        cleaning.StripeKey = stripeKeyTextBox.Text.Trim();

                        ctx.SaveChanges();
                        cleaningVSource.View.Refresh();
                    }
                    else
                    {
                        MessageBox.Show("You cannot change the Company Id!");
                        companyIdTextBox.Text = cleaning.CompanyId.ToString();
                    }
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Please select the row you want to edit!");
            }
        }

        private void btnDeleteCleaning_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cleaning cleaning = null;
                cleaning = (Cleaning)cleaningDataGrid.SelectedItem;
                ctx.Cleanings.Remove(cleaning);
                ctx.SaveChanges();

                cleaningVSource.View.Refresh();
                

            }
            catch (Exception)
            {

                MessageBox.Show("Please select the row you want to delete!");
            }
        }
    }
}
