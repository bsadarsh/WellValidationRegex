using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Reflection;
using CodeEffects.Rule.Attributes;

namespace DPRTDValidation.Code
{
    [Serializable()]
    public class ColumnAttributeMapping
    {
        //  This dictionary stores the UniqueID, Attribute name mapping in that order
        private Dictionary<string, string> _mappings = new Dictionary<string, string>();

        public ColumnAttributeMapping(object validationObject)
        {

            // Get all public static properties of DPRTight type
            PropertyInfo[] propertyInfos = validationObject.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            //  For each of the properties in the validation object
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                //  Get the custom attributes (the displayname of the column)
                var displayAttributes = propertyInfo.GetCustomAttributes(typeof(
                                        FieldAttribute), false);
              

                if (displayAttributes.Length > 0)
                {
                    //  Assign the Displayname and column name from the reflected properties
                    var displayFieldAttribute = (FieldAttribute)displayAttributes[0];

                    try
                    {
                        _mappings.Add(displayFieldAttribute.DisplayName, propertyInfo.Name);
                    }
                    catch (Exception ex)
                    {
                        Console.Write(ex.Message);
                    }
                   
                }
            }
        }

        public Dictionary<string, string> ColumnAttributeMappings
        {
            get { return _mappings; }
            set { _mappings = value; }
        }
    }
}