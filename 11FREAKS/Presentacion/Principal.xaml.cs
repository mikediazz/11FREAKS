using _11FREAKS.Datos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.IO;
using System.Data;
using System.Net.Http;
using System.Text.Json;
using System.ComponentModel.Design.Serialization;

namespace _11FREAKS.Presentacion
{
    /// <summary>
    /// Clase Interacción con Principal.xaml || Ventana Principal del Proyecto
    /// </summary>
    public partial class Principal : Window
    {
        private Datos.BaseDatos miBaseDatos;
        private Datos.BDOnline bdServer;
        private Datos.API_FOOTBALL api;
        Inicio inicio;
        string usuario;
        public bool Respuesta {get;set;}


        public Principal(Inicio winInicio, BDOnline bd, string user)                            //CONSTRUCTOR
        {
            InitializeComponent();
            inicio= winInicio;
            bdServer= bd;
            usuario = user;
            api=new API_FOOTBALL(this);

            if (bdServer.CompruebaPermisos())                        //EN CASO QUE SEA ADMIN, SE HABILITAN OPCIONES EXCLUSIVAS
            {
                menuOpcAdmin.IsEnabled= true;
                menuOpcAdmin.Visibility = Visibility.Visible;
            }


            bdServer.ActualizacionClasificacion();                                            //ORDENAMOS LISTA EQUIPOS
            equiposLigaListBox.ItemsSource = bdServer.DevuelveEquiposLiga("1");               //CARGAMOS EQUIPOS DE LA LIGA


        }

        /// <summary>
        ///     Función Retorna Ventana Incio
        /// </summary>
        public Inicio DevuelveIncio()
        {
            return inicio;
        }


        private void Window_Closed(object sender, EventArgs e)
        {
            inicio.Close();
        }

        private void btnPartido_Click(object sender, RoutedEventArgs e)
        {}

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {}


        private void menuAyuda_Click(object sender, RoutedEventArgs e)
        {
            /////////////////   MOSTRAR PDF AYUDA   /////////////////
            string rutaAyuda = @"..\..\..\Resources\Ayuda11Freaks.pdf";
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo();     //CREAMOS PROCESO
                processStartInfo.FileName = rutaAyuda;
                processStartInfo.UseShellExecute = true;
                Process.Start(processStartInfo);                                //LANZAMOS PROCESO SHELL
            }catch(Exception ex)
            {
                var mensajeTemporal2 = AutoClosingMessageBox.Show(
                text: "Whoops! Parece que estamos para acceder al recurso\nPuedes acceder al archivo de AYUDA accediendo al directorio 'Resources'" +ex.Message,
                caption: "EQUIPO DE 11FREAKS",
                timeout: 5000,
                buttons: MessageBoxButtons.OK);
            }
        }



        /// <summary>
        ///     Función Vinculada al Botón Consultar Usuario
        /// </summary>
        private void menuConsultarUsuarios_Click(object sender, RoutedEventArgs e)
        {
            expanderUsers.Visibility = Visibility.Visible;
        }


        /// <summary>
        ///     Función Vinculada al Botón Añadir Admin
        /// </summary>
        private void menuAñadirAdmin_Click(object sender, RoutedEventArgs e)
        {
            GestionUsuarios gesUsers = new GestionUsuarios(this,bdServer,1);     //CASO DE USO PARA AÑADIR ADMIN
            this.Hide();
            gesUsers.ShowDialog();
        }


