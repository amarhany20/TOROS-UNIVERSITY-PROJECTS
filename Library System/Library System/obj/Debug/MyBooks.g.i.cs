﻿#pragma checksum "..\..\MyBooks.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "F6AC455A473C0F36544D84BE78B55C10FCF148E232A878DB34C07DAF8417FF96"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Library_System;
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


namespace Library_System {
    
    
    /// <summary>
    /// MyBooks
    /// </summary>
    public partial class MyBooks : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\MyBooks.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid datagrid_MyBooksDisplay;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\MyBooks.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button_ReturnSelectedBook;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\MyBooks.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button_ReturnBackToMainMenu;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\MyBooks.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textbok_SearchMyBooks;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\MyBooks.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button_SearchMyBooks;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\MyBooks.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox checkbox_SearchByFirstLetter;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\MyBooks.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label_CurrentLimit;
        
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
            System.Uri resourceLocater = new System.Uri("/Library System;component/mybooks.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MyBooks.xaml"
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
            this.datagrid_MyBooksDisplay = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 2:
            this.button_ReturnSelectedBook = ((System.Windows.Controls.Button)(target));
            
            #line 12 "..\..\MyBooks.xaml"
            this.button_ReturnSelectedBook.Click += new System.Windows.RoutedEventHandler(this.button_ReturnSelectedBook_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.button_ReturnBackToMainMenu = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\MyBooks.xaml"
            this.button_ReturnBackToMainMenu.Click += new System.Windows.RoutedEventHandler(this.button_ReturnBackToMainMenu_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.textbok_SearchMyBooks = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.button_SearchMyBooks = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\MyBooks.xaml"
            this.button_SearchMyBooks.Click += new System.Windows.RoutedEventHandler(this.button_SearchMyBooks_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.checkbox_SearchByFirstLetter = ((System.Windows.Controls.CheckBox)(target));
            
            #line 16 "..\..\MyBooks.xaml"
            this.checkbox_SearchByFirstLetter.Checked += new System.Windows.RoutedEventHandler(this.checkbox_SearchByFirstLetter_Checked);
            
            #line default
            #line hidden
            
            #line 16 "..\..\MyBooks.xaml"
            this.checkbox_SearchByFirstLetter.Unchecked += new System.Windows.RoutedEventHandler(this.checkbox_SearchByFirstLetter_Unchecked);
            
            #line default
            #line hidden
            return;
            case 7:
            this.label_CurrentLimit = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

