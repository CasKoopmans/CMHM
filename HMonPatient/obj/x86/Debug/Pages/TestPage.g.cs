﻿#pragma checksum "C:\Users\Cas\Desktop\School\Jaar 2 Poging 2\Periode 1\Proftaak individueel\CMHM\HMonPatient\Pages\TestPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "30566EAA6F302C0E406CF1163FCA99EF"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HMonPatient.Pages
{
    partial class TestPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    this.DeviceListSource = (global::Windows.UI.Xaml.Data.CollectionViewSource)(target);
                }
                break;
            case 2:
                {
                    this.pageTitle = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 3:
                {
                    this.status = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 4:
                {
                    this.rcvdText = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 5:
                {
                    this.sendTextButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 63 "..\..\..\Pages\TestPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.sendTextButton).Click += this.sendTextButton_Click;
                    #line default
                }
                break;
            case 6:
                {
                    this.sendText = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 7:
                {
                    this.comPortInput = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 50 "..\..\..\Pages\TestPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.comPortInput).Click += this.comPortInput_Click;
                    #line default
                }
                break;
            case 8:
                {
                    this.closeDevice = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 51 "..\..\..\Pages\TestPage.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.closeDevice).Click += this.closeDevice_Click;
                    #line default
                }
                break;
            case 9:
                {
                    this.ConnectDevices = (global::Windows.UI.Xaml.Controls.ListBox)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}
