using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBS._005
{
    class Program
    {
        // Create test class [1]
        // Create additional class for class [1] to xml string transformation
        // {A:"AProp", B:"BProp"} ->
        //  <?xml version="1.0" encoding="utf-16"?><TestClass><A>AProp</A><B>BProp</B></TestClass>
        //
        // and opposite way - xml string to class [1]
        static void Main(string[] args)
        {
            var testClass = new TestClass();
            Console.WriteLine(testClass);
            var xml = testClass.ToXml();
            Console.WriteLine(xml);
            var testClassCloned = new TestClass(xml);
            Console.WriteLine(testClassCloned);
            Console.ReadKey();
        }
    }
}
