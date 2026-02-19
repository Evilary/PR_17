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
using PrintManagementSystem_Чернышков.Classes;

namespace PrintManagementSystem_Чернышков
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<TypeOperation> typeOperationList = TypeOperation.AllTypeOperation();
        public List<Format> formatsList = Format.AllFormats();
        public MainWindow()
        {
            InitializeComponent();

            LoadData();
        }

        void LoadData()
        {
            foreach (TypeOperation items in typeOperationList)
                typeOperation.Items.Add(items.name);
            foreach (Format item in formatsList)
                formats.Items.Add(item.format);
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            foreach (char c in e.Text)
            {
                if (!char.IsDigit(c))
                {
                    e.Handled = true;
                    return;
                }
            }
        }

        public void CostCalculations()
        {
            float price = 0;
            if (typeOperation.SelectedIndex != -1)
            {
                if (typeOperation.SelectedItem as String == "Сканирование") price = 10;
                else if (typeOperation.SelectedItem as String == "Печать" || typeOperation.SelectedItem as String == "Копия")
                {
                    if (formats.SelectedItem as String == "A4")
                    {
                        if (TwoSides.IsChecked == false)
                        {
                            if (Colors.IsChecked == false)
                            {
                                if (textBoxCount.Text.Length > 0 && int.Parse(textBoxCount.Text) < 30)
                                    price = 4;
                                else price = 3;
                            }
                            else
                                price = 20;
                        }
                        else
                        {
                            if (Colors.IsChecked == false)
                            {
                                if (textBoxCount.Text.Length > 0 && int.Parse(textBoxCount.Text) < 30)
                                    price = 6;
                                else
                                    price = 4;
                            }
                            else
                                price = 35;
                        }

                    }
                    else if (formats.SelectedItem as string == "A3")
                    {

                        if (TwoSides.IsChecked == false)
                        {
                            if (textBoxCount.Text.Length > 0 && int.Parse(textBoxCount.Text) < 30)
                                price = 8;
                            else
                                price = 6;
                        }
                        else
                        {

                            if (textBoxCount.Text.Length > 0 && int.Parse(textBoxCount.Text) < 30)
                                price = 12;
                            else
                                price = 10;
                        }
                    }
                    else if (formats.SelectedItem as string == "A2")
                    {
                        if (Colors.IsChecked == false)
                        {
                            if (LotOfColor.IsChecked == false)
                            {
                                price = 35;
                            }
                            else
                            {
                                price = 50;
                            }
                        }
                        else
                        {

                            if (LotOfColor.IsChecked == false)
                            {
                                price = 120;
                            }
                            else
                            {
                                price = 170;
                            }
                        }
                    }
                    else if (formats.SelectedItem as string == "A1")
                    {
                        if (Colors.IsChecked == false)
                        {
                            if (LotOfColor.IsChecked == false)
                                price = 75;
                            else
                                price = 120;
                        }
                        else
                        {

                            if (LotOfColor.IsChecked == false)
                                price = 170;
                            else
                                price = 250;
                        }
                    }
                    else if (typeOperation.SelectedItem as String == "Ризограф")
                    {
                        if (TwoSides.IsChecked == false)
                        {
                            if (textBoxCount.Text.Length > 0 && int.Parse(textBoxCount.Text) < 100)
                                price = 1.40f;
                            else if (textBoxCount.Text.Length > 0 &&
                            int.Parse(textBoxCount.Text) < 200 && textBoxCount.Text.Length > 0 &&
                            int.Parse(textBoxCount.Text) >= 100)
                                price = 1.10f;
                            else price = 1;
                        }
                        else
                        {

                            if (textBoxCount.Text.Length > 0 && int.Parse(textBoxCount.Text) < 100)
                                price = 1.80f;
                            else if (textBoxCount.Text.Length > 0 &&
                            int.Parse(textBoxCount.Text) < 200 &&
                            textBoxCount.Text.Length > 0 &&
                            int.Parse(textBoxCount.Text) >= 100)
                                price = 1.40f;
                            else price = 1.10f;
                        }
                    }
                }
                if (textBoxCount.Text.Length > 0)
                {
                    textBoxPrice.Text = (float.Parse(textBoxCount.Text) * price).ToString();
                }
            }
        }

        private void SelectedType(object sender, SelectionChangedEventArgs e)
        {
            if (typeOperation.SelectedIndex != -1)
            {
                if (typeOperation.SelectedItem as String == "Сканирование")
                {
                    formats.SelectedIndex = -1;
                    TwoSides.IsChecked = false;
                    Colors.IsChecked = false;
                    LotOfColor.IsChecked = false;

                    formats.IsEnabled = false;
                    TwoSides.IsEnabled = false;
                    Colors.IsEnabled = false;
                    LotOfColor.IsEnabled = false;
                }
                else if (typeOperation.SelectedItem as String == "Печать" || typeOperation.SelectedItem as String == "Копия")
                {
                    formats.IsEnabled = true;
                    TwoSides.IsEnabled = true;
                    Colors.IsEnabled = true;
                }

                if (formats.SelectedItem as String == "A4")
                {
                    TwoSides.IsEnabled = true;
                    Colors.IsEnabled = true;
                    LotOfColor.IsEnabled = false;
                }
                else if (formats.SelectedItem as String == "A3")
                {
                    TwoSides.IsEnabled = true;
                    Colors.IsEnabled = true;
                    LotOfColor.IsEnabled = false;
                }
                else if (formats.SelectedItem as String == "A2" || formats.SelectedItem as String == "A1")
                {
                    TwoSides.IsEnabled = false;
                    Colors.IsEnabled = true;
                    LotOfColor.IsEnabled = true;
                }
                else if (typeOperation.SelectedItem as String == "Размер") // Размер
                {
                    formats.SelectedIndex = 0;
                    formats.IsEnabled = false;
                    Colors.IsEnabled = false;
                    LotOfColor.IsEnabled = false;
                }
            }

            if (textBoxCount.Text.Length == 0)
                textBoxCount.Text = "1";

            CostCalculations();
        }
        

        private void SelectedFormat(object sender, SelectionChangedEventArgs e)
        {
            if (formats.SelectedItem as String == "A4") 
            {
                TwoSides.IsEnabled = true; 
                Colors.IsEnabled = true; 
                LotOfColor.IsEnabled = false; 
            }
            else if (formats.SelectedItem as String == "A3") 
            {
                TwoSides.IsEnabled = true; 
                Colors.IsEnabled = false; 
                LotOfColor.IsEnabled = false; 
            }
            else
            {
                TwoSides.IsEnabled = false; 
                Colors.IsEnabled = true; 
                LotOfColor.IsEnabled = true; 
            }

            if (textBoxCount.Text.Length == 0) 
                textBoxCount.Text = "1"; 

            CostCalculations(); 
        }
    }
}
