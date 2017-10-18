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
using System.Data.SQLite;
using System.IO;
using System.Threading;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.Win32;

namespace GTFSApplication
{
    /// <summary>
    /// Interaction logic 
    /// /// </summary>
    public partial class DatabaseManager : Window
    {
        public DatabaseManager(EditorClass editorclassfromMain, List<string> DatabaseFileNames, List<string> DatabaseNames)
        {

            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            editorclass = editorclassfromMain;





            DatabaseListFiles = DatabaseFileNames;
          

            for (int i=0; i< DatabaseNames.Count;i++)
            {
                bindingListNames.Add(DatabaseNames[i]);
            }
          


            //comboBox.SelectedValue = new Binding (ActiveDatabase);







            DataGridFilesLoader();
            DataGridDatabaseLoader();
            dataGridFiles.ItemsSource = datagridFilesItems;
            dataGridDatabase.ItemsSource = datagridDatabaseItems;

            DateTime lastModifiedDate = System.IO.File.GetLastWriteTime(DatabaseListFiles[ActiveDatabase]);

            lastModified.Text = "Naposledy aktualizované: " + lastModifiedDate.ToString("dd.MM.yyyy");

            comboBox.ItemsSource = bindingListNames;
            comboBox.SelectedIndex = 0;
            ActiveDatabase = 0;


        }

        private void DatabaseManagerClosed(object sender, EventArgs e)
        {
            Application.Current.MainWindow.Show();
    
        }

        List<DatagridFilesValues> datagridFilesItems = new List<DatagridFilesValues>();
        List<DataGridDatabaseClass> datagridDatabaseItems = new List<DataGridDatabaseClass>();

        EditorClass editorclass;



      
        int ActiveDatabase;

       
        public List<string> DatabaseListFiles = new List<string>();
        BindingList<string> bindingListNames = new BindingList<string>();
     

     

       




        string createAgency = @"CREATE TABLE IF NOT EXISTS [Agency]
                                (
                                
                                [agency_id] text,
                                [agency_name] text,
                                [agency_url] text,
                                [agency_timezone] text,
                                [agency_lang] text,
                                [agency_phone] text,
                                [agency_fare_url] text,
                                [agency_email] text 
                                )";

        string createCalendar = @"CREATE TABLE IF NOT EXISTS [Calendar]
                                (
                                
                                [service_id] text,
                                [monday] text,
                                [tuesday] text,
                                [wednesday] text,
                                [thursday] text,
                                [friday] text,
                                [saturday] text,
                                [sunday] text,
                                [start_date] text,
                                [end_date] text
                                )";


        string createCalendarDates = @"CREATE TABLE IF NOT EXISTS [CalendarDates]
                                (
                                
                                [service_id] text,
                                [date] text,
                                [exception_type] text,
                                FOREIGN KEY (service_id) REFERENCES Calendar(service_id)                               
                                )";

        string createRoutes = @"CREATE TABLE IF NOT EXISTS [Routes]
                                (
                                
                                [route_id] text,
                                [agency_id] text,
                                [route_short_name] text, 
                                [route_long_name] text,
                                [route_desc] text,
                                [route_type] text,
                                [route_url] text,
                                [route_color] text,
                                [route_text_color] text                          
                                )";

        string createTrips = @"CREATE TABLE IF NOT EXISTS [Trips]
                                (
                                
                                [route_id] text,
                                [service_id] text,
                                [trip_id] text, 
                                [trip_headsign] text,
                                [trip_short_name] text,
                                [direction_id] text,
                                [block_id] text,
                                [shape_id] text,
                                [wheelchair_accesible] text,  
                                [bikes_allowance] text,
                                FOREIGN KEY (route_id) REFERENCES Routes(route_id),  
                                FOREIGN KEY (service_id) REFERENCES Calendar(service_id)    
                                )";

        string createStops = @"CREATE TABLE IF NOT EXISTS [Stops]
                                (
                                
                                [stop_id] text,
                                [stop_code] text,
                                [stop_name] text, 
                                [stop_desc] text,
                                [stop_lat] text,
                                [stop_lon] text,
                                [zone_id] text,
                                [stop_url] text,
                                [location_type] text,
                                [parent_station] text,
                                [stop_timezone] text,
                                [wheelchair_boarding] text                                
                                )";



        string createStopTimes = @"CREATE TABLE IF NOT EXISTS [StopTimes]
                                (
                                
                                [trip_id] text,
                                [arrival_time] text,
                                [departure_time] text, 
                                [stop_id] text,
                                [stop_sequence] number,
                                [stop_headsign] text,
                                [pickup_type] text,
                                [drop_off_type] text,
                                [shape_dist_traveled] text,  
                                [timepoint] text,
                                FOREIGN KEY (trip_id) REFERENCES Trips(trip_id),
                                FOREIGN KEY (stop_id) REFERENCES Stops(stop_id)   
                                )";






        public string[] TextFileNames = new string[]
                {
                "agency.txt",
                "calendar.txt",
                "calendar_dates.txt",
                "routes.txt",
                "trips.txt",
                "stops.txt",
                "stop_times.txt"
                };

        public string[] TableNames = new string[]
        {
                "Agency",
                "Calendar",
                "Calendar Dates",
                "Routes",
                "Trips",
                "Stops",
                "StopTimes"
        };

        public class DatagridFilesValues
        {
            public string DBFilesEntityNames { get; set; }
            public string DBFilesEntityRows { get; set; }
            public string DBFilesEntityValidationStatus { get; set; }
        }

        public class DatagridDatabaseValues
        {
            public string DBTablesNames { get; set; }
            public string DBTablesRows { get; set; }
            public int DBProgressBars { get; set; }


        }
  



        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }





        private void DataGridFilesLoader()

