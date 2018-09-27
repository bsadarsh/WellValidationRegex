using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Reflection;
using GemBox.Spreadsheet;
using CodeEffects.Rule.Attributes;
using CodeEffects.Rule.Common;
using System.Data;
using System.Data.OleDb;
using DPRTDValidation.Code;
using ValidationObjectsLibrary2011;
using System.Text.RegularExpressions;

namespace DPRTDData
{
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

    public class DPRParse
    {
        public void ParsingDPRTDData()
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
                        ValidateTDDPRColumns(obj, objFull, rowindex);
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
                ValidateDPRFullData(objlist, objFull, rowindex);
            }
            catch (Exception ex)
            {
                throw ex;
                //Console.WriteLine(ex);
            }

        }

        private static void ValidateDPRFullData(List<DPRTDData> objlist, List<DPRFull> objFull, int rowindex)
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
                        //Console.WriteLine(errorObj.Data);
                        objerror.WellName = objDPRFull.FormalWellName;
                        objerror.ErrorMessage = errorObj.Data;
                        objerror.Row = i;
                        objDPRFull.Errors.Add(objerror);
                    }
                    else
                    {
                        //Console.WriteLine(errorObj.LT10);
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
                            //Console.WriteLine(errorObj.Case);
                            objerror.WellName = objlist[i].WellName;
                            objerror.ErrorMessage = errorObj.Case;
                            objerror.Row = i + rowindex + 3;
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
                        //Console.WriteLine(errorObj.UnitsMatch);
                        objerror.WellName = objlist[i].WellName;
                        objerror.ErrorMessage = errorObj.UnitsMatch;
                        objerror.Row = i + rowindex + 3;
                        objlist[i].Errors.Add(objerror);
                    }

                }

            }

        }
        
        private static void ValidateTDDPRColumns(DPRTDData obj, List<DPRFull> objDPRList, int rowindex)
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
                        //Console.WriteLine(errorObj.MDays);

                        objerror.WellName = obj.WellName;
                        objerror.ErrorMessage = errorObj.MDays;
                        objerror.Row = j + rowindex + 3;
                        obj.Errors.Add(objerror);
                    }

                    if (obj.Actualdepth[j] == null)
                    {
                       // Console.WriteLine(errorObj.MDepths);

                        objerror.WellName = obj.WellName;
                        objerror.ErrorMessage = errorObj.MDepths;
                        objerror.Row = j + rowindex + 3;
                        obj.Errors.Add(objerror);
                    }

                    if (obj.HoleSize[j] == null)
                    {
                        //Console.WriteLine(errorObj.MHole);

                        objerror.WellName = obj.WellName;
                        objerror.ErrorMessage = errorObj.MHole;
                        objerror.Row = j + rowindex + 3;
                        obj.Errors.Add(objerror);
                    }
                }
                CheckDUplicate(obj, rowindex);
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
                            //Console.WriteLine(errorObj.MTDMissing);

                            objerror.WellName = obj.WellName;
                            objerror.Row = i + rowindex + 3;
                            objerror.ErrorMessage = errorObj.MTDMissing;
                            obj.Errors.Add(objerror);
                        }
                        double? max = obj.Actualdepth.Max();
                        if (!(max < mtd1 && max > mtd2))
                        {
                            //Console.WriteLine(errorObj.MTDLargest);

                            objerror.WellName = obj.WellName;
                            objerror.Row = i + rowindex + 3;
                            objerror.ErrorMessage = errorObj.MTDLargest;
                            obj.Errors.Add(objerror);
                        }
                        if (obj.HoleSize.Last() != objFull.FinalDrillBitSize)
                        {
                            //Console.WriteLine(errorObj.HSDrill);

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
        private static void CheckDUplicate(DPRTDData obj, int rowindex)
        {
            ErrorMessage errorObj = new ErrorMessage();
            ValidationError objerror = new ValidationError();
            var duplicates = obj.NoOfDay
                            .GroupBy(i => i)
                            .Where(g => g.Count() > 1)
                            .Select(g => g.Key);
        
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
                        //Console.WriteLine(errorObj.DepthsSpike);
                        objerror.WellName = obj.WellName;
                        objerror.ErrorMessage = errorObj.DepthsSpike;
                        objerror.Row = j + rowindex + 3;
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
                       // Console.WriteLine(errorObj.DaysSeq);
                        objerror.WellName = obj.WellName;
                        objerror.ErrorMessage = errorObj.DaysSeq;
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
                       // Console.WriteLine(errorObj.DaysDec);

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
