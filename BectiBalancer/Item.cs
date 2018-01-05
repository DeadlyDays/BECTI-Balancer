using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BectiBalancer
{
    class Item
        //An Item is an object that is/can be defined in a units/gear/etc file
    {
        private String footer;
        public virtual String Footer
        { get; set; }
        private String header;
        public virtual String Header
        { get; set; }
        private List<String> formatNames;
        public virtual List<String> FormatNames
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
        public virtual List<String> FormatArrays
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
        public virtual List<String> FormatDefaults
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

        public Item()
        {
            Footer = "";
            Header = "";
            formatNames = new List<String>
            {
                "Classname"
            };
            formatArrays = new List<String>
            {
                "_c"//ClassName
            };
            formatDefaults = new List<String>
            {
                "''"//ClassName
            };
        }

    }
}
