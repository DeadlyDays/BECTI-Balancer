using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BectiBalancer
{
    class NewAmmo : NewItem
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

        public NewAmmo()
        {
            ArrayName = "_u";
            formatNames = new List<String>
            {
                "Enabled",
                "Name",
                "Type",
                "ClassName",
                "Location",
                "UpgradeLevel",
                "Price",
                "RearmTime",
                "MaxMags",
                "Filters"
            };
            formatDefaults = new List<String>
            {
                "true",/*Enabled*/
                "''",/*Name*/
                "''",/*Type*/
                "''",/*Class*/
                "[]",/*Location*/
                "0",/*Upgrade*/
                "0",/*Price*/
                "0",/*RearmTime*/
                "0",/*MaxMags*/
                "[]"/*Filters*/
            };
            FormatWrapper = new List<String>
            {
                "",/*Enabled*/
                "''",/*Name*/
                "''",/*Type*/
                "''",/*Class*/
                "[]",/*Location*/
                "",/*Upgrade*/
                "",/*Price*/
                "",/*RearmTime*/
                "",/*MaxMags*/
                "[]"/*Filters*/
            };
        }
    }
}
