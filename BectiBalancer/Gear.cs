using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BectiBalancer
{
    class Gear : Item
    {
        //Classname _i
        private String classNameVar;
        public String ClassNameVar
        {
            get
            {
                return classNameVar;
            }
            set
            {
                if (value == null)
                {
                    //addField(new Field("_i", "", "", "Ammo Classname", "\""), true);
                    classNameVar = "";
                }
                else
                {
                    addField(new Field("_i", "\"\"", value, "ClassNameVar", "\""), true);
                    classNameVar = value;
                }

            }
        }
        //_u
        private String upgradeLevel;
        public String UpgradeLevel
        {
            get
            {
                return upgradeLevel;
            }
            set
            {

                if (value == null)
                {
                    upgradeLevel = "";
                    //addField(new Field("_u", "", "", "Upgrade Level"), true);
                }
                else
                {
                    upgradeLevel = value;
                    addField(new Field("_u", "", value, "UpgradeLevel"), true);
                }
            }
        }
        //_p
        private String price;
        public String Price
        {
            get
            {
                return price;
            }
            set
            {

                if (value == null)
                {
                    price = "";
                    //addField(new Field("_p", "", "", "Price"), true);
                }
                else
                {
                    price = value;
                    addField(new Field("_p", "", value, "Price"), true);
                }
            }
        }
        //Build Time _g
        private String filter;
        public String Filter
        {
            get
            {
                return filter;
            }
            set
            {

                if (value == null)
                {
                    filter = "";
                    //addField(new Field("_t", "", "", "Build time"), true);
                }
                else
                {
                    filter = value;
                    addField(new Field("_g", "", value, "Filter", "\""), true);
                }
            }
        }

        private String[] arrayNames;
        public override String[] ArrayNames
        { get { return arrayNames; } set { arrayNames = value; } }

        private String footer;
        public override string Footer { get { return footer; } set { footer = value; } }

        public Gear()
        {
            ClassNameVar = "";
            ClassName = "";
            UpgradeLevel = "";
            Price = "";
            Filter = "";
            ArrayNames = new String[] { "_i", "_u", "_p", "_g" };
            Footer = "[_faction, _i, _u, _p, _g] call compile preprocessFileLineNumbers \"Common\\Config\\Common\\Gear\\Gear_Config_Set.sqf\";";
        }
        public Gear(String name)
        {
            ClassNameVar = name;
            ClassName = name;
            UpgradeLevel = null;
            Price = null;
            Filter = null;
            ArrayNames = new String[] { "_i", "_u", "_p", "_g" };
            Footer = "[_faction, _i, _u, _p, _g] call compile preprocessFileLineNumbers \"Common\\Config\\Common\\Gear\\Gear_Config_Set.sqf\";";
        }

        public override void addField(Field newField, Boolean intern)
        {
            if (!intern)
            {
                if (ClassName == "DefaultClassName")
                    if (newField.Name == "_i")
                    {
                        ClassName = newField.Value;
                        ClassNameVar = newField.Value;
                        return;
                    }

                if (newField.Name == "_i")
                {
                    ClassNameVar = newField.Value;
                    return;
                }
                else if (newField.Name == "_u")
                {
                    UpgradeLevel = newField.Value;
                    return;
                }
                else if (newField.Name == "_p")
                {
                    Price = newField.Value;
                    return;
                }
                else if (newField.Name == "_g")
                {
                    Filter = newField.Value;
                    return;
                }

            }
            else
            {
                for (int i = 0; i < FieldList.Count; i++)
                {
                    if (FieldList[i].Name == newField.Name)
                    {
                        FieldList[i].Value = newField.Value;
                        return;
                    }
                }
                FieldList.Add(newField);
                return;
            }
            for (int i = 0; i < FieldList.Count; i++)
            {
                if (FieldList[i].Name == newField.Name)
                {
                    FieldList[i].Value = newField.Value;
                    return;
                }
            }
            FieldList.Add(newField);
        }

        public new void editValue(String fieldName, String value)
        {
            //Edit the value of all fields with a matching fieldName
            for (int i = 0; i < FieldList.Count; i++)
            {
                if (FieldList[i].DisplayName == fieldName)
                {
                    FieldList[i].Value = value;
                }
            }
            //Also check the static Fields
            switch (fieldName)
            {
                case "ClassNameVar":
                    ClassNameVar = value;
                    break;
                case "ClassName":
                    ClassName = value;
                    break;
                case "UpgradeLevel":
                    UpgradeLevel = value;
                    break;
                case "Price":
                    Price = value;
                    break;
                case "Filter":
                    Filter = value;
                    break;

            }
        }

    }
}
