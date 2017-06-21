using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BectiBalancer
{
    class Ammo : Item
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
                    //addField(new Field("_i", "", "", "Ammo Classname", "\""), true);
                    classNameVar = "";
                }
                else
                {
                    addField(new Field("_i", "\"\"", value, "Ammo Classname", "\""), true);
                    classNameVar = value;
                }
                
            }
        }

        //Picture _p
        private String ordinanceLevel;
        public String OrdinanceLevel
        {
            get
            {
                return ordinanceLevel;
            }
            set
            {
                if (value == null)
                {
                    ordinanceLevel = "";
                    //addField(new Field("_o", "", "", "Ordinance Type", "\""), true);
                }
                else
                {
                    ordinanceLevel = value;
                    addField(new Field("_o", "\"\"", value, "Ordinance Type", "\""), true);
                }
            }
        }
        //Name _n
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
                    addField(new Field("_u", "", value, "Upgrade Level"), true);
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
                    //addField(new Field("_p", "", "", "Price"), true);
                }
                else
                {
                    price = value;
                    addField(new Field("_p", "", value, "Price"), true);
                }
            }
        }
        //Build Time _t
        private String rearmTimeRound;
        public String RearmTimeRound
        {
            get
            {
                return rearmTimeRound;
            }
            set
            {
                
                if (value == null)
                {
                    rearmTimeRound = "";
                    //addField(new Field("_t", "", "", "Build time"), true);
                }
                else
                {
                    rearmTimeRound = value;
                    addField(new Field("_t", "", value, "Rearm time per round"), true);
                }
            }
        }
        

        public Ammo()
        {
            ClassNameVar = "";
            ClassName = "";
            OrdinanceLevel = "";
            UpgradeLevel = "";
            Price = "";
            RearmTimeRound = "";
        }
        public Ammo(String name)
        {
            ClassNameVar = name;
            ClassName = name;
            OrdinanceLevel = null;
            UpgradeLevel = null;
            Price = null;
            RearmTimeRound = null;
        }

        public void addField(Field newField, Boolean intern)
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
                else if (newField.Name == "_o")
                {
                    OrdinanceLevel = newField.Value;
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
                else if (newField.Name == "_t")
                {
                    RearmTimeRound = newField.Value;
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

    }
}
