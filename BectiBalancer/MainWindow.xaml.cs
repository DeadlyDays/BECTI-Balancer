using System;
using System.Collections.Generic;
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
        CollectionList workingList;//Current Collection of Items
        Boolean isConnected;//Database Connection
        public MainWindow()
        {
            InitializeComponent();
            workingList = new BectiBalancer.CollectionList();
            isConnected = false;
            
        }

        private void Log(string text)
            //method for adding test to Log TextBox
        {
            String s = String.Format("{0:HH:mm:ss.fff}: {1}\r\n", DateTime.Now, text);
            rtbLog.AppendText(s);
            rtbLog.ScrollToEnd();
        }
        private void populateFields()
            //Populate the List of Items
        {
            /*lbItemList.ItemsSource = workingList.ItemList;*/
            Log("Populating Fields...");
            for (int i = 0; i < workingList.ItemList.Count; i++)
            {
                lbItemList.Items.Add(workingList.ItemList[i].ClassName);
            }
            Log("Populated Fields.");
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
            //Item Selected, Populate Details Field
        {
            String className = lbItemList.SelectedItem.ToString();

            dgBalancingFields.ItemsSource = workingList.returnFieldsOfItemNamed(className);
            lblClassName.Content = "Classname: " + className;

        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
            //Try to Connect to Database
        {

        }

        

        private void btnImport_Click(object sender, RoutedEventArgs e)
            //Attempt Formatted File Import
        {
            if (tbFilePath.Text == "")
            {
                Log("Invalid Path");
                return;
            }
                
            workingList.populateFromFormatedFile(tbFilePath.Text, "Unit");
            Log("Pulled Data");
            populateFields();
            
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
                    workingList = new CollectionList();
                    workingList.populateFromCSV(path, type);
                    populateFields();
                    Log("Populated Item List as Unit Type");
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
                tbFilePath.Text = filename;
            }
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            dgBalancingFields.IsSynchronizedWithCurrentItem = true;
            dgBalancingFields.CommitEdit();
            dgViewBalance.ItemsSource = null;
            dgViewBalance.ItemsSource = workingList.ItemList;
            dgViewBalance.CanUserAddRows = true;
        }

        private void dgBalancingFields_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnExportList_Click(object sender, RoutedEventArgs e)
        {
            if (tbExportPath.Text == "")
            {
                Log("Invalid Path");
                return;
            }
            if (Directory.Exists(tbExportPath.Text))
                //If this is a directory
            {
                //Create a file
                using (StreamWriter sw = File.CreateText(tbExportPath.Text + "\\Export.sqf"))
                {
                    sw.Write(workingList.returnFormatedFile("Unit"));
                }
                Log("File Written");
            }
            else if (File.Exists(tbExportPath.Text) || Directory.Exists(tbExportPath.Text.Remove(tbExportPath.Text.LastIndexOf("\\")+1)))
            //If this is a file
            {
                //Create this file
                using (StreamWriter sw = File.CreateText(tbExportPath.Text))
                {
                    sw.Write(workingList.returnFormatedFile("Unit"));
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

        }
    }
}
