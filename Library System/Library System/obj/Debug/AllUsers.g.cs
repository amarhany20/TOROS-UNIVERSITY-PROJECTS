#pragma checksum "..\..\AllUsers.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "36C6AA208E92FF08ACD1BF221DD3B93DF88C8CE07B9A51D6835D7B4F6A2803AF"
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
    /// AllUsers
    /// </summary>
    public partial class AllUsers : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\AllUsers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid datagrid_AllUsers;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\AllUsers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textbox_search;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\AllUsers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button_Search;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\AllUsers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button_UpdateChanges;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\AllUsers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Button_ReturnToMainMenu;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\AllUsers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button_DeleteUser;
        
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
            System.Uri resourceLocater = new System.Uri("/Library System;component/allusers.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\AllUsers.xaml"
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
            this.datagrid_AllUsers = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 2:
            this.textbox_search = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.button_Search = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\AllUsers.xaml"
            this.button_Search.Click += new System.Windows.RoutedEventHandler(this.button_Search_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.button_UpdateChanges = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\AllUsers.xaml"
            this.button_UpdateChanges.Click += new System.Windows.RoutedEventHandler(this.button_UpdateChanges_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Button_ReturnToMainMenu = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\AllUsers.xaml"
            this.Button_ReturnToMainMenu.Click += new System.Windows.RoutedEventHandler(this.Button_ReturnToMainMenu_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.button_DeleteUser = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\AllUsers.xaml"
            this.button_DeleteUser.Click += new System.Windows.RoutedEventHandler(this.button_DeleteUser_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

