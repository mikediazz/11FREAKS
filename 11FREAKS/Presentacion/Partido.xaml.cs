using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Windows.Threading;
using System.Windows.Forms;
using System.Windows.Media.Animation;
using System.Security.Cryptography;
using _11FREAKS.Datos;

namespace _11FREAKS.Presentacion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Partido : Window
    {
        Principal principal;
        BDOnline bdServer;
        DispatcherTimer dispatcherTimer;
        List<int> listaEventos;
        int local = 50;                         //Gestión Probabilidad Victoria Ambos Equipos
        int visitante = 50;
        int golesLocal = 0;
        int golesVisitante = 0;

        public Partido(Principal ppal, BDOnline bd)
        {
            InitializeComponent();

            this.principal = ppal;
            this.bdServer = bd;
            //dispatcherTimer = new DispatcherTimer();        //GESTIONA HORA DEL SISTEMA
            temporizador = new System.Windows.Threading.DispatcherTimer();
            temporizador.Interval = new TimeSpan(0, 0, 0, 1);
            temporizador.Tick += new EventHandler(temporizador_tick);



        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            /* DispatcherTimer timer = new DispatcherTimer();
             timer.Interval = TimeSpan.FromSeconds(1);
             timer.Tick += timer_Tick;
             timer.Start();*/






            /*string encriptado=Encriptar("Probando7");

             var msgDescuento = AutoClosingMessageBox.Show(
             text: "-" + encriptado + "-",
             caption: "ÁRBITRO",
             timeout: 1500,
             buttons: MessageBoxButtons.OK);*/



            listaEventos = SituarEventos(NumeroEventos());             //GENERAMOS NÚMERO DE EVENTOS QUE TENDRÁ EL PARTIDO Y EL MINUTO EN EL QUE SE PRODUCIRÁN





        }

        void timer_Tick(object sender, EventArgs e)
        {
            // lblCrono.Content = DateTime.Now.ToString();
        }

        System.Windows.Threading.DispatcherTimer temporizador;

        // variables
        int i = 1;
        int u = 0;
        int segundo = 0;
        int minuto = 0;
        int numero = 0;
        bool primeraParte = true;  //INFORMA SI EL PARTIDO SE ENCUENTRA EN LA 1º PARTE
        bool segundaParte = false;  //INFORMA SI EL PARTIDO SE ENCUENTRA EN LA 1º PARTE
        int descuento1 = 0;     //TIEMPO DESCUENTO PRIMERA MITAD
        int descuento2 = 0;     //TIEMPO DESCUENTO SEGUNDA MITAD
        int tiempo1 = 45;       //DURACIÓN PRIMERA MITAD
        int tiempo2 = 90;       //DURACIÓN SEGUNDA MITAD
        bool cartelonDto1 = false;       //INFORMA SI SE HA MOSTRADO EL DESCUENTO DEL 1º TIEMPO AL USUARIO
        bool cartelonDto2 = false;       //INFORMA SI SE HA MOSTRADO EL DESCUENTO DEL 2º TIEMPO AL USUARIO

        public void temporizador_tick(object sender, EventArgs e)
        {

            /*if (segundo == 60)
            {
                minuto++;
                i = 0;
                segundo = 0;
            }
            if (minuto == 60)
            {

                hora++;
                minuto = 0;
                segundo = 0;
                i = 0;
            }
            lblCrono.Content = hora.ToString("00") + ":" + minuto.ToString("00") + ":" + segundo.ToString("00");
             */



            if (listaEventos.Any(x => x == i))
            {
                /* var msgDescuento = AutoClosingMessageBox.Show(                                ////////////////////////////////// MUESTRA MINUTRO EN EL QUE SUCEDE UN EVENTO /////////////////////////////////
                 text: "EVENTO EN EL " + i + " '",
                 caption: "ÁRBITRO",
                 timeout: 1500,
                 buttons: MessageBoxButtons.OK);*/

                //ElegirBando(EleccionEvento(),local, visitante);
                EleccionEvento();
            }



            if (i > 44)                     //TIEMPO HASTA QUE SE MUESTRA CARTELÓN CON DESCUENTO
            {
                if (cartelonDto1 == false)
                {
                    descuento1 = TiempoDescuento();       //MUESTRA TIEMPO DESCUENTO
                    cartelonDto1 = true;
                }
            }

            if ((i == (tiempo1 + descuento1 + 1)))    //TIEMPO DESDE QUE DE MUESTRA EL DESCUENTO HASTA EL DESCANSO
            {
                lblInfo.Content = "DESCANSO";   //ANUNCIAMOS DESCANSO DEL PARTIDO
                System.Threading.Thread.Sleep(15000);       //15000
                lblInfo.Content = "2º PARTE";   //MOSTRAMOS FINAL DEL PARTIDO
                i = 45;
                lblCrono.Content = minuto.ToString("00") + "'";
                descuento1 = 100;

            }

            //---------------- DESCANSO ----------------//

            if (i > 89)                             //TIEMPO DESDE EL DESCANSO HASTA QUE SE MUESTRA CARTELÓN CON DESCUENTO 
            {
                if (cartelonDto2 == false)
                {
                    descuento2 = TiempoDescuento();       //MUESTRA TIEMPO DESCUENTO
                    cartelonDto2 = true;
                }

            }

            if (i > (tiempo2 + descuento2))
            {
                FinalizarPartido();
            }

            //internal async Task





            lblCrono.Content = minuto.ToString("00") + "'";         //ACTUALIZA MINUTO DEL PARTIDO

            i++;                //CRONO
            minuto = i;        //EQUIVALDRÍA A MINUTOS EN EL PARTIDO

        }





        private void btnIniciar_Click(object sender, RoutedEventArgs e)     //INICIO CRONOMETRO
        {
            temporizador.Start();
            u++;

            if (u == numero)
            {
                temporizador.Stop();
                numero = numero + 1;
            }

            /*                      ANIMACIÓN DE PARPADEO
             var fadeAnimation = new DoubleAnimation();
             fadeAnimation.From = 1;
             fadeAnimation.From = 0;

             fadeAnimation.AutoReverse = true;

             lblMarcador.BeginAnimation(System.Windows.Controls.Label.OpacityProperty, fadeAnimation);
            */

            btnIniciar.Visibility= Visibility.Collapsed;

        }

        private void btnPausar_Click(object sender, RoutedEventArgs e)
        {
            FinalizarPartido();         //INVOCAMOS AL MÉTODO FINALIZAAR PARTIDO
        }

        private void FinalizarPartido()
        {
            if (temporizador.IsEnabled)
            {
                temporizador.Stop();        //PARAMOS EL CRONO Y RESETEAMOS VALORES
                segundo = 0;
                minuto = 0;
                i = 0;
                lblInfo.Content = "¡FINAL!";   //MOSTRAMOS FINAL DEL PARTIDO
                lblCrono.Visibility = Visibility.Collapsed;
            }
            lblCrono.Content = string.Empty;
        }

        private void DescansoPartido()
        {
            if (temporizador.IsEnabled)
            {
                temporizador.Stop();        //PARAMOS EL CRONO Y RESETEAMOS VALORES
                segundo = 0;
                minuto = 45;
                i = 45;
                lblCrono.Content = "DESCANSO";   //MOSTRAMOS FINAL DEL PARTIDO
                System.Threading.Thread.Sleep(15000);
            }
        }

        private int TiempoDescuento()
        {
            var random = new Random();
            int descuento = random.Next(0, 7);
            var msgDescuento = AutoClosingMessageBox.Show(
            text: "TIEMPO DE DESCUENTO " + descuento.ToString() + " '",
            caption: "ÁRBITRO",
            timeout: 1500,
            buttons: MessageBoxButtons.OK);

            return descuento;
        }


        private string GenerarEvento()
        {
            return "gol";
        }


        private int NumeroEventos()
        {
            var random = new Random();
            int numEventos = random.Next(20, 40);
            return numEventos;
        }


        private List<int> SituarEventos(int numEventos)
        {
            List<int> listaEventos = new List<int>();
            for (int i = 0; i < numEventos; i++)
            {
                var random = new Random();
                int minuto = random.Next(0, 97);
                listaEventos.Add(minuto);
            }
            listaEventos.Sort();                                //ORDENAMOS EVENTOS POR ORDEN CRONOLÓGICO EN EL PARTIDO

            /* foreach (int minutoEvento in listaEventos)
             {
 C
             }*/

            return listaEventos;
        }


        static string Encriptar(string entrada)
        {
            /*
            StringBuilder sb = new StringBuilder();

            // Initialize a MD5 hash object
            using (MD5 md5 = MD5.Create())
            {
                // Compute the hash of the given string
                byte[] valorHash = md5.ComputeHash(Encoding.UTF8.GetBytes(entrada));

                // Convert the byte array to string format
                foreach (byte b in valorHash)
                {
                    sb.Append($"{b:X2}");
                }
            }

            return sb.ToString();*/


            string hash = String.Empty;

            // Initialize a SHA256 hash object
            using (SHA256 sha256 = SHA256.Create())
            {
                // Compute the hash of the given string
                byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(entrada));

                // Convert the byte array to string format
                foreach (byte b in hashValue)
                {
                    hash += $"{b:X2}";
                }
            }

            return hash;
        }





        public int EleccionEvento()
        {
            Random random = new Random();
            int aleatorio = random.Next(1, 101);

            switch (aleatorio)
            {

                case int n when (n >= 1 && n <= 15):
                    var msgEvento = AutoClosingMessageBox.Show(
                    text: "GOOOOL",
                    caption: "ÁRBITRO",
                    timeout: 1500,
                    buttons: MessageBoxButtons.OK);
                    Random bando = new Random();
                    ElegirBando(1, local, visitante);
                    return 1;

                case int n when (n >= 16 && n <= 18):
                    var msgEvento1 = AutoClosingMessageBox.Show(
                    text: "NOOO! GOL EN PROPIA",
                    caption: "ÁRBITRO",
                    timeout: 1500,
                    buttons: MessageBoxButtons.OK);
                    ElegirBando(2, local, visitante);
                    return 2;

                case 19:
                    var msgEvento2 = AutoClosingMessageBox.Show(
                    text: "TRAS SER REVISADO, EL GOL HA SIDO ANULADO",
                    caption: "ÁRBITRO",
                    timeout: 1500,
                    buttons: MessageBoxButtons.OK);
                    ElegirBando(3, local, visitante);
                    return 3;

                case int n when (n >= 20 && n <= 44):
                    var msgEvento3 = AutoClosingMessageBox.Show(
                    text: "FALTA",
                    caption: "ÁRBITRO",
                    timeout: 1500,
                    buttons: MessageBoxButtons.OK);
                    ElegirBando(4, local, visitante);                //ASIGNAMOS EL EQUIPO QUE HAYA HECHO LA FALTA
                    return 4;

                case int n when (n >= 45 && n <= 61):
                    var msgEvento4 = AutoClosingMessageBox.Show(
                    text: "AMARILLA",
                    caption: "ÁRBITRO",
                    timeout: 1500,
                    buttons: MessageBoxButtons.OK);
                    ElegirBando(5, local, visitante);
                    return 5;

                case int n when (n >= 62 && n <= 66):
                    var msgEvento5 = AutoClosingMessageBox.Show(
                     text: "ROJA! EL JUGADOR SE PERDERÁ EL PRÓXIMO PARTIDO",
                     caption: "ÁRBITRO",
                     timeout: 1500,
                     buttons: MessageBoxButtons.OK);
                    ElegirBando(6, local, visitante);
                    return 6;

                case int n when (n >= 67 && n <= 73):
                    var msgEvento6 = AutoClosingMessageBox.Show(
                    text: "PENALTI!!!",
                    caption: "ÁRBITRO",
                    timeout: 1500,
                    buttons: MessageBoxButtons.OK);
                    ElegirBando(7, local, visitante);
                    return 7;

                case int n when (n >= 74 && n <= 93):
                    var msgEvento7 = AutoClosingMessageBox.Show(
                    text: "SAQUE DE ESQUINA",
                    caption: "ÁRBITRO",
                    timeout: 1500,
                    buttons: MessageBoxButtons.OK);
                    ElegirBando(8, local, visitante);
                    return 8;

                case 94:
                    var msgEvento8 = AutoClosingMessageBox.Show(
                    text: "PINTA MAL, PARECE QUE NO PODRÁ CONTINUAR",
                    caption: "ÁRBITRO",
                    timeout: 1500,
                    buttons: MessageBoxButtons.OK);
                    ElegirBando(9, local, visitante);
                    return 9;

                case int n when (n >= 95 && n <= 97):
                    var msgEvento9 = AutoClosingMessageBox.Show(
                    text: "AL PALOO, ESO HA ESTADO CERCA",
                    caption: "ÁRBITRO",
                    timeout: 1500,
                    buttons: MessageBoxButtons.OK);
                    ElegirBando(10, local, visitante);
                    return 10;

                case int n when (n >= 98 && n <= 100):
                    var msgEvento10 = AutoClosingMessageBox.Show(
                    text: "EL JUGADOR ESTABA ADELANTADO",
                    caption: "ÁRBITRO",
                    timeout: 1500,
                    buttons: MessageBoxButtons.OK);
                    ElegirBando(11, local, visitante);
                    return 11;

                default:                //DEVUELVE -1 EN CASO DE ERROR
                    return -1;




            }
        }




        public string ElegirBando(int evento, int local, int visitante)
        {
            string resultado = string.Empty;
            Random random = new Random();
            int numeroAleatorio = random.Next(1, 101);       // Genera un número aleatorio entre 1 y 100 

            int umbralEquipoA = local;

            if (numeroAleatorio <= umbralEquipoA)
            {
                var msgEvento10 = AutoClosingMessageBox.Show(
                text: "Local",
                caption: "ÁRBITRO",
                timeout: 1500,
                buttons: MessageBoxButtons.OK);

                switch (evento)             //SEGÚN EL TIPO DE EVENTO, SE MODIFICARÁN LAS PROBABILIDADES DE VICTORIA PARA UNO U OTRO EQUIPO
                {
                    case 1:                  //GOL
                        local += 10;
                        visitante -= 10;
                        golesLocal++;
                        lblMarcador.Content = golesLocal + "-" + golesVisitante;       //ACTUALIZAMOS MARCADOR
                        break;

                    case 2:                 //AUTOGOL
                        local -= 12;
                        visitante += 12;
                        golesVisitante++;
                        lblMarcador.Content = golesLocal + "-" + golesVisitante;       //ACTUALIZAMOS MARCADOR
                        break;

                    case 3:                 //GOL ANULADO
                        local += 1;
                        visitante -= 1;
                        break;

                    case 5:                 //AMARILLA
                        local -= 1;
                        visitante += 1;
                        break;

                    case 6:                 //ROJA
                        local -= 15;
                        visitante += 15;
                        break;

                    case 7:                 //PENALTI
                        local += 7;                 //GESTIONAR SI SE MARCA O SE FALLA EL PENALTI
                        visitante -= 7;
                        break;

                    case 8:                 //CORNER
                        local += 1;
                        visitante -= 1;
                        break;

                    case 9:                 //LESIÓN
                        local -= 2;
                        visitante += 2;
                        break;

                    case 10:                //PALO
                        local += 1;
                        visitante -= 1;
                        break;

                }
                resultado = "local";


            }
            else
            {
                var msgEvento10 = AutoClosingMessageBox.Show(
                text: "Visitante",
                caption: "ÁRBITRO",
                timeout: 1500,
                buttons: MessageBoxButtons.OK);
                switch (evento)             //SEGÚN EL TIPO DE EVENTO, SE MODIFICARÁN LAS PROBABILIDADES DE VICTORIA PARA UNO U OTRO EQUIPO
                {
                    case 1:                  //GOL
                        visitante += 10;
                        local -= 10;
                        golesVisitante++;
                        lblMarcador.Content = golesLocal + "-" + golesVisitante;      //ACTUALIZAMOS MARCADOR
                        break;

                    case 2:                 //AUTOGOL
                        visitante -= 12;
                        local += 12;
                        golesLocal++;
                        lblMarcador.Content = golesLocal + "-" + golesVisitante;       //ACTUALIZAMOS MARCADOR
                        break;

                    case 3:                 //GOL ANULADO
                        visitante += 1;
                        local -= 1;
                        break;

                    case 5:                 //AMARILLA
                        visitante -= 1;
                        local += 1;
                        break;

                    case 6:                 //ROJA
                        visitante -= 15;
                        local += 15;
                        break;

                    case 7:                 //PENALTI
                        visitante += 7;                 //GESTIONAR SI SE MARCA O SE FALLA EL PENALTI
                        local -= 7;
                        break;

                    case 8:                 //CORNER
                        visitante += 1;
                        local -= 1;
                        break;

                    case 9:                 //LESIÓN
                        visitante -= 2;
                        local += 2;
                        break;

                    case 10:                //PALO
                        visitante += 1;
                        local -= 1;
                        break;

                }
                resultado = "visitante";


            }
            return resultado;



        }
    }
}
