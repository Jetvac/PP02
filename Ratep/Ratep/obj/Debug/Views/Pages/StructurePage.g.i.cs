﻿#pragma checksum "..\..\..\..\Views\Pages\StructurePage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "3118DC018133EDE3910FF91CA5393C3373E2D423D0D34B63C405975F385F07A7"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Ratep.Pages;
using ScottPlot;
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


namespace Ratep.Pages {
    
    
    /// <summary>
    /// StructurePage
    /// </summary>
    public partial class StructurePage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 23 "..\..\..\..\Views\Pages\StructurePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SelectedTxtBx;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\Views\Pages\StructurePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TreeView STData;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\..\Views\Pages\StructurePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GroupBox ControlPanel;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\..\Views\Pages\StructurePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SelectedItemNameTxtBx;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\..\..\Views\Pages\StructurePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox AmmountTxtBx;
        
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
            System.Uri resourceLocater = new System.Uri("/Ratep;component/views/pages/structurepage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\Pages\StructurePage.xaml"
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
            this.SelectedTxtBx = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            
            #line 25 "..\..\..\..\Views\Pages\StructurePage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ItemNavigationButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.STData = ((System.Windows.Controls.TreeView)(target));
            
            #line 29 "..\..\..\..\Views\Pages\StructurePage.xaml"
            this.STData.SelectedItemChanged += new System.Windows.RoutedPropertyChangedEventHandler<object>(this.STData_SelectedItemChanged);
            
            #line default
            #line hidden
            
            #line 29 "..\..\..\..\Views\Pages\StructurePage.xaml"
            this.STData.PreviewMouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.STData_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ControlPanel = ((System.Windows.Controls.GroupBox)(target));
            return;
            case 6:
            this.SelectedItemNameTxtBx = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            
            #line 62 "..\..\..\..\Views\Pages\StructurePage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.MiscButton_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.AmmountTxtBx = ((System.Windows.Controls.TextBox)(target));
            
            #line 68 "..\..\..\..\Views\Pages\StructurePage.xaml"
            this.AmmountTxtBx.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.AmmountTxtBx_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 70 "..\..\..\..\Views\Pages\StructurePage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.BindButton_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 74 "..\..\..\..\Views\Pages\StructurePage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.UnbindButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            System.Windows.EventSetter eventSetter;
            switch (connectionId)
            {
            case 4:
            eventSetter = new System.Windows.EventSetter();
            eventSetter.Event = System.Windows.Controls.TreeViewItem.ExpandedEvent;
            
            #line 32 "..\..\..\..\Views\Pages\StructurePage.xaml"
            eventSetter.Handler = new System.Windows.RoutedEventHandler(this.STData_Expanded);
            
            #line default
            #line hidden
            ((System.Windows.Style)(target)).Setters.Add(eventSetter);
            break;
            }
        }
    }
}