        {
            datagridFilesItems.Add(new DatagridFilesValues() { DBFilesEntityNames = TextFileNames[0], DBFilesEntityRows = editorclass.agencyItems.Count.ToString(), DBFilesEntityValidationStatus = "Neskontrolované" });
            datagridFilesItems.Add(new DatagridFilesValues() { DBFilesEntityNames = TextFileNames[1], DBFilesEntityRows = editorclass.calendarItems.Count.ToString(), DBFilesEntityValidationStatus = "Neskontrolované" });
            datagridFilesItems.Add(new DatagridFilesValues() { DBFilesEntityNames = TextFileNames[2], DBFilesEntityRows = editorclass.calendarDatesItems.Count.ToString(), DBFilesEntityValidationStatus = "Neskontrolované" });
            datagridFilesItems.Add(new DatagridFilesValues() { DBFilesEntityNames = TextFileNames[3], DBFilesEntityRows = editorclass.routesItems.Count.ToString(), DBFilesEntityValidationStatus = "Neskontrolované" });
            datagridFilesItems.Add(new DatagridFilesValues() { DBFilesEntityNames = TextFileNames[4], DBFilesEntityRows = editorclass.tripsItems.Count.ToString(), DBFilesEntityValidationStatus = "Neskontrolované" });
            datagridFilesItems.Add(new DatagridFilesValues() { DBFilesEntityNames = TextFileNames[5], DBFilesEntityRows = editorclass.stopsItems.Count.ToString(), DBFilesEntityValidationStatus = "Neskontrolované" });
            datagridFilesItems.Add(new DatagridFilesValues() { DBFilesEntityNames = TextFileNames[6], DBFilesEntityRows = editorclass.stopTimesItems.Count.ToString(), DBFilesEntityValidationStatus = "Neskontrolované" });
        }

        private void DataGridDatabaseLoader()

        {


            for (int i = 0; i < 7; i++)
            {
                datagridDatabaseItems.Add(new DataGridDatabaseClass() { DBTablesNames = TableNames[i], DBTablesRows = "0", ProgressValue = 0 });

            }
        }

        private void CreateNewDatabase(object sender, EventArgs e)
        {

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            saveFileDialog1.Title = "Uložiť databázu ako";
            //saveFileDialog1.CheckFileExists = true;
            saveFileDialog1.CheckPathExists = true;
            saveFileDialog1.DefaultExt = "db3";
            saveFileDialog1.Filter = "Databázy (*.db3)|*.db3|Všetky súbory (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == true)
            {
               
                string filename_with_ext = System.IO.Path.GetFileName(saveFileDialog1.FileName);
                SQLiteConnection.CreateFile(filename_with_ext);
                DatabaseListFiles.Add(filename_with_ext);
                string filename_without_ext = System.IO.Path.GetFileNameWithoutExtension(saveFileDialog1.FileName);          
                bindingListNames.Add(filename_without_ext);
                ActiveDatabase = (bindingListNames.Count)-1;                
                comboBox.SelectedIndex = ActiveDatabase;

            }

                   


        }

        private void dataGridRowCkecker()
        {
            string dbConnection = String.Format("Data Source={0}", DatabaseListFiles[ActiveDatabase]);
            SQLiteConnection conn = new SQLiteConnection(dbConnection);
            conn.Open();
            string rowCheckString = "select (select count() from Calendar) as MYCOUNT";


            SQLiteCommand RowCheck = new SQLiteCommand(rowCheckString, conn);
            string Result = "";
            try
            {
                SQLiteDataReader RowCheckReader = RowCheck.ExecuteReader();


                while (RowCheckReader.Read())
                {

                    Result = Convert.ToString(RowCheckReader["MYCOUNT"]);

                }
                datagridDatabaseItems[0].DBTablesRows = "8";
                datagridDatabaseItems[1].DBTablesRows = Result;

                rowCheckString = "select (select count() from CalendarDates) as MYCOUNT";
                RowCheck = new SQLiteCommand(rowCheckString, conn);
                RowCheckReader = RowCheck.ExecuteReader();

                while (RowCheckReader.Read())
                {

                    Result = Convert.ToString(RowCheckReader["MYCOUNT"]);

                }

                datagridDatabaseItems[2].DBTablesRows = Result;

                rowCheckString = "select (select count() from Routes) as MYCOUNT";
                RowCheck = new SQLiteCommand(rowCheckString, conn);
                RowCheckReader = RowCheck.ExecuteReader();

                while (RowCheckReader.Read())
                {

                    Result = Convert.ToString(RowCheckReader["MYCOUNT"]);

                }

                datagridDatabaseItems[3].DBTablesRows = Result;

                rowCheckString = "select (select count() from Trips) as MYCOUNT";
                RowCheck = new SQLiteCommand(rowCheckString, conn);
                RowCheckReader = RowCheck.ExecuteReader();

                while (RowCheckReader.Read())
                {

                    Result = Convert.ToString(RowCheckReader["MYCOUNT"]);

                }

                datagridDatabaseItems[4].DBTablesRows = Result;

                rowCheckString = "select (select count() from Stops) as MYCOUNT";
                RowCheck = new SQLiteCommand(rowCheckString, conn);
                RowCheckReader = RowCheck.ExecuteReader();

                while (RowCheckReader.Read())
                {

                    Result = Convert.ToString(RowCheckReader["MYCOUNT"]);

                }
                datagridDatabaseItems[5].DBTablesRows = Result;

                rowCheckString = "select (select count() from StopTimes) as MYCOUNT";
                RowCheck = new SQLiteCommand(rowCheckString, conn);
                RowCheckReader = RowCheck.ExecuteReader();

                while (RowCheckReader.Read())
                {

                    Result = Convert.ToString(RowCheckReader["MYCOUNT"]);

                }

                datagridDatabaseItems[6].DBTablesRows = Result;




            }
            catch(SQLiteException)
            {
                datagridDatabaseItems[0].DBTablesRows = "0";
                datagridDatabaseItems[1].DBTablesRows = "0";
                datagridDatabaseItems[2].DBTablesRows = "0";         
                datagridDatabaseItems[3].DBTablesRows = "0";
                datagridDatabaseItems[4].DBTablesRows = "0";
                datagridDatabaseItems[5].DBTablesRows = "0";
                datagridDatabaseItems[6].DBTablesRows = "0";
            }
            
          
        }
        CancellationTokenSource tokenSource = new CancellationTokenSource();


