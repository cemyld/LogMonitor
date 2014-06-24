using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LogMonitor
{
    public partial class SettingsPanel : Form
    {
        private string sourcepath;
        private string destpath;
        private StringCollection notificationstrings;
        private List<CheckBox> typecheckboxes;

        public SettingsPanel()
        {
            typecheckboxes = new List<CheckBox>();
            sourcepath = "";
            destpath = "";
            notificationstrings = LogMonitor.Properties.Settings.Default.Notification_Strings_Checked;
            InitializeComponent();
            AddCheckBoxes();
            if (LogMonitor.Properties.Settings.Default.Log_Path != "" &&
                LogMonitor.Properties.Settings.Default.Xml_Destination_Path != "")
            {
                sourcepath = LogMonitor.Properties.Settings.Default.Log_Path;
                destpath = LogMonitor.Properties.Settings.Default.Xml_Destination_Path;
            }
            updateSettingControls();

            LogMonitor.Properties.Settings.Default.Reload();

            foreach (CheckBox chkbox in typecheckboxes)
            {
                if (LogMonitor.Properties.Settings.Default.Notification_Strings_Checked.Contains(chkbox.Text))
                {
                    chkbox.Checked = true;
                }
            }
        }

        private void AddCheckBoxes()
        {
            foreach (Object obj in LogMonitor.Properties.Settings.Default.Notification_Strings)
            {
                CheckBox typebox = new CheckBox();
                typebox.Text = obj.ToString();
                typebox.AutoSize = true;
                typebox.Location = new System.Drawing.Point(3, 3);
                typebox.Name = obj.ToString(); ;
                typebox.Size = new System.Drawing.Size(113, 24);
                typebox.TabIndex = 0;
                typebox.UseVisualStyleBackColor = true;
                typecheckboxes.Add(typebox);
                this.flowLayoutPanel2.Controls.Add(typebox);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (sourcepath == "" || destpath == "")
            {
                MessageBox.Show("Please fill all settings.");
            }
            else
            {
                //if (checkBoxInfo.Checked) { notificationstrings.Add(checkBoxInfo.Text); } else { notificationstrings.Remove(checkBoxInfo.Text); }
                //if (checkBoxWarn.Checked) { notificationstrings.Add(checkBoxWarn.Text); } else { notificationstrings.Remove(checkBoxWarn.Text); }
                //if (checkBoxFatal.Checked) { notificationstrings.Add(checkBoxFatal.Text); } else { notificationstrings.Remove(checkBoxFatal.Text); }
                //if (checkBoxDebug.Checked) { notificationstrings.Add(checkBoxDebug.Text); } else { notificationstrings.Remove(checkBoxDebug.Text); }
                //if (checkBoxError.Checked) { notificationstrings.Add(checkBoxError.Text); } else { notificationstrings.Remove(checkBoxError.Text); }
                //if (checkBoxValidError.Checked) { notificationstrings.Add(checkBoxValidError.Text); } else { notificationstrings.Remove(checkBoxValidError.Text); }
                updateSettingControls();
                LogMonitor.Properties.Settings.Default.Notification_Strings_Checked = notificationstrings;
                LogMonitor.Properties.Settings.Default.Log_Path = sourcepath;
                LogMonitor.Properties.Settings.Default.Xml_Destination_Path = destpath;
                LogMonitor.Properties.Settings.Default.Save();
                this.Close();
            }
        }
        private void updateSettingControls()
        {
            //Update source textbox
            textBoxSourcePath.Text = sourcepath;
            //Update dest textbox
            textBoxDestPath.Text = destpath;
            //Update checkboxes
            foreach (CheckBox typebox in typecheckboxes)
            {
                if (typebox.Checked & !notificationstrings.Contains(typebox.Text))
                {
                    notificationstrings.Add(typebox.Text);
                }
                else if (!typebox.Checked & notificationstrings.Contains(typebox.Text))
                {
                    notificationstrings.Remove(typebox.Text);
                }
            }

            Console.WriteLine("*****STRINGS******");
            foreach (Object obj in notificationstrings)
            {
                Console.WriteLine(obj);
            }
            Console.WriteLine("****END*****");
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fDialog = new OpenFileDialog();
            fDialog.Title = "Select a Log File";
            fDialog.Filter = "TXT File|*.txt*|LOG File|*.log";
            //fDialog.InitialDirectory = Environment.SpecialFolder.DesktopDirectory.ToString();
            if (fDialog.ShowDialog() == DialogResult.OK)
            {
                sourcepath = fDialog.FileName.ToString();

            }
            updateSettingControls();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog1.ShowNewFolderButton = true;
            folderBrowserDialog1.Description =
            "Select a Destination Folder for XML Files";
            //folderBrowserDialog1.RootFolder = Environment.SpecialFolder.Personal;
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                destpath = folderBrowserDialog1.SelectedPath;
            }
            updateSettingControls();
        }

        private void SourceTextBoxChanged(object sender, EventArgs e)
        {
            sourcepath = textBoxSourcePath.Text;
            updateSettingControls();
        }

        private void DestTextBoxChanged(object sender, EventArgs e)
        {
            destpath = textBoxDestPath.Text;
            updateSettingControls();
        }


        
    }
}
