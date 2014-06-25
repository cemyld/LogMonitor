using System;
using System.Collections.Generic;
using System.IO;

using System.Text;
using System.Text.RegularExpressions;

using System.Xml;
using System.Xml.Serialization;

namespace LogMonitor
{
    class LogParser
    {
        public LogMessages currentlog;
        int textindex;
        string file_path;
        string xml_path;
        public LogParser()
        {
            textindex = 0;
            this.file_path = LogMonitor.Properties.Settings.Default.Log_Path; ;
            this.xml_path = LogMonitor.Properties.Settings.Default.Xml_Destination_Path + "\\" + file_path.Substring(file_path.LastIndexOf("\\") + 1) +DateTime.Now.ToString("yy-MM-dd-HH_mm") +".xml";
            currentlog = new LogMessages();
            currentlog.Items = new List<LogMessage>();
            try
            {
                parseNew();
            }
            catch (Exception e)
            {
                
            }

        }

        public void Serialize(LogMessages logmessages, string outputpath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(LogMessages));
            using (TextWriter writer = new StreamWriter(outputpath))
            {
                serializer.Serialize(writer, logmessages);
            }
        }
        public LogMessages parseNew()
        {
            LogMessages logmessages = new LogMessages();
            logmessages.Items = new List<LogMessage>();
            string filelogtext;
            using (var fs = new FileStream(file_path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var sr = new StreamReader(fs, Encoding.Default))
            {
                fs.Seek(textindex, SeekOrigin.Begin);
                filelogtext = sr.ReadToEnd();
                if (filelogtext == "") { 
                    textindex = 0;
                    fs.Seek(textindex, SeekOrigin.Begin);
                    filelogtext = sr.ReadToEnd();
                }
                textindex += filelogtext.Length;
            }
            Match m;
            string logpattern = @"(?<Date>(\d{4}-\d{2}-\d{2})\s\d{2}:\d{2}:\d{2},\d{3})\s(?<Num>\[\d{1,2}\])\s(?<Type>INFO|WARN|FATAL|DEBUG|ERROR|VALIDATION_ERROR)\s{1,2}-\s(?<Message>.*?)(?=\1|$)";
            filelogtext = filelogtext.Trim();
            m = Regex.Match(filelogtext, logpattern, RegexOptions.Singleline);
            while (m.Success)
            {
                LogMessage element = new LogMessage();
                element.Date = m.Groups["Date"].Value;
                element.Type = m.Groups["Type"].Value;
                element.Message = m.Groups["Message"].Value;

                logmessages.Items.Add(element);
                currentlog.Items.Add(element);


                m = m.NextMatch();
            }
            //textindex = filelogtext.Length - 1;
            Serialize(currentlog, xml_path);
            return logmessages;
        }
    }
    public class LogMessage
    {
        public String Date { get; set; }
        public String Type { get; set; }
        public String Message { get; set; }
        public override string ToString() { return "//LOG" + Date + Type + Message + "//LOG"; }
    }
    [XmlRoot("Log")]
    public class LogMessages
    {
        [XmlElement("LogMessage")]
        public List<LogMessage> Items { get; set; }
    }
}
