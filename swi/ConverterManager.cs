using Newtonsoft.Json.Linq;
using swi.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace swi
{
    public class ConverterManager
    {
        private XmlDocument XDoc { get; set; }
        public List<SingleObject> AllObjects { get; set; }
        public List<string> ObjectsNames { get; set; }

        public ConverterManager()
        {
            XDoc = new XmlDocument();
            AllObjects = new List<SingleObject>();
            ObjectsNames = new List<string>();
        }

        public bool LoadFile(string XMLPath)
        {
            string XmlContainer = "";
            if (File.Exists(XMLPath))
            {
                try
                {
                    using (StreamReader streamReader = new StreamReader(XMLPath))
                    {
                        XmlContainer = "<root>\n" + streamReader.ReadToEnd() + "\n</root>";
                        XDoc.LoadXml(XmlContainer);
                    }                                     
                }
                catch (Exception ex)
                {
                    Console.WriteLine("The file is invalid");
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                    Environment.Exit(0);
                    return false;
                }
            }
            else
            {
                Console.WriteLine("The file doesn't exsist");
                Environment.Exit(0);
                return false;
            }

            return true;
        }

        public void ConvertXmlToObject()
        {
            XmlNodeList objList = XDoc.GetElementsByTagName("object");
            ObjectBuilder objectBuilder;
            SingleObject singleObject;

            foreach (XmlNode singleNode in objList)
            {
                objectBuilder = new ObjectBuilder(new ObjectValidation());
                if (objectBuilder.isValid(singleNode, ObjectsNames) == false) continue;
                singleObject = objectBuilder.Build(singleNode);
                AllObjects.Add(singleObject);
                ObjectsNames.Add(singleObject.Name);
            }
        }

        public void ConvertObjectToJson(string path)
        {
            JObject MainObject = new JObject(
            from singleObj in AllObjects
            select new JProperty(singleObj.Name,
             new JObject(
                from obj in singleObj.fields
                select new JProperty(obj.Name, obj.Value)))
        );

            string json = MainObject.ToString();
            File.WriteAllText(path + "\\output.json", json);
            Console.WriteLine("Your file is converted!");
            Console.ReadKey();
        }
    }
}
