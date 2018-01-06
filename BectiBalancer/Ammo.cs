using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BectiBalancer
{
    class Ammo : Item
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

        public Ammo()
        {
            Footer = "" +
             "[_faction, _i, _o, _u, _p, _t]" +
             " call compile preprocessFileLineNumbers" +
             " \"Common\\Config\\Common\\Ammo\\Ammo_Config_Set.sqf\";";
            Header = "" +
                "private [\"_faction\", \"_g\", \"_i\", \"_p\", \"_side\", \"_u\"];\n" +
                "\n" +
                "_side = _this;\n" +
                "_faction = \"East\";\n" +
                "\n" +
                "_i = []; //Ammo Classname\n" +
                "_o = []; //Ordinance Type\n" +
                "_u = []; //Upgrade Level\n" +
                "_p = []; //Price\n" +
                "_t = []; //Rearm time per pylon round (seconds) or per mag for turrets\n" +
                "_m = []; //Max Mags per turret\n" +
                "\n" +
                "/*EXAMPLE\n" +
                "_i pushBack \"Rocket_03_AP_F\"; \n" +
                "_o pushBack \"Air\"; //values are Air or Land\n" +
                "_u pushBack 2; \n" +
                "_p pushBack 600; \n" +
                "_t pushBack 2; \n" +
                "_m pushBack 5; \n" +
                "*/\n" +
                "";
            formatNames = new List<String>
        {
            "ClassName",
            "Type",
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
