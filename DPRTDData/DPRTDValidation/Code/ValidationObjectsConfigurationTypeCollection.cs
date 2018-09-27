using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace DPRTDValidation.Code
{
    public class ValidationObjectsConfigurationTypeCollection : ConfigurationElementCollection
    {
        public ValidationObjectsConfigurationType this[int index]
        {
            get
            {
                return base.BaseGet(index) as ValidationObjectsConfigurationType;
            }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ValidationObjectsConfigurationType();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ValidationObjectsConfigurationType)element).SourceType;
        } 


    }
}