﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AttendanceCore.BusinessLogic {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
    internal sealed partial class BusinessLogic : global::System.Configuration.ApplicationSettingsBase {
        
        private static BusinessLogic defaultInstance = ((BusinessLogic)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new BusinessLogic())));
        
        public static BusinessLogic Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\RelojChecadorServicio\\Log\\")]
        public string Log {
            get {
                return ((string)(this["Log"]));
            }
            set {
                this["Log"] = value;
            }
        }
    }
}