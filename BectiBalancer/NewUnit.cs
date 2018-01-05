using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BectiBalancer
{
    class NewUnit : NewItem
    {
        private String arrayName;
        public override String ArrayName
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
        private List<String> formatWrapper;
        public override List<String> FormatWrapper
        {
            get
            {
                return formatWrapper;
            }
            set
            {
                if (value != null)
                    formatWrapper = value;
                else
                    formatWrapper = new List<String>();
            }
        }

        public NewUnit()
        {
            ArrayName = "_u";
            FormatNames = new List<String>
            {
                "Enabled",
                "Name", 
                "Class", 
                "MenuName", 
                "Location",
                "Upgrade", 
                "Price",
                "Time", 
                "Distance",
                "Camo",
                "Type", 
                "Ammmo",      
                "Script",
                "Picture"
            };
            FormatDefaults = new List<String>
            {
                "true",/*Enabled*/
                "''",/*Name*/
                "''",/*Class*/
                "[]",/*MenuName*/
                "[]",/*Location*/
                "0",/*Upgrade*/
                "0",/*Price*/
                "0",/*Time*/
                "0",/*Distance*/
                "[]",/*Camo*/
                "[]",/*Type*/
                "true",/*Ammmo*/
                "''",/*Script*/
                "''"/*Picture*/
            };
            FormatWrapper = new List<String>
            //2 char max, will split and encapsulate
            {
                "",/*Enabled*/
                "''",/*Name*/
                "''",/*Class*/
                "[]",/*MenuName*/
                "[]",/*Location*/
                "",/*Upgrade*/
                "",/*Price*/
                "",/*Time*/
                "",/*Distance*/
                "[]",/*Camo*/
                "[]",/*Type*/
                "",/*Ammmo*/
                "''",/*Script*/
                "''"/*Picture*/
            };
        }
    }
}
