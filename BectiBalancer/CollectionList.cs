﻿using System;
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

        private List<Unit> unitList;
        public List<Unit> UnitList
        {
            get
            {
                return unitList;
            }
            set
            {
                if (value != null)
                    unitList = value;
                else
                    unitList = new List<Unit>();
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
            UnitList = null;
        }

        public void clearCollection()
        {
            itemList = new List<Item>();
            unitList = new List<Unit>();
            Type = "";
        }
        public List<String> splitStringBy(String str, String delimiter)
            //Takes a String and splits it into a List by delimiter
        {
            if (delimiter != "\n") str = str.Replace("\n", "").Replace("\r", "");
            else str = str.Replace("\r", "");
            return Regex.Split(str, delimiter).ToList(); //str.Split(delimiter).ToList();
        }

        public void addItem(Object item, String type)
        {
            if (type == "Unit")
                UnitList.Add((Unit)item);
            else
                ItemList.Add((Item)item);
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
                    Unit newItem = new Unit(content[i].Replace(" ", ""));
                    
                    //newItem.addField(new Field("_c", ""));
                    newItem.addField(new Field("_p", "", "", "Picture", "\'"), false);
                    newItem.addField(new Field("_n", "", "", "Name", "\'"), false);
                    newItem.addField(new Field("_o", "", "", "Price"), false);
                    newItem.addField(new Field("_t", "", "", "Build Time"), false);
                    newItem.addField(new Field("_u", "", "", "Upgrade Level"), false);
                    newItem.addField(new Field("_f", "", "", "Factory"), false);
                    newItem.addField(new Field("_s", "", "", "Script", "\""), false);
                    newItem.addField(new Field("_d", "", "", "Extra Distance"), false);
                    newItem.addField(new Field("_g", "", "", "Camo", "\""), false);
                    newItem.FieldList[0].DisplayName = "ClassName";
                    newItem.FieldList[0].Tags = "\"";
                    addItem(newItem, type);
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
                List<String> _c, _p, _n, _o, _t, _u, _f, _s, _d, _g;
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
                _g = new List<String>();

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
                    else if (contentList[i].Contains("_g ") && contentList[i].Contains("pushBack") && contentList[i].Contains(";"))
                    {
                        _g.Add(contentList[i].Substring(contentList[i].IndexOf("pushBack") + 9, contentList[i].IndexOf(';') - contentList[i].IndexOf("pushBack") - 9));// from end of pushBack to ;
                    }
                }

                

                if((_c.Count == _p.Count) && (_c.Count == _n.Count) && (_c.Count == _o.Count) && (_c.Count == _t.Count) && (_c.Count == _u.Count)
                    && (_c.Count == _f.Count) && (_c.Count == _s.Count) && (_c.Count == _d.Count) && (_g.Count == _c.Count))
                    for (int i = 0; i < _c.Count; i++)
                    {
                        if (_c[i].IndexOf("\"") == 0)
                            _c[i] = _c[i].Substring(1, _c[i].Count() - 2);//cut first and last char
                        else if (_c[i].IndexOf("'") == 0)
                            _c[i] = _c[i].Substring(1, _c[i].Count() - 2);//cut first and last char
                        if (_p[i].IndexOf("\"") == 0)
                            _p[i] = _p[i].Substring(1, _p[i].Count() - 2);//cut first and last char
                        else if (_p[i].IndexOf("'") == 0)
                            _p[i] = _p[i].Substring(1, _p[i].Count() - 2);//cut first and last char
                        if (_n[i].IndexOf("\"") == 0)
                            _n[i] = _n[i].Substring(1, _n[i].Count() - 2);//cut first and last char
                        else if (_n[i].IndexOf("'") == 0)
                            _n[i] = _n[i].Substring(1, _n[i].Count() - 2);//cut first and last char
                        if (_o[i].IndexOf("\"") == 0)
                            _o[i] = _o[i].Substring(1, _o[i].Count() - 2);//cut first and last char
                        else if (_o[i].IndexOf("'") == 0)
                            _o[i] = _o[i].Substring(1, _o[i].Count() - 2);//cut first and last char
                        if (_t[i].IndexOf("\"") == 0)
                            _t[i] = _t[i].Substring(1, _t[i].Count() - 2);//cut first and last char
                        else if (_t[i].IndexOf("'") == 0)
                            _t[i] = _t[i].Substring(1, _t[i].Count() - 2);//cut first and last char
                        if (_u[i].IndexOf("\"") == 0)
                            _u[i] = _u[i].Substring(1, _u[i].Count() - 2);//cut first and last char
                        else if (_u[i].IndexOf("'") == 0)
                            _u[i] = _u[i].Substring(1, _u[i].Count() - 2);//cut first and last char
                        if (_f[i].IndexOf("\"") == 0)
                            _f[i] = _f[i].Substring(1, _f[i].Count() - 2);//cut first and last char
                        else if (_f[i].IndexOf("'") == 0)
                            _f[i] = _f[i].Substring(1, _f[i].Count() - 2);//cut first and last char
                        if (_s[i].IndexOf("\"") == 0)
                            _s[i] = _s[i].Substring(1, _s[i].Count() - 2);//cut first and last char
                        else if (_s[i].IndexOf("'") == 0)
                            _s[i] = _s[i].Substring(1, _s[i].Count() - 2);//cut first and last char
                        if (_d[i].IndexOf("\"") == 0)
                            _d[i] = _d[i].Substring(1, _d[i].Count() - 2);//cut first and last char
                        else if (_d[i].IndexOf("'") == 0)
                            _d[i] = _d[i].Substring(1, _d[i].Count() - 2);//cut first and last char
                        if (_g[i].IndexOf("\"") == 0)
                            _g[i] = _g[i].Substring(1, _g[i].Count() - 2);//cut first and last char
                        else if (_g[i].IndexOf("'") == 0)
                            _g[i] = _g[i].Substring(1, _g[i].Count() - 2);//cut first and last char


                        Unit newItem = new Unit();
                        newItem.addField(new Field("_c", "", _c[i], "Classname", "\""), false);
                        newItem.addField(new Field("_p", "", _p[i], "Picture", "'"), false);
                        newItem.addField(new Field("_n", "", _n[i], "Name", "'"), false);
                        newItem.addField(new Field("_o", "", _o[i], "Price"), false);
                        newItem.addField(new Field("_t", "", _t[i], "Build time"), false);
                        newItem.addField(new Field("_u", "", _u[i], "Upgrade level"), false);
                        newItem.addField(new Field("_f", "", _f[i], "Factory"), false);
                        newItem.addField(new Field("_s", "", _s[i], "Script", "\""), false);
                        newItem.addField(new Field("_d", "", _d[i], "Extra Distance"), false);
                        newItem.addField(new Field("_g", "", _g[i], "Camo", "\""), false);
                        addItem(newItem, type);
                    }
                

            }
        }

        public String returnFormatedFile(String Type)
        {
            String str = "";
            if(Type == "Unit")
            {
                for (int e = 0; e < UnitList.Count; e++)
                {
                    for(int i = 0; i < UnitList[e].FieldList.Count(); i++)
                    {
                        if(UnitList[e].FieldList[i].Value.IndexOf(UnitList[e].FieldList[i].Tags) != 0 && UnitList[e].FieldList[i].Value.LastIndexOf(UnitList[e].FieldList[i].Tags) != (UnitList[e].FieldList[i].Value.Count() - 1))
                            //if the value doesn't start and end with proper tags, add the tags
                        {
                            str += UnitList[e].FieldList[i].Name + " pushBack " + UnitList[e].FieldList[i].Tags + UnitList[e].FieldList[i].Value + UnitList[e].FieldList[i].Tags + ";\r\n";
                        }
                        else
                        //else, paste value as is
                        {
                            str += UnitList[e].FieldList[i].Name + " pushBack " + UnitList[e].FieldList[i].Value + ";\r\n";
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
