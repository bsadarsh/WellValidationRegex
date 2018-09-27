using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DPRTDValidation.DAL;
using System.Data;
using System.Reflection;
using CodeEffects.Rule;
using System.Threading;
using System.Globalization;

namespace DPRTDValidation.Code
{
    /// <summary>
    /// Author : Peter Milne
    /// Date : 14/10/11
    /// Purpose : Main class for the validation engine.  This class is responsible for checking that each
    ///           Column is valid by comparing its values with the rules stored against the column.
    /// </summary>
    public class ValidationEngine
    {
        readonly object _currentValidationObject;                                        //  The current validation object from the validation objects library
        readonly string _fileName;                                                       //  Filename of the input excel spreadsheet
        readonly string _validationObjectType;                                           //  Name of the validation object type
        readonly List<string> _levelOneFailures = new List<string>();                    //  List of columns that failed Level one validation
        readonly List<object> _validationObjects = new List<object>();                   //  List of DPRTight objects (one for each row)
        readonly List<string> _uniqueIDs = new List<string>();                           //  List of Unique IDs taken from the spreadsheet
        readonly ColumnAttributeMapping _colAttrMapping;                                 //  Object that maps uniqueIDs to attributes
        private readonly int _opStatusId;
        private readonly string _operator;
        private readonly string _businessUnit;
        private readonly string _group;
        private readonly string _region;

        public ValidationEngine(string fileName, string validationObjectType,
            string uniqueIdRange, string dataRange, int opStatusId, string Operator,
            string businessUnit, string region, string group
            )
        {
            //  There is an issue with the validation editor and dates if not in US locale
            //  So we set it here temporarily
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

            //  Set user selected variable values
            _opStatusId = opStatusId;
            _operator = Operator;
            _businessUnit = businessUnit;
            _region = region;
            _group = group;

            //  Set the file name of the excel submission workbook
            _fileName = fileName;

            //  Set the validation object type name
            _validationObjectType = validationObjectType;
             
            //  Create an instance of the current validation object
            //  We don't assign to this object but use it to access the types
            // properties through reflection
            _currentValidationObject = CreateValidationObject();

            //  Create a new instance of the column attribute mapper
            _colAttrMapping = new ColumnAttributeMapping(_currentValidationObject);

            BeginValidation(uniqueIdRange, dataRange);

            //  Return the locale to EN-GB
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-GB");
        }

        private object CreateValidationObject()
        {
            //  Get the fully qualified name of the assembly being used for validation
            string sourceType = ValidationObjectsConfiguration.GetConfig().SourceType;
            Assembly validationAssembly = Assembly.Load(sourceType);
            Type objectType = validationAssembly.GetType(_validationObjectType);

            //  Set the current validation object 
            object validationObject = Activator.CreateInstance(objectType);

            return validationObject;
        }

