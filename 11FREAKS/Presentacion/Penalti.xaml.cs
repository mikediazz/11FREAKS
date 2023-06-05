using _11FREAKS.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _11FREAKS.Presentacion
{
    /// <summary>
    /// Lógica de interacción para Penalti.xaml
    /// </summary>
    public partial class Penalti : Window
    {
        Partido partido;
        BDOnline bdServer;
        public bool Gol { get; set; }
        bool permitirClosing = false;



        public Penalti(Partido part, BDOnline bd)
        {
            InitializeComponent();
            this.partido = part;
            this.bdServer = bd;
        }

        private void OnCellClick(object sender, RoutedEventArgs e)
        {
            Button celda = (Button)sender;


            int fila = Grid.GetRow(celda);                // Obtén la posición de la celda en la cuadrícula
            int columna = Grid.GetColumn(celda);


            Gol = DeterminarGol(fila, columna);   // Realiza la lógica del lanzamiento de penalti y almacena en variable "gol"

            if (Gol)
            {
                celda.Background = Brushes.Green;
                MessageBox.Show("¡Goooooool!");         // La celda seleccionada fue un gol
                Thread.Sleep(5000);
                celda.Background = Brushes.LightGray;
                partido.GolesLocal += 1;                // Sumamos gol al equipo local
            }
            else
            {
                celda.Background = Brushes.Red;
                MessageBox.Show("¡El portero ha parado el disparo!");   // La celda seleccionada fue parada por el portero
                Thread.Sleep(5000);
                celda.Background = Brushes.LightGray;
            }

            partido.lblMarcador.Content = partido.GolesLocal + "-" + partido.GolesVisitante;       //ACTUALIZAMOS MARCADOR TRAS EL PENALTI

            permitirClosing = true;             //Permitimos que se pueda cerrar la ventana
            this.Close();



        }





        private bool DeterminarGol(int fila, int columna)
        {
            // Genera aleatoriamente la ubicación de las celdas que serán falsas (paradas del portero)
            List<Tuple<int, int>> blockedCells = GeneraParadas();

            // Comprueba si la celda seleccionada está bloqueada
            foreach (var blockedCell in blockedCells)
            {
                if (blockedCell.Item1 == fila && blockedCell.Item2 == columna)
                {
                    return false; // El lanzamiento fue parado por el portero
                }
            }

            return true; // El lanzamiento fue un gol
        }

        private List<Tuple<int, int>> GeneraParadas()
        {
            List<Tuple<int, int>> blockedCells = new List<Tuple<int, int>>();
            Random random = new Random();

            int totalCeldas = 9;    //Número total de celdas en la portería
            int celdasParadas = 3;  //Número de celdas que serán paradas del portero

            // Genera aleatoriamente las celdas bloqueadas
            while (blockedCells.Count < celdasParadas)
            {
                int fila = random.Next(3);              //Número de filas en la cuadrícula (0-2)
                int columna = random.Next(3);           //Número de columnas en la cuadrícula (0-2)

                Tuple<int, int> celda = new Tuple<int, int>(fila, columna);

                if (!blockedCells.Contains(celda))       // Verifica si la celda ya está bloqueada
                {
                    blockedCells.Add(celda);
                }
            }

            return blockedCells;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (permitirClosing==false)
            {
                e.Cancel = true;        //Cancelamos cierre ventana
            }
            
        }
    }
}
