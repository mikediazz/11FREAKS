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
        int caso;

        ////////// CASOS DE USO //////////
        /*
         Caso 1| Sign up Admin
         Caso 2| Cambio de Nombre de Usuario
         Caso 3| Cambio de Contraseña
         */

        public GestionUsuarios(Principal fprincipal, BaseDatos bbdd, int tipoCaso)
        {
            InitializeComponent();
            principal= fprincipal;
            miBaseDatos = bbdd;
            caso=tipoCaso;


            switch (caso)
            {
                case 1:                         //SIGN UP ADMIN
                    btnRegistrarAdmin.Content = "Registrar Admin";
                    break;

                case 2:                         //CAMBIO NOMBRE USUARIO
                    lbl1.Visibility = Visibility.Collapsed;
                    txtUsuario.Visibility = Visibility.Collapsed;
                    lbl2.Content = "NUEVO NOMBRE USUARIO";
                    lbl3.Visibility = Visibility.Collapsed;
                    txtEmail.Visibility = Visibility.Collapsed;
                    btnRegistrarAdmin.Content = "Confirmar";
                    break;

                case 3:                        //CAMBIO CONTRASEÑA
                    lbl1.Content = "NUEVA CONTRASEÑA";
                    lbl2.Content = "REPITA NUEVA CONTRASEÑA";
                    lbl3.Visibility = Visibility.Collapsed;
                    txtEmail.Visibility= Visibility.Collapsed;
                    btnRegistrarAdmin.Content = "Confirmar";
                    break;


                case 4:                        //CAMBIO CORREO ELECTRÓNICO
                    lbl1.Content = "NUEVO EMAIL";
                    lbl2.Content = "REPITA NUEVO EMAIL";
                    lbl3.Visibility = Visibility.Collapsed;
                    txtEmail.Visibility = Visibility.Collapsed;
                    btnRegistrarAdmin.Content = "Confirmar";

                    break;
            }
        }

        /// <summary>
        ///     Función Asociada al Botón "CREAR USUARIO" de la ventana de Registro
        /// </summary>
        private void btnRegistrarse_Click(object sender, RoutedEventArgs e)
        {
            Confirmacion confirmacion = new Confirmacion(this);                 //VERIFICAMOS QUE REALMENTE QUIERE REALIZAR LA OPERACIÓN
            confirmacion.ShowDialog();
            if (confirmacion.DevuelveRespuesta() == true)
            {

                switch (caso)
                {
                    case 1:             //SIGN UP ADMIN
                        
                        if (miBaseDatos.CompruebaPassword(txtUsuario.Text, txtPassword.Password))
                        {
                            if (miBaseDatos.Conectar(txtUsuario.Text, txtPassword.Password) == true)
                            {
                                var mensajeInvalidUsername = AutoClosingMessageBox.Show(
                                text: "Whoops! YA EXISTE ESTE USUARIO",
                                caption: "EQUIPO DE 11FREAKS",
                                timeout: 3000,
                                buttons: MessageBoxButtons.OK);
                            }
                            else
                            {
                                var mensajeTemporal1 = AutoClosingMessageBox.Show(
                                text: "**USUARIO CREADO**",
                                caption: "EQUIPO DE 11FREAKS",
                                timeout: 1500,
                                buttons: MessageBoxButtons.OK);
                                miBaseDatos.CrearUsuario(txtUsuario.Text, txtPassword.Password, true, null, txtEmail.Text);  //CREAMOS ADMIN

                                this.Hide();
                                principal.ShowDialog();
                            }

                        }
                        else
                        {                                                               //CONTRASEÑA NO VÁLIDA
                            var mensajeInvalidPass = AutoClosingMessageBox.Show(
                            text: "Whoops! LA CONTRASEÑA NO CUMPLE LOS REQUISITOS",
                            caption: "EQUIPO DE 11FREAKS",
                            timeout: 3000,
                            buttons: MessageBoxButtons.OK);
                        }
                        break;

                    case 2:                 //CAMBIO NOMBRE USUARIO

                        break;


                    case 3:                 //CAMBIO CONTRASEÑA

                        //DEBERÍAMOS COMPROBAR QUE AMBAS CONTRASEÑAS COINCIDEN

                        var mensajeTemporal3 = AutoClosingMessageBox.Show(
                        text: "SU CONTRASEÑA NUEVA VA A SER "+txtPassword.Password,
                        caption: "EQUIPO DE 11FREAKS",
                        timeout: 3000,
                        buttons: MessageBoxButtons.OK);

                        miBaseDatos.CambiarContraseña(txtPassword.Password);    
                        
                            var mensajeTemporal = AutoClosingMessageBox.Show(
                            text: "SU CONTRASEÑA HA SIDO RESTABLECIDA",
                            caption: "EQUIPO DE 11FREAKS",
                            timeout: 3000,
                            buttons: MessageBoxButtons.OK);


                            Correo correo = new Correo();

                        var mensajePrueba2 = AutoClosingMessageBox.Show(
                        text: "NOMBRE DEL USUARIO ->" + miBaseDatos.DevuelveUsuario(),
                        caption: "EQUIPO DE 11FREAKS",
                        timeout: 3000,
                        buttons: MessageBoxButtons.OK);
                        

                        var mensajePrueba3 = AutoClosingMessageBox.Show(
                        text: "EMAIL DEL USUARIO ->"+ miBaseDatos.DevuelveCorreo().ToString(),
                        caption: "EQUIPO DE 11FREAKS",
                        timeout: 3000,
                        buttons: MessageBoxButtons.OK);

                        correo.CorreoContraseña(miBaseDatos.DevuelveCorreo());     //ENVIAMOS CORREO INFORMATIVO AL USUARIO

                            this.Hide();
                        
                        break;

                    default:
                        var mensajePrueba = AutoClosingMessageBox.Show(
                        text: "**NO ENTRO EN LOS CASOS** " + caso,
                        caption: "EQUIPO DE 11FREAKS",
                        timeout: 3000,
                        buttons: MessageBoxButtons.OK);
                        break;
                }


            }




        }
    }
}
