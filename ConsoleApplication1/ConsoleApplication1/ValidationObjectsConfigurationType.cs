using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace ConsoleApplication1
{
    public class ValidationObjectsConfigurationType : ConfigurationElement
    {
        [ConfigurationProperty("SourceType", IsRequired = true)]
        public string SourceType
        {
            get
            {
                return this["SourceType"] as string;
            }
        }

        [ConfigurationProperty("DisplayName", IsRequired = true)]
        public string DisplayName
        {
            get
            {
                return this["DisplayName"] as string;
            }
        }
    }
}