        private void CreateTables()
        {                               
                string dbConnection = String.Format("Data Source={0}", DatabaseListFiles[ActiveDatabase]);
                SQLiteConnection conn = new SQLiteConnection(dbConnection);
                SQLiteCommand cmd = new SQLiteCommand(conn);
                using (conn)
                {
                    using (cmd)
                    {
                        conn.Open();
                        cmd.CommandText = createAgency;
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = createCalendar;
                        cmd.ExecuteNonQuery(); 
                        cmd.CommandText = createCalendarDates;
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = createRoutes;
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = createTrips;
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = createStops;
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = createStopTimes;
                        cmd.ExecuteNonQuery();
                        conn.Close();

                    }

                }
                            

                taskbuttonImage.Source = new BitmapImage(new Uri(@"/GTFSApplication;component/Resources/errorSearch.png", UriKind.RelativeOrAbsolute));
                taskbuttonText.Text = "Zrušiť";
                taskbutton.Click -= DatabaseUpdate;
                taskbutton.Click += DatabaseCancelUpdate;

            
                Task.Run(() => AddValuesAgency(tokenSource.Token), tokenSource.Token);
                Task.Run(() => AddValuesCalendar(tokenSource.Token), tokenSource.Token);
                Task.Run(() => AddValuesCalendarDates(tokenSource.Token), tokenSource.Token);
                Task.Run(() => AddValuesRoutes(tokenSource.Token), tokenSource.Token);
                Task.Run(() => AddValuesTrips(tokenSource.Token), tokenSource.Token);
                Task.Run(() => AddValuesStops(tokenSource.Token), tokenSource.Token);
                Task.Run(() => AddValuesStopTimes(tokenSource.Token), tokenSource.Token);                
        }



        /*
        private async Task CreateTablesAsync()
        {
            await Task.Run(() =>
            {
                Parallel.Invoke(() => AddValuesAgency(tokenSource.Token), () => AddValuesCalendar(tokenSource.Token), () => AddValuesCalendarDates(tokenSource.Token), () => AddValuesRoutes(tokenSource.Token), () => AddValuesTrips(tokenSource.Token),
               () => AddValuesStops(tokenSource.Token), () => AddValuesStopTimes(tokenSource.Token));
            });
        }
        */



