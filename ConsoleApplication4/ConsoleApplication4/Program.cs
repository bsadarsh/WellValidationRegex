using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GemBox.Spreadsheet;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
namespace ConsoleApplication4
{
    public class Program
    {

        static void Main(string[] args)
        {

            string fileName = @"D:\New folder\BP UK S1 UniqueIDSInc.xls";
           
            ParsingDPRFullData(fileName);
            Console.ReadLine();

        }

        private static DataSet ParsingDPRFullData(string fileName)
        {
            DataSet ds = new DataSet();          
            ExcelFile exf = new ExcelFile();           
            bool bPresent = false;         
            try
            {
                exf.LoadXls(fileName);           
                foreach (ExcelWorksheet sheet in exf.Worksheets)
                {
                    if (sheet.Name == "Sheet1")
                    {
                        bPresent = true;
                        break;
                    }
                }
             
                if (bPresent)
                {
                    ExcelWorksheet ws = exf.Worksheets["Sheet1"];
                    Dictionary<string, string> lst = new Dictionary<string, string>();
                    int k = 0;
                    if (ws.Cells["A1"].Value.ToString() == "WellName")
                    {
                        try
                        {
                            for (int i = 1; i <= 150; i++)
                            {
                                if (ws.Columns[0].Cells[i].Value != null)
                                {
                                    lst.Add(ws.Columns[0].Cells[i].Value.ToString(), ws.Columns[1].Cells[i].Value.ToString());
                                    k = lst.Count;
                                }                            
                            }

                            for (int i = 1; i <= 150; i++)
                            {
                                if (ws.Columns[2].Cells[i].Value != null)
                                {
                                        lst.Add(ws.Columns[2].Cells[i].Value.ToString(), ws.Columns[3].Cells[i].Value.ToString());
                                    k = i;
                                }
                            }

                            for (int i = 1; i <= 27; i++)
                            {
                                if (ws.Columns[4].Cells[i].Value != null)
                                {
                                    
                                        lst.Add(ws.Columns[4].Cells[i].Value.ToString(), ws.Columns[5].Cells[i].Value.ToString());

                                    k = i;
                                }
                            }

                            Console.WriteLine(k.ToString());
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }

                    if (ws.Cells["J1"].Value.ToString() == "well number")
                    {
                        for (int i = 2; i < 26; ++i)
                        {
                            if (ws.Cells["J" + i.ToString()].Value != null)
                            { 
                                if(ws.Cells["J" + i.ToString()].Value.ToString().Contains(" | "))
                                {
                                }
                                else
                                {
                                    if(lst.ContainsKey(ws.Cells["J" + i.ToString()].Value.ToString()))
                                        ws.Cells["H" + i].Value = lst[ws.Cells["J" + i.ToString()].Value.ToString()];
                                }                                
                            }
                        }
                    }
                  
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ds;
        }     
    }

 
   

}
