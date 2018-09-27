using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WellCheckOthers
{
    public class WellValidationOtherCheck
    {
        private static bool UKOtherWellType(string StrToTest, string rigType)
        {

            bool returnValue = false;

            if (rigType == "O")
            {
                string welloffshore = @"^([0-9]{1,3})([/]{1}[0-9]{1,2}[a-z]{0,1}|[/]{1}[0-9]{1,2})";
                Regex Objwellname2 = new Regex(welloffshore);
                returnValue = Objwellname2.IsMatch(StrToTest);

            }
            if (rigType == "L")
            {
                string wellLandUK = @"^([L][A-Z0-9]{1,2})([/]{1}[0-9]{1,2}[a-z]{0,1}|[/]{1}[0-9]{1,2})";

                Regex Objwellname1 = new Regex(wellLandUK);
                returnValue = Objwellname1.IsMatch(StrToTest);
            }

            return returnValue;
        }

        private static bool NorwayWellCheckOther(string StrToTest)
        {
            bool returnValue = false;
            string strStart = @"(^(([1-9]|0){1,2})|(([1-9]|0){4}))[/]{1}(([1-9]|0){1,2})[-]{1}";
            Regex ObjWellNorwayCheck = new Regex(strStart);
            returnValue = ObjWellNorwayCheck.IsMatch(StrToTest);
            return returnValue;
        }

        private static bool DenmarkWellCheckOther(string StrToTest)
        {
            bool returvalue = false;
            string DenmarkWell1 = @"^((55[0-9][0-9]|56[0-9][0-9]|57[0-9][0-9])[/]{1}([0-9]{1,2}))";

            string DenmarkWell2 = @"(\s{1}|[-]{1})$";

            Regex ObjDenmark = new Regex(DenmarkWell1 + DenmarkWell2);
            returvalue = ObjDenmark.IsMatch(StrToTest);
            return returvalue;

        }

        private static bool USWellCheck(string StrToTest)
        {
            bool returnValue = false;
            string USWellStart = @"^((AC)|(AT)|(DD)|(DC)|(EB)|(EW)|(GB)|(GC)|(KC)|(LL)|(LU)|(MC)|(PI)|(VK)|(WR))";

            string USWellsecound = @"(\s{1}[1-9]{3})$";

            Regex ObjWellUS = new Regex(USWellStart + USWellsecound);

            returnValue = ObjWellUS.IsMatch(StrToTest);

            return returnValue;
        }

        private static bool BrazilWellCheck(string StrToTest)
        {
            bool returnValue = false;

            string Brazil1 = @"^([0-9]{1})";

            string Brazil2 = @"([-]{1}[A-Z]{2,4}[-]{1}[0-9]{1,4}[A-Z]{0,4})";

            string Braziladd = Brazil1 + Brazil2;

            Regex ObjWellBrazil = new Regex(Braziladd);

            returnValue = ObjWellBrazil.IsMatch(StrToTest);

            return returnValue;
        }

    }
}
