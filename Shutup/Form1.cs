using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shutup
{
    public partial class Form1 : Form
    {
        //bool isClicked = false;
        public Form1()
        {
            InitializeComponent();
            

        }
        private string[] GetDirectoryFileListing(string dirPath)
        {
            //this is a function to pull all the files in a given directory

            // first lets get all file names in root directory into array.
            string[] fileList = Directory.GetFiles(dirPath);
            
            //now return the list of files
            return fileList;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private string ChooseFolder()
        {
            FolderBrowserDialog folderForm;
            string myPath;
            //create new instance of a folder browser dialog form
            folderForm = new FolderBrowserDialog();
            //set default path
            myPath = "C:\\";
            folderForm.ShowNewFolderButton = false;
            folderForm.RootFolder = System.Environment.SpecialFolder.MyComputer;
            //now show the dialog box to choose a folder
            DialogResult result = folderForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                myPath = folderForm.SelectedPath;
            }

            return myPath;
        }
        private void populateListBox(string[] fileList)
        {
            //empty out the listbox
            listBox1.Items.Clear();
            foreach (string name in fileList)
            {
                listBox1.Items.Add(name.ToString());
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            //Show all Files - pick a folder to read
            string folderPath = ChooseFolder();
            //read in all the files in the chosen folder
            string[] folderFiles = GetDirectoryFileListing(folderPath);
            //populate the list box
            populateListBox(folderFiles);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Move to Folder - locate the destination folder
            string folderPath = ChooseFolder();
            //call the method to copy the files
            moveFilesToDestination(folderPath);
           

        }
        private void copyFilesToDestination(string destFolder)
        {
            string fileName;
            string destFile;
            
            //iterate thru each of the selected items
            foreach (string name in listBox1.SelectedItems)
            {
                //get the full file name
                fileName = System.IO.Path.GetFileName(name);
                //create the destination file with full path
                destFile = System.IO.Path.Combine(destFolder, fileName);
                //copy the file
                System.IO.File.Copy(name, destFile, true);

            }

            MessageBox.Show("Finished Copying Files","File Copy...");

        }
        private void moveFilesToDestination(string destFolder)
        {
            string fileName;
            string destFile;

            //iterate thru each of the selected items
            foreach (string name in listBox1.SelectedItems)
            {
                //get the full file name
                fileName = System.IO.Path.GetFileName(name);
                //create the destination file with full path
                destFile = System.IO.Path.Combine(destFolder, fileName);
                //move the file
                System.IO.File.Move(name, destFile);

            }

            MessageBox.Show("Finished Moving Files", "File Copy...");

        }
        private void button3_Click(object sender, EventArgs e)
        {
            // Copy To Folder - locate the destination folder
            string folderPath = ChooseFolder();
            //call the method to copy the files
            copyFilesToDestination(folderPath);
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Find Old Files - find files older than 180 days
            // locate the search folder
            
            string folderPath = ChooseFolder();
            //look in the selected folder for files
            var directory = new DirectoryInfo(folderPath);
            DateTime from_date = DateTime.Now.AddMonths(-6);
            //load up the files and sort them by name
            if (checkBox1.Checked)
            {
                var files = directory.GetFiles("*.*", SearchOption.AllDirectories)
                  .Where(file => file.LastWriteTime <= from_date)
                  .OrderBy(fi => fi.Name);
                //empty out the listbox
                listBox1.Items.Clear();
                //loop thru the list of files
                foreach (var name in files)
                {
                    //Add the files to listbox
                    listBox1.Items.Add(name.FullName.ToString());
                }
            }
            else
            {
                var files = directory.GetFiles("*.*")
                  .Where(file => file.LastWriteTime <= from_date)
                  .OrderBy(fi => fi.Name);
                //empty out the listbox
                listBox1.Items.Clear();
                //loop thru the list of files
                foreach (var name in files)
                {
                    //Add the files to listbox
                    listBox1.Items.Add(name.FullName.ToString());
                }
            }
            //now show files

        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            button1.ForeColor = Color.Red;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.ForeColor = Color.Black;
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            button3.ForeColor = Color.Red;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.ForeColor = Color.Black;
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            button2.ForeColor = Color.Red;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.ForeColor = Color.Black;
        }

        private void button4_MouseHover(object sender, EventArgs e)
        {
            button4.ForeColor = Color.Red;
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            button4.ForeColor = Color.Black;
        }

    
    }
}