        /// <summary>
        /// Performs the DPR tight validation.  When validation is performed, any errors that
        /// occur during Level 1 or during type checking are tracked and then any column that
        /// is related to the error column is excluded from subsequent levels of checks.
        /// </summary>
        private void BeginValidation(string uniqueIdRange, string dataRange)
        {
            try
            {
                using (var context = new RushmoreSPValidationEngineDALDataContext())
                {
                    var importer= new SubmissionImporter();

                    //  Read the IDs row of the DPR Tight submission into list
                    DataSet idDataSet = importer.ReadExcel(_fileName, uniqueIdRange , "Data Input");

                    for (int x = 0; x < idDataSet.Tables[0].Rows.Count; x++)
                    {
                        for (int y = 0; y < idDataSet.Tables[0].Rows[x].Table.Columns.Count; y++)
                        {
                            _uniqueIDs.Add(idDataSet.Tables[0].Rows[x]
                                                    [idDataSet.Tables[0].Rows[x].Table.Columns[y]].ToString());
                        }
                    }

                    //  Get a list of levels from the database
                    List<t_FieldValidationLevel> levels = context.t_FieldValidationLevels.ToList();

                    //  Read the spreadsheet range into a Dataset
                    DataSet excelDataSet = importer.ReadExcel(_fileName, dataRange, "Data Input");

                    //  Create a validation object for each of the rows in the input sheet.
                    for (int x = 0; x < excelDataSet.Tables[0].Rows.Count; x++)
                    {
                        //  Create a new instance of the Validation object & set the row, OpStatusID, Operator
                        //  business unit, country and group properties
                        object validationObject = CreateValidationObject();
                        validationObject.GetType().GetProperty("Row").SetValue(validationObject, x, null);
                        validationObject.GetType().GetProperty("OpStatusId").SetValue(validationObject, _opStatusId, null);
                        validationObject.GetType().GetProperty("Operator").SetValue(validationObject, _operator, null);
                        validationObject.GetType().GetProperty("BusinessUnit").SetValue(validationObject, _businessUnit, null);
                        validationObject.GetType().GetProperty("Group").SetValue(validationObject, _group, null);
                        validationObject.GetType().GetProperty("Region").SetValue(validationObject, _region, null);

                        //  Loop through the columns in the dataset
                        for (int y = 0; y < excelDataSet.Tables[0].Rows[x].Table.Columns.Count; y++)
                        {

                            PropertyInfo attribute =
                                validationObject.GetType().GetProperty(
                                    _colAttrMapping.ColumnAttributeMappings.Single(
                                        c => c.Key == _uniqueIDs[y]).Value);


                            //  TODO:  We need to deal with data type errors here and return
                            //  warning to user.  These errors prevent validation going any further
                            //  Ensure this column has not been excluded due to level 1 errors
                            //if (!(_levelOneFailures.Contains(attribute.Name) && level.LevelName != "Level One"))
                            //{
                                object columnValue =
                                    excelDataSet.Tables[0].Rows[x][
                                        excelDataSet.Tables[0].Rows[x].Table.Columns[y]];

                                //  first, check if the column data is DBNull
                                //  If not then check the property type and 
                                //  set the value of the property.
                                if (columnValue is DBNull)
                                {
                                    attribute.SetValue(validationObject, null, null);
                                }
                                else if (attribute.PropertyType == typeof(string))
                                {
                                    attribute.SetValue(validationObject,
                                                       columnValue.ToString() == string.Empty
                                                           ? null
                                                           : columnValue.ToString(),
                                                       null);
                                }
                                else if (attribute.PropertyType == typeof(double?))
                                {
                                    double d;
                                    if (double.TryParse(columnValue.ToString(), out d))
                                    {
                                        attribute.SetValue(validationObject, d, null);
                                    }
                                    else
                                    {
                                        attribute.SetValue(validationObject, null, null);
                                        //if (level.LevelName == "Level One") AddLevelOneFailure(attribute.Name);
                                    }
                                }
                                else if (attribute.PropertyType == typeof(int?))
                                {
                                    int i;
                                    if(int.TryParse(columnValue.ToString(), out i))
                                    {
                                        attribute.SetValue(validationObject, i, null);
                                    }
                                    else
                                    {
                                        attribute.SetValue(validationObject, null, null);
                                    }
                                }
                                else if (attribute.PropertyType == typeof(DateTime?))
                                {
                                    DateTime dt;
                                    if (DateTime.TryParse(columnValue.ToString(), out dt))
                                    {
                                        attribute.SetValue(validationObject, dt, null);
                                    }
                                    else
                                    {
                                        attribute.SetValue(validationObject, null, null);
                                        //AddToErrorLog(context, "Data of wrong type", DateTime.Now, x, _uniqueIDs[y]);
                                       // if (level.LevelName == "Level One") AddLevelOneFailure(attribute.Name);
                                    }
                                }
                            }
                        //}
                        //  Add the validation object to the list of validation objects
                        _validationObjects.Add(validationObject);
                    } 
                    
                        //  Loop through the validation objects
                    for (int x = 0; x < _validationObjects.Count; x++)
                    {
                        //  For each of the levels defined
                        foreach (t_FieldValidationLevel level in levels)
                        {
                            //  We only want to perform calculations at the first level
                            if (level.LevelName == "Level One")
                            {
                                _validationObjects[x].GetType().InvokeMember("CalculateCalculatedColumns",
                                                                             BindingFlags.InvokeMethod |
                                                                             BindingFlags.Default, null,
                                                                             _validationObjects[x], new object[0]);
                            }
                            //  Get a list of ValidationXMLs for the current level
                            t_FieldValidationLevel level1 = level;
                            List<t_FieldValidation> validationXmLs =
                                context.t_FieldValidations.Where(v => v.LevelID == level1.LevelID &&
                                                                      v.AssemblyName ==
                                                                      _currentValidationObject.GetType().ToString()
                                    ).ToList();

                            //  If this is not Level one then we need to exlude validations
                            //  where that column failed a level one check AND
                            //  where this ValidationXML object is related to any of the Level one Failures
                            if (level.LevelName != "Level One")
                            {
                                //  Deal with columns with the same column name
                                List<t_FieldValidation> toDeleteValidationXmLs = (from v in validationXmLs
                                                                                  from lof in _levelOneFailures
                                                                                  where
                                                                                      lof ==
                                                                                      _colAttrMapping.
                                                                                          ColumnAttributeMappings.
                                                                                          Single
                                                                                          (c => c.Key == v.UniqueID)
                                                                                          .
                                                                                          Value
                                                                                  select v).ToList();

                                //  Deal with related columns
                                toDeleteValidationXmLs.AddRange(from v in validationXmLs
                                                                from lof in _levelOneFailures
                                                                from r in v.t_FieldValidationRelateds
                                                                where r.Attribute == lof
                                                                select v);


                                //  Remove the failed / dependant column validation
                                foreach (var v in toDeleteValidationXmLs)
                                {
                                    validationXmLs.Remove(v);
                                }
                            }

                            //  Perform the actual validation
                            foreach (var v in validationXmLs)
                            {
                                Evaluator.Execute(v.ValidationXML, _validationObjects[x]);
                            }

                            //  If this is Level One then we need to capture the ColumNames of the failures
                            if (level.LevelName == "Level One")
                            {
                                foreach (string failedColumnName in from ve in
                                                                        (List<ValidationError>)
                                                                        _validationObjects[x].GetType().GetProperty(
                                                                            "Errors").GetValue(
                                                                                _validationObjects[x], new object[0])
                                                                    select
                                                                        _colAttrMapping.ColumnAttributeMappings.
                                                                        Single(
                                                                            c => c.Key == ve.DisplayName).Value)
                                {
                                    AddLevelOneFailure(failedColumnName);
                                }
                            }
                        } //  Levels

                        _levelOneFailures.Clear();
                    } //  Validation objects

                    //  Store the DPRTight objects in session so we can use them to export to query workbook
                    HttpContext.Current.Session["validationObjects"] = _validationObjects;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void AddLevelOneFailure(string failedColumnName)
        {
            if (!_levelOneFailures.Contains(failedColumnName))
            {
                _levelOneFailures.Add(failedColumnName);
            }
        }
    }
}
