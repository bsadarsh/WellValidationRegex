using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string strToTest;

            Console.Write("enter the well ");
            strToTest = Console.ReadLine();

            if (UKWellCheckOther(strToTest, "O"))
            {
                Console.WriteLine("{0} is a valid wellname", strToTest);

            }
            else
            {
                Console.WriteLine("{0} is not a valid wellname", strToTest);

            }

            Console.ReadLine();
        }


        //public static bool IsValidInterventionReasonsIPR(string column)
        //{
        //    List<string> inputInterventionReasons = new List<string>();
        //    if (String.IsNullOrEmpty(column))
        //    {
        //        return false;
        //    }
        //    if (column.Contains(','))
        //    {
        //        inputInterventionReasons = column.Split(',').ToList();
        //    }
        //    else if (column.Contains('/'))
        //    {
        //        inputInterventionReasons = column.Split('/').ToList();
        //    }
        //    else
        //    {
        //        inputInterventionReasons.Add(column);
        //    }

        //    var validTypes = new List<string> { "MSS", "MAL", "MWI", "MOB", "MWH", "MTB", "MXX", "PAL", "PSC", "PPF", "PST", "PDA", "PSS", "PXX" };
        //    if (inputInterventionReasons.Any(t => validTypes.All(r => r != t)))
        //    {
        //        return false;
        //    }
        //    return true;
        //}

        //public static bool IsMaximumRangeValidIPR(string column, string column1)
        //{
        //    byte[] columnValues = Encoding.ASCII.GetBytes(column);
        //    byte[] column1Values = Encoding.ASCII.GetBytes(column1);
        //    if (columnValues[0] < column1Values[0])
        //        return false;
        //    else
        //        return true;
        //}

        //  Return boolean value indicating whether a given casing string for DPR Lite is valid
        //public static bool ValidLiteCasing(string column)
        //{
        //    const string start = @"^(([0-9]{1,}[.]{0,1}[0-9]{1,}[\s]{0,1}[/]{0,1}[\s]{0,1}|[0-9]{1,}[.]{0,1}[0-9]{1,}[\s]{0,1}[/]{0,1}[\s]{0,1}){1,})$";
        //    // example is 12.4 / 12.3 
        //    var objvalid = new Regex(start);
        //    return objvalid.IsMatch(column);
        //}


        //public static bool ValidYr(string column)
        //{
        //    const string start = @"^([0-9]{1,4}[\s]{0,1}[Q]{1}[0-9]{1})$";
        //    // example is 12.4 / 12.3 
        //    var objvalid = new Regex(start);
        //    return objvalid.IsMatch(column);
        //}

        //private static bool Valid(string StrToTest)
        //{
        //    //string start = @"^(([0-9]{2}[,]{0,1}[0-9]{2}|([0-9]{2}[-]{0,1}|[0-9]{2}[-]{0,1}[0-9]{2})){1,})$";
        //    //string start1 = @"^(([0-9]{2}|[0-9]{2}[,]{0,1}[0-9]{2}){1,})$";
        //   // Regex reg = new Regex(@"^([0-9]{2}[,]{0,1})+");


        //    string str = @"^(([1-9]{2})?(,([1-9]{2}))*|([0-9]{2}[-]{0,1}|[0-9]{2}[-]{0,1}[0-9]{2}))$";
        //    Regex objvalid = new Regex(str);
        //    //return objvalid.IsMatch(StrToTest);
        //    return objvalid.IsMatch(StrToTest);
        //}


        //private static bool UKWellCheck(string strToTest)
        //{
        //    // Regex objwellname = new Regex("^([L][A-Z 0-9]{2})[-]{1}([^ST])");
        //    //Regex objwellname3 = new Regex("^([-][^ST^st][a])");
        //    //Regex objwellname2 = new Regex(@"(\w{3})");
        //    // Regex objwellname4 = new Regex("^([L][A-Z 0-9]{2})([/]{1})([0-9]{2})([^ST^st])");
        //    //Regex objwellname5 = new Regex("^([L][A-Z 0-9]{2})([/]{1}[0-9]{1,2}[a-z]{1}|[/]{1}[0-9]{1,2})$");
        //    // Regex objwellname6 = new Regex("^([L][A-Z 0-9]{1,2})([/]{1}[0-9]{1,2}[a-z]{1}|[/]{1}[0-9]{1,2})[-]{1}([a-z0-9]{1,2}[,]).$");
        //    /* its works for LA/10-1,.
        //      * AND L10/12a-a,.
        //      * /
        // */
        //    //Regex objwellname7 = new Regex("[^A-Za-z0-9_]");
        //    //   Regex objwellname8 = new Regex(@"[\W]");

        //    //non alphanumeric character

        //    //Regex objwellname9 = new Regex(@"[\/{1})]"); slash stuff

        //    // Regex objwellname10 = new Regex(@"^([L][A-Z 0-9]{1,2})([/]{1}[0-9]{1,2}[a-z]{1}|[/]{1}[0-9]{1,2})[-]{1}(([a-z0-9]{1,2}[,]).$|[A-Za-z0-9_]{1,2})$");

        //    //Regex objwellname11 = new Regex("^([L][A-Z 0-9]{1,2})([/]{1}[0-9]{1,2}[a-z]{1}|[/]{1}[0-9]{1,2})[-]{1}([A-Za-z0-9_]){1,2}$");
        //    // should not contain non alpha numeric

        //    // Regex objwellname12 = new Regex(@"^([L][A-Z 0-9]{1,2})([/]{1}[0-9]{1,2}[a-z]{1}|[/]{1}[0-9]{1,2})[-]{1}(([\d]{1,2}[.])$)");
        //    // this is for like LAA/10a-12.(example)

        //    //Regex objwellname13 = new Regex(@"[\d]"); // only digit

        //    //Regex objwellname14 = new Regex(@"^([0-9]{1,3})([/]{1}[0-9]{1,2}[a-z]{1}|[/]{1}[0-9]{1,2})[-]{1}(([\d]{1,2}[.])$)");
        //    // this for like 12/10a-12.

        //    //Regex objwellname15 = new Regex(@"^([0-9]{1,3})([/]{1}[0-9]{1,2}[a-z]{1}|[/]{1}[0-9]{1,2})[-]{1}([A-Z]{1}[\d]{1,2}|[\d]{1,2}[.])$");
        //    // this examples for 11/30a-B12 or 12/30a-12.

        //    // Regex objwellname16 = new Regex("^(abc(?!def))");


        //    //Regex objwellname17 = new Regex(@"([0-9]{1,3})([/]{1}[0-9]{1,2}[a-z]{1}|[/]{1}[0-9]{1,2})[-]{1}((?!D)(?!E)(?!F)[A-Z]{1}[\d]{1,2}|[\d]{1,2}[.])$");

        //    //Regex objwellname18 = new Regex(@"([0-9]{1,3})([/]{1}[0-9]{1,2}[a-z]{1}|[/]{1}[0-9]{1,2})[-]{1}(?!BT)(?!SLOT)(?!AND)(?!bt)(?!slot)(?!and)([A-Z]{0,}[\d]{1,2}[t-z]{0,}|[\d]{1,2}[.])$");
        //    // this works fine for like the examples 12/03-23. and its dont accept (slot) (and) (st). and examples like
        //    // 2/05-23. 11/30a-B12
        //    // 30/13a-A11Z


        //    Regex objwellname19 = new Regex(@"([0-9]{1,3})([/]{1}[0-9]{1,2}[a-z]{1}|[/]{1}[0-9]{1,2})[-]{1}(?!BT)(?!SLOT)(?!AND)(?!bt)(?!slot)(?!and)([A-Z]{0,}[\d]{1,2}[t-z]{0,}|[\d]{1,2}[.]|[\d]{1,2}[,][\d]{1,2}[t-z]{0,})$");
        //    // its work fine for everything with start from number....




        //    //Regex objwellname20 = new Regex(@"([\d][,])");




        //    return objwellname19.IsMatch(strToTest);

        //    //throw new NotImplementedException();
        //}

        //private static bool NorwayWellCheck(string strToTest)
        //{
        //    string strStart = @"(^(([1-9]|0){1,2})|(([1-9]|0){4}))[/]{1}(([1-9]|0){1,2})[-]{1}";
        //    string strDPRDType = @"([^S]{1})[-]{1}";
        //    string strEndDPRType = @"((\s{0,1})([0-9]{1,2})(\s{0,1})(([A-Ra-z0-9/]{0,}[T-Za-z0-9/]{0,})|[0-9]{0,}[.]{1}))$";
        //    string strEnd = @"((\s{0,1})([0-9]{1,2})(\s{0,1})(([A-Za-z0-9/]{0,})|[0-9]{0,}[.]{1}))$";
        //    string strCPRNonSubSeaTypeEnd = @"((\s{0,1})([0-9]{1,2})(\s{0,1})(([A-Ga-z0-9/]{0,}[I-Za-z0-9/]{0,})|[0-9]{0,}[.]{1}))$";

        //    string strStartNNS = @"^(2([4-9]{1})|3([0-6]{1}))[/]{1}([1-9]|0){1,2}[-]{1}$";
        //    string strStartCNS = @"^(([1-9]|1[0-9]|2[0-3]|560[3-6]))[/]{1}([1-9]|0){1,2}[-]{1}$";
        //    string strStartNW = @"^(620[1-6] |630[0-8]|6[4-6](0[0-9]|10)|6[7-8](0[5-9]|10)|69(0[5-9]|1[0-5])|7[0-2](0[5-9]|[12][0-9]|30))[/]{1}([1-9]|0){1,2}[-]{1}$";

        //    //string strDPRDType = @"((^([1-9]|0){1,2})|(^([1-9]|0){4}))([/]{1}([1-9]|0){1,2}[-]{1})([^S]{1})[-]{1}(^\s*([0-9]{1,2})(?!&)(?!-)|([a-z]|[A-Z]|[0-9]|[/])|([0-9][.]{1}))$";
        //    //string strDPRNonDType = @"((^([1-9]|0){1,2})|(^([1-9]|0){4}))([/]{1}([1-9]|0){1,2}[-]{1})((\s{0,1})([0-9]{1,2})([a-zA-Z0-9]{0,1}[/]{0,1}|[0-9][.]{1}))$";
        //    //string strCPRNonSubSeaType = @"((^([1-9]|0){1,2})|(^([1-9]|0){4}))([/]{1}([1-9]|0){1,2}[-]{1})(^\s*([0-9]{1,2})(?!&)(?!-)|([a-z]|[^H]|[0-9]|[/])|([0-9][.]{1}))$";
        //    //string strCPRSubSeaType = @"((^([1-9]|0){1,2})|(^([1-9]|0){4}))([/]{1}([1-9]|0){1,2}[-]{1})([^S]{1})[-]{1}(^\s*([0-9]{1,2})(?!&)(?!-)|([a-z]|[A-Z]|[0-9]|[/])|([0-9][.]{1}))$";

        //    Regex objwellname1 = new Regex(strStart + strDPRDType + strEndDPRType);
        //    Regex objwellname2 = new Regex(strStart + strEnd);
        //    Regex objwellname3 = new Regex(strStart + strCPRNonSubSeaTypeEnd);

        //    Regex objwellname4 = new Regex(strStartNNS);
        //    Regex objwellname5 = new Regex(strStartCNS);
        //    Regex objwellname6 = new Regex(strStartNW);
        //    return objwellname4.IsMatch(strToTest);
        //}


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


        public static bool UKWellCheckOther(string StrToTest, string RigType)
        {
            bool returnValue = false;
            string rigtype = RigType;

            rigtype = FunRig(rigtype);
            if (rigtype == "O")
            {
                string welloffshore = @"^([0-9]{1,3})([/]{1}[0-9]{1,2}[a-z]{1}|[/]{1}[0-9]{1,2})";
                //This should be a number in the range 1 - 223 (see below for valid block numbers)            

                Regex ObjUKWellCheckRig = new Regex(welloffshore);
                returnValue = ObjUKWellCheckRig.IsMatch(StrToTest);
            }
            if (rigtype == "L")
            {
                string wellLandUK = @"^([L][A-Z0-9]{1,2})([/]{1}[0-9]{1,2}[a-z]{1}|[/]{1}[0-9]{1,2})";
                // This should be  L (uppercase) followed by a number or a letter (uppercase)	

                Regex ObjUKWellCheckRig = new Regex(wellLandUK);
                returnValue = ObjUKWellCheckRig.IsMatch(StrToTest);
            }
            else
            {
                returnValue = false;
            }

            return returnValue;
        }

        private static bool NorwayWellTypeOther(string StrToTest)
        {
            bool returnValue = false;

            string DPRstrStart = @"^((([1-9]|0){1,2})|(([1-9]|0){4}))[/]{1}(([1-9]|0){1,2})[-]{1}";
            // Before the slash (Quadrant number, equivalent to UK block number)
            //This is a number consisting of 1, 2 or 4 digits, with no leading zeroes with / 1 or 2 number

            //string DPRSecound = @"([-]{1}[A-Z]{1}[-]{1})";
            // There should be 2 hyphens, with a single uppercase alpha character between

            //   string DPRThird = @"[A-RT-Z]{1,}$";
            //    string strDPRDType = @"([^S]{1})[-]{1}";

           // string StrDPRDType = @"([^S]{1})";
            // Should not contain S	
           // string strEndDPRType = @"((\s{0,1})([0-9]{1,2})(\s{0,1})(([A-Ra-z0-9/]{0,}[T-Za-z0-9/]{0,})|[0-9]{0,}[.]{1}))$";


            Regex objwellnorway = new Regex(DPRstrStart);

            returnValue = objwellnorway.IsMatch(StrToTest);

            return returnValue;
        }

        private static bool DenmarkWellCheckOther(string StrToTest)
        {
            string wellstart = @"^((55[0-9][0-9]|56[0-9][0-9]|57[0-9][0-9])[/]{1}([0-9]{1,2}))";
            // first string example 5555/12

            string wellmiddle = @"((\s{1}[A-Z]{1,}[-]{1}[1-9]{1}[a-z]{1}))$";           

            Regex objwelldenamrk1 = new Regex(wellstart + wellmiddle);

            return objwelldenamrk1.IsMatch(StrToTest);
        }        

    }
}
