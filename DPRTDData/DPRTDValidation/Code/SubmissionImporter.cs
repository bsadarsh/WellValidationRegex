using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;

namespace DPRTDValidation.Code
{
    public class SubmissionImporter
    {
        public DataSet ReadExcel(string fileName, string range, string table)
        {
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties=\"Excel 8.0;HDR=NO;IMEX=1\"";

            var objConn = new OleDbConnection(connectionString);

            objConn.Open();

            var objCmdSelect = new OleDbCommand("SELECT * FROM [" + table + "$" + range + "]", objConn);

            var excelAdapter = new OleDbDataAdapter {SelectCommand = objCmdSelect};

            var excelDataset = new DataSet();

            excelAdapter.Fill(excelDataset);

            objConn.Close();

            return excelDataset;
        }
    }
}