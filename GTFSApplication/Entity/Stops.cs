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




    public class Stops
    {
        //
        //
        //






        //ROUTE ALL DATA CATEGORIES = 9

        //
        //
        public List<string> StopsDataList = new List<string>();
        private string filename = "stops.txt";
        //
        //


        //Constructor

        //This is the Trips constructor.
        // public ()
        // { }

        public Stops()
        {
            StopsReader();

        }


        public string[] StopsDataCompare = new string[]
        {
            "stop_id",
            "stop_code",
            "stop_name",
            "stop_desc",
            "stop_lat",
            "stop_lon",
            "zone_id",
            "stop_url",
            "location_type",
            "parent_station",
            "stop_timezone",
            "wheelchair_boarding"
        };

        public int[] StopsTypesPositions = new int[]
{
            12,
            12,
            12,
            12,
            12,
            12,
            12,
            12,
            12,
            12,
            12,
            12
};

        public bool FileError = false;



        private void StopsReader()
        {
            if (File.Exists(filename))
            {
                // Read sample data from CSV file
                using (CsvFileReader reader = new CsvFileReader(filename))
                {
                    CsvRow StopsRow = new CsvRow();

                    while (reader.ReadRow(StopsRow))
                    {
                        foreach (string s in StopsRow)
                        {

                            StopsDataList.Add(s);


                        }

                    }


                }
                if(StopsDataList.Count ==0)
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
            StopsPositions();
        }


        public void StopsWriter()
        {

            {
                // Write sample data to CSV file
                using (CsvFileWriter writer = new CsvFileWriter("stops.txt"))
                {
                    /*
                    //----------------------------------------------- WORKING EXAMPLE
                     CsvRow AgencyRow = new CsvRow();

                     foreach (string s in AgencyDataList)
                     {
                         AgencyRow.Add(s);
                     }


                         writer.WriteRow(AgencyRow);
                    //---------------------------------------------- WORKING EXAMPLE
                    */

                    CsvRow StopsRowValues = new CsvRow();

                    for (int i = 0; i < 12; i++)
                    {
                        StopsRowValues.Add(StopsDataList[i]);
                    }

                    writer.WriteRow(StopsRowValues);

                    CsvRow StopsRowData = new CsvRow();

                    for (int i = 12; i < StopsDataList.Count; i++)
                    {
                        StopsRowData.Add(StopsDataList[i]);
                    }
                    writer.WriteRow(StopsRowData);



                }

            }

        }


       private void StopsPositions()
        {
            if (FileError == false)
            {

                for (int j = 0; j < 12; j++)
                {
                    for (int i = 0; i < 12; i++)
                    {
                        if (StopsDataList[j] == StopsDataCompare[i])
                        {
                            StopsTypesPositions[i] = j;
                        }

                    }
                }


                StopsFileCheck();
            }
        }

        private void StopsFileCheck()
        {
            if (StopsTypesPositions[0] == 12)
            {
                FileError = true;
            }

            if (StopsTypesPositions[2] == 12)
            {
                FileError = true;
            }

            if (StopsTypesPositions[6] == 12)
            {
                FileError = true;
            }
        }








    }
}


