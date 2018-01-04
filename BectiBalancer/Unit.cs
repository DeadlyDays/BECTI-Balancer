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
        { get; set; }
        private List<String> formatArrays ;
        public override List<String> FormatArrays
        { get; set; }
        private List<String> formatDefaults ;
        public override List<String> FormatDefaults
        { get; set; }

        public Unit()
        {
            footer = "" +
             "[_side, _faction, _c, _p, _n, _o, _t, _u, _f, _s, _d, _g]" +
             " call compile preprocessFileLineNumbers" +
             " 'Common\\Config\\Common\\Units\\Set_Units.sqf';";
            formatNames = new List<String>
        {
            "Classname",
            "Price",
            "Name",
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
            "_p",//Price
            "_n",//Name
            "_o",//BuildTime
            "_t",//UpgradeLevel
            "_u",//Factory
            "_f",//Script
            "_s",//Script
            "_d",//Distance
            "_g"//Camo
        };
            formatDefaults = new List<String>
        {
            "''",//ClassName
            "''",//Price
            "",//Name
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
