using _11FREAKS.Datos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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
    /// Lógica de interacción para MiEquipo.xaml
    /// </summary>
    public partial class MiEquipo : Window
    {




        private Datos.BaseDatos miBaseDatos;
        private Datos.BDOnline bdServer;
        Principal principal;

        private string idEquipo;
        private Equipo EquipoFav;
        ArrayList fotosJugadores;
        public MiEquipo(Principal p, Datos.BDOnline bd)
        {
            InitializeComponent();
            principal = p;
            //miBaseDatos = bd;
            bdServer = bd;

            //fotosJugadores = new ArrayList();

            jugadoresListBox.ItemsSource = bdServer.DevuelveJugadoresEquipo(bdServer.DevuelveIdEquipo());               //CARGAMOS JUGADORES DEL EQUIPO DEL JUGADOR



            /*   if (miBaseDatos.DevuelveEquipoFav() == null)
               {
                   idEquipo = miBaseDatos.DevuelveEquipoFav();
                   var mensajeTemporal = AutoClosingMessageBox.Show(
                   text: "Whoops! Parece que no tienes ningún equipo favorito ",
                   caption: "EQUIPO DE 11FREAKS",
                   timeout: 4000,
                   buttons: MessageBoxButtons.OK);
               }
               else
               {
                   idEquipo = miBaseDatos.DevuelveEquipoFav();
                   EquipoFav = miBaseDatos.MiInfoEquipo(idEquipo);       //OBTENEMOS OBJETO EQUIPO FAVORITO
                   try
                   {
                       var mensajeTemporal = AutoClosingMessageBox.Show(
                       text: "BIENVENIDO A LA PESTAÑA PERSONALIZADA DEL " + EquipoFav.Nombre,
                       caption: "EQUIPO DE 11FREAKS",
                       timeout: 2000,
                       buttons: MessageBoxButtons.OK);
                       escudoEquipo.Source = new BitmapImage(new Uri(EquipoFav.Logo));     //MOSTRAMOS ESCUDO DEL EQUIPO
                   }
                   catch (Exception ex)
                   {
                       var mensajeTemporalError = AutoClosingMessageBox.Show(
                       text: "Whoops! Estamos teniendo dificultades " + ex.Message,
                       caption: "EQUIPO DE 11FREAKS",
                       timeout: 4000,
                       buttons: MessageBoxButtons.OK);
                   }


               }
            */


        }

        private void menuAyuda_Click(object sender, RoutedEventArgs e)
        {

        }

        private void menuAñadirAdmin_Click(object sender, RoutedEventArgs e)
        {

        }

        private void menuBorrarUsuario_Click(object sender, RoutedEventArgs e)
        {

        }

        private void menuConsultarUsuarios_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Deprecated
        /// </summary>
        private void menuInicio_Click(object sender, RoutedEventArgs e)
        {
            /*
            this.Hide();
            principal.Show();
            this.Close();*/
        }



        /// <summary>
        /// Método Para Volver al Inicio (Login)
        /// </summary>
        private void menuCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Inicio inicio = principal.DevuelveIncio();
            inicio.Activate();
            inicio.Visibility = Visibility.Visible; 
            inicio.txtUsuario.Text = string.Empty;
            inicio.txtPassword.Password = string.Empty;
            
        }

        /// <summary>
        /// Método Vinculado al Botón Plantilla
        /// </summary>
        private async void menuPlantilla_Click(object sender, RoutedEventArgs e)
        {
         /*   string respuesta;
            JsonDocument jsonBingMaps;

            try
            {
                miListBox.Visibility = Visibility.Visible;
                foto.Visibility = Visibility.Visible;
                miListBox.Items.Clear();
                racha.Visibility = Visibility.Hidden;
                formacion.Visibility = Visibility.Hidden;
                goles.Visibility = Visibility.Hidden;


                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://api-football-v1.p.rapidapi.com/v3/players/squads?team=" + EquipoFav.Id),
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

                int numJugadores = jsonBingMaps.RootElement.GetProperty("response")[0].GetProperty("players").GetArrayLength();      //OBTENEMOS NUMERO JUGADORES EN PLANTILLA
                fotosJugadores.Clear();

                for (int i = 0; i < numJugadores; i++)                //OBTENEMOS LOS JUGADORES
                {
                    string resultadoJugador = string.Empty;
                    string nomJugador = jsonBingMaps.RootElement.GetProperty("response")[0].GetProperty("players")[i].GetProperty("name").ToString();
                    string dorsal = jsonBingMaps.RootElement.GetProperty("response")[0].GetProperty("players")[i].GetProperty("number").ToString();
                    string posicion = jsonBingMaps.RootElement.GetProperty("response")[0].GetProperty("players")[i].GetProperty("position").ToString();
                    string edad = jsonBingMaps.RootElement.GetProperty("response")[0].GetProperty("players")[i].GetProperty("age").ToString();
                    string rutaFoto = jsonBingMaps.RootElement.GetProperty("response")[0].GetProperty("players")[i].GetProperty("photo").ToString();


                    fotosJugadores.Add(rutaFoto);           //ALMACENAMOS RUTA FOTO

                    //GUARDAMOS EN VARIABLE LO QUE QUEREMOS MOSTRAR POR CADA GOLEADOR
                    resultadoJugador = dorsal + "|\t" + nomJugador + "    POS->" + posicion + "    EDAD " + edad;

                    miListBox.Items.Add(resultadoJugador);
                    //Añadimos los Datos del Jugador
                }
            }

            catch (Exception ex)
            {
                var mensajeTemporal2 = AutoClosingMessageBox.Show(
                text: "Whoops! Parece que estamos teniendo problemas con la API" + ex.Message,
                caption: "EQUIPO DE 11FREAKS",
                timeout: 5000,
                buttons: MessageBoxButtons.OK);
            }*/
        }

        /// <summary>
        ///     Función Oculta Listbox
        /// </summary>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            miListBox.Visibility = Visibility.Hidden;
        }


        /// <summary>
        /// Método Controla el Indice de Lista en Plantilla para Mostrar su Foto
        /// </summary>
        private void miListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {            
                int indice = miListBox.SelectedIndex;
                if (indice != -1)
                {
                    foto.Source = new BitmapImage(new Uri(fotosJugadores[indice].ToString()));
                }
            }catch(Exception ex)
            {
                var mensajeTemporal2 = AutoClosingMessageBox.Show(
                text: "Whoops! Este jugador no debería ser muy fotogénico" + ex.Message,
                caption: "EQUIPO DE 11FREAKS",
                timeout: 5000,
                buttons: MessageBoxButtons.OK);
            }

        }

        /// <summary>
        /// Método Vinculado al Botón Estadiísticas Equipo
        /// </summary>
     /*   private async void menuEstadisticasEquipo_Click(object sender, RoutedEventArgs e)
        {
            string respuesta;
            JsonDocument jsonBingMaps;

            miListBox.Visibility = Visibility.Hidden;
            miListBox.Items.Clear();
            foto.Visibility = Visibility.Hidden;

            racha.Visibility = Visibility.Visible;
            formacion.Visibility = Visibility.Visible;
            goles.Visibility = Visibility.Visible;

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://api-football-v1.p.rapidapi.com/v3/teams/statistics?league=140&season=2022&team=" + EquipoFav.Id),
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



                string resultadoJugador = string.Empty;
                formacion.Content ="FORMACIÓN |   "+ jsonBingMaps.RootElement.GetProperty("response").GetProperty("lineups")[0].GetProperty("formation").ToString();
                racha.Content = "\t\t  RACHA\n" + jsonBingMaps.RootElement.GetProperty("response").GetProperty("form").ToString();
                goles.Content = "GOLES A FAVOR     | " + jsonBingMaps.RootElement.GetProperty("response").GetProperty("goals").GetProperty("for").GetProperty("total").GetProperty("total").ToString() + " " +
                            "  PROMEDIO " + jsonBingMaps.RootElement.GetProperty("response").GetProperty("goals").GetProperty("for").GetProperty("average").GetProperty("total").ToString() +
                            "\n\tLOCAL         | " + jsonBingMaps.RootElement.GetProperty("response").GetProperty("goals").GetProperty("for").GetProperty("total").GetProperty("home").ToString() +
                             "  PROMEDIO " + jsonBingMaps.RootElement.GetProperty("response").GetProperty("goals").GetProperty("for").GetProperty("average").GetProperty("home").ToString() +
                             "\n\tVISITANTE | " + jsonBingMaps.RootElement.GetProperty("response").GetProperty("goals").GetProperty("for").GetProperty("total").GetProperty("away").ToString() +
                             "  PROMEDIO " + jsonBingMaps.RootElement.GetProperty("response").GetProperty("goals").GetProperty("for").GetProperty("average").GetProperty("away").ToString() +

                            "\n\nGOLES EN CONTRA   | " + jsonBingMaps.RootElement.GetProperty("response").GetProperty("goals").GetProperty("against").GetProperty("total").GetProperty("total").ToString() + " " +
                            "  PROMEDIO " + jsonBingMaps.RootElement.GetProperty("response").GetProperty("goals").GetProperty("against").GetProperty("average").GetProperty("total").ToString() +
                            "\n\tLOCAL         | " + jsonBingMaps.RootElement.GetProperty("response").GetProperty("goals").GetProperty("against").GetProperty("total").GetProperty("home").ToString() +
                             "  PROMEDIO " + jsonBingMaps.RootElement.GetProperty("response").GetProperty("goals").GetProperty("against").GetProperty("average").GetProperty("home").ToString() +
                             "\n\tVISITANTE | " + jsonBingMaps.RootElement.GetProperty("response").GetProperty("goals").GetProperty("for").GetProperty("total").GetProperty("away").ToString() +
                             "  PROMEDIO " + jsonBingMaps.RootElement.GetProperty("response").GetProperty("goals").GetProperty("against").GetProperty("average").GetProperty("away").ToString();

        }*/


        /// <summary>
        /// Método Para Ocultar Vistas de Estadísticas de Equipo
        /// </summary>
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            formacion.Visibility = Visibility.Hidden;
            racha.Visibility = Visibility.Hidden;
            goles.Visibility = Visibility.Hidden;
        }


        /// <summary>
        /// Método Para Ocultar Vistas de Plantilla
        /// </summary>
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            miListBox.Visibility = Visibility.Hidden;
            foto.Visibility = Visibility.Hidden;
        }








        /// <summary>
        /// Método Vinculado al Botón Lesiones Reportadas
        /// </summary>
        private async void menuLesiones_Click(object sender, RoutedEventArgs e)
        {
        /*    string respuesta;
            JsonDocument jsonBingMaps;

            try
            {
                miListBox.Visibility = Visibility.Visible;
                foto.Visibility = Visibility.Visible;
                miListBox.Items.Clear();
                racha.Visibility = Visibility.Hidden;
                formacion.Visibility = Visibility.Hidden;
                goles.Visibility = Visibility.Hidden;


                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://api-football-v1.p.rapidapi.com/v3/injuries?league=140&season=2022&team=" + EquipoFav.Id),
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

                int numJugadores = jsonBingMaps.RootElement.GetProperty("response").GetArrayLength();      //OBTENEMOS DE LESIONES REPORTADAS
                fotosJugadores.Clear();

                for (int i = 0; i < numJugadores; i++)                //OBTENEMOS LOS JUGADORES
                {
                    string resultadoLesion = string.Empty;
                    string nomJugador = jsonBingMaps.RootElement.GetProperty("response")[i].GetProperty("player").GetProperty("name").ToString();
                    string foto = jsonBingMaps.RootElement.GetProperty("response")[i].GetProperty("player").GetProperty("photo").ToString();
                    string motivo = jsonBingMaps.RootElement.GetProperty("response")[i].GetProperty("player").GetProperty("reason").ToString();
                    string f = jsonBingMaps.RootElement.GetProperty("response")[i].GetProperty("fixture").GetProperty("date").ToString();
                    string fecha = f.Substring(0, 10);

                    
                    fotosJugadores.Add(foto);           //ALMACENAMOS RUTA FOTO

                    //GUARDAMOS EN VARIABLE LO QUE QUEREMOS MOSTRAR POR CADA REPORTE DE LESIÓN
                    resultadoLesion = nomJugador + "    MOTIVO->" + motivo + "    FECHA " + fecha;

                    miListBox.Items.Add(resultadoLesion);
                    //Añadimos los Datos del Jugador
                }
            }

            catch (Exception ex)
            {
                var mensajeTemporal2 = AutoClosingMessageBox.Show(
                text: "Whoops! Parece que estamos teniendo problemas con la API" + ex.Message,
                caption: "EQUIPO DE 11FREAKS",
                timeout: 5000,
                buttons: MessageBoxButtons.OK);
            }*/
        }








        private async void menuTraspasos_Click(object sender, RoutedEventArgs e)
        {
         /*   string respuesta;
            JsonDocument jsonBingMaps;

            try
            {
                miListBox.Visibility = Visibility.Visible;
                foto.Visibility = Visibility.Visible;
                miListBox.Items.Clear();
                racha.Visibility = Visibility.Hidden;
                formacion.Visibility = Visibility.Hidden;
                goles.Visibility = Visibility.Hidden;


                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://api-football-v1.p.rapidapi.com/v2/transfers/team/" + EquipoFav.Id),
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

                int numJugadores = jsonBingMaps.RootElement.GetProperty("api").GetProperty("transfers").GetArrayLength();      //OBTENEMOS DE LESIONES REPORTADAS
                fotosJugadores.Clear();

                for (int i = 0; i < numJugadores; i++)                //OBTENEMOS LOS JUGADORES
                {
                    string resultadoTraspaso = string.Empty;
                    string nomJugador = jsonBingMaps.RootElement.GetProperty("api").GetProperty("transfers")[i].GetProperty("player_name").ToString();
                    string precio = jsonBingMaps.RootElement.GetProperty("api").GetProperty("transfers")[i].GetProperty("type").ToString();
                    string origen = jsonBingMaps.RootElement.GetProperty("api").GetProperty("transfers")[i].GetProperty("team_in").GetProperty("team_name").ToString();
                    string destino = jsonBingMaps.RootElement.GetProperty("api").GetProperty("transfers")[i].GetProperty("team_out").GetProperty("team_name").ToString();
                    string f = jsonBingMaps.RootElement.GetProperty("api").GetProperty("transfers")[i].GetProperty("transfer_date").ToString();

                    try
                    {
                        if (f.Length < 10){
                            //DATOS ERRONEOS RECIBIDOS DE LA API
                        }
                        else
                        {
                            string fecha = f.Substring(0, 10);
                            string año=f.Substring(0, 4);
                            int valorAño = Int32.Parse(año);

                            if (valorAño ==2022)                              //ÚLTIMA VENTANA DE TRASPASOS RELEVANTE
                            {
                                //GUARDAMOS EN VARIABLE LO QUE QUEREMOS MOSTRAR POR CADA REPORTE DE LESIÓN
                                resultadoTraspaso = nomJugador + "\t\t" + origen + " ---> " + destino + "\t(" + precio + ")\t" + fecha;

                                miListBox.Items.Add(resultadoTraspaso);
                                //Añadimos los Datos del Jugador
                            }
                        }


                    }catch(Exception ex)
                    {
                        var mensajeTemporal3 = AutoClosingMessageBox.Show(
                        text: "Whoops! Inconsistencia en la API\t" + ex.Message,
                        caption: "EQUIPO DE 11FREAKS",
                        timeout: 5000,
                        buttons: MessageBoxButtons.OK);
                    }


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

        private void menuSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            principal.Close();
        }

        private void menuVolverPrincipal_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            principal.Visibility = Visibility.Visible;
            this.Close();
        }

        private void menuMiEquipo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void jugadoresListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Jugador jugadorSeleccionado = jugadoresListBox.SelectedItem as Jugador;

            if (jugadorSeleccionado != null)
            {
                int idJugador = jugadorSeleccionado.idJugador;
                string nombreJugador = jugadorSeleccionado.Nombre;

                var mensajeTemporal2 = AutoClosingMessageBox.Show(
                text: $"ID del Jugador: {idJugador}",
                caption: "EQUIPO DE 11FREAKS",
                timeout: 5000,
                buttons: MessageBoxButtons.OK);

                var mensajeTemporal3 = AutoClosingMessageBox.Show(
                text: $"Nombre del Jugador: {nombreJugador}",
                caption: "EQUIPO DE 11FREAKS",
                timeout: 5000,
                buttons: MessageBoxButtons.OK);

            }*/
        }

        private void menuVolverPrincipal_Click(object sender, RoutedEventArgs e)
        {

        }

        private void menuMiEquipo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void menuEstadisticasEquipo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void jugadoresListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void menuSalir_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
