﻿#pragma checksum "..\..\InsertWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "414740999956535B8DE21DD4D323262EE0A67F122D257072B191A34D828DF2A1"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Cashier;
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


namespace Cashier {
    
    
    /// <summary>
    /// InsertWindow
    /// </summary>
    public partial class InsertWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 52 "..\..\InsertWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textbox_Name;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\InsertWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textbox_Category;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\InsertWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textbox_Barcode;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\InsertWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textbox_Price;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\InsertWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textbox_Description;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\InsertWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textbox_Stock;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\InsertWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Cancel;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\InsertWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Insert;
        
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
            System.Uri resourceLocater = new System.Uri("/Cashier;component/insertwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\InsertWindow.xaml"
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
            this.textbox_Name = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.textbox_Category = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.textbox_Barcode = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.textbox_Price = ((System.Windows.Controls.TextBox)(target));
            
            #line 61 "..\..\InsertWindow.xaml"
            this.textbox_Price.AddHandler(System.Windows.DataObject.PastingEvent, new System.Windows.DataObjectPastingEventHandler(this.textbox_Price_Pasting));
            
            #line default
            #line hidden
            
            #line 61 "..\..\InsertWindow.xaml"
            this.textbox_Price.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.textbox_Price_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 5:
            this.textbox_Description = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.textbox_Stock = ((System.Windows.Controls.TextBox)(target));
            
            #line 67 "..\..\InsertWindow.xaml"
            this.textbox_Stock.AddHandler(System.Windows.DataObject.PastingEvent, new System.Windows.DataObjectPastingEventHandler(this.textbox_Stock_Pasting));
            
            #line default
            #line hidden
            
            #line 67 "..\..\InsertWindow.xaml"
            this.textbox_Stock.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.textbox_Stock_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btn_Cancel = ((System.Windows.Controls.Button)(target));
            
            #line 77 "..\..\InsertWindow.xaml"
            this.btn_Cancel.Click += new System.Windows.RoutedEventHandler(this.btn_Cancel_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btn_Insert = ((System.Windows.Controls.Button)(target));
            
            #line 78 "..\..\InsertWindow.xaml"
            this.btn_Insert.Click += new System.Windows.RoutedEventHandler(this.btn_Insert_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
