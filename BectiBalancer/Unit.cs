using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BectiBalancer
{
    class Unit : Item
    {
        //Classname _c
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
                    //addField(new Field("_c", "", "", "Classname", "\""), true);
                    classNameVar = "";
                }
                else
                {
                    addField(new Field("_c", "\"\"", value, "ClassNameVar", "\""), true);
                    classNameVar = value;
                }
                
            }
        }

        //Picture _p
        private String picture;
        public String Picture
        {
            get
            {
                return picture;
                
            }
            set
            {
                if (value == null)
                { 
                    picture = "";
                    //addField(new Field("_p", "", "", "Picture", "'"), true);
                }
                else
                { 
                    picture = value;
                    addField(new Field("_p", "''", value, "Picture", "'"), true);
                }
            }
        }
        //Name _n
        private String name;
        public String Name
        {
            get
            {
                return name;
               
            }
            set
            {
                
                if (value == null)
                {
                    name = "";
                    //addField(new Field("_n", "", "", "Name", "'"), true);
                }
                else
                {
                    name = value;
                    addField(new Field("_n", "''", value, "Name", "'"), true);
                }
            }
        }
        //Price _o
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
                    //addField(new Field("_o", "", "", "Price"), true);
                }
                else
                {
                    price = value;
                    addField(new Field("_o", "", value, "Price"), true);
                }
            }
        }
        //Build Time _t
        private String buildTime;
        public String BuildTime
        {
            get
            {
                return buildTime;
                
            }
            set
            {
                
                if (value == null)
                {
                    buildTime = "";
                    //addField(new Field("_t", "", "", "Build time"), true);
                }
                else
                {
                    buildTime = value;
                    addField(new Field("_t", "", value, "BuildTime"), true);
                }
            }
        }
        //Upgrade Level _u
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
                    //addField(new Field("_u", "", "", "Upgrade level"), true);
                }
                else
                {
                    upgradeLevel = value;
                    addField(new Field("_u", "", value, "UpgradeLevel"), true);
                }
            }
        }
        //Factory _f
        private String factory;
        public String Factory
        {
            get
            {
                return factory;
                
            }
            set
            {
                
                if (value == null)
                {
                    factory = "";
                    //addField(new Field("_f", "", "", "Factory"), true);
                }
                else
                {
                    factory = value;
                    addField(new Field("_f", "", value, "Factory"), true);
                }
            }
        }
        //Script _s
        private String script;
        public String Script
        {
            get
            {
                return script;
                
            }
            set
            {
                
                if (value == null)
                {
                    script = "";
                    //addField(new Field("_s", "", "", "Script", "\""), true);
                }
                else
                {
                    script = value;
                    addField(new Field("_s", "\"\"", value, "Script", "\""), true);
                }
            }
        }
        //Distance _d
        private String distance;
        public String Distance
        {
            get
            {
                return distance;
                
            }
            set
            {
                
                if (value == null)
                {
                    distance = "";
                    //addField(new Field("_d", "", "", "Extra Distance"), true);
                }
                else
                {
                    distance = value;
                    addField(new Field("_d", "", value, "Distance"), true);
                }
            }
        }
        //Camo _g
        private String camo;
        public String Camo
        {
            get
            {
                return camo;
                
            }
            set
            {
                
                if (value == null)
                {
                    camo = "";
                    //addField(new Field("_g", "", "", "Camo", "\""), true);
                }
                else
                {
                    camo = value;
                    addField(new Field("_g", "\"\"", value, "Camo", "\""), true);
                }
            }
        }
        

        public Unit()
        {
            ClassNameVar = "";
            Picture = "";
            Name = "";
            Price = "";
            BuildTime = "";
            UpgradeLevel = "";
            Factory = "";
            Script = "";
            Distance = "";
            Camo = "";
        }
        public Unit(String name)
        {
            ClassNameVar = name;
            Picture = null;
            Name = name;
            Price = null;
            BuildTime = null;
            UpgradeLevel = null;
            Factory = null;
            Script = null;
            Distance = null;
            Camo = null;
        }

        public void addField(Field newField, Boolean intern)
        {
            if (!intern)
            {
                if (ClassName == "DefaultClassName")
                    if (newField.Name == "_c")
                    {
                        ClassName = newField.Value;
                        ClassNameVar = newField.Value;
                        return;
                    }

                if (newField.Name == "_c")
                {
                    ClassNameVar = newField.Value;
                    return;
                }
                else if (newField.Name == "_p")
                {
                    Picture = newField.Value;
                    return;
                }
                else if (newField.Name == "_n")
                {
                    Name = newField.Value;
                    return;
                }
                else if (newField.Name == "_o")
                {
                    Price = newField.Value;
                    return;
                }
                else if (newField.Name == "_t")
                {
                    BuildTime = newField.Value;
                    return;
                }
                else if (newField.Name == "_u")
                {
                    UpgradeLevel = newField.Value;
                    return;
                }
                else if (newField.Name == "_f")
                {
                    Factory = newField.Value;
                    return;
                }
                else if (newField.Name == "_s")
                {
                    Script = newField.Value;
                    return;
                }
                else if (newField.Name == "_d")
                {
                    Distance = newField.Value;
                    return;
                }
                else if (newField.Name == "_g")
                {
                    Camo = newField.Value;
                    return;
                }
            }
            else
            {
                for(int i = 0; i < FieldList.Count; i++)
                {
                    if(FieldList[i].Name == newField.Name)
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
            switch(fieldName)
            {
                case "ClassNameVar":
                    ClassNameVar = value;
                    break;
                case "Picture":
                    Picture = value;
                    break;
                case "Name":
                    Name = value;
                    break;
                case "Price":
                    Price = value;
                    break;
                case "BuildTime":
                    BuildTime = value;
                    break;
                case "UpgradeLevel":
                    UpgradeLevel = value;
                    break;
                case "Factory":
                    Factory = value;
                    break;
                case "Script":
                    Script = value;
                    break;
                case "Distance":
                    Distance = value;
                    break;
                case "Camo":
                    Camo = value;
                    break;
            }
        }

    }
}
