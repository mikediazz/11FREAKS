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

namespace _11FREAKS.Datos
{
    internal class API_FOOTBALL
    {

        private Datos.BDOnline bdOnline;
        

        public API_FOOTBALL()
        {
            bdOnline = new Datos.BDOnline();
        }


        internal async Task<string> JsonJugadores()
        {

            string respuesta;
            JsonDocument jsonBingMaps=null;
            ArrayList fotosJugadores= new ArrayList();
            ArrayList resultados = new ArrayList();
            string resultadoJugador=String.Empty;
            int currentPage =2;
            int totalPaginas=500;       //PONEMOS NÚMERO GRANDE COMO PRUEBA
            int contJugadores=0;

            try
            {
                while (currentPage <= totalPaginas)
                {

                    var client = new HttpClient();
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri("https://api-football-v1.p.rapidapi.com/v3/players?league=140&season=2022&page="+currentPage),
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
                    if (dorsal.Equals("null") || dorsal==null){
                        dorsal = "0";
                    }
                    string nacionalidad = jsonBingMaps.RootElement.GetProperty("response")[i].GetProperty("player").GetProperty("nationality").ToString();
                    string posicion = jsonBingMaps.RootElement.GetProperty("response")[i].GetProperty("statistics")[0].GetProperty("games").GetProperty("position").ToString();


                    resultadoJugador = idJugador + "\t" + nomJugador + "\t" + idEquipo + "\t" + edad + "\t" + dorsal + "\t" + nacionalidad + "\t" +posicion;

                        bdOnline.InsertarJugador(idJugador,nomJugador, idEquipo, escudo,edad,foto,convocable,dorsal,nacionalidad,posicion); //HARÍAMOS INSERT JUGADOR

                        var datosJugador = AutoClosingMessageBox.Show(
                        text: resultadoJugador,
                        caption: "EQUIPO DE 11FREAKS",
                        timeout: 5000,
                        buttons: MessageBoxButtons.OK);

                       

                    contJugadores = contJugadores+1;
                }//FIN PÁGINA-->LECTURA DE TODOS LOS RESPONSE DEL ARRAY
                        currentPage = currentPage + 1;
                                var mensajeTemporal2 = AutoClosingMessageBox.Show(
                                text: "PÁGINAS TOTALES |" + totalPaginas + "\nJUGADORES TOTALES |" +contJugadores+ "\nPÁGINA ACTUAL | "+currentPage,
                                caption: "EQUIPO DE 11FREAKS",
                                timeout: 5000,
                                buttons: MessageBoxButtons.OK);
                        
            }//FIN PÁGINAS



            }catch (Exception ex)
            {
                var mensajeTemporal2 = AutoClosingMessageBox.Show(
                text: "Whoops! Parece que estamos teniendo problemas con la API" + ex.Message,
                caption: "EQUIPO DE 11FREAKS",
                timeout: 5000,
                buttons: MessageBoxButtons.OK);
            }
            return resultadoJugador;
        }


        





        public void CargarJugadores(string filePath)
        {


            try
            {
                //string json = File.ReadAllText(filePath);
                //var jsonObject = JsonSerializer.Deserialize<JsonElement>(json);

                // Acceder a los valores del JSON
                //string nombre = jsonObject.GetProperty("response").GetArrayLength().ToString();
                //
                //int numJugadores = jsonBingMaps.RootElement.GetProperty("response").GetArrayLength();      //OBTENEMOS NUMERO JUGADORES EN PLANTILLA






                //string filePath = "ruta/al/archivo.json"; // Reemplaza "ruta/al/archivo.json" con la ruta real de tu archivo JSON

                string json = File.ReadAllText(filePath);

                dynamic jsonObject = JsonConvert.DeserializeObject(json);
                dynamic response = jsonObject.response;

                foreach (var playerData in response)
                {
                    string playerName = playerData.ToString();
                    var picas = AutoClosingMessageBox.Show(
                    text: "NOMBRE|||| " + playerName,
                    caption: "EQUIPO DE 11FREAKS",
                    timeout: 5000,
                    buttons: MessageBoxButtons.OK);

                }








            }
            catch (Exception ex)
            {
                var pica = AutoClosingMessageBox.Show(
                text: "RAW|||| " + ex.Message,
                caption: "EQUIPO DE 11FREAKS",
                timeout: 5000,
                buttons: MessageBoxButtons.OK);
            }
        }
    }
}
