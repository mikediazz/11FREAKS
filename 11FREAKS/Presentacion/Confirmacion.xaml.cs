using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Lógica de interacción para Confirmacion.xaml
    /// </summary>
    public partial class Confirmacion : Window
    {
        Principal principal;
        bool respuesta;
        public Confirmacion(Principal prin)
        {
            InitializeComponent();
            principal = prin;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnSi_Click(object sender, RoutedEventArgs e)
        {
            respuesta = true;
            this.Hide();
            var mensajeTemporal2 = AutoClosingMessageBox.Show(
            text: "Confirmando Cambios ...",
            caption: "EQUIPO DE 11FREAKS",
            timeout: 2000,
            buttons: MessageBoxButtons.OK);
            this.Close();
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            respuesta = false;
            this.Hide();
            var mensajeTemporal2 = AutoClosingMessageBox.Show(
            text: "Regresando ...",
            caption: "EQUIPO DE 11FREAKS",
            timeout: 2000,
            buttons: MessageBoxButtons.OK);
            this.Close();
        }


        public bool DevuelveRespuesta()
        {
            return respuesta;
        }
    }
}
