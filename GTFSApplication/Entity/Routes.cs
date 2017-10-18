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




    public class Routes
    {
        //
        //
        //






        //ROUTE ALL DATA CATEGORIES = 9

        //
        //
        public List<string> RoutesDataList = new List<string>();
        private string filename = "routes.txt";

        //
        //


        //Constructor

        //This is the Routes constructor.
        // public ()
        // { }

        public Routes()
        {
            RoutesReader();

        }


        public string[] RoutesDataCompare = new string[]
           {
               "route_id",
               "agency_id",
               "route_short_name",
               "route_long_name",
               "route_desc",
               "route_type",
               "route_url",
              "route_color",
              "route_text_color"

          };

        public int[] RoutesTypesPositions = new int[]
        {
        9,
        9,
        9,
        9,
        9,
        9,        
        9,
        9,
        9
       };


        // int RouteIdPos = 0;       //
        //  int AgencyIdPos = 1;
        //     int RouteShortNamePos = 2;
        //      int RouteLongNamePos = 3;
        //    int RouteDescPos = 4;
        //      int RouteTypePos = 5;
        //      int RouteUrlPos = 6;
        //     int RouteColorPos = 7;
        //     int RouteTextColor = 8;

        public bool FileError = false;




    private void RoutesReader()
        {

            // Read sample data from CSV file
            if (File.Exists(filename))
            {

                using (CsvFileReader Routesrreader = new CsvFileReader(filename))
                {
                    CsvRow RoutesRow = new CsvRow();
                    while (Routesrreader.ReadRow(RoutesRow))
                    {
                        foreach (string s in RoutesRow)
                        {

                            RoutesDataList.Add(s);


                        }

                    }


                }

                if(RoutesDataList.Count==0)
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

            RoutesPositions();

        }
        

     public void RoutesWriter()
               {

                   {
                       // Write sample data to CSV file
                       using (CsvFileWriter writer = new CsvFileWriter("routes.txt"))
                       {
                           
                            
                    CsvRow RowValues = new CsvRow();

                    for (int i = 0; i < 9; i++)
                    {
                        RowValues.Add(RoutesDataList[i]);
                    }

                    writer.WriteRow(RowValues);

                    CsvRow RowData = new CsvRow();

                    int j = 0;
                    for (int i = 9; i < RoutesDataList.Count; i++)
                    {

                        RowData.Add(RoutesDataList[i]);
                        if (j == 8)
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

        private void RoutesPositions()
        {

            if (FileError == false)
            {


                for (int j = 0; j < 9; j++)
                {
                    for (int i = 0; i < 9; i++)
                    {
                        if (RoutesDataList[j] == RoutesDataCompare[i])
                        {
                            RoutesTypesPositions[i] = j;
                        }

                    }
                }


                RoutesFileCheck();
            }
        }

        private void RoutesFileCheck()
        {
            
                if(RoutesTypesPositions[0]==9)
                {
                    FileError = true;
                }

                if (RoutesTypesPositions[2] == 9)
                {
                    FileError = true;
                }

                if (RoutesTypesPositions[3] == 9)
                {
                    FileError = true;
                }

                if (RoutesTypesPositions[5] == 9)
                {
                    FileError = true;
                }







       }

     











    }
}


