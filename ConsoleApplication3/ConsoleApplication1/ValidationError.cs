using System;
using System.Linq;
using ConsoleApplication1.DAL;

namespace ConsoleApplication1
{
    [Serializable]
    public class ValidationError
    {
        private string _wellName;
        private string _columnName;
        private string _errorMessage;
        private string _errorTime;
        private string _displayErrorMessage;
        private int _row;
        private string _value;

        public string WellName
        {
            get { return _wellName; }
            set { _wellName = value; }
        }

        public string DisplayName
        {
            get { return _columnName; }
            set {_columnName = value;}
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; }
        }

        public string DisplayErrorMessage
        {
            get { return _displayErrorMessage; }
            set { _displayErrorMessage = value; }
        }

        public string ErrorTime
        {
            get { return _errorTime; }
            set { _errorTime = value; }
        }

        public int Row
        {
            get { return _row; }
            set { _row = value; }
        }

        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public ValidationError()
        {
        }

        public ValidationError(Type valobj, string wellName, string columnName, string errorMessage, int row, string value)
        {
            //  Set object properties
            _wellName = wellName;
            _columnName = columnName;
            _errorMessage = errorMessage;
            _errorTime = DateTime.Now.ToString();
            _row = row;
            _value = value;

            //  Get an english translation of the error code from the DB
            using (var context = new RushmoreSPValidationEngineDALDataContext())
            {
                try
                {
                    string errorCode = ErrorMessage.Substring(ErrorMessage.Length - 1, 1);
                    int fieldNumber = int.Parse(ErrorMessage.Substring(0, ErrorMessage.Length - 1));

                    DisplayErrorMessage = context.t_FieldValidationErrorCodes.Single(ec => ec.FieldNumber == fieldNumber &&
                                                 ec.ErrorCode == errorCode && ec.AssemblyName == valobj.ToString()).ErrorMessage;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }
    }
}