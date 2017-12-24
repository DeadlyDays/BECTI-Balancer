using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BectiBalancer
{
    class Tag
    {
        //Tag Name
        private String tagName;
        public String TagName
        {
            get
            {
                return tagName;
            }
            set
            {
                if (value == null)
                {
                    tagName = "";
                }
                else
                {
                    tagName = value;
                }

            }
        }
        //Tag Value
        private String tagValue;
        public String TagValue
        {
            get
            {
                return tagValue;
            }
            set
            {
                if (value == null)
                {
                    tagValue = "";
                }
                else
                {
                    tagValue = value;
                }

            }
        }

        //Is this a comment line
        private Boolean isComment;
        public Boolean IsComment
        {
            get
            {
                return isComment;
            }
            set
            {
                isComment = value;
            }
        }

        //Should we output this value to code
        private Boolean hideValue;
        public Boolean HideValue
        {
            get
            {
                return hideValue;
            }
            set
            {
                hideValue = value;
            }
        }

        public Tag()
        {
            TagName = null;
            TagValue = null;
        }

        
        public Tag(String tag)
        {
            parseTag(tag);
        }

        //name:value
        public void parseTag(String tag)
        {
            String[] tagV = new String[2];
            if (tag.Contains(":"))
            {
                //Split into Name and Value
                tagV = Regex.Split(tag, ":");

                TagName = tagV[0];
                TagValue = tagV[1];
            }
            else
            {
                TagName = null;
                TagValue = null;
            }

        }
    }
}
