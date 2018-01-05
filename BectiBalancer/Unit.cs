using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BectiBalancer
{
    class Unit : Item
    {
        private String footer;
        public override String Footer
        { get; set; }
        private String header;
        public override String Header
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
            Footer = "" +
             "[_side, _faction, _c, _p, _n, _o, _t, _u, _f, _s, _d, _g]" +
             " call compile preprocessFileLineNumbers" +
             " 'Common\\Config\\Common\\Units\\Set_Units.sqf';";
            Header = "" +
                "/*\n" +
                "//--- This file presents classnames and their values to the mission, to manage units that are in factory menu use factory file.\n" +
                "//--- A tiny error in this file will break the game, be very careful when editing!\n" +
                "*/\n" +
                "/*\n" +
                "//--- EXAMPLE FORMATS-\n" +
                "\n" +
                "_c pushBack 'O_T_Soldier_AA_F'; //--- Class Name\n" +
                "\n" +
                "_p pushBack ''; //--- Picture will be used from config\n" +
                "_p pushBack '\\A3\\EditorPreviews_F\\Data\\CfgVehicles\\Land_Pod_Heli_Transport_04_medevac_F.jpg'; //--- Custom picture if config doesn’t have one\n" +
                "\n" +
                "_n pushBack ''; //--- Name leaving blank will use name from config\n" +
                "_n pushBack ['%1 CustomTextHere']; //--- Default config name + custom\n" +
                "_n pushBack 'Friendly name'; //--- Fully custom name\n" +
                "_n pushBack (format ['Friendly name - Range %1 m',CTI_RESPAWN_MOBILE_RANGE]); //--- Name that will have spawn range based on current upgrade\n" +
                "\n" +
                "_o pushBack 4000; //--- Price of the unit\n" +
                "_t pushBack 5; //--- Time it will take to build in seconds \n" +
                "_u pushBack 2; //--- Upgrade level which unit will be available starts from 0 \n" +
                "_f pushBack CTI_FACTORY_BARRACKS; //--- Which factory unit will be available for purchase\n" +
                "\n" +
                "_s pushBack ''; //-- Special / Script blank will do nothing special \n" +
                "_s pushBack 'service-medic'; //---  Special / Script service-medic will mark vehicles as medical respawn truck\n" +
                "\n" +
                "_d pushBack 0; //--- Distance unit will spawn from factory in meters\n" +
                "_g pushBack 'Woodland'; //--- Filter by Camo\n" +
                "\n" +
                "*/\n" +
                "\n" +
                "\n" +
                "_side = _this;\n" +
                "_faction = 'East';\n" +
                "\n" +
                "_c = []; //--- Classname\n" +
                "_p = []; //--- Picture. 				'' = auto generated.\n" +
                "_n = []; //--- Name. 					'' = auto generated.\n" +
                "_o = []; //--- Price.\n" +
                "_t = []; //--- Build time.\n" +
                "_u = []; //--- Upgrade level needed.    0 1 2 3...\n" +
                "_f = []; //--- Built from Factory.\n" +
                "_s = []; //--- Script\n" +
                "_d = []; //--- Extra Distance (From Factory)\n" +
                "_g = []; //--- Filter by Camo\n" +
                "";
            formatNames = new List<String>
        {
            "ClassName",
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
