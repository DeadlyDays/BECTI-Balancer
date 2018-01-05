using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BectiBalancer
{
    class Gear : Item
    {
        private String footer;
        public override String Footer
        { get; set; }
        private String header;
        public override String Header
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
            Footer = "" +
             "[_faction, _i, _u, _p, _g]" +
             " call compile preprocessFileLineNumbers" +
             " \"Common\\Config\\Common\\Gear\\Gear_Config_Set.sqf\";";
            Header = "" +
                "private [\"_faction\", \"_g\", \"_i\", \"_p\", \"_side\", \"_u\"];\n" +
                "\n" +
                "_side = _this;\n" +
                "_faction = \"East\";\n" +
                "\n" +
                "_i = []; //Gear Classname\n" +
                "_u = []; //Upgrade Level\n" +
                "_p = []; //Price\n" +
                "_g = []; //Filter\n" +
                "\n" +
                "/*EXAMPLE\n" +
                "_i pushBack \"ATMine_Range_Mag\";\n" +
                "_u pushBack 2;\n" +
                "_p pushBack 600;\n" +
                "_g pushBack \"AT Mine\";\n" +
                "*/\n" +
                "\n"; 
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
