using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BectiBalancer
{
    class Group
    {
        //Name of group(sort bby)
        private String groupName;
        public String GroupName
        {
            get
            {
                return groupName;
            }
            set
            {
                if (value == null)
                {
                    groupName = "";
                }
                else
                {
                    groupName = value;
                }
            }
        }
        //weight level(Higher is first)
        private int weight;
        public int Weight
        {
            get
            {
                return weight;
            }
            set
            {
                weight = value;
            }
        }

        public Group()
        {
            GroupName = null;
            Weight = 0;
        }
        public Group(String name)
        {
            GroupName = name;
            Weight = 0;
        }
        public Group(String name, int weight)
        {
            GroupName = name;
            Weight = weight;
        }

    }
}
