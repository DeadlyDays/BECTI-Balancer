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
        String unitPattern, gearPattern, ammoPattern;
        private static NewUnit myNewUnit = new NewUnit();
        private static NewGear myNewGear = new NewGear();
        private static NewAmmo myNewAmmo = new NewAmmo();
        String newUnitPattern, newGearPattern, newAmmoPattern;
        
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

            filtered = false;
            MyType = null;

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

            filtered = false;
            MyType = null;
            View = null;

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
                    int iend = input.IndexOf("*/") + 2;
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
            if (keyword != "")
                filtered = true;
            else
                filtered = false;
            updateData();
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
                                  UID =
                                    item.Field<int>("UID"),
                                  FieldType = 
                                    item.Field<String>("TypeDB_Type"),
                                  FieldName =
                                    field.Field<String>("Name"),
                                  FieldValue =
                                    field.Field<String>("Value")
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
        public void updateData()
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
                        int UID = Convert.ToInt32(dr.ItemArray[0]);
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
                        
                        for(int f = 1; f < (MyType.FormatNames.Count + 1); f++)
                            //Iterate through where each field should be (Columns)
                            for(int r = 0; r < Data.Tables[2].Rows.Count; r++)// (DataRow dr2 in Data.Tables[2].Rows)
                            //FieldsDB - Iterating through Rows
                            {
                                if(UID == Convert.ToInt32(Data.Tables[2].Rows[r].ItemArray[6]) && changes.Columns[f].ColumnName == (String)Data.Tables[2].Rows[r].ItemArray[1])
                                    //if both UID match and Column Name
                                {
                                    if(Data.Tables[2].Rows[r].ItemArray[2] != dr.ItemArray[f])
                                        //if fieldvalues do not match, update dr2
                                    {

                                        Data.Tables[2].Rows[r].SetField(2, dr.ItemArray[f]);
                                    }
                                }
                            }
                        /* There are no tags yet
                        foreach (DataRow dr2 in Data.Tables[3].Rows)
                        //TagsDB
                        {

                        }
                        */

                    }

                }
            
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



            if (Regex.IsMatch(content, @unitPattern) || Regex.IsMatch(content, @gearPattern) || Regex.IsMatch(content, @ammoPattern))
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
            {
                for(int p = 0; p < MyType.FormatNames.Count(); p++)
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
                                        //If should have ", do we also have '(replace thme if we do)
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
                                    output += NewType.FormatNames[p] + "*/" + Regex.Replace((String)dr.ItemArray[c], @"[^\w]", "") + "";
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
                if (Regex.IsMatch(content, @unitPattern) || Regex.IsMatch(content, @gearPattern) || Regex.IsMatch(content, @ammoPattern))
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
                updateView("");

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

        //end
    }
}
