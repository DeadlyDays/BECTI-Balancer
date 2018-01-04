using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Data;

namespace BectiBalancer
{
    
    class CollectionList
    {
        //Store stuff in Dataset instead
        private DataSet data;
        public DataSet Data
        { get; set; }

        private DataTable displayTable;
        public DataTable DisplayTable
        { get; set; }

        private DataView view;
        public DataView View
        { get; set; }

        
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

        private static Unit myUnit = new Unit();
        private static Gear myGear = new Gear();
        private static Ammo myAmmo = new Ammo();

        public CollectionList()
        {
            Data = new DataSet();
            Data.Tables.Add(new DataTable("ItemDB"));
            Data.Tables[0].Columns.Add("UID", typeof(int));
            Data.Tables[0].Columns.Add("ClassName", typeof(String));
            Data.Tables[0].Columns.Add("TypeDB_Type", typeof(String));

            Data.Tables.Add(new DataTable("TypeDB"));
            Data.Tables[1].Columns.Add("Type", typeof(String));
            Data.Tables[1].Columns.Add("Footer", typeof(String));

            Data.Tables.Add(new DataTable("FieldsDB"));
            Data.Tables[2].Columns.Add("UID", typeof(int));
            Data.Tables[2].Columns.Add("Name", typeof(String));
            Data.Tables[2].Columns.Add("Value", typeof(String));
            Data.Tables[2].Columns.Add("Default", typeof(String));
            Data.Tables[2].Columns.Add("ArrayName", typeof(String));
            Data.Tables[2].Columns.Add("Hidden", typeof(int));
            Data.Tables[2].Columns.Add("ItemDB_UID", typeof(int));

            Data.Tables.Add(new DataTable("TagsDB"));
            Data.Tables[3].Columns.Add("UID", typeof(int));
            Data.Tables[3].Columns.Add("Name", typeof(String));
            Data.Tables[3].Columns.Add("Value", typeof(String));
            Data.Tables[3].Columns.Add("Hidden", typeof(int));
            Data.Tables[3].Columns.Add("ItemDB_UID", typeof(int));

            //Between TypeDB and ItemDB
            Data.Relations.Add(new DataRelation("i_TypeDB_Type", 
                Data.Tables[1].Columns[0], 
                Data.Tables[0].Columns[2]
                ));
            //Between TagsDB and ItemDB
            Data.Relations.Add(new DataRelation("t_ItemDB_UID",
                Data.Tables[0].Columns[0],
                Data.Tables[3].Columns[4]
                ));
            //Between FieldsTB and ItemDB
            Data.Relations.Add(new DataRelation("f_ItemDB_UID",
                Data.Tables[0].Columns[0],
                Data.Tables[2].Columns[6]
                ));

            //Add Types
            DataRow row = Data.Tables[1].NewRow();
            row.SetField("Type", "Unit");
            row.SetField("Footer", myUnit.Footer);
            Data.Tables[1].Rows.Add(row);
            row = Data.Tables[1].NewRow();
            row.SetField("Type", "Gear");
            row.SetField("Footer", myGear.Footer);
            Data.Tables[1].Rows.Add(row);
            row = Data.Tables[1].NewRow();
            row.SetField("Type", "Ammo");
            row.SetField("Footer", myAmmo.Footer);
            Data.Tables[1].Rows.Add(row);


            Type = "";
        }

        public void clearCollection()
        {
            Data = new DataSet();
            DisplayTable = new DataTable();
            View = new DataView();
            Type = "";
        }
        
        //
        //--Create a String that is formated for a config file
        //
        public StringBuilder formatedOutput(String version)
        {

            //We want ability to output both current format, and future format

            switch (version)
            {
                case "Current":
                    
                    break;
                case "Future":
                    
                    break;
                default:

                    break;
            }

            return new StringBuilder();
        }
        public StringBuilder curFormat(StringBuilder str)
        {
            return new StringBuilder();
        }
        
