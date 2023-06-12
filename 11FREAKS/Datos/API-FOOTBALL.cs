using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Collections;
using _11FREAKS.Presentacion;
using System.IO;
using Newtonsoft.Json;
using System.Windows.Controls;
using MySqlX.XDevAPI;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Collections.ObjectModel;

namespace _11FREAKS.Datos
{
    /// <summary>
    /// Clase para la gestión de peticiones a la API
    /// </summary>
    internal class API_FOOTBALL
    {

        private Datos.BDOnline bdOnline;
        private Principal principal;

        /// <summary>
        /// Contructor Clase API por defecto (vacío) 
        /// </summary>
        public API_FOOTBALL()
        {
            bdOnline = new Datos.BDOnline();
        }

        /// <summary>
        /// Contructor Clase API para Ventana Principal
        /// </summary>
        /// <param name="ppal"/>
        public API_FOOTBALL(Principal ppal)
        {
            bdOnline = new Datos.BDOnline();
            principal = ppal;
        }



        /// <summary>
        ///     Método para cargar jugadores desde API
        /// </summary>
        /// <returns>
        ///     Devuelve string con los datos de cada jugador
        ///     <see cref="string"/>
        /// </returns>
        internal async Task<string> JsonJugadores()
        {
            string respuesta;
            JsonDocument jsonBingMaps = null;
            ArrayList fotosJugadores = new ArrayList();
            ArrayList resultados = new ArrayList();
            string resultadoJugador = String.Empty;
            int currentPage = 1;
            int totalPaginas = 500;       //PONEMOS NÚMERO GRANDE COMO PRUEBA
            int contJugadores = 0;

            try
            {
                while (currentPage <= totalPaginas)
                {

                    var client = new HttpClient();
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri("https://api-football-v1.p.rapidapi.com/v3/players?league=140&season=2022&page=" + currentPage),
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



                    //GUARDAMOS JSON 
                    try
                    {
                        string json = jsonBingMaps.RootElement.GetRawText();      //OBTENEMOS NUMERO JUGADORES EN PLANTILLA
                        File.WriteAllText("C:\\Users\\mdnon\\Desktop\\2DAM\\DI\\EjerDI\\11FREAKS\\11FREAKS\\Resources\\Json-Jugadores\\Jugadores.json", json);
                        var jsonEscriton = AutoClosingMessageBox.Show(
                        text: "JSON ESCRITO CON ÉXITO",
                        caption: "EQUIPO DE 11FREAKS",
                        timeout: 5000,
                        buttons: MessageBoxButtons.OK);
                    }
                    catch (Exception ex)
                    {
                        var jsonEscriton = AutoClosingMessageBox.Show(
                        text: "NO SE PUDO ESCRIBIR JSON",
                        caption: "EQUIPO DE 11FREAKS",
                        timeout: 5000,
                        buttons: MessageBoxButtons.OK);
                    }

                    //GESTIÓN PÁGINAS
                    //totalPaginas =Int32.Parse(jsonBingMaps.RootElement.GetProperty("paging").GetProperty("total").ToString());


                    //currentPage = Int32.Parse(jsonBingMaps.RootElement.GetProperty("paging").GetProperty("current").ToString());
                    int numJugadores = jsonBingMaps.RootElement.GetProperty("response").GetArrayLength();      //OBTENEMOS NUMERO JUGADORES EN PLANTILLA

                    for (int i = 0; i < numJugadores; i++)                //OBTENEMOS LOS JUGADORES
                    {
                        int idJugador = Convert.ToInt32(jsonBingMaps.RootElement.GetProperty("response")[i].GetProperty("player").GetProperty("id").ToString());
                        string nomJugador = jsonBingMaps.RootElement.GetProperty("response")[i].GetProperty("player").GetProperty("name").ToString();
                        string idEquipo = jsonBingMaps.RootElement.GetProperty("response")[i].GetProperty("statistics")[0].GetProperty("team").GetProperty("id").ToString();
                        string escudo = jsonBingMaps.RootElement.GetProperty("response")[i].GetProperty("statistics")[0].GetProperty("team").GetProperty("logo").ToString();
                        int edad = Convert.ToInt32(jsonBingMaps.RootElement.GetProperty("response")[i].GetProperty("player").GetProperty("age").ToString());
                        string foto = jsonBingMaps.RootElement.GetProperty("response")[i].GetProperty("player").GetProperty("photo").ToString();
                        string convocable = jsonBingMaps.RootElement.GetProperty("response")[i].GetProperty("player").GetProperty("injured").ToString();
                        string dorsal = jsonBingMaps.RootElement.GetProperty("response")[i].GetProperty("statistics")[0].GetProperty("games").GetProperty("number").ToString();
                        if (dorsal.Equals("null") || dorsal == null)
                        {
                            dorsal = "0";
                        }
                        string nacionalidad = jsonBingMaps.RootElement.GetProperty("response")[i].GetProperty("player").GetProperty("nationality").ToString();
                        string posicion = jsonBingMaps.RootElement.GetProperty("response")[i].GetProperty("statistics")[0].GetProperty("games").GetProperty("position").ToString();


                        resultadoJugador = idJugador + "\t" + nomJugador + "\t" + idEquipo + "\t" + edad + "\t" + dorsal + "\t" + nacionalidad + "\t" + posicion;

                        bdOnline.InsertarJugador(idJugador, nomJugador, idEquipo, escudo, edad, foto, convocable, dorsal, nacionalidad, posicion); //HARÍAMOS INSERT JUGADOR

                        var datosJugador = AutoClosingMessageBox.Show(
                        text: resultadoJugador,
                        caption: "EQUIPO DE 11FREAKS",
                        timeout: 5000,
                        buttons: MessageBoxButtons.OK);



                        contJugadores = contJugadores + 1;
                    }//FIN PÁGINA-->LECTURA DE TODOS LOS RESPONSE DEL ARRAY
                    currentPage = currentPage + 1;
                    var mensajeTemporal2 = AutoClosingMessageBox.Show(
                    text: "PÁGINAS TOTALES |" + totalPaginas + "\nJUGADORES TOTALES |" + contJugadores + "\nPÁGINA ACTUAL | " + currentPage,
                    caption: "EQUIPO DE 11FREAKS",
                    timeout: 5000,
                    buttons: MessageBoxButtons.OK);

                }//FIN PÁGINAS



            }
            catch (Exception ex)
            {
                var mensajeTemporal2 = AutoClosingMessageBox.Show(
                text: "Whoops! Parece que estamos teniendo problemas con la API" + ex.Message,
                caption: "EQUIPO DE 11FREAKS",
                timeout: 5000,
                buttons: MessageBoxButtons.OK);
            }
            return resultadoJugador;
        }










