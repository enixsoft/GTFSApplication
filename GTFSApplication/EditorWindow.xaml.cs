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
using System.Text.RegularExpressions;

namespace GTFSApplication
{

    public partial class EditorWindow : Window
    {
        public EditorWindow()
        {
            //InitializeComponent();
            //W/indowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            //AgencyLoader();
            //dataGridLoader();
            // CalendarLoader();


        }

        public EditorWindow(int choice, EditorClass editorclassfromMain)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            editorclass = editorclassfromMain;
            dataGridLoader();
            //WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            //dataGridLoader();

            switch (choice)
            {



                case 0:
                    {


                        datagridsAll[0].Visibility = Visibility.Visible;
                        comboBox.SelectedIndex = 0;
                        ActiveEntity = 0;

                    }
                    break;
                case 1:
                    {

                        datagridsAll[1].Visibility = Visibility.Visible;
                        CalendarPagePanel.Visibility = Visibility.Visible;
                        comboBox.SelectedIndex = 1;
                        ActiveEntity = 1;
                    }
                    break;

                case 2:
                    {

                        datagridsAll[2].Visibility = Visibility.Visible;
                        CalendarDatesPagePanel.Visibility = Visibility.Visible;
                        comboBox.SelectedIndex = 2;
                        ActiveEntity = 2;

                    }
                    break;
                case 3:
                    {

                        datagridsAll[3].Visibility = Visibility.Visible;
                        RoutesPagePanel.Visibility = Visibility.Visible;
                        comboBox.SelectedIndex = 3;
                        ActiveEntity = 3;

                    }
                    break;
                case 4:
                    {

                        datagridsAll[4].Visibility = Visibility.Visible;
                        TripsPagePanel.Visibility = Visibility.Visible;
                        comboBox.SelectedIndex = 4;
                        ActiveEntity = 4;

                    }
                    break;
                case 5:
                    {

                        datagridsAll[5].Visibility = Visibility.Visible;
                        comboBox.SelectedIndex = 5;
                        StopPagePanel.Visibility = Visibility.Visible;
                        ActiveEntity = 5;
                    }
                    break;

                case 6:
                    {

                        datagridsAll[6].Visibility = Visibility.Visible;
                        comboBox.SelectedIndex = 6;
                        StopTimesPagePanel.Visibility = Visibility.Visible;
                        ActiveEntity = 6;

                    }
                    break;



            }

        }

        EditorClass editorclass;
        int ActiveEntity;

        String[] entityList =
        {
            "Agency",
            "Calendar",
            "Calendar_dates",
            "Routes",
            "Trips",
            "Stops",
            "Stop_times"

        };

        //LISTS FOR DATAGRIDS



        List<DataGrid> datagridsAll = new List<DataGrid>();

        int CalendarPage = 0;
        int CalendarMaxPage;
        int CalendarRowsPerPage = 10;



        int CalendarDatesPage = 0;
        int CalendarDatesMaxPage;
        int CalendarDatesRowsPerPage = 10;


        int RoutesPage = 0;
        int RoutesMaxPage;
        int RoutesRowsPerPage = 10;


        int TripsPage = 0;
        int TripsMaxPage;
        int TripsRowsPerPage = 10;


       


        int StopsPage = 0;
        int StopsMaxPage;
        int StopsRowsPerPage = 10;



        int StopTimesPage = 0;
        int StopTimesMaxPage;
        int StopTimesRowsPerPage = 10;


    
        private void dataGridLoader()
        {

           

            datagridsAll.Add(dataGridAgency);
            datagridsAll[0].ItemsSource = editorclass.agencyItems;


            datagridsAll.Add(dataGridCalendar);
            
            CalendarPages(CalendarPage);

            datagridsAll.Add(dataGridCalendarDates);
            CalendarDatesServiceIDFromCalendarRefresh();
            CalendarDatesPages(CalendarDatesPage);
           
            datagridsAll.Add(dataGridRoutes);
           
            //datagridsAll[3].ItemsSource = editorclass.routesItems;
            RoutesPages(RoutesPage);

            datagridsAll.Add(dataGridTrips);
            TripsPages(TripsPage);


            datagridsAll.Add(dataGridStops);
            StopsPages(StopsPage);
            
            datagridsAll.Add(dataGridStopTimes);
            StopTimesPages(StopTimesPage);


        }




        // SAVING DATA FROM DATAGRID




        // VARIABLES FOR DATAGRID 