        //
        //--Read a formated String, as if from a config file
        //
        public void populateData(String input, Boolean version)
        {
            //
            //---Wipe Comment Blocks, Comment blocks are just comments we dont give no fuck
            //
            if (input.Contains("/*") && input.Contains("/*"))
                while (input.Contains("/*") && input.Contains("/*"))
                {
                    int startIndex, iCount;
                    startIndex = input.IndexOf("/*");
                    if (startIndex > 0)
                        if (input[startIndex - 1] == '/')
                        {
                            input = input.Insert(startIndex + 1, "_");
                            continue;
                        }
                    int istart = input.IndexOf("/*");
                    int iend = input.IndexOf("*/") + 2;
                    iCount = (iend - istart);
                    if (iCount > 0 /*&& startIndex > 0*/)
                        input = input.Remove(startIndex, iCount);//Get rid of comment blocks

                }

            //
            //--Type
            //
            String unitPattern, gearPattern, ammoPattern;
            unitPattern = ".*";
            for (int i = 0; i < (myUnit.FormatArrays).Count; i++)
            {
                unitPattern +=
                    "(" +
                    myUnit.FormatArrays[i] +
                    " pushBack (.*);.*\n){1}.*"
                    ;

            }
            
            gearPattern = ".*";
            for (int i = 0; i < myGear.FormatArrays.Count; i++)
            {
                gearPattern +=
                    "(" +
                    myGear.FormatArrays[i] +
                    " pushBack (.*);.*\n){1}.*"
                    ;

            }
            ammoPattern = ".*";
            for (int i = 0; i < myAmmo.FormatArrays.Count; i++)
            {
                ammoPattern +=
                    "(" +
                    myAmmo.FormatArrays[i] +
                    " pushBack (.*);.*\n){1}.*"
                    ;

            }

            //Dynamic Type Detection
            String type = "";
            if (Regex.IsMatch(input, @unitPattern))
            {
                type = "Unit";
            }
            else if(Regex.IsMatch(input, gearPattern))
            {
                type = "Gear";
            }
            else if(Regex.IsMatch(input, ammoPattern))
            {
                type = "Ammo";
            }
            else
            {
                type = "Other";
            }

            //
            //--Tags
            //
            String pattern =
                @"(?:(?:.*)" +//specify front of search has nothing above it, or a ; with any amount of characters between it and next part
                @"(?:\/\/\[Tag;(?:.+?):(?:.+?)?\])" +//find 0 or more instances of tags [Tag;tagName:tagValue]
                @"(?:.*.*?\n.*?))*"//space between tag and everything else
                ;

            switch (version)
            {
                case false://Current
                    switch (type)
                    {
                        case "Unit":
                            currDataPop<Unit>(pattern, input);
                            break;
                        case "Gear":
                            currDataPop<Gear>(pattern, input);
                            break;
                        case "Ammo":
                            currDataPop<Ammo>(pattern, input);
                            break;
                        default:
                            break;
                    }
                    break;
                case true://Future

                    break;
                default:

                    break;
            }

        }
        public void currDataPop<T>(String pattern, String input) where T : Item, new()
        {

            //
            //--Values
            //

            for(int p = 0; p < new T().FormatArrays.Count;p++)
            {
                pattern += @"(?:.*)" + (new T().FormatArrays[p]) + @" pushBack (?<" + new T().FormatNames[p] + @">.+?);.*?\n.*?";
            }
            //Every item
            MatchCollection matchCol = Regex.Matches(input, @pattern, RegexOptions.None, Regex.InfiniteMatchTimeout);

            T thisClass;
            //Populate Items
            if (matchCol.Count > 0)
                foreach (Match p in matchCol)//Iterate through each item p in matchCol
                {
                    thisClass = new T();
                    //Make  new row for ItemDB
                    DataRow itemRow = Data.Tables[0].NewRow();
                    //new UID
                    int itemUID = Data.Tables[0].Rows.Count;
                    //Set UID
                    itemRow.SetField("UID", itemUID);
                    //Set Classname
                    itemRow.SetField("ClassName", p.Groups[1].Value);
                    //Set TypeDB_Type
                    itemRow.SetField("TypeDB_Type", typeof(T).Name.ToString());
                    //Commit Row
                    Data.Tables[0].Rows.Add(itemRow);
                    //
                    //---Grabs Tags
                    //
                    String tagPattern =
                        @"(?:(?:.*)" +//specify front of search has nothing above it, or a ; with any amount of characters between it and next part
                        @"(?<tag>\/\/\[Tag;(?<tagName>.+?):(?<tagValue>.+?)?\])" +//find 0 or more instances of tags [Tag;tagName:tagValue]
                        @"(?:.*.*?\n*?))+"//space between tag and everything else
                        ;
                    if (p.Captures.Count > 0)
                        foreach (Capture c in p.Captures)
                        //This will only go once as there is only 1 capture per match
                        {
                            if (Regex.IsMatch(c.Value, @tagPattern))
                            {
                                //divide the matches
                                MatchCollection tagCollection = Regex.Matches(c.Value, @tagPattern);
                                //add tags to Item
                                if (tagCollection.Count > 0)
                                    foreach (Match m in tagCollection)
                                    {
                                        //Make new row for TagsDB
                                        DataRow tagsRow = Data.Tables[3].NewRow();
                                        //Set UID
                                        itemRow.SetField("UID", Data.Tables[3].Rows.Count);
                                        //
                                        //--Group 2 is tagName, Group 3 is tagValue
                                        //
                                        //Set Name
                                        itemRow.SetField("Name", m.Groups[2].Value);
                                        //Set Value
                                        itemRow.SetField("Value", m.Groups[3].Value);
                                        //Set Hidden
                                        //Set ItemDB_UID
                                        itemRow.SetField("ItemDB_UID", itemUID);

                                        //Commit Row
                                        Data.Tables[3].Rows.Add(tagsRow);
                                    }
                            }
                        }

                    //
                    //---This grabs all the property fields and populates every item found
                    //

                    for (int i = 0; i < (new T().FormatArrays.Count); i++)//Iterate through each property i in matchCol.Group
                    {
                        //This is where we store fields

                        //Create new row in FieldsTB
                        DataRow fieldRow = Data.Tables[2].NewRow();
                        //Set UID
                        fieldRow.SetField("UID", Data.Tables[2].Rows.Count);
                        //Set Name
                        fieldRow.SetField("Name", new T().FormatNames[i]);
                        //Set Value
                        fieldRow.SetField("Value", p.Groups[i+1].Value);
                        //Set Default
                        fieldRow.SetField("Default", new T().FormatDefaults[i]);
                        //Set ArrayName
                        fieldRow.SetField("ArrayName", new T().FormatArrays[i]);
                        //Set Hidden
                        //Set ItemDB_UID
                        fieldRow.SetField("ItemDB_UID", itemUID);

                        //Commit Row
                        Data.Tables[2].Rows.Add(fieldRow);
                    }

                    //Data.Tables[0].Rows.Add(row);
                    //addItem(thisClass, typeof(T).ToString());

                }
        }
        public void updateView(String keyword)
        {
            //
            //--Populate DisplayTable
            //
            DataTable itemDB = Data.Tables["ItemDB"];
            DataTable fieldsTB = Data.Tables["FieldsDB"];
            var resultArray = from item in itemDB.AsEnumerable()
                              join field in fieldsTB.AsEnumerable()
                              on item.Field<int>("UID") equals
                              field.Field<int>("ItemDB_UID")
                              select new
                              {
                                  FieldType = 
                                    item.Field<String>("TypeDB_Type"),
                                  FieldName =
                                    field.Field<String>("Name"),
                                  FieldValue =
                                    field.Field<String>("Value")
                              };
            String str = "";
            DisplayTable = new DataTable();
            Item myItem = new Item();
            if (resultArray.Count() == 0);
            else if (resultArray.ElementAt(0).FieldType == "Unit")
                myItem = myUnit;
            else if (resultArray.ElementAt(0).FieldType == "Gear")
                myItem = myGear;
            else if (resultArray.ElementAt(0).FieldType == "Ammo")
                myItem = myAmmo;
            if (resultArray.Count() != 0)
            for (int i = 0; i < myItem.FormatNames.Count; i++)
            {
                DataColumn dc = new DataColumn(resultArray.ElementAt(i).FieldName);
                DisplayTable.Columns.Add(dc);
            }
            if (resultArray.Count() != 0)
            for (int i = 0; i < resultArray.Count(); i += myItem.FormatNames.Count)
            //iterate through each item
            {
                DataRow dr = DisplayTable.NewRow();
                for (int p = 0; p < myItem.FormatNames.Count; p++)
                //iterate through each property
                {
                    //actual index is combination of both item and property indexs
                    int val = i + p;
                    //add each field
                    dr.SetField(resultArray.ElementAt(val).FieldName, resultArray.ElementAt(val).FieldValue);
                }
                //commit new row
                DisplayTable.Rows.Add(dr);
            }

            //
            //--Setup View
            //
            View = DisplayTable.DefaultView;
            String filterBuilder = "";
            for(int p = 0; p < myItem.FormatNames.Count; p++)
            {
                if (p > 0)
                    filterBuilder += " OR ";
                filterBuilder += myItem.FormatNames[p] + " LIKE '%" + keyword + "%'";
            }
            View.RowFilter = filterBuilder;
        }
        //
        //--Read a file into a string(to be interpreted)
        //
        public String readFile(String path)
        {
            String content = System.IO.File.ReadAllText(path, System.Text.Encoding.UTF8);
            //populates String file with entire documents text
            return content;
        }

