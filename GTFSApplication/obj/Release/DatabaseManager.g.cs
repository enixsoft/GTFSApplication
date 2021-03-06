﻿#pragma checksum "..\..\DatabaseManager.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E995CB71DCF2B10CC4C17A227BF40D03"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using GTFSApplication;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace GTFSApplication {
    
    
    /// <summary>
    /// DatabaseManager
    /// </summary>
    public partial class DatabaseManager : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 31 "..\..\DatabaseManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Previous;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\DatabaseManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox comboBox;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\DatabaseManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Next;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\DatabaseManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button taskbutton;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\DatabaseManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image taskbuttonImage;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\DatabaseManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock taskbuttonText;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\DatabaseManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dataGridFiles;
        
        #line default
        #line hidden
        
        
        #line 106 "..\..\DatabaseManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dataGridDatabase;
        
        #line default
        #line hidden
        
        
        #line 146 "..\..\DatabaseManager.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock lastModified;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/GTFSApplication;component/databasemanager.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\DatabaseManager.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 8 "..\..\DatabaseManager.xaml"
            ((GTFSApplication.DatabaseManager)(target)).Closed += new System.EventHandler(this.DatabaseManagerClosed);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 21 "..\..\DatabaseManager.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CreateNewDatabase);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Previous = ((System.Windows.Controls.Button)(target));
            return;
            case 4:
            this.comboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 37 "..\..\DatabaseManager.xaml"
            this.comboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.DatabaseSelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Next = ((System.Windows.Controls.Button)(target));
            return;
            case 6:
            this.taskbutton = ((System.Windows.Controls.Button)(target));
            
            #line 51 "..\..\DatabaseManager.xaml"
            this.taskbutton.Click += new System.Windows.RoutedEventHandler(this.DatabaseUpdate);
            
            #line default
            #line hidden
            return;
            case 7:
            this.taskbuttonImage = ((System.Windows.Controls.Image)(target));
            return;
            case 8:
            this.taskbuttonText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 9:
            this.dataGridFiles = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 10:
            this.dataGridDatabase = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 11:
            this.lastModified = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

