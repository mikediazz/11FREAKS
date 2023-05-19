using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Security.Cryptography;
using System.Security.Policy;
using MySql.Data.MySqlClient;


namespace _11FREAKS.Datos
{
    public class BDOnline
    {
        //VARIABLES
        private MySqlConnection conexion;
        private MySqlCommand comando;
        private MySqlDataReader lector;
        //private string connectionString = "server=localhost;user=root;password=CIFP1;database=11freaks";
        private string connectionString = "Server=localhost;Database=11freaks;Uid=root;Pwd=CIFP1;";
        private string nomusuario;
        private string password;
        private bool permisos;
        private string equipo;
        private string correo;
        private bool conDisponible=false;



        public bool ConectarServer(string usuario, string contraseña){

            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {

                string hash = String.Empty;


                using (SHA256 sha256 = SHA256.Create())         // Inicializamos a SHA256 la Contraseña de Login
                {
                    byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(contraseña));      //Hasheo

                    foreach (byte b in hashValue)       //Convertimos Hash de Byte[] -> String
                    {
                        hash += $"{b:X2}";
                    }
                }




                try
                {
                    conexion.Open();
                    comando = new MySqlCommand("SELECT * FROM usuarios WHERE Usuario='" + usuario + "' and Password='" + hash + "'", conexion);
                    lector = comando.ExecuteReader();

                    if (lector.HasRows)                                                              //BUSCAMOS USUARIO
                    {
                        if (lector.Read())
                        {                                                         //COMPROBAMOS USUARIO Y SI ES ADMIN
                            nomusuario = lector.GetString(0);
                            password = lector["Password"].ToString();
                            permisos = Boolean.Parse(lector["Permisos"].ToString());
                            correo = lector.GetString(3);
                            //ALMACENARÍA IMAGEN FOTO PERFIL
                            equipo = lector["Equipo"].ToString();
                            conDisponible = true;
                        }
                    }
                    else
                    {                                                                          //SI NO EXISTE USUARIO...s                                                                     
                        MessageBox.Show("NO SE ENCONTRARON REGISTROS+++");
                    }

                    lector.Close();
                    conexion.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN " + ex.Message);
                }
                
            }//FIN USING 
            return conDisponible;
        }




        /// <summary>
        ///     Método para crer Usuario (SIGN UP) --> Encriptamos Contraseña
        /// </summary>
        /// <param name="usuario">
        ///     Recibimos Usuario del Login o Para Hacer Consulta
        /// </param>
        /// <param name="contraseña">
        ///     Recibimos Contraseña del Usuario
        /// </param>
        /// <param name="idEquipo">
        ///     Recibimos id Equipo Favorito (PUEDE SER NULO)
        /// </param>
        /// <returns>
        ///     Devuelve un Booleano con el Resultado de la Operación
        ///     <see cref="bool"/>
        /// </returns>
        public bool CrearUsuario(string usuario, string contraseña, string equipo, string correo)
        {
            bool conDisponible = false;

            //////// HASHEO PASSWORD ////////
            string hash = String.Empty;

            using (SHA256 sha256 = SHA256.Create())         // Inicializamos SHA256
            {
                byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(contraseña));  //Hasheamos Password

                foreach (byte b in hashValue) //Convertimos el Hash de Byte[] -> String
                {
                    hash += $"{b:X2}";
                }
            }

            // MessageBox.Show("EL HASH DE LA CONTRASEÑA ES -->"+hash);

            try
            {
                if (conexion == null)
                {
                    connectionString = "Server=localhost;Database=11freaks;Uid=root;Pwd=CIFP1;";
                    conexion = new MySqlConnection(connectionString);
                    comando = new MySqlCommand("INSERT INTO usuarios VALUES ( '" + usuario + "' , '" + hash + "', 'false', '" + correo + "', NULL , '" + equipo + "', 'true'); ", conexion);
                    
                    conexion.Open();
                    comando.ExecuteNonQuery();
                    conexion.Close();

                    MessageBox.Show("BIENVENIDO " + usuario + ", AHORA FORMAS PARTE DE 11FREAKS!");
                    conDisponible = true;
                }
                else
                {
                    MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN COD1*");
                    conDisponible = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN COD2*\n" + ex.Message);
            }



            return conDisponible;
        }






