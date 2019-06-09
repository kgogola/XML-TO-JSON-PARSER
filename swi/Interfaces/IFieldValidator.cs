using swi.Data;
using System.Collections.Generic;
using System.Xml;

namespace swi.Interfaces
{
    public interface IFieldValidator
    {
        bool isTypeValid(Field potenitalField);
        bool isFieldContainProperKeyWords(XmlNode node);
        bool isContainNonPrintableSign(Field potentialField);
        bool isFieldNameUnique(string name, List<string> fieldsNames);
    }
}