        private void AddValues()
        {



            string dbConnection = String.Format("Data Source={0}", DatabaseListFiles[ActiveDatabase]);
            SQLiteConnection conn = new SQLiteConnection(dbConnection);
            SQLiteCommand cmd = new SQLiteCommand(conn);



            using (conn)
            {
                using (cmd)
                {
                    conn.Open();

                    cmd.CommandText = "delete from Agency";
                    cmd.ExecuteNonQuery();

                    string agencyAdd;

                    agencyAdd = "INSERT INTO Agency values(";
                    for (int i = 0; i < 8; i++)
                    {
                        agencyAdd = agencyAdd + "'" + editorclass.agencyItems[i].AgencyData + "'";
                        if (i != 7)
                        {
                            agencyAdd = agencyAdd + ",";
                        }
                    }

                    agencyAdd = agencyAdd + ")";



                    cmd.CommandText = agencyAdd;
                    cmd.ExecuteNonQuery();


                    cmd.CommandText = "delete from Calendar";
                    cmd.ExecuteNonQuery();

                    string calendarAdd;

                    for (int j = 0; j < editorclass.calendarItems.Count; j++)
                    {
                        calendarAdd = "INSERT INTO Calendar values(";
                        calendarAdd = calendarAdd + "'" + editorclass.calendarItems[j].CalendarServiceid + "',";
                        calendarAdd = calendarAdd + "'" + ConvertStyle(editorclass.calendarItems[j].CalendarMondayStyle) + "',";
                        calendarAdd = calendarAdd + "'" + ConvertStyle(editorclass.calendarItems[j].CalendarTuesdayStyle) + "',";
                        calendarAdd = calendarAdd + "'" + ConvertStyle(editorclass.calendarItems[j].CalendarWednesdayStyle) + "',";
                        calendarAdd = calendarAdd + "'" + ConvertStyle(editorclass.calendarItems[j].CalendarThursdayStyle) + "',";
                        calendarAdd = calendarAdd + "'" + ConvertStyle(editorclass.calendarItems[j].CalendarFridayStyle) + "',";
                        calendarAdd = calendarAdd + "'" + ConvertStyle(editorclass.calendarItems[j].CalendarSaturdayStyle) + "',";
                        calendarAdd = calendarAdd + "'" + ConvertStyle(editorclass.calendarItems[j].CalendarSundayStyle) + "',";
                        calendarAdd = calendarAdd + "'" + editorclass.calendarItems[j].CalendarStartDate.ToString("yyyyMMdd") + "',";
                        calendarAdd = calendarAdd + "'" + editorclass.calendarItems[j].CalendarEndDate.ToString("yyyyMMdd") + "'";
                        calendarAdd = calendarAdd + ")";
                        cmd.CommandText = calendarAdd;
                        cmd.ExecuteNonQuery();
                        //DBValidationTextBlock.Text = DBValidationTextBlock.Text + "\n" + "Zapisujem " + j.ToString() + ".riadok do Calendar tabuľky";

                    }

                    cmd.CommandText = "delete from CalendarDates";
                    cmd.ExecuteNonQuery();

                    string calendarDatesAdd;

                    for (int j = 0; j < editorclass.calendarDatesItems.Count; j++)
                    {
                        calendarDatesAdd = "INSERT INTO CalendarDates values(";
                        calendarDatesAdd = calendarDatesAdd + "'" + editorclass.calendarDatesItems[j].CalendarDatesServiceid + "',";
                        calendarDatesAdd = calendarDatesAdd + "'" + editorclass.calendarDatesItems[j].CalendarDatesDate.ToString("yyyyMMdd") + "',";
                        calendarDatesAdd = calendarDatesAdd + "'" + ConvertStyle2(editorclass.calendarDatesItems[j].CalendarDatesExceptionType) + "'";
                        calendarDatesAdd = calendarDatesAdd + ")";
                        cmd.CommandText = calendarDatesAdd;
                        cmd.ExecuteNonQuery();
                        //DBValidationTextBlock.Text = DBValidationTextBlock.Text + "\n" + "Zapisujem " + j.ToString() + ".riadok do CalendarDates tabuľky";

                    }

                    cmd.CommandText = "delete from Routes";
                    cmd.ExecuteNonQuery();

                    string RoutesAdd;

                    for (int j = 0; j < editorclass.routesItems.Count; j++)
                    {
                        RoutesAdd = "INSERT INTO Routes values(";
                        RoutesAdd = RoutesAdd + "'" + editorclass.routesItems[j].Routeid + "',";
                        RoutesAdd = RoutesAdd + "'" + editorclass.routesItems[j].RouteAgencyid + "',";
                        RoutesAdd = RoutesAdd + "'" + editorclass.routesItems[j].RouteShortName + "',";
                        RoutesAdd = RoutesAdd + "'" + editorclass.routesItems[j].RouteLongName + "',";
                        RoutesAdd = RoutesAdd + "'" + editorclass.routesItems[j].RouteDesc + "',";
                        RoutesAdd = RoutesAdd + "'" + editorclass.routesItems[j].RouteType + "',";
                        RoutesAdd = RoutesAdd + "'" + editorclass.routesItems[j].RouteUrl + "',";
                        RoutesAdd = RoutesAdd + "'" + editorclass.routesItems[j].RouteColor + "',";
                        RoutesAdd = RoutesAdd + "'" + editorclass.routesItems[j].RouteColorText + "'";

                        RoutesAdd = RoutesAdd + ")";
                        cmd.CommandText = RoutesAdd;
                        cmd.ExecuteNonQuery();
                        //DBValidationTextBlock.Text = DBValidationTextBlock.Text + "\n" + "Zapisujem " + j.ToString() + ".riadok do Routes tabuľky";

                    }

                    cmd.CommandText = "delete from Trips";
                    cmd.ExecuteNonQuery();

                    string TripsAdd;

                    for (int j = 0; j < editorclass.tripsItems.Count; j++)
                    {
                        TripsAdd = "INSERT INTO Trips values(";
                        TripsAdd = TripsAdd + "'" + editorclass.tripsItems[j].TripsRouteid + "',";
                        TripsAdd = TripsAdd + "'" + editorclass.tripsItems[j].TripsServiceid + "',";
                        TripsAdd = TripsAdd + "'" + editorclass.tripsItems[j].TripsTripid + "',";
                        TripsAdd = TripsAdd + "'" + editorclass.tripsItems[j].TripsHeadsign + "',";
                        TripsAdd = TripsAdd + "'" + editorclass.tripsItems[j].TripsShortName + "',";
                        TripsAdd = TripsAdd + "'" + editorclass.tripsItems[j].TripsDirectionid + "',";
                        TripsAdd = TripsAdd + "'" + editorclass.tripsItems[j].TripsBlockid + "',";
                        TripsAdd = TripsAdd + "'" + editorclass.tripsItems[j].TripsShapeid + "',";
                        TripsAdd = TripsAdd + "'" + editorclass.tripsItems[j].TripsWheelchair + "',";
                        TripsAdd = TripsAdd + "'" + editorclass.tripsItems[j].TripsBikes + "'";

                        TripsAdd = TripsAdd + ")";
                        cmd.CommandText = TripsAdd;
                        cmd.ExecuteNonQuery();
                        //DBValidationTextBlock.Text = DBValidationTextBlock.Text + "\n" + "Zapisujem " + j.ToString() + ".riadok do Trips tabuľky";
                    }

                    cmd.CommandText = "delete from Stops";
                    cmd.ExecuteNonQuery();

                    string StopsAdd;

                    for (int j = 0; j < editorclass.stopsItems.Count; j++)
                    {
                        StopsAdd = "INSERT INTO Stops values(";
                        StopsAdd = StopsAdd + "'" + editorclass.stopsItems[j].Stopsid + "',";
                        StopsAdd = StopsAdd + "'" + editorclass.stopsItems[j].StopsCode + "',";
                        StopsAdd = StopsAdd + "'" + editorclass.stopsItems[j].StopsName + "',";
                        StopsAdd = StopsAdd + "'" + editorclass.stopsItems[j].StopsDesc + "',";
                        StopsAdd = StopsAdd + "'" + editorclass.stopsItems[j].StopsLat + "',";
                        StopsAdd = StopsAdd + "'" + editorclass.stopsItems[j].StopsLon + "',";
                        StopsAdd = StopsAdd + "'" + editorclass.stopsItems[j].StopsZoneid + "',";
                        StopsAdd = StopsAdd + "'" + editorclass.stopsItems[j].StopsUrl + "',";
                        StopsAdd = StopsAdd + "'" + editorclass.stopsItems[j].StopsLocationType + "',";
                        StopsAdd = StopsAdd + "'" + editorclass.stopsItems[j].StopsParentStation + "',";
                        StopsAdd = StopsAdd + "'" + editorclass.stopsItems[j].StopsTimezone + "',";
                        StopsAdd = StopsAdd + "'" + editorclass.stopsItems[j].StopsWheelChair + "'";

                        StopsAdd = StopsAdd + ")";
                        cmd.CommandText = StopsAdd;
                        cmd.ExecuteNonQuery();
                       //DBValidationTextBlock.Text = DBValidationTextBlock.Text + "\n" + "Zapisujem " + j.ToString() + ".riadok do Stops tabuľky";
                    }



                    cmd.CommandText = "delete from StopTimes";
                    cmd.ExecuteNonQuery();

                    string StopTimesAdd;

                    for (int j = 0; j < editorclass.stopTimesItems.Count; j++)
                    {
                        StopTimesAdd = "INSERT INTO StopTimes values(";
                        StopTimesAdd = StopTimesAdd + "'" + editorclass.stopTimesItems[j].StopTimesTripid + "',";
                        StopTimesAdd = StopTimesAdd + "'" + editorclass.stopTimesItems[j].StopTimesArrivalTimeHours + ":" + editorclass.stopTimesItems[j].StopTimesArrivalTimeMinutes + ":" + editorclass.stopTimesItems[j].StopTimesArrivalTimeSeconds + "',";
                        StopTimesAdd = StopTimesAdd + "'" + editorclass.stopTimesItems[j].StopTimesDepartureTimeHours + ":" + editorclass.stopTimesItems[j].StopTimesDepartureTimeMinutes + ":" + editorclass.stopTimesItems[j].StopTimesDepartureTimeSeconds + "',";
                        StopTimesAdd = StopTimesAdd + "'" + editorclass.stopTimesItems[j].StopTimesStopsid + "',";
                        StopTimesAdd = StopTimesAdd + "'" + editorclass.stopTimesItems[j].StopTimesSequence + "',";
                        StopTimesAdd = StopTimesAdd + "'" + editorclass.stopTimesItems[j].StopTimesHeadsign + "',";
                        StopTimesAdd = StopTimesAdd + "'" + editorclass.stopTimesItems[j].StopTimesPickupType + "',";
                        StopTimesAdd = StopTimesAdd + "'" + editorclass.stopTimesItems[j].StopTimesDropoffType + "',";
                        StopTimesAdd = StopTimesAdd + "'" + editorclass.stopTimesItems[j].StopTimesShape + "',";
                        StopTimesAdd = StopTimesAdd + "'" + editorclass.stopTimesItems[j].StopTimesTimepoint + "'";

                        StopTimesAdd = StopTimesAdd + ")";
                        cmd.CommandText = StopTimesAdd;
                        cmd.ExecuteNonQuery();
                        //DBValidationTextBlock.Text = DBValidationTextBlock.Text + "\n" + "Zapisujem " + j.ToString() + ".riadok do StopTimes tabuľky";
                    }




                    conn.Close();



                }

            }


        }

