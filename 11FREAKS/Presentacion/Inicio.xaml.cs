using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _11FREAKS.Presentacion
{
    /// <summary>
    /// Clase Interacción Inicio.xaml || Es la ventana de Login (PODREMOS CREAR USUARIO O INICIAR SESIÓN)
    /// </summary>
    public partial class Inicio : Window
    {
        private Datos.BaseDatos miBaseDatos;

        public Inicio()
        {
            InitializeComponent();
            miBaseDatos = new Datos.BaseDatos();
        }

        /// <summary>
        ///     Función  Vinculada al Botón Iniciar Sesión --> Nos Envía a la Ventana "PRINCIPAL"
        /// </summary>
        private void btnInicioSesion_Click(object sender, RoutedEventArgs e)
        {
            miBaseDatos = new Datos.BaseDatos();
            if (miBaseDatos.Conectar(txtUsuario.Text, txtPassword.Password) == true)
            {
                Principal principal = new Principal(this, miBaseDatos);
                this.Hide();
                principal.ShowDialog();
            }
        }

        /// <summary>
        ///     Función Vinculada al Botón Registrarse --> Nos Envía a la Ventana "REGISTRO"
        /// </summary>

        private void btnRegistrarse_Click(object sender, RoutedEventArgs e)
        {
            RegistroUsuario registro = new RegistroUsuario(this);
            this.Hide();
            registro.ShowDialog();
        }

        /// <summary>
        ///     Función Para Dar la Bienvenida al Usuario
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var mensajeTemporal = AutoClosingMessageBox.Show(
            text: "BIENVENIDO A 11FREAKS",
            caption: "EQUIPO DE 11FREAKS",
            timeout: 3000,
            buttons: MessageBoxButtons.OK);//showCountDown:true
            
        }



    }
}
