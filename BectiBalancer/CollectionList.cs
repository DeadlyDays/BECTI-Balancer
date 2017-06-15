using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BectiBalancer
{
    class CollectionList
    {
        private List<Item> itemList;//List of Items
        public List<Item> ItemList
        {
            get
            {
                return itemList;
            }
            set
            {
                if (value != null)
                    itemList = value;
                else
                    itemList = new List<Item>();
            }
        }

        private String type;//is this a Unit, Gear, etc type file
        public String Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        public CollectionList()
        {
            ItemList = null;
        }

        public void clearCollection()
        {
            itemList = new List<Item>();
            Type = "";
        }
        public List<String> splitStringBy(String str, String delimiter)
            //Takes a String and splits it into a List by delimiter
        {
            if (delimiter != "\n") str = str.Replace("\n", "").Replace("\r", "");
            else str = str.Replace("\r", "");
            return Regex.Split(str, delimiter).ToList(); //str.Split(delimiter).ToList();
        }

        public void addItem(Item item)
        {
            ItemList.Add(item);
            //new Item
        }
        public List<Field> returnFieldsOfItemNamed(String name)
        {
            for(int i = 0; i < ItemList.Count; i++)
            {
                if(ItemList[i].ClassName == name)
                {
                    return ItemList[i].FieldList;
                }
            }
            return new List<Field>();
        }

        public void populateFromCSV(String path, String type)
        {
            String file = System.IO.File.ReadAllText(path, System.Text.Encoding.UTF8);
            //populates String file with entire documents text

            //UNITS
            if (type == "Unit")
                //Populate Units
            {
                Type = "Unit";
                String _c, _p, _n, _o, _t, _u, _f, _s, _d;
                //values to populate

                //int iPointer = 0;//current index Number
                //String sPointer;//current index Value

                List<String> content = splitStringBy(file, ",");
                if (content.Count <= 0)
                    //If file has no content, Exit
                {
                    return;//Empty File, exit
                }

                for(int i = 0; i < content.Count; i++)
                //iterate through content
                {
                    Item newItem = new BectiBalancer.Item(content[i].Replace(" ", ""));
                    
                    //newItem.addField(new Field("_c", ""));
                    newItem.addField(new Field("_p", "", "", "Picture", "\'"));
                    newItem.addField(new Field("_n", "", "", "Name", "\'"));
                    newItem.addField(new Field("_o", "", "", "Price"));
                    newItem.addField(new Field("_t", "", "", "Build Time"));
                    newItem.addField(new Field("_u", "", "", "Upgrade Level"));
                    newItem.addField(new Field("_f", "", "", "Factory"));
                    newItem.addField(new Field("_s", "", "", "Script", "\""));
                    newItem.addField(new Field("_d", "", "", "Extra Distance"));
                    newItem.FieldList[0].DisplayName = "ClassName";
                    newItem.FieldList[0].Tags = "\"";
                    addItem(newItem);
                    //Add each line to item

                }
                
            }
        }

        public void populateFromFormatedFile(String path, String type)
        {
            
            String file = System.IO.File.ReadAllText(path, System.Text.Encoding.UTF8);
            //populates String file with entire documents text

            //UNITS
            if (type == "Unit")
            //Populate Units
            {
                Type = "Unit";
                List<String> _c, _p, _n, _o, _t, _u, _f, _s, _d;
                //values to populate
                _c = new List<String>();
                _p = new List<String>();
                _n = new List<String>();
                _o = new List<String>();
                _t = new List<String>();
                _u = new List<String>();
                _f = new List<String>();
                _s = new List<String>();
                _d = new List<String>();

                int iPointer;//current line Number
                String sPointer;//current line Value
                while (file.Contains("/*"))
                {
                    file = file.Remove(file.IndexOf("/*"), (file.IndexOf("*/") - file.IndexOf("/*") + 2));//Get rid of comment blocks
                }
                    List<String> contentList = splitStringBy(file, "\n");
                
                
                for(int i = 0; i < contentList.Count; i++)
                {
                    //get rid of comments
                    if(contentList[i].Contains("//"))
                    {
                        contentList[i] = contentList[i].Remove(contentList[i].IndexOf("//"));
                    }

                    if (contentList[i].Contains("_c ") && contentList[i].Contains("pushBack") && contentList[i].Contains(";"))
                    {
                        _c.Add(contentList[i].Substring(contentList[i].IndexOf("pushBack") + 9, contentList[i].IndexOf(';') - contentList[i].IndexOf("pushBack") - 9));// from end of pushBack to ;
                    }
                    else if (contentList[i].Contains("_p ") && contentList[i].Contains("pushBack") && contentList[i].Contains(";"))
                    {
                        _p.Add(contentList[i].Substring(contentList[i].IndexOf("pushBack") + 9, contentList[i].IndexOf(';') - contentList[i].IndexOf("pushBack") - 9));// from end of pushBack to ;
                    }
                    else if (contentList[i].Contains("_n ") && contentList[i].Contains("pushBack") && contentList[i].Contains(";"))
                    {
                        _n.Add(contentList[i].Substring(contentList[i].IndexOf("pushBack") + 9, contentList[i].IndexOf(';') - contentList[i].IndexOf("pushBack") - 9));// from end of pushBack to ;
                    }
                    else if (contentList[i].Contains("_o ") && contentList[i].Contains("pushBack") && contentList[i].Contains(";"))
                    {
                        _o.Add(contentList[i].Substring(contentList[i].IndexOf("pushBack") + 9, contentList[i].IndexOf(';') - contentList[i].IndexOf("pushBack") - 9));// from end of pushBack to ;
                    }
                    else if (contentList[i].Contains("_t ") && contentList[i].Contains("pushBack") && contentList[i].Contains(";"))
                    {
                        _t.Add(contentList[i].Substring(contentList[i].IndexOf("pushBack") + 9, contentList[i].IndexOf(';') - contentList[i].IndexOf("pushBack") - 9));// from end of pushBack to ;
                    }
                    else if (contentList[i].Contains("_u ") && contentList[i].Contains("pushBack") && contentList[i].Contains(";"))
                    {
                        _u.Add(contentList[i].Substring(contentList[i].IndexOf("pushBack") + 9, contentList[i].IndexOf(';') - contentList[i].IndexOf("pushBack") - 9));// from end of pushBack to ;
                    }
                    else if (contentList[i].Contains("_f ") && contentList[i].Contains("pushBack") && contentList[i].Contains(";"))
                    {
                        _f.Add(contentList[i].Substring(contentList[i].IndexOf("pushBack") + 9, contentList[i].IndexOf(';') - contentList[i].IndexOf("pushBack") - 9));// from end of pushBack to ;
                    }
                    else if (contentList[i].Contains("_s ") && contentList[i].Contains("pushBack") && contentList[i].Contains(";"))
                    {
                        _s.Add(contentList[i].Substring(contentList[i].IndexOf("pushBack") + 9, contentList[i].IndexOf(';') - contentList[i].IndexOf("pushBack") - 9));// from end of pushBack to ;
                    }
                    else if (contentList[i].Contains("_d ") && contentList[i].Contains("pushBack") && contentList[i].Contains(";"))
                    {
                        _d.Add(contentList[i].Substring(contentList[i].IndexOf("pushBack") + 9, contentList[i].IndexOf(';') - contentList[i].IndexOf("pushBack") - 9));// from end of pushBack to ;
                    }
                }
                if((_c.Count == _p.Count) && (_c.Count == _n.Count) && (_c.Count == _o.Count) && (_c.Count == _t.Count) && (_c.Count == _u.Count) && (_c.Count == _f.Count) && (_c.Count == _s.Count) && (_c.Count == _d.Count))
                for (int i = 0; i < _c.Count; i++)
                {
                    Item newItem = new Item();
                    newItem.addField(new Field("_c", "", _c[i], "Classname", "\""));
                    newItem.addField(new Field("_p", "", _p[i], "Picture", "'"));
                    newItem.addField(new Field("_n", "", _n[i], "Name", "'"));
                    newItem.addField(new Field("_o", "", _o[i], "Price"));
                    newItem.addField(new Field("_t", "", _t[i], "Build time"));
                    newItem.addField(new Field("_u", "", _u[i], "Upgrade level"));
                    newItem.addField(new Field("_f", "", _f[i], "Factory"));
                    newItem.addField(new Field("_s", "", _s[i], "Script", "\""));
                    newItem.addField(new Field("_d", "", _d[i], "Extra Distance"));
                    addItem(newItem);
                }
                

            }
        }

        public String returnFormatedFile(String Type)
        {
            String str = "";
            if(Type == "Unit")
            {
                foreach (Item x in ItemList)
                {
                    for(int i = 0; i < x.FieldList.Count(); i++)
                    {
                        if(x.FieldList[i].Value.IndexOf(x.FieldList[i].Tags) != 0 && x.FieldList[i].Value.LastIndexOf(x.FieldList[i].Tags) != (x.FieldList[i].Value.Count() - 1))
                            //if the value doesn't start and end with proper tags, add the tags
                        {
                            str += x.FieldList[i].Name + " pushBack " + x.FieldList[i].Tags + x.FieldList[i].Value + x.FieldList[i].Tags + ";\r\n";
                        }
                        else
                        //else, paste value as is
                        {
                            str += x.FieldList[i].Name + " pushBack " + x.FieldList[i].Value + ";\r\n";
                        }
                        
                    }
                    
                    str += "\r\n\r\n";
                }
                return str;
            }
            return "";
        }
    }
}
