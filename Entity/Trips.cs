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




    public class Trips
    {
        //
        //
        //

       




               //ROUTE ALL DATA CATEGORIES = 9
               
               //
               //
               public List<string> TripsDataList = new List<string>();
        private string filename = "trips.txt";
        //
        //


        //Constructor

        //This is the Trips constructor.
        // public ()
        // { }

        public Trips()
               {
                     TripsReader();

               }


        public string[] TripsDataCompare = new string[]
                  {
                      "route_id",   //req
                      "service_id", //req
                     "trip_id",     //req
                      "trip_headsign", //not implemented
                      "trip_short_name", //not implemented
                      "direction_id", //not implemented
                      "block_id",//opt
                      "shape_id",//opt
                      "wheelchair_accesible",//opt
                      "bikes_allowance" //opt

                };


        public int[] TripsTypesPositions = new int[]
        {
            10,
            10,
            10,
            10,
            10,
            10,
            10,
            10,
            10,
            10
        };

        public bool FileError = false;

        private void TripsReader()
               {
            if (File.Exists(filename))
            {
                // Read sample data from CSV file
                using (CsvFileReader reader = new CsvFileReader("trips.txt"))
                {
                    CsvRow TripsRow = new CsvRow();

                    while (reader.ReadRow(TripsRow))
                    {
                        foreach (string s in TripsRow)
                        {

                            TripsDataList.Add(s);


                        }

                    }


                }
                if(TripsDataList.Count == 0)
                {
                    FileError = true;
                }

            }
            else
            {
                FileStream fs = new FileStream(filename, FileMode.CreateNew);
                fs.Close();

                FileError = true;
            }

            TripsPositions();

        }


       public void TripsWriter()
               {

                   {
                       // Write sample data to CSV file
                       using (CsvFileWriter writer = new CsvFileWriter("trips.txt"))
                       {

                    CsvRow RowValues = new CsvRow();

                    for (int i = 0; i < 10; i++)
                    {
                        RowValues.Add(TripsDataList[i]);
                    }

                    writer.WriteRow(RowValues);

                    CsvRow RowData = new CsvRow();

                    int j = 0;
                    for (int i = 10; i < TripsDataList.Count; i++)
                    {

                        RowData.Add(TripsDataList[i]);
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

        private void TripsPositions()
        {
            if (FileError == false)
            {

                for (int j = 0; j < 10; j++)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        if (TripsDataList[j] == TripsDataCompare[i])
                        {
                            TripsTypesPositions[i] = j;
                        }

                    }
                }


                TripsFileCheck();
            }
        }

        private void TripsFileCheck()
        {
            if (TripsTypesPositions[0] == 10)
            {
                FileError = true;
            }

            if (TripsTypesPositions[1] == 10)
            {
                FileError = true;
            }

            if (TripsTypesPositions[2] == 10)
            {
                FileError = true;
            }

           

        }











    }
}


