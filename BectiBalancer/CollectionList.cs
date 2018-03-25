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

        
        private Item myType;//is this a Unit, Gear, etc type file
        public Item MyType
        {
            get
            {
                return myType;
            }
            set
            {
                if (value == null)
                    myType = new Item();
                else
                    myType = value;
            }
        }
        private NewItem newtype;//is this a NewUnit, NewGear, etc type file
        public NewItem NewType
        {
            get
            {
                return newtype;
            }
            set
            {
                if (value == null)
                    newtype = new NewItem();
                else
                    newtype = value;
            }
        }

        private static Unit myUnit = new Unit();
        private static Gear myGear = new Gear();
        private static Ammo myAmmo = new Ammo();
        private static Defence myDefence = new Defence();
        String unitPattern, gearPattern, ammoPattern, defencePattern;
        private static NewUnit myNewUnit = new NewUnit();
        private static NewGear myNewGear = new NewGear();
        private static NewAmmo myNewAmmo = new NewAmmo();
        private static NewDefence myNewDefence = new NewDefence();
        String newUnitPattern, newGearPattern, newAmmoPattern, newDefencePattern;
        
        public Boolean filtered;

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

            Data.Tables[0].PrimaryKey = new DataColumn[] { Data.Tables[0].Columns[0] };
            Data.Tables[1].PrimaryKey = new DataColumn[] { Data.Tables[1].Columns[0] };
            Data.Tables[2].PrimaryKey = new DataColumn[] { Data.Tables[2].Columns[0] };
            Data.Tables[3].PrimaryKey = new DataColumn[] { Data.Tables[3].Columns[0] };

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
            row = Data.Tables[1].NewRow();
            row.SetField("Type", "Defence");
            row.SetField("Footer", myDefence.Footer);
            Data.Tables[1].Rows.Add(row);

            filtered = false;
            MyType = null;

            unitPattern = ".*";
            for (int i = 0; i < (myUnit.FormatArrays).Count; i++)
            {
                unitPattern +=
                    "(" +
                    myUnit.FormatArrays[i] +
                    " pushBack(.*);.*\n){1}.*"
                    ;

            }

            gearPattern = ".*";
            for (int i = 0; i < myGear.FormatArrays.Count; i++)
            {
                gearPattern +=
                    "(" +
                    myGear.FormatArrays[i] +
                    " pushBack(.*);.*\n){1}.*"
                    ;

            }
            ammoPattern = ".*";
            for (int i = 0; i < myAmmo.FormatArrays.Count; i++)
            {
                ammoPattern +=
                    "(" +
                    myAmmo.FormatArrays[i] +
                    " pushBack(.*);.*\n){1}.*"
                    ;

            }
            defencePattern = ".*";
            for (int i = 0; i < myDefence.FormatArrays.Count; i++)
            {
                defencePattern +=
                    "(" +
                    myDefence.FormatArrays[i] +
                    " pushBack(.*);.*\n){1}.*"
                    ;

            }
        }

        public void clearCollection()
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
            row = Data.Tables[1].NewRow();
            row.SetField("Type", "Defence");
            row.SetField("Footer", myDefence.Footer);
            Data.Tables[1].Rows.Add(row);

            filtered = false;
            MyType = null;
            View = null;

            unitPattern = ".*";
            for (int i = 0; i < (myUnit.FormatArrays).Count; i++)
            {
                unitPattern +=
                    "(" +
                    myUnit.FormatArrays[i] +
                    " pushBack(.*);.*\n){1}.*"
                    ;

            }

            gearPattern = ".*";
            for (int i = 0; i < myGear.FormatArrays.Count; i++)
            {
                gearPattern +=
                    "(" +
                    myGear.FormatArrays[i] +
                    " pushBack(.*);.*\n){1}.*"
                    ;

            }
            ammoPattern = ".*";
            for (int i = 0; i < myAmmo.FormatArrays.Count; i++)
            {
                ammoPattern +=
                    "(" +
                    myAmmo.FormatArrays[i] +
                    " pushBack(.*);.*\n){1}.*"
                    ;

            }
            defencePattern = ".*";
            for (int i = 0; i < myDefence.FormatArrays.Count; i++)
            {
                defencePattern +=
                    "(" +
                    myDefence.FormatArrays[i] +
                    " pushBack(.*);.*\n){1}.*"
                    ;

            }
        }
        
        //
        //--Read a formated String, as if from a config file
        //
        public void populateData(String input)
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
                    int iend = input.IndexOf("*/", istart) + 2;
                    iCount = (iend - istart);
                    if (iCount > 0 /*&& startIndex > 0*/)
                        input = input.Remove(startIndex, iCount);//Get rid of comment blocks

                }

            //
            //--Type
            //
            //Dynamic Type Detection
            String type = "";
            if (Regex.IsMatch(input, @unitPattern))
            {
                type = "Unit";
                MyType = myUnit;
            }
            else if(Regex.IsMatch(input, @gearPattern))
            {
                type = "Gear";
                MyType = myGear;
            }
            else if(Regex.IsMatch(input, @ammoPattern))
            {
                type = "Ammo";
                MyType = myAmmo;
            }
            else if (Regex.IsMatch(input, @defencePattern))
            {
                type = "Defence";
                MyType = myDefence;
            }
            else
            {
                type = "Other";
                MyType = new Item();
            }

            //
            //--Tags
            //
            String pattern =
                @"(?:(?:.*)" +//specify front of search has nothing above it, or a ; with any amount of characters between it and next part
                @"(?:\/\/\[Tag;(?:.+?):(?:.+?)?\])" +//find 0 or more instances of tags [Tag;tagName:tagValue]
                @"(?:.*.*?\n.*?))*"//space between tag and everything else
                ;

            switch (false)
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
                        case "Defence":
                            currDataPop<Defence>(pattern, input);
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
                pattern += @"(?:.*)" + (new T().FormatArrays[p]) + @" pushBack[ \t]+(?<" + new T().FormatNames[p] + @">.+?);.*?\n.*?";
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
                    if(typeof(T) == typeof(Defence))
                    {
                        //Set Classname
                        itemRow.SetField("ClassName", p.Groups[2].Value);
                    }
                    else
                    {
                        //Set Classname
                        itemRow.SetField("ClassName", p.Groups[1].Value);
                    }
                    
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
                                        tagsRow.SetField("UID", Data.Tables[3].Rows.Count);
                                        //
                                        //--Group 2 is tagName, Group 3 is tagValue
                                        //
                                        //Set Name
                                        tagsRow.SetField("Name", m.Groups[2].Value);
                                        //Set Value
                                        tagsRow.SetField("Value", m.Groups[3].Value);
                                        //Set Hidden
                                        //Set ItemDB_UID
                                        tagsRow.SetField("ItemDB_UID", itemUID);

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
        public void updateView(String keyword, Boolean showTags)
        {
            if (keyword != "")
                filtered = true;
            else
                filtered = false;
            updateData(showTags);
            //
            //--Populate DisplayTable
            //
            DataTable itemDB = Data.Tables["ItemDB"];
            DataTable fieldsTB = Data.Tables["FieldsDB"];
            var resultArray = from item in itemDB.AsEnumerable()
                              join field in fieldsTB.AsEnumerable() 
                              on item.Field<int?>("UID") 
                              equals field.Field<int?>("ItemDB_UID")
                              into gj
                              from subfield in gj.DefaultIfEmpty()
                              select new
                              {
                                  UID =
                                    item.Field<int>("UID"),
                                  FieldType = 
                                    item.Field<String>("TypeDB_Type"),
                                  FieldName =
                                    subfield?.Field<String>("Name") ?? String.Empty,
                                  FieldValue =
                                    subfield?.Field<String>("Value") ?? String.Empty
                              };
            
            DisplayTable = new DataTable();
            Item myItem = new Item();
            if (resultArray.Count() == 0);
            else if (resultArray.ElementAt(0).FieldType == "Unit")
                myItem = myUnit;
            else if (resultArray.ElementAt(0).FieldType == "Gear")
                myItem = myGear;
            else if (resultArray.ElementAt(0).FieldType == "Ammo")
                myItem = myAmmo;
            else if (resultArray.ElementAt(0).FieldType == "Defence")
                myItem = myDefence;
            if (resultArray.Count() != 0)
            {
                //Add the PK UID
                DataColumn c = new DataColumn("UID");
                //Dont let anyone mess with UID, this is internal value
                c.ReadOnly = true;
                c.Unique = true;
                DisplayTable.Columns.Add(c);
                for (int i = 0; i < myItem.FormatNames.Count; i++)
                {
                    DataColumn dc = new DataColumn(resultArray.ElementAt(i).FieldName);
                    DisplayTable.Columns.Add(dc);
                }
                if(showTags)
                    //add tag columns if enabled
                {
                    HashSet<String> hs = returnTagNames();
                    for(int i = 0; i < hs.Count; i++)
                    {
                        DataColumn dc = new DataColumn(hs.ElementAt(i));
                        DisplayTable.Columns.Add(dc + "(t)");
                    }
                }
            }
            if (resultArray.Count() != 0)
            for (int i = 0; i < resultArray.Count(); i += myItem.FormatNames.Count)
            //iterate through each item
            {
                DataRow dr = DisplayTable.NewRow();
                dr.SetField("UID", resultArray.ElementAt(i).UID);
                for (int p = 0; p < myItem.FormatNames.Count; p++)
                //iterate through each property
                {
                    //actual index is combination of both item and property indexs
                    int val = i + p;
                    //add each field
                    dr.SetField(resultArray.ElementAt(val).FieldName, resultArray.ElementAt(val).FieldValue);
                }

                //Add Tag Values if enabled
                    if(showTags)
                    {
                        DataTable ItemDB = Data.Tables["ItemDB"];
                        DataTable TagsTB = Data.Tables["TagsDB"];
                        var tagsArray = from Item in ItemDB.AsEnumerable()
                                          join Tag in TagsTB.AsEnumerable()
                                          on Item.Field<int?>("UID") equals
                                          Tag.Field<int?>("ItemDB_UID")
                                          where Item.Field<int?>("UID")==Convert.ToInt32(resultArray.ElementAt(i).UID)
                                          select new
                                          {
                                              TagName =
                                                Tag.Field<String>("Name"),
                                              TagValue =
                                                Tag.Field<String>("Value")
                                          };
                        if(tagsArray.Count() > 0)
                            for (int t = 0; t < tagsArray.Count(); t++)
                            {
                                dr.SetField(tagsArray.ElementAt(t).TagName + "(t)", tagsArray.ElementAt(t).TagValue);
                            }
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
            if(DisplayTable.Columns.Count > 0)
                View.RowFilter = filterBuilder;
            //This sets a baseline for checking Changes
            DisplayTable.AcceptChanges();

        }
        public void updateData(Boolean showTags)
            //Updates to the DataGrid onscreen needs to be pushes to DataSet
            //from the DataTable(DisplayTable)
        {
            //
            //--Grab and commit updates from DataTable
            //
            if(DisplayTable != null)
                if(DisplayTable.GetChanges() != null)
                {
                    DataTable changes = DisplayTable.GetChanges();

                    foreach(DataRow dr in changes.Rows)
                        //Cycle updates rows
                    {
                        int UID = 0;
                        if (dr.RowState == DataRowState.Deleted)
                        //skip deleted rows, they will need to be handled seperately
                        {
                            dr.Delete();
                            continue;
                        }
                        if (dr.ItemArray[0] == DBNull.Value)
                            continue;
                        UID = Convert.ToInt32(dr.ItemArray[0]);
                        //
                        //--Locate rows to update
                        //
                        for (int r = 0; r < Data.Tables[0].Rows.Count; r++)
                        //ItemDB
                        {
                            if(UID == Convert.ToInt32(Data.Tables[0].Rows[r].ItemArray[0]))
                                //This Item matches this Row via UID
                            {
                                if(dr.ItemArray[1] != Data.Tables[0].Rows[r].ItemArray[1])
                                    //if Classname isn't the same, update Data(dr2)
                                {
                                    Data.Tables[0].Rows[r].SetField(1, dr.ItemArray[1]);
                                    break;//there can only be one....classname
                                }
                            }
                        }
                        /*
                        foreach (DataRow dr2 in Data.Tables[1].Rows)
                        //TypesDB
                        {

                        }*/

                        for (int f = 1; f < (MyType.FormatNames.Count + 1); f++)
                        //Iterate through where each field should be (Columns)
                        {
                            for (int r = 0; r < Data.Tables[2].Rows.Count; r++)// (DataRow dr2 in Data.Tables[2].Rows)
                            //FieldsDB - Iterating through Rows
                            {
                                if (UID == Convert.ToInt32(Data.Tables[2].Rows[r].ItemArray[6]) && changes.Columns[f].ColumnName == (String)Data.Tables[2].Rows[r].ItemArray[1])
                                //if both UID match and Column Name
                                {
                                    if (Data.Tables[2].Rows[r].ItemArray[2] != dr.ItemArray[f])
                                    //if fieldvalues do not match, update dr2
                                    {

                                        Data.Tables[2].Rows[r].SetField(2, dr.ItemArray[f]);
                                    }
                                }
                            }
                        }

                        if(showTags)
                        { 
                            for (int c = (DisplayTable.Columns.Count - 1);
                                c > (DisplayTable.Columns.Count - returnTagNames().Count - 1); c--)
                            //Iterate through only the cells that are tags
                            //TagsDB
                            {

                                if (DisplayTable.Columns[c].ColumnName.Contains("(t)"))
                                {

                                    //This is the column name - the (t)
                                    String Column = DisplayTable.Columns[c].ColumnName.Substring(0, DisplayTable.Columns[c].ColumnName.Length - 3);
                                    //Value for current Column
                                    String Value = dr.ItemArray[c].ToString();
                                    if (Value == "" || Value == null)
                                        continue;
                                    Boolean found = false;
                                    //Need to triangulate location in Dataset
                                    for(int d = 0; d < Data.Tables[3].Rows.Count; d++)
                                        //Iterate Dataset TagsDB rows
                                    {
                                        if(UID == Convert.ToInt32(Data.Tables[3].Rows[d].ItemArray[4]))
                                            //Does it match Item
                                        {
                                            
                                                if(Column == Data.Tables[3].Rows[d].ItemArray[1].ToString())
                                                //Do column names match?
                                                {
                                                    if(Value == Data.Tables[3].Rows[d].ItemArray[2].ToString())
                                                        //If values match skip
                                                    {
                                                        found = true;
                                                        continue;
                                                    }
                                                    else
                                                    //Values don't match, this is an updated value
                                                    {
                                                        Data.Tables[3].Rows[d].SetField("Value", Value);
                                                        found = true;
                                                        break;
                                                    }
                                                }
                                            
                                        }
                                    }
                                    if(!found)
                                    //if we aren't updating existing, add new
                                    {
                                        DataRow newRow = Data.Tables[3].NewRow();
                                        newRow.SetField("UID", Data.Tables[3].Rows.Count);
                                        newRow.SetField("Name", Column);
                                        newRow.SetField("Value", Value);
                                        newRow.SetField("ItemDB_UID", UID);
                                        Data.Tables[3].Rows.Add(newRow);
                                    }
                                }
                            }
                            
                            
                        }
                        

                    }

                }
            Data.AcceptChanges();
        }
        
        public void removeDeletedData(List<DataRowView> data)
            //remove the specified data from the internal DB
        {
            if(data != null)
            {
                for(int rv = 0; rv < data.Count; rv++)
                    //iterate rowsviews in data
                {
                    //we only need the itemUID to drop item and all related
                    int UID = Convert.ToInt32(data[rv].Row.ItemArray[0]);

                    //drop item in ItemDB
                    Data.Tables[0].Rows.Find(UID).Delete();

                    //maybe i can just orphan the other data?
                    //drop fields in FieldDB
                    //Data.Tables[2].
                    //drop tags in TagsDB

                }
            }
        }

        public Boolean addRow(DataRow row)
            //handle adding a row to dataset, return false if item exists
        {
            if (row.ItemArray[1] == DBNull.Value)
                return false;
            //grab the className
            String className = (String)row.ItemArray[1];
            //check and make sure there are no conflicts for this new className
            if (Data.Tables[0].Select("ClassName = '" + className + "'").Count() > 0)
                return false;


            //Make  new row for ItemDB
            DataRow itemRow = Data.Tables[0].NewRow();
            //new UID
            int itemUID = Data.Tables[0].Rows.Count;
            //Set UID
            itemRow.SetField("UID", itemUID);
            //Set Classname
            itemRow.SetField("ClassName", className);
            //Set TypeDB_Type
            itemRow.SetField("TypeDB_Type", MyType.GetType().Name.ToString());
            //Commit Row
            Data.Tables[0].Rows.Add(itemRow);

            //we need to add fields, empty all but classname

            //
            //---This grabs all the property fields and populates every item found
            //

            for (int i = 0; i < (row.ItemArray.Count() - 1); i++)//Iterate through each property i in matchCol.Group
            {
                //This is where we store fields

                //Create new row in FieldsTB
                DataRow fieldRow = Data.Tables[2].NewRow();
                //Set UID
                fieldRow.SetField("UID", Data.Tables[2].Rows.Count);
                //Set Name
                fieldRow.SetField("Name", MyType.FormatNames[i]);
                //Set Value
                fieldRow.SetField("Value", row[i + 1]);
                //Set Default
                fieldRow.SetField("Default", MyType.FormatDefaults[i]);
                //Set ArrayName
                fieldRow.SetField("ArrayName", MyType.FormatArrays[i]);
                //Set Hidden
                //Set ItemDB_UID
                fieldRow.SetField("ItemDB_UID", itemUID);

                //Commit Row
                Data.Tables[2].Rows.Add(fieldRow);
            }

            return true;
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
        public void generateFile(String str)
        {

        }

        //
        //--Update Existing File using current Data
        //
        public Boolean updateFile(String filePath)
            //return false if filetypes don't match
        {
            //
            //--Verify Data type is same as what file is filled with
            //

            String content = System.IO.File.ReadAllText(filePath, System.Text.Encoding.UTF8);



            if (Regex.IsMatch(content, @unitPattern) || Regex.IsMatch(content, @gearPattern) || Regex.IsMatch(content, @ammoPattern) || Regex.IsMatch(content, @defencePattern))
            {
                //Datatype is good, continue
                String newFile = "";
                newFile += MyType.Header + "\n\n";

                //
                //--Add Data to file
                //
                newFile += generateFormatedString();

                newFile += MyType.Footer;

                //
                //--Replace file or filecontent with newFile string
                //
                using (System.IO.StreamWriter newTask = new System.IO.StreamWriter(filePath, false))
                {
                    newTask.Write(newFile);
                }

                return true;
            }
            else
                return false;
        }
        public void updateNewFile(String filePath, String newFile)
        {
            using (System.IO.StreamWriter newTask = new System.IO.StreamWriter(filePath, false))
            {
                newTask.Write(newFile);
            }
        }
        //
        //--Generate formatted string from Data
        //
        public String generateFormatedString()
        {
            String output = "";
            foreach (DataRow dr in DisplayTable.Rows)
                //Iterate Rows
            {
                //Names
                DataTable itemDB = Data.Tables["ItemDB"];
                DataTable tagsDB = Data.Tables["TagsDB"];

                //DataRowCollection tagRows = ;
                if (Data.Tables[3].Rows.Count > 0)
                    //dont look for tags if there are none
                {
                    var tagRows = from item in itemDB.AsEnumerable()
                                  join tag in tagsDB.AsEnumerable()
                                  on item.Field<int?>("UID") equals
                                  tag.Field<int?>("ItemDB_UID")
                                  where item.Field<int?>("UID") == Convert.ToInt64(dr.Field<String>("UID"))
                                  select new
                                  {
                                      UID =
                                        tag.Field<int>("UID"),
                                      TagName =
                                        tag.Field<String>("Name"),
                                      TagValue =
                                        tag.Field<String>("Value")
                                  };

                    if (tagRows.Count() > 0)
                        for (int t = 0; t < tagRows.Count(); t++)
                        //Iterate Tags
                        {
                            output += "//[Tag;" + tagRows.ElementAt(t).TagName + ":" + tagRows.ElementAt(t).TagValue + "]\n";
                        }
                }
                //Values
                for(int p = 0; p < MyType.FormatNames.Count(); p++)
                    //Iterate Values
                {
                    if(Convert.ToString(dr.ItemArray[p+1]) != "")
                        output += MyType.FormatArrays[p] + " pushBack " + dr.ItemArray[p + 1] + ";\n";
                    else
                        output += MyType.FormatArrays[p] + " pushBack " + MyType.FormatDefaults[p] + ";\n";
                }
                output += "\n";//an extra space between items
            }


            return output;
        }

        public String generateNewFormatedString()
        //for new format
        {
            String output = "";
            foreach (DataRow dr in DisplayTable.Rows)
                //Iterate through displayRows
            {
                output += NewType.ArrayName + " pushBack [";
                for (int p = 0; p < NewType.FormatNames.Count(); p++)
                    //Iterates through Output properties(maybe this is wrong way to go about this)
                {
                    Boolean found = false;
                    for( int c = 0; c < DisplayTable.Columns.Count; c++)
                    //Lets iterate through what we have to find correct column and value
                    {
                        if(DisplayTable.Columns[c].ColumnName == NewType.FormatNames[p])
                            //This is correct
                        {
                            found = true;//so we found a column that matches a value we want
                            if (p == 0)
                                //no comma needed on first run
                                output += "\n\t/*";
                            else
                                output += ",\n\t/*";

                            //
                            //--Add Formated Line
                            //
                            if (Convert.ToString(dr.ItemArray[c]) != "")
                                //If there is a value, use it
                            {
                                //
                                //--Need to check if value is properly formated for new format
                                //
                                if(NewType.FormatWrapper[p].Count() > 0)
                                    //if value is supposed to be bracketed by chars
                                {
                                    String[] bracket = new String[]{
                                        NewType.FormatWrapper[p].Substring(0, (NewType.FormatWrapper[p].Count()/2)),
                                        NewType.FormatWrapper[p].Substring((NewType.FormatWrapper[p].Count()/2), (NewType.FormatWrapper[p].Count()/2))
                                    };
                                    //is the value we are going to store surrounded by the brackets?
                                    if(Regex.IsMatch((String)dr.ItemArray[c], bracket[0] + @".*" + bracket[1]))
                                        //If it is surrounded
                                    {
                                        String newItem = Regex.Replace((String)dr.ItemArray[c], @"[\[\]]", "");
                                        if(newItem.Contains("\""))
                                        //If should have ", do we also have '(replace them if we do)
                                        {
                                            newItem = Regex.Replace(newItem, @"[']", "'");
                                            output += NewType.FormatNames[p] + "*/" +
                                            newItem;
                                        }
                                        else if(newItem.Contains("'"))
                                        {
                                            newItem = Regex.Replace(newItem, @"[""]", @"""");
                                            output += NewType.FormatNames[p] + "*/" +
                                            newItem;
                                        }
                                        else
                                        {
                                            output += NewType.FormatNames[p] + "*/" +
                                            newItem;
                                        }
                                        
                                    }
                                    else
                                    //if it isn't surrounded
                                    {
                                        //get rid of '' "" or []
                                        String newItem = Regex.Replace((String)dr.ItemArray[c], @"[\[\]]", "");
                                        if (newItem.Contains("\""))
                                        //If should have ", do we also have '(replace them if we do)
                                        {
                                            newItem = Regex.Replace(newItem, @"[']", @"""");
                                            if(bracket.Contains("\""))
                                                //don't dupe with adding
                                            {
                                                output += NewType.FormatNames[p] + "*/" + newItem;
                                            }
                                            else if(bracket.Contains("'"))
                                                //replace "" with ''
                                            {
                                                output += NewType.FormatNames[p] + "*/" + Regex.Replace(newItem,@"[""]","'");
                                            }
                                            else
                                                //need added
                                            {
                                                output += NewType.FormatNames[p] + "*/" + bracket[0] +
                                                    newItem + bracket[1];
                                            }
                                            
                                        }
                                        else if (newItem.Contains("'"))
                                        {
                                            newItem = Regex.Replace(newItem, @"[""]", "'");
                                            if (bracket.Contains("'"))
                                            //don't dupe with adding
                                            {
                                                output += NewType.FormatNames[p] + "*/" + newItem;
                                            }
                                            else if (bracket.Contains("\""))
                                            //replace "" with ''
                                            {
                                                output += NewType.FormatNames[p] + "*/" + Regex.Replace(newItem, @"[']", "\"");
                                            }
                                            else
                                            {
                                                output += NewType.FormatNames[p] + "*/" + bracket[0] +
                                                    newItem + bracket[1];
                                            }
                                        }
                                        else
                                        {
                                            output += NewType.FormatNames[p] + "*/" + bracket[0] +
                                            newItem + bracket[1];
                                        }
                                        
                                    }
                                }
                                else
                                //if value is supposed to not be bracketed by any chars
                                {
                                    //
                                    //--if this is the case, need to strip special chars if there are any
                                    //

                                    //This behavior might need to change
                                    output += NewType.FormatNames[p] + "*/" + (String)dr.ItemArray[c] + "";
                                }
                                
                                //Formatted line
                                //output += NewType.FormatNames[p] + "*/" + dr.ItemArray[c] + "";
                            }
                            else
                                //if there is no value, then use the provided default
                            {
                                output += NewType.FormatNames[p] + "*/" + NewType.FormatDefaults[p] + "";
                            }

                            //This was the match, we can break out to next property
                            break;
                        }
                    }
                    if(!found)
                        //if not found we need to apply the default
                    {
                        if (p == 0)
                            //no comma needed on first run
                            output += "\n\t/*";
                        else
                            output += ",\n\t/*";
                        output += NewType.FormatNames[p] + "*/" + NewType.FormatDefaults[p] + "";
                    }
                    
                }
                output += "\n];\n\n";//an extra space between items
            }
            return output;
        }
        //
        //--Convert Directory
        //
        public Boolean convertDirectory(String dirPath)
        {
            String[] filePaths = System.IO.Directory.GetFiles(dirPath);
            Boolean success = true;
            foreach(String filePath in filePaths)
                //Iterate through all the files in the directory
            {
                clearCollection();
                //
                //--Get file Contents
                //
                String content = readFile(filePath);

                //
                //--Verify contents are a valid type
                //
                if (Regex.IsMatch(content, @unitPattern) || Regex.IsMatch(content, @gearPattern) || Regex.IsMatch(content, @ammoPattern) || Regex.IsMatch(content, @defencePattern))
                //if this is a valid type
                {
                    ;
                }
                else
                //not a valid type
                {
                    success = false;
                    continue;//the next file might be legit, we want to convert all that we can
                }

                //
                //--Populate Data
                //
                populateData(content);
                updateView("", false);

                //
                //--Generate Converted String
                //
                
                switch(myType.GetType().Name)
                {
                    case "Unit":
                        NewType = myNewUnit;
                        break;
                    case "Gear":
                        NewType = myNewGear;
                        break;
                    case "Ammo":
                        NewType = myNewAmmo;
                        break;
                    case "Defence":
                        NewType = myNewDefence;
                        break;
                    default:
                        NewType = new NewItem();
                        break;
                }
                content = generateNewFormatedString();

                //
                //--Overwrite file with Converted String
                //
                updateNewFile(filePath, content);
            }
            return success;
        }

        public HashSet<String> returnTagNames()
            //Returns a unique list of all TagNames in DataSet
        {
            HashSet<String> tags = new HashSet<String>();
            if (Data.Tables[3].Rows.Count > 0)
                foreach (DataRow r in Data.Tables[3].Rows)
                //Iterate through every tag row
                {
                    if(tags.Contains(r.Field<String>("Name")))
                        //if the name is already in the list, skip
                    {
                        continue;
                    }
                    else
                    {
                        tags.Add(r.Field<String>("Name"));
                    }
                }
            return tags;
        }

        public Boolean checkForTag(int ItemUID, String tagName)
        {
            DataTable itemDB = Data.Tables["ItemDB"];
            DataTable tagsTB = Data.Tables["TagsDB"];
            var resultArray = from item in itemDB.AsEnumerable()
                              join tag in tagsTB.AsEnumerable()
                              on item.Field<int?>("UID") equals
                              tag.Field<int?>("ItemDB_UID")
                              where item.Field<int?>("UID")==ItemUID
                              select new
                              {
                                  TagName =
                                    tag.Field<String>("Name")
                              };
            if(resultArray.Count() > 0)
            {
                //if there are results, check for matches with tagName
                for(int i = 0; i < resultArray.Count(); i++)
                {
                    if(resultArray.ElementAt(i).TagName == tagName)
                    {
                        //match
                        return true;
                    }
                }
                //if loop finishes without match, its nogut
                return false;
            }
            else
                return false;
            
        }
        public Boolean checkForTag(int ItemUID, String tagName, String tagValue)
        {
            return false;
        }

        //
        //--Add Tag
        //
        public Boolean addTag(int ItemUID, String tagName, String tagValue)
            //Adds a Tag to an Item
        {
            //Check if TagName exists, new TagName means new Column if not hidden
            if(returnTagNames().Contains(tagName))
                //
            {
                //No new column needed

            }
            else
            {
                //New Column needed if not hidden

            }
            //
            if (!checkForTag(ItemUID, tagName))
            //if item has a tag by this name already, reject
            {
                DataRow newRow = Data.Tables[3].NewRow();
                newRow.SetField("UID", Data.Tables[3].Rows.Count);
                newRow.SetField("Name", tagName);
                newRow.SetField("Value", tagValue);
                newRow.SetField("ItemDB_UID", ItemUID);
                Data.Tables[3].Rows.Add(newRow);
                return true;
            }
            else
                return false;
            

            return false;
        }

        //end
    }
}
