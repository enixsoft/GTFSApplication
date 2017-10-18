using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;

namespace GTFS.Entities
{

    // 




    public class Calendar
    {
        //
        //
        //
      
/*
        public string service_id { get; set; } //REQUIRED
        public string monday { get; set; } //REQUIRED
        public string tuesday { get; set; } //REQUIRED
        public string wednesday { get; set; } //REQUIRED
        public string thursday { get; set; }   //REQUIRED
        public string friday { get; set; } //REQUIRED
        public string saturday { get; set; } //REQUIRED
        public string sunday { get; set; } //REQUIRED
        public string start_date { get; set; }//REQUIRED
        public string end_date { get; set; } //REQUIRED

  
    */


     
     
        
        public string[] CalendarDataCompare = new string[]
        {
            "service_id",    //REQ 0
            "monday", //REQUIRED 1
            "tuesday",  //REQUIRED 2
            "wednesday",  //REQUIRED 3
            "thursday",  //REQ 4
            "friday", //REQ 5
            "saturday", //REQ 6
            "sunday",  //REQ 7
            "start_date", //REQ 8
            "end_date" //REQ 9

       };
       
      
        public int DataTypesCount; 
        //
        //
        public List<string> CalendarDataList = new List<string>();
        private List<bool> CalendarDataBool = new List<bool>();
        private string filename = "calendar.txt";
        //
        //


        //Constructor

        //This is the Calendar constructor.
        // public ()
        // { }

        public Calendar()
        {
            CalendarReader();
          
        }



        private void CalendarReader()
        {

            // Read sample data from CSV file
            if (File.Exists(filename))
            { 
                using (CsvFileReader Calendarreader = new CsvFileReader(filename))
                {
                    CsvRow CalendarRow = new CsvRow();
                    while (Calendarreader.ReadRow(CalendarRow))
                    {
                        foreach (string s in CalendarRow)
                        {

                            CalendarDataList.Add(s);


                        }

                    }


                }
                DataTypesCount = CalendarDataList.Count();
            }
            else
            {
                FileStream fs = new FileStream(filename, FileMode.CreateNew);
                fs.Close();

                using (CsvFileReader Calendarreader = new CsvFileReader(filename))
                {
                    CsvRow CalendarRow = new CsvRow();
                    while (Calendarreader.ReadRow(CalendarRow))
                    {
                        foreach (string s in CalendarRow)
                        {

                            CalendarDataList.Add(s);


                        }

                    }


                }
                DataTypesCount = CalendarDataList.Count();

            }
        }


        public void CalendarWriter()
        {

            {
                // Write sample data to CSV file
                using (CsvFileWriter writer = new CsvFileWriter("calendar.txt"))
                {

                    CsvRow RowValues = new CsvRow();

                    for (int i = 0; i < 10; i++)
                    {
                        RowValues.Add(CalendarDataList[i]);
                    }

                    writer.WriteRow(RowValues);

                    CsvRow RowData = new CsvRow();

                    int j = 0;
                    for (int i = 10; i < CalendarDataList.Count; i++)
                    {

                        RowData.Add(CalendarDataList[i]);
                        if (j == 9)
                        {
                            RowData.Add("");
                            writer.WriteRow(RowData);
                            RowData.Clear();
                            j = 0;

                        }
                        else
                        {
                            j++;
                        }

                    }





                }

            }

        }












    }
}

   
      