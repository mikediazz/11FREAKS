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


        public Penalti(Partido part, BDOnline bd)
        {
            InitializeComponent();
            this.partido = part;
            this.bdServer = bd;
        }

        private void OnCellClick(object sender, RoutedEventArgs e)
        {
            Button cell = (Button)sender;


            int row = Grid.GetRow(cell);                // Obtén la posición de la celda en la cuadrícula
            int column = Grid.GetColumn(cell);


            bool isGoal = DetermineGoal(row, column);   // Realiza la lógica del lanzamiento de penalti

            if (isGoal)
            {
                cell.Background = Brushes.Green;
                MessageBox.Show("¡Goooooool!");         // La celda seleccionada fue un gol
                Thread.Sleep(5000);
                cell.Background = Brushes.LightGray;
            }
            else
            {
                cell.Background = Brushes.Red;
                MessageBox.Show("¡El portero ha parado el disparo!");   // La celda seleccionada fue parada por el portero
                Thread.Sleep(5000);
                cell.Background = Brushes.LightGray;


            }
        }





        private bool DetermineGoal(int row, int column)
        {
            // Genera aleatoriamente la ubicación de las celdas que serán falsas (paradas del portero)
            List<Tuple<int, int>> blockedCells = GenerateBlockedCells();

            // Comprueba si la celda seleccionada está bloqueada
            foreach (var blockedCell in blockedCells)
            {
                if (blockedCell.Item1 == row && blockedCell.Item2 == column)
                {
                    return false; // El lanzamiento fue parado por el portero
                }
            }

            return true; // El lanzamiento fue un gol
        }

        private List<Tuple<int, int>> GenerateBlockedCells()
        {
            List<Tuple<int, int>> blockedCells = new List<Tuple<int, int>>();
            Random random = new Random();

            int totalCells = 9;  // Número total de celdas en la portería
            int blockedCellsCount = 3;  // Número de celdas que serán paradas del portero

            // Genera aleatoriamente las celdas bloqueadas
            while (blockedCells.Count < blockedCellsCount)
            {
                int row = random.Next(3);  // Número de filas en la cuadrícula (0-2)
                int column = random.Next(3);  // Número de columnas en la cuadrícula (0-2)

                Tuple<int, int> cell = new Tuple<int, int>(row, column);

                // Verifica si la celda ya está bloqueada
                if (!blockedCells.Contains(cell))
                {
                    blockedCells.Add(cell);
                }
            }

            return blockedCells;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;        //CANCELAMOS CIERRE VENTANA
        }
    }
}
