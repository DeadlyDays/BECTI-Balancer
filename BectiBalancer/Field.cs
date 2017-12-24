using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BectiBalancer
{
    class Field
        //A field is either a variable or an array that belongs to a Class
    {
        private String displayName;
        public String DisplayName
        {
            get
            {
                return displayName;
            }
            set
            {
                if (value == null)
                    displayName = "";
                else
                    displayName = value;
            }
        }

        private String defaultValue;
        //If field must have a default value
        protected String DefaultValue
        {
            get
            {
                return defaultValue;
            }
            set
            {
                defaultValue = value;
            }
        }

        private String value;
        //Field Value;Value overriden if empty by defaultValue
        public String Value
        {
            get
            {
                return value;
            }
            set
            {
                if (value != "")
                    this.value = value;
                else
                    this.value = defaultValue;
            }
        }

        private String name;
        //Field Name
        public String Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        //
        private String tags;
        public String Tags
        {
            get
            {
                return tags;
            }
            set
            {
                if (value == null)
                    tags = "";
                else
                    tags = value;
            }
        }

        //
        private List<Group> grouping;
        public List<Group> Grouping
        {
            get
            {
                return grouping;
            }
            set
            {
                if (value == null)
                    grouping = new List<Group>();
                else
                    grouping = value;
            }
        }

        public Field()
            //Empty Constructor
        {
            Name = "DefaultName";
            DefaultValue = "";
            Value = "";
            DisplayName = "";
            Tags = "";
        }

        public Field(String name)
            //Field name
        {
            Name = name;
            DefaultValue = "";
            Value = "";
            DisplayName = "";
            Tags = "";
        }

        public Field(String name, String value)
            //Field name, Field Value
        {
            Name = name;
            DefaultValue = "";
            Value = value;
            DisplayName = "";
            Tags = "";
        }

        public Field(String name, String defaultValue, String value)
            //Field name, Default Value, Field Value
        {
            Name = name;
            DefaultValue = defaultValue;
            Value = value;
            DisplayName = "";
            Tags = "";
        }
        public Field(String name, String defaultValue, String value, String displayName)
        {
            Name = name;
            DefaultValue = defaultValue;
            Value = value;
            DisplayName = displayName;
            Tags = "";
        }
        public Field(String name, String defaultValue, String value, String displayName, String tags)
        {
            Name = name;
            DefaultValue = defaultValue;
            Value = value;
            DisplayName = displayName;
            Tags = tags;
        }

        public void addGrouping(String name)
        {
            Grouping.Add(new Group(name));
        }
        public void addGrouping(String name, int weight)
        {
            Grouping.Add(new Group(name, weight));
        }

        public int returnGroupingWeight(String name)
        {

            foreach(Group x in Grouping)
            {
                if (x.GroupName == name)
                    return x.Weight;
            }
            return 0;
        }

        public List<String> returnGroupingNames()
        {
            List<String> temp = new List<string>();
            foreach(Group x in Grouping)
            {
                temp.Add(x.GroupName);
            }
            return temp;
        }
    }
}
