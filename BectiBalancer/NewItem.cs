using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BectiBalancer
{
    class NewItem
    {
        private String arrayName;
        public virtual String ArrayName
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
        private List<String> formatWrapper;
        public virtual List<String> FormatWrapper
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
        
        public NewItem()
        {
            ArrayName = "_u";
            FormatNames = new List<String>
            {
                "ClassName"
            };
            FormatDefaults = new List<String>
            {
                "''"//ClassName
            };
            FormatWrapper = new List<String>
            {
                "'"//ClassName
            };
        }


    }
}
