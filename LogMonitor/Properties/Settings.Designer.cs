﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LogMonitor.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string Log_Path {
            get {
                return ((string)(this["Log_Path"]));
            }
            set {
                this["Log_Path"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string Xml_Destination_Path {
            get {
                return ((string)(this["Xml_Destination_Path"]));
            }
            set {
                this["Xml_Destination_Path"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>INFO</string>
  <string>WARN</string>
  <string>FATAL</string>
  <string>DEBUG</string>
  <string>ERROR</string>
  <string>VALIDATION_ERROR</string>
</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection Notification_Strings_Checked {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["Notification_Strings_Checked"]));
            }
            set {
                this["Notification_Strings_Checked"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"<?xml version=""1.0"" encoding=""utf-16""?>
<ArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <string>INFO</string>
  <string>WARN</string>
  <string>FATAL</string>
  <string>DEBUG</string>
  <string>ERROR</string>
  <string>VALIDATION_ERROR</string>
</ArrayOfString>")]
        public global::System.Collections.Specialized.StringCollection Notification_Strings {
            get {
                return ((global::System.Collections.Specialized.StringCollection)(this["Notification_Strings"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool isMonitoring {
            get {
                return ((bool)(this["isMonitoring"]));
            }
            set {
                this["isMonitoring"] = value;
            }
        }
    }
}
