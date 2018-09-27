using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DrillingValidation
{
    class Program
    {
        static void Main(string[] args)
        {
            string strToTest = string.Empty;
            Console.Write("Enter the Drilling Name ");
            strToTest = Console.ReadLine();

            //if (ISValidDrillingMethod(strToTest))
            //{
            //    Console.WriteLine("{0} is a valid Drilling Name", strToTest);
            //}
            //else
            //{
            //    Console.WriteLine("{0} is not a valid Drilling Name", strToTest);
            //}

            string str = ReplaceMethod(strToTest);

            Console.WriteLine("{0} is a Name", str);
            Console.ReadLine();
        }

        /// <summary>
        /// Validation for Drilling Method
        /// </summary>
        /// <param name="strToTest"></param>
        /// <returns></returns>
        private static bool ISValidDrillingMethod(string strToTest)
        {
            bool status = false;
            string strCode = @"(^[RTAMCDXZEHPYBO]{0,})$";
            Regex objReg = new Regex(strCode);
            int nDIndex = 0;
            int nEIndex = 0;
            string substr = string.Empty;

            if (!string.IsNullOrEmpty(strToTest))  //Checks if the string is empty or null
            {
                if (objReg.IsMatch(strToTest)) //Checks if the string contains any other character other than above
                {
                    if (strToTest.Contains('D')) //Checks if the string contains 'D'
                    {
                        nDIndex = strToTest.IndexOf('D');
                        if (strToTest.Length > nDIndex+1)
                        {
                            substr = strToTest.Substring(nDIndex, 2);
                            if (substr.Contains('X') || substr.Contains('Z')) 
                                status = true;
                            else
                                status = false;
                        }
                    }
                    else if (strToTest.Contains('E')) //Checks if the string contains 'E'
                    {
                        nEIndex = strToTest.IndexOf('E');
                        if (strToTest.Length > nEIndex + 1)
                        {
                            substr = strToTest.Substring(nEIndex, 2);
                            if (substr.Contains('X') || substr.Contains('Z'))
                                status = true;
                            else
                                status = false;
                        }
                    }
                    else
                        status = true;
                }
                else
                    status = false;               
            }
            else
                status = false;

            return status;
        }

        private static string ReplaceMethod(string strToTest)
        {         
            string substr = string.Empty;
            string substr1 = string.Empty;
            //substr1 = Regex.Replace(strToTest, "[,]{2}", ",");
            substr1 = Regex.Replace(strToTest, ",{2,}", ",");
            substr1 = substr1.Trim(',');         

          // substr= strToTest.Replace(",", "");


            return substr1;
        }      
    }
}
