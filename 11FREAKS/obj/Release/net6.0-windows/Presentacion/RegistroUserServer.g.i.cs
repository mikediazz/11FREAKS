﻿#pragma checksum "..\..\..\..\Presentacion\RegistroUserServer.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "C20C9F80D8F56536C8B5C594CD86FC125BB2ED61"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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
using _11FREAKS.Presentacion;


namespace _11FREAKS.Presentacion {
    
    
    /// <summary>
    /// RegistroUserServer
    /// </summary>
    public partial class RegistroUserServer : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\..\Presentacion\RegistroUserServer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtUsuario;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\..\Presentacion\RegistroUserServer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox txtPassword;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\..\Presentacion\RegistroUserServer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnRegistrarse;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\..\Presentacion\RegistroUserServer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtEmail;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\..\Presentacion\RegistroUserServer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtEquipo;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\..\Presentacion\RegistroUserServer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblAbrev;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\..\..\Presentacion\RegistroUserServer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtAbreviatura;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.5.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/11FREAKS;V1.0.0.0;component/presentacion/registrouserserver.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Presentacion\RegistroUserServer.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.5.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 8 "..\..\..\..\Presentacion\RegistroUserServer.xaml"
            ((_11FREAKS.Presentacion.RegistroUserServer)(target)).Closed += new System.EventHandler(this.Window_Closed);
            
            #line default
            #line hidden
            return;
            case 2:
            this.txtUsuario = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.txtPassword = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 4:
            this.btnRegistrarse = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\..\..\Presentacion\RegistroUserServer.xaml"
            this.btnRegistrarse.Click += new System.Windows.RoutedEventHandler(this.btnRegistrarse_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.txtEmail = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.txtEquipo = ((System.Windows.Controls.TextBox)(target));
            
            #line 54 "..\..\..\..\Presentacion\RegistroUserServer.xaml"
            this.txtEquipo.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtEquipo_TextChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.lblAbrev = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.txtAbreviatura = ((System.Windows.Controls.TextBox)(target));
            
            #line 64 "..\..\..\..\Presentacion\RegistroUserServer.xaml"
            this.txtAbreviatura.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.txtEquipo_TextChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 92 "..\..\..\..\Presentacion\RegistroUserServer.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

