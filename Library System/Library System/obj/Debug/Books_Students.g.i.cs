// Updated by XamlIntelliSenseFileGenerator 6/1/2020 1:35:33 PM
#pragma checksum "..\..\Books_Students.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "A290CFD0B54BCB7EE9E63BEE08FECCB96F3497383A73D88FA47003DA6BA7563E"
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


namespace Library_System
{


    /// <summary>
    /// Books_Students
    /// </summary>
    public partial class Books : System.Windows.Window, System.Windows.Markup.IComponentConnector
    {

#line default
#line hidden


#line 12 "..\..\Books_Students.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button_TakeSelectedBook;

#line default
#line hidden


#line 16 "..\..\Books_Students.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox checkbox_InStockOnly;

#line default
#line hidden

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Library System;component/books_students.xaml", System.UriKind.Relative);

#line 1 "..\..\Books_Students.xaml"
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
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.datagrid_StudentsBooksDisplay = ((System.Windows.Controls.DataGrid)(target));
                    return;
                case 2:
                    this.button_TakeSelectedBook = ((System.Windows.Controls.Button)(target));

#line 12 "..\..\Books_Students.xaml"
                    this.button_TakeSelectedBook.Click += new System.Windows.RoutedEventHandler(this.button_TakeSelectedBook_Click);

#line default
#line hidden
                    return;
                case 3:
                    this.button_StudentsReturnBackToMainMenu = ((System.Windows.Controls.Button)(target));

#line 13 "..\..\Books_Students.xaml"
                    this.button_StudentsReturnBackToMainMenu.Click += new System.Windows.RoutedEventHandler(this.button_StudentsReturnBackToMainMenu_Click);

#line default
#line hidden
                    return;
                case 4:
                    this.textbok_SearchBooksStudent = ((System.Windows.Controls.TextBox)(target));

#line 14 "..\..\Books_Students.xaml"
                    this.textbok_SearchBooksStudent.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.textbok_SearchBooksStudent_TextChanged);

#line default
#line hidden
                    return;
                case 5:
                    this.button_SearchBooksStudent = ((System.Windows.Controls.Button)(target));

#line 15 "..\..\Books_Students.xaml"
                    this.button_SearchBooksStudent.Click += new System.Windows.RoutedEventHandler(this.button_SearchBooksStudent_Click);

#line default
#line hidden
                    return;
                case 6:
                    this.checkbox_InStockOnly = ((System.Windows.Controls.CheckBox)(target));

#line 16 "..\..\Books_Students.xaml"
                    this.checkbox_InStockOnly.Checked += new System.Windows.RoutedEventHandler(this.checkbox_InStockOnly_Checked);

#line default
#line hidden

#line 16 "..\..\Books_Students.xaml"
                    this.checkbox_InStockOnly.Unchecked += new System.Windows.RoutedEventHandler(this.checkbox_InStockOnly_Unchecked);

#line default
#line hidden
                    return;
            }
            this._contentLoaded = true;
        }

        internal System.Windows.Controls.Button button_SearchBooks;
        internal System.Windows.Controls.Button button_ReturnBackToMainMenu;
        internal System.Windows.Controls.DataGrid datagrid_BooksDisplay;
        internal System.Windows.Controls.TextBox textbok_SearchBooks;
    }
}
