using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using Microsoft.Win32;
using LogMonitor.Properties;

namespace LogMonitor
{
    public delegate void EventHandler();
    class LogWatcher:IDisposable
    {
        private System.IO.FileSystemWatcher m_Watcher;
        private bool IsWatching;
        private string file_path; 
        public event EventHandler filechanged;
        public LogWatcher()
        {
            IsWatching = false;
        }
        public void toggleMonitor()
        {
            if (IsWatching)
            {
                IsWatching = false;
                m_Watcher.EnableRaisingEvents = false;
                m_Watcher.Dispose();
            }
            else
            {
                IsWatching = true;
                m_Watcher = new System.IO.FileSystemWatcher();
                file_path = LogMonitor.Properties.Settings.Default.Log_Path;
                if (file_path == "")
                {
                    return;
                }
                m_Watcher.Filter = file_path.Substring(file_path.LastIndexOf("\\")+1);
                m_Watcher.Path = file_path.Substring(0,file_path.Length - m_Watcher.Filter.Length);

                m_Watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.Size;
                m_Watcher.Changed += new FileSystemEventHandler(OnChange);

                m_Watcher.EnableRaisingEvents = true;
            }
        }

        private void OnChange(object sender, FileSystemEventArgs e)
        {
            //Console.WriteLine("File modified!");
            filechanged.Invoke();
            
        }

        public void Dispose()
        {
            if (m_Watcher != null)
            {
                m_Watcher.Dispose();
            }
        }
    }
}
