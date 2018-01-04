using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BectiBalancer
{
    class Gear : Item
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

        public Gear()
        {
            footer = "" +
             "[_faction, _i, _u, _p, _g]" +
             " call compile preprocessFileLineNumbers" +
             " \"Common\\Config\\Common\\Gear\\Gear_Config_Set.sqf\";";
            formatNames = new List<String>
        {
            "Classname",
            "UpgradeLevel",
            "Price",
            "Filter"
        };
            formatArrays = new List<String>
        {
            "_i",//ClassName
            "_u",//UpgradeLevel
            "_p",//Price
            "_g"//Filter
        };
            formatDefaults = new List<String>
        {
            "''",//ClassName
            "",//UpgradeLevel
            "",//Price
            "''"//Filter
        };
        }

    }
}
