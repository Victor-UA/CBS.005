using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CBS._005
{
    internal static class TestConverter
    {
        public static string ToXml(this object @class)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new XmlTextWriter(stream, Encoding.UTF8))
                {                    
                    writer.WriteStartDocument();                    
                    writer.WriteStartElement(@class.GetType().Name);

                    var properties = @class.GetType().GetProperties();
                    foreach (var propertyInfo in properties)
                    {
                        var attributes = propertyInfo.GetCustomAttributes(typeof(MyAttributeAttribute), false);
                        if (attributes.Length > 0)
                        {
                            var attribute = (MyAttributeAttribute)attributes[0];
                            writer.WriteElementString(attribute.PropertyName, propertyInfo.GetValue(@class).ToString());                            
                        }
                    }
                    writer.WriteEndElement();
                    writer.Flush();
                    stream.Seek(0, SeekOrigin.Begin);
                    using (var reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
                }
                
            }
        }        
    }
}
