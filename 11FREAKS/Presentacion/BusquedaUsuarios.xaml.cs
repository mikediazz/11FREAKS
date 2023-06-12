using _11FREAKS.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
//using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _11FREAKS.Presentacion
{
    /// <summary>
    /// Clase Interacción con BusquedaUsuarios.xaml || Control Personalizado para la Busqueda de Usuarios Registrados
    /// </summary>
    public partial class BusquedaUsuarios : UserControl
    {

        public delegate void MiEvento();
        public event MiEvento miEvento;
        private Datos.BDOnline bdServer;

        public BusquedaUsuarios()
        {
            InitializeComponent();                          
            bdServer = new Datos.BDOnline();                                    //INICIALIZAMOS BASE DATOS
        }

        /// <summary>
        ///     Función Vinculada al Botón Buscar
        /// </summary>
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            listBoxUsuarios.Items.Clear();                                  //RESETEAMOS LISTBOX

            try
            {
                for (int i = 0; i < bdServer.ConsultaUsuarios().Count; i++)
                {
                    listBoxUsuarios.Items.Add(bdServer.ConsultaUsuarios()[i]);
                }
            }
            catch (Exception ex)
            {
                //MENSAJE ERROR 
            }

            if (txtUsuario.Text.Length > 0 || txtUsuario.Text!=null){
               
                for (int i = 0; i < listBoxUsuarios.Items.Count; i++)
                {
                    if (txtUsuario.Text == listBoxUsuarios.Items[i].ToString())
                    {
                        listBoxUsuarios.SelectedIndex = listBoxUsuarios.Items.IndexOf(listBoxUsuarios.Items[i]);
                        //MessageBox.Show("resultado "+listBoxUsuarios.Items.IndexOf(listBoxUsuarios.Items[i]).ToString());
                    }
                }
            }


            


        }

        private void listBoxUsuarios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { 
            //MessageBox.Show(listBoxUsuarios.SelectedIndex.ToString());  
        }


        /// <summary>
        ///     Función Gestiona Interacción con Elementos de ListBox Usuarios --> Permite Borrado de Usuarios
        /// </summary>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

            if (listBoxUsuarios.SelectedIndex == -1)
            {
                return;
            }

            string auxNom = listBoxUsuarios.Items[listBoxUsuarios.SelectedIndex].ToString();
            char esAdmin = auxNom[0];                                                           

            if (esAdmin == '#')
            {
                MessageBox.Show("ESTE USUARIO ES ADMINISTRADOR");
            }
            else
            {
                MessageBox.Show("USUARIO SELECCIONADO -->" + auxNom);

                string correoBaneado= bdServer.DevuelveCorreo(auxNom);           //ERROR AL REALIZAR CONEXIÓN 
                MessageBox.Show("EMAIL DE USUARIO A BANEAR "+correoBaneado);
                bdServer.BorrarUsuario(auxNom);
                listBoxUsuarios.Items.RemoveAt(listBoxUsuarios.SelectedIndex);

                Correo correo = new Correo();
                correo.CorreoBaneo(correoBaneado);
            }


        }


        /// <summary>
        ///     Función Gestiona Interacción con Elementos de ListBox Usuarios --> Permite Otorgar Permisos
        /// </summary>
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            string auxNom = listBoxUsuarios.Items[listBoxUsuarios.SelectedIndex].ToString();
            char esAdmin = auxNom[0];

            if (listBoxUsuarios.SelectedIndex == -1)
            {
                return;
            }
            else
            {
                bdServer.DarPermisos(auxNom);
            }
            if (esAdmin == '#')
            {
                MessageBox.Show("ESTE USUARIO ES ADMINISTRADOR");
            }
 
        }
    }
}
