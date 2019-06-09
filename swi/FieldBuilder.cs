using swi.Data;
using swi.Interfaces;
using System;
using System.Collections.Generic;
using System.Xml;

namespace swi
{
    public class FieldBuilder
    {
        public Field potentialField { get; set; }
        IFieldValidator validator;

        public FieldBuilder(IFieldValidator validator)
        {
            this.validator = validator;
            potentialField = new Field();
        }

        public Field Build(XmlNode Node)
        {
            return potentialField;
        }

        public bool isValid(XmlNode Node, List<string> fieldsNames)
        {
            setPotentialValues(Node);

            if ((validator.isTypeValid(potentialField) == false) ||
            (validator.isContainNonPrintableSign(potentialField) == false) ||
            (validator.isFieldContainProperKeyWords(Node) == false) ||
            (validator.isFieldNameUnique(potentialField.Name, fieldsNames) == false))
            {
                return false;
            }

            return true;
        }

        private void setPotentialValues(XmlNode Node)
        {
            potentialField.Type = Node.SelectSingleNode("type").InnerText;
            potentialField.Value = Node.SelectSingleNode("value").InnerText;
            potentialField.Name = Node.SelectSingleNode("name").InnerText;
        }
    }
}