        private void AddValuesAgency(CancellationToken ct)
        {
            string dbConnection = String.Format("Data Source={0}", DatabaseListFiles[ActiveDatabase]);
            SQLiteConnection conn = new SQLiteConnection(dbConnection);
            SQLiteCommand cmd = new SQLiteCommand(conn);


            
                using (conn)
                {
                    using (cmd)
                    {
                        conn.Open();

                        cmd.CommandText = "delete from Agency";
                        cmd.ExecuteNonQuery();

                        string agencyAdd;

                        double progressValueIncrement = (double)100 / 8;
                        double progressValue = 0;

                        agencyAdd = "INSERT INTO Agency values(";


                        for (int i = 0; i < 8; i++)
                        {
                            tokenSource.Token.ThrowIfCancellationRequested();
                            agencyAdd = agencyAdd + "'" + editorclass.agencyItems[i].AgencyData + "'";
                            //this.Dispatcher.Invoke(() => DBValidationTextBlock.Text = DBValidationTextBlock.Text + "\n" + "Zapisujem " + i.ToString() + ".riadok do Agency tabuľky");
                            progressValue = progressValue + progressValueIncrement;

                            this.Dispatcher.Invoke(() => datagridDatabaseItems[0].DBTablesRows = (i + 1).ToString());
                            this.Dispatcher.Invoke(() => datagridDatabaseItems[0].ProgressValue = progressValue);

                            if (i != 7)
                            {
                                agencyAdd = agencyAdd + ",";
                            }
                        }

                        agencyAdd = agencyAdd + ")";



                        cmd.CommandText = agencyAdd;
                        cmd.ExecuteNonQuery();




                        conn.Close();
                        



                    }

                }
            
        }

        private void AddValuesCalendar(CancellationToken ct)
        {
            string dbConnection = String.Format("Data Source={0}", DatabaseListFiles[ActiveDatabase]);
            SQLiteConnection conn = new SQLiteConnection(dbConnection);
            SQLiteCommand cmd = new SQLiteCommand(conn);

            double progressValueIncrement = (double)100 / editorclass.calendarItems.Count;
            double progressValue = 0;


            using (conn)
            {
                using (cmd)
                {
                    conn.Open();

                    cmd.CommandText = "delete from Calendar";
                    cmd.ExecuteNonQuery();

                    string calendarAdd;

                    for (int j = 0; j < editorclass.calendarItems.Count; j++)
                    {
                        tokenSource.Token.ThrowIfCancellationRequested();
                        calendarAdd = "INSERT INTO Calendar values(";
                        calendarAdd = calendarAdd + "'" + editorclass.calendarItems[j].CalendarServiceid + "',";
                        calendarAdd = calendarAdd + "'" + ConvertStyle(editorclass.calendarItems[j].CalendarMondayStyle) + "',";
                        calendarAdd = calendarAdd + "'" + ConvertStyle(editorclass.calendarItems[j].CalendarTuesdayStyle) + "',";
                        calendarAdd = calendarAdd + "'" + ConvertStyle(editorclass.calendarItems[j].CalendarWednesdayStyle) + "',";
                        calendarAdd = calendarAdd + "'" + ConvertStyle(editorclass.calendarItems[j].CalendarThursdayStyle) + "',";
                        calendarAdd = calendarAdd + "'" + ConvertStyle(editorclass.calendarItems[j].CalendarFridayStyle) + "',";
                        calendarAdd = calendarAdd + "'" + ConvertStyle(editorclass.calendarItems[j].CalendarSaturdayStyle) + "',";
                        calendarAdd = calendarAdd + "'" + ConvertStyle(editorclass.calendarItems[j].CalendarSundayStyle) + "',";
                        calendarAdd = calendarAdd + "'" + editorclass.calendarItems[j].CalendarStartDate.ToString("yyyyMMdd") + "',";
                        calendarAdd = calendarAdd + "'" + editorclass.calendarItems[j].CalendarEndDate.ToString("yyyyMMdd") + "'";
                        calendarAdd = calendarAdd + ")";
                        cmd.CommandText = calendarAdd;
                        cmd.ExecuteNonQuery();

                        //this.Dispatcher.Invoke(() => DBValidationTextBlock.Text = DBValidationTextBlock.Text + "\n" + "Zapisujem " + j.ToString() + ".riadok do Calendar tabuľky");
                        progressValue = progressValue + progressValueIncrement;

                        this.Dispatcher.Invoke(() => datagridDatabaseItems[1].DBTablesRows = (j + 1).ToString());
                        this.Dispatcher.Invoke(() => datagridDatabaseItems[1].ProgressValue = progressValue);                 
                        
                    }




                    conn.Close();

                    //Thread.CurrentThread.Abort();


                }

            }
        }

