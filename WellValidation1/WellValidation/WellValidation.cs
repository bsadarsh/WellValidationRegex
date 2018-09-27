using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WellValidation
{
    public static class WellValidation
    {
        static void Main(string[] args)
        {
            string strToTest;
            Console.Write("enter the well ");
            strToTest = Console.ReadLine();


            if (DenmarkWellCheck(strToTest))
            {
                Console.WriteLine("{0} is a valid wellname", strToTest);

            }
            else
            {
                Console.WriteLine("{0} is not a valid wellname", strToTest);

            }

            Console.ReadLine();
        }


        public static bool UKWellCheckDpr(string StrToTest, string WellType, string RigType)
        {
            bool returnValue = false;
            string rigtype = RigType;
            if ("D" == WellType)
            {
                rigtype = FunRig(rigtype);
                if (rigtype == "O")
                {
                    string welloffshore = @"^([0-9]{1,3})([/]{1}[0-9]{1,2}[a-z]{1}|[/]{1}[0-9]{1,2})";
                    //This should be a number in the range 1 - 223 (see below for valid block numbers)

                    //string wellDpr = @"[-]{1}(?!BT)(?!SLOT)(?!AND)(?!bt)(?!slot)(?!and)([A-Z]{0,}[\d]{1,2}[t-z]{0,}|[\d]{1,2}[.]|[\d]{1,2}[,][\d]{1,2}[t-z]{0,}|[A-Z]{1})$";

                    string WellOffshoreDpr = @"[-]{1}([A-Z]{1,})$";
                    //Hypen and no bt or slot and in any case A-Z 1 OR MANY SPACE and t-z

                    Regex ObjUKWellCheckRig = new Regex(welloffshore + WellOffshoreDpr);

                    returnValue = ObjUKWellCheckRig.IsMatch(StrToTest);
                }
            }
            else
            {
                rigtype = FunRig(rigtype);
                if (rigtype == "L")
                {

                    string wellLandUK = @"^([L][A-Z0-9]{1,2})([/]{1}[0-9]{1,2}[a-z]{1}|[/]{1}[0-9]{1,2})";
                    // This should be  L (uppercase) followed by a number or a letter (uppercase)	




                    string wellDpr = @"[-]{1}(?!BT)(?!SLOT)(?!AND)(?!bt)(?!slot)(?!and)([A-Z]{0,}[\d]{1,2}[t-z]{0,}|[\d]{1,2}[.]|[\d]{1,2}[,][\d]{1,2}[t-z]{0,}|[A-Z]{1})$";
                    //Hypen and no bt or slot and in any case A-Z 1 OR MANY SPACE and t-z
                    Regex ObjUKWellCheckRig = new Regex(wellLandUK + wellDpr);


                    returnValue = ObjUKWellCheckRig.IsMatch(StrToTest);
                }
                else
                {
                    returnValue = false;
                }
            }
            return returnValue;
        }

        private static string FunRig(string rig)
        {
            string returnValue = string.Empty;

            if (string.IsNullOrEmpty(rig))
            {
                return null;
            }
            if (rig == "LA" || rig == "LO" || rig == "HR" || rig == "OL")
            {
                returnValue = "L";
            }
            else if (rig == "JK" || rig == "JP" || rig == "HP" || rig == "PL" || rig == "PT" || rig == "PS" || rig == "SP" || rig == "TB" || rig == "TS" || rig == "TJ" || rig == "SS" || rig == "DS" || rig == "OO")
            {
                returnValue = "O";
            }
            else if (rig == "SU" || rig == "BA")
            {
                returnValue = "S";
            }
            return returnValue;
        }

        private static bool DenmarkWellCheck(string StrToTest)
        {
            string wellstart = @"^((55[0-9][0-9]|56[0-9][0-9]|57[0-9][0-9])[/]{1}([0-9]{1,2}))";
            // first string example 5555/12

            string wellmiddle = @"((\s{1}[A-Z]{1,}[-]{1}[1-9]{1}[a-z]{1})|([-]{1}[\d]{1,2}[.]{1}|[-]{1}[\d]{1,2}[a-z]{1}))$";


            // secound string example [space] ,[alpha upper case] ,[hypen],[number]  and aplha lower case.


            //string wellend = @"($)";

            Regex objwelldenamrk1 = new Regex(wellstart + wellmiddle);

            return objwelldenamrk1.IsMatch(StrToTest);
        }

        private static bool NorwayWellTypeDPR(string StrToTest, string WellType)
        {
            bool returnValue = false;
            if (WellType == "D")
            {
                string DPRstrStart = @"^((([1-9]|0){1,2})|(([1-9]|0){4}))[/]{1}(([1-9]|0){1,2})";
                // Before the slash (Quadrant number, equivalent to UK block number)
                //This is a number consisting of 1, 2 or 4 digits, with no leading zeroes with / 1 or 2 number

                string DPRSecound = @"([-]{1}[A-Z]{1}[-]{1})";
                // There should be 2 hyphens, with a single uppercase alpha character between

                //   string DPRThird = @"[A-RT-Z]{1,}$";
                //    string strDPRDType = @"([^S]{1})[-]{1}";

                string StrDPRDType = @"([^S]{1})";
                // Should not contain S	
                string strEndDPRType = @"((\s{0,1})([0-9]{1,2})(\s{0,1})(([A-Ra-z0-9/]{0,}[T-Za-z0-9/]{0,})|[0-9]{0,}[.]{1}))$";


                Regex objwellnorway = new Regex(DPRstrStart + DPRSecound + StrDPRDType + strEndDPRType);

                returnValue = objwellnorway.IsMatch(StrToTest);
                //returnvalue = true;
            }

            return returnValue;
        }

        private static bool NorwayWellTypeCPR(string StrToTest, string WellType)
        {
            bool returnValue = false;
            if (WellType == "S")
            {
                string CPRTypeSubSeaFirst = @"^((([1-9]|0){1,2})|(([1-9]|0){4}))[/]{1}(([1-9]|0){1,2})";
                // Before the slash (Quadrant number, equivalent to UK block number)
                //This is a number consisting of 1, 2 or 4 digits, with no leading zeroes with / 1 or 2 number
                string CPRTypeSubSeaSecound = @"([-]{1})";
                // if cpr it should contain one hypen
                string CPRTypeSubSeaThird = @"([A-GI-Z]{0,})$";
                // should not contain H
                Regex ObjwellNorwayCpr = new Regex(CPRTypeSubSeaFirst + CPRTypeSubSeaSecound + CPRTypeSubSeaThird);

                returnValue = ObjwellNorwayCpr.IsMatch(StrToTest);
            }
            else
            {
                string CPRTypeNotSubSeaFirst = @"^((([1-9]|0){1,2})|(([1-9]|0){4}))[/]{1}(([1-9]|0){1,2})";
                // Before the slash (Quadrant number, equivalent to UK block number)
                //This is a number consisting of 1, 2 or 4 digits, with no leading zeroes with / 1 or 2 number
                string CPRTypeNotSubSeaSecound = @"([-]{1})";
                // if cpr it should contain one hypen
                string CPRTypeSubNotSubSeaThird = @"([A-Z]{0,})$";
                // IT CAN CONTAIN H
                Regex ObjWellNorwayCprNOTSubSea = new Regex(CPRTypeNotSubSeaFirst + CPRTypeNotSubSeaSecound + CPRTypeSubNotSubSeaThird);

                returnValue = ObjWellNorwayCprNOTSubSea.IsMatch(StrToTest);
            }
            return returnValue;
        }        

        private static bool USWellCheckWellType(string StrToTest, string Welltype)
        {
            string WellTypeUS = "D";
            bool returnValue = false;

            if (WellTypeUS == Welltype)
            {
                string USWellstart = @"^((AC)|(AT)|(DD)|(DC)|(EB)|(EW)|(GB)|(GC)|(KC)|(LL)|(LU)|(MC)|(PI)|(VK)|(WR))";
                //RULE 1 Field names should be above letter.

                string USWellsecound = @"(\s{1}[1-9]{3})";
                // Block number 3 digit between the first and secound space)

                string USWellthird = @"((\s{1})([A-Z]{1,2}[1-9]{3}))";
                // This is for DPR check

                string USWellend = @"(\s{1})((ST)[0-9]{2}(BP)[0-9]{2})$";
                // Ending bit of the well name

                Regex objwellUS1 = new Regex(USWellstart + USWellsecound + USWellthird + USWellend);

                returnValue = objwellUS1.IsMatch(StrToTest);

            }
            else
            {
                string USWellstartS = @"^((AC)|(AT)|(DD)|(DC)|(EB)|(EW)|(GB)|(GC)|(KC)|(LL)|(LU)|(MC)|(PI)|(VK)|(WR))";
                //RULE 1 Field names should be above letter.

                string USWellsecoundS = @"(\s{1}[1-9]{3})";
                // Block number 3 digit between the first and secound space)

                string USWellthirdS = @"((\s{1})([A-Z0-9]{0,}))";
                // This is for DPR check

                string USWellendS = @"(\s{1})((ST)[0-9]{2}(BP)[0-9]{2})$";
                // Ending bit of the well name

                Regex objwellUS11 = new Regex(USWellstartS + USWellsecoundS + USWellthirdS + USWellendS);

                returnValue = objwellUS11.IsMatch(StrToTest);
            }
            return returnValue;
        }

        private static bool BrazilWellTypeDPR(string StrToTest, string WellType)
        {
            bool returnValue = false;
            string DRPWellType = string.Empty;
            string WellDPRTypeD = "D";
            string WellDPRTypeA = "A";
            string WellDPRTypeE = "E";
            if (WellDPRTypeD == WellType || WellDPRTypeA == WellType || WellDPRTypeE == WellType)
            {
                string DPRStart = @"^([7-9]{1}|[1-6]{1})";
                // Dpr D Range from 7-9 and A or E range from 1-6 
                string Commonstring = @"([-]{1}[A-Z]{2,4}[-]{1}[0-9]{1,4}[A-Z]{0,4})";
                // ([-]{1}[A-Z]{2}[S])
                string DPRSecound = @"(([-]{1}[A-Z]{2}[S])|((\s{0,1}|[-]{0,1})([A-Z]{1,2})))$";
                DRPWellType = DPRStart + Commonstring + DPRSecound;

                Regex ObjWellBrazilDPR = new Regex(DRPWellType);

                returnValue = ObjWellBrazilDPR.IsMatch(StrToTest);
            }
            else
            {

                returnValue = false;
            }
            return returnValue;
        }

        private static bool BrazilWellTypeCPR(string StrToTest, string WellType)
        {
            string CPRWellType = string.Empty;
            string subseacpr = "S";
            bool returnValue = false;
            if (subseacpr == WellType)
            {
                string CPRStart = @"^([7-9]{1})";

                //string DPRStart = @"^([7-9]{1}|[1-6]{1})";
                // Dpr D Range from 7-9 and A or E range from 1-6 
                string Commonstring = @"([-]{1}[A-Z]{2,4}[-]{1}[0-9]{1,4}[A-Z]{0,4})";
                // ([-]{1}[A-Z]{2}[S])
                // string DPRSecound = @"(([-]{1}[A-Z]{2}[S])|((\s{0,1}|[-]{0,1})([A-Z]{1,2})))$";

                string CPRSecound = @"(([-]{1}[A-Z]{2}[S])|((\s{0,1}|[-]{0,1})([A-Z]{1,2})))$";
                //DRPWellType = DPRStart + Commonstring + DPRSecound;

                CPRWellType = CPRStart + Commonstring + CPRSecound;
                Regex ObjWellBrazilDPR = new Regex(CPRWellType);

                returnValue = ObjWellBrazilDPR.IsMatch(StrToTest);
            }
            else
            {
                returnValue = false;
            }
            return returnValue;
        }
    }
}
