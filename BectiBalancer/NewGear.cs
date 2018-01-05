using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BectiBalancer
{
    class NewGear : NewItem
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

        public NewGear()
        {
            ArrayName = "_u";
            formatNames = new List<String>
            {
                "Enabled",
                "Name",
                "Class",
                "Location",
                "Upgrade",
                "Price",
                "Camo",
                "Type"
            };
            formatDefaults = new List<String>
            {
                "true",/*Enabled*/
                "''",/*Name*/
                "''",/*Class*/
                "[]",/*Location*/
                "0",/*Upgrade*/
                "0",/*Price*/
                "[]",/*Camo*/
                "[]",/*Type*/
            };
            FormatWrapper = new List<String>
            {
                "'"//ClassName
            };
        }
    }
}
