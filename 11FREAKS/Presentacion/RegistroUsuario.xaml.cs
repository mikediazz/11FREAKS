using _11FREAKS.Datos;
using Org.BouncyCastle.Asn1.Crmf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
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
    /// Lógica de interacción para RegistroUsuario.xaml
    /// </summary>
    public partial class RegistroUsuario : Window
    {
        private Datos.BaseDatos miBaseDatos;
        Inicio inicio;

        string idEquipo;

        public RegistroUsuario(Inicio pIncio)
        {
            InitializeComponent();
            this.inicio = pIncio;
            miBaseDatos=new Datos.BaseDatos();
        }

        private void btnRegistrarse_Click(object sender, RoutedEventArgs e)
        {
            miBaseDatos = new Datos.BaseDatos();
            if (miBaseDatos.CompruebaPassword(txtUsuario.Text, txtPassword.Password))
            {
                if (miBaseDatos.Conectar(txtUsuario.Text, txtPassword.Password) == true)
                {
                    var mensajeTemporal = AutoClosingMessageBox.Show(
                    text: "Whoops! YA EXISTE ESTE USUARIO",
                    caption: "EQUIPO DE 11FREAKS",
                    timeout: 3000,
                    buttons: MessageBoxButtons.OK);
                }
                else
                {
                    var mensajeTemporal = AutoClosingMessageBox.Show(
                    text: "**USUARIO CREADO**    ID EQUIPO"+idEquipo,
                    caption: "EQUIPO DE 11FREAKS",
                    timeout: 1500,
                    buttons: MessageBoxButtons.OK);
                    miBaseDatos.CrearUsuario(txtUsuario.Text, txtPassword.Password, idEquipo);          //CREAMOS USUARIO


                    Principal principal = new Principal(inicio, miBaseDatos);
                    this.Hide();
                    principal.ShowDialog();
                }

            }
            else{                                                               //CONTRASEÑA NO VÁLIDA
                var mensajeTemporal = AutoClosingMessageBox.Show(
                text: "Whoops! LA CONTRASEÑA NO CUMPLE LOS REQUISITOS",
                caption: "EQUIPO DE 11FREAKS",
                timeout: 3000,
                buttons: MessageBoxButtons.OK);
            }



        }

        private void Window_Closed(object sender, EventArgs e)
        {
            inicio.Close();
        }

        private void txtUsuario_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private async void ListBox_Loaded(object sender, RoutedEventArgs e)
        {
            string respuesta;
            JsonDocument jsonBingMaps;

            try {

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
                    Console.WriteLine(body);
                    txtRespuesta.Text=body;
                    jsonBingMaps = JsonDocument.Parse(body);
                }

                    miBaseDatos.BorrarDatosEquipos();               //ELIMINAMOS LOS DATOS ANTIGUOS
                
                for (int i = 0; i < 20; i++)
                {
                    string idEquipo = jsonBingMaps.RootElement.GetProperty("response")[i].GetProperty("team").GetProperty("id").ToString();
                    string nomEquipo = jsonBingMaps.RootElement.GetProperty("response")[i].GetProperty("team").GetProperty("name").ToString();
                    string fundEquipo = jsonBingMaps.RootElement.GetProperty("response")[i].GetProperty("team").GetProperty("founded").ToString();
                    string logoEquipo = jsonBingMaps.RootElement.GetProperty("response")[i].GetProperty("team").GetProperty("logo").ToString();
                    string estadio = jsonBingMaps.RootElement.GetProperty("response")[i].GetProperty("venue").GetProperty("name").ToString();
                    string ciudadEquipo = jsonBingMaps.RootElement.GetProperty("response")[i].GetProperty("venue").GetProperty("city").ToString();

                    
                    miBaseDatos.CargarDatosEquipos(idEquipo, nomEquipo, fundEquipo,logoEquipo, estadio, ciudadEquipo);                             //GUARDAMOS EQUIPO BASE DATOS

                    listBoxEquipos.Items.Add(jsonBingMaps.RootElement.GetProperty("response")[i].GetProperty("team").GetProperty("name"));  //Añadimos a la lista


                }

                var mensajeTemporal = AutoClosingMessageBox.Show(
                text: "HA SIDO CREADO ACTUALIZADOS A DÍA " + DateTime.Now.ToString(),
                caption: "EQUIPO DE 11FREAKS",
                timeout: 2500,
                buttons: MessageBoxButtons.OK);

            }
            catch (Exception ex){

            }


      
        }

        private void listBoxEquipos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var mensajeTemporal = AutoClosingMessageBox.Show(
            text: "INDICE SELECCIONADO "+listBoxEquipos.SelectedIndex,
            caption: "EQUIPO DE 11FREAKS",
            timeout: 2500,
            buttons: MessageBoxButtons.OK);

            idEquipo= miBaseDatos.BuscarEquipo(listBoxEquipos.Items[listBoxEquipos.SelectedIndex].ToString());

            var mensajeTemporal2 = AutoClosingMessageBox.Show(
            text: listBoxEquipos.Items[listBoxEquipos.SelectedIndex].ToString(),
            caption: "EQUIPO DE 11FREAKS",
            timeout: 2500,
            buttons: MessageBoxButtons.OK);


        }
    }
}
