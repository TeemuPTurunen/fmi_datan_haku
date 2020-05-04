using GemBox.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Xml;
using System.Configuration;

namespace fmiDatanHaku
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            path = Path.Combine(path, Constants.FolderName, Constants.FileName);
            if (File.Exists(path))
            {
                textBoxAPIKey.Text = File.ReadAllText(@path);
            }

        }
        /// <summary>
        /// save user api key to appsettings.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Click(object sender, RoutedEventArgs e)
        {
            SaveAPIKey.SaveKey(textBoxAPIKey.Text);            
        }


        private void buttonLuoExcel_Click(object sender, RoutedEventArgs e)
        {
            labelFileCreated.Content = "";

            if (DatePickerStart.SelectedDate >= DatePickerEnd.SelectedDate)
            {
                labelEndDateTooLow.Content = "Loppu pvm pitää olla suurempi kuin alku pvm";
            }
            else if (listBoxPlaces.Items.Count == 0)
            {
                labelNoPlacesError.Content = "Vähintään yksi paikka pitää olla annettu";
            }
            else if (labelTotalDays.Content.ToString() != "" || DatePickerStart.SelectedDate == null || DatePickerEnd.SelectedDate == null)
            {
                labelNoPlacesError.Content = "Tarkista päivämäärät";
            }
            else
            {
                labelEndDateTooLow.Content = "";
                labelNoPlacesError.Content = "";

                labelFileCreated.Content = "Luodaan tiedostoa...";

                string[] places = new string[listBoxPlaces.Items.Count];

                for (int i = 0; i < listBoxPlaces.Items.Count; i++)
                {
                    places[i] = listBoxPlaces.Items.GetItemAt(i).ToString();
                }

                string file = FileDialog.AskFile(places, Convert.ToDateTime(DatePickerStart.SelectedDate), Convert.ToDateTime(DatePickerEnd.SelectedDate));

                

                string[] urls = ParseUrl.GetUrl(textBoxAPIKey.Text, Convert.ToDateTime(DatePickerStart.SelectedDate), Convert.ToDateTime(DatePickerEnd.SelectedDate), places, textBoxTimeStep.Text);

                List<Temperature> measurements = new List<Temperature>();


                //measurements = ReadData.ReadTemperature(measurements, urls);
                ReadData.ReadTemperature(measurements, urls);


                WriteCSV.Write(measurements, file);

                labelFileCreated.Content = "Tiedosto luotu";

            }

        }

        private void buttonAddPlace_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxPlace.Text != "")
            {
                listBoxPlaces.Items.Add(textBoxPlace.Text);
                textBoxPlace.Text = "";
            }
        }

        private void buttonRemove_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxPlaces.SelectedItem != null)
            {
                listBoxPlaces.Items.RemoveAt(listBoxPlaces.SelectedIndex);
            }
        }

        private void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            listBoxPlaces.Items.Clear();
        }

        private void DatePickerEnd_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DatePickerStart.SelectedDate != null)
            {
                if ((DatePickerEnd.SelectedDate.Value - DatePickerStart.SelectedDate.Value).TotalDays > 4100)
                {
                    labelTotalDays.Content = "Haun pitää olla alle 11 vuotta pitkä";
                }
                else
                {
                    labelTotalDays.Content = "";
                }
            }
        }

        private void DatePickerStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DatePickerEnd.SelectedDate != null)
            {
                if ((DatePickerEnd.SelectedDate.Value - DatePickerStart.SelectedDate.Value).TotalDays > 4100)
                {
                    labelTotalDays.Content = "Haun pitää olla alle 11 vuotta pitkä";
                }
                else
                {
                    labelTotalDays.Content = "";
                }
            }
        }
    }
}
