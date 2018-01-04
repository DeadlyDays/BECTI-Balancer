using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BectiBalancer
{
    class Ammo : Item
    {
        private String footer = "";
        public override String Footer
        { get; set; }
        private List<String> formatNames;
        public override List<String> FormatNames
        {
            get
            {
                return formatNames;
            }

            set
            {
                if (value != null)
                    formatNames = value;
                else formatNames = new List<String>();
            }
        }
        private List<String> formatArrays;
        public override List<String> FormatArrays
        {
            get
            {
                return formatArrays;
            }

            set
            {
                if (value != null)
                    formatArrays = value;
                else formatArrays = new List<String>();
            }
        }
        private List<String> formatDefaults;
        public override List<String> FormatDefaults
        {
            get
            {
                return formatDefaults;
            }

            set
            {
                if (value != null)
                    formatDefaults = value;
                else formatDefaults = new List<String>();
            }
        }

        public Ammo()
        {
            footer = "" +
             "[_faction, _i, _o, _u, _p, _t]" +
             " call compile preprocessFileLineNumbers" +
             " \"Common\\Config\\Common\\Ammo\\Ammo_Config_Set.sqf\";";
            formatNames = new List<String>
        {
            "Classname",
            "Ordinance",
            "UpgradeLevel",
            "Price",
            "RearmTime"
        };
            formatArrays = new List<String>
        {
            "_i",//ClassName
            "_o",//Ordinance
            "_u",//UpgradeLevel
            "_p",//Price
            "_t"//RearmTime
        };
            formatDefaults = new List<String>
        {
            "''",//ClassName
            "''",//Ordinance
            "",//UpgradeLevel
            "",//Price
            ""//RearmTime
        };
        }

    }
}
