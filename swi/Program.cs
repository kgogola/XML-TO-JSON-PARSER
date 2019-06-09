using System;
using System.IO;

namespace swi
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory();
            string fileName = "input.xml";
            string XMLpath = path + "\\" + fileName;

            ConverterManager converterManager = new ConverterManager();
            converterManager.LoadFile(XMLpath);
            converterManager.ConvertXmlToObject();
            converterManager.ConvertObjectToJson(path);
        }
    }
}
