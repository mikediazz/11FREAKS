using _11FREAKS.Datos;
using Org.BouncyCastle.Asn1.Crmf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
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
                        miBaseDatos.CrearUsuario(txtUsuario.Text, txtPassword.Password, idEquipo, txtEmail.Text);          //CREAMOS USUARIO

                        var mensajeTemporal = AutoClosingMessageBox.Show(
                        text: "**USUARIO CREADO**    ID EQUIPO",
                        caption: "EQUIPO DE 11FREAKS",
                        timeout: 1500,
                        buttons: MessageBoxButtons.OK);

                        Correo mailBienvenida=new Correo();                                                 //ENVIAMOS CORREO DE BIENVENIDA AL USUARIO
                       // mailBienvenida.CorreoBienvenida(txtEmail.Text);

                        /*Thread volumenThread = new Thread(new ThreadStart(mailBienvenida.CorreoBienvenida(txtEmail.)));          //CREAMOS HILO AL QUE LE PASAMOS EL MÉTODO PARA INICIAR INTRO                                                                        
                        volumenThread.Start();*/

                        /*  Thread hilo = new Thread(new ParameterizedThreadStart(mailBienvenida.CorreoBienvenida);
                          hilo.Start(txtEmail.Text);*/

                        Thread thMailBienvenida = new Thread(() => mailBienvenida.CorreoBienvenida(txtEmail.Text));
                        thMailBienvenida.Start();

                        Principal principal = new Principal(inicio, miBaseDatos, txtUsuario.Text);
                        this.Hide();
                        principal.ShowDialog();
                    }
                    
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
                text: "PLANTILLAS ACTUALIZADAS A DÍA " + DateTime.Now.ToString(),
                caption: "EQUIPO DE 11FREAKS",
                timeout: 2500,
                buttons: MessageBoxButtons.OK);

            }
            catch (Exception ex){

            }


      
        }

        private void listBoxEquipos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*var mensajeTemporal = AutoClosingMessageBox.Show(
            text: "INDICE SELECCIONADO "+listBoxEquipos.SelectedIndex,
            caption: "EQUIPO DE 11FREAKS",
            timeout: 2500,
            buttons: MessageBoxButtons.OK);*/

            idEquipo= miBaseDatos.BuscarEquipo(listBoxEquipos.Items[listBoxEquipos.SelectedIndex].ToString());

            /*var mensajeTemporal2 = AutoClosingMessageBox.Show(
            text: listBoxEquipos.Items[listBoxEquipos.SelectedIndex].ToString(),
            caption: "EQUIPO DE 11FREAKS",
            timeout: 2500,
            buttons: MessageBoxButtons.OK);*/


        }



        private int CompruebaCampos()
        {
            int comprobador=0;          //VARIABLE COMPROBACIÓN REQUISITOS

            if(txtUsuario==null || txtUsuario.Text == string.Empty)
            {
                comprobador +=1;
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


    }
}
