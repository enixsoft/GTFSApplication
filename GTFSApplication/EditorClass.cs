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

    public partial class EditorClass
    {
        public EditorClass()
        {
            //InitializeComponent();
            //WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            //AgencyLoader();
            //dataGridLoader();
            // CalendarLoader();

            dataGridLoader();


        }




        //LOADING FILES VIA GTFS ENTITIES CLASSES

        GTFS.Entities.Agency AgencyLoadFromFile = new GTFS.Entities.Agency();
        GTFS.Entities.Calendar CalendarLoadFromFile = new GTFS.Entities.Calendar();
        GTFS.Entities.CalendarDates CalendarDatesLoadFromFile = new GTFS.Entities.CalendarDates();
        GTFS.Entities.Routes RoutesLoadFromFile = new GTFS.Entities.Routes();
        GTFS.Entities.Trips TripsLoadFromFile = new GTFS.Entities.Trips();
        GTFS.Entities.Stops StopsLoadFromFile = new GTFS.Entities.Stops();
        GTFS.Entities.StopTimes StopTimesLoadFromFile = new GTFS.Entities.StopTimes();

        //int ActiveEntity;

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



        public List<AgencyValues> agencyItems = new List<AgencyValues>();
        public List<CalendarValues> calendarItems = new List<CalendarValues>();
        public List<CalendarDatesValues> calendarDatesItems = new List<CalendarDatesValues>();
        public List<RoutesValues> routesItems = new List<RoutesValues>();
        public List<TripsValues> tripsItems = new List<TripsValues>();
        public List<StopsValues> stopsItems = new List<StopsValues>();
        public List<StopTimesValues> stopTimesItems = new List<StopTimesValues>();





        private void dataGridLoader()
        {
            AgencyLoader();
            CalendarLoader();
            CalendarDatesLoader();
            RoutesLoader();
            TripsLoader();
            StopsLoader();
            StopTimesLoader();


        }

        public void dataGridRefresher(int choice)
        {
            switch (choice)
            {
                case 0:
                    agencyItems.Clear();
                    AgencyLoader();
                    break;
                case 1:
                    calendarItems.Clear();
                    CalendarLoader();
                    break;
                case 2:
                    calendarDatesItems.Clear();
                    CalendarDatesLoader();
                    break;
                case 3:
                    routesItems.Clear();
                    RoutesLoader();
                    break;
                case 4:
                    tripsItems.Clear();
                    TripsLoader();
                    break;
                case 5:
                    stopsItems.Clear();
                    StopsLoader();
                    break;
                case 6:
                    stopTimesItems.Clear();
                    StopTimesLoader();
                    break;






            }





        }





        // LOADING DATA FOR DATAGRID


        private void AgencyLoader()
        {
            if (AgencyLoadFromFile.AgencyDataList.Count == 0)
            {
                for (int i = 0; i < 8; i++)
                {
                    AgencyLoadFromFile.AgencyDataList.Add(AgencyLoadFromFile.AgencyDataCompare[i]);

                }

                for (int i = 8; i < 16; i++)
                {
                    AgencyLoadFromFile.AgencyDataList.Add("");

                }


            }




            AgencyallEntriesCount = AgencyLoadFromFile.AgencyDataList.Count; //POCET RIADKOV
            AgencydataRowsCount = AgencyallEntriesCount - 8;
            AgencyFullCategoryRepeatTimes = AgencydataRowsCount / 8;
            AgencyTotalRows = (AgencyFullCategoryRepeatTimes * 8) + (AgencydataRowsCount % 8);

          

            if (AgencyFullCategoryRepeatTimes >= 1)
            {
                for (int i = 0; i < 8; i++)
                {
                    agencyItems.Add(new AgencyValues() { AgencyKategoria = AgencyLoadFromFile.AgencyDataCompare[i], AgencyData = AgencyLoadFromFile.AgencyDataList[8 + i] });

                }
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    agencyItems.Add(new AgencyValues() { AgencyKategoria = AgencyLoadFromFile.AgencyDataCompare[i], AgencyData = "" });

                }
            }

        }

        private void CalendarLoader()
        {
            //dataGrid.Columns.Clear();


            Style yesStyle = new Style();
            yesStyle = (Style)Application.Current.Resources["YesButtonImageStyle"];

            Style noStyle = new Style();
            noStyle = (Style)Application.Current.Resources["NoButtonImageStyle"];

            Style qStyle = new Style();
            qStyle = (Style)Application.Current.Resources["UnknownButtonImageStyle"];

            DateTime tempStartDateTime = new DateTime(2017, 1, 1);
            DateTime tempStartDate = tempStartDateTime.Date;

            DateTime tempEndDateTime = new DateTime(2017, 12, 31);
            DateTime tempEndDate = tempEndDateTime.Date;



            if (CalendarLoadFromFile.CalendarDataList.Count == 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    CalendarLoadFromFile.CalendarDataList.Add(CalendarLoadFromFile.CalendarDataCompare[i]);

                }

               



            }


            CalendarallEntriesCount = CalendarLoadFromFile.CalendarDataList.Count;
            CalendardataCellsCount = CalendarallEntriesCount - 10;
            CalendarFullCategoryRepeatTimes = CalendardataCellsCount / 10;
            CalendarTotalCells = (CalendarFullCategoryRepeatTimes * 10) + (CalendardataCellsCount % 10);
            int loadedCell = 0;


            for (int i = 1; i <= CalendarFullCategoryRepeatTimes; i++)
            {
                calendarItems.Add(new CalendarValues()
                {
                    CalendarServiceid = "",
                    CalendarMondayStyle = qStyle,
                    CalendarTuesdayStyle = qStyle,
                    CalendarWednesdayStyle = qStyle,
                    CalendarThursdayStyle = qStyle,
                    CalendarFridayStyle = qStyle,
                    CalendarSaturdayStyle = qStyle,
                    CalendarSundayStyle = qStyle,
                    CalendarStartDate = tempStartDate,
                    CalendarEndDate = tempEndDate
                });


            }

            //MAIN LOADER FULL ROW 

            for (int i = 0; i < CalendarFullCategoryRepeatTimes; i++)
            {


                calendarItems[i].CalendarServiceid = CalendarLoadFromFile.CalendarDataList[10 * (i + 1)];


                if (CalendarLoadFromFile.CalendarDataList[((i + 1) * 10) + 1] == "0") //MON
                {
                    calendarItems[i].CalendarMondayStyle = noStyle;
                }
                else
                {
                    calendarItems[i].CalendarMondayStyle = yesStyle;
                }

                if (CalendarLoadFromFile.CalendarDataList[((i + 1) * 10) + 2] == "0")
                {
                    calendarItems[i].CalendarTuesdayStyle = noStyle;

                }
                else
                {
                    calendarItems[i].CalendarTuesdayStyle = yesStyle;
                }


                if (CalendarLoadFromFile.CalendarDataList[((i + 1) * 10) + 3] == "0")
                {
                    calendarItems[i].CalendarWednesdayStyle = noStyle;

                }
                else
                {
                    calendarItems[i].CalendarWednesdayStyle = yesStyle;
                }

                if (CalendarLoadFromFile.CalendarDataList[((i + 1) * 10) + 4] == "0")
                {
                    calendarItems[i].CalendarThursdayStyle = noStyle;

                }
                else
                {
                    calendarItems[i].CalendarThursdayStyle = yesStyle;
                }

                if (CalendarLoadFromFile.CalendarDataList[((i + 1) * 10) + 5] == "0")
                {
                    calendarItems[i].CalendarFridayStyle = noStyle;

                }
                else
                {
                    calendarItems[i].CalendarFridayStyle = yesStyle;
                }

                if (CalendarLoadFromFile.CalendarDataList[((i + 1) * 10) + 6] == "0")
                {
                    calendarItems[i].CalendarSaturdayStyle = noStyle;

                }
                else
                {
                    calendarItems[i].CalendarSaturdayStyle = yesStyle;
                }

                if (CalendarLoadFromFile.CalendarDataList[((i + 1) * 10) + 7] == "0")
                {
                    calendarItems[i].CalendarSundayStyle = noStyle;
                }
                else
                {
                    calendarItems[i].CalendarSundayStyle = yesStyle;
                }

                //----------------StartDate & EndDate parsing

                calendarItems[i].CalendarStartDate = DateTime.ParseExact(CalendarLoadFromFile.CalendarDataList[((i + 1) * 10) + 8],
                                  "yyyyMMdd",
                                   null);

                calendarItems[i].CalendarEndDate = DateTime.ParseExact(CalendarLoadFromFile.CalendarDataList[((i + 1) * 10) + 9],
                                "yyyyMMdd",
                                 null);


                // date 18 date 19  10-19 total cells for row

                loadedCell = loadedCell + 10;




            }


            //REMAINING CELLS
            /*
            if (loadedCell != CalendardataCellsCount)
            {
                calendarItems.Add(new CalendarValues()
                {
                    CalendarServiceid = "",
                    CalendarMondayStyle = qStyle,
                    CalendarTuesdayStyle = qStyle,
                    CalendarWednesdayStyle = qStyle,
                    CalendarThursdayStyle = qStyle,
                    CalendarFridayStyle = qStyle,
                    CalendarSaturdayStyle = qStyle,
                    CalendarSundayStyle = qStyle,
                    CalendarStartDate = tempStartDate,
                    CalendarEndDate = tempEndDate
                });



                for (int i = 0; i < CalendardataCellsCount - loadedCell; i++)
                {
                    if (i == 0)
                    {
                        calendarItems[CalendarFullCategoryRepeatTimes].CalendarServiceid = CalendarLoadFromFile.CalendarDataList[loadedCell + 10];
                    }

                    if (i == 1)
                    {
                        if (CalendarLoadFromFile.CalendarDataList[loadedCell + 11] == "0")

                        {
                            calendarItems[CalendarFullCategoryRepeatTimes].CalendarMondayStyle = noStyle;

                        }

                        else
                        {
                            calendarItems[CalendarFullCategoryRepeatTimes].CalendarMondayStyle = yesStyle;
                        }
                    }

                    if (i == 2)
                    {
                        if (CalendarLoadFromFile.CalendarDataList[loadedCell + 12] == "0")

                        {
                            calendarItems[CalendarFullCategoryRepeatTimes].CalendarTuesdayStyle = noStyle;

                        }

                        else
                        {
                            calendarItems[CalendarFullCategoryRepeatTimes].CalendarTuesdayStyle = yesStyle;
                        }
                    }

                    if (i == 3)
                    {
                        if (CalendarLoadFromFile.CalendarDataList[loadedCell + 13] == "0")

                        {
                            calendarItems[CalendarFullCategoryRepeatTimes].CalendarWednesdayStyle = noStyle;

                        }
                        else
                        {
                            calendarItems[CalendarFullCategoryRepeatTimes].CalendarWednesdayStyle = yesStyle;
                        }
                    }

                    if (i == 4)
                    {
                        if (CalendarLoadFromFile.CalendarDataList[loadedCell + 14] == "0")

                        {
                            calendarItems[CalendarFullCategoryRepeatTimes].CalendarThursdayStyle = noStyle;

                        }
                        else
                        {
                            calendarItems[CalendarFullCategoryRepeatTimes].CalendarThursdayStyle = yesStyle;
                        }
                    }

                    if (i == 5)
                    {
                        if (CalendarLoadFromFile.CalendarDataList[loadedCell + 15] == "0")

                        {
                            calendarItems[CalendarFullCategoryRepeatTimes].CalendarFridayStyle = noStyle;

                        }
                        else
                        {
                            calendarItems[CalendarFullCategoryRepeatTimes].CalendarFridayStyle = yesStyle;
                        }
                    }

                    if (i == 6)
                    {
                        if (CalendarLoadFromFile.CalendarDataList[loadedCell + 16] == "0")

                        {
                            calendarItems[CalendarFullCategoryRepeatTimes].CalendarSaturdayStyle = noStyle;

                        }
                        else
                        {
                            calendarItems[CalendarFullCategoryRepeatTimes].CalendarSaturdayStyle = yesStyle;
                        }
                    }


                    if (i == 7)
                    {
                        if (CalendarLoadFromFile.CalendarDataList[loadedCell + 17] == "0")

                        {
                            calendarItems[CalendarFullCategoryRepeatTimes].CalendarSundayStyle = noStyle;

                        }
                        else
                        {
                            calendarItems[CalendarFullCategoryRepeatTimes].CalendarSundayStyle = yesStyle;
                        }
                    }

                    //if (i == 8) a loadedCell + 18 este tento datum dopracovat, keby tam je 9 tak uz je full category a tento doplnac sa nespusti 

                    if (i == 8)
                    {
                        calendarItems[CalendarFullCategoryRepeatTimes].CalendarStartDate = DateTime.ParseExact(CalendarLoadFromFile.CalendarDataList[loadedCell + 18],
                                    "yyyyMMdd",
                                     null);
                    }


                }


            }
            */


        }

        private void CalendarDatesLoader()
        {
            Style yesStyle = new Style();
            yesStyle = (Style)Application.Current.Resources["YesButtonImageStyle"];

            Style noStyle = new Style();
            noStyle = (Style)Application.Current.Resources["NoButtonImageStyle"];

            Style qStyle = new Style();
            qStyle = (Style)Application.Current.Resources["UnknownButtonImageStyle"];

            DateTime tempDateTime = new DateTime(2017, 1, 1);
            DateTime tempDate = tempDateTime.Date;


      



            if (CalendarDatesLoadFromFile.CalendarDatesDataList.Count == 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    CalendarDatesLoadFromFile.CalendarDatesDataList.Add(CalendarDatesLoadFromFile.CalendarDatesDataCompare[i]);

                }                              
               






            }



            CalendarDatesallEntriesCount = CalendarDatesLoadFromFile.CalendarDatesDataList.Count;
            CalendarDatesdataCellsCount = CalendarDatesallEntriesCount - 3;
            CalendarDatesFullCategoryRepeatTimes = CalendarDatesdataCellsCount / 3;
            CalendarDatesTotalCells = (CalendarDatesFullCategoryRepeatTimes * 3) + (CalendardataCellsCount % 3);
            int loadedCell = 0;


         


            for (int i = 1; i <= CalendarDatesFullCategoryRepeatTimes; i++)
            {
                calendarDatesItems.Add(new CalendarDatesValues()
                {
                    CalendarDatesServiceid = "",
                    CalendarDatesDate = tempDate,
                    CalendarDatesExceptionType = qStyle,
                    CalendarDatesServiceIdFromCalendar=new List<string>()
                });

                calendarDatesItems[i-1].CalendarDatesServiceIdFromCalendar.Add("");


            }

            //MAIN LOADER FULL ROW 

            for (int i = 0; i < CalendarDatesFullCategoryRepeatTimes; i++)
            {


                calendarDatesItems[i].CalendarDatesServiceid = CalendarDatesLoadFromFile.CalendarDatesDataList[3 * (i + 1)];
                calendarDatesItems[i].CalendarDatesServiceIdFromCalendar[0]=CalendarDatesLoadFromFile.CalendarDatesDataList[3 * (i + 1)];



                //----------------Date Parsing


                calendarDatesItems[i].CalendarDatesDate = DateTime.ParseExact(CalendarDatesLoadFromFile.CalendarDatesDataList[((i + 1) * 3) + 1],
                                "yyyyMMdd",
                                 null);

                //-----------Exception Type Parsing
                if (CalendarDatesLoadFromFile.CalendarDatesDataList[((i + 1) * 3) + 2] == "2")
                {
                    calendarDatesItems[i].CalendarDatesExceptionType = noStyle;
                }
                else
                {
                    calendarDatesItems[i].CalendarDatesExceptionType = yesStyle;
                }


                // 3-6 total cells for row

                loadedCell = loadedCell + 3;




            }


            //REMAINING CELLS
            /*
            if (loadedCell != CalendarDatesdataCellsCount)
            {
                calendarDatesItems.Add(new CalendarDatesValues()
                {
                    CalendarDatesServiceid = "",
                    CalendarDatesDate = tempDate,
                    CalendarDatesExceptionType = qStyle,
                    CalendarDatesServiceIdFromCalendar = new List<string>()
                });





                for (int i = 0; i < CalendarDatesdataCellsCount - loadedCell; i++)
                {
                    if (i == 0)
                    {
                        calendarDatesItems[CalendarDatesFullCategoryRepeatTimes].CalendarDatesServiceid = CalendarDatesLoadFromFile.CalendarDatesDataList[loadedCell + 3];
                        calendarDatesItems[i].CalendarDatesServiceIdFromCalendar.Add(CalendarDatesLoadFromFile.CalendarDatesDataList[loadedCell + 3]);

                    }

                    if (i == 1)
                    {
                        calendarDatesItems[CalendarDatesFullCategoryRepeatTimes].CalendarDatesDate = DateTime.ParseExact(CalendarDatesLoadFromFile.CalendarDatesDataList[loadedCell + 4],
                                    "yyyyMMdd",
                                     null);

                    }


                    //keby tam je 2 tak uz je full category a tento doplnac sa nespusti 




                }


            }
            */
        }

        private void RoutesLoader()
        {

            Style qStyle = (Style)Application.Current.Resources["UnknownButtonImageStyle"];

            Style Route0Style = (Style)Application.Current.Resources["Route0ImageStyle"];
            Style Route1Style = (Style)Application.Current.Resources["Route1ImageStyle"];
            Style Route2Style = (Style)Application.Current.Resources["Route2ImageStyle"];
            Style Route3Style = (Style)Application.Current.Resources["Route3ImageStyle"];
            Style Route4Style = (Style)Application.Current.Resources["Route4ImageStyle"];
            Style Route5Style = (Style)Application.Current.Resources["Route5ImageStyle"];
            Style Route6Style = (Style)Application.Current.Resources["Route6ImageStyle"];
            Style Route7Style = (Style)Application.Current.Resources["Route7ImageStyle"];
            Style Route11Style = (Style)Application.Current.Resources["Route11ImageStyle"];



            if (RoutesLoadFromFile.FileError==true)
            {
                RoutesLoadFromFile.RoutesDataList.Clear();
              
                for(int i=0;i<9;i++)
                {
                    RoutesLoadFromFile.RoutesTypesPositions[i] = i;
                }

                RoutesLoadFromFile.FileError = false;

            }

            
            if (RoutesLoadFromFile.RoutesDataList.Count == 0)
            {
                for (int i = 0; i < 9; i++)
                {
                    RoutesLoadFromFile.RoutesDataList.Add(RoutesLoadFromFile.RoutesDataCompare[i]);

                }            



            }
            
            RoutesDataTypes = 0;

            for (int i=0; i<9; i++)
            {
                if(RoutesLoadFromFile.RoutesTypesPositions[i]!=9)
                {
                    RoutesDataTypes++;
                }
            }

                      
            RoutesallEntriesCount = RoutesLoadFromFile.RoutesDataList.Count;




            RoutesdataCellsCount = RoutesallEntriesCount - RoutesDataTypes;
            RoutesFullCategoryRepeatTimes = RoutesdataCellsCount / RoutesDataTypes;
            RoutesTotalCells = (RoutesFullCategoryRepeatTimes * RoutesDataTypes) + (RoutesdataCellsCount % RoutesDataTypes);

            int loadedCell = 0;


            for (int i = 1; i <= RoutesFullCategoryRepeatTimes; i++)
            {
                routesItems.Add(new RoutesValues()
                {
                    Routeid = "",
                    RouteAgencyid = "",
                    RouteShortName = "",
                    RouteLongName = "",
                    RouteDesc = "",
                    RouteType = qStyle,
                    RouteUrl = "",
                    RouteColor = "",
                    RouteColorText = ""

                });


            }

            //MAIN LOADER FULL ROW 

            for (int i = 0; i < RoutesFullCategoryRepeatTimes; i++)
            {


                //routesItems[i].Routeid = RoutesLoadFromFile.RoutesDataList[9 * (i + 1)];
                routesItems[i].Routeid = RoutesLoadFromFile.RoutesDataList[RoutesDataTypes * (i + 1) + (RoutesLoadFromFile.RoutesTypesPositions[0])];


                //----------------


                //routesItems[i].RouteShortName = RoutesLoadFromFile.RoutesDataList[((i + 1) * 9) + 2];
                routesItems[i].RouteShortName = RoutesLoadFromFile.RoutesDataList[((i + 1) * RoutesDataTypes) + (RoutesLoadFromFile.RoutesTypesPositions[2])];
                //routesItems[i].RouteLongName = RoutesLoadFromFile.RoutesDataList[((i + 1) * 9) + 3];
                routesItems[i].RouteLongName = RoutesLoadFromFile.RoutesDataList[((i + 1) * RoutesDataTypes) + (RoutesLoadFromFile.RoutesTypesPositions[3])];

                /*
                  //-----------RouteType Parsing
                if (RoutesLoadFromFile.RoutesDataList[((i + 1) * 9) + 5] == "0")
                {
                    routesItems[i].RouteType = Route0Style;
                }
                else if (RoutesLoadFromFile.RoutesDataList[((i + 1) * 9) + 5] == "1")
                {
                    routesItems[i].RouteType = Route1Style;
                }
                else if (RoutesLoadFromFile.RoutesDataList[((i + 1) * 9) + 5] == "2")
                {
                    routesItems[i].RouteType = Route2Style;
                }
                else if (RoutesLoadFromFile.RoutesDataList[((i + 1) * 9) + 5] == "3")
                {
                    routesItems[i].RouteType = Route3Style;
                }
                else if (RoutesLoadFromFile.RoutesDataList[((i + 1) * 9) + 5] == "4")
                {
                    routesItems[i].RouteType = Route4Style;
                }
                else if (RoutesLoadFromFile.RoutesDataList[((i + 1) * 9) + 5] == "5")
                {
                    routesItems[i].RouteType = Route5Style;
                }
                else if (RoutesLoadFromFile.RoutesDataList[((i + 1) * 9) + 5] == "6")
                {
                    routesItems[i].RouteType = Route6Style;
                }
                else if (RoutesLoadFromFile.RoutesDataList[((i + 1) * 9) + 5] == "7")
                {
                    routesItems[i].RouteType = Route7Style;
                }
                else
                {
                    routesItems[i].RouteType = qStyle;
                }
                */
                //-----------RouteType Parsing

                if (RoutesLoadFromFile.RoutesDataList[((i + 1) * RoutesDataTypes) + (RoutesLoadFromFile.RoutesTypesPositions[5])] == "0")
                {
                    routesItems[i].RouteType = Route0Style;
                }
                else if (RoutesLoadFromFile.RoutesDataList[((i + 1) * RoutesDataTypes) + (RoutesLoadFromFile.RoutesTypesPositions[5])] == "1")
                {
                    routesItems[i].RouteType = Route1Style;
                }
                else if (RoutesLoadFromFile.RoutesDataList[((i + 1) * RoutesDataTypes) + (RoutesLoadFromFile.RoutesTypesPositions[5])] == "2")
                {
                    routesItems[i].RouteType = Route2Style;
                }
                else if (RoutesLoadFromFile.RoutesDataList[((i + 1) * RoutesDataTypes) + (RoutesLoadFromFile.RoutesTypesPositions[5])] == "3")
                {
                    routesItems[i].RouteType = Route3Style;
                }
                else if (RoutesLoadFromFile.RoutesDataList[((i + 1) * RoutesDataTypes) + (RoutesLoadFromFile.RoutesTypesPositions[5])] == "4")
                {
                    routesItems[i].RouteType = Route4Style;
                }
                else if (RoutesLoadFromFile.RoutesDataList[((i + 1) * RoutesDataTypes) + (RoutesLoadFromFile.RoutesTypesPositions[5])] == "5")
                {
                    routesItems[i].RouteType = Route5Style;
                }
                else if (RoutesLoadFromFile.RoutesDataList[((i + 1) * RoutesDataTypes) + (RoutesLoadFromFile.RoutesTypesPositions[5])] == "6")
                {
                    routesItems[i].RouteType = Route6Style;
                }
                else if (RoutesLoadFromFile.RoutesDataList[((i + 1) * RoutesDataTypes) + (RoutesLoadFromFile.RoutesTypesPositions[5])] == "7")
                {
                    routesItems[i].RouteType = Route7Style;
                }
                else if (RoutesLoadFromFile.RoutesDataList[((i + 1) * RoutesDataTypes) + (RoutesLoadFromFile.RoutesTypesPositions[5])] == "11")
                {
                    routesItems[i].RouteType = Route11Style;
                }
                else
                {
                    routesItems[i].RouteType = qStyle;
                }




                // 9 total cells for row, 4 used, 5 skipped

                loadedCell = loadedCell + RoutesDataTypes;




            }


            //REMAINING CELLS
            /*

            if (loadedCell != RoutesdataCellsCount)
            {
                routesItems.Add(new RoutesValues()
                {
                    Routeid = "",
                    RouteAgencyid = "",
                    RouteShortName = "NO DATA",
                    RouteLongName = "NO DATA",
                    RouteDesc = "",
                    RouteType = qStyle,
                    RouteUrl = "",
                    RouteColor = "",
                    RouteColorText = ""
                });





                for (int i = 0; i < RoutesdataCellsCount - loadedCell; i++)
                {
                    if (i == 0)
                    {
                        routesItems[RoutesFullCategoryRepeatTimes].Routeid = RoutesLoadFromFile.RoutesDataList[loadedCell + 9];

                    }

                    if (i == 2)
                    {
                        routesItems[RoutesFullCategoryRepeatTimes].RouteShortName = RoutesLoadFromFile.RoutesDataList[loadedCell + 11];
                    }

                    if (i == 3)
                    {
                        routesItems[RoutesFullCategoryRepeatTimes].RouteLongName = RoutesLoadFromFile.RoutesDataList[loadedCell + 12];
                    }

                    if (i == 5)
                    {
                        if (RoutesLoadFromFile.RoutesDataList[loadedCell + 14] == "0")
                        {
                            routesItems[RoutesFullCategoryRepeatTimes].RouteType = Route0Style;
                        }
                        else if (RoutesLoadFromFile.RoutesDataList[loadedCell + 14] == "1")
                        {
                            routesItems[RoutesFullCategoryRepeatTimes].RouteType = Route1Style;
                        }
                        else if (RoutesLoadFromFile.RoutesDataList[loadedCell + 14] == "2")
                        {
                            routesItems[RoutesFullCategoryRepeatTimes].RouteType = Route2Style;
                        }
                        else if (RoutesLoadFromFile.RoutesDataList[loadedCell + 14] == "3")
                        {
                            routesItems[RoutesFullCategoryRepeatTimes].RouteType = Route3Style;
                        }
                        else if (RoutesLoadFromFile.RoutesDataList[loadedCell + 14] == "4")
                        {
                            routesItems[RoutesFullCategoryRepeatTimes].RouteType = Route4Style;
                        }
                        else if (RoutesLoadFromFile.RoutesDataList[loadedCell + 14] == "5")
                        {
                            routesItems[RoutesFullCategoryRepeatTimes].RouteType = Route5Style;
                        }
                        else if (RoutesLoadFromFile.RoutesDataList[loadedCell + 14] == "6")
                        {
                            routesItems[RoutesFullCategoryRepeatTimes].RouteType = Route6Style;
                        }
                        else if (RoutesLoadFromFile.RoutesDataList[loadedCell + 14] == "7")
                        {
                            routesItems[RoutesFullCategoryRepeatTimes].RouteType = Route7Style;
                        }
                        else
                        {
                            routesItems[RoutesFullCategoryRepeatTimes].RouteType = qStyle;
                        }


                    }


            
                    




                }


            }

                        */

           
        }

        private void TripsLoader()
        {


            if (TripsLoadFromFile.FileError == true)
            {
                TripsLoadFromFile.TripsDataList.Clear();

                for (int i = 0; i < 10; i++)
                {
                    TripsLoadFromFile.TripsTypesPositions[i] = i;
                }

                TripsLoadFromFile.FileError = false;

            }




            if (TripsLoadFromFile.TripsDataList.Count == 0)
            {
                TripsLoadFromFile.TripsDataList.Add("route_id");
                TripsLoadFromFile.TripsDataList.Add("service_id");
                TripsLoadFromFile.TripsDataList.Add("trip_id");
                TripsLoadFromFile.TripsDataList.Add("trip_headsign");
                TripsLoadFromFile.TripsDataList.Add("trip_short_name");
                TripsLoadFromFile.TripsDataList.Add("direction_id");
                TripsLoadFromFile.TripsDataList.Add("block_id");
                TripsLoadFromFile.TripsDataList.Add("shape_id");
                TripsLoadFromFile.TripsDataList.Add("wheelchair_accesible");
                TripsLoadFromFile.TripsDataList.Add("bikes_allowed");              

            }


            TripsDataTypes = 0;

            for (int i = 0; i < 10; i++)
            {
                if (TripsLoadFromFile.TripsTypesPositions[i] != 10)
                {
                    TripsDataTypes++;
                }
            }

            TripsallEntriesCount = TripsLoadFromFile.TripsDataList.Count;
            TripsdataCellsCount = TripsallEntriesCount - TripsDataTypes;
            TripsFullCategoryRepeatTimes = TripsdataCellsCount / TripsDataTypes;
            TripsTotalCells = (TripsFullCategoryRepeatTimes * TripsDataTypes) + (TripsdataCellsCount % TripsDataTypes);
            int loadedCell = 0;


            for (int i = 1; i <= TripsFullCategoryRepeatTimes; i++)
            {
                tripsItems.Add(new TripsValues()
                {
                    TripsRouteid = "",
                    TripsServiceid = "",
                    TripsTripid = " ",
                    TripsHeadsign = "",
                    TripsShortName = "",
                    TripsDirectionid = "",
                    TripsBlockid = "",
                    TripsShapeid = "",
                    TripsWheelchair = "",
                    TripsBikes = ""

                });


            }

            //MAIN LOADER FULL ROW 

            for (int i = 0; i < TripsFullCategoryRepeatTimes; i++)
            {

           
                tripsItems[i].TripsRouteid = TripsLoadFromFile.TripsDataList[TripsDataTypes * (i + 1)+ (TripsLoadFromFile.TripsTypesPositions[0])];

                tripsItems[i].TripsServiceid = TripsLoadFromFile.TripsDataList[((i + 1) * TripsDataTypes) + (TripsLoadFromFile.TripsTypesPositions[1])];

                tripsItems[i].TripsTripid = TripsLoadFromFile.TripsDataList[((i + 1) * TripsDataTypes) + (TripsLoadFromFile.TripsTypesPositions[2])];

                tripsItems[i].TripsHeadsign = TripsLoadFromFile.TripsDataList[((i + 1) * TripsDataTypes) + (TripsLoadFromFile.TripsTypesPositions[3])];

                //tripsItems[i].TripsShortName = TripsLoadFromFile.TripsDataList[((i + 1) * TripsDataTypes) + (TripsLoadFromFile.TripsTypesPositions[4])];

                tripsItems[i].TripsDirectionid = TripsLoadFromFile.TripsDataList[((i + 1) * TripsDataTypes) + (TripsLoadFromFile.TripsTypesPositions[5])];







                // 10 total cells for row, 3 used, 7 skipped

                loadedCell = loadedCell + TripsDataTypes;




            }


            //REMAINING CELLS

            /*
            if (loadedCell != TripsdataCellsCount)
            {
                tripsItems.Add(new TripsValues()
                {
                    TripsRouteid = "",
                    TripsServiceid = "",
                    TripsTripid = "",
                    TripsHeadsign = "",
                    TripsShortName = "",
                    TripsDirectionid = "",
                    TripsBlockid = "",
                    TripsShapeid = "",
                    TripsWheelchair = "",
                    TripsBikes = ""
                });





                for (int i = 0; i < TripsdataCellsCount - loadedCell; i++)
                {
                    if (i == 0)
                    {
                        tripsItems[TripsFullCategoryRepeatTimes].TripsRouteid = TripsLoadFromFile.TripsDataList[loadedCell + 10];

                    }

                    if (i == 1)
                    {
                        tripsItems[TripsFullCategoryRepeatTimes].TripsServiceid = TripsLoadFromFile.TripsDataList[loadedCell + 11];
                    }



                    if (i == 3)
                    {
                        tripsItems[TripsFullCategoryRepeatTimes].TripsTripid = TripsLoadFromFile.TripsDataList[loadedCell + 12];
                    }









                }


            }
                */
        }

        private void StopsLoader()
        {

            if (StopsLoadFromFile.FileError == true)
            {
                StopsLoadFromFile.StopsDataList.Clear();

                for (int i = 0; i < 12; i++)
                {
                    StopsLoadFromFile.StopsTypesPositions[i] = i;
                }

                StopsLoadFromFile.FileError = false;
            }





                if (StopsLoadFromFile.StopsDataList.Count == 0)
            {
                StopsLoadFromFile.StopsDataList.Add("stop_id");
                StopsLoadFromFile.StopsDataList.Add("stop_code");
                StopsLoadFromFile.StopsDataList.Add("stop_name");
                StopsLoadFromFile.StopsDataList.Add("stop_desc");
                StopsLoadFromFile.StopsDataList.Add("stop_lat");
                StopsLoadFromFile.StopsDataList.Add("stop_lon");
                StopsLoadFromFile.StopsDataList.Add("zone_id");
                StopsLoadFromFile.StopsDataList.Add("stop_url");
                StopsLoadFromFile.StopsDataList.Add("location_type");
                StopsLoadFromFile.StopsDataList.Add("parent_station");
                StopsLoadFromFile.StopsDataList.Add("stop_timezone");
                StopsLoadFromFile.StopsDataList.Add("wheelchair_boarding");
               

            }

                StopsDataTypes = 0;

                for (int i = 0; i < 12; i++)
                {
                    if (StopsLoadFromFile.StopsTypesPositions[i] != 12)
                    {
                        StopsDataTypes++;
                    }
                }



            StopsallEntriesCount = StopsLoadFromFile.StopsDataList.Count;
            StopsdataCellsCount = StopsallEntriesCount - StopsDataTypes;
            StopsFullCategoryRepeatTimes = StopsdataCellsCount / StopsDataTypes;
            StopsTotalCells = (StopsFullCategoryRepeatTimes * StopsDataTypes) + (StopsdataCellsCount % StopsDataTypes);
            int loadedCell = 0;


            for (int i = 1; i <= StopsFullCategoryRepeatTimes; i++)
            {
                stopsItems.Add(new StopsValues()
                {
                    Stopsid = "",
                    StopsCode = "",
                    StopsName = " ",
                    StopsDesc = "",
                    StopsLat = "",
                    StopsLon = "",
                    StopsZoneid = "",
                    StopsUrl = "",
                    StopsLocationType = "",
                    StopsParentStation = "",
                    StopsTimezone = "",
                    StopsWheelChair = ""
                });


            }

            //MAIN LOADER FULL ROW 

            for (int i = 0; i < StopsFullCategoryRepeatTimes; i++)
            {


                stopsItems[i].Stopsid = StopsLoadFromFile.StopsDataList[StopsDataTypes * (i + 1) + (StopsLoadFromFile.StopsTypesPositions[0])];

                stopsItems[i].StopsName = StopsLoadFromFile.StopsDataList[((i + 1) * StopsDataTypes) + (StopsLoadFromFile.StopsTypesPositions[2])];
                
                stopsItems[i].StopsZoneid = StopsLoadFromFile.StopsDataList[((i + 1) * StopsDataTypes) + (StopsLoadFromFile.StopsTypesPositions[6])];










                    // 12 total cells for row, 2 used, 10 skipped

                    loadedCell = loadedCell + StopsDataTypes;




            }


            //REMAINING CELLS
            
            /*
            if (loadedCell != StopsdataCellsCount)
            {
                stopsItems.Add(new StopsValues()
                {
                    Stopsid = "",
                    StopsCode = "",
                    StopsName = " ",
                    StopsDesc = "",
                    StopsLat = "",
                    StopsLon = "",
                    StopsZoneid = "",
                    StopsUrl = "",
                    StopsLocationType = "",
                    StopsParentStation = "",
                    StopsTimezone = "",
                    StopsWheelChair = ""
                });





                for (int i = 0; i < StopsdataCellsCount - loadedCell; i++)
                {
                    if (i == 0)
                    {
                        stopsItems[StopsFullCategoryRepeatTimes].Stopsid = StopsLoadFromFile.StopsDataList[loadedCell + 12];

                    }

                    if (i == 1)
                    {
                        stopsItems[StopsFullCategoryRepeatTimes].StopsName = StopsLoadFromFile.StopsDataList[loadedCell + 14];
                    }











                }
            }

                */
        }

        private void StopTimesLoader()
        {

            if (StopTimesLoadFromFile.FileError == true)
            {
                StopTimesLoadFromFile.StopTimesDataList.Clear();

                for (int i = 0; i < 10; i++)
                {
                    StopTimesLoadFromFile.StopTimesTypesPositions[i] = i;
                }

                StopTimesLoadFromFile.FileError = false;
            }





            if (StopTimesLoadFromFile.StopTimesDataList.Count == 0)
            {
                StopTimesLoadFromFile.StopTimesDataList.Add("trip_id");
                StopTimesLoadFromFile.StopTimesDataList.Add("arrival_time");
                StopTimesLoadFromFile.StopTimesDataList.Add("departure_time");
                StopTimesLoadFromFile.StopTimesDataList.Add("stop_id");
                StopTimesLoadFromFile.StopTimesDataList.Add("stop_sequence");
                StopTimesLoadFromFile.StopTimesDataList.Add("stop_headsign");
                StopTimesLoadFromFile.StopTimesDataList.Add("pickup_type");
                StopTimesLoadFromFile.StopTimesDataList.Add("drop_off_type");
                StopTimesLoadFromFile.StopTimesDataList.Add("shape_dist_traveled");
                StopTimesLoadFromFile.StopTimesDataList.Add("timepoint");

            }

            StopTimesDataTypes = 0;


            for (int i = 0; i < 10; i++)
            {
                if (StopTimesLoadFromFile.StopTimesTypesPositions[i] != 10)
                {
                    StopTimesDataTypes++;
                }
            }




            StopTimesallEntriesCount = StopTimesLoadFromFile.StopTimesDataList.Count;
            StopTimesdataCellsCount = StopTimesallEntriesCount - StopTimesDataTypes;
            StopTimesFullCategoryRepeatTimes = StopTimesdataCellsCount / StopTimesDataTypes;
            StopTimesTotalCells = (StopTimesFullCategoryRepeatTimes * StopTimesDataTypes) + (StopTimesdataCellsCount % StopTimesDataTypes);
            int loadedCell = 0;


            for (int i = 1; i <= StopTimesFullCategoryRepeatTimes; i++)
            {
                stopTimesItems.Add(new StopTimesValues()
                {
                    StopTimesTripid = "",
                    StopTimesArrivalTimeHours = "00",
                    StopTimesArrivalTimeMinutes = "00",
                    StopTimesArrivalTimeSeconds = "00",
                    StopTimesDepartureTimeHours = "00",
                    StopTimesDepartureTimeMinutes = "00",
                    StopTimesDepartureTimeSeconds = "00",
                    StopTimesStopsid = "",
                    StopTimesSequence = "",
                    StopTimesHeadsign = "",
                    StopTimesPickupType = "",
                    StopTimesDropoffType = "",
                    StopTimesShape = "",
                    StopTimesTimepoint = ""

                });


            }

            string Time = null;
            string TimeHours = null;
            string TimeHoursFirstZeroRemoval;
            string TimeMinutes = null;
            string TimeSeconds = null;

            //MAIN LOADER FULL ROW 

            for (int i = 0; i < StopTimesFullCategoryRepeatTimes; i++)
            {


                stopTimesItems[i].StopTimesTripid = StopTimesLoadFromFile.StopTimesDataList[StopTimesDataTypes * (i + 1)+(StopTimesLoadFromFile.StopTimesTypesPositions[0])];

                if (StopTimesLoadFromFile.StopTimesDataList[((i + 1) * StopTimesDataTypes) + 1].Length == 8 | StopTimesLoadFromFile.StopTimesDataList[((i + 1) * StopTimesDataTypes) + (StopTimesLoadFromFile.StopTimesTypesPositions[1])].Length == 7)
                {
                    Time = StopTimesLoadFromFile.StopTimesDataList[((i + 1) * StopTimesDataTypes) + (StopTimesLoadFromFile.StopTimesTypesPositions[1])];

                    if (StopTimesLoadFromFile.StopTimesDataList[((i + 1) * StopTimesDataTypes) + (StopTimesLoadFromFile.StopTimesTypesPositions[1])].Length == 8)
                    {
                        TimeHours = Time.Substring(0, 2);

                        if (TimeHours != "")
                        {
                            if (IsDigitsOnly(TimeHours) == true)
                            {
                                TimeHoursFirstZeroRemoval = TimeHours.Substring(0, 1);
                                if (TimeHoursFirstZeroRemoval == "0")
                                {
                                    stopTimesItems[i].StopTimesArrivalTimeHours = TimeHours.Substring(1, 1);
                                }
                                else
                                { stopTimesItems[i].StopTimesArrivalTimeHours = TimeHours; }


                            }

                        }

                        TimeMinutes = Time.Substring(3, 2);


                        if (TimeMinutes != "")
                        {
                            if (IsDigitsOnly(TimeMinutes) == true)
                            {
                                stopTimesItems[i].StopTimesArrivalTimeMinutes = TimeMinutes;
                            }

                        }

                        TimeSeconds = Time.Substring(6, 2);


                        if (TimeSeconds != null)
                        {
                            if (IsDigitsOnly(TimeSeconds) == true)
                            {
                                stopTimesItems[i].StopTimesArrivalTimeSeconds = TimeSeconds;
                            }

                        }

                    }
                    else
                    {
                        TimeHours = Time.Substring(0, 1);

                        if (TimeHours != null)
                        {
                            if (IsDigitsOnly(TimeHours) == true)
                            {
                                stopTimesItems[i].StopTimesArrivalTimeHours = TimeHours;
                            }

                        }

                        TimeMinutes = Time.Substring(2, 2);


                        if (TimeMinutes != null)
                        {
                            if (IsDigitsOnly(TimeMinutes) == true)
                            {
                                stopTimesItems[i].StopTimesArrivalTimeMinutes = TimeMinutes;
                            }

                        }

                        TimeSeconds = Time.Substring(5, 2);

                        if (TimeSeconds != null)
                        {
                            if (IsDigitsOnly(TimeSeconds) == true)
                            {
                                stopTimesItems[i].StopTimesArrivalTimeSeconds = TimeSeconds;
                            }

                        }
                    }
                }

                if (StopTimesLoadFromFile.StopTimesDataList[((i + 1) * StopTimesDataTypes) + (StopTimesLoadFromFile.StopTimesTypesPositions[2])].Length == 8 | StopTimesLoadFromFile.StopTimesDataList[((i + 1) * StopTimesDataTypes) + (StopTimesLoadFromFile.StopTimesTypesPositions[2])].Length == 7)
                {
                    Time = StopTimesLoadFromFile.StopTimesDataList[((i + 1) * StopTimesDataTypes) + (StopTimesLoadFromFile.StopTimesTypesPositions[2])];

                    if (StopTimesLoadFromFile.StopTimesDataList[((i + 1) * StopTimesDataTypes) + (StopTimesLoadFromFile.StopTimesTypesPositions[2])].Length == 8)
                    {
                        TimeHours = Time.Substring(0, 2);

                        if (TimeHours != "")
                        {
                            if (IsDigitsOnly(TimeHours) == true)
                            {
                                TimeHoursFirstZeroRemoval = TimeHours.Substring(0, 1);
                                if (TimeHoursFirstZeroRemoval == "0")
                                {
                                    stopTimesItems[i].StopTimesDepartureTimeHours = TimeHours.Substring(1, 1);
                                }
                                else
                                { stopTimesItems[i].StopTimesDepartureTimeHours = TimeHours; }


                            }

                        }

                        TimeMinutes = Time.Substring(3, 2);


                        if (TimeMinutes != "")
                        {
                            if (IsDigitsOnly(TimeMinutes) == true)
                            {
                                stopTimesItems[i].StopTimesDepartureTimeMinutes = TimeMinutes;
                            }

                        }

                        TimeSeconds = Time.Substring(6, 2);


                        if (TimeSeconds != "")
                        {
                            if (IsDigitsOnly(TimeSeconds) == true)
                            {
                                stopTimesItems[i].StopTimesDepartureTimeSeconds = TimeSeconds;
                            }

                        }

                    }
                    else
                    {
                        TimeHours = Time.Substring(0, 1);

                        if (TimeHours != null)
                        {
                            if (IsDigitsOnly(TimeHours) == true)
                            {
                                stopTimesItems[i].StopTimesDepartureTimeHours = TimeHours;
                            }

                        }

                        TimeMinutes = Time.Substring(2, 2);


                        if (TimeMinutes != null)
                        {
                            if (IsDigitsOnly(TimeMinutes) == true)
                            {
                                stopTimesItems[i].StopTimesDepartureTimeMinutes = TimeMinutes;
                            }

                        }

                        TimeSeconds = Time.Substring(5, 2);

                        if (TimeSeconds != null)
                        {
                            if (IsDigitsOnly(TimeSeconds) == true)
                            {
                                stopTimesItems[i].StopTimesDepartureTimeSeconds = TimeSeconds;
                            }

                        }
                    }
                }




                stopTimesItems[i].StopTimesStopsid = StopTimesLoadFromFile.StopTimesDataList[((i + 1) * StopTimesDataTypes) + (StopTimesLoadFromFile.StopTimesTypesPositions[3])];

                stopTimesItems[i].StopTimesSequence = StopTimesLoadFromFile.StopTimesDataList[((i + 1) * StopTimesDataTypes) + (StopTimesLoadFromFile.StopTimesTypesPositions[4])];

                


















                // 10 total cells for row, 5 used, 5 skipped

                loadedCell = loadedCell + StopTimesDataTypes;




            }

         


            /*
            //REMAINING CELLS
            if (loadedCell != StopTimesdataCellsCount)
            {
                stopTimesItems.Add(new StopTimesValues()
                {
                    StopTimesTripid = "",
                    StopTimesArrivalTimeHours = "00",
                    StopTimesArrivalTimeMinutes = "00",
                    StopTimesArrivalTimeSeconds = "00",
                    StopTimesDepartureTimeHours = "00",
                    StopTimesDepartureTimeMinutes = "00",
                    StopTimesDepartureTimeSeconds = "00",
                    StopTimesStopsid = "",
                    StopTimesSequence = "",
                    StopTimesHeadsign = "",
                    StopTimesPickupType = "",
                    StopTimesDropoffType = "",
                    StopTimesShape = "",
                    StopTimesTimepoint = ""
                });





                for (int i = 0; i < StopTimesdataCellsCount - loadedCell; i++)
                {
                    if (i == 0)
                    {
                        stopTimesItems[StopTimesFullCategoryRepeatTimes].StopTimesTripid = StopTimesLoadFromFile.StopTimesDataList[loadedCell + 10];

                    }

                    if (i == 1)
                    {

                        if (StopTimesLoadFromFile.StopTimesDataList[loadedCell + 11].Length == 8 | StopTimesLoadFromFile.StopTimesDataList[loadedCell + 11].Length == 7)
                        {
                            Time = StopTimesLoadFromFile.StopTimesDataList[loadedCell + 11];

                            if (Time.Length == 8)
                            {
                                TimeHours = Time.Substring(0, 2);

                                if (TimeHours != "")
                                {
                                    if (IsDigitsOnly(TimeHours) == true)
                                    {
                                        TimeHoursFirstZeroRemoval = TimeHours.Substring(0, 1);
                                        if (TimeHoursFirstZeroRemoval == "0")
                                        {
                                            stopTimesItems[StopTimesFullCategoryRepeatTimes].StopTimesArrivalTimeHours = TimeHours.Substring(1, 1);
                                        }
                                        else
                                        { stopTimesItems[StopTimesFullCategoryRepeatTimes].StopTimesArrivalTimeHours = TimeHours; }


                                    }

                                }

                                TimeMinutes = Time.Substring(3, 2);


                                if (TimeMinutes != "")
                                {
                                    if (IsDigitsOnly(TimeMinutes) == true)
                                    {
                                        stopTimesItems[StopTimesFullCategoryRepeatTimes].StopTimesArrivalTimeMinutes = TimeMinutes;
                                    }

                                }

                                TimeSeconds = Time.Substring(6, 2);


                                if (TimeSeconds != null)
                                {
                                    if (IsDigitsOnly(TimeSeconds) == true)
                                    {
                                        stopTimesItems[StopTimesFullCategoryRepeatTimes].StopTimesArrivalTimeSeconds = TimeSeconds;
                                    }

                                }

                            }
                            else
                            {
                                TimeHours = Time.Substring(0, 1);

                                if (TimeHours != null)
                                {
                                    if (IsDigitsOnly(TimeHours) == true)
                                    {
                                        stopTimesItems[StopTimesFullCategoryRepeatTimes].StopTimesArrivalTimeHours = TimeHours;
                                    }

                                }

                                TimeMinutes = Time.Substring(2, 2);


                                if (TimeMinutes != null)
                                {
                                    if (IsDigitsOnly(TimeMinutes) == true)
                                    {
                                        stopTimesItems[StopTimesFullCategoryRepeatTimes].StopTimesArrivalTimeMinutes = TimeMinutes;
                                    }

                                }

                                TimeSeconds = Time.Substring(5, 2);

                                if (TimeSeconds != null)
                                {
                                    if (IsDigitsOnly(TimeSeconds) == true)
                                    {
                                        stopTimesItems[StopTimesFullCategoryRepeatTimes].StopTimesArrivalTimeSeconds = TimeSeconds;
                                    }

                                }
                            }
                        }

                    }

                    if (i == 2)
                    {
                        if (StopTimesLoadFromFile.StopTimesDataList[loadedCell + 12].Length == 8 | StopTimesLoadFromFile.StopTimesDataList[loadedCell + 12].Length == 7)
                        {
                            Time = StopTimesLoadFromFile.StopTimesDataList[loadedCell + 12];

                            if (Time.Length == 8)
                            {
                                TimeHours = Time.Substring(0, 2);

                                if (TimeHours != "")
                                {
                                    if (IsDigitsOnly(TimeHours) == true)
                                    {
                                        TimeHoursFirstZeroRemoval = TimeHours.Substring(0, 1);
                                        if (TimeHoursFirstZeroRemoval == "0")
                                        {
                                            stopTimesItems[StopTimesFullCategoryRepeatTimes].StopTimesDepartureTimeHours = TimeHours.Substring(1, 1);
                                        }
                                        else
                                        { stopTimesItems[StopTimesFullCategoryRepeatTimes].StopTimesDepartureTimeHours = TimeHours; }


                                    }

                                }

                                TimeMinutes = Time.Substring(3, 2);


                                if (TimeMinutes != "")
                                {
                                    if (IsDigitsOnly(TimeMinutes) == true)
                                    {
                                        stopTimesItems[StopTimesFullCategoryRepeatTimes].StopTimesDepartureTimeMinutes = TimeMinutes;
                                    }

                                }

                                TimeSeconds = Time.Substring(6, 2);


                                if (TimeSeconds != null)
                                {
                                    if (IsDigitsOnly(TimeSeconds) == true)
                                    {
                                        stopTimesItems[StopTimesFullCategoryRepeatTimes].StopTimesDepartureTimeSeconds = TimeSeconds;
                                    }

                                }

                            }
                            else
                            {
                                TimeHours = Time.Substring(0, 1);

                                if (TimeHours != null)
                                {
                                    if (IsDigitsOnly(TimeHours) == true)
                                    {
                                        stopTimesItems[StopTimesFullCategoryRepeatTimes].StopTimesDepartureTimeHours = TimeHours;
                                    }

                                }

                                TimeMinutes = Time.Substring(2, 2);


                                if (TimeMinutes != null)
                                {
                                    if (IsDigitsOnly(TimeMinutes) == true)
                                    {
                                        stopTimesItems[StopTimesFullCategoryRepeatTimes].StopTimesDepartureTimeMinutes = TimeMinutes;
                                    }

                                }

                                TimeSeconds = Time.Substring(5, 2);

                                if (TimeSeconds != null)
                                {
                                    if (IsDigitsOnly(TimeSeconds) == true)
                                    {
                                        stopTimesItems[StopTimesFullCategoryRepeatTimes].StopTimesDepartureTimeSeconds = TimeSeconds;
                                    }

                                }
                            }
                        }

                    }

                    if (i == 3)
                    {
                        stopTimesItems[StopTimesFullCategoryRepeatTimes].StopTimesStopsid = StopTimesLoadFromFile.StopTimesDataList[loadedCell + 13];
                    }

                    if (i == 4)
                    {
                        stopTimesItems[StopTimesFullCategoryRepeatTimes].StopTimesSequence = StopTimesLoadFromFile.StopTimesDataList[loadedCell + 14];
                    }
















                }
            }
            */
        }

        // SAVING DATA FROM DATAGRID
        private void AgencySaver() //simple save
        {

            AgencyLoadFromFile.AgencyDataList.Clear();
            for (int i = 0; i < 8; i++)
            {
                AgencyLoadFromFile.AgencyDataList.Add(AgencyLoadFromFile.AgencyDataCompare[i]);

            }



            for (int i = 0; i < agencyItems.Count; i++)
            {
                AgencyLoadFromFile.AgencyDataList.Add("");
                AgencyLoadFromFile.AgencyDataList[8 + i] = agencyItems[i].AgencyData;
            }

        }

        private void CalendarSaver()
        {

            Style styleYes = new Style();
            styleYes = (Style)Application.Current.Resources["YesButtonImageStyle"];

            CalendarLoadFromFile.CalendarDataList.Clear();

            for (int i = 0; i < 10; i++)
            {
                CalendarLoadFromFile.CalendarDataList.Add(CalendarLoadFromFile.CalendarDataCompare[i]);

            }

            int calendarRowsToSave = calendarItems.Count;
            int calendarCellToSave = 10;


            /*if ((CalendarLoadFromFile.CalendarDataList.Count - 10) != (calendarItems.Count * 10))
            {
                int i = CalendarLoadFromFile.CalendarDataList.Count - 10;
                int j = calendarItems.Count * 10;
                string s = "";
                for (int c = i; c < j; c++)

                {
                    CalendarLoadFromFile.CalendarDataList.Add(s);
                }
            }
            */

            for (int i = 0; i < calendarRowsToSave; i++)
            {

                for (int j = 0; j < 10; j++)
                {
                    CalendarLoadFromFile.CalendarDataList.Add("");

                    switch (j)
                    {
                        case 0:
                            {
                                CalendarLoadFromFile.CalendarDataList[calendarCellToSave] = calendarItems[i].CalendarServiceid;
                                calendarCellToSave++;
                            }
                            break;
                        case 1:
                            {
                                if (calendarItems[i].CalendarMondayStyle == styleYes)
                                {
                                    CalendarLoadFromFile.CalendarDataList[calendarCellToSave] = "1";
                                }
                                else
                                {
                                    CalendarLoadFromFile.CalendarDataList[calendarCellToSave] = "0";
                                }
                                calendarCellToSave++;
                            }
                            break;
                        case 2:
                            {
                                if (calendarItems[i].CalendarTuesdayStyle == styleYes)
                                {
                                    CalendarLoadFromFile.CalendarDataList[calendarCellToSave] = "1";
                                }
                                else
                                {
                                    CalendarLoadFromFile.CalendarDataList[calendarCellToSave] = "0";
                                }
                                calendarCellToSave++;
                            }
                            break;
                        case 3:
                            {
                                if (calendarItems[i].CalendarWednesdayStyle == styleYes)
                                {
                                    CalendarLoadFromFile.CalendarDataList[calendarCellToSave] = "1";
                                }
                                else
                                {
                                    CalendarLoadFromFile.CalendarDataList[calendarCellToSave] = "0";
                                }
                                calendarCellToSave++;
                            }
                            break;
                        case 4:
                            {
                                if (calendarItems[i].CalendarThursdayStyle == styleYes)
                                {
                                    CalendarLoadFromFile.CalendarDataList[calendarCellToSave] = "1";
                                }
                                else
                                {
                                    CalendarLoadFromFile.CalendarDataList[calendarCellToSave] = "0";
                                }
                                calendarCellToSave++;
                            }
                            break;

                        case 5:
                            {
                                if (calendarItems[i].CalendarFridayStyle == styleYes)
                                {
                                    CalendarLoadFromFile.CalendarDataList[calendarCellToSave] = "1";
                                }
                                else
                                {
                                    CalendarLoadFromFile.CalendarDataList[calendarCellToSave] = "0";
                                }
                                calendarCellToSave++;
                            }
                            break;
                        case 6:
                            {
                                if (calendarItems[i].CalendarSaturdayStyle == styleYes)
                                {
                                    CalendarLoadFromFile.CalendarDataList[calendarCellToSave] = "1";
                                }
                                else
                                {
                                    CalendarLoadFromFile.CalendarDataList[calendarCellToSave] = "0";
                                }
                                calendarCellToSave++;
                            }
                            break;
                        case 7:
                            {
                                if (calendarItems[i].CalendarSundayStyle == styleYes)
                                {
                                    CalendarLoadFromFile.CalendarDataList[calendarCellToSave] = "1";
                                }
                                else
                                {
                                    CalendarLoadFromFile.CalendarDataList[calendarCellToSave] = "0";
                                }
                                calendarCellToSave++;
                            }
                            break;
                        case 8:
                            {

                                CalendarLoadFromFile.CalendarDataList[calendarCellToSave] = calendarItems[i].CalendarStartDate.ToString("yyyyMMdd");
                                calendarCellToSave++;
                            }
                            break;
                        case 9:
                            {

                                CalendarLoadFromFile.CalendarDataList[calendarCellToSave] = calendarItems[i].CalendarEndDate.ToString("yyyyMMdd");
                                calendarCellToSave++;
                            }
                            break;



                    }


               
                }

            }

        }

        private void CalendarDatesSaver()
        {
            Style styleYes = new Style();
            styleYes = (Style)Application.Current.Resources["YesButtonImageStyle"];

            CalendarDatesLoadFromFile.CalendarDatesDataList.Clear();

            for (int i = 0; i < 3; i++)
            {
                CalendarDatesLoadFromFile.CalendarDatesDataList.Add(CalendarDatesLoadFromFile.CalendarDatesDataCompare[i]);

            }

            int calendarDatesRowsToSave = calendarDatesItems.Count;
            int calendarDatesCellToSave = 3;
            /*
            if ((CalendarDatesLoadFromFile.CalendarDatesDataList.Count - 3) != (calendarDatesItems.Count * 3))
            {
                int i = CalendarDatesLoadFromFile.CalendarDatesDataList.Count - 3;
                int j = calendarDatesItems.Count * 3;
                string s = "";
                for (int c = i; c < j; c++)

                {
                    CalendarDatesLoadFromFile.CalendarDatesDataList.Add(s);
                }
            }
            */

            for (int i = 0; i < calendarDatesRowsToSave; i++)
            {

                for (int j = 0; j < 3; j++)
                {
                    CalendarDatesLoadFromFile.CalendarDatesDataList.Add("");
                    switch (j)
                    {
                        case 0:
                            {
                                CalendarDatesLoadFromFile.CalendarDatesDataList[calendarDatesCellToSave] = calendarDatesItems[i].CalendarDatesServiceid;
                                calendarDatesCellToSave++;
                            }
                            break;
                        case 1:
                            {
                                CalendarDatesLoadFromFile.CalendarDatesDataList[calendarDatesCellToSave] = calendarDatesItems[i].CalendarDatesDate.ToString("yyyyMMdd");

                                calendarDatesCellToSave++;
                            }
                            break;
                        case 2:
                            {
                                if (calendarDatesItems[i].CalendarDatesExceptionType == styleYes)
                                {
                                    CalendarDatesLoadFromFile.CalendarDatesDataList[calendarDatesCellToSave] = "1";
                                }
                                else
                                {
                                    CalendarDatesLoadFromFile.CalendarDatesDataList[calendarDatesCellToSave] = "2";
                                }
                                calendarDatesCellToSave++;
                            }
                            break;

                    }


                    //CalendarLoadFromFile.CalendarDataList[10 + i] = calendarItems[i].Count;
                }

            }

        }

        private void RoutesSaver()
        {


            Style qStyle = (Style)Application.Current.Resources["UnknownButtonImageStyle"];

            Style Route0Style = (Style)Application.Current.Resources["Route0ImageStyle"];
            Style Route1Style = (Style)Application.Current.Resources["Route1ImageStyle"];
            Style Route2Style = (Style)Application.Current.Resources["Route2ImageStyle"];
            Style Route3Style = (Style)Application.Current.Resources["Route3ImageStyle"];
            Style Route4Style = (Style)Application.Current.Resources["Route4ImageStyle"];
            Style Route5Style = (Style)Application.Current.Resources["Route5ImageStyle"];
            Style Route6Style = (Style)Application.Current.Resources["Route6ImageStyle"];
            Style Route7Style = (Style)Application.Current.Resources["Route7ImageStyle"];


            RoutesLoadFromFile.RoutesDataList.Clear();

            for (int i = 0; i < 9; i++)
            {
                RoutesLoadFromFile.RoutesDataList.Add(RoutesLoadFromFile.RoutesDataCompare[i]);

            }

            int routesRowsToSave = routesItems.Count;
            int routesCellToSave = 9;


            /*
            if ((RoutesLoadFromFile.RoutesDataList.Count - 9) != (routesItems.Count * 9))
            {
                int i = RoutesLoadFromFile.RoutesDataList.Count - 9;
                int j = routesItems.Count * 9;
                string s = "";
                for (int c = i; c < j; c++)

                {
                    RoutesLoadFromFile.RoutesDataList.Add(s);
                }
            }
            */
            for (int i = 0; i < routesRowsToSave; i++)
            {

                for (int j = 0; j < 9; j++)
                {
                    RoutesLoadFromFile.RoutesDataList.Add("");

                    switch (j)
                    {
                        case 0:
                            {
                                RoutesLoadFromFile.RoutesDataList[routesCellToSave] = routesItems[i].Routeid;
                                routesCellToSave++;
                            }
                            break;
                        case 1:
                            {
                                RoutesLoadFromFile.RoutesDataList[routesCellToSave] = "";

                                routesCellToSave++;
                            }
                            break;

                        case 2:
                            {
                                RoutesLoadFromFile.RoutesDataList[routesCellToSave] = routesItems[i].RouteShortName;
                                routesCellToSave++;
                            }
                            break;

                        case 3:
                            {
                                RoutesLoadFromFile.RoutesDataList[routesCellToSave] = routesItems[i].RouteLongName;
                                routesCellToSave++;
                            }
                            break;

                        case 4:
                            {
                                RoutesLoadFromFile.RoutesDataList[routesCellToSave] = "";

                                routesCellToSave++;
                            }
                            break;



                        case 5:
                            {
                                if (routesItems[i].RouteType == Route0Style)
                                {
                                    RoutesLoadFromFile.RoutesDataList[routesCellToSave] = "0";
                                }
                                else if (routesItems[i].RouteType == Route1Style)
                                {
                                    RoutesLoadFromFile.RoutesDataList[routesCellToSave] = "1";
                                }
                                else if (routesItems[i].RouteType == Route2Style)
                                {
                                    RoutesLoadFromFile.RoutesDataList[routesCellToSave] = "2";
                                }
                                else if (routesItems[i].RouteType == Route3Style)
                                {
                                    RoutesLoadFromFile.RoutesDataList[routesCellToSave] = "3";
                                }
                                else if (routesItems[i].RouteType == Route4Style)
                                {
                                    RoutesLoadFromFile.RoutesDataList[routesCellToSave] = "4";
                                }
                                else if (routesItems[i].RouteType == Route5Style)
                                {
                                    RoutesLoadFromFile.RoutesDataList[routesCellToSave] = "5";
                                }
                                else if (routesItems[i].RouteType == Route6Style)
                                {
                                    RoutesLoadFromFile.RoutesDataList[routesCellToSave] = "6";
                                }
                                else if (routesItems[i].RouteType == Route7Style)
                                {
                                    RoutesLoadFromFile.RoutesDataList[routesCellToSave] = "7";
                                }
                                else
                                {
                                    RoutesLoadFromFile.RoutesDataList[routesCellToSave] = "";
                                }

                                routesCellToSave++;
                            }
                            break;


                        case 6:
                            {
                                RoutesLoadFromFile.RoutesDataList[routesCellToSave] = "";

                                routesCellToSave++;
                            }
                            break;
                        case 7:
                            {
                                RoutesLoadFromFile.RoutesDataList[routesCellToSave] = "";

                                routesCellToSave++;
                            }
                            break;
                        case 8:
                            {
                                RoutesLoadFromFile.RoutesDataList[routesCellToSave] = "";

                                routesCellToSave++;
                            }
                            break;






                    }


                    //CalendarLoadFromFile.CalendarDataList[10 + i] = calendarItems[i].Count;
                }

            }
        }

        private void TripsSaver()
        {

            TripsLoadFromFile.TripsDataList.Clear();

            for (int i = 0; i < 10; i++)
            {
                TripsLoadFromFile.TripsDataList.Add(TripsLoadFromFile.TripsDataCompare[i]);

            }


            int tripsRowsToSave = tripsItems.Count;
            int tripsCellToSave = 10;

            /*
            if ((TripsLoadFromFile.TripsDataList.Count - 10) != (tripsItems.Count * 10))
            {
                int i = TripsLoadFromFile.TripsDataList.Count - 10;
                int j = tripsItems.Count * 10;
                string s = "";
                for (int c = i; c < j; c++)

                {
                    TripsLoadFromFile.TripsDataList.Add(s);
                }
            }
            */

            for (int i = 0; i < tripsRowsToSave; i++)
            {

                for (int j = 0; j < 10; j++)
                {

                    TripsLoadFromFile.TripsDataList.Add("");
                    switch (j)
                    {
                        case 0:
                            {
                                TripsLoadFromFile.TripsDataList[tripsCellToSave] = tripsItems[i].TripsRouteid;
                                tripsCellToSave++;
                            }
                            break;
                        case 1:
                            {
                                TripsLoadFromFile.TripsDataList[tripsCellToSave] = tripsItems[i].TripsServiceid;
                                tripsCellToSave++;
                            }
                            break;

                        case 2:
                            {
                                TripsLoadFromFile.TripsDataList[tripsCellToSave] = tripsItems[i].TripsTripid;
                                tripsCellToSave++;
                            }
                            break;

                        case 3:
                            {
                                TripsLoadFromFile.TripsDataList[tripsCellToSave] = "";
                                tripsCellToSave++;
                            }
                            break;

                        case 4:
                            {
                                TripsLoadFromFile.TripsDataList[tripsCellToSave] = "";
                                tripsCellToSave++;
                            }
                            break;



                        case 5:
                            {
                                TripsLoadFromFile.TripsDataList[tripsCellToSave] = "";
                                tripsCellToSave++;
                            }
                            break;


                        case 6:
                            {
                                TripsLoadFromFile.TripsDataList[tripsCellToSave] = "";
                                tripsCellToSave++;
                            }
                            break;
                        case 7:
                            {
                                TripsLoadFromFile.TripsDataList[tripsCellToSave] = "";
                                tripsCellToSave++;
                            }
                            break;
                        case 8:
                            {
                                TripsLoadFromFile.TripsDataList[tripsCellToSave] = "";
                                tripsCellToSave++;
                            }
                            break;
                        case 9:
                            {
                                TripsLoadFromFile.TripsDataList[tripsCellToSave] = "";
                                tripsCellToSave++;
                            }
                            break;







                    }


                    //CalendarLoadFromFile.CalendarDataList[10 + i] = calendarItems[i].Count;
                }

            }
        }

        private void StopsSaver()
        {
            StopsLoadFromFile.StopsDataList.Clear();

            for (int i = 0; i < 12; i++)
            {
                StopsLoadFromFile.StopsDataList.Add(StopsLoadFromFile.StopsDataCompare[i]);

            }

            int stopsRowsToSave = stopsItems.Count;
            int stopsCellToSave = 12;
            /*
            if ((StopsLoadFromFile.StopsDataList.Count - 12) != (stopsItems.Count * 12))
            {
                int i = StopsLoadFromFile.StopsDataList.Count - 12;
                int j = stopsItems.Count * 12;
                string s = "";
                for (int c = i; c < j; c++)

                {
                    StopsLoadFromFile.StopsDataList.Add(s);
                }
            }
            */

            for (int i = 0; i < stopsRowsToSave; i++)
            {


                for (int j = 0; j < 12; j++)
                {
                    StopsLoadFromFile.StopsDataList.Add("");
                    switch (j)
                    {
                        case 0:
                            {
                                StopsLoadFromFile.StopsDataList[stopsCellToSave] = stopsItems[i].Stopsid;
                                stopsCellToSave++;
                            }
                            break;
                        case 1:
                            {
                                StopsLoadFromFile.StopsDataList[stopsCellToSave] = "";
                                stopsCellToSave++;
                            }
                            break;

                        case 2:
                            {
                                StopsLoadFromFile.StopsDataList[stopsCellToSave] = stopsItems[i].StopsName;
                                stopsCellToSave++;
                            }
                            break;

                        case 3:
                            {
                                StopsLoadFromFile.StopsDataList[stopsCellToSave] = "";
                                stopsCellToSave++;
                            }
                            break;

                        case 4:
                            {
                                StopsLoadFromFile.StopsDataList[stopsCellToSave] = "";
                                stopsCellToSave++;
                            }
                            break;



                        case 5:
                            {
                                StopsLoadFromFile.StopsDataList[stopsCellToSave] = "";
                                stopsCellToSave++;
                            }
                            break;


                        case 6:
                            {
                                StopsLoadFromFile.StopsDataList[stopsCellToSave] = stopsItems[i].StopsZoneid;
                                stopsCellToSave++;
                            }
                            break;
                        case 7:
                            {
                                StopsLoadFromFile.StopsDataList[stopsCellToSave] = "";
                                stopsCellToSave++;
                            }
                            break;
                        case 8:
                            {
                                StopsLoadFromFile.StopsDataList[stopsCellToSave] = "";
                                stopsCellToSave++;
                            }
                            break;
                        case 9:
                            {
                                StopsLoadFromFile.StopsDataList[stopsCellToSave] = "";
                                stopsCellToSave++;
                            }
                            break;

                        case 10:
                            {
                                StopsLoadFromFile.StopsDataList[stopsCellToSave] = "";
                                stopsCellToSave++;
                            }
                            break;

                        case 11:
                            {
                                StopsLoadFromFile.StopsDataList[stopsCellToSave] = "";
                                stopsCellToSave++;
                            }
                            break;






                    }



                }

            }
        }

        private void StopTimesSaver()
        {

            StopTimesLoadFromFile.StopTimesDataList.Clear();

            for (int i = 0; i < 10; i++)
            {
                StopTimesLoadFromFile.StopTimesDataList.Add(StopTimesLoadFromFile.StopTimesDataCompare[i]);

            }

            int stopTimesRowsToSave = stopTimesItems.Count;
            int stopTimesCellToSave = 10;

            /*
            if ((StopTimesLoadFromFile.StopTimesDataList.Count - 10) != (stopsItems.Count * 10))
            {
                int i = StopTimesLoadFromFile.StopTimesDataList.Count - 10;
                int j = stopTimesItems.Count * 10;
                string s = "";
                for (int c = i; c < j; c++)

                {
                    StopTimesLoadFromFile.StopTimesDataList.Add(s);
                }
            }
            */
            for (int i = 0; i < stopTimesRowsToSave; i++)
            {

                for (int j = 0; j < 10; j++)
                {
                    StopTimesLoadFromFile.StopTimesDataList.Add("");

                    switch (j)
                    {
                        case 0:
                            {
                                StopTimesLoadFromFile.StopTimesDataList[stopTimesCellToSave] = stopTimesItems[i].StopTimesTripid;
                                stopTimesCellToSave++;
                            }
                            break;
                        case 1:
                            {
                                string ArrivalTime = stopTimesItems[i].StopTimesArrivalTimeHours + ":" + stopTimesItems[i].StopTimesArrivalTimeMinutes + ":" + stopTimesItems[i].StopTimesArrivalTimeSeconds;
                                StopTimesLoadFromFile.StopTimesDataList[stopTimesCellToSave] = ArrivalTime;
                                stopTimesCellToSave++;
                            }
                            break;

                        case 2:
                            {
                                string DepartureTime = stopTimesItems[i].StopTimesDepartureTimeHours + ":" + stopTimesItems[i].StopTimesDepartureTimeMinutes + ":" + stopTimesItems[i].StopTimesDepartureTimeSeconds;
                                StopTimesLoadFromFile.StopTimesDataList[stopTimesCellToSave] = DepartureTime;
                                stopTimesCellToSave++;
                            }
                            break;

                        case 3:
                            {
                                StopTimesLoadFromFile.StopTimesDataList[stopTimesCellToSave] = stopTimesItems[i].StopTimesStopsid;
                                stopTimesCellToSave++;
                            }
                            break;

                        case 4:
                            {
                                StopTimesLoadFromFile.StopTimesDataList[stopTimesCellToSave] = stopTimesItems[i].StopTimesSequence;
                                stopTimesCellToSave++;
                            }
                            break;



                        case 5:
                            {
                                StopTimesLoadFromFile.StopTimesDataList[stopTimesCellToSave] = "";
                                stopTimesCellToSave++;
                            }
                            break;


                        case 6:
                            {
                                StopTimesLoadFromFile.StopTimesDataList[stopTimesCellToSave] = "";
                                stopTimesCellToSave++;
                            }
                            break;
                        case 7:
                            {
                                StopTimesLoadFromFile.StopTimesDataList[stopTimesCellToSave] = "";
                                stopTimesCellToSave++;
                            }
                            break;
                        case 8:
                            {
                                StopTimesLoadFromFile.StopTimesDataList[stopTimesCellToSave] = "";
                                stopTimesCellToSave++;
                            }
                            break;
                        case 9:
                            {
                                StopTimesLoadFromFile.StopTimesDataList[stopTimesCellToSave] = "";
                                stopTimesCellToSave++;
                            }
                            break;







                    }



                }

            }
        }

        // VALUES CLASSES FOR DATAGRID 
        public class AgencyValues
        {
            public string AgencyKategoria { get; set; }
            public string AgencyData { get; set; }
        }

        public class CalendarValues
        {
            public string CalendarServiceid { get; set; }

            public Style CalendarMondayStyle { get; set; }

            public Style CalendarTuesdayStyle { get; set; }

            public Style CalendarWednesdayStyle { get; set; }

            public Style CalendarThursdayStyle { get; set; }

            public Style CalendarFridayStyle { get; set; }

            public Style CalendarSaturdayStyle { get; set; }

            public Style CalendarSundayStyle { get; set; }

            public DateTime CalendarStartDate { get; set; }

            public DateTime CalendarEndDate { get; set; }



            //datetime etc



        }

        public class CalendarDatesValues
        {
            public string CalendarDatesServiceid { get; set; }

            public DateTime CalendarDatesDate { get; set; }

            public Style CalendarDatesExceptionType { get; set; }

            public List<string> CalendarDatesServiceIdFromCalendar { get; set; }

    }

        public class RoutesValues
        {
            public string Routeid { get; set; }

            public string RouteAgencyid { get; set; }     //OPTIONAL

            public string RouteShortName { get; set; }

            public string RouteLongName { get; set; }

            public string RouteDesc { get; set; }       //OPTIONAL

            public Style RouteType { get; set; }

            public string RouteUrl { get; set; }        //OPTIONAL

            public string RouteColor { get; set; }     //OPTIONAL

            public string RouteColorText { get; set; }  //OPTIONAL
        }

        public class TripsValues
        {
            public string TripsRouteid { get; set; }

            public string TripsServiceid { get; set; }

            public string TripsTripid { get; set; }

            public string TripsHeadsign { get; set; }        //OPTIONAL

            public string TripsShortName { get; set; }       //OPTIONAL

            public string TripsDirectionid { get; set; }     //OPTIONAL

            public string TripsBlockid { get; set; }        //OPTIONAL

            public string TripsShapeid { get; set; }     //OPTIONAL

            public string TripsWheelchair { get; set; }  //OPTIONAL

            public string TripsBikes { get; set; }  //OPTIONAL
        }

        public class StopsValues
        {


            public string Stopsid { get; set; }

            public string StopsCode { get; set; } //OPTIONAL

            public string StopsName { get; set; }

            public string StopsDesc { get; set; }        //OPTIONAL

            public string StopsLat { get; set; }       //OPTIONAL

            public string StopsLon { get; set; }     //OPTIONAL

            public string StopsZoneid { get; set; }        //OPTIONAL

            public string StopsUrl { get; set; }     //OPTIONAL

            public string StopsLocationType { get; set; }  //OPTIONAL

            public string StopsParentStation { get; set; }  //OPTIONAL

            public string StopsTimezone { get; set; }       //OPTIONAL

            public string StopsWheelChair { get; set; }  //OPTIONAL

        }

        public class StopTimesValues
        {
            public string StopTimesTripid { get; set; }

            public string StopTimesArrivalTimeHours { get; set; }

            public string StopTimesArrivalTimeMinutes { get; set; }

            public string StopTimesArrivalTimeSeconds { get; set; }

            public string StopTimesDepartureTimeHours { get; set; }

            public string StopTimesDepartureTimeMinutes { get; set; }

            public string StopTimesDepartureTimeSeconds { get; set; }

            public string StopTimesStopsid { get; set; }

            public string StopTimesSequence { get; set; }

            public string StopTimesHeadsign { get; set; }     //OPTIONAL

            public string StopTimesPickupType { get; set; }        //OPTIONAL

            public string StopTimesDropoffType { get; set; }     //OPTIONAL

            public string StopTimesShape { get; set; }  //OPTIONAL

            public string StopTimesTimepoint { get; set; }  //OPTIONAL


        }




        // VARIABLES FOR DATAGRID 
        int AgencyallEntriesCount;
        int AgencydataRowsCount;
        int AgencyFullCategoryRepeatTimes;
        int AgencyTotalRows;

        int CalendarallEntriesCount;
        int CalendardataCellsCount;
        int CalendarFullCategoryRepeatTimes;
        int CalendarTotalCells;

        int CalendarDatesallEntriesCount;
        int CalendarDatesdataCellsCount;
        int CalendarDatesFullCategoryRepeatTimes;
        int CalendarDatesTotalCells;

        int RoutesDataTypes;
        int RoutesallEntriesCount;
        int RoutesdataCellsCount;
        int RoutesFullCategoryRepeatTimes;
        int RoutesTotalCells;

        int TripsDataTypes;
        int TripsallEntriesCount;
        int TripsdataCellsCount;
        int TripsFullCategoryRepeatTimes;
        int TripsTotalCells;

        int StopsDataTypes;
        int StopsallEntriesCount;
        int StopsdataCellsCount;
        int StopsFullCategoryRepeatTimes;
        int StopsTotalCells;

        int StopTimesDataTypes;
        int StopTimesallEntriesCount;
        int StopTimesdataCellsCount;
        int StopTimesFullCategoryRepeatTimes;
        int StopTimesTotalCells;






        public void SaveToFile(int choice)
        {
            switch (choice)
            {

                case 0:
                    {
                        AgencySaver();
                        AgencyLoadFromFile.AgencyWriter();

                    }
                    break;

                case 1:
                    {
                        CalendarSaver();
                        CalendarLoadFromFile.CalendarWriter();

                    }
                    break;

                case 2:
                    {
                        CalendarDatesSaver();
                        CalendarDatesLoadFromFile.CalendarDatesWriter();

                    }
                    break;
                case 3:
                    {
                        RoutesSaver();
                        RoutesLoadFromFile.RoutesWriter();

                    }
                    break;
                case 4:
                    {
                        TripsSaver();
                        TripsLoadFromFile.TripsWriter();

                    }
                    break;
                case 5:
                    {
                        StopsSaver();
                        StopsLoadFromFile.StopsWriter(); //Work In Progress

                    }
                    break;
                case 6:
                    {
                        StopTimesSaver();
                        StopTimesLoadFromFile.StopTimesWriter(); //Work In Progress

                    }
                    break;




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


        public void DeleteRow(int row, int entity)
        {
            switch (entity)
            {
                case 0:

                    calendarItems.RemoveAt(row);
                    break;

                case 1:
                    calendarDatesItems.RemoveAt(row);
                    break;
                case 2:
                    routesItems.RemoveAt(row);
                    break;
                case 3:
                    tripsItems.RemoveAt(row);
                    break;
                case 4:
                    stopsItems.RemoveAt(row);
                    break;
                case 5:
                    stopTimesItems.RemoveAt(row);
                    break;

            }


        }

        public void CalendarAddRow()
        {
            
            Style qStyle = new Style();
            qStyle = (Style)Application.Current.Resources["UnknownButtonImageStyle"];

            DateTime tempStartDateTime = new DateTime(2017, 1, 1);
            DateTime tempStartDate = tempStartDateTime.Date;

            DateTime tempEndDateTime = new DateTime(2017, 12, 31);
            DateTime tempEndDate = tempEndDateTime.Date;

            calendarItems.Add(new CalendarValues()
            {
                CalendarServiceid = "",
                CalendarMondayStyle = qStyle,
                CalendarTuesdayStyle = qStyle,
                CalendarWednesdayStyle = qStyle,
                CalendarThursdayStyle = qStyle,
                CalendarFridayStyle = qStyle,
                CalendarSaturdayStyle = qStyle,
                CalendarSundayStyle = qStyle,
                CalendarStartDate = tempStartDate,
                CalendarEndDate = tempEndDate
            });


        }

        public void CalendarDatesAddRow()
        {
            

            Style qStyle = new Style();
            qStyle = (Style)Application.Current.Resources["UnknownButtonImageStyle"];

            DateTime tempDateTime = new DateTime(2017, 1, 1);
            DateTime tempDate = tempDateTime.Date;






            calendarDatesItems.Add(new CalendarDatesValues()
            {
                CalendarDatesServiceid = "",
                CalendarDatesDate = tempDate,
                CalendarDatesExceptionType = qStyle,
                CalendarDatesServiceIdFromCalendar = new List<string>()
            });

            calendarDatesItems[((calendarDatesItems.Count)-1)].CalendarDatesServiceIdFromCalendar.Add("");
    

         






        }

        public void RoutesAddRow()
        {
            Style qStyle = (Style)Application.Current.Resources["UnknownButtonImageStyle"];





            routesItems.Add(new RoutesValues()
            {
                Routeid = "",
                RouteAgencyid = "",
                RouteShortName = "",
                RouteLongName = "",
                RouteDesc = "",
                RouteType = qStyle,
                RouteUrl = "",
                RouteColor = "",
                RouteColorText = ""

            });




        }

        public void TripsAddRow()
        {
            tripsItems.Add(new TripsValues()
            {
                TripsRouteid = "",
                TripsServiceid = "",
                TripsTripid = " ",
                TripsHeadsign = "",
                TripsShortName = "",
                TripsDirectionid = "",
                TripsBlockid = "",
                TripsShapeid = "",
                TripsWheelchair = "",
                TripsBikes = ""

            });

        }

        public void StopsAddRow()
        {

            stopsItems.Add(new StopsValues()
            {
                Stopsid = "",
                StopsCode = "",
                StopsName = " ",
                StopsDesc = "",
                StopsLat = "",
                StopsLon = "",
                StopsZoneid = "",
                StopsUrl = "",
                StopsLocationType = "",
                StopsParentStation = "",
                StopsTimezone = "",
                StopsWheelChair = ""
            });
        }

        public void StopTimesAddRow()
        {


            stopTimesItems.Add(new StopTimesValues()
            {
                StopTimesTripid = "",
                StopTimesArrivalTimeHours = "00",
                StopTimesArrivalTimeMinutes = "00",
                StopTimesArrivalTimeSeconds = "00",
                StopTimesDepartureTimeHours = "00",
                StopTimesDepartureTimeMinutes = "00",
                StopTimesDepartureTimeSeconds = "00",
                StopTimesStopsid = "",
                StopTimesSequence = "",
                StopTimesHeadsign = "",
                StopTimesPickupType = "",
                StopTimesDropoffType = "",
                StopTimesShape = "",
                StopTimesTimepoint = ""

            });
        }





    }
}

