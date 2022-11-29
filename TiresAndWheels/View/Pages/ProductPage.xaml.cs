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
using TiresAndWheels.Model;

namespace TiresAndWheels.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        Core db = new Core();
        public ProductPage()
        {
            InitializeComponent();
            UpdateUI();
        }

        private void UpdateUI()
        {
            List<Product> displayProduct = GetRows().ToList();
            ProductListView.ItemsSource = displayProduct;
        }

        private object GetRows()
        {
            List<Product> arrayProduct = db.context.Product.ToList();
            return arrayProduct;
        }

        private void SearchTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = String.Empty;
        }
    }
}
