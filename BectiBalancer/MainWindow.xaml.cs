using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Forms;


namespace BectiBalancer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CollectionList backupList, currentList;//Current Collection of Items
        Boolean isConnected;//Database Connection
        public MainWindow()
        {
            InitializeComponent();
            currentList = new BectiBalancer.CollectionList();
            isConnected = false;
            //currentList = workingList;//point at workingList
            showVersion();
        }

        private void showVersion()
        {
            
            //lblVersion.Content = "Version: " + System.Reflection.Assembly.GetEntryAssembly().GetName().Version;
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                ApplicationDeployment ver = ApplicationDeployment.CurrentDeployment;
                lblVersion.Content = "Version: " + ver.CurrentVersion;
            }
        }

        private void Log(string text)
            //method for adding test to Log TextBox
        {
            String s = String.Format("{0:HH:mm:ss.fff}: {1}\r\n", DateTime.Now, text);
            rtbLog.AppendText(s);
            rtbLog.ScrollToEnd();
        }
        private void populateFields(String type)
            //Populate the List of Items
        {
            /*lbItemList.ItemsSource = workingList.ItemList;*/
            Log("Populating Fields...");
            for (int i = 0; i < currentList.ItemList.Count; i++)
            {
                //lbItemList.Items.Add(workingList.ItemList[i].ClassName);
            }
            Log("Populated Fields.");
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
            //Try to Connect to Database
        {

        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
            //Attempt Formatted File Import
        {
            
            if (tbImportFilePath.Text == "")
            {
                Log("Invalid Path");
                return;
            }

            if (cbImportFormated.Text == "Unit")
            {
                currentList.populateFromFormatedFile(tbFilePath.Text, cbImportFormated.Text);
                Log("Pulled Data");
                populateFields("Unit");

                //dgBalancingFields.CommitEdit();
                dgViewBalance.ItemsSource = null;
                dgViewBalance.ItemsSource = currentList.UnitList;
                dgViewBalance.IsSynchronizedWithCurrentItem = true;
                dgViewBalance.CanUserAddRows = true;
            }
            else if (cbImportFormated.Text == "Ammo")
            {
                currentList.populateFromFormatedFile(tbFilePath.Text, cbImportFormated.Text);
                Log("Pulled Data");
                populateFields("Ammo");

                //dgBalancingFields.CommitEdit();
                dgViewBalance.ItemsSource = null;
                dgViewBalance.ItemsSource = currentList.AmmoList;
                dgViewBalance.IsSynchronizedWithCurrentItem = true;
                dgViewBalance.CanUserAddRows = true;
            }
            else if (cbImportFormated.Text == "Gear")
            {
                currentList.populateFromFormatedFile(tbFilePath.Text, cbImportFormated.Text);
                Log("Pulled Data");
                populateFields("Gear");

                //dgBalancingFields.CommitEdit();
                dgViewBalance.ItemsSource = null;
                dgViewBalance.ItemsSource = currentList.GearList;
                dgViewBalance.IsSynchronizedWithCurrentItem = true;
                dgViewBalance.CanUserAddRows = true;
            }



            }

        private void btnCreateNewFileFromList_Click(object sender, RoutedEventArgs e)
            //New Collection Generated from csv List of items
        {
            if (tbImportExportPath.Text == "")
            {
                Log("Invalid Path");
                return;
            }
            String type = cbListTypeNewExport.Text;

            String path = tbImportExportPath.Text;
            if(!System.IO.File.Exists(path))
                //if the given file doesn't exist, we need to alert user and exit method
            {

            }
            //Path to CSV File

            switch (type)
                //Switch to populate different types of CollectionLists
            {
                case "Unit":
                    currentList = new CollectionList();
                    currentList.populateFromCSV(path, type);
                    populateFields(type);
                    Log("Populated Item List as Unit Type");
                    dgViewBalance.ItemsSource = null;
                    dgViewBalance.ItemsSource = currentList.UnitList;
                    dgViewBalance.IsSynchronizedWithCurrentItem = true;
                    dgViewBalance.CanUserAddRows = true;
                    break;
                case "Ammo":
                    currentList = new CollectionList();
                    currentList.populateFromCSV(path, type);
                    populateFields(type);
                    Log("Populated Item List as Ammo Type");
                    dgViewBalance.ItemsSource = null;
                    dgViewBalance.ItemsSource = currentList.AmmoList;
                    dgViewBalance.IsSynchronizedWithCurrentItem = true;
                    dgViewBalance.CanUserAddRows = true;
                    break;
                case "Gear":
                    currentList = new CollectionList();
                    currentList.populateFromCSV(path, type);
                    populateFields(type);
                    Log("Populated Item List as Gear Type");
                    dgViewBalance.ItemsSource = null;
                    dgViewBalance.ItemsSource = currentList.GearList;
                    dgViewBalance.IsSynchronizedWithCurrentItem = true;
                    dgViewBalance.CanUserAddRows = true;
                    break;
                default:
                    Log("Improper Type Selected");
                    break;
            }
        }
        private Microsoft.Win32.OpenFileDialog browseForFile(String path, String filter)
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            // Set filter for file extension and default file extension
            switch (filter)
            {
                case "txt":
                    dlg.DefaultExt = ".txt";
                    dlg.Filter = "Text Documents (.txt)|*.txt";
                    break;
                case "csv":
                    dlg.DefaultExt = ".csv";
                    dlg.Filter = "Comma Seperated Value (.csv)|*.csv";
                    break;
                case "sqf":
                    dlg.DefaultExt = ".sqf";
                    dlg.Filter = "Arma 3 Scripting File (.sqf)|*.sqf";
                    break;
            }

            // If there is a path, open to that path first
            if (tbImportExportPath.Text == "")
                ;
            else
                dlg.InitialDirectory = tbImportExportPath.Text;

            return dlg;
        }
        private System.Windows.Forms.FolderBrowserDialog browseForDirectory(String path)
        {
            // Create OpenFileDialog
            System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog();

            // If there is a path, open to that path first
            if (path == "")
                ;
            else
                dlg.SelectedPath = path;

            return dlg;
        }
        private void btnBrowseForPath_Click(object sender, RoutedEventArgs e)
            //For New File from CSV button
        {
            Microsoft.Win32.OpenFileDialog dlg = browseForFile(tbImportExportPath.Text, "csv");
            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();
            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;
                tbImportExportPath.Text = filename;
            }
        }
        private void btnBrowseForExportPath_Click(object sender, RoutedEventArgs e)
        //For Export as File Button
        {
            System.Windows.Forms.FolderBrowserDialog dlg = browseForDirectory(tbExportPath.Text);

            // Open Dialog and verify it opens via opening via Showdialog and checking DialogResult Get the selected file name and display in a TextBox
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Open document
                string folderpath = dlg.SelectedPath;
                tbExportPath.Text = folderpath;
            }

        }
        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        //Browse for Path to Import Formatted File
        {
            Microsoft.Win32.OpenFileDialog dlg = browseForFile(tbFilePath.Text, "sqf");
            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();
            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;
                tbImportFilePath.Text = filename;
            }
        }
        
        private void btnExportList_Click(object sender, RoutedEventArgs e)
        {
            if (currentList.filtered)
            {
                Log("Error: You must clear the filter before you may continue");
                return;
            }

            if (tbExportPath.Text == "")
            {
                Log("Invalid Path");
                return;
            }
            if(cbListTypeExport.Text == "")
            {
                Log("Invalid Type");
                return;
            }
            if (Directory.Exists(tbExportPath.Text))
                //If this is a directory
            {
                //Create a file
                using (StreamWriter sw = File.CreateText(tbExportPath.Text + "\\Export.sqf"))
                {
                    sw.Write(currentList.returnFormatedFile(cbListTypeExport.Text));
                }
                Log("File Written");
            }
            else if (File.Exists(tbExportPath.Text) || Directory.Exists(tbExportPath.Text.Remove(tbExportPath.Text.LastIndexOf("\\")+1)))
            //If this is a file
            {
                //Create this file
                using (StreamWriter sw = File.CreateText(tbExportPath.Text))
                {
                    sw.Write(currentList.returnFormatedFile(cbListTypeExport.Text));
                }
                Log("File Written");
            }
            else
            //this is neither a file nor directory
            {
                //Error brah, exist function
                Log("Cannot Write File, Path is not valid");
                return;
            }

            
        }

        private void dgViewBalance_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dgViewBalance.CommitEdit();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource collectionListViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("collectionListViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // collectionListViewSource.Source = [generic data source]
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            //Enable Editing more than one cell at a time

            //Validate Type Selected
            if (cbEditType.Text == "")
            {
                Log("No Type Selected");
                return;//exit function if not Type Selected
            }
            //Validate Column Selected
            if(cbEditColumn.Text == "")
            {
                Log("No Column Selected");
                return;
            }

            if(cbEditType.Text == "Unit")
            {
                foreach (Unit x in dgViewBalance.SelectedItems)
                {
                    x.editValue(cbEditColumn.Text, txtEditValue.Text);
                }
                dgViewBalance.CommitEdit();
                dgViewBalance.ItemsSource = null;
                dgViewBalance.ItemsSource = currentList.UnitList;
            }
            else if (cbEditType.Text == "Ammo")
            {
                foreach (Ammo x in dgViewBalance.SelectedItems)
                {
                    x.editValue(cbEditColumn.Text, txtEditValue.Text);
                }
                dgViewBalance.CommitEdit();
                dgViewBalance.ItemsSource = null;
                dgViewBalance.ItemsSource = currentList.AmmoList;
            }
            else if (cbEditType.Text == "Gear")
            {
                foreach (Gear x in dgViewBalance.SelectedItems)
                {
                    x.editValue(cbEditColumn.Text, txtEditValue.Text);
                }
                dgViewBalance.CommitEdit();
                dgViewBalance.ItemsSource = null;
                dgViewBalance.ItemsSource = currentList.GearList;
            }
            
        }

        private void dgViewBalance_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            
        }

        private void dgViewBalance_AutoGeneratedColumns(object sender, EventArgs e)
        {
            //datagrid is populated with new columns, so update columnlist
            //Clear cbEditColumn
            cbEditColumn.Items.Clear();
            //Populate cbEditColumn
            foreach (DataGridColumn x in dgViewBalance.Columns)
            {
                cbEditColumn.Items.Add(x.Header);//add the text of the column header to combobox
            }
        }

        private void txtEditValue_GotFocus(object sender, RoutedEventArgs e)
        {
            //box gains focus(highlight all text)
            //txtEditValue.SelectAll();
        }

        private void btnAddColumn_Click(object sender, RoutedEventArgs e)
        {
            //Type of data
            String type = cbTypeAddColumn.Text;

            //New Tag
            String tag = tbAddColumn.Text;
            
            //Tag Format - tenative
            //Name:Value
            //examples
            //Name:Kaliba 6.5mm
            //Caliber: 6.5mm
            //Color:Red


        }

        private void btnCopyToClipboard_Click(object sender, RoutedEventArgs e)
        {
            if (currentList.filtered)
            {
                Log("Error: You must clear the filter before you may continue");
                return;
            }

            //Copy output to clipboard instead of to a file so it can be pasted into a text document
            if (cbListTypeToClipboard.Text != "")
            {
                System.Windows.Clipboard.SetText(currentList.returnFormatedFile(cbListTypeToClipboard.Text));
                Log("Dataset Copied to Clipboard");
            }
            else
            {
                Log("Error: No Type Set");
            }
        }

        private void tbFilterText_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //
            if(e.Key == Key.Return)
            {
                filterLists();
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (currentList.filtered)
            {
                Log("Error: You must clear the filter before you may continue");
                return;
            }

            //Replace contents of import file with data we now have
            if (tbImportFilePath.Text != "")
            {
                if(File.Exists(tbImportFilePath.Text))
                {
                    //Replace the file
                    /*
                     * An idea here
                     * For config files check for a CONFIGSTART comment and add data below that
                     * Purpose: Won't overwrite the comments made above if there are any
                     * So, if there is no CONFIGSTART comment, pop a message in GUI warning user to add one if they need one
                     * endline
                     */

                    //Grab file content

                    //Check for CONFIGSTART

                    //Remove Text below CONFIGSTART

                    //Add new Text

                    //Replace file

                    Log("*Feature Not Complete(Nothing done)* File: '" + tbImportFilePath.Text + "' updated");
                }
                else
                {
                    //File doesn't exist at path
                    Log("Error: File does not exist at path '" + tbImportFilePath.Text + "'");
                }
            }
            else
            {
                //File Path empty
                Log("Error: File path is Empty");
            }
        }

        private void tbFilterText_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            //
            filterLists();
        }

        //Filter things out
        private void filterLists()
        {
            String type = cbFilterType.Text;
            String filterKeyword = tbFilterText.Text;



        }

    }
}
