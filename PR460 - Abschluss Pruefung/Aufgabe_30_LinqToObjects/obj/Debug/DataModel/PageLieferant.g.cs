﻿#pragma checksum "..\..\..\DataModel\PageLieferant.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "218AE3B4EC578F92F0D65169A44787F846E870096F305634E10BBE639AB9380C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Aufgabe_30_LinqToObjects.DataModel;
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


namespace Aufgabe_30_LinqToObjects.DataModel {
    
    
    /// <summary>
    /// PageLieferant
    /// </summary>
    public partial class PageLieferant : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 23 "..\..\..\DataModel\PageLieferant.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TId;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\DataModel\PageLieferant.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TFirma;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\DataModel\PageLieferant.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox TPersonID;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\DataModel\PageLieferant.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnInsert;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\DataModel\PageLieferant.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnUpdate;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\DataModel\PageLieferant.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnDelete;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\DataModel\PageLieferant.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid DgDataGrid;
        
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
            System.Uri resourceLocater = new System.Uri("/Aufgabe_30_LinqToObjects;component/datamodel/pagelieferant.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\DataModel\PageLieferant.xaml"
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
            this.TId = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.TFirma = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.TPersonID = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.BtnInsert = ((System.Windows.Controls.Button)(target));
            
            #line 40 "..\..\..\DataModel\PageLieferant.xaml"
            this.BtnInsert.Click += new System.Windows.RoutedEventHandler(this.BtnInsert_OnClick);
            
            #line default
            #line hidden
            return;
            case 5:
            this.BtnUpdate = ((System.Windows.Controls.Button)(target));
            
            #line 41 "..\..\..\DataModel\PageLieferant.xaml"
            this.BtnUpdate.Click += new System.Windows.RoutedEventHandler(this.BtnUpdate_OnClick);
            
            #line default
            #line hidden
            return;
            case 6:
            this.BtnDelete = ((System.Windows.Controls.Button)(target));
            
            #line 42 "..\..\..\DataModel\PageLieferant.xaml"
            this.BtnDelete.Click += new System.Windows.RoutedEventHandler(this.BtnDelete_OnClick);
            
            #line default
            #line hidden
            return;
            case 7:
            this.DgDataGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 57 "..\..\..\DataModel\PageLieferant.xaml"
            this.DgDataGrid.SelectedCellsChanged += new System.Windows.Controls.SelectedCellsChangedEventHandler(this.DgDataGrid_SelectedCellsChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

