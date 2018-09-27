using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace ConsoleApplication1
{
    public class ValidationObjectsConfiguration : ConfigurationSection
    {
        public static ValidationObjectsConfiguration GetConfig()
        {
            return System.Configuration.ConfigurationManager.GetSection("ValidationObjects") as ValidationObjectsConfiguration;
        }

        [ConfigurationProperty("SourceType", IsRequired = true)]
        public string SourceType
        {
            get
            {
                return this["SourceType"].ToString();
            }
        }

        [ConfigurationProperty("ValidationYear", IsRequired = true)]
        public string ValidationYear
        {
            get { return this["ValidationYear"].ToString(); }
        }

        [ConfigurationProperty("ConfigurationTypes")]
        public ValidationObjectsConfigurationTypeCollection ConfigurationTypes
        {
            get
            {
                return this["ConfigurationTypes"] as ValidationObjectsConfigurationTypeCollection;
            }
        }
    }
}