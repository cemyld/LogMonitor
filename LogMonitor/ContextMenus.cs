using System;
using System.Collections.Generic;

using System.Text;

using System.Drawing;
using LogMonitor.Properties;
using System.Windows.Forms;
using System.Diagnostics;

namespace LogMonitor
{
    class ContextMenus
    {
        bool isSettingsLoaded;
        public ContextMenuStrip Create()
        {
            //if (Properties.Settings.Default.Log_Path == "")
            //{
            //    if (!isSettingsLoaded)
            //    {
            //        isSettingsLoaded = true;
            //        new SettingsPanel().ShowDialog();
            //        isSettingsLoaded = false;

            //    }
            //}
            //Add default menu options.
            ContextMenuStrip menu = new ContextMenuStrip();
            ToolStripMenuItem item;
            ToolStripSeparator sep;

            //Settings
            item = new ToolStripMenuItem();
            item.Text = "Settings";
            item.Click += new System.EventHandler(Settings_Click);
            menu.Items.Add(item);

            

            //Separator
            sep = new ToolStripSeparator();
            menu.Items.Add(sep);

            //Exit
            item = new ToolStripMenuItem();
            item.Text = "Exit";
            item.Click += new System.EventHandler(Exit_Click);
            menu.Items.Add(item);

            return menu;
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            if (!isSettingsLoaded)
            {
                isSettingsLoaded = true;
                new SettingsPanel().ShowDialog();
                isSettingsLoaded = false;
                
            }
            }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        
    }
}
