using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading;
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
        private Datos.BDOnline bdServer;


        public Inicio()
        {
            InitializeComponent();
            miBaseDatos = new Datos.BaseDatos();        //INSTANCIAMOS BASE DATOS LOCAL
            bdServer = new Datos.BDOnline();            //INSTANCIAMOS BASE DATOS SERVER
        }


        /// <summary>
        ///     Función  Vinculada al Botón Iniciar Sesión --> Nos Envía a la Ventana "PRINCIPAL"
        /// </summary>
        private void btnInicioSesion_Click(object sender, RoutedEventArgs e)
        {
            /*
            if (miBaseDatos.Conectar(txtUsuario.Text, txtPassword.Password) == true)
            {
                Principal principal = new Principal(this, miBaseDatos, txtUsuario.Text);
                this.Hide();
                principal.ShowDialog();
            }
            */

            if (bdServer.ConectarServer(txtUsuario.Text, txtPassword.Password) == true)
            {
                Principal principal = new Principal(this, bdServer, txtUsuario.Text);
                this.Hide();
                principal.ShowDialog();
            }
        }

        /// <summary>
        ///     Función Vinculada al Botón Registrarse --> Nos Envía a la Ventana "REGISTRO"
        /// </summary>

        private void btnRegistrarse_Click(object sender, RoutedEventArgs e)
        {
            //RegistroUsuario registro = new RegistroUsuario(this);
            RegistroUserServer registro = new RegistroUserServer(this);
            this.Hide();
            registro.ShowDialog();
        }

        /// <summary>
        ///     Función Para Dar la Bienvenida al Usuario
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Thread videoThread = new Thread(new ThreadStart(ReproducirIntro));          //CREAMOS HILO AL QUE LE PASAMOS EL MÉTODO PARA INICIAR INTRO                                                                        
            videoThread.Start();                                                        //LANZAMOS HILO
        }


        private void btnSkip_Click(object sender, RoutedEventArgs e)
        {
            Thread ocultarThread = new Thread(new ThreadStart(OcultarIntro));          //CREAMOS HILO AL QUE LE PASAMOS EL MÉTODO PARA INICIAR INTRO                                                                        
            ocultarThread.Start();
        }



        private void imgVolumen_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Thread volumenThread = new Thread(new ThreadStart(GestionarVolumen));          //CREAMOS HILO AL QUE LE PASAMOS EL MÉTODO PARA INICIAR INTRO                                                                        
            volumenThread.Start();                                                           
        }

        private void btnSkip_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void imgSkip_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OcultarIntro();
        }



        private void ReproducirIntro()
        {
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                video.Play();
                video.Volume = 0.5;
                imgSkip.Visibility = Visibility.Visible;
                imgVolumen.Visibility = Visibility.Visible;
                btnSkip.Visibility = Visibility.Visible;
                bgSolid.Visibility = Visibility.Visible;
            }));

        }



        private void OcultarIntro()
        {

            System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                video.Stop();                               //DETENEMOS VIDEO
                video.Volume = 0;
                video.Visibility = Visibility.Collapsed;      //OCULTAMOS REPRODUCTOR
                btnSkip.Visibility = Visibility.Collapsed;    //OCULTAMOS BOTÓN SALTAR
                imgSkip.Visibility=Visibility.Collapsed;        //OCULTAMOS ICONOS
                imgVolumen.Visibility=Visibility.Collapsed;
                lblMute.Visibility=Visibility.Collapsed;
                bgSolid.Visibility = Visibility.Collapsed;
            }));

        }


        private void GestionarVolumen()
        {
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                if (video.Volume == 0)
                {
                    video.Volume = 1;
                    lblMute.Visibility = Visibility.Collapsed;
                }
                else
                {
                    video.Volume = 0;
                    lblMute.Visibility = Visibility.Visible;
                }
            }));
        }

        private void lblMute_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Thread volumenThread = new Thread(new ThreadStart(GestionarVolumen));          //CREAMOS HILO AL QUE LE PASAMOS EL MÉTODO PARA INICIAR INTRO                                                                        
            volumenThread.Start();



        }



    }
}