        //
        //--Read a CSV file and populate Data with it
        //
        public void populateDataFromCSV()
        {

        }

        //
        //--Create File from formated String
        //
        public void generateFile(StringBuilder str)
        {

        }

        //
        //--Update Existing File using current Data
        //
        public void updateFile()
        {

        }

        //Old Code
        /*
        public void populateFromCSV(String path, String type)
        {
            String file = System.IO.File.ReadAllText(path, System.Text.Encoding.UTF8);
            //populates String file with entire documents text

            //UNITS
            if (type == "Unit")
                //Populate Units
            {
                Type = "Unit";
                //String _c, _p, _n, _o, _t, _u, _f, _s, _d;
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
                    newItem.addField(new Field("_t", "", "", "BuildTime"), false);
                    newItem.addField(new Field("_u", "", "", "UpgradeLevel"), false);
                    newItem.addField(new Field("_f", "", "", "Factory"), false);
                    newItem.addField(new Field("_s", "", "", "Script", "\""), false);
                    newItem.addField(new Field("_d", "", "", "Distance"), false);
                    newItem.addField(new Field("_g", "", "", "Camo", "\""), false);
                    newItem.FieldList[0].DisplayName = "ClassName";
                    newItem.FieldList[0].Tags = "\"";
                    addItem(newItem, type);
                    //Add each line to item

                }
                
            }

            //AMMO
            if (type == "Ammo")
            //Populate Units
            {
                Type = "Ammo";
                //String _i, _o, _u, _p, _t;
                //values to populate

                //int iPointer = 0;//current index Number
                //String sPointer;//current index Value

                List<String> content = splitStringBy(file, ",");
                if (content.Count <= 0)
                //If file has no content, Exit
                {
                    return;//Empty File, exit
                }

                for (int i = 0; i < content.Count; i++)
                //iterate through content
                {
                    Ammo newItem = new Ammo(content[i].Replace(" ", ""));

                    //newItem.addField(new Field("_c", ""));

                    newItem.addField(new Field("_o", "", "", "OrdinanceLevel", "\""), false);
                    newItem.addField(new Field("_u", "", "", "UpgradeLevel"), false);
                    newItem.addField(new Field("_p", "", "", "Price"), false);
                    newItem.addField(new Field("_t", "", "", "RearmTimeRound"), false);
                    newItem.FieldList[0].DisplayName = "ClassName";
                    newItem.FieldList[0].Tags = "\"";
                    addItem(newItem, type);
                    //Add each line to item

                }

            }

            //GEAR
            if (type == "Gear")
            //Populate Units
            {
                Type = "Gear";
                //String _i, _u, _p, _g;
                //values to populate

                //int iPointer = 0;//current index Number
                //String sPointer;//current index Value

                List<String> content = splitStringBy(file, ",");
                if (content.Count <= 0)
                //If file has no content, Exit
                {
                    return;//Empty File, exit
                }

                for (int i = 0; i < content.Count; i++)
                //iterate through content
                {
                    Gear newItem = new Gear(content[i].Replace(" ", ""));

                    //newItem.addField(new Field("_c", ""));
                    
                    newItem.addField(new Field("_u", "", "", "UpgradeLevel"), false);
                    newItem.addField(new Field("_p", "", "", "Price"), false);
                    newItem.addField(new Field("_g", "", "", "Filter"), false);
                    newItem.FieldList[0].DisplayName = "ClassName";
                    newItem.FieldList[0].Tags = "\"";
                    addItem(newItem, type);
                    //Add each line to item

                }

            }
        }

        
        public void populateFromFormatedFile(String path, String type)
        {
            
            content = System.IO.File.ReadAllText(path, System.Text.Encoding.UTF8);
            //populates String file with entire documents text
            populateFromFormatedText(content, type);
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
            else if (Type == "Ammo")
            {
                for (int e = 0; e < AmmoList.Count; e++)
                {
                    for (int i = 0; i < AmmoList[e].FieldList.Count(); i++)
                    {
                        if (AmmoList[e].FieldList[i].Value.IndexOf(AmmoList[e].FieldList[i].Tags) != 0 && AmmoList[e].FieldList[i].Value.LastIndexOf(AmmoList[e].FieldList[i].Tags) != (AmmoList[e].FieldList[i].Value.Count() - 1))
                        //if the value doesn't start and end with proper tags, add the tags
                        {
                            str += AmmoList[e].FieldList[i].Name + " pushBack " + AmmoList[e].FieldList[i].Tags + AmmoList[e].FieldList[i].Value + AmmoList[e].FieldList[i].Tags + ";\r\n";
                        }
                        else
                        //else, paste value as is
                        {
                            str += AmmoList[e].FieldList[i].Name + " pushBack " + AmmoList[e].FieldList[i].Value + ";\r\n";
                        }

                    }

                    str += "\r\n\r\n";
                }
                return str;
            }
            else if (Type == "Gear")
            {
                for (int e = 0; e < GearList.Count; e++)
                {
                    for (int i = 0; i < GearList[e].FieldList.Count(); i++)
                    {
                        if (GearList[e].FieldList[i].Value.IndexOf(GearList[e].FieldList[i].Tags) != 0 && GearList[e].FieldList[i].Value.LastIndexOf(GearList[e].FieldList[i].Tags) != (GearList[e].FieldList[i].Value.Count() - 1))
                        //if the value doesn't start and end with proper tags, add the tags
                        {
                            str += GearList[e].FieldList[i].Name + " pushBack " + GearList[e].FieldList[i].Tags + GearList[e].FieldList[i].Value + GearList[e].FieldList[i].Tags + ";\r\n";
                        }
                        else
                        //else, paste value as is
                        {
                            str += GearList[e].FieldList[i].Name + " pushBack " + GearList[e].FieldList[i].Value + ";\r\n";
                        }

                    }

                    str += "\r\n\r\n";
                }
                return str;
            }
            return "";
        }
        */
        

        //end
    }
}
