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

namespace _11FREAKS.Datos
{
    internal class API_FOOTBALL
    {
        /*
        private async Task<string> VerPlantilla(object sender, RoutedEventArgs e)
        {

            string respuesta;
            JsonDocument jsonBingMaps;
            ArrayList fotosJugadores= new ArrayList();
            ArrayList resultados = new ArrayList();

            try
            { 

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
                //fotosJugadores.Clear();

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
                    
                    resultados.Add(resultadoJugador);
                    //Añadimos los Datos del Jugador

                    //return new Pair<ArrayList<String>, ArrayList<String>>(fotosJugadores, resultados);
                    return resultadoJugador;

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
        }*/
    }
}