        private void SaveToFile(object sender, RoutedEventArgs e)
        {
            switch (ActiveEntity)
            {

                case 0:
                    {
                        editorclass.SaveToFile(0);

                    }
                    break;

                case 1:
                    {
                        editorclass.SaveToFile(1);
                        CalendarDatesServiceIDFromCalendarRefresh();
                        CalendarDatesLoadPage(CalendarDatesPage);

                    }
                    break;

                case 2:
                    {
                        editorclass.SaveToFile(2);
                        CalendarDatesServiceIDFromCalendarRefresh();
                        CalendarDatesLoadPage(CalendarDatesPage);

                    }
                    break;
                case 3:
                    {
                        editorclass.SaveToFile(3);

                    }
                    break;
                case 4:
                    {
                        editorclass.SaveToFile(4);

                    }
                    break;
                case 5:
                    {
                        editorclass.SaveToFile(5);

                    }
                    break;
                case 6:
                    {
                        editorclass.SaveToFile(6);

                    }
                    break;




            }
        }


        private void EditorClosed(object sender, EventArgs e)
        {
            Application.Current.MainWindow.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Style styleYes = new Style();

            styleYes = (Style)Application.Current.Resources["YesButtonImageStyle"];

            Style styleNo = new Style();

            styleNo = (Style)Application.Current.Resources["NoButtonImageStyle"];

            Button choice = ((Button)sender);
            Style style = choice.Style;

            if (style == styleYes)
            {
                choice.Style = styleNo;
            }
            else if (style == styleNo)
            {
                choice.Style = styleYes;
            }
            else
            {
                choice.Style = styleYes;
            }


        }

        private void datagridCalendar_DatePicker_Enter(object sender, KeyEventArgs e)
        {
            var uie = e.OriginalSource as UIElement;
            if (e.Key == Key.Enter)
            {
                //dataGridCalendar.CancelEdit();
                e.Handled = true;
                dataGridCalendar.CommitEdit();
            }
        }

        private void datagridCalendarDates_DatePicker_Enter(object sender, KeyEventArgs e)
        {
            var uie = e.OriginalSource as UIElement;
            if (e.Key == Key.Enter)
            {
                //dataGridCalendar.CancelEdit();
                e.Handled = true;
                dataGridCalendarDates.CommitEdit();
            }
        }

        private void RouteButton_Click(object sender, RoutedEventArgs e)
        {
            Style qStyle = (Style)Application.Current.Resources["UnknownRouteImageStyle"];

            Style Route0Style = (Style)Application.Current.Resources["Route0ImageStyle"];
            Style Route1Style = (Style)Application.Current.Resources["Route1ImageStyle"];
            Style Route2Style = (Style)Application.Current.Resources["Route2ImageStyle"];
            Style Route3Style = (Style)Application.Current.Resources["Route3ImageStyle"];
            Style Route4Style = (Style)Application.Current.Resources["Route4ImageStyle"];
            Style Route5Style = (Style)Application.Current.Resources["Route5ImageStyle"];
            Style Route6Style = (Style)Application.Current.Resources["Route6ImageStyle"];
            Style Route7Style = (Style)Application.Current.Resources["Route7ImageStyle"];
            Style Route11Style = (Style)Application.Current.Resources["Route11ImageStyle"];


            Button choice = ((Button)sender);
            Style style = choice.Style;


            if (style == Route0Style)
            {
                choice.Style = Route1Style;
            }

            else if (style == Route1Style)
            {
                choice.Style = Route2Style;
            }
            else if (style == Route2Style)
            {
                choice.Style = Route3Style;
            }
            else if (style == Route3Style)
            {
                choice.Style = Route4Style;
            }
            else if (style == Route4Style)
            {
                choice.Style = Route5Style;
            }
            else if (style == Route5Style)
            {
                choice.Style = Route6Style;
            }
            else if (style == Route6Style)
            {
                choice.Style = Route7Style;
            }
            else if (style == Route7Style)
            {
                choice.Style = Route11Style;
            }
            else if (style == Route11Style)
            {
                choice.Style = Route0Style;
            }
            else
            {
                choice.Style = Route0Style;
            }





        }

        bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;

            }

            return true;
        }

        bool IsSpace(string str)
        {
            foreach (char c in str)
            {
                if (c == ' ')
                    return true;


            }
            return false;

        }



        private void NumberValidationTextBox2(object sender, TextCompositionEventArgs e)
        {
            bool result = true;
            if (IsDigitsOnly(e.Text) == true)
            {
                result = false;
            }
            if (IsSpace(e.Text) == true)
            {
                result = true;
            }

            //e.Handled = !IsDigitsOnly(e.Text);
            e.Handled = result;

        }

        private void SpaceBar(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }

            base.OnKeyDown(e);
        }


        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)

        {

            e.Handled = !Regex.IsMatch(e.Text, @"[0-9]"); // allow only numbers

        }

        private void SpaceBarTextBox(object sender, KeyEventArgs e)

        {

            e.Handled = e.Key == Key.Space; // doesn't allow space in textBox

        }

        private void StopTimesHoursButton(object sender, RoutedEventArgs e)
        {
            Button choice = ((Button)sender);
            StackPanel parent = (StackPanel)choice.Parent;
            StackPanel parent2 = (StackPanel)parent.Parent;
            TextBox text = (TextBox)parent2.Children[0];
            int number = Int32.Parse(text.Text);

            if (choice.Name == "HoursPlus")
            {


                if (number < 36)  //kolko je dobre povolit maximalne hodiny?
                {
                    number++;
                    text.Text = Convert.ToString(number);
                }
                else
                {
                    number = 0;
                    text.Text = Convert.ToString(number);
                }
            }
            else
            {

                if (number != 0)
                {
                    number--;
                    text.Text = Convert.ToString(number);
                }
                else
                {
                    number = 0;
                    text.Text = Convert.ToString(number);
                }
            }

        }

        private void StopTimesMinutesButton(object sender, RoutedEventArgs e)
        {
            Button choice = ((Button)sender);
            StackPanel parent = (StackPanel)choice.Parent;
            StackPanel parent2 = (StackPanel)parent.Parent;
            TextBox text = (TextBox)parent2.Children[4];
            int number = Int32.Parse(text.Text);
            string ZeroAdd = "0";

            if (choice.Name == "MinutesPlus")
            {


                if (number < 59)
                {
                    number++;
                    if (number == 0 | number < 10)
                    {
                        ZeroAdd = ZeroAdd + Convert.ToString(number);
                        text.Text = ZeroAdd;
                    }
                    else
                    {
                        text.Text = Convert.ToString(number);
                    }

                }
                else
                {
                    number = 0;
                    ZeroAdd = ZeroAdd + Convert.ToString(number);
                    text.Text = ZeroAdd;
                }
            }
            else
            {

                if (number != 0)
                {
                    number--;
                    if (number == 0 | number < 10)
                    {
                        ZeroAdd = ZeroAdd + Convert.ToString(number);
                        text.Text = ZeroAdd;
                    }
                    else
                    {
                        text.Text = Convert.ToString(number);
                    }
                }
                else
                {
                    number = 0;
                    ZeroAdd = ZeroAdd + Convert.ToString(number);
                    text.Text = Convert.ToString(ZeroAdd);
                }
            }

        }

        private void StopTimesSecondsButton(object sender, RoutedEventArgs e)
        {
            Button choice = ((Button)sender);
            StackPanel parent = (StackPanel)choice.Parent;
            StackPanel parent2 = (StackPanel)parent.Parent;
            TextBox text = (TextBox)parent2.Children[8];
            int number = Int32.Parse(text.Text);
            string ZeroAdd = "0";

            if (choice.Name == "SecondsPlus")
            {


                if (number < 59)
                {
                    number++;
                    if (number == 0 | number < 10)
                    {
                        ZeroAdd = ZeroAdd + Convert.ToString(number);
                        text.Text = ZeroAdd;
                    }
                    else
                    {
                        text.Text = Convert.ToString(number);
                    }

                }
                else
                {
                    number = 0;
                    ZeroAdd = ZeroAdd + Convert.ToString(number);
                    text.Text = ZeroAdd;
                }
            }
            else
            {

                if (number != 0)
                {
                    number--;
                    if (number == 0 | number < 10)
                    {
                        ZeroAdd = ZeroAdd + Convert.ToString(number);
                        text.Text = ZeroAdd;
                    }
                    else
                    {
                        text.Text = Convert.ToString(number);
                    }
                }
                else
                {
                    number = 0;
                    ZeroAdd = ZeroAdd + Convert.ToString(number);
                    text.Text = Convert.ToString(ZeroAdd);
                }
            }

        }

        private void ChangeEntity(object sender, RoutedEventArgs e)
        {
            int choice;
            Button buttonType = (Button)sender;
            if (buttonType.Name == "Previous")
            {

                if (ActiveEntity != 0)
                {
                    
                    choice = ActiveEntity - 1;
                    comboBox.SelectedIndex = choice;
                    EntityChanger(choice);
                }


            }

            else
            {
                if (ActiveEntity != 6)
                {


                    choice = ActiveEntity + 1;
                    comboBox.SelectedIndex = choice;
                    EntityChanger(choice);
                }
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // ... Get the ComboBox.
            var comboBox = sender as ComboBox;

            // ... Set SelectedItem as Window Title.
            int choice = comboBox.SelectedIndex;
            EntityChanger(choice);


        }
        private void EntityChanger(int choice)
        {
            if (ActiveEntity != choice)
            {
                switch (choice)
                {
                    case 0:
                        {
                            datagridsAll[ActiveEntity].Visibility = Visibility.Hidden;
                            dataGridAgency.Visibility = Visibility.Visible;
                            StopTimesPagePanel.Visibility = Visibility.Hidden;
                            StopPagePanel.Visibility = Visibility.Hidden;
                            TripsPagePanel.Visibility = Visibility.Hidden;
                            RoutesPagePanel.Visibility = Visibility.Hidden;
                            CalendarDatesPagePanel.Visibility = Visibility.Hidden;
                            CalendarPagePanel.Visibility = Visibility.Hidden;
                            ActiveEntity = 0;
                        }
                        break;

                    case 1:
                        {
                            datagridsAll[ActiveEntity].Visibility = Visibility.Hidden;
                            dataGridCalendar.Visibility = Visibility.Visible;
                            StopTimesPagePanel.Visibility = Visibility.Hidden;
                            StopPagePanel.Visibility = Visibility.Hidden;
                            TripsPagePanel.Visibility = Visibility.Hidden;
                            RoutesPagePanel.Visibility = Visibility.Hidden;
                            CalendarDatesPagePanel.Visibility = Visibility.Hidden;
                            CalendarPagePanel.Visibility = Visibility.Visible;
                            ActiveEntity = 1;
                        }
                        break;

                    case 2:
                        {
                            datagridsAll[ActiveEntity].Visibility = Visibility.Hidden;
                            dataGridCalendarDates.Visibility = Visibility.Visible;
                            StopTimesPagePanel.Visibility = Visibility.Hidden;
                            StopPagePanel.Visibility = Visibility.Hidden;
                            TripsPagePanel.Visibility = Visibility.Hidden;
                            RoutesPagePanel.Visibility = Visibility.Hidden;
                            CalendarDatesPagePanel.Visibility = Visibility.Visible;
                            CalendarPagePanel.Visibility = Visibility.Hidden;
                            ActiveEntity = 2;
                        }
                        break;

                    case 3:
                        {
                            datagridsAll[ActiveEntity].Visibility = Visibility.Hidden;
                            dataGridRoutes.Visibility = Visibility.Visible;
                            StopTimesPagePanel.Visibility = Visibility.Hidden;
                            StopPagePanel.Visibility = Visibility.Hidden;
                            TripsPagePanel.Visibility = Visibility.Hidden;
                            RoutesPagePanel.Visibility = Visibility.Visible;
                            CalendarDatesPagePanel.Visibility = Visibility.Hidden;
                            CalendarPagePanel.Visibility = Visibility.Hidden;
                            ActiveEntity = 3;
                        }
                        break;

                    case 4:
                        {
                            datagridsAll[ActiveEntity].Visibility = Visibility.Hidden;
                            dataGridTrips.Visibility = Visibility.Visible;
                            StopTimesPagePanel.Visibility = Visibility.Hidden;
                            StopPagePanel.Visibility = Visibility.Hidden;
                            TripsPagePanel.Visibility = Visibility.Visible;
                            RoutesPagePanel.Visibility = Visibility.Hidden;
                            CalendarDatesPagePanel.Visibility = Visibility.Hidden;
                            CalendarPagePanel.Visibility = Visibility.Hidden;
                            ActiveEntity = 4;
                        }
                        break;

                    case 5:
                        {
                            datagridsAll[ActiveEntity].Visibility = Visibility.Hidden;
                            dataGridStops.Visibility = Visibility.Visible;
                            StopTimesPagePanel.Visibility = Visibility.Hidden;
                            StopPagePanel.Visibility = Visibility.Visible;
                            TripsPagePanel.Visibility = Visibility.Hidden;
                            RoutesPagePanel.Visibility = Visibility.Hidden;
                            CalendarDatesPagePanel.Visibility = Visibility.Hidden;
                            CalendarPagePanel.Visibility = Visibility.Hidden;
                            ActiveEntity = 5;
                        }
                        break;

                    case 6:
                        {
                            datagridsAll[ActiveEntity].Visibility = Visibility.Hidden;
                            dataGridStopTimes.Visibility = Visibility.Visible;
                            StopTimesPagePanel.Visibility = Visibility.Visible;
                            StopPagePanel.Visibility = Visibility.Hidden;
                            TripsPagePanel.Visibility = Visibility.Hidden;
                            RoutesPagePanel.Visibility = Visibility.Hidden;
                            CalendarDatesPagePanel.Visibility = Visibility.Hidden;
                            CalendarPagePanel.Visibility = Visibility.Hidden;
                            ActiveEntity = 6;
                        }
                        break;


                }
            }




        }

        private void CalendarDeleteRow(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            DataGridRow dd = (DataGridRow)btn.Tag;
            var RowIndex = dd.GetIndex();

            int row = RowIndex + (CalendarPage * CalendarRowsPerPage);
            editorclass.DeleteRow(row, 0);

            if(RowIndex==0)
            {
                if (CalendarPage==0)
                {
                CalendarPages(CalendarPage);
                }
                else
                {
                CalendarPages(CalendarPage - 1);
                }
                
            }
            else
            {
                CalendarPages(CalendarPage);
            }

      
        }

        private void CalendarDatesDeleteRow(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            DataGridRow dd = (DataGridRow)btn.Tag;
            var RowIndex = dd.GetIndex();

            int row = RowIndex + (CalendarDatesPage * CalendarDatesRowsPerPage);
            editorclass.DeleteRow(row, 1);

            if (RowIndex == 0)
            {
                if (CalendarDatesPage == 0)
                {
                    CalendarDatesPages(CalendarDatesPage);
                }
                else
                {
                    CalendarDatesPages(CalendarDatesPage - 1);
                }

            }
            else
            {
                CalendarDatesPages(CalendarDatesPage);
            }
        }

        private void RoutesDeleteRow(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            DataGridRow dd = (DataGridRow)btn.Tag;
            var RowIndex = dd.GetIndex();

            int row = RowIndex + (RoutesPage * RoutesRowsPerPage);
            editorclass.DeleteRow(row, 2);

            if (RowIndex == 0)
            {
                if (RoutesPage == 0)
                {
                    RoutesPages(RoutesPage);
                }
                else
                {
                    RoutesPages(RoutesPage - 1);
                }

            }
            else
            {
                RoutesPages(RoutesPage);
            }
        }

        private void TripsDeleteRow(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            DataGridRow dd = (DataGridRow)btn.Tag;
            var RowIndex = dd.GetIndex();

            int row = RowIndex + (TripsPage * TripsRowsPerPage);
            editorclass.DeleteRow(row, 3);

            if (RowIndex == 0)
            {
                if (TripsPage == 0)
                {
                    TripsPages(TripsPage);
                }
                else
                {
                    TripsPages(TripsPage - 1);
                }

            }
            else
            {
                TripsPages(TripsPage);
            }
        }

        private void StopsDeleteRow(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            DataGridRow dd = (DataGridRow)btn.Tag;
            var RowIndex = dd.GetIndex();

            int row = RowIndex + (StopsPage * StopsRowsPerPage);
            editorclass.DeleteRow(row, 4);

            if (RowIndex == 0)
            {
                if (StopsPage == 0)
                {
                    StopsPages(StopsPage);
                }
                else
                {
                    StopsPages(StopsPage - 1);
                }

            }
            else
            {
                StopsPages(StopsPage);
            }
        }

        private void StopTimesDeleteRow(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            DataGridRow dd = (DataGridRow)btn.Tag;
            var RowIndex = dd.GetIndex();

            int row = RowIndex+(StopTimesPage*StopTimesRowsPerPage);

            editorclass.DeleteRow(row, 5);

            if (RowIndex == 0)
            {
                if (StopTimesPage == 0)
                {
                    StopTimesPages(StopTimesPage);
                }
                else
                {
                    StopTimesPages(StopTimesPage - 1);
                }

            }
            else
            {
                StopTimesPages(StopTimesPage);
            }
        }

        private void CalendarPages(int currentpage)
        {
            int totalcount = editorclass.calendarItems.Count;
            CalendarMaxPage = (totalcount - 1) / CalendarRowsPerPage;


            CalendarLoadPage(currentpage);

            CalendarCurrentPage.Text = (CalendarPage + 1).ToString();
            CalendarMaxPages.Text = (CalendarMaxPage + 1).ToString();

        }

        private void CalendarLoadPage(int page)
        {


            int startLoad = page * CalendarRowsPerPage;
            int endLoad;
            if (page == CalendarMaxPage)
            {
                endLoad = editorclass.calendarItems.Count - (page * CalendarRowsPerPage);
            }
            else
            {
                endLoad = CalendarRowsPerPage;
            }



            dataGridCalendar.ItemsSource = editorclass.calendarItems.GetRange(startLoad, endLoad);


            CalendarPage = page;
            CalendarCurrentPage.Text = (CalendarPage + 1).ToString();
            CalendarMaxPages.Text = (CalendarMaxPage + 1).ToString();

            dataGridCalendar.Items.Refresh();



        }





        private void CalendarDatesPages(int currentpage)
        {
            int totalcount = editorclass.calendarDatesItems.Count;
            CalendarDatesMaxPage = (totalcount - 1) / CalendarDatesRowsPerPage;


            CalendarDatesLoadPage(currentpage);

            CalendarDatesCurrentPage.Text = (CalendarDatesPage + 1).ToString();
            CalendarDatesMaxPages.Text = (CalendarDatesMaxPage + 1).ToString();

        }

        private void CalendarDatesLoadPage(int page)
        {


            int startLoad = page * CalendarDatesRowsPerPage;
            int endLoad;
            if (page == CalendarDatesMaxPage)
            {
                endLoad = editorclass.calendarDatesItems.Count - (page * CalendarDatesRowsPerPage);
            }
            else
            {
                endLoad = CalendarDatesRowsPerPage;
            }



            dataGridCalendarDates.ItemsSource = editorclass.calendarDatesItems.GetRange(startLoad, endLoad);


            CalendarDatesPage = page;
            CalendarDatesCurrentPage.Text = (CalendarDatesPage + 1).ToString();
            CalendarDatesMaxPages.Text = (CalendarDatesMaxPage + 1).ToString();

            dataGridCalendarDates.Items.Refresh();



        }



        private void RoutesPages(int currentpage)
        {
            int totalcount = editorclass.routesItems.Count;
            RoutesMaxPage = (totalcount - 1) / RoutesRowsPerPage;


            RoutesLoadPage(currentpage);

            RoutesCurrentPage.Text = (RoutesPage + 1).ToString();
            RoutesMaxPages.Text = (RoutesMaxPage + 1).ToString();

        }

        private void RoutesLoadPage(int page)
        {


            int startLoad = page * RoutesRowsPerPage;
            int endLoad;
            if (page == RoutesMaxPage)
            {
                endLoad = editorclass.routesItems.Count - (page * RoutesRowsPerPage);
            }
            else
            {
                endLoad = RoutesRowsPerPage;
            }



            dataGridRoutes.ItemsSource = editorclass.routesItems.GetRange(startLoad, endLoad);


            RoutesPage = page;
            RoutesCurrentPage.Text = (RoutesPage + 1).ToString();
            RoutesMaxPages.Text = (RoutesMaxPage + 1).ToString();

            dataGridRoutes.Items.Refresh();



        }


        private void TripsPages(int currentpage)
        {
            int totalcount = editorclass.tripsItems.Count;
            TripsMaxPage = (totalcount - 1) / TripsRowsPerPage;


            TripsLoadPage(currentpage);

            TripsCurrentPage.Text = (TripsPage + 1).ToString();
            TripsMaxPages.Text = (TripsMaxPage + 1).ToString();

        }

        private void TripsLoadPage(int page)
        {


            int startLoad = page * TripsRowsPerPage;
            int endLoad;
            if (page == TripsMaxPage)
            {
                endLoad = editorclass.tripsItems.Count - (page * TripsRowsPerPage);
            }
            else
            {
                endLoad = TripsRowsPerPage;
            }



            dataGridTrips.ItemsSource = editorclass.tripsItems.GetRange(startLoad, endLoad);


            TripsPage = page;
            TripsCurrentPage.Text = (TripsPage + 1).ToString();
            TripsMaxPages.Text = (TripsMaxPage + 1).ToString();

            dataGridTrips.Items.Refresh();



        }





        private void StopsPages(int currentpage)
        {
            int totalcount = editorclass.stopsItems.Count;
            StopsMaxPage = (totalcount - 1) / StopsRowsPerPage;


            StopsLoadPage(currentpage);

            StopCurrentPage.Text = (StopsPage + 1).ToString();
            StopMaxPages.Text = (StopsMaxPage + 1).ToString();

        }

        private void StopsLoadPage(int page)
        {


            int startLoad = page * StopsRowsPerPage;
            int endLoad;
            if (page == StopsMaxPage)
            {
                endLoad = editorclass.stopsItems.Count - (page * StopsRowsPerPage);
            }
            else
            {
                endLoad = StopsRowsPerPage;
            }



            dataGridStops.ItemsSource = editorclass.stopsItems.GetRange(startLoad, endLoad);


            StopsPage = page;
            StopCurrentPage.Text = (StopsPage + 1).ToString();
            StopMaxPages.Text = (StopsMaxPage + 1).ToString();

            dataGridStops.Items.Refresh();

           

        }
        




        private void StopTimesPages(int currentpage)
        {
            int totalcount = editorclass.stopTimesItems.Count;
            StopTimesMaxPage = (totalcount-1) / StopTimesRowsPerPage;

         
            StopTimesLoadPage(currentpage);

            StopTimesCurrentPage.Text = (StopTimesPage+1).ToString();
            StopTimesMaxPages.Text = (StopTimesMaxPage + 1).ToString();

          


        }


        private void StopTimesLoadPage(int page)
        {
            int startLoad = page * StopTimesRowsPerPage;
            int endLoad;
            if(page==StopTimesMaxPage)
            {
                endLoad = editorclass.stopTimesItems.Count - (page*StopTimesRowsPerPage) ;
            }
            else
            {
                endLoad = StopTimesRowsPerPage;
            }

            
                           
            dataGridStopTimes.ItemsSource = editorclass.stopTimesItems.GetRange(startLoad, endLoad);
            

            StopTimesPage = page;
            StopTimesCurrentPage.Text = (StopTimesPage + 1).ToString();
            StopTimesMaxPages.Text = (StopTimesMaxPage + 1).ToString();
            
            dataGridStopTimes.Items.Refresh();
            



        }

       

        private void LoadPage(int choice)
        {
            switch (choice)
            {
                case 1:
                    {
                        CalendarPages(CalendarPage);
                    }
                    break;


                case 2:
                    {
                        CalendarDatesPages(CalendarDatesPage);
                    }
                    break;




                case 3:
                    {
                        RoutesPages(RoutesPage);
                    }
                    break;


                case 4:
                    {
                        TripsPages(TripsPage);
                    }
                    break;


                case 5:
                    {
                        StopsPages(StopsPage);
                    }
                    break;

                case 6:
                    {
                        StopTimesPages(StopTimesPage);
                    }
                   break;
            }
        }


        private void PreviousPage(object sender, RoutedEventArgs e)
        {
            Button btn = ((Button)sender);
            string tag = btn.Tag.ToString();

            int choice = Int32.Parse(tag);

            switch (choice)
            {
                case 1:
                    if (CalendarPage != 0)

                    {
                        CalendarPage--;
                        CalendarLoadPage(CalendarPage);
                    }
                    break;


                case 2:
                    if (CalendarDatesPage != 0)

                    {
                        CalendarDatesPage--;
                        CalendarDatesLoadPage(CalendarDatesPage);
                    }
                    break;



                case 3:
                    if (RoutesPage != 0)

                    {
                        RoutesPage--;
                        RoutesLoadPage(RoutesPage);
                    }
                    break;
                    

                case 4:
                    if (TripsPage != 0)

                    {
                        TripsPage--;
                        TripsLoadPage(TripsPage);
                    }
                    break;

                case 5:
                    if (StopsPage != 0)

                    {
                        StopsPage--;
                        StopsLoadPage(StopsPage);
                    }
                    break;


                case 6:
                    if (StopTimesPage != 0)
                    {
                        StopTimesPage--;
                        StopTimesLoadPage(StopTimesPage);
                    }
                    break;

            }
        }

        private void NextPage(object sender, RoutedEventArgs e)
        {
            Button btn = ((Button)sender);
            string tag = btn.Tag.ToString();

            int choice = Int32.Parse(tag);

            switch (choice)
       {


                case 1:
                    if (CalendarPage != CalendarMaxPage)

                    {
                        CalendarPage++;
                        CalendarLoadPage(CalendarPage);
                    }
                    break;


                case 2:
                    if (CalendarDatesPage != CalendarDatesMaxPage)

                    {
                        CalendarDatesPage++;
                        CalendarDatesLoadPage(CalendarDatesPage);                        
                    }
                    break;




                case 3:
                    if (RoutesPage != RoutesMaxPage)

                    {
                        RoutesPage++;
                        RoutesLoadPage(RoutesPage);
                    }
                    break;



                case 4:
                    if (TripsPage != TripsMaxPage)

                    {
                        TripsPage++;
                       TripsLoadPage(TripsPage);
                    }
                    break;




                case 5:
                    if (StopsPage != StopsMaxPage)

                    {
                        StopsPage++;
                        StopsLoadPage(StopsPage);
                    }
                    break;

                 case 6:
                     if (StopTimesPage != StopTimesMaxPage)

                     {
                           StopTimesPage++;
                        StopTimesLoadPage(StopTimesPage);
                     }
                        break;

      }

        }

        private void Refresh(object sender, RoutedEventArgs e)
        {


            editorclass.dataGridRefresher(ActiveEntity);
            LoadPage(ActiveEntity);

            if(ActiveEntity==2)
            {
                CalendarDatesServiceIDFromCalendarRefresh();
            }
            //datagridsAll[ActiveEntity].Items.Refresh();

        }

        private void SaveToFileAll(object sender, RoutedEventArgs e)
        {
         for (int i=0; i<7;i++)
            {
                editorclass.SaveToFile(i);
            }
        }

        private void AddRow(object sender, RoutedEventArgs e)
        {
            switch (ActiveEntity)
            {
                case 1:
                    {
                        editorclass.CalendarAddRow();                       
                        CalendarPages(CalendarPage);
                    }
                    break;

                case 2:
                    {
                        editorclass.CalendarDatesAddRow();
                        CalendarDatesServiceIDFromCalendarRefresh();
                        CalendarDatesPages(CalendarDatesPage);


                    }
                    break;

                case 3:
                    {
                        editorclass.RoutesAddRow();                   
                        RoutesPages(RoutesPage);

                    }
                    break;
                case 4:
                    {
                        editorclass.TripsAddRow();                   
                        TripsPages(TripsPage);

                    }
                    break;
                case 5:
                    {
                        editorclass.StopsAddRow();                     
                        StopsPages(StopsPage);

                    }
                    break;

                case 6:
                    {
                        editorclass.StopTimesAddRow();                        
                        StopTimesPages(StopTimesPage);

                    }
                    break;




            }
        }

        /*private void CalendarDatesServiceIDFromCalendar()
        {
            int count = (editorclass.calendarItems.Count);
            int count2 = (editorclass.calendarDatesItems.Count);
            for (int i = 0; i < count2; i++)
            {

                for (int j = 0; j < count; j++)
                {
                    if (editorclass.calendarDatesItems[i].CalendarDatesServiceIdFromCalendar[0] != editorclass.calendarItems[j].CalendarServiceid)
                    {
                        editorclass.calendarDatesItems[i].CalendarDatesServiceIdFromCalendar.Add(editorclass.calendarItems[j].CalendarServiceid);
                    }
                }
            }

        }
        */

        private void CalendarDatesServiceIDFromCalendarRefresh()
        {
            int count = (editorclass.calendarItems.Count);
            int count2 = (editorclass.calendarDatesItems.Count);

            for (int i = 0; i < count2; i++)
            {
                for (int j = ((editorclass.calendarDatesItems[i].CalendarDatesServiceIdFromCalendar.Count) - 1); j >=1; j--)
                {
                    editorclass.calendarDatesItems[i].CalendarDatesServiceIdFromCalendar.RemoveAt(j);
                }

                editorclass.calendarDatesItems[i].CalendarDatesServiceIdFromCalendar[0] = editorclass.calendarDatesItems[i].CalendarDatesServiceid;
            }
         


                for (int i = 0; i < count2; i++)
            {

                for (int j = 0; j < count; j++)
                {
                    if (editorclass.calendarDatesItems[i].CalendarDatesServiceIdFromCalendar[0] != editorclass.calendarItems[j].CalendarServiceid)
                    {
                        editorclass.calendarDatesItems[i].CalendarDatesServiceIdFromCalendar.Add(editorclass.calendarItems[j].CalendarServiceid);
                    }
                }
            }

         
        }

       



    }
}

