﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AttendanceCore.DeviceAccess {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
    internal sealed partial class Devices : global::System.Configuration.ApplicationSettingsBase {
        
        private static Devices defaultInstance = ((Devices)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Devices())));
        
        public static Devices Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("192.168.1.20")]
        public string EntradaCoorporativo {
            get {
                return ((string)(this["EntradaCoorporativo"]));
            }
            set {
                this["EntradaCoorporativo"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("192.168.3.201")]
        public string EntradaCEDIS {
            get {
                return ((string)(this["EntradaCEDIS"]));
            }
            set {
                this["EntradaCEDIS"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("172.179.1.90")]
        public string ComedorCoorporativo {
            get {
                return ((string)(this["ComedorCoorporativo"]));
            }
            set {
                this["ComedorCoorporativo"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("192.168.3.202")]
        public string ComedorCEDIS {
            get {
                return ((string)(this["ComedorCEDIS"]));
            }
            set {
                this["ComedorCEDIS"] = value;
            }
        }
    }
}