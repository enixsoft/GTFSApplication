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




    public class CalendarDates
    {
        //
        //
        //

        /*
                public string service_id { get; set; } //REQUIRED

            Contains an ID that uniquely identifies a set of dates when a service exception is available for one or more routes. 
            Each (service_id, date) pair can only appear once in calendar_dates.txt. 
            If the a service_id value appears in both the calendar.txt and calendar_dates.txt files, 
            the information in calendar_dates.txt modifies the service information specified in calendar.txt. This field is referenced by the trips.txt file.
                
            
                public string date { get; set; } //REQUIRED
            
            Specifies a particular date when service availability is different than the norm. You can use the exception_type field to indicate whether service is available on the specified date.

            The date field's value should be in YYYYMMDD format.

                public string exception_type { get; set; } //REQUIRED
           
            Indicates whether service is available on the date specified in the date field.

            A value of 1 indicates that service has been added for the specified date.
            A value of 2 indicates that service has been removed for the specified date.
            

            */





        public string[] CalendarDatesDataCompare = new string[]
        {
            "service_id",    //REQUIRED 0
            "date", //REQUIRED 1
            "exception_type",  //REQUIRED 2
            

       };

        private string filename = "calendar_dates.txt";
        public int CalendarDatesTotalServices = 0;
        //
        //
        public List<string> CalendarDatesDataList = new List<string>();
        //private List<bool> CalendarDatesDataBool = new List<bool>();
        //private List<bool> CalendarDatesValidDateBool = new List<bool>();  OLD OLD OLD REMOVE

        //
        //

       /* private void CalendarDatesDataCategoryAnalyzer() //ANALYZER AGENCY DATA BOOL BROKEN
        {


            int currentstring = 0;
            int currentcomparestring = 0;



            for (int i = 0; i < 3; i++)
            {
                if (CalendarDatesDataList[currentstring] == CalendarDatesDataCompare[currentcomparestring]) // 
                {
                    CalendarDatesDataBool.Add(true);                                                                                                                                                                              // 
                                                                                                                                                                                                                                  //
                    currentstring++;
                    currentcomparestring++;

                }

                else
                {

                    currentcomparestring++;
                }

            }



                for (int j = 3; j < CalendarDatesDataList.Count; j++)
                      {
                          if (CalendarDatesDataList[j] != null)
                          {
                              CalendarDatesDataBool.Add(true);

                          }
                          else
                      {
                          CalendarDatesDataBool.Add(false);
                      }

                      }

                  
            CalendarDatesTotalServices = CalendarDatesDataList.Count / 3;  

            string[] dateString = new string[CalendarDatesTotalServices];  //8 CHARACTERS CHECK + NUMBERS ONLY CHECK    NEEDS MORE CONTROL for non existing dates *** TO BE IMPLEMENTED ***

            for (int j = 1; j < CalendarDatesTotalServices; j++)
            {

                //CalendarDatesValidDateBool[j] = true; // -----------------------

                if (CalendarDatesDataBool[j-1] == true)
                {
                    CalendarDatesValidDateBool.Add(true);

                    dateString[j] = CalendarDatesDataList[1 + (j * 3)];

                    if (dateString[j].Length != 8)  //YYYYMMDD

                    {
                        CalendarDatesValidDateBool[j-1]=false;
                    }


                    else { 
                    foreach (char c in dateString[j])
                        {
                            if (Char.IsDigit(c) == false)
                            {
                                CalendarDatesValidDateBool[j-1] = false; 
                                break;
                            }

                        }

                    }


                }
               

            }
            

        
        }

                    
    
                

             */        
            


       // private void CalendarDatesDataCategoryWriter()
       // {


            /* for (int currentstring = 10; currentstring < CalendarTotalServices*10; currentstring++)
             {

                 {
                     if (CalendarDataBool[currentstring] != true)
                     { 


                     }

                 }

                 else
                 {

                     // number++;
                 }

             }
             // }
             */

           /* if (CalendarDatesDataBool[0] == false | CalendarDatesDataBool[1] == false | CalendarDatesDataBool[2] == false )
            {
                MessageBox.Show( //TO BE REPLACED BY FORM
                  "CALENDAR DATES ERROR REQUIRED DATA MISSING",
                  "Error",
                 MessageBoxButtons.OK);

            }

            for(int j = 0; j < CalendarDatesValidDateBool.Count; j++)
            {
                if (CalendarDatesValidDateBool[j] == false)
                    
                {
                    MessageBox.Show( //TO BE REPLACED BY FORM
                      "CALENDAR DATES ERROR DATA not VALID",
                      "Error",
                     MessageBoxButtons.OK);

                }
            }

        }
        */

        //Constructor

        //This is the Calendar constructor.
        // public ()
        // { }

        public CalendarDates()
        {
            CalendarDatesReader();
            //CalendarDatesDataCategoryAnalyzer();
            //CalendarDatesDataCategoryWriter();
        }



        private void CalendarDatesReader()
        {

            // Read sample data from CSV file
            if (File.Exists(filename))
            {

                using (CsvFileReader CalendarDatesreader = new CsvFileReader(filename))
                {
                    CsvRow CalendarDatesRow = new CsvRow();
                    while (CalendarDatesreader.ReadRow(CalendarDatesRow))
                    {
                        foreach (string s in CalendarDatesRow)
                        {

                            CalendarDatesDataList.Add(s);


                        }

                    }
                }
            }
            else
            {
                FileStream fs = new FileStream(filename, FileMode.CreateNew);
                fs.Close();


                using (CsvFileReader CalendarDatesreader = new CsvFileReader(filename))
                {
                    CsvRow CalendarDatesRow = new CsvRow();
                    while (CalendarDatesreader.ReadRow(CalendarDatesRow))
                    {
                        foreach (string s in CalendarDatesRow)
                        {

                            CalendarDatesDataList.Add(s);


                        }

                    }
                }

            }

        }

        public void CalendarDatesWriter()
        {

            {
                // Write sample data to CSV file
                using (CsvFileWriter writer = new CsvFileWriter("calendar_dates.txt"))
                {


                    CsvRow RowValues = new CsvRow();

                    for (int i = 0; i < 3; i++)
                    {
                        RowValues.Add(CalendarDatesDataList[i]);
                    }

                    writer.WriteRow(RowValues);

                    CsvRow RowData = new CsvRow();

                    int j = 0;
                    for (int i = 3; i < CalendarDatesDataList.Count; i++)
                    {

                        RowData.Add(CalendarDatesDataList[i]);
                        if (j == 2)
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


