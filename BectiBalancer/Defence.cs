using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BectiBalancer
{
    class Defence : Item
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

        public Defence()
        {
            Footer = "" +
             "" +
             " " +
             " ";
            Header = "";
            formatNames = new List<String>
            {
                "Headers",
                "Class",
                "Price",
                "Placement",
                "Tags",
                "Coinmenus",
                "Blacklist",
                "Upgrade",
                "Specials"
            };
            formatArrays = new List<String>
            {
                "_headers",//Headers
                "_classes",//ClassName
                "_prices",//Price
                "_placements",//Placement
                "_categories",//Tags
                "_coinmenus",//Coinmenus
                "_coinblacklist",//Blacklist
                "_upgrade",//Upgrade
                "_specials"//Specials
            };
            formatDefaults = new List<String>
            {
                "",//Headers
                "''",//ClassName
                "0",//Price
                "[]",//Placement
                "''",//Tags
                "[]",//Coinmenus
                "[] ",//Blacklist
                "0 ",//Upgrade
                "[]"//Specials
            };
        }
    }
}
