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
using System.Threading;

namespace _11FREAKS.Presentacion
{
    /// <summary>
    /// Clase que gestiona la Ventana Partido
    /// </summary>
    public partial class Partido : Window
    {
        Principal principal;
        BDOnline bdServer;
        DispatcherTimer dispatcherTimer;
        List<int> listaEventos;
        int local = 50;                         //Gestión Probabilidad Victoria Ambos Equipos
        int visitante = 50;
        public int GolesLocal { get; set; }
        public int GolesVisitante { get; set; }
        bool permitirCierre = false;
        int idEvento=-1;

            //VARIABLES TIEMPO
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
            System.Windows.Threading.DispatcherTimer temporizador;


        public Partido(Principal ppal, BDOnline bd)
        {
            InitializeComponent();

            this.principal = ppal;
            this.bdServer = bd;

            GolesLocal = 0;
            GolesVisitante = 0;
            //dispatcherTimer = new DispatcherTimer();        //GESTIONA HORA DEL SISTEMA
            temporizador = new System.Windows.Threading.DispatcherTimer();
            temporizador.Interval = new TimeSpan(0, 0, 0, 1);
            temporizador.Tick += new EventHandler(temporizador_tick);

        }

        /// <summary>
        /// Método que al cargar ventana Partido, llama a los método encargados de generar los eventos y en el minuto en el que se producirán
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listaEventos = SituarEventos(NumeroEventos());             //GENERAMOS NÚMERO DE EVENTOS QUE TENDRÁ EL PARTIDO Y EL MINUTO EN EL QUE SE PRODUCIRÁN

            if (WindowState == WindowState.Maximized)
            {
                double width = ActualWidth;
                double height = ActualHeight;
                System.Windows.MessageBox.Show(width+"x"+height);

                // Haz lo que necesites con las dimensiones de la ventana maximizada
            }
        }


        /// <summary>
        /// Método se encarga de que pasen los minutos 
        /// </summary>
        public void temporizador_tick(object sender, EventArgs e)
        {

            if (listaEventos.Any(x => x == i))
            {
                idEvento=EleccionEvento();

                recursoEvento.Source = new Uri(bdServer.DevuelveRecursoEvento(idEvento));       //LLAMAR MÉTODO QUE MUESTRE RECURSO EVENTO
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




        /// <summary>
        /// OnClick botón Iniciar
        /// </summary>
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


        /// <summary>
        /// Método que gestiona la finalización del partido
        /// </summary>
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


            if(GolesLocal>GolesVisitante)                                       //ACTUALIZAMOS ESTADÍSTICAS EQUIPO TRAS EL PARTIDO
            {
                bdServer.ResultadosPartido(1, GolesLocal, GolesVisitante);      //VICTORIA
            }else if (GolesLocal == GolesVisitante)
            {
                bdServer.ResultadosPartido(0, GolesLocal, GolesVisitante);      //EMPATE
            }
            else
            {
                bdServer.ResultadosPartido(2, GolesLocal, GolesVisitante);      //DERROTA
            }

            bdServer.ActualizacionClasificacion();                              //TRAS FINALIZAR EL PARTIDO, ACTUALIZAMOS CLASIFICACIÓN DE LA LIGA

            Thread.Sleep(3000);                                                 //ESPERAMOS 3 SEGUNDOS Y CERRAMOS LA VENTANA
            this.Close();
        }


        /// <summary>
        /// Método que gestiona el descanso del partido
        /// </summary>
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

        /// <summary>
        /// Método que asigna los tiempos de descuento del partido
        /// </summary>
        /// <returns>
        ///     Devuelve minutos de descuento
        ///     <see cref="int"/>
        /// </returns>
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

        /// <summary>
        /// Método que genera aleatoriamente el número de eventos que tendrán lugar en el partido
        /// </summary>
        /// <returns>
        ///     Devuelve número de eventos que tendrá el partido
        ///     <see cref="int"/>
        /// </returns>
        private int NumeroEventos()
        {
            var random = new Random();
            int numEventos = random.Next(20, 40);
            return numEventos;
        }


        /// <summary>
        /// Método que asigna cada evento a un minuto del partido de forma aleatoria
        /// </summary>
        /// <param name="numEventos">
        ///     Recibimos número de eventos totales del partido
        /// </param>
        /// <returns>
        ///     Devuelve lista ordenada con los minutos en los que ocurrirán los eventos
        ///     <see cref="List"/>
        /// </returns>
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

            return listaEventos;
        }