        private void AddValuesCalendarDates(CancellationToken ct)
        {
            string dbConnection = String.Format("Data Source={0}", DatabaseListFiles[ActiveDatabase]);
            SQLiteConnection conn = new SQLiteConnection(dbConnection);
            SQLiteCommand cmd = new SQLiteCommand(conn);


            double progressValueIncrement = (double)100 / editorclass.calendarDatesItems.Count;
            double progressValue = 0;


            using (conn)
            {
                using (cmd)
                {
                    conn.Open();


                    cmd.CommandText = "delete from CalendarDates";
                    cmd.ExecuteNonQuery();

                    string calendarDatesAdd;

                    for (int j = 0; j < editorclass.calendarDatesItems.Count; j++)
                    {
                        tokenSource.Token.ThrowIfCancellationRequested();
                        calendarDatesAdd = "INSERT INTO CalendarDates values(";
                        calendarDatesAdd = calendarDatesAdd + "'" + editorclass.calendarDatesItems[j].CalendarDatesServiceid + "',";
                        calendarDatesAdd = calendarDatesAdd + "'" + editorclass.calendarDatesItems[j].CalendarDatesDate.ToString("yyyyMMdd") + "',";
                        calendarDatesAdd = calendarDatesAdd + "'" + ConvertStyle2(editorclass.calendarDatesItems[j].CalendarDatesExceptionType) + "'";
                        calendarDatesAdd = calendarDatesAdd + ")";

                        cmd.CommandText = calendarDatesAdd;
                        cmd.ExecuteNonQuery();

                        //this.Dispatcher.Invoke(() => DBValidationTextBlock.Text = DBValidationTextBlock.Text + "\n" + "Zapisujem " + j.ToString() + ".riadok do CalendarDates tabuľky");
                        progressValue = progressValue + progressValueIncrement;

                        this.Dispatcher.Invoke(() => datagridDatabaseItems[2].DBTablesRows = (j + 1).ToString());
                        this.Dispatcher.Invoke(() => datagridDatabaseItems[2].ProgressValue = progressValue);

                    }

             

                    conn.Close();
                    //Thread.CurrentThread.Abort();


                }

            }
        }

        private void AddValuesRoutes(CancellationToken ct)
        {
            string dbConnection = String.Format("Data Source={0}", DatabaseListFiles[ActiveDatabase]);
            SQLiteConnection conn = new SQLiteConnection(dbConnection);
            SQLiteCommand cmd = new SQLiteCommand(conn);

            double progressValueIncrement = (double)100 / editorclass.routesItems.Count;
            double progressValue = 0;


            using (conn)
            {
                using (cmd)
                {
                    conn.Open();


                    cmd.CommandText = "delete from Routes";
                    cmd.ExecuteNonQuery();

                    string RoutesAdd;

                    for (int j = 0; j < editorclass.routesItems.Count; j++)
                    {
                        tokenSource.Token.ThrowIfCancellationRequested();
                        RoutesAdd = "INSERT INTO Routes values(";
                        RoutesAdd = RoutesAdd + "'" + editorclass.routesItems[j].Routeid + "',";
                        RoutesAdd = RoutesAdd + "'" + editorclass.routesItems[j].RouteAgencyid + "',";
                        RoutesAdd = RoutesAdd + "'" + editorclass.routesItems[j].RouteShortName + "',";
                        RoutesAdd = RoutesAdd + "'" + editorclass.routesItems[j].RouteLongName + "',";
                        RoutesAdd = RoutesAdd + "'" + editorclass.routesItems[j].RouteDesc + "',";
                        RoutesAdd = RoutesAdd + "'" + ConvertStyle3(editorclass.routesItems[j].RouteType) + "',";                    
                        RoutesAdd = RoutesAdd + "'" + editorclass.routesItems[j].RouteUrl + "',";
                        RoutesAdd = RoutesAdd + "'" + editorclass.routesItems[j].RouteColor + "',";
                        RoutesAdd = RoutesAdd + "'" + editorclass.routesItems[j].RouteColorText + "'";

                        RoutesAdd = RoutesAdd + ")";
                        cmd.CommandText = RoutesAdd;
                        cmd.ExecuteNonQuery();


                        //this.Dispatcher.Invoke(() => DBValidationTextBlock.Text = DBValidationTextBlock.Text + "\n" + "Zapisujem " + j.ToString() + ".riadok do Routes tabuľky");
                        progressValue = progressValue + progressValueIncrement;

                        this.Dispatcher.Invoke(() => datagridDatabaseItems[3].DBTablesRows = (j + 1).ToString());
                        this.Dispatcher.Invoke(() => datagridDatabaseItems[3].ProgressValue = progressValue);

                    }



                    conn.Close();
                    //Thread.CurrentThread.Abort();


                }

            }




        }


        private void AddValuesTrips(CancellationToken ct)
        {
            string dbConnection = String.Format("Data Source={0}", DatabaseListFiles[ActiveDatabase]);
            SQLiteConnection conn = new SQLiteConnection(dbConnection);
            SQLiteCommand cmd = new SQLiteCommand(conn);

            double progressValueIncrement = (double)100 / editorclass.tripsItems.Count;
            double progressValue = 0;

            using (conn)
            {
                using (cmd)
                {
                    conn.Open();


                    cmd.CommandText = "delete from Trips";
                    cmd.ExecuteNonQuery();

                    string TripsAdd;

                    for (int j = 0; j < editorclass.tripsItems.Count; j++)
                    {
                        tokenSource.Token.ThrowIfCancellationRequested();
                        TripsAdd = "INSERT INTO Trips values(";
                        TripsAdd = TripsAdd + "'" + editorclass.tripsItems[j].TripsRouteid + "',";
                        TripsAdd = TripsAdd + "'" + editorclass.tripsItems[j].TripsServiceid + "',";
                        TripsAdd = TripsAdd + "'" + editorclass.tripsItems[j].TripsTripid + "',";
                        TripsAdd = TripsAdd + "'" + editorclass.tripsItems[j].TripsHeadsign + "',";
                        TripsAdd = TripsAdd + "'" + editorclass.tripsItems[j].TripsShortName + "',";
                        TripsAdd = TripsAdd + "'" + editorclass.tripsItems[j].TripsDirectionid + "',";
                        TripsAdd = TripsAdd + "'" + editorclass.tripsItems[j].TripsBlockid + "',";
                        TripsAdd = TripsAdd + "'" + editorclass.tripsItems[j].TripsShapeid + "',";
                        TripsAdd = TripsAdd + "'" + editorclass.tripsItems[j].TripsWheelchair + "',";
                        TripsAdd = TripsAdd + "'" + editorclass.tripsItems[j].TripsBikes + "'";

                        TripsAdd = TripsAdd + ")";
                        cmd.CommandText = TripsAdd;
                        cmd.ExecuteNonQuery();



                        //this.Dispatcher.Invoke(() => DBValidationTextBlock.Text = DBValidationTextBlock.Text + "\n" + "Zapisujem " + j.ToString() + ".riadok do Trips tabuľky");
                        progressValue = progressValue + progressValueIncrement;

                        this.Dispatcher.Invoke(() => datagridDatabaseItems[4].DBTablesRows = (j + 1).ToString());
                        this.Dispatcher.Invoke(() => datagridDatabaseItems[4].ProgressValue = progressValue);

                    }




                    conn.Close();

                    //Thread.CurrentThread.Abort();


                }

            }
        }

