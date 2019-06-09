using System.Collections.Generic;
using System.Xml;

namespace swi
{
    public interface IObjectValidator
    {
        bool IsObjNameValid(string name);
        bool IsObjNameUnique(string name, List<string> objNames);
        bool IsObjContainProperKeyWords(XmlNode node);
    }
}
