using swi.Data;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;

namespace swi.Interfaces
{
    public class FieldValidation : IFieldValidator
    {
        public bool isContainNonPrintableSign(Field potentialField)
        {
            if(Regex.IsMatch(potentialField.Name, @"\p{C}+"))
            {
                return false;
            }

            if (Regex.IsMatch(potentialField.Value, @"\p{C}+"))
            {
                return false;
            }

            if (Regex.IsMatch(potentialField.Type, @"\p{C}+"))
            {
                return false;
            }

            return true;
        }

        public bool isFieldContainProperKeyWords(XmlNode node)
        {
            XmlNodeList childNodes = node.ChildNodes;

            foreach (XmlNode potentialNode in childNodes)
            {
                if (potentialNode.Name != "name" &&
                    potentialNode.Name != "value" &&
                    potentialNode.Name != "type") return false;
            }

            return true;
        }

        public bool isFieldNameUnique(string name, List<string> fieldsNames)
        {
            if (fieldsNames == null) return true;
            if (fieldsNames.Contains(name)) return false;

            return true;
        }

        public bool isTypeValid(Field potenitalField)
        {
            if (potenitalField.Type != "string" && potenitalField.Type != "int") return false;
            if (potenitalField.Type == "int")
            {
                if (int.TryParse(potenitalField.Value, out int x) == false)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
