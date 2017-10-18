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
using System.Data.SQLite;
using System.IO;
using System.Threading;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.RegularExpressions;


namespace GTFSApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public EditorClass editorclass = new EditorClass();

        public MainWindow()
        {


           
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;


        
            AddDatabases();
        
            
            comboBox.ItemsSource = DatabaseListNames;
            comboBox.SelectedIndex = 0;
            ActiveDatabase = 0;                   
        }


        public List<string> DatabaseListNames = new List<string>();
        public List<string> DatabaseListFiles = new List<string>();
        string defaultFilename = "default.db3";
        int ActiveDatabase;


        public List<string> stopList = new List<string>();
        StopTextboxModel searchFromModel = new StopTextboxModel();
        StopTextboxModel searchToModel = new StopTextboxModel();

        public List<ResultValues> resultItems = new List<ResultValues>();
        public List<ResultValues> prevresultItems = new List<ResultValues>();
        public List<ResultValues> reverseresultItems = new List<ResultValues>();

        Thread Searcher;
        bool searching;


        public class StopTextboxModel
        {
            public IEnumerable<string> Stops { get; set; }
            public string SelectedStop { get; set; }
        }

        public class ResultValues
        {
            public string resultDate { get; set; }
            public string resultDepartureTime { get; set; } //ODCHOD ZO
            public string resultArrivalTime { get; set; }   //PRICHOD NA
            public string resultStop { get; set; }
            public string resultNextStop { get; set; }
            public string resultTripNumber { get; set; }
        }

        public class TripResultValues
        {
            public string resultNumber { get; set; }         
            public string resultArrivalTime { get; set; }   //PRICHOD NA
            public string resultStop { get; set; }
         
        }
        public void ManagerClosedRefreshDatabase()
        {
            AddDatabases();
        }   
     

        private void AddDatabases()
        {
            DatabaseListFiles.Clear();
            DatabaseListNames.Clear();

            string[] files = System.IO.Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.db3");
            foreach (string s in files)
            {
                DatabaseListFiles.Add(System.IO.Path.GetFileName(s));
                DatabaseListNames.Add(System.IO.Path.GetFileNameWithoutExtension(s));
            }


            if (DatabaseListFiles.Count==0)
            {
                SQLiteConnection.CreateFile(defaultFilename);
                DatabaseListNames.Add("default");
                DatabaseListFiles.Add(defaultFilename);

            }


            


            comboBox.Items.Refresh();
            
        }

    


        private void AddStopsItems()
        {
            stopList.Clear();
            searchFromModel.Stops = stopList;            
            searchToModel.Stops = stopList;



            string dbConnection = String.Format("Data Source={0}", DatabaseListFiles[ActiveDatabase]);
            SQLiteConnection conn = new SQLiteConnection(dbConnection);

            conn.Open();

            try
            { 
            string sql = @"SELECT DISTINCT stop_name FROM Stops"; 

            SQLiteCommand command = new SQLiteCommand(sql, conn);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                stopList.Add(Convert.ToString(reader["stop_name"]));
                 
            }

            searchFromModel.SelectedStop = "";
            searchFromModel.Stops = stopList;
            searchToModel.SelectedStop = "";
            searchToModel.Stops = stopList;
           
            }
            catch (SQLiteException)
            {

            }
            conn.Close();

        }

        public void ClickMe(object sender, RoutedEventArgs e)
        {
            Button choice = ((Button)sender);
            switch (choice.Name)
            {
                case "buttonAgency":
                    {
                        EditorWindow editor = new EditorWindow(0, editorclass);
                        editor.Show();
                    }
                    break;

                case "buttonCalendar":
                    {
                        Window editor = new EditorWindow(1, editorclass);
                        editor.Show();
                    }
                    break;

                case "buttonCalendarDates":
                    {
                        Window editor = new EditorWindow(2, editorclass);
                        editor.Show();
                    }
                    break;

                case "buttonRoutes":
                    {
                        Window editor = new EditorWindow(3, editorclass);
                        editor.Show();
                    }
                    break;

                case "buttonTrips":
                    {
                        Window editor = new EditorWindow(4, editorclass);
                        editor.Show();
                    }
                    break;

                case "buttonStops":
                    {
                        Window editor = new EditorWindow(5, editorclass);
                        editor.Show();
                    }
                    break;

                case "buttonStopTimes":
                    {
                        Window editor = new EditorWindow(6, editorclass);
                        editor.Show();
                    }
                    break;

                case "buttonDatabaseManager":
                    {
                        Window dbManager = new DatabaseManager(editorclass, DatabaseListFiles, DatabaseListNames);
                        dbManager.Closed += dbManagerClosed;
                        dbManager.Show();
                    }
                    break;








            }
            this.Hide();


            //Window Editor = new Editor();
            //editor.Show();






        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            currentResultPage = 0;
          
            searchError.Visibility= Visibility.Hidden;
            dataGridResult.Visibility = Visibility.Hidden;
            loadingImage.Visibility = Visibility.Visible;
            previousButton.Visibility = Visibility.Hidden;
            nextButton.Visibility = Visibility.Hidden;
            searchError.Text = "Vyhľadávanie nenašlo žiadne výsledky.";

            searchButton.Content = "ZRUŠIŤ";
            searchButton.Background = Brushes.OrangeRed;
            searchButton.Click -= Search_Click;
            searchButton.Click += Cancel_Click;

       


            
            if (!searching)
            {
                searching = true;
                Searcher = new Thread(() => Search(0));
                Searcher.Priority = ThreadPriority.AboveNormal;
                Searcher.Start();
            }
            

        }


        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Searcher.Abort();
            searching = false;
            BrushConverter bc = new BrushConverter();
            loadingImage.Visibility = Visibility.Hidden;
            searchButton.Content = "VYHĽADAŤ";
            searchButton.Background = (Brush)bc.ConvertFrom("#FF9ECAF5");
            searchButton.Click -= Cancel_Click;
            searchButton.Click += Search_Click;

        }

        List<string> stopFromResults = new List<string>();
        List<string> stopToResults = new List<string>();

        List<ResultFromStopTimesValues> tempstopTimesResults = new List<ResultFromStopTimesValues>();
        List<TotalResultFromStopTimesValues> stopTimesResults = new List<TotalResultFromStopTimesValues>();

        string searchTime;
        int triesNumLimit = 30;

        int searchTimeHours;
        int searchTimeMinutes;
        bool TimeError;

        string searchDateFromPicker;
        DateTime DTDateFromPicker;

      
        int currentResultPage;


        private bool CheckDatabase()
        {
            string dbConnection = String.Format("Data Source={0}", DatabaseListFiles[ActiveDatabase]);
            SQLiteConnection conn = new SQLiteConnection(dbConnection);

            conn.Open();
            bool valid = true;

            string[] results = new string[7];
            string[] queries =
            {
                "SELECT count(*) FROM sqlite_master WHERE type='table' AND name='Agency';",
                "SELECT count(*) FROM sqlite_master WHERE type='table' AND name='Calendar';",
                "SELECT count(*) FROM sqlite_master WHERE type='table' AND name='CalendarDates';",
                "SELECT count(*) FROM sqlite_master WHERE type='table' AND name='Stops';",
                "SELECT count(*) FROM sqlite_master WHERE type='table' AND name='StopTimes';",
                "SELECT count(*) FROM sqlite_master WHERE type='table' AND name='Trips';",
                "SELECT count(*) FROM sqlite_master WHERE type='table' AND name='Routes';"

            };


            for (int i = 0; i < 7; i++)
            {
                SQLiteCommand command = new SQLiteCommand(queries[i], conn);
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    results[i]=(Convert.ToString(reader[0]));
                }
            }

            conn.Close();

            for (int i = 0; i < 7; i++)
            {
                if (results[i] == "1")
                {
                    continue;
                }
                else
                {
                    valid = false;
                    break;                    
                    
                }
                
            }

            
          

            if (!valid)
            {
                return false;
            }
            else
            {
                return true;
            }
               

        }

        private void Search(int choice)
        {
                        

                ResultValues lastResult;
                int triesNum = 0;

                if (choice == 0)
                {

                    resultItems.Clear();
                    string searchFromstring = "";
                    string searchTostring = "";

                    this.Dispatcher.Invoke(() => searchFromstring = searchFrom.Text);
                    this.Dispatcher.Invoke(() => searchTostring = searchTo.Text);

                    this.Dispatcher.Invoke(() => loadingImage.Source = new BitmapImage(new Uri(@"/GTFSApplication;component/Resources/sandClock.png", UriKind.RelativeOrAbsolute)));

                    TimeError = false;

                    this.Dispatcher.Invoke(() => searchError.Visibility = Visibility.Hidden);
                    this.Dispatcher.Invoke(() => searchFromError.Text = "Zástavka nebola nájdená.");
                    this.Dispatcher.Invoke(() => searchFromError.Visibility = Visibility.Hidden);
                    this.Dispatcher.Invoke(() => searchToError.Text = "Zástavka nebola nájdená.");
                    this.Dispatcher.Invoke(() => searchToError.Visibility = Visibility.Hidden);
                    this.Dispatcher.Invoke(() => searchError.Visibility = Visibility.Hidden);

                    this.Dispatcher.Invoke(() => searchTimeError.Visibility = Visibility.Hidden);
                    stopFromResults.Clear();
                    CheckIfStopExistsInStops(searchFromstring, stopFromResults);

                    stopToResults.Clear();
                    CheckIfStopExistsInStops(searchTostring, stopToResults);



                    this.Dispatcher.Invoke(() => (searchTime = searchTimeTextBox.Text)); // FUNCTION  NEEDED

                    if (searchTime == "")
                    {
                        searchTime = DateTime.Now.ToString("HH:mm");
                    }

                    if (searchTime.Length == 5)
                    {
                       
                        searchTimeHours = Int32.Parse(searchTime.Substring(0, 2));

                        searchTimeMinutes = Int32.Parse(searchTime.Substring(3, 2));

                    }
                    else
                    {
                        searchTimeHours = Int32.Parse(searchTime.Substring(0, 1));

                        searchTimeMinutes = Int32.Parse(searchTime.Substring(2, 2));

                        
                    }
                }
                else
                {

                    this.Dispatcher.Invoke(() => loadingImage.Source = new BitmapImage(new Uri(@"/GTFSApplication;component/Resources/sandClock.png", UriKind.RelativeOrAbsolute)));


                    lastResult = (ResultValues)dataGridResult.Items[4];

                    searchTime = lastResult.resultDepartureTime.ToString();
                    if (searchTime.Length == 8)
                    {
                       searchTime = searchTime.Substring(0, 5);

                    }
                    else
                    {
                        searchTime = searchTime.Substring(0, 4);
                    }
                    

                  

                    if (searchTime.Length == 5)
                    {                      

                        searchTimeHours = Int32.Parse(searchTime.Substring(0, 2));
                        searchTimeMinutes = Int32.Parse(searchTime.Substring(3, 2));

                    }
                    else
                    {
                        searchTimeHours = Int32.Parse(searchTime.Substring(0, 1));
                        searchTimeMinutes = Int32.Parse(searchTime.Substring(2, 2));

                       
                    }


                 




                }


                if (searchTimeHours > 23)
                {
                    TimeError = true;
                }

                if (searchTimeMinutes > 59)
                {
                    TimeError = true;
                }

                if (TimeError == true)
                {
                    this.Dispatcher.Invoke(() => searchTimeError.Visibility = Visibility.Visible);
                    this.Dispatcher.Invoke(() => loadingImage.Source = new BitmapImage(new Uri(@"/GTFSApplication;component/Resources/errorSearch.png", UriKind.RelativeOrAbsolute)));
                }
                else
                {


                    this.Dispatcher.Invoke(() => searchDateFromPicker = searchDatePicker.Text);
                    //if (searchDateFromPicker.Length==) WORKS LIKE THAT NOW

                    if (searchDateFromPicker == "")
                    {
                        searchDateFromPicker = DateTime.Now.Date.ToString("dd.MM.yyyy");
                        DTDateFromPicker = DateTime.ParseExact(searchDateFromPicker,
                                        "dd.MM.yyyy",
                                         null);
                    }
                    else
                    {
                        DTDateFromPicker = DateTime.ParseExact(searchDateFromPicker,
                                      "dd.MM.yyyy",
                                       null);
                    }



                    int ChangeToPercentWhen = 10 - (searchTimeMinutes % 10);
                    int RemoveMinutesWhen = ChangeToPercentWhen + (((60 - searchTimeMinutes) / 10) - 1) + 6;
                 


                    if (stopFromResults.Count == 1 && stopToResults.Count == 1)
                    {
                        this.Dispatcher.Invoke(() => searchFrom.Text = stopFromResults[0]);
                        this.Dispatcher.Invoke(() => searchTo.Text = stopToResults[0]);

                        do
                        {



                            if (triesNum < ChangeToPercentWhen)
                            {
                                if (searchTimeMinutes < 10)
                                {
                                    searchTime = searchTimeHours.ToString() + ":0" + searchTimeMinutes.ToString() + "%";
                                }
                                else
                                {
                                    searchTime = searchTimeHours.ToString() + ":" + searchTimeMinutes.ToString() + "%";
                                }
                            }
                            else
                            {
                                if (triesNum >= RemoveMinutesWhen)
                                {
                                    searchTime = searchTimeHours.ToString() + ":%";

                                }
                                else
                                {


                                    if (searchTimeMinutes < 10)
                                    {
                                        searchTime = searchTimeHours.ToString() + ":0%";
                                    }

                                    searchTime = searchTimeHours.ToString() + ":" + searchTimeMinutes.ToString().Substring(0, 1) + "%";
                                }

                            }

                            
                            tempstopTimesResults.Clear();
                            stopTimesResults.Clear();

                            GetStopTimes(stopFromResults, stopToResults, tempstopTimesResults, stopTimesResults, searchTime, DTDateFromPicker);

                            if (stopTimesResults.Count != 0)
                            {
                                for (int i = 0; i < stopTimesResults.Count; i++)
                                {

                                    if (stopTimesResults[i].FROMdeparture_time != "")
                                    {

                                       

                                        if (resultItems.Count < ((currentResultPage * 5) + 5))
                                        {
                                            resultItems.Add(new ResultValues()
                                            {
                                                resultDate = searchDateFromPicker,
                                                resultDepartureTime = stopTimesResults[i].FROMdeparture_time,
                                                resultArrivalTime = stopTimesResults[i].TOarrival_time,
                                                resultStop = stopTimesResults[i].FROMstop_name,
                                                resultNextStop = stopTimesResults[i].TOstop_name,
                                                resultTripNumber = stopTimesResults[i].trip_id
                                            });
                                        }
                                        else
                                        {
                                            break;
                                        }

                                    }
                                }


                            }

                            if (triesNum < ChangeToPercentWhen)
                            {
                                if (searchTimeMinutes == 59 & searchTimeHours == 23)
                                {
                                    DTDateFromPicker = DTDateFromPicker.AddDays(1);
                                    searchDateFromPicker = DTDateFromPicker.Date.ToString("dd.MM.yyyy");
                                    searchTimeHours = 0;
                                    searchTimeMinutes = 0;
                                }
                                else
                                {

                                    if (searchTimeMinutes < 59)
                                    {
                                        searchTimeMinutes++;
                                    }
                                    else
                                    {
                                        searchTimeHours++;
                                        searchTimeMinutes = 0;
                                    }
                                }
                            }
                            else

                            {
                                if (triesNum >= RemoveMinutesWhen)
                                {
                                    if (searchTimeHours == 23)
                                    {
                                        DTDateFromPicker = DTDateFromPicker.AddDays(1);
                                        searchDateFromPicker = DTDateFromPicker.Date.ToString("dd.MM.yyyy");
                                        searchTimeHours = 0;
                                    }
                                    else
                                    {
                                        searchTimeHours++;
                                    }

                                }
                                else
                                {

                                    if (searchTimeMinutes == 50 & searchTimeHours == 23)
                                    {
                                        DTDateFromPicker = DTDateFromPicker.AddDays(1);
                                        searchDateFromPicker = DTDateFromPicker.Date.ToString("dd.MM.yyyy");
                                        searchTimeHours = 0;
                                        searchTimeMinutes = 0;
                                    }
                                    else
                                    {

                                        if (searchTimeMinutes < 50)
                                        {
                                            searchTimeMinutes = searchTimeMinutes + 10;
                                        }
                                        else
                                        {
                                            searchTimeHours++;
                                            searchTimeMinutes = 0;
                                        }
                                    }

                                }
                            }



                            triesNum++;



                        } while (resultItems.Count < ((currentResultPage * 5) + 5) & triesNum < triesNumLimit);




                    }
                    else if (stopFromResults.Count == 0 && stopToResults.Count == 1)
                    {
                        this.Dispatcher.Invoke(() => searchFromError.Visibility = Visibility.Visible);
                    }

                    else if (stopFromResults.Count == 1 && stopToResults.Count == 0)
                    {
                        this.Dispatcher.Invoke(() => searchToError.Visibility = Visibility.Visible);
                    }

                    else if (stopFromResults.Count > 1 && stopToResults.Count > 1)
                    {
                        this.Dispatcher.Invoke(() => searchFromError.Text = "Zástavku je potrebné presnejšie uviesť.");
                        this.Dispatcher.Invoke(() => searchFromError.Visibility = Visibility.Visible);

                        this.Dispatcher.Invoke(() => searchToError.Text = "Zástavku je potrebné presnejšie uviesť.");
                        this.Dispatcher.Invoke(() => searchToError.Visibility = Visibility.Visible);

                    }

                    else if (stopFromResults.Count > 1)
                    {

                        this.Dispatcher.Invoke(() => searchFromError.Text = "Zástavku je potrebné presnejšie uviesť.");
                        this.Dispatcher.Invoke(() => searchFromError.Visibility = Visibility.Visible);
                    }
                    else if (stopToResults.Count > 1)
                    {
                        this.Dispatcher.Invoke(() => searchToError.Text = "Zástavku je potrebné presnejšie uviesť.");
                        this.Dispatcher.Invoke(() => searchToError.Visibility = Visibility.Visible);
                    }



                    else if (stopFromResults.Count == 0 && stopToResults.Count == 0)
                    {

                        this.Dispatcher.Invoke(() => searchFromError.Visibility = Visibility.Visible);

                        this.Dispatcher.Invoke(() => searchToError.Visibility = Visibility.Visible);


                    }









                    ResultPageLoader(currentResultPage);
                    Searcher.Abort();

                }

                this.Dispatcher.Invoke(() => searchError.Visibility = Visibility.Visible);
                this.Dispatcher.Invoke(() => loadingImage.Source = new BitmapImage(new Uri(@"/GTFSApplication;component/Resources/errorSearch.png", UriKind.RelativeOrAbsolute)));
                searching = false;
                Searcher.Abort();
            

        }
        

        

        private void prevSearch()
        {


            ResultValues lastResult;
            int triesNum = 0;

            if (currentResultPage >= 0)
            {
                lastResult = (ResultValues)dataGridResult.Items[4];
               
            }
            else
            {
                lastResult = (ResultValues)dataGridResult.Items[0];
            }

         

            searchTime = lastResult.resultDepartureTime;
            DTDateFromPicker = DateTime.ParseExact(lastResult.resultDate,
                                    "dd.MM.yyyy",
                                     null);
            searchDateFromPicker = DTDateFromPicker.Date.ToString("dd.MM.yyyy");

            if (searchTime.Length == 8)
            {
                //searchTime = searchTime.Substring(0, 4) + "%";

                searchTime = searchTime.Substring(0, 5);

            }
            else
            {
                searchTime = searchTime.Substring(0, 4);
            }



     

            if (searchTime.Length == 5)
            {
                //searchTime = searchTime.Substring(0, 4) + "%";

                searchTimeHours = Int32.Parse(searchTime.Substring(0, 2));

                searchTimeMinutes = Int32.Parse(searchTime.Substring(3, 2));

            }
            else
            {
                searchTimeHours = Int32.Parse(searchTime.Substring(0, 1));

                searchTimeMinutes = Int32.Parse(searchTime.Substring(2, 2));

                //searchTime = searchTime.Substring(0, 3)+ "%";
            }


    




            int ChangeToPercentWhen = (searchTimeMinutes % 10)+1;
            int RemoveMinutesWhen = ChangeToPercentWhen + (((60 - searchTimeMinutes) / 10) - 1) + 6;


            

            do
            {



                if (triesNum < ChangeToPercentWhen)
                {
                    if (searchTimeMinutes < 10)
                    {
                        searchTime = searchTimeHours.ToString() + ":0" + searchTimeMinutes.ToString() + "%";
                    }
                    else
                    {
                        searchTime = searchTimeHours.ToString() + ":" + searchTimeMinutes.ToString() + "%";
                    }
                }
                else
                {
                    if (triesNum >= RemoveMinutesWhen)
                    {
                        searchTime = searchTimeHours.ToString() + ":%";

                    }
                    else
                    {


                        if (searchTimeMinutes < 10)
                        {
                            searchTime = searchTimeHours.ToString() + ":0%";
                        }

                        searchTime = searchTimeHours.ToString() + ":" + searchTimeMinutes.ToString().Substring(0, 1) + "%";
                    }

                }

               

                tempstopTimesResults.Clear();
                stopTimesResults.Clear();

                GetStopTimes(stopFromResults, stopToResults, tempstopTimesResults, stopTimesResults, searchTime, DTDateFromPicker);

                if (stopTimesResults.Count != 0)
                {

                    if (triesNum >= RemoveMinutesWhen)
                    {
                        for (int i = stopTimesResults.Count-1; i >=0; i--)
                        {

                            if (stopTimesResults[i].FROMdeparture_time != "")

                            {

                              



                                if (prevresultItems.Count < ((Math.Abs(currentResultPage) * 5)))
                                {
                                    prevresultItems.Add(new ResultValues()
                                    {
                                        resultDate = searchDateFromPicker,
                                        resultDepartureTime = stopTimesResults[i].FROMdeparture_time,
                                        resultArrivalTime = stopTimesResults[i].TOarrival_time,
                                        resultStop = stopTimesResults[i].FROMstop_name,
                                        resultNextStop = stopTimesResults[i].TOstop_name,
                                        resultTripNumber = stopTimesResults[i].trip_id
                                    });
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < stopTimesResults.Count; i++)
                        {

                            if (stopTimesResults[i].FROMdeparture_time != "")

                            {

                            



                                if (prevresultItems.Count < ((Math.Abs(currentResultPage) * 5)))
                                {
                                    prevresultItems.Add(new ResultValues()
                                    {
                                        resultDate = searchDateFromPicker,
                                        resultDepartureTime = stopTimesResults[i].FROMdeparture_time,
                                        resultArrivalTime = stopTimesResults[i].TOarrival_time,
                                        resultStop = stopTimesResults[i].FROMstop_name,
                                        resultNextStop = stopTimesResults[i].TOstop_name,
                                        resultTripNumber = stopTimesResults[i].trip_id
                                    });
                                }
                                else
                                {
                                    break;
                                }

                            }
                        }
                    }


                }

                if (triesNum < ChangeToPercentWhen)
                {
                    if (searchTimeMinutes == 0 & searchTimeHours == 0)
                    {
                        DTDateFromPicker = DTDateFromPicker.AddDays(-1);
                        searchDateFromPicker = DTDateFromPicker.Date.ToString("dd.MM.yyyy");
                        searchTimeHours = 23;
                        searchTimeMinutes = 59;
                    }
                    else
                    {

                        if (searchTimeMinutes > 0)
                        {
                            searchTimeMinutes--;
                        }
                        else
                        {
                            searchTimeHours--;
                            searchTimeMinutes = 59;
                        }
                    }
                }
                else

                {
                    if (triesNum >= RemoveMinutesWhen)
                    {
                        if (searchTimeHours == 0)
                        {
                            DTDateFromPicker = DTDateFromPicker.AddDays(-1);
                            searchDateFromPicker = DTDateFromPicker.Date.ToString("dd.MM.yyyy");
                            searchTimeHours = 23;                           
                        }
                        else
                        {
                            searchTimeHours--;
                        }

                    }
                    else
                    {

                        if (searchTimeMinutes == 0 & searchTimeHours == 0)
                        {
                            DTDateFromPicker = DTDateFromPicker.AddDays(-1);
                            searchDateFromPicker = DTDateFromPicker.Date.ToString("dd.MM.yyyy");
                            searchTimeHours = 23;
                            searchTimeMinutes = 50;
                        }
                        else
                        {

                            if (searchTimeMinutes > 0)
                            {
                                searchTimeMinutes = searchTimeMinutes - 10;
                            }
                            else
                            {
                                searchTimeHours--;
                                searchTimeMinutes = 50;
                            }
                        }

                    }
                }



                triesNum++;



            } while (prevresultItems.Count < (Math.Abs(currentResultPage) * 5) & triesNum < triesNumLimit);


          

            ResultPageLoader(currentResultPage);
            Searcher.Abort();


        }











        private void CheckIfStopExistsInStops(string stop, List<string> results)
        {

            string dbConnection = String.Format("Data Source={0}", DatabaseListFiles[ActiveDatabase]);
            SQLiteConnection conn = new SQLiteConnection(dbConnection);

            conn.Open();


            string sql = "SELECT DISTINCT stop_name FROM Stops WHERE stop_name='" + stop + "'";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                results.Add(Convert.ToString(reader["stop_name"]));
            }
            conn.Close();



        }

        public class ResultFromStopTimesValues
        {
            public string FROMstop_name { get; set; }
            public string FROMstop_sequence { get; set; }
            public string FROMDdeparture_time { get; set; }
            public string trip_id { get; set; }
            public string service_id { get; set; }
        }

        public class TotalResultFromStopTimesValues
        {
            public string FROMstop_name { get; set; }
            public string FROMstop_sequence { get; set; }
            public string FROMdeparture_time { get; set; }
            public string trip_id { get; set; }
            public string TOstop_name { get; set; }
            public string TOstop_sequence { get; set; }
            public string TOarrival_time { get; set; }

        }



        private void GetStopTimes(List<string> fromStop, List<string> toStop, List<ResultFromStopTimesValues> tempresults, List<TotalResultFromStopTimesValues> totalResults, string Time, DateTime Date)
        {
           

            string sqlDayCheck;
            string sqlDateCheckStart;
            string sqlDateCheckEnd;
            string sqlCalendarDatesCheck1;
            string sqlCalendarDatesCheck2;
            string ServiceCheck = "";

            DateTime StartDateCheckValue = DateTime.Now;
            DateTime EndDateCheckValue = DateTime.Now;
            bool StartDateCheck = false;
            bool EndDateCheck = false;


            string CalendarDatesCheck1 = "";
            string CalendarDatesCheck2 = "";

            string DateDayOfWeek = Date.DayOfWeek.ToString();
            string DateToCheck = Date.ToString("yyyyMMdd");


            string dbConnection = String.Format("Data Source={0}", DatabaseListFiles[ActiveDatabase]);
            SQLiteConnection conn = new SQLiteConnection(dbConnection);

            conn.Open();


            string sql1 = @"SELECT stop_name, stop_sequence, departure_time, StopTimes.trip_id, Trips.service_id FROM StopTimes LEFT JOIN Stops ON StopTimes.stop_id = Stops.stop_id 
             LEFT JOIN Trips ON StopTimes.trip_id = Trips.trip_id WHERE stop_name='" + fromStop[0] + "' AND departure_time LIKE '" + Time + "'"; 

            SQLiteCommand command = new SQLiteCommand(sql1, conn);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                tempresults.Add(new ResultFromStopTimesValues()
                {
                    FROMstop_name = Convert.ToString(reader["stop_name"]),
                    FROMstop_sequence = Convert.ToString(reader["stop_sequence"]),
                    FROMDdeparture_time = Convert.ToString(reader["departure_time"]),
                    trip_id = Convert.ToString(reader["trip_id"]),
                    service_id = Convert.ToString(reader["service_id"])
                });
            }


            


            if (tempresults.Count == 0)
            {
                //koniec
            }
            else // REMOVE AT I CHECK
            {
                for (int i = tempresults.Count - 1; i >= 0; i--)
                {

                    //SQL DATE CHECK 

                    sqlDayCheck = @"SELECT service_id from Calendar WHERE " + DateDayOfWeek + "='1' AND service_id='" + tempresults[i].service_id + "'";
                    sqlDateCheckStart = @"SELECT start_date from Calendar WHERE service_id='" + tempresults[i].service_id + "'";
                    sqlDateCheckEnd = @"SELECT end_date from Calendar WHERE service_id='" + tempresults[i].service_id + "'";
                 
                    sqlCalendarDatesCheck1 = @"SELECT exception_type from CalendarDates WHERE Date='" + DateToCheck + "' AND service_id='" + tempresults[i].service_id + "' and exception_type='2'";
                    sqlCalendarDatesCheck2 = @"SELECT exception_type from CalendarDates WHERE Date='" + DateToCheck + "' AND service_id='" + tempresults[i].service_id + "' and exception_type='1'";
                    ServiceCheck = "";
                    CalendarDatesCheck1 = "";
                    CalendarDatesCheck2 = "";
                  

                    SQLiteCommand startDateCheck = new SQLiteCommand(sqlDateCheckStart, conn);
                    SQLiteDataReader readerStartDateCheck = startDateCheck.ExecuteReader();
                    while (readerStartDateCheck.Read())
                    {

                        StartDateCheckValue = DateTime.ParseExact(readerStartDateCheck["start_date"].ToString(),
                                "yyyyMMdd",
                                 null);

                    }

                    SQLiteCommand endDateCheck = new SQLiteCommand(sqlDateCheckEnd, conn);
                    SQLiteDataReader readerEndDateCheck = endDateCheck.ExecuteReader();
                    while (readerEndDateCheck.Read())
                    {

                        EndDateCheckValue = DateTime.ParseExact(readerEndDateCheck["end_date"].ToString(),
                                "yyyyMMdd",
                                 null);

                    }


                    if (Date.Date < StartDateCheckValue.Date)
                    {
                        StartDateCheck = false;
                    }
                    else
                    {
                        StartDateCheck = true;
                    }

                    if (Date.Date > EndDateCheckValue.Date)
                    {
                        EndDateCheck = false;
                    }
                    else
                    {
                        EndDateCheck = true;
                    }



                   
                    //----------------------------------------------------------------------

                    if (!StartDateCheck | !EndDateCheck)
                    {
                        tempresults.RemoveAt(i);
                    }


                    else
                    {


                        SQLiteCommand commandDateCheck = new SQLiteCommand(sqlDayCheck, conn);
                        SQLiteDataReader readerDateCheck = commandDateCheck.ExecuteReader();

                       

                        if (readerDateCheck.HasRows == true)
                        {
                            while (readerDateCheck.Read())
                            {

                                ServiceCheck = Convert.ToString(readerDateCheck["service_id"]);

                            }
                        }

                     


                     //-------------------------------------------------------------------------------------------------- 
                        if (ServiceCheck != tempresults[i].service_id)
                        {


                            //SKONTROLOVAT CI NIE JE PRIDANA VYNIMKA V TEN DEN (exception type 1)

                            SQLiteCommand commandCalendarDatesCheck1 = new SQLiteCommand(sqlCalendarDatesCheck1, conn);
                            SQLiteDataReader readerCalendarDatesCheck1 = commandCalendarDatesCheck1.ExecuteReader();


                          

                            if (readerCalendarDatesCheck1.HasRows == true)
                            {
                                while (readerCalendarDatesCheck1.Read())
                                {

                                    CalendarDatesCheck1 = Convert.ToString(readerCalendarDatesCheck1["exception_type"]);

                                }
                            }

                            if (CalendarDatesCheck1 != "1")

                            {
                                tempresults.RemoveAt(i);

                            }




                        }
                        //------------------------------------------------------------------------------------
                        else   //SKONTROLOVAT CI NIE JE SVIATOK/ZRUSENE V TEN DEN (exception type 2)
                        {

                            SQLiteCommand commandCalendarDatesCheck2 = new SQLiteCommand(sqlCalendarDatesCheck2, conn);
                            SQLiteDataReader readerCalendarDatesCheck2 = commandCalendarDatesCheck2.ExecuteReader();



                            if (readerCalendarDatesCheck2.HasRows == true)
                            {
                                while (readerCalendarDatesCheck2.Read())
                                {

                                    CalendarDatesCheck2 = Convert.ToString(readerCalendarDatesCheck2["exception_type"]);

                                }
                            }

                          

                            if (CalendarDatesCheck2 == "2")
                            {
                                tempresults.RemoveAt(i);
                            }


                        }
                    }

                }


                
                //---------------------------------------------------------OPTIMALIZACIA 



                if (tempresults.Count > 0)
                {

                    for (int i = 0; i < tempresults.Count; i++)
                    {
                        totalResults.Add(new TotalResultFromStopTimesValues()
                        {
                            FROMstop_name = "",
                            FROMstop_sequence = "",
                            FROMdeparture_time = "",
                            trip_id = "",
                            TOstop_name = "",
                            TOstop_sequence = "",
                            TOarrival_time = ""

                        });
                    }

                    for (int i = tempresults.Count - 1; i >= 0; i--)
                    {
                        string sql2 = @"SELECT stop_name, stop_sequence, arrival_time, StopTimes.trip_id FROM StopTimes LEFT JOIN Stops
             ON StopTimes.stop_id = Stops.stop_id LEFT JOIN Trips ON StopTimes.trip_id = Trips.trip_id WHERE stop_sequence>" + tempresults[i].FROMstop_sequence + " AND StopTimes.trip_id = '" +
                     tempresults[i].trip_id + "' AND stop_name='" + toStop[0] + "'";


                        SQLiteCommand command2 = new SQLiteCommand(sql2, conn);
                        SQLiteDataReader reader2 = command2.ExecuteReader();



                        totalResults[i].FROMstop_name = tempresults[i].FROMstop_name;
                        totalResults[i].FROMstop_sequence = tempresults[i].FROMstop_sequence;
                        totalResults[i].FROMdeparture_time = tempresults[i].FROMDdeparture_time;
                        totalResults[i].trip_id = tempresults[i].trip_id;



                        while (reader2.Read())
                        {
                            totalResults[i].TOstop_name = Convert.ToString(reader2["stop_name"]);
                            totalResults[i].TOstop_sequence = Convert.ToString(reader2["stop_sequence"]);
                            totalResults[i].TOarrival_time = Convert.ToString(reader2["arrival_time"]);

                        }

                   


                        if (totalResults[i].TOstop_name == "")
                        {
                            totalResults.RemoveAt(i);

                        }




                    }
                }
            }


            conn.Close();





           



        }


        private void CheckIfDateExistsInCalendar()
        {
            string SearchDateFromPicker = searchDatePicker.Text;
            string NewSearchDate = SearchDateFromPicker.Substring(6, 4) + SearchDateFromPicker.Substring(3, 2) + SearchDateFromPicker.Substring(0, 2);
            /*DateTime SearchDate = DateTime.ParseExact(SearchDateFromPicker,
                                "yyyyMMdd",
                                 null);
            */


            string dbConnection = String.Format("Data Source={0}", DatabaseListFiles[ActiveDatabase]);
            SQLiteConnection conn = new SQLiteConnection(dbConnection);


            string sql = "select * from calendar where ";
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            SQLiteDataReader reader = command.ExecuteReader();


            conn.Close();






        }


        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)

        {

            e.Handled = !Regex.IsMatch(e.Text, @"[0-9]"); // allow only numbers

        }

        private void SpaceBarTextBox(object sender, KeyEventArgs e)

        {

            e.Handled = e.Key == Key.Space; // doesn't allow space in textBox

        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            if (tb.Text.Length > 4)

            {

                if (tb.Text.Substring(2, 1) != ":")
                    tb.Text = tb.Text.Substring(0, 1) + tb.Text.Substring(2, 1) + ":" + tb.Text.Substring(3, 2);

            }


            if (tb.Text.Length == 3)
            {
                if (!tb.Text.Contains(":"))
                {
                    tb.Text = tb.Text.Substring(0, 1) + ":" + tb.Text.Substring(1, 2);
                }
            }


            tb.CaretIndex = tb.Text.Length;
        }

        private void searchTimeTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var tb = (TextBox)sender;
            if (tb.Text.Length > 2)
            {
                if (!tb.Text.Contains(":"))
                {

                    tb.Text = tb.Text.Substring(0, 1) + ":" + tb.Text.Substring(1, 2);

                }
            }
        }

        private void ResultPageLoader(int currentpage)

        {
            if (currentpage == 0)
            {
                if (resultItems.Count > 0)
                {
                 
                    this.Dispatcher.Invoke(() => (dataGridResult.ItemsSource = resultItems.GetRange(currentpage, 5)));
                    this.Dispatcher.Invoke(() => dataGridResult.Items.Refresh());
                    this.Dispatcher.Invoke(() => dataGridResult.Visibility = Visibility.Visible);
                    this.Dispatcher.Invoke(() => loadingImage.Visibility = Visibility.Hidden);
                    this.Dispatcher.Invoke(() => previousButton.Visibility = Visibility.Visible);
                    this.Dispatcher.Invoke(() => nextButton.Visibility = Visibility.Visible);
              
                    searching = false;              
                }
                else
                {
                    this.Dispatcher.Invoke(() => searchError.Visibility = Visibility.Visible);
                    this.Dispatcher.Invoke(() => loadingImage.Source = new BitmapImage(new Uri(@"/GTFSApplication;component/Resources/errorSearch.png", UriKind.RelativeOrAbsolute)));
                    searching = false;
                   

                }


            }

            if (currentpage > 0)
            {
                
                if (resultItems.Count >= ((currentpage * 5) + 5))
                {
                    this.Dispatcher.Invoke(() => dataGridResult.ItemsSource = resultItems.GetRange(currentpage * 5, 5));
                    this.Dispatcher.Invoke(() => dataGridResult.Items.Refresh());
                    this.Dispatcher.Invoke(() => dataGridResult.Visibility = Visibility.Visible);
                    this.Dispatcher.Invoke(() => loadingImage.Visibility = Visibility.Hidden);
                    this.Dispatcher.Invoke(() => previousButton.Visibility = Visibility.Visible);
                    this.Dispatcher.Invoke(() => nextButton.Visibility = Visibility.Visible);

                    searching = false;
              
                }

                else
                {
                    this.Dispatcher.Invoke(() => searchError.Visibility = Visibility.Visible);
                    this.Dispatcher.Invoke(() => loadingImage.Source = new BitmapImage(new Uri(@"/GTFSApplication;component/Resources/errorSearch.png", UriKind.RelativeOrAbsolute)));
                    searching = false;
                
                }

            }

            if (currentpage < 0)
            {
                int convertedpage = Math.Abs(currentpage);


              
                if (prevresultItems.Count >= convertedpage * 5)
                {
                    reverseresultItems = prevresultItems.GetRange((convertedpage * 5)-5, 5);
                    reverseresultItems.Reverse();
                    this.Dispatcher.Invoke(() => dataGridResult.ItemsSource = reverseresultItems);
                                  
                    this.Dispatcher.Invoke(() => dataGridResult.Items.Refresh());
                    this.Dispatcher.Invoke(() => dataGridResult.Visibility = Visibility.Visible);
                    this.Dispatcher.Invoke(() => loadingImage.Visibility = Visibility.Hidden);
                    this.Dispatcher.Invoke(() => previousButton.Visibility = Visibility.Visible);
                    this.Dispatcher.Invoke(() => nextButton.Visibility = Visibility.Visible);
                    searching = false;
                
                }
                else
                {
                    this.Dispatcher.Invoke(() => searchError.Visibility = Visibility.Visible);
                    this.Dispatcher.Invoke(() => loadingImage.Source = new BitmapImage(new Uri(@"/GTFSApplication;component/Resources/errorSearch.png", UriKind.RelativeOrAbsolute)));
                    searching = false;
              
                }



            }





            this.Dispatcher.Invoke(() => page.Text = currentpage.ToString());
            BrushConverter bc = new BrushConverter();
            this.Dispatcher.Invoke(() => searchButton.Content = "VYHĽADAŤ");
            this.Dispatcher.Invoke(() => searchButton.Background = (Brush)bc.ConvertFrom("#FF9ECAF5"));
            this.Dispatcher.Invoke(() => searchButton.Click -= Cancel_Click);            
            this.Dispatcher.Invoke(() => searchButton.Click += Search_Click);



        }

        private void nextSearch_Click (object sender, RoutedEventArgs e)
        {
            currentResultPage++;
            

            dataGridResult.Visibility = Visibility.Hidden;
            loadingImage.Visibility = Visibility.Visible;
            previousButton.Visibility = Visibility.Hidden;
            nextButton.Visibility = Visibility.Hidden;

            if (!searching)
            {
                searching = true;
                if (currentResultPage < 0)
                {
                    ResultPageLoader(currentResultPage);
                }
                else if (currentResultPage == 0)
                {
                    ResultPageLoader(currentResultPage);
                }                
                else
                {
                    if(resultItems.Count >= (currentResultPage * 5)+5)
                    {
                        ResultPageLoader(currentResultPage);
                    }
                    else
                    {
                        Searcher = new Thread(() => Search(1));
                        Searcher.Priority = ThreadPriority.AboveNormal;
                        Searcher.Start();
                    }
                }
            }

        }

        private void prevSearch_Click(object sender, RoutedEventArgs e)
        {
            currentResultPage--;

            dataGridResult.Visibility = Visibility.Hidden;
            loadingImage.Visibility = Visibility.Visible;
            previousButton.Visibility = Visibility.Hidden;
            nextButton.Visibility = Visibility.Hidden;

            if (!searching)
            {

                searching = true;
                if (currentResultPage < 0)
                {
                    if (prevresultItems.Count >= Math.Abs(currentResultPage) * 5)
                    {
                        ResultPageLoader(currentResultPage);
                    }
                    else
                    {
                        Searcher = new Thread(() => prevSearch());
                        Searcher.Priority = ThreadPriority.AboveNormal;
                        Searcher.Start();
                    }
                }
                else
                {
                    ResultPageLoader(currentResultPage);
                }
            }

        }
        private void swap_Click(object sender, RoutedEventArgs e)
        {
            string swapFrom = searchFrom.Text;
            string swapTo = searchTo.Text;

            searchFrom.Text = swapTo;
            searchTo.Text = swapFrom;
        }

        private void GetFullTrip(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            string trip_id = btn.Tag.ToString();
            List<TripResultValues> resultsTrip = new List<TripResultValues>();

            string dbConnection = String.Format("Data Source={0}", DatabaseListFiles[ActiveDatabase]);
            SQLiteConnection conn = new SQLiteConnection(dbConnection);

            conn.Open();

            string sql = @"SELECT stop_name, stop_sequence, arrival_time, StopTimes.trip_id FROM StopTimes LEFT JOIN Stops
             ON StopTimes.stop_id = Stops.stop_id LEFT JOIN Trips ON StopTimes.trip_id = Trips.trip_id WHERE StopTimes.trip_id = '" +
                     trip_id + "'";

            SQLiteCommand command = new SQLiteCommand(sql, conn);
            SQLiteDataReader reader = command.ExecuteReader();
            int i = 1;
            while (reader.Read())
            {
                resultsTrip.Add(new TripResultValues()
                {
                    resultStop = Convert.ToString(reader["stop_name"]),                    
                    resultArrivalTime = Convert.ToString(reader["arrival_time"]),
                    resultNumber=i.ToString()
                   
                });
                i++;

             }

            conn.Close();


            TripWindow n = new TripWindow(resultsTrip,trip_id);
          




        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ActiveDatabase = comboBox.SelectedIndex;

            bool CheckDatabaseIfValid = CheckDatabase();
            if (!CheckDatabaseIfValid)
            {
                searchButton.IsEnabled = false;
                searchError.Text = "V databáze nie je možné vyhľadávať.";
                searchError.Visibility = Visibility.Visible;

            }
            else
            {
                AddStopsItems();

                searchButton.IsEnabled = true;
                searchError.Visibility = Visibility.Hidden;

                searchFrom.DataContext = null;
                searchFrom.DataContext = searchFromModel;
                searchTo.DataContext = null;
                searchTo.DataContext = searchToModel;
            }


         


        }

        private void dbManagerClosed (object sender, EventArgs e)
        {
            AddDatabases();

            comboBox.ItemsSource = DatabaseListNames;
            comboBox.SelectedIndex = 0;
            ActiveDatabase = 0;
        }
    

}

    }
