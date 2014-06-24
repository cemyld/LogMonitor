using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;

using System.Windows.Forms;
using System.Diagnostics;
using LogMonitor.Properties;

namespace LogMonitor
{
    class ProcessIcon : IDisposable
    {
        NotifyIcon ni;
        LogWatcher watcher;
        LogParser parser;
        bool isWatched;
        bool isSettingsLoaded;
        Thread messagenotifier;
        Queue<LogMessage> messagequeue;

        public ProcessIcon()
        {

            ni = new NotifyIcon();
            isWatched = false;
            watcher = new LogWatcher();
            isSettingsLoaded = false;
            messagequeue = new Queue<LogMessage>();

        }
        public void Display()
        {
            watcher.filechanged += new EventHandler(watcher_FileChange);
            ni.MouseClick += new MouseEventHandler(ni_MouseClick);

            ni.Icon = Resources.log_watcher_stopped;
            ni.Text = "Log Monitor";
            ni.Visible = true;

            ni.ContextMenuStrip = new ContextMenus().Create();
            //Start toggled
            ni_MouseClick(this, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
            messagenotifier = new Thread(new ThreadStart(ConsumeQueue));
            messagenotifier.Start();
        }

        private void ConsumeQueue()
        {
            const int MESSAGELIMIT = 3;
            while (true)
            {
                while (messagequeue.Count <= MESSAGELIMIT & messagequeue.Count > 0)
                {
                    LogMessage message = messagequeue.Dequeue();
                    ni.BalloonTipTitle = message.Type;
                    ni.BalloonTipText = message.Message;
                    ni.ShowBalloonTip(30000);
                    System.Threading.Thread.Sleep(2000);
                }
                if (messagequeue.Count > MESSAGELIMIT)
                {
                    messagequeue.Dequeue();
                }
            }
        }
        public void Dispose()
        {
            watcher.Dispose();
            ni.Dispose();
        }
        //Toggle watching
        private void ni_MouseClick(object sender, MouseEventArgs e)
        {
            if (Properties.Settings.Default.Log_Path == "" && e.Button == MouseButtons.Left)
            {
                if (!isSettingsLoaded)
                {
                    isSettingsLoaded = true;
                    MessageBox.Show("Please choose settings first");
                    new SettingsPanel().ShowDialog();
                    isSettingsLoaded = false;
                    return;

                }
            }
            if (e.Button == MouseButtons.Left)
            {
                watcher.toggleMonitor();
                isWatched = !isWatched;
            }
            if (isWatched)
            {
                ni.Icon = Resources.log_watcher_working;
                if (parser == null)
                {
                    parser = new LogParser();
                }
            }
            else
            {
                ni.Icon = Resources.log_watcher_stopped;
            }
        }
        private void watcher_FileChange()
        {
            LogMessages messages = parser.parseNew();
            foreach (LogMessage message in messages.Items)
            {
                //if (LogMonitor.Properties.Settings.Default.Notification_Strings_Checked.Contains(message.Type))
                //{
                //    ni.BalloonTipTitle = message.Type;
                //    ni.BalloonTipText = message.Message;
                //    ni.ShowBalloonTip(30000);
                //    System.Threading.Thread.Sleep(2000);
                //}
                if (LogMonitor.Properties.Settings.Default.Notification_Strings_Checked.Contains(message.Type))
                {
                    messagequeue.Enqueue(message);
                }
            }
        }

    }

}
