#pragma checksum "..\..\RankApprovingWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "C6FC269C3E02205BE8551E856D9FE122692C68A934E4A19AE0AE5A9A2F3989FD"
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
    /// RankApprovingWindow
    /// </summary>
    public partial class RankApprovingWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\RankApprovingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid datagrid_RankApproval;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\RankApprovingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button_SetTeacher;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\RankApprovingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button_SetAdmin;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\RankApprovingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button_ReturnToMainMenu;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\RankApprovingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textbox_Search;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\RankApprovingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button_Search;
        
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
            System.Uri resourceLocater = new System.Uri("/Library System;component/rankapprovingwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\RankApprovingWindow.xaml"
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
            this.datagrid_RankApproval = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 2:
            this.button_SetTeacher = ((System.Windows.Controls.Button)(target));
            
            #line 12 "..\..\RankApprovingWindow.xaml"
            this.button_SetTeacher.Click += new System.Windows.RoutedEventHandler(this.button_SetTeacher_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.button_SetAdmin = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\RankApprovingWindow.xaml"
            this.button_SetAdmin.Click += new System.Windows.RoutedEventHandler(this.button_SetAdmin_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.button_ReturnToMainMenu = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\RankApprovingWindow.xaml"
            this.button_ReturnToMainMenu.Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.textbox_Search = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.button_Search = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\RankApprovingWindow.xaml"
            this.button_Search.Click += new System.Windows.RoutedEventHandler(this.button_Search_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

