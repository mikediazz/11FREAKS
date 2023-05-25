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
            api=new API_FOOTBALL();

            if (bdServer.CompruebaPermisos())                        //EN CASO QUE SEA ADMIN, SE HABILITAN OPCIONES EXCLUSIVAS
            {
                menuOpcAdmin.IsEnabled= true;
                menuOpcAdmin.Visibility = Visibility.Visible;
            }

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
            /////////////////   PRUEBAS PARA MOSTRAR PDF AYUDA   /////////////////
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.FileName = "C:\\Users\\mdnon\\Desktop\\2DAM\\DI\\EjerDI\\11FREAKS\\11FREAKS\\Resources\\Ayuda11Freaks.pdf";
                processStartInfo.UseShellExecute = true;
                Process.Start(processStartInfo);
            }catch(Exception ex)
            {
                var mensajeTemporal2 = AutoClosingMessageBox.Show(
                text: "Whoops! Parece que estamos para acceder al recurso\nPuedes acceder al archivo de AYUDA accediendo al directorio 'Resources'",
                caption: "EQUIPO DE 11FREAKS",
                timeout: 5000,
                buttons: MessageBoxButtons.OK);
            }

        }




        /// <summary>
        ///     Función Vinculada al Botón Borrar Usuario
        /// </summary>
       /* private void menuBorrarUsuario_Click(object sender, RoutedEventArgs e)
        {
            if (miBaseDatos.CompruebaPermisos()==true) {
                expanderUsers.Visibility = Visibility.Visible;
               // miBaseDatos.BorrarUsuario("");      //PONER NOMBRE DEL USUARIO A BORRAR
            }
        }*/


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
            GestionUsuarios gesUsers = new GestionUsuarios(this,miBaseDatos,1);     //CASO DE USO PARA AÑADIR ADMIN
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

            string respuesta;
            JsonDocument jsonBingMaps;

            try
            {
                miListBox.Visibility=Visibility.Visible;
                miListBox.Items.Clear();
                indicadorLbox.Visibility=Visibility.Visible;
                indicadorLbox.Content = "CLASIFICACIÓN EQUIPOS";

                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://api-football-v1.p.rapidapi.com/v3/standings?season=2022&league=140"),
                    Headers =
                    {
                        { "X-RapidAPI-Key", "13407a5035mshae17fa61f3ee711p161488jsnc097d19e6099" },
                        { "X-RapidAPI-Host", "api-football-v1.p.rapidapi.com" },
                    },
                };

                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    jsonBingMaps = JsonDocument.Parse(body);
                }


                for (int i = 0; i < 20; i++)                //OBTENEMOS LOS 20 EQUIPOS EN ORDEN
                {
                    string resultadoEquipo = string.Empty;
                    string nomEquipo = jsonBingMaps.RootElement.GetProperty("response")[0].GetProperty("league").GetProperty("standings")[0][i].GetProperty("team").GetProperty("name").ToString();
                    string puntos = jsonBingMaps.RootElement.GetProperty("response")[0].GetProperty("league").GetProperty("standings")[0][i].GetProperty("points").ToString();
                    string racha = jsonBingMaps.RootElement.GetProperty("response")[0].GetProperty("league").GetProperty("standings")[0][i].GetProperty("form").ToString();
                    string jugados = jsonBingMaps.RootElement.GetProperty("response")[0].GetProperty("league").GetProperty("standings")[0][i].GetProperty("all").GetProperty("played").ToString();

                    string victorias = jsonBingMaps.RootElement.GetProperty("response")[0].GetProperty("league").GetProperty("standings")[0][i].GetProperty("all").GetProperty("win").ToString();
                    string empates = jsonBingMaps.RootElement.GetProperty("response")[0].GetProperty("league").GetProperty("standings")[0][i].GetProperty("all").GetProperty("draw").ToString();
                    string derrotas = jsonBingMaps.RootElement.GetProperty("response")[0].GetProperty("league").GetProperty("standings")[0][i].GetProperty("all").GetProperty("lose").ToString();
                    //GUARDAMOS EN VARIABLE LO QUE QUEREMOS MOSTRAR POR CADA EQUIPO
                    resultadoEquipo = (i + 1) + "| " + nomEquipo + "\t\tPTS " + puntos + "\t\tPJ " + jugados + "\t\tPG " + victorias + "\t\tPE " + empates + "\t\tPP " + derrotas + "\t\tRACHA " + racha;

                    miListBox.Items.Add(resultadoEquipo);                                                                              //Añadimos los Datos del Equipo
                }
            }

            catch (Exception ex)
            {
                var mensajeTemporal2 = AutoClosingMessageBox.Show(
                text: "Whoops! Parece que estamos teniendo problemas con la API" + ex.Message,
                caption: "EQUIPO DE 11FREAKS",
                timeout: 5000,
                buttons: MessageBoxButtons.OK);
            }

        }

        /// <summary>
        ///     Función Vinculada al Botón Máximos Goleadores
        /// </summary>
        private async void menuMaximosGoleadores_Click(object sender, RoutedEventArgs e)
        {

            string respuesta;
            JsonDocument jsonBingMaps;

            try
            {
                miListBox.Visibility= Visibility.Visible;
                miListBox.Items.Clear();
                indicadorLbox.Visibility = Visibility.Visible;
                indicadorLbox.Content = "MÁXIMOS GOLEADORES";

                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://api-football-v1.p.rapidapi.com/v3/players/topscorers?league=140&season=2022"),
                    Headers =
                    {
                        { "X-RapidAPI-Key", "13407a5035mshae17fa61f3ee711p161488jsnc097d19e6099" },
                        { "X-RapidAPI-Host", "api-football-v1.p.rapidapi.com" },
                    },
                };

                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    jsonBingMaps = JsonDocument.Parse(body);
                }


                for (int i = 0; i < 20; i++)                //OBTENEMOS LOS 20 GOLEADORES EN ORDEN
                {
                    string resultadoGoleador = string.Empty;
                    string nomJugador = jsonBingMaps.RootElement.GetProperty("response")[i].GetProperty("player").GetProperty("name").ToString();
                    string equipo = jsonBingMaps.RootElement.GetProperty("response")[i].GetProperty("statistics")[0].GetProperty("team").GetProperty("name").ToString();
                    string goles = jsonBingMaps.RootElement.GetProperty("response")[i].GetProperty("statistics")[0].GetProperty("goals").GetProperty("total").ToString();
                    string jugados = jsonBingMaps.RootElement.GetProperty("response")[i].GetProperty("statistics")[0].GetProperty("games").GetProperty("appearences").ToString(); ("played").ToString();
         
                    //GUARDAMOS EN VARIABLE LO QUE QUEREMOS MOSTRAR POR CADA GOLEADOR
                    resultadoGoleador = (i + 1) + "|\t" + nomJugador + "   " + equipo + "   GOLES " + goles + "   PJ " + jugados ;

                    miListBox.Items.Add(resultadoGoleador);                                                                              //Añadimos los Datos del Goleador
                }
            }

            catch (Exception ex)
            {
                var mensajeTemporal2 = AutoClosingMessageBox.Show(
                text: "Whoops! Parece que estamos teniendo problemas con la API" + ex.Message,
                caption: "EQUIPO DE 11FREAKS",
                timeout: 5000,
                buttons: MessageBoxButtons.OK);
            }
        }



        /// <summary>
        ///     Función Vinculada al Botón Máximos Asistentes
        /// </summary>
        private async void menuMaximosAsistentes_Click(object sender, RoutedEventArgs e)
        {
            string respuesta;
            JsonDocument jsonBingMaps;

            try
            { 
                miListBox.Visibility= Visibility.Visible;
                miListBox.Items.Clear();
                indicadorLbox.Visibility = Visibility.Visible;
                indicadorLbox.Content = "MÁXIMOS ASISTENTES";

                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://api-football-v1.p.rapidapi.com/v3/players/topassists?league=140&season=2022"),
                    Headers =
                    {
                        { "X-RapidAPI-Key", "13407a5035mshae17fa61f3ee711p161488jsnc097d19e6099" },
                        { "X-RapidAPI-Host", "api-football-v1.p.rapidapi.com" },
                    },
                };

                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    jsonBingMaps = JsonDocument.Parse(body);
                }


                for (int i = 0; i < 20; i++)                //OBTENEMOS LOS 20 ASISTENTES EN ORDEN
                {
                    string resultadoAsistente = string.Empty;
                    string nomJugador = jsonBingMaps.RootElement.GetProperty("response")[i].GetProperty("player").GetProperty("name").ToString();
                    string equipo = jsonBingMaps.RootElement.GetProperty("response")[i].GetProperty("statistics")[0].GetProperty("team").GetProperty("name").ToString();
                    string asistencias = jsonBingMaps.RootElement.GetProperty("response")[i].GetProperty("statistics")[0].GetProperty("goals").GetProperty("assists").ToString();
                    string jugados = jsonBingMaps.RootElement.GetProperty("response")[i].GetProperty("statistics")[0].GetProperty("games").GetProperty("appearences").ToString(); ("played").ToString();

                    //GUARDAMOS EN VARIABLE LO QUE QUEREMOS MOSTRAR POR CADA GOLEADOR
                    resultadoAsistente = (i + 1) + "|\t" + nomJugador + "   " + equipo + "   ASISTENCIAS " + asistencias + "   PJ " + jugados;

                    miListBox.Items.Add(resultadoAsistente);                                                                              //Añadimos los Datos del Asistente
                }
            }

            catch (Exception ex)
            {
                var mensajeTemporal = AutoClosingMessageBox.Show(
                text: "Whoops! Parece que estamos teniendo problemas con la API " + ex.Message,
                caption: "EQUIPO DE 11FREAKS",
                timeout: 5000,
                buttons: MessageBoxButtons.OK);
            }
        }

        /// <summary>
        ///     Función Vinculada al Botón Máximos Expulsados
        /// </summary>
        private async void menuMaximosRojas_Click(object sender, RoutedEventArgs e)
        {

            string respuesta;
            JsonDocument jsonBingMaps;

            try
            {
                miListBox.Visibility = Visibility.Visible;
                miListBox.Items.Clear();
                indicadorLbox.Visibility = Visibility.Visible;
                indicadorLbox.Content = "JUGADORES MÁS EXPULSADOS";

                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://api-football-v1.p.rapidapi.com/v3/players/topredcards?league=140&season=2022"),
                    Headers =
                    {
                        { "X-RapidAPI-Key", "13407a5035mshae17fa61f3ee711p161488jsnc097d19e6099" },
                        { "X-RapidAPI-Host", "api-football-v1.p.rapidapi.com" },
                    },
                };

                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    jsonBingMaps = JsonDocument.Parse(body);
                }


                for (int i = 0; i < 10; i++)                //OBTENEMOS LOS 10 EXPULSADOS EN ORDEN
                {
                    string resultadoExpulsado = string.Empty;
                    string nomJugador = jsonBingMaps.RootElement.GetProperty("response")[i].GetProperty("player").GetProperty("name").ToString();
                    string equipo = jsonBingMaps.RootElement.GetProperty("response")[i].GetProperty("statistics")[0].GetProperty("team").GetProperty("name").ToString();
                    string expulsiones = jsonBingMaps.RootElement.GetProperty("response")[i].GetProperty("statistics")[0].GetProperty("cards").GetProperty("red").ToString();
                    string jugados = jsonBingMaps.RootElement.GetProperty("response")[i].GetProperty("statistics")[0].GetProperty("games").GetProperty("appearences").ToString(); ("played").ToString();

                    //GUARDAMOS EN VARIABLE LO QUE QUEREMOS MOSTRAR POR CADA GOLEADOR
                    resultadoExpulsado = (i + 1) + "|\t" + nomJugador + "   " + equipo + "   EXPULSIONES " + expulsiones + "  PJ " + jugados;

                    miListBox.Items.Add(resultadoExpulsado);                                                                              //Añadimos los Datos del Expulsado
                }
            }

            catch (Exception ex)
            {
                var mensajeTemporal = AutoClosingMessageBox.Show(
                text: "Whoops! Parece que estamos teniendo problemas con la API " + ex.Message,
                caption: "EQUIPO DE 11FREAKS",
                timeout: 5000,
                buttons: MessageBoxButtons.OK);
            }
        }

        /// <summary>
        ///     Función Vinculada al Botón Top Amarillas
        /// </summary>
        private async void menuMaximosAmarillas_Click(object sender, RoutedEventArgs e)
        {

            string respuesta;
            JsonDocument jsonBingMaps;

            try
            {
                miListBox.Visibility = Visibility.Visible;
                miListBox.Items.Clear();
                indicadorLbox.Visibility = Visibility.Visible;
                indicadorLbox.Content = "JUGADORES CON MÁS AMARILLAS";

                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://api-football-v1.p.rapidapi.com/v3/players/topyellowcards?league=140&season=2022"),
                    Headers =
                    {
                        { "X-RapidAPI-Key", "13407a5035mshae17fa61f3ee711p161488jsnc097d19e6099" },
                        { "X-RapidAPI-Host", "api-football-v1.p.rapidapi.com" },
                    },
                };

                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    jsonBingMaps = JsonDocument.Parse(body);
                }


                for (int i = 0; i < 10; i++)                //OBTENEMOS LOS 10 AMONESTADOS EN ORDEN
                {
                    string resultadoAmonestado = string.Empty;
                    string nomJugador = jsonBingMaps.RootElement.GetProperty("response")[i].GetProperty("player").GetProperty("name").ToString();
                    string equipo = jsonBingMaps.RootElement.GetProperty("response")[i].GetProperty("statistics")[0].GetProperty("team").GetProperty("name").ToString();
                    string amarillas = jsonBingMaps.RootElement.GetProperty("response")[i].GetProperty("statistics")[0].GetProperty("cards").GetProperty("yellow").ToString();
                    string jugados = jsonBingMaps.RootElement.GetProperty("response")[i].GetProperty("statistics")[0].GetProperty("games").GetProperty("appearences").ToString(); ("played").ToString();

                    //GUARDAMOS EN VARIABLE LO QUE QUEREMOS MOSTRAR POR CADA AMONESTADO
                    resultadoAmonestado = (i + 1) + "|\t" + nomJugador + "   " + equipo + "   AMARILLAS " + amarillas + "   PJ " + jugados;

                    miListBox.Items.Add(resultadoAmonestado);   
                    //Añadimos los Datos del Amonestado
                }
            }

            catch (Exception ex)
            {
                var mensajeTemporal = AutoClosingMessageBox.Show(
                text: "Whoops! Parece que estamos teniendo problemas con la API " + ex.Message,
                caption: "EQUIPO DE 11FREAKS",
                timeout: 5000,
                buttons: MessageBoxButtons.OK);
            }
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
            MiEquipo miEquipo = new MiEquipo(this, miBaseDatos);
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

        private void menuMiEquipo_Click(object sender, RoutedEventArgs e)
        {

            /*            
            miListBox.Visibility = Visibility.Visible;
            miListBox.Items.Clear();
            foreach (string equipo in miBaseDatos.ConsultaEquipos())
            {
                miListBox.Items.Add(equipo);
            }

            miBaseDatos.CambiarEquipo(miBaseDatos.DevuelveEquipoFav(),miBaseDatos.DevuelveUsuario());*/
        }

        private void menuOcultarLogo_Click(object sender, RoutedEventArgs e)
        {
            logo.Visibility = Visibility.Hidden;
        }

        private async void menuCambiarMiEquipo_Click(object sender, RoutedEventArgs e)
        {
            var mensajeTemporal = AutoClosingMessageBox.Show(
            text: "ESTA FUNCIONALIDAD SE AÑADIRÁ PROXIMAMENTE",
            caption: "EQUIPO DE 11FREAKS",
            timeout: 3000,
            buttons: MessageBoxButtons.OK);

            miListBox.Items.Clear();                        //RESETEAMOS LISTBOX
            miListBox.Visibility = Visibility.Visible;      //HACEMOS LISTBOX VISIBLE


            string respuesta;
            JsonDocument jsonBingMaps;

            try
            {

                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://api-football-v1.p.rapidapi.com/v3/teams?league=140&season=2022"),
                    Headers =
                    {
                        { "X-RapidAPI-Key", "13407a5035mshae17fa61f3ee711p161488jsnc097d19e6099" },
                        { "X-RapidAPI-Host", "api-football-v1.p.rapidapi.com" },
                    },
                };

                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    jsonBingMaps = JsonDocument.Parse(body);
                }

                for (int i = 0; i < 20; i++)
                {
                    miListBox.Items.Add(jsonBingMaps.RootElement.GetProperty("response")[i].GetProperty("team").GetProperty("name"));  //Añadimos a la lista
                }

                var mensajeEquipos = AutoClosingMessageBox.Show(
                text: "EQUIPOS CARGADOS",
                caption: "EQUIPO DE 11FREAKS",
                timeout: 2500,
                buttons: MessageBoxButtons.OK);

            }
            catch (Exception ex)
            {

            }


            //miBaseDatos.CambiarEquipo(usuario, 541.ToString());
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
            GestionUsuarios gest = new GestionUsuarios(this,miBaseDatos, 2);
            gest.ShowDialog();
        }

        private void menuCambiarContraseña_Click(object sender, RoutedEventArgs e)
        {
            GestionUsuarios gest = new GestionUsuarios(this,miBaseDatos, 3);
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
            GestionUsuarios gest = new GestionUsuarios(this, miBaseDatos, 4);
            gest.ShowDialog();
        }

        private void menuAjustarVolumen_Click(object sender, RoutedEventArgs e)
        {

        }

        private void menuMiClasificacion_Click(object sender, RoutedEventArgs e)
        {
            //MÉTODO VER CLASIFICACIÓN MI LIGA
        }

        private void equiposLigaListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