        /// <summary>
        ///     Método para crear EQUIPO (SIGN UP)
        /// </summary>
        /// <param name="equipo">
        ///     Recibimos Usuario del Login o Para Hacer Consulta
        /// </param>
        /// <param name="abreviatura">
        ///     Recibimos Contraseña del Usuario
        /// </param>
        /// <param name="liga">
        ///     Recibimos id Equipo Favorito (PUEDE SER NULO)
        /// </param>
        /// <returns>
        ///     Devuelve un Booleano con el Resultado de la Operación
        ///     <see cref="bool"/>
        /// </returns>
        public bool CrearEquipo(string equipo, string abreviatura, string liga)
        {
            bool conDisponible = false;
            conexion = null;

            try
            {
                if (conexion == null)
                {
                    connectionString = "Server=localhost;Database=11freaks;Uid=root;Pwd=CIFP1;";
                    conexion = new MySqlConnection(connectionString);
                    comando = new MySqlCommand("INSERT INTO equipos VALUES ( idEquipo , '"+liga+"'  , '" + equipo + "', '"+abreviatura+"', ' -1 ', 0 , '100000000', 0, 0, 0); ", conexion);  

                    conexion.Open();
                    MessageBox.Show("CONEXIÓN CREAR_EQUIPO "+conexion.State.ToString());
                    comando.ExecuteNonQuery();
                    conexion.Close();

                    MessageBox.Show(equipo + "HA SIDO INSCRITO EN LA LIGA "+liga+"!");
                    conDisponible = true;

                }
                else
                {
                    MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN COD1*");
                    conDisponible = false;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN COD2*\n" + ex.Message);
            }



            return conDisponible;
        }












        /// <summary>
        ///     Método para Comprobar que la Contraseña es Segura 
        /// </summary>
        /// <param name="password">
        ///     Recibimos la Contraseña del Usuario
        /// </param>
        /// <returns>
        ///     Devuelve si cumple los requisitos mínimos de seguridad
        /// </returns>
        public bool CompruebaPassword(String usuario, String password)
        {
            bool validador = false;

            bool validezPass = false;                             //VALIDADOR CONTRASEÑA
            bool simbolosPass = false;                          //VALIDADOR SQL INJECTION
            int contMayusPass = 0;
            int contNumPass = 0;

            bool validezUser = false;                             //VALIDADOR CONTRASEÑA
            bool simbolosUser = false;                          //VALIDADOR SQL INJECTION
            int contMayusUser = 0;
            int contNumUser = 0;


            ///////////////////COMPROBACIÓN CONTRASEÑA///////////////////
            if (password.Length > 6)
            {                       //COMPRUEBA LONGITUD MÍNIMA
                for (int i = 0; i < password.Length; i++)
                {
                    if (char.IsUpper(password[i]))
                    {         //COMPRUEBA SI CONTIENE MAYÚSCULAS
                        contMayusPass++;
                    }
                    if (char.IsNumber(password[i]))
                    {        //COMPRUEBA SI CONTIENE NÚMEROS
                        contNumPass++;
                    }
                    if (char.IsSymbol(password[i]))         //COMPROBACIÓN DE CARACTERES EXTRAÑOS (SQL INJECTION)
                    {
                        simbolosPass = true;
                    }
                    if (char.IsPunctuation(password[i]))    //COMPROBACIÓN DE CARACTERES EXTRAÑOS (SQL INJECTION)
                    {
                        simbolosPass = true;
                    }

                }
                if (contMayusPass > 0 && contNumPass > 0 && simbolosPass == false)
                {
                    validezPass = true;
                    MessageBox.Show("PASS " + validezPass);

                }
            }


            ///////////////////COMPROBACIÓN USUARIO///////////////////
            if (usuario.Length > 5)
            {                       //COMPRUEBA LONGITUD MÍNIMA
                for (int i = 0; i < usuario.Length; i++)
                {

                    if (char.IsSymbol(usuario[i]))         //COMPROBACIÓN DE CARACTERES EXTRAÑOS (SQL INJECTION)
                    {
                        simbolosUser = true;
                    }
                    if (char.IsPunctuation(usuario[i]))    //COMPROBACIÓN DE CARACTERES EXTRAÑOS (SQL INJECTION)
                    {
                        simbolosUser = true;
                    }
                }
                if (simbolosUser == false)
                {
                    validezUser = true;
                    MessageBox.Show("USER " + validezUser);
                }


            }
            // MessageBox.Show("VALIDEZ USUARIO-> "+validezUser+ "\tPASSWORD-> "+validezPass);

            if (validezUser && validezPass)      //COMPROBAMOS QUE HAYAN PASADO LA PRUEBA USUARIO Y CONTRASEÑA
            {
                validador = true;
            }

            return validador;
        }






        /// <summary>
        ///     Método para Comprobar los Permisos de un Usuario
        /// </summary>
        /// <returns>
        ///     Devuelve Si Un Usuario es Admin
        /// </returns>

        public bool CompruebaPermisos()
        {
            return permisos;                                        //DEVUELVE SI USUARIO ES ADMINISTRADOR
        }






        /// <summary>
        ///     Método para insertar JUGADOR
        /// </summary>
        /// <param name="idJugador">
        ///     Recibimos idJugador
        /// </param>
        /// <param name="nombre">
        ///     Recibimos nombre jugador
        /// </param>
        /// <param name="idEquipoOriginal">
        ///     Recibimos id Equipo
        /// </param>
        /// <param name="escudo">
        ///     Recibimos escudo equipo original
        /// </param>
        /// <param name="edad">
        ///     Recibimos edad
        /// </param>
        /// <param name="foto">
        ///     Recibimos foto jugador
        /// </param>
        /// <param name="convocable">
        ///     Recibimos si el jugador es convocable al momento de la inserción
        /// </param>
        /// <param name="dorsal">
        ///     Recibimos el dorsal del jugador
        /// </param>
        /// <param name="nacionalidad">
        ///     Recibimos nacionalidad del jugador
        /// </param>
        /// <param name="posicion">
        ///     Recibimos posición del jugador
        /// </param>
        /// <returns>
        ///     Devuelve un Booleano con el Resultado de la Operación
        ///     <see cref="bool"/>
        /// </returns>
        public bool InsertarJugador(int idJugador, string nombre, string idEquipoOriginal, string escudo, int edad, string foto, string convocable, string dorsal, string nacionalidad, string posicion)
        {
            bool conDisponible = false;
            conexion = null;

            try
            {
                if (conexion == null)
                {
                    connectionString = "Server=localhost;Database=11freaks;Uid=root;Pwd=CIFP1;";
                    conexion = new MySqlConnection(connectionString);
                    comando = new MySqlCommand("INSERT INTO jugadores VALUES ( "+idJugador+" , DEFAULT  , '" + nombre + "', DEFAULT , "+edad+", '"+foto+"' ,'"+convocable+ "',  DEFAULT  , '" + dorsal+"', '"+nacionalidad+"' , "+idEquipoOriginal+" , '"+posicion+"'); ", conexion);

                    conexion.Open();
                    MessageBox.Show("CONEXIÓN CREAR_EQUIPO " + conexion.State.ToString());
                    comando.ExecuteNonQuery();
                    conexion.Close();

                    MessageBox.Show(nombre + "HA SIDO DRAFTEADO EN LA LIGA !");
                    conDisponible = true;

                }
                else
                {
                    MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN COD1*");
                    conDisponible = false;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN COD2*\n" + ex.Message);
            }



            return conDisponible;
        }



    }
}
