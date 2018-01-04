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
        private List<String> formatNames;
        public virtual List<String> FormatNames
        { get; set; }
        private List<String> formatArrays;
        public virtual List<String> FormatArrays
        { get; set; }
        private List<String> formatDefaults;
        public virtual List<String> FormatDefaults
        { get; set; }

        public Item()
        {
            footer = "";
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
