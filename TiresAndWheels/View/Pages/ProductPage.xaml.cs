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
        List<ProductType> productTypes;
        public ProductPage()
        {
            InitializeComponent();
            productTypes = new List<ProductType> {
                new ProductType() {
                    ID = 0,
                    Title = "Все типы"
                }
            };
            productTypes.AddRange(db.context.ProductType.ToList());
            FilterComboBox.ItemsSource = productTypes;
            FilterComboBox.DisplayMemberPath = "Title";
            FilterComboBox.SelectedValuePath = "ID";
            FilterComboBox.SelectedIndex = 0;
            UpdateUI();
        }



        private void UpdateUI()
        {
            ProductListView.ItemsSource = GetRows();

        }



        private List<Product> GetRows()
        {
            int filter;
            List<Product> arrayProduct = db.context.Product.ToList();
            string searchData = SearchTextBox.Text.ToUpper();
            if (!String.IsNullOrEmpty(SearchTextBox.Text))
            {
                 arrayProduct = arrayProduct.Where(x => x.Title.ToUpper().Contains(searchData)).ToList();
               // arrayProduct = arrayProduct.Where(x => LeveshteinDistance(x.Title.ToUpper(), searchData) <= 6).ToList();
            }
            if (FilterComboBox.SelectedIndex!=0)
            {
                filter = Convert.ToInt32(FilterComboBox.SelectedValue);
            }
            else
            {
                filter = 0;
            }
            
            if (FilterComboBox.SelectedItem!=null && FilterComboBox.SelectedIndex == 0)
            {
                arrayProduct = arrayProduct.ToList();
            }
            else
            {
                arrayProduct = arrayProduct.Where(x => x.ProductTypeID == filter).ToList();
            }
            return arrayProduct;
        }

        private int LeveshteinDistance(string firstWord, string secondWord)
        {
            var n = firstWord.Length + 1;
            var m = secondWord.Length + 1;
            var matrixD = new int[n, m];

            const int deletionCost = 1;
            const int insertionCost = 1;

            for (var i = 0; i < n; i++)
            {
                matrixD[i, 0] = i;
            }

            for (var j = 0; j < m; j++)
            {
                matrixD[0, j] = j;
            }

            for (var i = 1; i < n; i++)
            {
                for (var j = 1; j < m; j++)
                {
                    var substitutionCost = firstWord[i - 1] == secondWord[j - 1] ? 0 : 1;

                    matrixD[i, j] = Minimum(matrixD[i - 1, j] + deletionCost,          // удаление
                                            matrixD[i, j - 1] + insertionCost,         // вставка
                                            matrixD[i - 1, j - 1] + substitutionCost); // замена
                }
            }

            return matrixD[n - 1, m - 1];
        }

        static int Minimum(int a, int b, int c) => (a = a < b ? a : b) < c ? a : c;

        private void SearchTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = String.Empty;
        }

        private void SearchTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateUI();
        }

        private void FilterComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateUI();
        }
    }
}