        private void AddValuesStops(CancellationToken ct)
        {

            string dbConnection = String.Format("Data Source={0}", DatabaseListFiles[ActiveDatabase]);
            SQLiteConnection conn = new SQLiteConnection(dbConnection);
            SQLiteCommand cmd = new SQLiteCommand(conn);


            double progressValueIncrement = (double)100 / editorclass.stopsItems.Count;
            double progressValue = 0;



            using (conn)
            {
                using (cmd)
                {
                    conn.Open();

        
                    cmd.CommandText = "delete from Stops";
                    cmd.ExecuteNonQuery();

                    string StopsAdd;

                    for (int j = 0; j < editorclass.stopsItems.Count; j++)
                    {
                        tokenSource.Token.ThrowIfCancellationRequested();
                        StopsAdd = "INSERT INTO Stops values(";
                        StopsAdd = StopsAdd + "'" + editorclass.stopsItems[j].Stopsid + "',";
                        StopsAdd = StopsAdd + "'" + editorclass.stopsItems[j].StopsCode + "',";
                        StopsAdd = StopsAdd + "'" + editorclass.stopsItems[j].StopsName + "',";
                        StopsAdd = StopsAdd + "'" + editorclass.stopsItems[j].StopsDesc + "',";
                        StopsAdd = StopsAdd + "'" + editorclass.stopsItems[j].StopsLat + "',";
                        StopsAdd = StopsAdd + "'" + editorclass.stopsItems[j].StopsLon + "',";
                        StopsAdd = StopsAdd + "'" + editorclass.stopsItems[j].StopsZoneid + "',";
                        StopsAdd = StopsAdd + "'" + editorclass.stopsItems[j].StopsUrl + "',";
                        StopsAdd = StopsAdd + "'" + editorclass.stopsItems[j].StopsLocationType + "',";
                        StopsAdd = StopsAdd + "'" + editorclass.stopsItems[j].StopsParentStation + "',";
                        StopsAdd = StopsAdd + "'" + editorclass.stopsItems[j].StopsTimezone + "',";
                        StopsAdd = StopsAdd + "'" + editorclass.stopsItems[j].StopsWheelChair + "'";

                        StopsAdd = StopsAdd + ")";
                        cmd.CommandText = StopsAdd;
                        cmd.ExecuteNonQuery();


                        //this.Dispatcher.Invoke(() => DBValidationTextBlock.Text = DBValidationTextBlock.Text + "\n" + "Zapisujem " + j.ToString() + ".riadok do Stops tabuľky");
                        progressValue = progressValue + progressValueIncrement;

                        this.Dispatcher.Invoke(() => datagridDatabaseItems[5].DBTablesRows = (j + 1).ToString());
                        this.Dispatcher.Invoke(() => datagridDatabaseItems[5].ProgressValue = progressValue);
                    }



 

                    conn.Close();
                


                }

            }

        }

        private void AddValuesStopTimes(CancellationToken ct)
        {
            string dbConnection = String.Format("Data Source={0}", DatabaseListFiles[ActiveDatabase]);
            SQLiteConnection conn = new SQLiteConnection(dbConnection);
            SQLiteCommand cmd = new SQLiteCommand(conn);


            double progressValueIncrement = (double)100 / editorclass.stopTimesItems.Count;
            double progressValue = 0;

            using (conn)
                {
                    using (cmd)
                    {
                        conn.Open();


                        cmd.CommandText = "delete from StopTimes";
                        cmd.ExecuteNonQuery();

                        string StopTimesAdd;

                        for (int j = 0; j < editorclass.stopTimesItems.Count; j++)
                        {

                            tokenSource.Token.ThrowIfCancellationRequested();
                            StopTimesAdd = "INSERT INTO StopTimes values(";
                            StopTimesAdd = StopTimesAdd + "'" + editorclass.stopTimesItems[j].StopTimesTripid + "',";
                            StopTimesAdd = StopTimesAdd + "'" + editorclass.stopTimesItems[j].StopTimesArrivalTimeHours + ":" + editorclass.stopTimesItems[j].StopTimesArrivalTimeMinutes + ":" + editorclass.stopTimesItems[j].StopTimesArrivalTimeSeconds + "',";
                            StopTimesAdd = StopTimesAdd + "'" + editorclass.stopTimesItems[j].StopTimesDepartureTimeHours + ":" + editorclass.stopTimesItems[j].StopTimesDepartureTimeMinutes + ":" + editorclass.stopTimesItems[j].StopTimesDepartureTimeSeconds + "',";
                            StopTimesAdd = StopTimesAdd + "'" + editorclass.stopTimesItems[j].StopTimesStopsid + "',";
                            StopTimesAdd = StopTimesAdd + "'" + editorclass.stopTimesItems[j].StopTimesSequence + "',";
                            StopTimesAdd = StopTimesAdd + "'" + editorclass.stopTimesItems[j].StopTimesHeadsign + "',";
                            StopTimesAdd = StopTimesAdd + "'" + editorclass.stopTimesItems[j].StopTimesPickupType + "',";
                            StopTimesAdd = StopTimesAdd + "'" + editorclass.stopTimesItems[j].StopTimesDropoffType + "',";
                            StopTimesAdd = StopTimesAdd + "'" + editorclass.stopTimesItems[j].StopTimesShape + "',";
                            StopTimesAdd = StopTimesAdd + "'" + editorclass.stopTimesItems[j].StopTimesTimepoint + "'";

                            StopTimesAdd = StopTimesAdd + ")";
                            cmd.CommandText = StopTimesAdd;
                            cmd.ExecuteNonQuery();


                            //this.Dispatcher.Invoke(() => DBValidationTextBlock.Text = DBValidationTextBlock.Text + "\n" + "Zapisujem " + j.ToString() + ".riadok do StopTimes tabuľky");
                            progressValue = progressValue + progressValueIncrement;

                            this.Dispatcher.Invoke(() => datagridDatabaseItems[6].DBTablesRows = (j + 1).ToString());
                            this.Dispatcher.Invoke(() => datagridDatabaseItems[6].ProgressValue = progressValue);
                        }




                        conn.Close();
                        //Thread.CurrentThread.Abort();



                    }

                }
            

          
        }


