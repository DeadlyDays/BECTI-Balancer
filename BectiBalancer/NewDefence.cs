using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BectiBalancer
{
    class NewDefence : NewItem
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

        public NewDefence()
        {
            ArrayName = "_u";
            formatNames = new List<String>
            {
                "Enabled",
                "Name",
                "Headers",
                "Class",
                "Price",
                "Placement",
                "Tags",
                "Coinmenus",
                "Blacklist",
                "Upgrade",
                "MaxCount",
                "Cooldown",
                "Specials"
            };
            formatDefaults = new List<String>
            {
                "false",//Enabled
                "''",//Name
                "''",//Headers
                "''",//Class
                "0",//Price
                "[]",//Placement
                "[]",//Tags
                "[]",//Coinmenus
                "[]",//Blacklist
                "0",//Upgrade
                "-1",//MaxCount
                "-1",//Cooldown
                "[]"//Specials
            };
            FormatWrapper = new List<String>
            {
                "",//Enabled
                "''",//Name
                "",//Headers
                "''",//Class
                "",//Price
                "[]",//Placement
                "[]",//Tags
                "[]",//Coinmenus
                "[]",//Blacklist
                "",//Upgrade
                "",//MaxCount
                "",//Cooldown
                "[]"//Specials
            };
        }
    }
}
