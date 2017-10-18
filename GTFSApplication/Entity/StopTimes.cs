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




    public class StopTimes
    {
        //
        //
        //






        //ROUTE ALL DATA CATEGORIES = 9

        //
        //
        public List<string> StopTimesDataList = new List<string>();
        private string filename = "stop_times.txt";
        //
        //


        //Constructor

        //This is the StopTimes constructor.
        // public ()
        // { }

        public StopTimes()
        {
            StopTimesReader();

        }

        public string[] StopTimesDataCompare = new string[]
        {
        "trip_id",
        "arrival_time",
        "departure_time",
        "stop_id",
        "stop_sequence",
        "stop_headsign",
        "pickup_type",
        "drop_off_type",
        "shape_dist_traveled",
        "timepoint"
        };

      

        public int[] StopTimesTypesPositions = new int[]
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












        private void StopTimesReader()
        {
            if (File.Exists(filename))
            {
                // Read sample data from CSV file
                using (CsvFileReader reader = new CsvFileReader(filename))
                {
                    CsvRow Row = new CsvRow();

                    while (reader.ReadRow(Row))
                    {
                        foreach (string s in Row)
                        {

                            StopTimesDataList.Add(s);


                        }

                    }


                }

                if(StopTimesDataList.Count==0)
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
           
            StopTimesPositions();
        }


        public void StopTimesWriter()
        {

            {
                // Write sample data to CSV file
                using (CsvFileWriter writer = new CsvFileWriter("stop_times.txt"))
                {
                    

                    CsvRow RowValues = new CsvRow();

                    for (int i = 0; i < 10; i++)
                    {
                        RowValues.Add(StopTimesDataList[i]);
                    }

                    writer.WriteRow(RowValues);

                    CsvRow RowData = new CsvRow();

                    int j = 0;
                    for (int i = 10; i < StopTimesDataList.Count; i++)
                    {

                        RowData.Add(StopTimesDataList[i]);
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


        private void StopTimesPositions()
        {
            if (FileError == false)
            {

                for (int j = 0; j < 10; j++)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        if (StopTimesDataList[j] == StopTimesDataCompare[i])
                        {
                            StopTimesTypesPositions[i] = j;
                        }

                    }
                }


                StopTimesFileCheck();
            }
        }

         private void StopTimesFileCheck()
        {
            if (StopTimesTypesPositions[0] == 10)
            {
                FileError = true;
            }

            if (StopTimesTypesPositions[1] == 10)
            {
                FileError = true;
            }

            if (StopTimesTypesPositions[2] == 10)
            {
                FileError = true;
            }

            if (StopTimesTypesPositions[3] == 10)
            {
                FileError = true;
            }

            if (StopTimesTypesPositions[4] == 10)
            {
                FileError = true;
            }


        }







    }
}


