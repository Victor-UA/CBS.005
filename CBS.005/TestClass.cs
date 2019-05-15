using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CBS._005
{
    internal class TestClass
    {
        public TestClass(): this("AProp", "BProp") { }
        public TestClass(string a, string b)
        {
            A = a;
            B = b;
        }
        public TestClass(string xml)
        {
            var properties = typeof(TestClass).GetProperties();
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml ?? "")))
            {
                using (var reader = new XmlTextReader(stream))
                {
                    if (reader.ReadToFollowing(nameof(TestClass)))
                    {
                        string lastElementName = "";
                        do
                        {
                            switch (reader.NodeType)
                            {
                                case XmlNodeType.Element:
                                    lastElementName = reader.Name;
                                    break;
                                case XmlNodeType.Text:
                                    foreach (var propertyInfo in properties)
                                    {
                                        var attributes = propertyInfo.GetCustomAttributes(typeof(MyAttributeAttribute), false);
                                        var attribute = (MyAttributeAttribute)attributes[0];
                                        if (attribute.PropertyName.Equals(lastElementName))
                                        {
                                            propertyInfo.SetValue(this, reader.Value);
                                        }
                                    }
                                    break;
                            }
                        } while (reader.Read());
                    }
                }
            }
        }

        [MyAttribute("A")] public string A { get; private set; } 
        [MyAttribute("B")] public string B { get; private set; }



        public override string ToString()
        {
            return $"A: \"{A}\", B: \"{B}\"";
        }
    }
}
