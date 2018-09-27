using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GemBox.Spreadsheet;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.OleDb;

namespace ConsoleApplication1
{
    public class Program
    {

        static void Main(string[] args)
        {

            List<DPRTDData> objlist = new List<DPRTDData>();
            ExcelFile exf = new ExcelFile();
            int cntColumn = 0;
            int rowcount = 0;
            int rowindex = 0;
            try
            {
                List<DPRFull> objFull = AssignDataInput();
                exf.LoadXls(@"C:\Rushmore\Studies\Data\DPR 11\DPR Africa 11\Raw Data\Submissions\Cabinda Gulf Angola S1.xls");
                ExcelWorksheet ws = exf.Worksheets["Time-depth data"];

                if (ws.Cells["A2"].Value.ToString() == "Drilling Performance Review" && ws.Cells["A8"].Value.ToString() == "Units (M/F)")
                {
                    cntColumn = ws.CalculateMaxUsedColumns();
                    rowindex = GetRowIndex(ws);
                    if (cntColumn >= 3)
                    {
                        DPRTDData obj = populatedDPRData(ws, rowindex, cntColumn);
                        ValidateTDDPRColumns(obj, objFull,rowindex);
                        objlist.Add(obj);
                        for (int i = 3; i < cntColumn; i++)
                        {
                            if (cntColumn - i > 2)
                            {
                                rowcount = GetRowCount(ws, i);
                                rowcount = rowcount + 2;
                                DPRTDData obj1 = new DPRTDData();
                                if (ws.Columns[i + 1].Cells[rowindex].Value != null)
                                    obj1.Units = ws.Columns[i + 1].Cells[rowindex].Value.ToString();
                                if (ws.Columns[i + 1].Cells[rowindex + 1].Value != null)
                                    obj1.WellName = ws.Columns[i + 1].Cells[rowindex + 1].Value.ToString();
                                if (obj1.Units != null)
                                {
                                    obj1 = populatedData(ws, i, rowindex, rowcount, cntColumn, obj1);
                                    ValidateTDDPRColumns(obj1, objFull, rowindex);
                                    objlist.Add(obj1);
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        DPRTDData obj = populatedDPRData(ws, rowindex, cntColumn);
                    }
                }

                //ValidateTDDPRColumns(objlist, objFull);
                ValidateDPRFullData(objlist, objFull,rowindex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            //Console.ReadLine();
        }

        private static void ValidateDPRFullData(List<DPRTDData> objlist, List<DPRFull> objFull,int rowindex)
        {
            ErrorMessage errorObj = new ErrorMessage();
            ValidationError objerror = new ValidationError();
            bool status = false;
            for (int i = 0; i < objFull.Count; i++)
            {
                DPRFull objDPRFull = objFull[i];
                for (int j = 0; j < objlist.Count; j++)
                {
                    if (objlist[j].WellName == objDPRFull.FormalWellName)
                    {
                        status = true;
                        break;
                    }
                    else
                        status = false;                       
                }
                if (!status)
                {
                    if (objDPRFull.DryHoleDays > 10)
                    {
                        Console.WriteLine(errorObj.Data);
                        objerror.WellName = objDPRFull.FormalWellName;
                        objerror.ErrorMessage = errorObj.Data;
                        objerror.Row = i;
                        objDPRFull.Errors.Add(objerror);
                    }
                    else
                    {
                        Console.WriteLine(errorObj.LT10);
                        objerror.WellName = objDPRFull.FormalWellName;
                        objerror.ErrorMessage = errorObj.LT10;
                        objerror.Row = i;
                        objDPRFull.Errors.Add(objerror);
                    }
                }
            }

            for (int i = 0; i < objlist.Count; i++)
            {
                for (int j = 0; j < objFull.Count; j++)
                {
                    if (objFull[j].FormalWellName.ToLower() == objlist[i].WellName.ToLower())
                    {
                        if (!objlist[i].WellName.Equals(objFull[j].FormalWellName, StringComparison.Ordinal))
                        {
                            Console.WriteLine(errorObj.Case);
                            objerror.WellName = objlist[i].WellName;
                            objerror.ErrorMessage = errorObj.Case;
                            objerror.Row = i+rowindex+3;
                            objlist[i].Errors.Add(objerror);
                        }
                        break;
                    }
                    else
                    {
                        objerror.WellName = objlist[i].WellName;
                        objerror.ErrorMessage = errorObj.TD;
                        objerror.Row = i + rowindex + 3;
                        objlist[i].Errors.Add(objerror);
                    }                   
                    if (objFull[j].UnitOfMeasurement != objlist[i].Units)
                    {
                        Console.WriteLine(errorObj.UnitsMatch);
                        objerror.WellName = objlist[i].WellName;
                        objerror.ErrorMessage = errorObj.UnitsMatch;
                        objerror.Row = i + rowindex + 3;
                        objlist[i].Errors.Add(objerror);
                    }                 
                
                }             
               
            }

        }

        private static void ValidateTDDPRColumns1(List<DPRTDData> objlist, List<DPRFull> objDPRList)
        {
            ErrorMessage errorObj = new ErrorMessage();

            double? mtd1 = 0;
            double? mtd2 = 0;
            try
            {
                for (int i = 0; i < objlist.Count; i++)
                {
                    DPRTDData obj = objlist[i];
                    for (int j = 0; j < obj.NoOfDay.Count; j++)
                    {
                        if (obj.NoOfDay[j] == null)
                            Console.WriteLine(errorObj.MDays);

                        if (obj.Actualdepth[j] == null)
                            Console.WriteLine(errorObj.MDepths);

                        if (obj.HoleSize[j] == null)
                            Console.WriteLine(errorObj.MHole);
                    }
                   // CheckDUplicate(obj);
                    //CheckDepthsSpike(obj);
                    //CheckDaysSeq(obj);
                    //CheckDaysDesc(obj);
                }

                for (int i = 0; i < objlist.Count; i++)
                {
                    DPRTDData obj = objlist[i];
                    DPRFull objFull = objDPRList[i];

                    if (obj.WellName == objFull.FormalWellName)
                    {
                        mtd1 = objFull.Mtd + (objFull.Mtd * 0.01);
                        mtd2 = objFull.Mtd - (objFull.Mtd * 0.01);

                        if (!obj.Actualdepth.Contains(objFull.Mtd))
                            Console.WriteLine(errorObj.MTDMissing);
                        double? max = obj.Actualdepth.Max();
                        if (!(max < mtd1 && max > mtd2))
                            Console.WriteLine(errorObj.MTDLargest);
                        if (obj.HoleSize.Last() != objFull.FinalDrillBitSize)
                            Console.WriteLine(errorObj.HSDrill);
                        for (int j = 0; j < obj.NoOfDay.Count; j++)
                        {
                            if (obj.Actualdepth[j] < mtd1 && obj.Actualdepth[j] > mtd2)
                                obj.DepthFlag = true;
                            else
                                obj.DepthFlag = false;
                        }
                    }
                }
                //Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        private static void ValidateTDDPRColumns(DPRTDData obj, List<DPRFull> objDPRList,int rowindex)
        {
            ErrorMessage errorObj = new ErrorMessage();
            ValidationError objerror = new ValidationError();


            double? mtd1 = 0;
            double? mtd2 = 0;
            try
            {
                for (int j = 0; j < obj.NoOfDay.Count; j++)
                {
                    if (obj.NoOfDay[j] == null)
                    {
                        Console.WriteLine(errorObj.MDays);
 
                        objerror.WellName = obj.WellName;
                        objerror.ErrorMessage = errorObj.MDays;
                        objerror.Row = j + rowindex+3;
                        obj.Errors.Add(objerror);
                    }

                    if (obj.Actualdepth[j] == null)
                    {
                        Console.WriteLine(errorObj.MDepths);
                
                        objerror.WellName = obj.WellName;
                        objerror.ErrorMessage = errorObj.MDepths;
                        objerror.Row = j + rowindex + 3;
                        obj.Errors.Add(objerror);
                    }

                    if (obj.HoleSize[j] == null)
                    {
                        Console.WriteLine(errorObj.MHole);
              
                        objerror.WellName = obj.WellName;
                        objerror.ErrorMessage = errorObj.MHole;
                        objerror.Row = j + rowindex + 3;
                        obj.Errors.Add(objerror);
                    }
                }
                CheckDUplicate(obj,rowindex);
                CheckDepthsSpike(obj, rowindex);
                CheckDaysSeq(obj, rowindex);
                CheckDaysDesc(obj, rowindex);

                for (int i = 0; i < objDPRList.Count; i++)
                {                   
                    DPRFull objFull = objDPRList[i];

                    if (obj.WellName == objFull.FormalWellName)
                    {
                        mtd1 = objFull.Mtd + (objFull.Mtd * 0.01);
                        mtd2 = objFull.Mtd - (objFull.Mtd * 0.01);

                        if (!obj.Actualdepth.Contains(objFull.Mtd))
                        {
                            Console.WriteLine(errorObj.MTDMissing);
                     
                            objerror.WellName = obj.WellName;
                            objerror.Row = i + rowindex + 3;
                            objerror.ErrorMessage = errorObj.MTDMissing;
                            obj.Errors.Add(objerror);
                        }
                        double? max = obj.Actualdepth.Max();
                        if (!(max < mtd1 && max > mtd2))
                        {
                            Console.WriteLine(errorObj.MTDLargest);
                    
                            objerror.WellName = obj.WellName;
                            objerror.Row = i + rowindex + 3;
                            objerror.ErrorMessage = errorObj.MTDLargest;
                            obj.Errors.Add(objerror);
                        }
                        if (obj.HoleSize.Last() != objFull.FinalDrillBitSize)
                        {
                            Console.WriteLine(errorObj.HSDrill);
               
                            objerror.WellName = obj.WellName;
                            objerror.Row = i + rowindex + 3;
                            objerror.ErrorMessage = errorObj.HSDrill;
                            obj.Errors.Add(objerror);
                        }
                        for (int j = 0; j < obj.NoOfDay.Count; j++)
                        {
                            if (obj.Actualdepth[j] < mtd1 && obj.Actualdepth[j] > mtd2)
                                obj.DepthFlag = true;
                            else
                                obj.DepthFlag = false;
                        }
                    }
                }            
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        private static void CheckDUplicate(DPRTDData obj,int rowindex)
        {
            ErrorMessage errorObj = new ErrorMessage();
            ValidationError objerror = new ValidationError();
            var duplicates = obj.NoOfDay
                            .GroupBy(i => i)
                            .Where(g => g.Count() > 1)
                            .Select(g => g.Key);

            //foreach (var well in duplicates)
            //{
            //    Console.WriteLine(well.Value.ToString());
            //}
            if (duplicates.Count() > 0)
            {
                Console.WriteLine(errorObj.DaysRep);       
                objerror.WellName = obj.WellName;
                objerror.ErrorMessage = errorObj.DaysRep;
                obj.Errors.Add(objerror);
            }

        }

        private static void CheckDepthsSpike(DPRTDData obj, int rowindex)
        {
            ErrorMessage errorObj = new ErrorMessage();
            ValidationError objerror = new ValidationError();
            try
            {
                for (int j = 0; j < obj.Actualdepth.Count - 1; j++)
                {
                    if (obj.Actualdepth[j].Value < (obj.Actualdepth[j + 1].Value * 0.2) || obj.Actualdepth[j + 1].Value > (obj.Actualdepth[j].Value * 5))
                    {
                        Console.WriteLine(errorObj.DepthsSpike);
                        objerror.WellName = obj.WellName;
                        objerror.ErrorMessage = errorObj.DepthsSpike;
                        objerror.Row = j+rowindex+3;
                        obj.Errors.Add(objerror);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        private static void CheckDaysSeq(DPRTDData obj, int rowindex)
        {
            ErrorMessage errorObj = new ErrorMessage();
            ValidationError objerror = new ValidationError();
            int? diff = obj.NoOfDay[1] - obj.NoOfDay[0];
            try
            {
                for (int i = 0; i < obj.NoOfDay.Count - 1; i++)
                {
                    if (!(obj.NoOfDay[i + 1] > obj.NoOfDay[i] && obj.NoOfDay[i + 1] - obj.NoOfDay[i] == diff))
                    {
                        Console.WriteLine(errorObj.DaysSeq);
                        objerror.WellName = obj.WellName;
                        objerror.ErrorMessage = errorObj.DaysSeq;
                        objerror.Row = i + rowindex + 3;
                        obj.Errors.Add(objerror);
                    }
                }

                //Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        private static void CheckDaysDesc(DPRTDData obj, int rowindex)
        {
            ErrorMessage errorObj = new ErrorMessage();
            ValidationError objerror = new ValidationError();
            int? dayFirst = obj.NoOfDay.First();
            int? dayLast = obj.NoOfDay.Last();
            try
            {
                for (int i = 0; i < obj.NoOfDay.Count - 1; i++)
                {
                    if (!(obj.NoOfDay[i] >= dayFirst && obj.NoOfDay[i] <= dayLast && obj.NoOfDay[i + 1] > obj.NoOfDay[i]))
                    {
                        Console.WriteLine(errorObj.DaysDec);

                        objerror.WellName = obj.WellName;
                        objerror.ErrorMessage = errorObj.DaysDec;
                        objerror.Row = i + rowindex + 3;
                        obj.Errors.Add(objerror);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        private static DPRTDData populatedDPRData(ExcelWorksheet ws, int rowindex, int cntColumn)
        {
            try
            {
                int rowcount = GetRowCount(ws, 0);
                DPRTDData obj = new DPRTDData();
                for (int i = 0; i < 2; i++)
                {
                    if (ws.Columns[i].Cells[rowindex].Value != null && ws.Columns[i].Cells[rowindex].Value.ToString() == "Units (M/F)")
                          obj.Units = ws.Rows[rowindex].Cells[1].Value.ToString();

                    if (ws.Columns[i].Cells[rowindex + 1].Value != null && ws.Columns[i].Cells[rowindex + 1].Value.ToString() == "Well name")
                        obj.WellName = ws.Rows[rowindex + 1].Cells[1].Value.ToString();

                    if (obj.WellName != ws.Columns[i].Cells[rowindex + 1].Value.ToString())
                        obj = populatedData(ws, i, rowindex, rowcount, cntColumn, obj);
                }
                return obj;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        private static DPRTDData populatedData1(ExcelWorksheet ws, int i, int rowindex, int rowcount, int cntColumn, DPRTDData obj)
        {
            try
            {
                int colCnt = i + 2;
                for (int m = rowindex; m < rowcount; m++)
                {
                    if (ws.Columns[i].Cells[m].Value != null && ws.Columns[i].Cells[m].Value.ToString() == "Day")
                    {
                        int cnt = m;
                        for (int p = 0; p < rowcount - 3; p++)
                        {
                            obj.NoOfDay.Add(Convert.ToInt32(ws.Rows[cnt + 1].Cells[0].Value.ToString()));
                            cnt++;
                        }
                    }
                }
                for (int n = i; n <= colCnt; n++)
                {
                    for (int p = rowindex; p < rowcount; p++)
                    {
                        if (ws.Columns[n].Cells[p].Value != null && ws.Columns[n].Cells[p].Value.ToString() == "Actual depth")
                        {
                            int cnt = p;
                            for (int q = 0; q < rowcount - 3; q++)
                            {
                                obj.Actualdepth.Add(Convert.ToDouble(ws.Columns[n].Cells[cnt + 1].Value));
                                cnt++;
                            }
                        }
                        if (ws.Columns[n].Cells[p].Value != null && ws.Columns[n].Cells[p].Value.ToString().Contains("Hole size"))
                        {
                            int cnt = p;
                            for (int q = 0; q < rowcount - 3; q++)
                            {
                                obj.HoleSize.Add(Convert.ToDouble((ws.Columns[n].Cells[cnt + 1].Value != null && ws.Columns[n].Cells[cnt + 1].Value.ToString() != string.Empty) ? ws.Columns[n].Cells[cnt + 1].Value : 0.0));
                                cnt++;
                            }
                        }
                    }
                }

                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static DPRTDData populatedData(ExcelWorksheet ws, int i, int rowindex, int rowcount, int cntColumn, DPRTDData obj)
        {
            try
            {
                ErrorMessage errorObj = new ErrorMessage();
                int colCnt = i + 2;
                for (int m = rowindex; m < rowcount; m++)
                {
                    if (ws.Columns[i].Cells[m].Value != null && ws.Columns[i].Cells[m].Value.ToString() == "Day")
                    {
                        int cnt = m;
                        for (int p = 0; p < rowcount - 3; p++)
                        {
                           int day = 0;
                            if ((ws.Rows[cnt + 1].Cells[0].Value != null) && int.TryParse(ws.Rows[cnt + 1].Cells[0].Value.ToString(), out day))
                            {
                                obj.NoOfDay.Add(day);
                            }
                            else
                            {
                                obj.NoOfDay.Add((int?)null);
                                ValidationError objerror = new ValidationError();
                                objerror.ErrorMessage = errorObj.NonNumericDays;
                                objerror.WellName = obj.WellName;
                                objerror.Row = rowindex + 3 + p;
                                obj.Errors.Add(objerror);
                            }                       
                            cnt++;
                        }
                    }
                }
                for (int n = i; n <= colCnt; n++)
                {
                    for (int p = rowindex; p < rowcount; p++)
                    {
                        if (ws.Columns[n].Cells[p].Value != null && ws.Columns[n].Cells[p].Value.ToString() == "Actual depth")
                        {
                            int cnt = p;
                            for (int q = 0; q < rowcount - 3; q++)
                            {
                                double depth = 0;
                                if ((ws.Columns[n].Cells[cnt + 1].Value != null) && double.TryParse(ws.Columns[n].Cells[cnt + 1].Value.ToString(), out depth))
                                {
                                    obj.Actualdepth.Add(depth);
                                }
                                else
                                {
                                    obj.Actualdepth.Add((double?)null);
                                    ValidationError objerror = new ValidationError();
                                    objerror.ErrorMessage = errorObj.NonNumericDepths;
                                    objerror.WellName = obj.WellName;
                                    objerror.Row = rowindex + 3 + q;
                                    obj.Errors.Add(objerror);
                                }                            
                              cnt++;
                            }
                        }
                       if (ws.Columns[n].Cells[p].Value != null && ws.Columns[n].Cells[p].Value.ToString().Contains("Hole size"))
                        {
                            int cnt = p;
                            for (int q = 0; q < rowcount - 3; q++)
                            {
                                double size = 0;
                                if ((ws.Columns[n].Cells[cnt + 1].Value != null) && double.TryParse(ws.Columns[n].Cells[cnt + 1].Value.ToString(), out size))
                                {
                                    obj.HoleSize.Add(size);
                                }
                                else
                                {
                                    obj.HoleSize.Add((double?)null);
                                    ValidationError objerror = new ValidationError();
                                    objerror.ErrorMessage = errorObj.NonNumericHS;
                                    objerror.WellName = obj.WellName;
                                    objerror.Row = rowindex + 3 + q;
                                    obj.Errors.Add(objerror);
                                }                              
                                cnt++;
                           }
                        }
                    }
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static int GetRowCount(ExcelWorksheet ws, int i)
        {
            ExcelColumn col = ws.Columns[i];
            int rowcount = 0;
            int cntNull = 0;
            try
            {
                for (int j = 7; j < col.Cells.Count(); j++)
                {
                    if (col.Cells[j].Value != null)
                        rowcount++;
                    else
                        cntNull++;

                    if (cntNull > 3)
                        break;
                }

                return rowcount;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

        }

        private static int GetRowIndex(ExcelWorksheet ws)
        {
            ExcelColumn col = ws.Columns[0];
            int rowindex = 0;
            try
            {
                for (int j = 0; j < col.Cells.Count(); j++)
                {
                    if (col.Cells[j].Value != null && col.Cells[j].Value.ToString() == "Units (M/F)")
                    {
                        rowindex = j;
                        break;
                    }
                }

                return rowindex;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        private static List<DPRFull> AssignDataInput()
        {
            try
            {
                List<DPRFull> objDPR = new List<DPRFull>();

                DPRFull obj = new DPRFull();

                DPRFull obj1 = new DPRFull();
                obj.FormalWellName = "BL6P10";
                obj.CommonWellName = "BL6P10";
                obj.BasinName = "Belize";
                obj.BlockNumber = "14";
                obj.LatDeg = 5;
                obj.LatMin = 37;
                obj.LatSec = 6.95;
                obj.Ns = "S";
                obj.LongDeg = 11;
                obj.LongMin = 54;
                obj.LongSec = 44.9;
                obj.Ew = "E";
                obj.DrillingContractor = "KCAD";
                obj.RigName = "BBLT";
                obj.OwnerDrilled = 1;
                obj.HoleType = "N";
                obj.LocatorWell = "N";
                obj.MultiLateral = "N";
                obj.WellType = "D";
                obj.PlayType = "H";
                obj.RigType = "PL";
                obj.DrillingMethod = "TAM";
                obj.UnitOfMeasurement = "F";
                obj.WaterDepth = 1281;
                obj.SpudDepthBrt = 1482;
                obj.Mtd = 17900;
                obj.Tvd = 8138;
                obj.SubSalt = "N";
                obj.ComplexWell = "N";
                obj.MaximumAngleDegrees = 81;
                obj.FinalDrillBitSize = 12.25;
                obj.DryHoleDays = 31.2;
                objDPR.Add(obj);
                obj1.FormalWellName = "bl6p10";
                objDPR.Add(obj1);
                return objDPR;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }

    public class DPRTDData
    {
        #region InstanceVariables

        private string _units;
        private string _wellName;
        private bool _depthFlag;
        private List<int?> _noOfDay = new List<int?>();
        private List<double?> _actualDepth = new List<double?>();
        private List<double?> _holeSize = new List<double?>();
        private List<ValidationError> _errors = new List<ValidationError>();
        #endregion

        #region Properties

        public string Units
        {
            get { return _units; }
            set { _units = value; }
        }


        public string WellName
        {
            get { return _wellName; }
            set { _wellName = value; }
        }

        public bool DepthFlag
        {
            get { return _depthFlag; }
            set { _depthFlag = value; }
        }

        public List<int?> NoOfDay
        {
            get { return _noOfDay; }
            set { _noOfDay = value; }
        }


        public List<double?> Actualdepth
        {
            get { return _actualDepth; }
            set { _actualDepth = value; }
        }


        public List<double?> HoleSize
        {
            get { return _holeSize; }
            set { _holeSize = value; }
        }

        public List<ValidationError> Errors
        {
            get { return _errors; }
            set { _errors = value; }
        }

        #endregion

    }

    public class DPRFull
    {
        #region Instance Variables
        private int _opStatusId;
        private string _group;
        private string _company;
        private string _businessUnit;
        private string _country;
        private string _formalWellName;
        private string _commonWellName;
        private string _basinName;
        private string _blockNumber;
        private double? _latDeg;
        private double? _latMin;
        private double? _latSec;
        private string _ns;
        private double? _longDeg;
        private double? _longMin;
        private double? _longSec;
        private string _ew;
        private string _drillingContractor;
        private string _rigName;
        private int? _ownerDrilled;
        private string _holeType;
        private string _locatorWell;
        private string _multiLateral;
        private string _multiLateralJunctionType;
        private double? _numberOfLaterals;
        private double? _continuingGeolSTracks;
        private double? _mechanicalSTracks;
        private string _reSpuddedreDrilled;
        private string _originalNameForReSpud;
        private string _wellType;
        private string _playType;
        private string _hp;
        private string _ht;
        private string _rigType;
        private string _drillingMethod;
        private string _unitOfMeasurement;
        private double? _waterDepth;
        private double? _spudDepthBrt;
        private double? _mtd;
        private double? _unusedLengthsDueToStracks;
        private double? _locatorUnusedLengths;
        private double? _tvd;
        private string _subSalt;
        private double? _tvdStartOfSalt;
        private double? _tvdEndOfSalt;
        private string _complexWell;
        private double? _maximumAngleDegrees;
        private double? _totalLengthHorizSections;
        private double? _finalDrillBitSize;
        private string _preCasing1;
        private string _preCasing2;
        private string _preCasing3;
        private string _preCasing4;
        private string _preCasing5;
        private string _preCasing6;
        private string _preCasing7;
        private string _preCasing8;
        private string _preCasing9;
        private string _preCasing10;
        private string _preCasing11;
        private string _newConductorCasing;
        private string _conductorInstalledByRig;
        private string _newCasing2;
        private string _newCasing3;
        private string _newCasing4;
        private string _newCasing5;
        private string _newCasing6;
        private string _newCasing7;
        private string _newCasing8;
        private string _newCasing9;
        private string _newCasing10;
        private string _newCasing11;
        private string _underBalancedWell;
        private string _drillingFluidType;
        private string _mudWeightUnits;
        private double? _mudWeightTd;
        private double? _maxMudWeight;
        private string _cuttingsDisposal;
        private double? _coringDays;
        private double? _coringInterval;
        private double? _loggingDaysNotAtTd;
        private double? _loggingDaysAtTd;
        private double? _underReamingDays;
        private double? _underReamedInterval;
        private string _fewd;
        private string _ageOfDeepestResevoir;
        private string _newTechniques;
        private double? _rigMoveTime;
        private string _rigMoveWithinField;
        private double? _geoSideTrackWhipstockDays;
        private double? _slotRecoveryDays;
        private string _offlineSlotRecoveryOps;
        private string _batchSetDrilled;
        private DateTime? _spudDate;
        private double? _dryHoleDays;
        private DateTime? _endOfDryholePeriod;
        private int? _dryHoleSuspensions;
        private double? _daysSpentSuspensions;
        private double? _totalWellSiteDays;
        private double? _dryHoleCost;
        private string _prelimOrFinalCost;
        private string _completenessOfCost;
        private double? _totalWellCost;
        private string _currency;
        private string _wellStauts;
        private double? _paSuCoDays;
        private double? _otherOPdays;
        private DateTime? _endOfWellOps;
        private double? _wowBeforeMoveOff;
        private string _rigMooringSystem;
        private double? _daysSpentOnMooring;
        private double? _daysSpentDeMooring;
        private double? _wowDuringMooring;
        private double? _wowDuringDeMooring;
        private double? _totalWowDuringDryhole;
        private double? _totalNptExclWow;
        private double? _nptDueToContractor;
        private double? _nptDueToServiceCompany;
        private double? _nptDueToOperatorProbs;
        private double? _nptDueToExternalProbs;
        private double? _nptDueToDownholeProbs;
        private string _mainInterruptsDryHole;
        private string _furtherDetails;
        private string _comments;
        private string _comments2;
        private string _apiWellNumber;
        private string _lwlWellName;
        private double? _lwlLoggingTime;
        private string _dataType = "F";
        private List<ValidationError> _errors = new List<ValidationError>();
        private int _row;
        // private readonly ColumnAttributeMapping _mapping;


        #endregion

        #region Properties
        public int OpStatusId
        {
            get { return _opStatusId; }
            set { _opStatusId = value; }
        }

        public string Group
        {
            get { return _group; }
            set { _group = value; }
        }

        public string Company
        {
            get { return _company; }
            set { _company = value; }
        }

        public string BusinessUnit
        {
            get { return _businessUnit; }
            set { _businessUnit = value; }
        }

        public string Country
        {
            get { return _country; }
            set { _country = value; }
        }

        public string FormalWellName
        {
            get { return _formalWellName; }
            set { _formalWellName = value; }
        }

        public string CommonWellName
        {
            get { return _commonWellName; }
            set { _commonWellName = value; }
        }

        public string BasinName
        {
            get { return _basinName; }
            set { _basinName = value; }
        }

        public string BlockNumber
        {
            get { return _blockNumber; }
            set { _blockNumber = value; }
        }

        public double? LatDeg
        {
            get { return _latDeg; }
            set { _latDeg = value; }
        }

        public double? LatMin
        {
            get { return _latMin; }
            set { _latMin = value; }
        }

        public double? LatSec
        {
            get { return _latSec; }
            set { _latSec = value; }
        }

        public string Ns
        {
            get { return _ns; }
            set { _ns = value; }
        }

        public double? LongDeg
        {
            get { return _longDeg; }
            set { _longDeg = value; }
        }

        public double? LongMin
        {
            get { return _longMin; }
            set { _longMin = value; }
        }

        public double? LongSec
        {
            get { return _longSec; }
            set { _longSec = value; }
        }

        public string Ew
        {
            get { return _ew; }
            set { _ew = value; }
        }

        public string DrillingContractor
        {
            get { return _drillingContractor; }
            set { _drillingContractor = value; }
        }

        public string RigName
        {
            get { return _rigName; }
            set { _rigName = value; }
        }

        public int? OwnerDrilled
        {
            get { return _ownerDrilled; }
            set { _ownerDrilled = value; }
        }

        public string HoleType
        {
            get { return _holeType; }
            set { _holeType = value; }
        }

        public string LocatorWell
        {
            get { return _locatorWell; }
            set { _locatorWell = value; }
        }

        public string MultiLateral
        {
            get { return _multiLateral; }
            set { _multiLateral = value; }
        }

        public string MultiLateralJunctionType
        {
            get { return _multiLateralJunctionType; }
            set { _multiLateralJunctionType = value; }
        }

        public double? NumberOfLaterals
        {
            get { return _numberOfLaterals; }
            set { _numberOfLaterals = value; }
        }

        public double? ContinuingGeolSTracks
        {
            get { return _continuingGeolSTracks; }
            set { _continuingGeolSTracks = value; }
        }

        public double? MechanicalSTracks
        {
            get { return _mechanicalSTracks; }
            set { _mechanicalSTracks = value; }
        }

        public string ReSpuddedreDrilled
        {
            get { return _reSpuddedreDrilled; }
            set { _reSpuddedreDrilled = value; }
        }

        public string OriginalNameForReSpud
        {
            get { return _originalNameForReSpud; }
            set { _originalNameForReSpud = value; }
        }

        public string WellType
        {
            get { return _wellType; }
            set { _wellType = value; }
        }

        public string PlayType
        {
            get { return _playType; }
            set { _playType = value; }
        }

        public string Hp
        {
            get { return _hp; }
            set { _hp = value; }
        }

        public string Ht
        {
            get { return _ht; }
            set { _ht = value; }
        }

        public string RigType
        {
            get { return _rigType; }
            set { _rigType = value; }
        }

        public string DrillingMethod
        {
            get { return _drillingMethod; }
            set { _drillingMethod = value; }
        }

        public string UnitOfMeasurement
        {
            get { return _unitOfMeasurement; }
            set { _unitOfMeasurement = value; }
        }

        public double? WaterDepth
        {
            get { return _waterDepth; }
            set { _waterDepth = value; }
        }

        public double? SpudDepthBrt
        {
            get { return _spudDepthBrt; }
            set { _spudDepthBrt = value; }
        }

        public double? Mtd
        {
            get { return _mtd; }
            set { _mtd = value; }
        }

        public double? UnusedLengthsDueToStracks
        {
            get { return _unusedLengthsDueToStracks; }
            set { _unusedLengthsDueToStracks = value; }
        }

        public double? LocatorUnusedLengths
        {
            get { return _locatorUnusedLengths; }
            set { _locatorUnusedLengths = value; }
        }

        public double? Tvd
        {
            get { return _tvd; }
            set { _tvd = value; }
        }

        public string SubSalt
        {
            get { return _subSalt; }
            set { _subSalt = value; }
        }

        public double? TvdStartOfSalt
        {
            get { return _tvdStartOfSalt; }
            set { _tvdStartOfSalt = value; }
        }

        public double? TvdEndOfSalt
        {
            get { return _tvdEndOfSalt; }
            set { _tvdEndOfSalt = value; }
        }

        public string ComplexWell
        {
            get { return _complexWell; }
            set { _complexWell = value; }
        }

        public double? MaximumAngleDegrees
        {
            get { return _maximumAngleDegrees; }
            set { _maximumAngleDegrees = value; }
        }

        public double? TotalLengthHorizSections
        {
            get { return _totalLengthHorizSections; }
            set { _totalLengthHorizSections = value; }
        }

        public double? FinalDrillBitSize
        {
            get { return _finalDrillBitSize; }
            set { _finalDrillBitSize = value; }
        }

        public string PreCasing1
        {
            get { return _preCasing1; }
            set { _preCasing1 = value; }
        }

        public string PreCasing2
        {
            get { return _preCasing2; }
            set { _preCasing2 = value; }
        }

        public string PreCasing3
        {
            get { return _preCasing3; }
            set { _preCasing3 = value; }
        }

        public string PreCasing4
        {
            get { return _preCasing4; }
            set { _preCasing4 = value; }
        }

        public string PreCasing5
        {
            get { return _preCasing5; }
            set { _preCasing5 = value; }
        }

        public string PreCasing6
        {
            get { return _preCasing6; }
            set { _preCasing6 = value; }
        }

        public string PreCasing7
        {
            get { return _preCasing7; }
            set { _preCasing7 = value; }
        }

        public string PreCasing8
        {
            get { return _preCasing8; }
            set { _preCasing8 = value; }
        }

        public string PreCasing9
        {
            get { return _preCasing9; }
            set { _preCasing9 = value; }
        }

        public string PreCasing10
        {
            get { return _preCasing10; }
            set { _preCasing10 = value; }
        }

        public string PreCasing11
        {
            get { return _preCasing11; }
            set { _preCasing11 = value; }
        }

        public string NewConductorCasing
        {
            get { return _newConductorCasing; }
            set { _newConductorCasing = value; }
        }

        public string ConductorInstalledByRig
        {
            get { return _conductorInstalledByRig; }
            set { _conductorInstalledByRig = value; }
        }

        public string NewCasing2
        {
            get { return _newCasing2; }
            set { _newCasing2 = value; }
        }

        public string NewCasing3
        {
            get { return _newCasing3; }
            set { _newCasing3 = value; }
        }

        public string NewCasing4
        {
            get { return _newCasing4; }
            set { _newCasing4 = value; }
        }

        public string NewCasing5
        {
            get { return _newCasing5; }
            set { _newCasing5 = value; }
        }

        public string NewCasing6
        {
            get { return _newCasing6; }
            set { _newCasing6 = value; }
        }

        public string NewCasing7
        {
            get { return _newCasing7; }
            set { _newCasing7 = value; }
        }

        public string NewCasing8
        {
            get { return _newCasing8; }
            set { _newCasing8 = value; }
        }

        public string NewCasing9
        {
            get { return _newCasing9; }
            set { _newCasing9 = value; }
        }

        public string NewCasing10
        {
            get { return _newCasing10; }
            set { _newCasing10 = value; }
        }

        public string NewCasing11
        {
            get { return _newCasing11; }
            set { _newCasing11 = value; }
        }

        public string UnderBalancedWell
        {
            get { return _underBalancedWell; }
            set { _underBalancedWell = value; }
        }

        public string DrillingFluidType
        {
            get { return _drillingFluidType; }
            set { _drillingFluidType = value; }
        }

        public string MudWeightUnits
        {
            get { return _mudWeightUnits; }
            set { _mudWeightUnits = value; }
        }

        public double? MudWeightTd
        {
            get { return _mudWeightTd; }
            set { _mudWeightTd = value; }
        }

        public double? MaxMudWeight
        {
            get { return _maxMudWeight; }
            set { _maxMudWeight = value; }
        }

        public string CuttingsDisposal
        {
            get { return _cuttingsDisposal; }
            set { _cuttingsDisposal = value; }
        }

        public double? CoringDays
        {
            get { return _coringDays; }
            set { _coringDays = value; }
        }

        public double? CoringInterval
        {
            get { return _coringInterval; }
            set { _coringInterval = value; }
        }

        public double? LoggingDaysNotAtTd
        {
            get { return _loggingDaysNotAtTd; }
            set { _loggingDaysNotAtTd = value; }
        }

        public double? LoggingDaysAtTd
        {
            get { return _loggingDaysAtTd; }
            set { _loggingDaysAtTd = value; }
        }

        public double? UnderReamingDays
        {
            get { return _underReamingDays; }
            set { _underReamingDays = value; }
        }

        public double? UnderReamedInterval
        {
            get { return _underReamedInterval; }
            set { _underReamedInterval = value; }
        }

        public string Fewd
        {
            get { return _fewd; }
            set { _fewd = value; }
        }

        public string AgeOfDeepestResevoir
        {
            get { return _ageOfDeepestResevoir; }
            set { _ageOfDeepestResevoir = value; }
        }

        public string NewTechniques
        {
            get { return _newTechniques; }
            set { _newTechniques = value; }
        }

        public double? RigMoveTime
        {
            get { return _rigMoveTime; }
            set { _rigMoveTime = value; }
        }

        public string RigMoveWithinField
        {
            get { return _rigMoveWithinField; }
            set { _rigMoveWithinField = value; }
        }

        public double? GeoSideTrackWhipstockDays
        {
            get { return _geoSideTrackWhipstockDays; }
            set { _geoSideTrackWhipstockDays = value; }
        }

        public double? SlotRecoveryDays
        {
            get { return _slotRecoveryDays; }
            set { _slotRecoveryDays = value; }
        }

        public string OfflineSlotRecoveryOps
        {
            get { return _offlineSlotRecoveryOps; }
            set { _offlineSlotRecoveryOps = value; }
        }

        public string BatchSetDrilled
        {
            get { return _batchSetDrilled; }
            set { _batchSetDrilled = value; }
        }

        public DateTime? SpudDate
        {
            get { return _spudDate; }
            set { _spudDate = value; }
        }

        public double? DryHoleDays
        {
            get { return _dryHoleDays; }
            set { _dryHoleDays = value; }
        }

        public DateTime? EndOfDryholePeriod
        {
            get { return _endOfDryholePeriod; }
            set { _endOfDryholePeriod = value; }
        }

        public int? DryHoleSuspensions
        {
            get { return _dryHoleSuspensions; }
            set { _dryHoleSuspensions = value; }
        }

        public double? DaysSpentSuspensions
        {
            get { return _daysSpentSuspensions; }
            set { _daysSpentSuspensions = value; }
        }

        public double? TotalWellSiteDays
        {
            get { return _totalWellSiteDays; }
            set { _totalWellSiteDays = value; }
        }

        public double? DryHoleCost
        {
            get { return _dryHoleCost; }
            set { _dryHoleCost = value; }
        }

        public string PrelimOrFinalCost
        {
            get { return _prelimOrFinalCost; }
            set { _prelimOrFinalCost = value; }
        }

        public string CompletenessOfCost
        {
            get { return _completenessOfCost; }
            set { _completenessOfCost = value; }
        }

        public double? TotalWellCost
        {
            get { return _totalWellCost; }
            set { _totalWellCost = value; }
        }

        public string Currency
        {
            get { return _currency; }
            set { _currency = value; }
        }

        public string WellStauts
        {
            get { return _wellStauts; }
            set { _wellStauts = value; }
        }

        public double? PaSuCoDays
        {
            get { return _paSuCoDays; }
            set { _paSuCoDays = value; }
        }

        public double? OtherOPdays
        {
            get { return _otherOPdays; }
            set { _otherOPdays = value; }
        }

        public DateTime? EndOfWellOps
        {
            get { return _endOfWellOps; }
            set { _endOfWellOps = value; }
        }

        public double? WowBeforeMoveOff
        {
            get { return _wowBeforeMoveOff; }
            set { _wowBeforeMoveOff = value; }
        }

        public string RigMooringSystem
        {
            get { return _rigMooringSystem; }
            set { _rigMooringSystem = value; }
        }

        public double? DaysSpentOnMooring
        {
            get { return _daysSpentOnMooring; }
            set { _daysSpentOnMooring = value; }
        }

        public double? DaysSpentDeMooring
        {
            get { return _daysSpentDeMooring; }
            set { _daysSpentDeMooring = value; }
        }

        public double? WowDuringMooring
        {
            get { return _wowDuringMooring; }
            set { _wowDuringMooring = value; }
        }

        public double? WowDuringDeMooring
        {
            get { return _wowDuringDeMooring; }
            set { _wowDuringDeMooring = value; }
        }

        public double? TotalWowDuringDryhole
        {
            get { return _totalWowDuringDryhole; }
            set { _totalWowDuringDryhole = value; }
        }

        public double? TotalNptExclWow
        {
            get { return _totalNptExclWow; }
            set { _totalNptExclWow = value; }
        }

        public double? NptDueToContractor
        {
            get { return _nptDueToContractor; }
            set { _nptDueToContractor = value; }
        }

        public double? NptDueToServiceCompany
        {
            get { return _nptDueToServiceCompany; }
            set { _nptDueToServiceCompany = value; }
        }

        public double? NptDueToOperatorProbs
        {
            get { return _nptDueToOperatorProbs; }
            set { _nptDueToOperatorProbs = value; }
        }

        public double? NptDueToExternalProbs
        {
            get { return _nptDueToExternalProbs; }
            set { _nptDueToExternalProbs = value; }
        }

        public double? NptDueToDownholeProbs
        {
            get { return _nptDueToDownholeProbs; }
            set { _nptDueToDownholeProbs = value; }
        }

        public string MainInterruptsDryHole
        {
            get { return _mainInterruptsDryHole; }
            set { _mainInterruptsDryHole = value; }
        }

        public string FurtherDetails
        {
            get { return _furtherDetails; }
            set { _furtherDetails = value; }
        }

        public string Comments
        {
            get { return _comments; }
            set { _comments = value; }
        }

        public string Comments2
        {
            get { return _comments2; }
            set { _comments2 = value; }
        }

        public string ApiWellNumber
        {
            get { return _apiWellNumber; }
            set { _apiWellNumber = value; }
        }

        public string LwlWellName
        {
            get { return _lwlWellName; }
            set { _lwlWellName = value; }
        }

        public double? LwlLoggingTime
        {
            get { return _lwlLoggingTime; }
            set { _lwlLoggingTime = value; }
        }

        public string DataType
        {
            get { return _dataType; }
            set { _dataType = value; }
        }

        public List<ValidationError> Errors
        {
            get { return _errors; }
            set { _errors = value; }
        }

        public int Row
        {
            get { return _row; }
            set { _row = value; }
        }

        #endregion

    }

    public class ErrorMessage
    {
        public string Data = "This well appears on the Data Input spreadsheet and has more than 10 dry hole days but there is no Time-Depth data.";
        public string TD = "There is Time-Depth data for this well but it does not appear on the Data Input spreadsheet.";
        public string LT10 = "Although this well appears in the Data Input spreadsheet and has no Time-Depth data, that's okay because it has less than 10 dry hole days and therefore does not require time-depth data.";
        public string TwoTD = "This well appears more than once in the TD data.";
        public string TwoData = "This well appears more than once on the Data Input spreadsheet.";
        public string Case = "The name of this well is the same as on the Data Input spreadsheet but the case is different.";
        public string MUnits = "Please insert valid units (M or F) into TD data sheet.";
        public string UnitsMatch = "The units in the TD data do not match the units in the Data Input spreadsheet.";
        public string MDays = "Value(s) missing from the 'Days' column.";
        public string DaysSeq = "Non-sequential values appear in the 'Days' column.";
        public string DaysDec = "The days are not increasing from low to high.";
        public string DaysRep = "Repeated values appear in the 'days' column.";
        public string NonNumericDays = "There are non-numeric values in the 'days' column.";
        public string MDepths = "Value(s) missing from the 'Actual' column.";
        public string DepthsSpike = "Extreme spikes appear in the 'Actual' column.";
        public string MTDLargest = "The largest depth does not equal the MTD.";
        public string MTDMissing = "The MTD does not appear in the 'Actual' column.";
        public string NonNumericDepths = "There are non-numeric values in the 'Actual' column.";
        public string MHole = "Value(s) missing from the 'Hole size' column.";
        public string HSDrill = "The final drillbit size does not equal the smallest hole size.";
        public string NonNumericHS = "There are non-numeric values in the 'Hole size' column.";

    }


}
