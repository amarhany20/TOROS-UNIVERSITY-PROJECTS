#pragma checksum "..\..\EditOrders.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "DDEB0A8CA436E075594FFE034686E4CA9173BB05A8CEBD623D045F95CEC871E9"
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
    /// EditOrders
    /// </summary>
    public partial class EditOrders : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 1 "..\..\EditOrders.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Cashier.EditOrders EditOrdersWindow;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\EditOrders.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textbox_Search;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\EditOrders.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dg_Orders;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\EditOrders.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Back;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\EditOrders.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Delete;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\EditOrders.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox combobox_PaymentMethod;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\EditOrders.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_PaymentMethod;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\EditOrders.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox combobox_user;
        
        #line default
        #line hidden
        
        
        #line 83 "..\..\EditOrders.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_user;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\EditOrders.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_OrderDetails;
        
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
            System.Uri resourceLocater = new System.Uri("/Cashier;component/editorders.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\EditOrders.xaml"
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
            this.EditOrdersWindow = ((Cashier.EditOrders)(target));
            return;
            case 2:
            this.textbox_Search = ((System.Windows.Controls.TextBox)(target));
            
            #line 43 "..\..\EditOrders.xaml"
            this.textbox_Search.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.textbox_Search_TextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.dg_Orders = ((System.Windows.Controls.DataGrid)(target));
            
            #line 49 "..\..\EditOrders.xaml"
            this.dg_Orders.AutoGeneratingColumn += new System.EventHandler<System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs>(this.dg_Orders_AutoGeneratingColumn);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btn_Back = ((System.Windows.Controls.Button)(target));
            
            #line 61 "..\..\EditOrders.xaml"
            this.btn_Back.Click += new System.Windows.RoutedEventHandler(this.btn_Back_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btn_Delete = ((System.Windows.Controls.Button)(target));
            
            #line 62 "..\..\EditOrders.xaml"
            this.btn_Delete.Click += new System.Windows.RoutedEventHandler(this.btn_Delete_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.combobox_PaymentMethod = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.btn_PaymentMethod = ((System.Windows.Controls.Button)(target));
            
            #line 72 "..\..\EditOrders.xaml"
            this.btn_PaymentMethod.Click += new System.Windows.RoutedEventHandler(this.btn_PaymentMethod_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.combobox_user = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 9:
            this.btn_user = ((System.Windows.Controls.Button)(target));
            
            #line 83 "..\..\EditOrders.xaml"
            this.btn_user.Click += new System.Windows.RoutedEventHandler(this.btn_user_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.btn_OrderDetails = ((System.Windows.Controls.Button)(target));
            
            #line 87 "..\..\EditOrders.xaml"
            this.btn_OrderDetails.Click += new System.Windows.RoutedEventHandler(this.btn_OrderDetails_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