        /// <summary>
        ///     Función Vinculada al Botón Cerrar Sesión
        /// </summary>
        private void menuCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            inicio.Visibility= Visibility.Visible;
            inicio.Activate();
            inicio.txtUsuario.Text = string.Empty;
            inicio.txtPassword.Password = string.Empty;
            this.Hide();
        }


        /// <summary>
        ///     Función Vinculada al Botón Ver Clasificación
        /// </summary>
        private async void menuVerClasificacion_Click(object sender, RoutedEventArgs e)
        {
            CambiarVisibilidadLB();
            api.MostrarClasificacion();
        }

        /// <summary>
        ///     Función Vinculada al Botón Máximos Goleadores
        /// </summary>
        private async void menuMaximosGoleadores_Click(object sender, RoutedEventArgs e)
        {
            CambiarVisibilidadLB();
            api.MaximosGoleadores();
        }



        /// <summary>
        ///     Función Vinculada al Botón Máximos Asistentes
        /// </summary>
        private async void menuMaximosAsistentes_Click(object sender, RoutedEventArgs e)
        {
            CambiarVisibilidadLB();
            api.MaximosAsistentes();
        }

        /// <summary>
        ///     Función Vinculada al Botón Máximos Expulsados
        /// </summary>
        private async void menuMaximosRojas_Click(object sender, RoutedEventArgs e)
        {
            CambiarVisibilidadLB();
            api.MaximosExpulsados();
        }

        /// <summary>
        ///     Función Vinculada al Botón Top Amarillas
        /// </summary>
        private async void menuMaximosAmarillas_Click(object sender, RoutedEventArgs e)
        {
            CambiarVisibilidadLB();
            api.MaximosAmarillas();
        }

        private void miListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var mensajeTemporal = AutoClosingMessageBox.Show(
            text: "INDICE SELECCIONADO " + miListBox.SelectedIndex,
            caption: "EQUIPO DE 11FREAKS",
            timeout: 2500,
            buttons: MessageBoxButtons.OK);

            string idEquipo = miBaseDatos.BuscarEquipo(miListBox.Items[miListBox.SelectedIndex].ToString());

            var mensajeTemporal2 = AutoClosingMessageBox.Show(
            text: miListBox.Items[miListBox.SelectedIndex].ToString(),
            caption: "EQUIPO DE 11FREAKS",
            timeout: 2500,
            buttons: MessageBoxButtons.YesNo);

        }

        /// <summary>
        ///     Función Oculta Vistas Clasificación
        /// </summary>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            miListBox.Visibility = Visibility.Hidden;
            indicadorLbox.Visibility = Visibility.Hidden;
        }

        /// <summary>
        ///     Función Nos Envía a ventana MiEquipo
        /// </summary>
        private void menuVerEquipo_Click(object sender, RoutedEventArgs e)
        {
            MiEquipo miEquipo = new MiEquipo(this, bdServer);
            miEquipo.Show();
            this.Hide();
        }


        /// <summary>
        ///     Función Vinculada al Botón Salir
        /// </summary>
        private void menuSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            inicio.Close();
        }


        private void menuOcultarLogo_Click(object sender, RoutedEventArgs e)
        {
            logo.Visibility = Visibility.Hidden;
        }


        private async void menuCambiarMiEquipo_Click(object sender, RoutedEventArgs e)
        {
        }


        public void CtrShortcut1(Object sender, ExecutedRoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            var mensajeTemporal = AutoClosingMessageBox.Show(
            text: "SHORTCUT LANZADO",
            caption: "EQUIPO DE 11FREAKS",
            timeout: 3000,
            buttons: MessageBoxButtons.OK);
        }



        public void F1Shortcut(Object sender, ExecutedRoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private void menuCambiarUsuario_Click(object sender, RoutedEventArgs e)
        {
            GestionUsuarios gest = new GestionUsuarios(this, bdServer, 2);
            gest.ShowDialog();
        }

        private void menuCambiarContraseña_Click(object sender, RoutedEventArgs e)
        {
            GestionUsuarios gest = new GestionUsuarios(this, bdServer, 3);
            gest.ShowDialog();
        }

        private void menuBorrarCuenta_Click(object sender, RoutedEventArgs e)
        {
            Confirmacion confirmacion = new Confirmacion(this);
            confirmacion.ShowDialog();
            if (confirmacion.DevuelveRespuesta() == true)
            {
                miBaseDatos.BorrarUsuario(usuario);     //BORRAMOS CUENTA TRAS LA CONFIRMACIÓN
                var mensajeTemporal = AutoClosingMessageBox.Show(
                text: "SU CUENTA HA SIDO ELIMINADA CON ÉXITO",
                caption: "EQUIPO DE 11FREAKS",
                timeout: 3000,
                buttons: MessageBoxButtons.OK);

                Correo correo = new Correo();
                correo.CorreoCuentaEliminada(miBaseDatos.DevuelveCorreo());     //ENVIAMOS CORREO INFORMATIVO AL USUARIO

                inicio.Visibility = Visibility.Visible;                         //CERRAMOS SESIÓN
                inicio.Activate();
                inicio.txtUsuario.Text = string.Empty;
                inicio.txtPassword.Password = string.Empty;
                this.Hide();
            }
             
        }


        private void menuCambiarCorreo_Click(object sender, RoutedEventArgs e)
        {
            GestionUsuarios gest = new GestionUsuarios(this, bdServer, 4);
            gest.ShowDialog();
        }

        private void menuAjustarVolumen_Click(object sender, RoutedEventArgs e)
        {

        }

        private void menuMiClasificacion_Click(object sender, RoutedEventArgs e)
        {
            VistaClasificacion();
        }

        private void equiposLigaListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void menuJugarPartido_Click(object sender, RoutedEventArgs e)
        {
            Partido partido = new Partido(this, bdServer);
            partido.ShowDialog();

        }

        private void menuDocumentacion_Click(object sender, RoutedEventArgs e)
        {
            /////////////////   MOSTRAR PDF AYUDA   /////////////////
            string rutaAyuda = @"..\..\..\..\Documentation\Help\index.html";
            string absolutePath = @"C:\Users\mdnon\Desktop\2DAM\DI\EjerDI\11FREAKS\Documentation\Help\index.html";
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo();     //CREAMOS PROCESO
                processStartInfo.FileName = absolutePath;
                processStartInfo.UseShellExecute = true;
                Process.Start(processStartInfo);                                //LANZAMOS PROCESO SHELL
            }
            catch (Exception ex)
            {
                var mensajeTemporal2 = AutoClosingMessageBox.Show(
                text: "Whoops! Parece que estamos para acceder al recurso\nPuedes acceder al archivo de DOCUMENTACIÓN accediendo al directorio 'Documentation'" + ex.Message,
                caption: "EQUIPO DE 11FREAKS",
                timeout: 5000,
                buttons: MessageBoxButtons.OK);
            }

        }

        private void menuMercadoTraspasos_Click(object sender, RoutedEventArgs e)
        {
            jugadoresListBox.ItemsSource = bdServer.DevuelveTransferibles(20,"1");               //CARGAMOS JUGADORES DEL MERCADO
            VistaMercadoFichajes();
        }

        private void jugadoresListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Jugador jugadorSeleccionado = (Jugador)jugadoresListBox.SelectedItem;

            if (jugadorSeleccionado != null)
            {
                MostrarOpcionDeCompra(jugadorSeleccionado);  // Mostrar la opción de compra al usuario
            }
        }

        private void MostrarOpcionDeCompra(Jugador jugador)
        {
            MessageBoxResult result = System.Windows.MessageBox.Show($"¿Deseas comprar a {jugador.Nombre} por {jugador.Valor}€?", "FICHAJE", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)         //CONFIRMACIÓN DEL USUARIO
            {
                bdServer.Traspaso(jugador.Valor, jugador.idEquipo, bdServer.DevuelveIdEquipo(), jugador.idJugador);
            }


        }



        public void CambiarVisibilidadLB()
        {
            if (equiposLigaListBox.Visibility==Visibility.Visible)
            {
                jugadoresListBox.Visibility = Visibility.Collapsed;
                equiposLigaListBox.Visibility = Visibility.Collapsed;
            }
            else
            {
                jugadoresListBox.Visibility = Visibility.Visible;
                equiposLigaListBox.Visibility = Visibility.Visible;
            }
        }



        public void CambiarHeaderTabla(int opcion)
        {
            var columnaAmarillas = tablaOjeadores.Columns[2] as DataGridTextColumn;         //CAMBIO ENCABEZADO COLUMNA

            switch (opcion)
            {
                case 1:

                    break; 

                case 2:
                    columnaAmarillas.Header = "GOLES";
                    break;

                case 3:
                    columnaAmarillas.Header = "ASISTENCIAS";
                    break;

                case 4:
                    columnaAmarillas.Header = "ROJAS";
                    break;

                case 5:
                    columnaAmarillas.Header = "AMARILLAS";
                    break;

            }
            

        }



        public void VistaMercadoFichajes()
        {
            jugadoresListBox.Visibility=Visibility.Visible;
            equiposLigaListBox.Visibility = Visibility.Collapsed;

        }


        public void VistaClasificacion()
        {
            jugadoresListBox.Visibility = Visibility.Collapsed;
            equiposLigaListBox.Visibility = Visibility.Visible;

        }



    }

}
