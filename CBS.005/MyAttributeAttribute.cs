using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBS._005
{
    [AttributeUsageAttribute(AttributeTargets.Property)]
    public class MyAttributeAttribute: Attribute
    {
        public MyAttributeAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }

        public string PropertyName { get; }
    }
}
