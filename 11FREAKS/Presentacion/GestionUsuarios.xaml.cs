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
    /// Clase GestionUsuarios.xaml || Tiene la función de administrar usuarios y otorgar funcionalidad
    /// </summary>
    public partial class GestionUsuarios : Window
    {
        private Datos.BaseDatos miBaseDatos;
        private Datos.BDOnline bdServer;
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
            bdServer = new BDOnline();
            caso=tipoCaso;


            switch (caso)
            {
                case 1:                         //SIGN UP ADMIN
                    btnRegistrarAdmin.Content = "Registrar Admin";
                    break;

                case 2:                         //CAMBIO NOMBRE USUARIO
                    lbl1.Visibility = Visibility.Collapsed;
                    txtUsuario.Visibility = Visibility.Collapsed;
                    txtPassword.Visibility = Visibility.Collapsed;
                    lbl2.Content = "NUEVO NOMBRE USUARIO";
                    txtMedio.Visibility = Visibility.Visible;
                    lbl3.Visibility = Visibility.Collapsed;
                    txtEmail.Visibility = Visibility.Collapsed;
                    btnRegistrarAdmin.Content = "Confirmar";
                    break;

                case 3:                        //CAMBIO CONTRASEÑA
                    lbl1.Content = "NUEVA CONTRASEÑA";
                    txtPassword1.Visibility = Visibility.Visible;
                    lbl2.Content = "REPITA NUEVA CONTRASEÑA";
                    lbl3.Visibility = Visibility.Collapsed;
                    txtEmail.Visibility= Visibility.Collapsed;
                    btnRegistrarAdmin.Content = "Confirmar";
                    break;


                case 4:                        //CAMBIO CORREO ELECTRÓNICO
                    lbl1.Content = "NUEVO EMAIL";
                    lbl2.Content = "REPITA NUEVO EMAIL";
                    txtMedio.Visibility = Visibility.Visible;
                    lbl3.Visibility = Visibility.Collapsed;
                    txtEmail.Visibility = Visibility.Collapsed;
                    txtPassword.Visibility = Visibility.Collapsed; 
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
                        
                        if (bdServer.CompruebaPassword(txtUsuario.Text, txtPassword.Password))
                        {
                            if (bdServer.ConectarServer(txtUsuario.Text, txtPassword.Password) == true)
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
                                //miBaseDatos.CrearUsuario(txtUsuario.Text, txtPassword.Password, true, null, txtEmail.Text);  //CREAMOS ADMIN
                                bdServer.CrearUsuario(txtUsuario.Text, txtPassword.Password, txtEmail.Text);  //CREAMOS ADMIN
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
                        this.Close();

                        break;

                    case 2:                 //CAMBIO NOMBRE USUARIO

                        break;


                    case 3:                 //CAMBIO CONTRASEÑA

                        //DEBERÍAMOS COMPROBAR QUE AMBAS CONTRASEÑAS COINCIDEN

                        
                        if (CompruebaCamposCaso3() > 0)
                        {
                            var mensajeDifPass = AutoClosingMessageBox.Show(           //ALERTA ERROR
                            text: "COMPRUEBE QUE AMBAS CONTRASEÑAS COINCIDEN",
                            caption: "EQUIPO DE 11FREAKS",
                            timeout: 2500,
                            buttons: MessageBoxButtons.OK);
                            txtPassword.Clear();                                        //Reseteamos Campos
                            txtPassword1.Clear();
                        }
                        else
                        {
                            bdServer.CambiarContraseña(txtPassword.Password);

                            var mensajeTemporal = AutoClosingMessageBox.Show(           //ALERTA INFORMATIVA
                            text: "SU CONTRASEÑA HA SIDO RESTABLECIDA",
                            caption: "EQUIPO DE 11FREAKS",
                            timeout: 2500,
                            buttons: MessageBoxButtons.OK);

                            Correo correoPass = new Correo();
                            correoPass.CorreoContraseña(bdServer.DevuelveCorreo());     //ENVIAMOS CORREO INFORMATIVO AL USUARIO
                            this.Close();
                        }



                        
                        
                        break;


                    case 4:
                        bdServer.CambiarCorreo(bdServer.DevuelveUsuario(),bdServer.DevuelveCorreo());
                        
                        Correo correo = new Correo();
                        correo.CorreoCambioEmail(bdServer.DevuelveCorreo());     //ENVIAMOS CORREO INFORMATIVO AL USUARIO
                        this.Close();
                        break;


                    default:
                        var mensajePrueba = AutoClosingMessageBox.Show(
                        text: "**NO ENTRO EN LOS CASOS** " + caso,
                        caption: "EQUIPO DE 11FREAKS",
                        timeout: 3000,
                        buttons: MessageBoxButtons.OK);
                        this.Close();
                        break;
                }


            }
            else
            {
                this.Close();               //SI NO QUIERE CONTINUAR CON LOS CAMBIOS, SE CIERRA VENTANA
            }




        }



        public int CompruebaCamposCaso1()
        {
            int comprobador = 0;          //VARIABLE COMPROBACIÓN REQUISITOS

            if (txtUsuario == null || txtUsuario.Text == string.Empty)
            {
                comprobador += 1;
            }
            if (txtPassword == null || txtPassword.Password == string.Empty)
            {
                comprobador += 1;
            }
            if (txtEmail == null || txtEmail.Text == string.Empty)
            {
                comprobador += 1;
            }
            return comprobador;
        }


        public int CompruebaCamposCaso2()                                       //////////////////////////////////////////////////COMPLETAR
        {
            int comprobador = 0;          //VARIABLE COMPROBACIÓN REQUISITOS

            if (txtUsuario == null || txtUsuario.Text == string.Empty)
            {
                comprobador += 1;
            }
            if (txtPassword == null || txtPassword.Password == string.Empty)
            {
                comprobador += 1;
            }
            if (txtEmail == null || txtEmail.Text == string.Empty)
            {
                comprobador += 1;
            }
            return comprobador;
        }

        public int CompruebaCamposCaso3()
        {
            int comprobador = 0;          //VARIABLE COMPROBACIÓN REQUISITOS

            if (txtPassword1 == null || txtPassword1.Password == string.Empty)
            {
                comprobador += 1;
            }
            if (txtPassword == null || txtPassword.Password == string.Empty)
            {
                comprobador += 1;
            }
            if (txtPassword1.Password != txtPassword.Password)
            {
                comprobador += 1;
            }
            return comprobador;
        }


        public int CompruebaCamposCaso4()
        {
            int comprobador = 0;          //VARIABLE COMPROBACIÓN REQUISITOS

            if (txtUsuario == null || txtUsuario.Text == string.Empty)
            {
                comprobador += 1;
            }
            if (txtMedio == null || txtMedio.Text == string.Empty)
            {
                comprobador += 1;
            }
            if (txtUsuario.Text != txtMedio.Text)
            {
                comprobador += 1;
            }
            return comprobador;
        }
    }
}
