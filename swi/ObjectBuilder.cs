using swi.Data;
using swi.Interfaces;
using System;
using System.Collections.Generic;
using System.Xml;

namespace swi
{
    public class ObjectBuilder
    {
        IObjectValidator validator;
        public List<Field> PotentialFields { get; set; }
        public List<string> FieldsNames { get; set; }
        public SingleObject SingleObject;

        public ObjectBuilder(IObjectValidator validator)
        {
            this.validator = validator;
            PotentialFields = new List<Field>();
            SingleObject = new SingleObject();
            FieldsNames = new List<string>();
        }

        public SingleObject Build(XmlNode Node)
        {
            return SingleObject;
        }

        public bool isValid(XmlNode objectNode, List<string> objNames)
        {
            if(setPotentialValue(objectNode) == false) return false;
            XmlNodeList fields = objectNode.SelectNodes("field");

            if ((setAndCheckAmountOfFields(fields) == false) ||
             (validator.IsObjContainProperKeyWords(objectNode) == false) ||
             (validator.IsObjNameUnique(SingleObject.Name, objNames) == false) ||
             (validator.IsObjNameValid(SingleObject.Name) == false))
            {
                return false;
            }

            SingleObject.fields = PotentialFields;

            return true;
        }

        public bool setAndCheckAmountOfFields(XmlNodeList fieldList)
        {
            Field singleField;
            FieldBuilder fieldBuilder;
            foreach (XmlNode _field in fieldList)
            {
                fieldBuilder = new FieldBuilder(new FieldValidation());
                if (fieldBuilder.isValid(_field, FieldsNames) == false) return false;
                singleField = fieldBuilder.Build(_field);
                PotentialFields.Add(singleField);
                FieldsNames.Add(singleField.Name);
            }

            if (PotentialFields.Count == 0) return false;
            return true;
        }

        public bool setPotentialValue(XmlNode node)
        {
            try
            {
                SingleObject.Name = node.SelectSingleNode("obj_name").InnerText;
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