        private string ConvertStyle(Style style)
        {
            Style yesStyle = new Style();
            yesStyle = (Style)Application.Current.Resources["YesButtonImageStyle"];

            Style noStyle = new Style();
            noStyle = (Style)Application.Current.Resources["NoButtonImageStyle"];

            Style qStyle = new Style();
            qStyle = (Style)Application.Current.Resources["UnknownButtonImageStyle"];

            if (style == yesStyle)
            {
                return "1";
            }
            else return "0";


        }


        private string ConvertStyle2(Style style)
        {
            Style yesStyle = new Style();
            yesStyle = (Style)Application.Current.Resources["YesButtonImageStyle"];

            Style noStyle = new Style();
            noStyle = (Style)Application.Current.Resources["NoButtonImageStyle"];

            Style qStyle = new Style();
            qStyle = (Style)Application.Current.Resources["UnknownButtonImageStyle"];

            if (style == yesStyle)
            {
                return "1";
            }
            else return "2";


        }


        private string ConvertStyle3(Style style)
        {
            Style route0 = new Style();
            route0 = (Style)Application.Current.Resources["Route0ImageStyle"];

            Style route1 = new Style();
            route1 = (Style)Application.Current.Resources["Route1ImageStyle"];

            Style route2 = new Style();
            route2 = (Style)Application.Current.Resources["Route2ImageStyle"];

            Style route3 = new Style();
            route3 = (Style)Application.Current.Resources["Route3ImageStyle"];

            Style route4 = new Style();
            route4 = (Style)Application.Current.Resources["Route4ImageStyle"];

            Style route5 = new Style();
            route5 = (Style)Application.Current.Resources["Route5ImageStyle"];

            Style route6 = new Style();
            route6 = (Style)Application.Current.Resources["Route6ImageStyle"];

            Style route7 = new Style();
            route7 = (Style)Application.Current.Resources["Route7ImageStyle"];

            Style route11 = new Style();
            route11 = (Style)Application.Current.Resources["Route11ImageStyle"];



            if (style == route0)
            {
                return "0";
            }
            else if (style == route1)
            {
                return "1";
            }
            else if (style == route2)
            {
                return "2";
            }
            else if (style == route3)
            {
                return "3";
            }
            else if (style == route4)
            {
                return "4";
            }
            else if (style == route5)
            {
                return "5";
            }
            else if (style == route6)
            {
                return "6";
            }
            else if (style == route7)
            {
                return "7";
            }
            else if (style == route11)
            {
                return "11";
            }
            else
            {
                return "0";
            }




        }






        //db.execSQL("delete from "+ TABLE_NAME);




       
        private void DatabaseSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var comboBox = sender as ComboBox;


            int choice = comboBox.SelectedIndex;

            // Function(Choice);
            ActiveDatabase = choice;
            dataGridRowCkecker();
            DateTime lastModifiedDate = System.IO.File.GetLastWriteTime(DatabaseListFiles[ActiveDatabase]);

            lastModified.Text = "Naposledy aktualizované: " + lastModifiedDate.ToString("dd.MM.yyyy");
        }



        private void DatabaseUpdate(object sender, RoutedEventArgs e)
        {
            CreateTables();
        }

        private void DatabaseCancelUpdate(object sender, RoutedEventArgs e)
        {
            tokenSource.Cancel();
            taskbuttonImage.Source = new BitmapImage(new Uri(@"/GTFSApplication;component/Resources/refresh.png", UriKind.RelativeOrAbsolute));
            taskbuttonText.Text = "Aktualizovať databázu";
            taskbutton.Click -= DatabaseCancelUpdate;
            taskbutton.Click += DatabaseUpdate;
        }






        private void button1_Click(object sender, EventArgs e) //that button to start the threads
        {

            datagridDatabaseItems[0].DBTablesRows = "4000";
            datagridDatabaseItems[0].ProgressValue = 70;
            //DBProgressBars=60;


            //dataGridDatabase.Items.Refresh();



        }


        private void button2_Click(object sender, EventArgs e) // finally with that button you can remove all of the threads
        {

        }

        


        class DataGridDatabaseClass : INotifyPropertyChanged
        {


            private string mDBTablesNames { get; set; }

            public string DBTablesNames
            {
                get { return mDBTablesNames; }
                set { mDBTablesNames = value; OnPropChanged("DBTablesNames"); }
            }



            public string mDBTablesRows { get; set; }

            public string DBTablesRows
            {
                get { return mDBTablesRows; }
                set { mDBTablesRows = value; OnPropChanged("DBTablesRows"); }
            }

            private double myVar;

            public double ProgressValue
            {
                get { return myVar; }
                set { myVar = value; OnPropChanged("ProgressValue"); }
            }


            public event PropertyChangedEventHandler PropertyChanged;

            private void OnPropChanged(string propName)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propName));
                }
            }


        }






    }
    }









