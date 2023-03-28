using _11FREAKS.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _11FREAKS.Presentacion
{
    /// <summary>
    /// Clase GestionUsuarios.xaml || Tiene la función de administrar usuarios (SOLO ACCESIBLE POR ADMINISTRADORES) 
    /// </summary>
    public partial class GestionUsuarios : Window
    {
        private Datos.BaseDatos miBaseDatos;
        Principal principal;

        public GestionUsuarios(Principal fprincipal)
        {
            InitializeComponent();
            principal= fprincipal;
            miBaseDatos = new Datos.BaseDatos();
        }

        /// <summary>
        ///     Función Asociada al Botón "CREAR USUARIO" de la ventana de Registro
        /// </summary>
        private void btnRegistrarse_Click(object sender, RoutedEventArgs e)
        {
            miBaseDatos = new Datos.BaseDatos();
            if (miBaseDatos.CompruebaPassword(txtUsuario.Text,txtPassword.Password))
            {
                if (miBaseDatos.Conectar(txtUsuario.Text, txtPassword.Password) == true)
                {
                    var mensajeTemporal = AutoClosingMessageBox.Show(
                    text: "Whoops! YA EXISTE ESTE USUARIO",
                    caption: "EQUIPO DE 11FREAKS",
                    timeout: 3000,
                    buttons: MessageBoxButtons.OK);
                }
                else
                {
                    var mensajeTemporal = AutoClosingMessageBox.Show(
                    text: "**USUARIO CREADO**",
                    caption: "EQUIPO DE 11FREAKS",
                    timeout: 1500,
                    buttons: MessageBoxButtons.OK);
                    miBaseDatos.CrearUsuario(txtUsuario.Text, txtPassword.Password, true, null);  //CREAMOS ADMIN

                    this.Hide();
                    principal.ShowDialog();
                }

            }
            else
            {                                                               //CONTRASEÑA NO VÁLIDA
                var mensajeTemporal = AutoClosingMessageBox.Show(
                text: "Whoops! LA CONTRASEÑA NO CUMPLE LOS REQUISITOS",
                caption: "EQUIPO DE 11FREAKS",
                timeout: 3000,
                buttons: MessageBoxButtons.OK);
            }

        }
    }
}