        /// <summary>
        /// Método que asigna cada evento del partido
        /// </summary>
        /// <returns>
        ///     Devuelve idEvento
        ///     <see cref="int"/>
        /// </returns>
        public int EleccionEvento()
        {
            Random random = new Random();
            int aleatorio = random.Next(1, 101);

            switch (aleatorio)
            {
                case int n when (n >= 1 && n <= 1):           // ROJA (1%)
                    var msgEvento5 = AutoClosingMessageBox.Show(
                        text: "ROJA! EL JUGADOR SE PERDERÁ EL PRÓXIMO PARTIDO",
                        caption: "ÁRBITRO",
                        timeout: 1500,
                        buttons: MessageBoxButtons.OK);
                    ElegirBando(6, local, visitante);
                    return 6;

                case int n when (n >= 2 && n <= 3):           // AUTOGOL (2%)
                    var msgEvento1 = AutoClosingMessageBox.Show(
                        text: "NOOO! GOL EN PROPIA",
                        caption: "ÁRBITRO",
                        timeout: 1500,
                        buttons: MessageBoxButtons.OK);
                    ElegirBando(2, local, visitante);
                    return 2;

                case int n when (n >= 4 && n <= 8):           // GOL ANULADO (5%)
                    var msgEvento2 = AutoClosingMessageBox.Show(
                        text: "TRAS SER REVISADO, EL GOL HA SIDO ANULADO",
                        caption: "ÁRBITRO",
                        timeout: 1500,
                        buttons: MessageBoxButtons.OK);
                    ElegirBando(3, local, visitante);
                    return 3;

                case int n when (n >= 9 && n <= 18):          // GOL (10%)
                    var msgEvento = AutoClosingMessageBox.Show(
                        text: "GOOOOL",
                        caption: "ÁRBITRO",
                        timeout: 1500,
                        buttons: MessageBoxButtons.OK);
                    Random bando = new Random();
                    ElegirBando(1, local, visitante);
                    return 1;

                case int n when (n >= 19 && n <= 44):          // FALTA (26%)
                    var msgEvento3 = AutoClosingMessageBox.Show(
                        text: "FALTA",
                        caption: "ÁRBITRO",
                        timeout: 1500,
                        buttons: MessageBoxButtons.OK);
                    ElegirBando(4, local, visitante);
                    return 4;

                case int n when (n >= 45 && n <= 60):          // AMARILLA (16%)
                    var msgEvento4 = AutoClosingMessageBox.Show(
                        text: "AMARILLA",
                        caption: "ÁRBITRO",
                        timeout: 1500,
                        buttons: MessageBoxButtons.OK);
                    ElegirBando(5, local, visitante);
                    return 5;

                case int n when (n >= 61 && n <= 64):          // PALO (4%)
                    var msgEvento9 = AutoClosingMessageBox.Show(
                        text: "AL PALOO, ESO HA ESTADO CERCA",
                        caption: "ÁRBITRO",
                        timeout: 1500,
                        buttons: MessageBoxButtons.OK);
                    ElegirBando(10, local, visitante);
                    return 10;

                case int n when (n >= 65 && n <= 66):          // LESIÓN (2%)
                    var msgEvento8 = AutoClosingMessageBox.Show(
                        text: "PINTA MAL, PARECE QUE NO PODRÁ CONTINUAR",
                        caption: "ÁRBITRO",
                        timeout: 1500,
                        buttons: MessageBoxButtons.OK);
                    ElegirBando(9, local, visitante);
                    return 9;

                case int n when (n >= 67 && n <= 91):          // SAQUE DE ESQUINA (25%)
                    var msgEvento7 = AutoClosingMessageBox.Show(
                        text: "SAQUE DE ESQUINA",
                        caption: "ÁRBITRO",
                        timeout: 1500,
                        buttons: MessageBoxButtons.OK);
                    ElegirBando(8, local, visitante);
                    return 8;

                case int n when (n >= 92 && n <= 96):          // PENALTI (5%)
                    var msgEvento6 = AutoClosingMessageBox.Show(
                        text: "PENALTI!!!",
                        caption: "ÁRBITRO",
                        timeout: 1500,
                        buttons: MessageBoxButtons.OK);
                    ElegirBando(7, local, visitante);
                    Penalti penalti = new Penalti(this, bdServer);  // ABRIMOS VENTANA LANZAMIENTO PENALTI
                    penalti.ShowDialog();
                    return 7;

                case int n when (n >= 97 && n <= 100):         // FUERA DE JUEGO (15%)
                    var msgEvento10 = AutoClosingMessageBox.Show(
                        text: "FUERA DE JUEGO",
                        caption: "ÁRBITRO",
                        timeout: 1500,
                        buttons: MessageBoxButtons.OK);
                    ElegirBando(11, local, visitante);
                    return 11;

                default:                // DEVUELVE -1 EN CASO DE ERROR
                    return -1;
            }
        }






