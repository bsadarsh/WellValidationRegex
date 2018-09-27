using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace ConsoleApplication2
{
    class Program
    {
        private double _d;

        public double D
        {
            get { return s && _u == "F" ? _d * 3 : _d; }
            set { _d = value; }
        }
        private bool s = true;

        public bool S
        {
            get { return s; }
            set { s = value; }
        }
        private string _u ="F";

        public string U
        {
            get { return _u; }
            set { _u = value; }
        }


        static void Main(string[] args)
        {
            //DateTime? str2 = Convert.ToDateTime("23/01/2012");    

            // double? d = 2.5;
            //DateTime? c = CalcWowMooring(str2);

            //string file = @"C:\Rushmore\Studies\Data\DPR 11\FullQueryWorkbookTemplate2.xls";

           // string destfile = Path.GetTempPath() + Guid.NewGuid().ToString() + ".xls";
            //File.Copy(file, destfile);

            //  Create new instance of GemBox Spreadsheet 
            //var ef = new ExcelFile();
            //ef.LoadXls(file,xlsOptions:XlsOptions.PreserveAll);
            //ExcelWorksheet ws = ef.Worksheets["Queries"];
            //ws.Cells["A1"].Value = "Drilling Performance Review " +  DateTime.Now.Year.ToString() + " -  Query 1 for " + "BP" + DateTime.Today.ToString("dd-MMM-yy");
          //  ws.Cells["A3"].Value = "Drilling Performance Review1 " + DateTime.Now.Year.ToString() + " -  Query 2 for " + "BP1" + DateTime.Today.ToString("dd-MMM-yy");
            // Load source Excel file.
            //ws.Rows[0].AutoFit();
            //ef.SaveXls("destfile.xls");
            
           // Console.ReadLine();

            string strToTest;
            Console.Write("enter the well ");
            strToTest = Console.ReadLine();
            Program p = new Program();
            p.D = Convert.ToDouble(strToTest);
            Console.WriteLine(p.D.ToString());
            Console.ReadLine();

        }

        private static string CalcAreaCode(string areaCode, string country)
        {
            if (country.ToUpper() != "AUSTRALIA" && country.ToUpper() != "NEW ZEALAND")
            {
                return null;
            }
            int partOne;
            if (int.TryParse(areaCode, out partOne))
            {
                if (int.TryParse(areaCode.Substring(0, 2), out partOne))
                {
                    return areaCode.Substring(0, 2);
                }
            }
            return null;
        }



        private static DateTime? CalcEndWellOPs(DateTime? endOfDryholePeriod, double? paSuCoDays)
        {
            DateTime parsedDate;
            double dno;

            if (endOfDryholePeriod == null)
            {
                return null;
            }           

            if (DateTime.TryParse(endOfDryholePeriod.ToString(), out parsedDate))
            {
                 if (double.TryParse(paSuCoDays.ToString(), out dno))
                 {
                     return parsedDate.Add(TimeSpan.FromDays(dno -1));
                 }                    
                
            }
            return null;
        }


        public static DateTime? CalcWowMooring(DateTime? endOfWellOps)
        {
            DateTime parsedDate;
            if (endOfWellOps == null)
            {
                return null;
            }
            if (DateTime.TryParse(endOfWellOps.ToString(), out parsedDate))
            {        
                TimeSpan d = DateTime.Now - parsedDate;
                return Convert.ToDateTime(d.ToString());
            }
            return null;
        }


    }
}
