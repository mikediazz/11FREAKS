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
        private Datos.BaseDatos miBaseDatos;

        public BusquedaUsuarios()
        {
            InitializeComponent();
            miBaseDatos= new Datos.BaseDatos();                             //INICIALIZO BASE DATOS
        }

        /// <summary>
        ///     Función Vinculada al Botón Buscar
        /// </summary>
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            listBoxUsuarios.Items.Clear();                                  //RESETEAMOS LISTBOX

            try
            {
                for (int i = 0; i < miBaseDatos.ConsultaUsuarios().Count; i++)
                {
                    listBoxUsuarios.Items.Add(miBaseDatos.ConsultaUsuarios()[i]);
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

                string correoBaneado= miBaseDatos.DevuelveCorreo(auxNom);           //ERROR AL REALIZAR CONEXIÓN 
                MessageBox.Show("EMAIL DE USUARIO A BANEAR "+correoBaneado);

                miBaseDatos.BorrarUsuario(auxNom);
                listBoxUsuarios.Items.RemoveAt(listBoxUsuarios.SelectedIndex);

                Correo correo = new Correo();
                correo.CorreoBaneo(correoBaneado);
            }


        }
    }
}