        /// <summary>
        ///     Método para cargar máximos goleadores de LaLiga
        /// </summary>
        internal async void MostrarClasificacion()
        {
            string respuesta;
            JsonDocument jsonBingMaps;

            try
            {
                principal.miListBox.Visibility = Visibility.Visible;
                principal.miListBox.Items.Clear();
                principal.indicadorLbox.Visibility = Visibility.Visible;
                principal.indicadorLbox.Content = "CLASIFICACIÓN EQUIPOS";

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

                    principal.miListBox.Items.Add(resultadoEquipo);                                                                              //Añadimos los Datos del Equipo
                }
                principal.CambiarHeaderTabla(1);
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
        ///     Método para cargar clasificación de LaLiga
        /// </summary>
        internal async void MaximosGoleadores()
        {

            string respuesta;
            JsonDocument jsonBingMaps;

            try
            {
                principal.miListBox.Visibility = Visibility.Visible;
                principal.miListBox.Items.Clear();
                principal.indicadorLbox.Visibility = Visibility.Visible;
                principal.indicadorLbox.Content = "MÁXIMOS GOLEADORES";

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
                    resultadoGoleador = (i + 1) + "|\t" + nomJugador + "   " + equipo + "   GOLES " + goles + "   PJ " + jugados;

                    principal.miListBox.Items.Add(resultadoGoleador);                                                                              //Añadimos los Datos del Goleador
                }
                principal.CambiarHeaderTabla(2);
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
        ///     Método para cargar clasificación de LaLiga
        /// </summary>
        internal async void MaximosAsistentes()
        {

            string respuesta;
            JsonDocument jsonBingMaps;

            try
            {
                principal.miListBox.Visibility = Visibility.Visible;
                principal.miListBox.Items.Clear();
                principal.indicadorLbox.Visibility = Visibility.Visible;
                principal.indicadorLbox.Content = "MÁXIMOS ASISTENTES";

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

                    principal.miListBox.Items.Add(resultadoAsistente);                                                                              //Añadimos los Datos del Asistente
                }

                principal.CambiarHeaderTabla(3);
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
        ///     Método para cargar jugadores más expulsados de LaLiga
        /// </summary>
        internal async void MaximosExpulsados()
        {

            string respuesta;
            JsonDocument jsonBingMaps;

            try
            {
                principal.miListBox.Visibility = Visibility.Visible;
                principal.miListBox.Items.Clear();
                principal.indicadorLbox.Visibility = Visibility.Visible;
                principal.indicadorLbox.Content = "JUGADORES MÁS EXPULSADOS";

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

                    principal.miListBox.Items.Add(resultadoExpulsado);                                                                              //Añadimos los Datos del Expulsado
                }

                principal.CambiarHeaderTabla(4);



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
        ///     Método para cargar jugadores que más tarjetas amarillas han recibido de LaLiga
        /// </summary>
        internal async void MaximosAmarillas()
        {
            string respuesta;
            JsonDocument jsonBingMaps;

            try
            {
                principal.tablaOjeadores.Visibility = Visibility.Visible;
                principal.tablaOjeadores.Items.Clear();
                principal.indicadorLbox.Visibility = Visibility.Visible;
                principal.indicadorLbox.Content = "JUGADORES CON MÁS AMARILLAS";

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

                List<JugadorAux> jugadores = new List<JugadorAux>();

                for (int i = 0; i < 10; i++)
                {
                    string resultadoAmonestado = string.Empty;
                    string nomJugador = jsonBingMaps.RootElement.GetProperty("response")[i].GetProperty("player").GetProperty("name").ToString();
                    string equipo = jsonBingMaps.RootElement.GetProperty("response")[i].GetProperty("statistics")[0].GetProperty("team").GetProperty("name").ToString();
                    string amarillas = jsonBingMaps.RootElement.GetProperty("response")[i].GetProperty("statistics")[0].GetProperty("cards").GetProperty("yellow").ToString();
                    string jugados = jsonBingMaps.RootElement.GetProperty("response")[i].GetProperty("statistics")[0].GetProperty("games").GetProperty("appearences").ToString();

                    JugadorAux jugador = new JugadorAux(nomJugador, equipo, int.Parse(amarillas), int.Parse(jugados));

                    jugadores.Add(jugador);
                    resultadoAmonestado = (i + 1) + "|\t" + nomJugador + "   " + equipo + "   AMARILLAS " + amarillas + "   PJ " + jugados;
                    principal.miListBox.Items.Add(resultadoAmonestado);
                }

                principal.tablaOjeadores.ItemsSource = jugadores;
                principal.CambiarHeaderTabla(5);
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














    }
}
