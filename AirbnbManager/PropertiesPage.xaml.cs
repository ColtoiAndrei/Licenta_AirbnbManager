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
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class PropertiesPage : UserControl
    {
        DatabaseEntitiesModel ctx = new DatabaseEntitiesModel();
        CollectionViewSource propertyVSource;
        CollectionViewSource cleaningVSource;



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
            ctx.Cleanings.Load();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
