using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BectiBalancer
{
    class Unit : Item
    {
        private String footer = "";
        public override String Footer
        { get; set; }
        private List<String> formatNames ;
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
        private List<String> formatArrays ;
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
        private List<String> formatDefaults ;
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

        public Unit()
        {
            footer = "" +
             "[_side, _faction, _c, _p, _n, _o, _t, _u, _f, _s, _d, _g]" +
             " call compile preprocessFileLineNumbers" +
             " 'Common\\Config\\Common\\Units\\Set_Units.sqf';";
            formatNames = new List<String>
        {
            "Classname",
            "Picture",
            "Name",
            "Price",
            "BuildTime",
            "UpgradeLevel",
            "Factory",
            "Script",
            "Distance",
            "Camo"
        };
            formatArrays = new List<String>
        {
            "_c",//ClassName
            "_p",//Picture
            "_n",//Name
            "_o",//Price
            "_t",//BuildTime
            "_u",//UpgradeLevel
            "_f",//Factory
            "_s",//Script
            "_d",//Distance
            "_g"//Camo
        };
            formatDefaults = new List<String>
        {
            "''",//ClassName
            "''",//Picture
            "",//Name
            "",//Price
            "",//BuildTime
            "",//UpgradeLevel
            "",//Factory
            "",//Script
            "",//Distance
            ""//Camo
        };
        }
    }
}