        /// <summary>
        /// Método que asinga cada evento a cada equipo
        /// </summary>
        /// <param name="evento">
        ///     Recibimos evento al cual se le asignará un equipo
        /// </param>
        /// <param name="local">
        ///     Recibimos probabilidad de victoria del equipo local
        /// </param>
        /// <param name="visitante">
        ///     Recibimos probabilidad de victoria del equipo visitante
        /// </param>
        /// <returns>
        ///     Devuelve el equipo al cual a sido asignado el evento pasado por parámetro
        ///     <see cref="string"/>
        /// </returns>
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
                        GolesLocal++;
                        lblMarcador.Content = GolesLocal + "-" + GolesVisitante;       //ACTUALIZAMOS MARCADOR
                        break;

                    case 2:                 //AUTOGOL
                        local -= 12;
                        visitante += 12;
                        GolesVisitante++;
                        lblMarcador.Content = GolesLocal + "-" + GolesVisitante;       //ACTUALIZAMOS MARCADOR
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
                        local += 7;                 
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
                        GolesVisitante++;
                        lblMarcador.Content = GolesLocal + "-" + GolesVisitante;      //ACTUALIZAMOS MARCADOR
                        break;

                    case 2:                 //AUTOGOL
                        visitante -= 12;
                        local += 12;
                        GolesLocal++;
                        lblMarcador.Content = GolesLocal + "-" + GolesVisitante;       //ACTUALIZAMOS MARCADOR
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
                        visitante += 7;                 
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

        /// <summary>
        /// Método que gestiona si se puede cerrar o no la ventana Partido
        /// </summary>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (permitirCierre==false) {
                e.Cancel = true;        //CANCELAMOS CIERRE VENTANA
            }
            else
            {
                e.Cancel = false;
            }
            
        }


        /// <summary>
        /// Método que abre la ventana Penalti y recoge el resultado del mismo
        /// </summary>
        private void AbrirVentanaPenalti()
        {
            Penalti ventanaPenalti = new Penalti(this, bdServer);
            ventanaPenalti.ShowDialog();

            
            bool valorGol = ventanaPenalti.Gol; // Acceder al valor después de que se cierre la ventana "Penalti"
            var msgEvento10 = AutoClosingMessageBox.Show(
            text: "RESULTADO DEL PENALTI\t" +valorGol,
            caption: "ÁRBITRO",
            timeout: 1500,
            buttons: MessageBoxButtons.OK);

        }

        private void recursoEvento_MediaEnded(object sender, RoutedEventArgs e)
        {
            recursoEvento.Source = null;
        }
    }
}
