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
using System.Windows.Shapes;

namespace GTFSApplication
{
    /// <summary>
    /// Interaction logic for TripWindow.xaml
    /// </summary>
    public partial class TripWindow : Window
    {
        public TripWindow(List<GTFSApplication.MainWindow.TripResultValues> resultItems,string trip_id)
        {

            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.Show();
            Trip.Text = Trip.Text + trip_id;
            dataGridResult.ItemsSource = resultItems;
            dataGridResult.Items.Refresh();


        }
    }




}
