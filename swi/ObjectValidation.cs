using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;

namespace swi
{
    public class ObjectValidation : IObjectValidator
    {
        public bool isValid { get; set; }

        public bool IsObjContainProperKeyWords(XmlNode node)
        {
            {
                XmlNodeList childNodes = node.ChildNodes;

                foreach (XmlNode potentialNode in childNodes)
                {
                    if (potentialNode.Name != "obj_name" &&
                        potentialNode.Name != "field") return false;
                }

                return true;
            }
        }

        public bool IsObjNameUnique(string name, List<string> objNames)
        {
            if (objNames == null) return true;
            if (objNames.Contains(name)) return false;

            return true;
        }

        public bool IsObjNameValid(string name)
        {
            if (name == "") return false;

            if (Regex.IsMatch(name, @"\p{C}+"))
            {
                return false;
            }

            return true;
        }
    }
}
