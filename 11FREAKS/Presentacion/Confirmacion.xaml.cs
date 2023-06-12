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
    /// Ventana Auxiliar para pedir confirmación sobre diversas operaciones
    /// </summary>
    public partial class Confirmacion : Window
    {
        Principal principal;
        GestionUsuarios gestion;
        bool respuesta;


        public Confirmacion(Principal prin)
        {
            InitializeComponent();
            principal = prin;
        }
        public Confirmacion(GestionUsuarios gest)
        {
            InitializeComponent();
            gestion = gest;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Lógica de la interacción con el botón "SI"
        /// </summary>
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


        /// <summary>
        /// Lógica de la interacción con el botón "NO"
        /// </summary>
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

        /// <summary>
        /// Devuelve el resultado de la elección
        /// </summary>
        public bool DevuelveRespuesta()
        {
            return respuesta;
        }
    }
}
