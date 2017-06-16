using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BectiBalancer
{
    class Item
        //An Item is an object that is/can be defined in a units/gear/etc file
    {
        private String className;//The Arma 3 Classname
        public String ClassName
        {
            get
            {
                return className;
            }
            set
            {
                if (value == "")
                    className = "DefaultClassName";
                else
                    className = value;
            }
        }

        private String type;//Type of Item
        public String Type
        {
            get
            {
                string returnString = "";
                if (fieldList.Count > 0)
                returnString = fieldList[0].Name + " pushBack " + fieldList[0].Value;
                for(int i = 1; i < fieldList.Count; i++)
                {
                    returnString += "; " + fieldList[i].Name + " pushBack " + fieldList[i].Value;
                }
                return returnString;
            }
            set
            {
                if (value == null)
                    type = "";
                else
                    type = value;
            }
        }

        

        private List<Field> fieldList;//List of fields belonging to Item
        public List<Field> FieldList
        {
            get
            {
                return fieldList;
            }
            set
            {
                if (value == null)
                    fieldList = new List<Field>();
                else
                    fieldList = value;
            }
        }
        

        public Item()
        {
            ClassName = "";
            FieldList = null;
        }

        public Item(String className)
        {
            ClassName = className;
            FieldList = null;
            addField(new BectiBalancer.Field("_c", ClassName));
        }

        public void addField(Field newField)
        {
            if (ClassName == "DefaultClassName")
                if (newField.Name == "_c")
                    ClassName = newField.Value;
            FieldList.Add(newField);
        }
    }
}
