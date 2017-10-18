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
    
    

                    
    public class Agency
    {
        //
        //
        /*
        public string agency_id { get; set; }
        public string agency_name { get; set; } //REQUIRED
        public string agency_url { get; set; }   //REQUIRED
        public string agency_timezone { get; set; } //REQUIRED
        public string agency_lang { get; set; }
        public string agency_phone { get; set; }
        public string agency_fare_url { get; set; }
        public string agency_email { get; set; }
       */
        
        private bool[] AgencyDataBool = new bool[8];
        private string filename = "agency.txt";
        public string[] AgencyDataCompare = new string[]
        {
            "agency_id",    //0
            "agency_name", //REQUIRED 1
            "agency_url",  //REQUIRED 2
            "agency_timezone",  //REQUIRED 3
            "agency_lang",  // 4
            "agency_phone", // 5
            "agency_fare_url", // 6
            "agency_email"  // 7

       };


        public List<string> AgencyDataList = new List<string>();

        
        //
        public int DataTypesCount;

        //Constructor

        //This is the Agency constructor.
       // public Agency()
       // { }

        public Agency()
       {
        AgencyReader();
        //AgencyDataCategoryAnalyzer();
        //AgencyDataCategoryWriter();
       }



        private void AgencyReader()
        {
            if (File.Exists(filename))
            {
                // Read sample data from CSV file

                using (CsvFileReader agencyreader = new CsvFileReader(filename))

                {




                    CsvRow agencyRow = new CsvRow();
                    while (agencyreader.ReadRow(agencyRow))
                    {
                        foreach (string s in agencyRow)
                        {

                            AgencyDataList.Add(s);


                        }

                    }
                }
                DataTypesCount = AgencyDataList.Count();
            }
            else
            {
                FileStream fs = new FileStream(filename, FileMode.CreateNew);
                fs.Close();

                using (CsvFileReader agencyreader = new CsvFileReader(filename))

                {




                    CsvRow agencyRow = new CsvRow();
                    while (agencyreader.ReadRow(agencyRow))
                    {
                        foreach (string s in agencyRow)
                        {

                            AgencyDataList.Add(s);


                        }

                    }
                }
                DataTypesCount = AgencyDataList.Count();

            }
            
                

            
        }
        
        

    public void AgencyWriter()//(CsvRow row)

        {
            // Write sample data to CSV file
            using (CsvFileWriter writer = new CsvFileWriter("agency.txt"))
            {
                /*
                //----------------------------------------------- WORKING
                 CsvRow AgencyRow = new CsvRow();

                 foreach (string s in AgencyDataList)
                 {
                     AgencyRow.Add(s);
                 }


                     writer.WriteRow(AgencyRow);
                //---------------------------------------------- WORKING 
                */

                CsvRow AgencyRowValues = new CsvRow();

                for(int i=0; i<8;i++)
                {
                    AgencyRowValues.Add(AgencyDataList[i]);
                }

                writer.WriteRow(AgencyRowValues);

                CsvRow AgencyRowData = new CsvRow();

                for (int i = 8; i < AgencyDataList.Count; i++)
                {
                    AgencyRowData.Add(AgencyDataList[i]);
                }
                writer.WriteRow(AgencyRowData);

            }

        }









    }
}

