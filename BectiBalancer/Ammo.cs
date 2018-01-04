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
        { get; set; }
        private List<String> formatArrays;
        public override List<String> FormatArrays
        { get; set; }
        private List<String> formatDefaults;
        public override List<String> FormatDefaults
        { get; set; }

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
            "RearmTime",
            "MaxMags"
        };
            formatArrays = new List<String>
        {
            "_i",//ClassName
            "_o",//Ordinance
            "_u",//UpgradeLevel
            "_p",//Price
            "_t",//RearmTime
            "_m"//MaxMags
        };
            formatDefaults = new List<String>
        {
            "''",//ClassName
            "''",//Ordinance
            "",//UpgradeLevel
            "",//Price
            "",//RearmTime
            ""//MaxMags
        };
        }

    }
}
