using _11FREAKS.Datos;
using _11FREAKS.Presentacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Lógica de interacción para RegistroUserServer.xaml
    /// </summary>
    public partial class RegistroUserServer : Window
    {
        private Inicio inicio;
        private Datos.BDOnline bdServer;
        public RegistroUserServer(Inicio pInicio)
        {
            InitializeComponent();
            this.inicio = pInicio;
            bdServer=new BDOnline();
        }


        private void txtEquipo_TextChanged(object sender, TextChangedEventArgs e)
        {

        }



        private void btnRegistrarse_Click(object sender, RoutedEventArgs e)
        {

            if (bdServer.CompruebaPassword(txtUsuario.Text, txtPassword.Password))
            {
                if (bdServer.ConectarServer(txtUsuario.Text, txtPassword.Password) == true)
                {
                    var mensajeTemporal = AutoClosingMessageBox.Show(
                    text: "Whoops! YA EXISTE ESTE USUARIO",
                    caption: "EQUIPO DE 11FREAKS",
                    timeout: 3000,
                    buttons: MessageBoxButtons.OK);
                }
                else
                {
                    if (CompruebaCampos() > 0)
                    {
                        var mensajeTemporal = AutoClosingMessageBox.Show(
                        text: "**DEBE RELLENAR TODOS LOS CAMPOS PARA COMPLETAR EL REGISTRO**",
                        caption: "EQUIPO DE 11FREAKS",
                        timeout: 1500,
                        buttons: MessageBoxButtons.OK);
                    }
                    else
                    {
                        bdServer.CrearEquipo(txtEquipo.Text, txtAbreviatura.Text, "1");     //CREAMOS EQUIPO -> LO METEMOS EN LIGA 1
                            var msgCreacionEquipo = AutoClosingMessageBox.Show(
                            text: "CREANDO USUARIO...",
                            caption: "EQUIPO DE 11FREAKS",
                            timeout: 1500,
                            buttons: MessageBoxButtons.OK);

                        bdServer.CrearUsuario(txtUsuario.Text, txtPassword.Password, txtEquipo.Text.ToUpper(), txtEmail.Text, bdServer.DevuelveIdEquipo(txtEquipo.Text.ToUpper()));          //CREAMOS USUARIO
                            var msgCreacionUser = AutoClosingMessageBox.Show(
                            text: "CREANDO USUARIO...",
                            caption: "EQUIPO DE 11FREAKS",
                            timeout: 1500,
                            buttons: MessageBoxButtons.OK);

                        bdServer.GenerarEquipo(bdServer.DevuelveUsuario(), txtEquipo.Text.ToUpper());                 //GENERAMOS EQUIPO -> DRAFT JUGADORES
                            var msgDraft = AutoClosingMessageBox.Show(
                            text: "HACIENDO DRAFT...",
                            caption: "EQUIPO DE 11FREAKS",
                            timeout: 1500,
                            buttons: MessageBoxButtons.OK);

                        var mensajeTemporal = AutoClosingMessageBox.Show(
                        text: "**USUARIO CREADO**    ID EQUIPO",
                        caption: "EQUIPO DE 11FREAKS",
                        timeout: 1500,
                        buttons: MessageBoxButtons.OK);

                        Correo mailBienvenida = new Correo();                                                 //ENVIAMOS CORREO DE BIENVENIDA AL USUARIO
                                                                                                              // mailBienvenida.CorreoBienvenida(txtEmail.Text);

                        /*Thread volumenThread = new Thread(new ThreadStart(mailBienvenida.CorreoBienvenida(txtEmail.)));          //CREAMOS HILO AL QUE LE PASAMOS EL MÉTODO PARA INICIAR INTRO                                                                        
                        volumenThread.Start();*/

                        /*  Thread hilo = new Thread(new ParameterizedThreadStart(mailBienvenida.CorreoBienvenida);
                          hilo.Start(txtEmail.Text);*/
                       
                        
                        /*try
                        {
                            Thread thMailBienvenida = new Thread(() => mailBienvenida.CorreoBienvenida(txtEmail.Text));
                            thMailBienvenida.Start();
                        }
                        catch (Exception ex)
                        {
                            var mensajeCorreo = AutoClosingMessageBox.Show(
                            text: "NO SE PUDO ENVIAR CORREO " + ex.Message,
                            caption: "EQUIPO DE 11FREAKS",
                            timeout: 1500,
                            buttons: MessageBoxButtons.OK);
                        }*/


                        Principal principal = new Principal(inicio, bdServer, txtUsuario.Text);
                        this.Hide();
                        principal.ShowDialog();
                    }

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



        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
        }








        private int CompruebaCampos()
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
            if (txtEquipo == null || txtEquipo.Text == string.Empty)
            {
                comprobador += 1;
            }

            return comprobador;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (bdServer.CompruebaPassword(txtUsuario.Text, txtPassword.Password))
            {
                if (bdServer.ConectarServer(txtUsuario.Text, txtPassword.Password) == true)
                {
                    var mensajeTemporal = AutoClosingMessageBox.Show(
                    text: "Whoops! YA EXISTE ESTE USUARIO",
                    caption: "EQUIPO DE 11FREAKS",
                    timeout: 3000,
                    buttons: MessageBoxButtons.OK);
                }
                else
                {
                    if (CompruebaCampos() > 0)
                    {
                        var mensajeTemporal = AutoClosingMessageBox.Show(
                        text: "**DEBE RELLENAR TODOS LOS CAMPOS PARA COMPLETAR EL REGISTRO**",
                        caption: "EQUIPO DE 11FREAKS",
                        timeout: 1500,
                        buttons: MessageBoxButtons.OK);
                    }
                    else
                    {
                        bdServer.CrearEquipo(txtEquipo.Text, txtAbreviatura.Text, "1");     //CREAMOS EQUIPO -> LO METEMOS EN LIGA 1
                        var msgCreacionEquipo = AutoClosingMessageBox.Show(
                        text: "CREANDO USUARIO...",
                        caption: "EQUIPO DE 11FREAKS",
                        timeout: 1500,
                        buttons: MessageBoxButtons.OK);

                        bdServer.CrearUsuario(txtUsuario.Text, txtPassword.Password, txtEquipo.Text.ToUpper(), txtEmail.Text, bdServer.DevuelveIdEquipo(txtEquipo.Text.ToUpper()));          //CREAMOS USUARIO
                        var msgCreacionUser = AutoClosingMessageBox.Show(
                        text: "CREANDO USUARIO...",
                        caption: "EQUIPO DE 11FREAKS",
                        timeout: 1500,
                        buttons: MessageBoxButtons.OK);

                        bdServer.GenerarEquipo(bdServer.DevuelveUsuario(), txtEquipo.Text.ToUpper());                 //GENERAMOS EQUIPO -> DRAFT JUGADORES
                        var msgDraft = AutoClosingMessageBox.Show(
                        text: "HACIENDO DRAFT...",
                        caption: "EQUIPO DE 11FREAKS",
                        timeout: 1500,
                        buttons: MessageBoxButtons.OK);

                        var mensajeTemporal = AutoClosingMessageBox.Show(
                        text: "**USUARIO CREADO**    ID EQUIPO",
                        caption: "EQUIPO DE 11FREAKS",
                        timeout: 1500,
                        buttons: MessageBoxButtons.OK);

                        Correo mailBienvenida = new Correo();                                                 //ENVIAMOS CORREO DE BIENVENIDA AL USUARIO
                                                                                                              // mailBienvenida.CorreoBienvenida(txtEmail.Text);

                        /*Thread volumenThread = new Thread(new ThreadStart(mailBienvenida.CorreoBienvenida(txtEmail.)));          //CREAMOS HILO AL QUE LE PASAMOS EL MÉTODO PARA INICIAR INTRO                                                                        
                        volumenThread.Start();*/

                        /*  Thread hilo = new Thread(new ParameterizedThreadStart(mailBienvenida.CorreoBienvenida);
                          hilo.Start(txtEmail.Text);*/


                        /*try
                        {
                            Thread thMailBienvenida = new Thread(() => mailBienvenida.CorreoBienvenida(txtEmail.Text));
                            thMailBienvenida.Start();
                        }
                        catch (Exception ex)
                        {
                            var mensajeCorreo = AutoClosingMessageBox.Show(
                            text: "NO SE PUDO ENVIAR CORREO " + ex.Message,
                            caption: "EQUIPO DE 11FREAKS",
                            timeout: 1500,
                            buttons: MessageBoxButtons.OK);
                        }*/


                        Principal principal = new Principal(inicio, bdServer, txtUsuario.Text);
                        this.Hide();
                        principal.ShowDialog();
                    }

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