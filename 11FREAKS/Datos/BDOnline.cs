using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Security.Cryptography;
using System.Security.Policy;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Data.SQLite;

namespace _11FREAKS.Datos
{
    /// <summary>
    /// Clase para la gestión de la Base de Datos (MySQLServer --> MODO ONLINE)
    /// </summary>
    public class BDOnline
    {
        //VARIABLES
        private MySqlConnection conexion;
        private MySqlCommand comando;
        private MySqlDataReader lector;
        //private string connectionString = "server=localhost;user=root;password=CIFP1;database=11freaks";
        private string connectionString = "Server=localhost;Database=11freaks;Uid=root;Pwd=CIFP1;";
        private string nomusuario, nomJugador;
        private string password;
        private bool permisos;
        private string equipo;
        private string correo;
        private string activo;
        private int idEquipo;
        private bool conDisponible=false;



        /// <summary>
        ///     Método para inicio de sesión de usuario
        /// </summary>
        /// <param name="usuario">
        ///     Recibimos Usuario del Login
        /// </param>
        /// <param name="contraseña">
        ///     Recibimos Contraseña del Usuario
        /// </param>
        /// <returns>
        ///     Devuelve un Booleano con el Resultado de la Operación
        ///     <see cref="bool"/>
        /// </returns>
        public bool ConectarServer(string usuario, string contraseña){

            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {

                string hash = String.Empty;


                using (SHA256 sha256 = SHA256.Create())         // Inicializamos a SHA256 la Contraseña de Login
                {
                    byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(contraseña));      //Hasheo pass

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

                    if (lector.HasRows)                                           //BUSCAMOS USUARIO
                    {
                        if (lector.Read())
                        {                                                         //ALMACENAMOS VARIABLES USUARIO
                            nomusuario = lector.GetString(0);
                            password = lector["Password"].ToString();
                            permisos = Boolean.Parse(lector["Permisos"].ToString());
                            correo = lector.GetString(3);
                            //ALMACENARÍA IMAGEN FOTO PERFIL
                            equipo = lector["Equipo"].ToString();
                            activo = lector.GetString(6);
                            idEquipo = lector.GetInt32(7);
                            conDisponible = true;
                        }
                    }
                    else
                    {                                                                          //SI NO EXISTE USUARIO...                                                                     
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
        public bool CrearUsuario(string usuario, string contraseña, string equipo, string correo, int idEquipo)
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

            try
            {
                conexion = null;
                if (conexion == null)
                {
                    connectionString = "Server=localhost;Database=11freaks;Uid=root;Pwd=CIFP1;";
                    conexion = new MySqlConnection(connectionString);
                    comando = new MySqlCommand("INSERT INTO usuarios VALUES ( '" + usuario + "' , '" + hash + "', 'false', '" + correo + "', NULL , '" + equipo + "', 'true', "+idEquipo+"); ", conexion);
                    
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
        ///     Método para crear Usuario (ADMIN) --> Encriptamos Contraseña
        /// </summary>
        /// <param name="usuario">
        ///     Recibimos Usuario
        /// </param>
        /// <param name="contraseña">
        ///     Recibimos Contraseña del Usuario
        /// </param>
        /// <param name="correo">
        ///     Recibimos correo del Admin
        /// </param>
        /// <returns>
        ///     Devuelve un Booleano con el Resultado de la Operación
        ///     <see cref="bool"/>
        /// </returns>
        public bool CrearUsuario(string usuario, string contraseña, string correo)
        {
            bool conDisponible = false;

            //////// HASHEO PASSWORD ////////
            string hash = String.Empty;

            using (SHA256 sha256 = SHA256.Create())         // Inicializamos SHA256
            {
                byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(contraseña));  //Hasheamos Password

                foreach (byte b in hashValue)   //Convertimos el Hash de Byte[] -> String
                {
                    hash += $"{b:X2}";
                }
            }

            try
            {
                conexion = null;
                if (conexion == null)
                {
                    connectionString = "Server=localhost;Database=11freaks;Uid=root;Pwd=CIFP1;";
                    conexion = new MySqlConnection(connectionString);
                    comando = new MySqlCommand("INSERT INTO usuarios VALUES ( '" + usuario + "' , '" + hash + "', 'true', '" + correo + "', NULL , NULL, DEFAULT, NULL);", conexion);

                    conexion.Open();
                    comando.ExecuteNonQuery();
                    conexion.Close();

                    MessageBox.Show("BIENVENIDO " + usuario + ", AHORA FORMAS PARTE DE 11FREAKS COMO ADMIN!");
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
        public void CrearEquipo(string equipo, string abreviatura, string liga)
        {
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

                }
                else
                {
                    MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN COD1*");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN COD2*\n" + ex.Message);
            }

        }







        /// <summary>
        ///     Método para actualizar los registros tras partido
        /// </summary>
        /// <param name="resultado">
        ///     Recibimos Usuario
        /// </param>
        /// <param name="golesAFavor">
        ///     Recibimos Contraseña del Usuario
        /// </param>
        /// <param name="golesEnContra">
        ///     Recibimos correo del Admin
        /// </param>
        public void ResultadosPartido(int resultado, int golesAFavor, int golesEnContra)
        {
            if (conexion != null)       //COMPROBAMOS LA CONEXIÓN
            {
                conexion.Close();
                conexion = null;
            }

            switch (resultado)
            {
                case 0:         //EMPATES
                    try
                    {
                        if (conexion == null)
                        {
                            conexion = new MySqlConnection(connectionString);
                            comando = new MySqlCommand("UPDATE equipos SET Empates=Empates +1, Puntos=Puntos+1 , GolesAFavor=GolesAFavor+" + golesAFavor + " , GolesEnContra=GolesEnContra+ " + golesEnContra + "  WHERE idEquipo='" + idEquipo + "'", conexion);

                            conexion.Open();                                                                         //CARGAMOS TODOS LOS DATOS ACTUALIZADOSs
                            comando.ExecuteNonQuery();
                            conexion.Close();
                        }
                        else
                        {
                            MessageBox.Show("*NO SE PUDO COMPLETAR LA OPERACIÓN*");
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN*\n" + ex.Message);
                    }
                    break;

                case 1:         //VICTORIA
                    try
                    {
                        if (conexion == null)
                        {
                            conexion = new MySqlConnection(connectionString);
                            comando = new MySqlCommand("UPDATE equipos SET Victorias=Victorias +1 , Puntos=Puntos+3, GolesAFavor=GolesAFavor+" + golesAFavor + " , GolesEnContra=GolesEnContra+ "+golesEnContra+"  WHERE idEquipo='" + idEquipo + "'", conexion);

                            conexion.Open();                                                                         //CARGAMOS TODOS LOS DATOS ACTUALIZADOSs
                            comando.ExecuteNonQuery();
                            conexion.Close();
                        }
                        else
                        {
                            MessageBox.Show("*NO SE PUDO COMPLETAR LA OPERACIÓN*");
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN*\n" + ex.Message);
                    }
                    break; 
                
                case 2:        //DERROTA
                    try
                    {
                        if (conexion == null)
                        {
                            conexion = new MySqlConnection(connectionString);
                            comando = new MySqlCommand("UPDATE equipos SET Derrotas=Derrotas+1 , GolesAFavor=GolesAFavor+" + golesAFavor + " , GolesEnContra=GolesEnContra+ " + golesEnContra + "  WHERE idEquipo='" + idEquipo + "'", conexion);

                            conexion.Open();                                                                         //CARGAMOS TODOS LOS DATOS ACTUALIZADOSs
                            comando.ExecuteNonQuery();
                            conexion.Close();
                        }
                        else
                        {
                            MessageBox.Show("*NO SE PUDO COMPLETAR LA OPERACIÓN*");
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN*\n" + ex.Message);
                    }
                    break;
            }

        }






        /// <summary>
        ///     Método para actualizar la clasificación de la liga
        /// </summary>
        public void ActualizacionClasificacion()
        {

            ArrayList equiposOrdenados= new ArrayList();
            equiposOrdenados = DevuelveEquiposLiga("-1");                   //CONSEGUIMOS EQUIPOS ORDENADOS POR PUNTOS


            for (int i = 0; i < equiposOrdenados.Count; i++)        //CAMBIARÍA ID LIGA
            {
                var equipo =(Equipo)equiposOrdenados[i];           //OBTENEMOS CADA EQUIPO POR ORDEN

                if (conexion != null)       //COMPROBAMOS LA CONEXIÓN
                {
                    conexion.Close();
                    conexion = null;
                }

                try
                {
                    if (conexion == null)
                    {
                        conexion = new MySqlConnection(connectionString);
                        comando = new MySqlCommand($"UPDATE equipos SET Posicion = {i + 1} WHERE idEquipo = {equipo.idEquipo}", conexion);      //ACTUALIZAMOS POSICIÓN DE CADA EQUIPO

                        conexion.Open();                                                                         
                        comando.ExecuteNonQuery();
                        conexion.Close();
                    }
                    else
                    {
                        MessageBox.Show("*NO SE PUDO COMPLETAR LA OPERACIÓN*");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN*\n" + ex.Message);
                }
            }

        }





        /// <summary>
        ///     Método para cambiar contraseña de un usuario
        /// </summary>
        /// <param name="nuevaPass">
        ///     Recibimos la nueva contraseña del usuario
        /// </param>
        public void CambiarContraseña(string nuevaPass)
        {
            if (conexion != null)       //COMPROBAMOS LA CONEXIÓN
            {
                conexion.Close();
                conexion = null;
            }

            //////// HASHEO PASSWORD ////////
            string hash = String.Empty;

            using (SHA256 sha256 = SHA256.Create())         // Inicializamos SHA256
            {
                byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(nuevaPass));  //Hasheamos Password

                foreach (byte b in hashValue) //Convertimos el Hash de Byte[] -> String
                {
                    hash += $"{b:X2}";
                }
            }

            try
            {
                if (conexion == null)
                {
                    conexion = new MySqlConnection(connectionString);
                    comando = new MySqlCommand("UPDATE usuarios SET Password='" + hash + "' WHERE Usuario='" + nomusuario + "'", conexion);

                    conexion.Open();                                                                         //CARGAMOS TODOS LOS DATOS ACTUALIZADOSs
                    comando.ExecuteNonQuery();
                    conexion.Close();

                    MessageBox.Show("HAS CAMBIADO TU CONTRASEÑA");

                }
                else
                {
                    MessageBox.Show("*NO SE PUDO COMPLETAR LA OPERACIÓN*");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN*\n" + ex.Message);
            }
        }







        /// <summary>
        ///     Método para cambiar email asociado de un usuario
        /// </summary>
        /// <param name="nuevoCorreo">
        ///     Recibimos el nuevo correo contraseña del usuario
        /// </param>
        public void CambiarCorreo(string usuario, string nuevoCorreo)
        {
            if (conexion != null)       //COMPROBAMOS LA CONEXIÓN
            {
                conexion.Close();
                conexion = null;
            }
            try
            {
                if (conexion == null)
                {
                    conexion = new MySqlConnection(connectionString);
                    comando = new MySqlCommand("UPDATE usuarios SET Email= '" + nuevoCorreo + "' WHERE Usuario='" + usuario + "'", conexion);

                    conexion.Open();                                                                         //REALIZAMOS OPERACIÓN
                    comando.ExecuteNonQuery();
                    conexion.Close();

                    MessageBox.Show("SE HA ACTUALIZADO EL CORREO DE SU CUENTA " + usuario);

                }
                else
                {
                    MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN*");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN*\n" + ex.Message);
            }

        }


        /// <summary>
        ///     Método para otorgar permisos a un usuario
        /// </summary>
        /// <param name="usuario">
        ///     Recibimos Usuario
        /// </param>
        public void DarPermisos(string usuario)
        {
            if (conexion != null)       //COMPROBAMOS LA CONEXIÓN
            {
                conexion.Close();
                conexion = null;
            }
            try
            {
                if (conexion == null)
                {
                    conexion = new MySqlConnection(connectionString);
                    comando = new MySqlCommand("UPDATE usuarios SET Permisos= 'true' WHERE Usuario='" + usuario + "'", conexion);

                    conexion.Open();                                                                         //CARGAMOS TODOS LOS DATOS ACTUALIZADOS
                    comando.ExecuteNonQuery();
                    conexion.Close();

                    MessageBox.Show("SE HAN OTORGADO PERMISOS DE ADMIN A " + usuario);

                }
                else
                {
                    MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN*");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN*\n" + ex.Message);
            }

        }

















        /// <summary>
        ///     Método para obtener Email actual
        /// </summary>
        /// <returns>
        ///     Devuelve Nombre Email actual
        /// </returns>
        public string DevuelveCorreo()
        {
            return correo;
        }


        /// <summary>
        ///     Método para obtener Usuario actual
        /// </summary>
        /// <returns>
        ///     Devuelve Usuario actual
        /// </returns>
        public string DevuelveUsuario()
        {
            return nomusuario;
        }



        /// <summary>
        ///     Método para consultar el estado de la cuenta
        /// </summary>
        /// <returns>
        ///     Devuelve si el usuario ha sido la cuenta está activa
        /// </returns>
        public string DevuelveActivo()
        {
            return activo;
        }



        /// <summary>
        ///     Método para consultar idEquipo del usuario actual
        /// </summary>
        /// <returns>
        ///     Devuelve idEquipo del usuario actual
        /// </returns>
        public int DevuelveIdEquipo()
        {
            return idEquipo;
        }








        /// <summary>
        ///     Método para devolver idEquipo de un Equipo dado su nombre
        /// </summary>
        /// <param name="nombreEquipo">
        ///     Recibimos Usuario
        /// </param>
        /// <returns>
        ///     Devuelve idEquipo de un Equipo
        ///     <see cref="int"/>
        /// </returns>
        public int DevuelveIdEquipo(string nombreEquipo)
        {
           
            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {

                try
                {
                    conexion.Open();
                    comando = new MySqlCommand("SELECT idEquipo FROM equipos WHERE Nombre='" + nombreEquipo + "'", conexion);
                    lector = comando.ExecuteReader();

                    if (lector.HasRows)                                                              //BUSCAMOS USUARIO
                    {
                        if (lector.Read())
                        {                                                         //DEVOLVEMOS IDEQUIPO DEL USUARIO INTRODUCIDO POR TECLADO
                            idEquipo = lector.GetInt32(0);;
                        }
                    }
                    else
                    {                                                                          //SI NO EXISTE USUARIO...s                                                                     
                        MessageBox.Show("NO SE ENCONTRARON REGISTROS");
                    }

                    lector.Close();
                    conexion.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN " + ex.Message);
                }

            }//FIN USING 
            return idEquipo;
        }





        /// <summary>
        ///     Método para devolver la url del recurso asociado a un evento
        /// </summary>
        /// <param name="idEvento">
        ///     Recibimos Usuario
        /// </param>
        /// <returns>
        ///     Devuelve url del recurso asociado
        ///     <see cref="string"/>
        /// </returns>
        public string DevuelveRecursoEvento(int idEvento)
        {
            string urlRecurso=string.Empty;

            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {

                try
                {
                    conexion.Open();
                    comando = new MySqlCommand("SELECT Recurso FROM eventos WHERE idTipoEvento=" + idEvento, conexion);
                    lector = comando.ExecuteReader();

                    if (lector.HasRows)                                                              //BUSCAMOS USUARIO
                    {
                        if (lector.Read())
                        {                                                         //DEVOLVEMOS IDEQUIPO DEL USUARIO INTRODUCIDO POR TECLADO
                            urlRecurso = lector.GetString(0);
                        }
                    }
                    else
                    {                                                                          //SI NO EXISTE USUARIO...s                                                                     
                        MessageBox.Show("NO SE ENCONTRARON REGISTROS");
                    }

                    lector.Close();
                    conexion.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN " + ex.Message);
                }

            }//FIN USING 
            return urlRecurso;
        }










        /// <summary>
        ///     Método para Comprobar que la Contraseña es Segura 
        /// </summary>
        /// <param name="password">
        ///     Recibimos la Contraseña del Usuario
        /// </param>
        /// <returns>
        ///     Devuelve un Booleano con el Resultado de la Operación
        ///     <see cref="bool"/>
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
                    //MessageBox.Show("PASS " + validezPass);
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
                    //MessageBox.Show("USER " + validezUser);
                }


            }

            if (validezUser && validezPass)      //COMPROBAMOS QUE HAYAN PASADO LA PRUEBA USUARIO Y CONTRASEÑA
            {
                validador = true;
            }

            return validador;
        }








        /// <summary>
        ///     Función Para Mostrar Todos los Usuarios Registrados
        /// </summary>
        ///<returns>
        ///     Devuelve un ArrayList con todos los Usuario Registrados
        ///     <see cref="ArrayList"/>
        /// </returns>
        public ArrayList ConsultaUsuarios()                             //MÉTODO CONEXIÓN BBDD SQLITE
        {
            ArrayList listaUsuarios = new ArrayList();                    //INICIALIZAMOS LISTA CADA VEZ QUE LLAMEMOS AL MÉTODO
            string userName = string.Empty;
            bool permisosUser;

            if (conexion != null)                                       //COMPROBAMOS LA CONEXIÓN
            {
                conexion.Close();
                conexion = null;
            }

            try
            {

                if (conexion == null)
                {
                    conexion = new MySqlConnection(connectionString);
                    comando = new MySqlCommand("SELECT * FROM Usuarios ", conexion);

                    conexion.Open();
                    lector = comando.ExecuteReader();
                    if (lector.HasRows)                                                             //BUSCAMOS USUARIO
                    {
                        while (lector.Read())
                        {                                                         //COMPROBAMOS USUARIO Y SI ES ADMIN
                            userName = lector.GetString(0);
                            permisosUser = Boolean.Parse(lector["Permisos"].ToString());

                            if (permisosUser == true)
                            {
                                listaUsuarios.Add("#" + userName);              //PONEMOS HASHTAG PARA INDICAR QUE ES ADMIN
                            }
                            else
                            {
                                listaUsuarios.Add(userName);
                            }

                        }
                    }
                    else
                    {                                                                          //SI NO EXISTE USUARIO...s                                                                     
                        MessageBox.Show("NO SE ENCONTRARON REGISTROS");
                    }

                    lector.Close();
                    conexion.Close();

                }
                else
                {
                    MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN*");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN*\n" + ex.Message);
            }

            return listaUsuarios;

        }









        /// <summary>
        ///     Función Para Mostrar Todos los Usuarios Registrados
        /// </summary>
        ///<returns>
        ///     Devuelve un ArrayList con todos los Usuario Registrados
        ///     <see cref="ArrayList"/>
        /// </returns>
        public ArrayList DevuelveJugadoresEquipo(int idEquipo)                  
        {
            ArrayList listaJugadores = new ArrayList();                    //INICIALIZAMOS LISTA CADA VEZ QUE LLAMEMOS AL MÉTODO

            if (conexion != null)                                       //COMPROBAMOS LA CONEXIÓN
            {
                conexion.Close();
                conexion = null;
            }
            
            try
            {

                if (conexion == null)
                {
                    conexion = new MySqlConnection(connectionString);
                    comando = new MySqlCommand("SELECT * FROM jugadores WHERE idEquipo= "+idEquipo+";", conexion);

                    conexion.Open();
                    lector = comando.ExecuteReader();
                    if (lector.HasRows)                                                             //BUSCAMOS JUGADORES
                    {
                        while (lector.Read())                                                       //OBTENEMOS DATOS JUGADOR
                        {
                            Jugador jugador = new Jugador(
                                lector.GetInt32(0),
                                lector.GetInt32(1),
                                lector.GetString(2),
                                lector.GetInt32(3),
                                lector.GetInt32(4),
                                lector.GetString(5),
                                lector.GetString(6),
                                lector.GetString(7),
                                lector.GetString(8),
                                lector.GetString(9),
                                lector.GetInt32(10),
                                lector.GetString(11)
                            );

                            listaJugadores.Add( jugador );                                          //AÑADIMOS JUGADOR A LA LISTA

                        }
                    }
                    else
                    {                                                                          //SI NO EXISTEN JUGADORES...                                                                     
                        MessageBox.Show("NO SE ENCONTRARON REGISTROS");
                    }

                    lector.Close();
                    conexion.Close();
                }
                else
                {
                    MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN*");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN*\n" + ex.Message);
            }

            return listaJugadores;

        }





        /// <summary>
        ///     Función Para Mostrar Todos los Usuarios Registrados
        /// </summary>
        ///<returns>
        ///     Devuelve un ArrayList con todos los Usuario Registrados
        ///     <see cref="ArrayList"/>
        /// </returns>
        public ArrayList DevuelveJugadoresLiga(string liga)
        {
            ArrayList listaJugadores = new ArrayList();                    //INICIALIZAMOS LISTA CADA VEZ QUE LLAMEMOS AL MÉTODO

            if (conexion != null)                                       //COMPROBAMOS LA CONEXIÓN
            {
                conexion.Close();
                conexion = null;
            }

            try
            {
                if (conexion == null)
                {
                    conexion = new MySqlConnection(connectionString);
                    comando = new MySqlCommand("SELECT * FROM usuarios WHERE idEquipo!=null ;", conexion);     //CAMBIAR CUANDO EXISTAN MÁS LIGAS

                    conexion.Open();
                    lector = comando.ExecuteReader();
                    if (lector.HasRows)                                                             //BUSCAMOS USAURIO
                    {
                        while (lector.Read())                                                       //OBTENEMOS DATOS USAURIO
                        {
                            Usuarios usuario = new Usuarios(
                                lector.GetString(0),
                                lector.GetString(1),
                                lector.GetString(2),
                                lector.GetString(3),
                                lector.GetString(4),
                                lector.GetString(5),
                                lector.GetString(6),
                                lector.GetInt32(7)
                            );

                            listaJugadores.Add(usuario);                                          //AÑADIMOS USAURIO A LA LISTA

                        }
                    }
                    else
                    {                                                                          //SI NO EXISTEN USAURIOS...                                                                     
                        MessageBox.Show("NO SE ENCONTRARON REGISTROS");
                    }

                    lector.Close();
                    conexion.Close();
                }
                else
                {
                    MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN*");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN*\n" + ex.Message);
            }

            return listaJugadores;
        }






        /// <summary>
        ///     Función Para Devolver Todos los Equipos de una misma Liga
        /// </summary>
        ///<returns>
        ///     Devuelve un ArrayList con todos los Equipos de una liga
        ///     <see cref="ArrayList"/>
        /// </returns>
        public ArrayList DevuelveEquiposLiga(string liga)
        {
            ArrayList listaEquipos = new ArrayList();                    //INICIALIZAMOS LISTA CADA VEZ QUE LLAMEMOS AL MÉTODO

            if (conexion != null)                                       //COMPROBAMOS LA CONEXIÓN
            {
                conexion.Close();
                conexion = null;
            }

            try
            {
                if (conexion == null)
                {
                    conexion = new MySqlConnection(connectionString);
                    comando = new MySqlCommand("SELECT * FROM equipos WHERE idLiga=1 ;", conexion);     //CAMBIAR CUANDO EXISTAN MÁS LIGAS

                    conexion.Open();
                    lector = comando.ExecuteReader();
                    if (lector.HasRows)                                                             //BUSCAMOS EQUIPO
                    {
                        while (lector.Read())                                                       //OBTENEMOS DATOS EQUIPO
                        {
                            Equipo equipo = new Equipo(
                                lector.GetInt32(0),
                                lector.GetString(1),
                                lector.GetString(2),
                                lector.GetString(3),
                                lector.GetInt32(4),
                                lector.GetInt32(5),
                                lector.GetInt32(6),
                                lector.GetInt32(7),
                                lector.GetInt32(8),
                                lector.GetInt32(9)
                            );

                            listaEquipos.Add(equipo);                                          //AÑADIMOS EQUIPO A LA LISTA

                        }
                    }
                    else
                    {                                                                          //SI NO EXISTEN USAURIOS...                                                                     
                        MessageBox.Show("NO SE ENCONTRARON REGISTROS");
                    }

                    lector.Close();
                    conexion.Close();

                    var equiposOrdenados = listaEquipos.Cast<Equipo>()                          //ORDENA EQUIPOS SEGÚN SU PUNTUACIÓN
                    .OrderByDescending(e => e.Puntos)
                    .ToList();

                    listaEquipos = new ArrayList(equiposOrdenados);
                }
                else
                {
                    MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN*");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN*\n" + ex.Message);
            }

            return listaEquipos;
        }



        /// <summary>
        ///     Función Para Devolver Jugadores Trasferibles para el Mercado de Traspasos
        /// </summary>
        ///<returns>
        ///     Devuelve un ArrayList con todos los jugadores que la liga
        ///     <see cref="ArrayList"/>
        /// </returns>
        public ArrayList DevuelveTransferibles(int numMaximo,string liga)
        {
            ArrayList listaTransferibles = new ArrayList();             //INICIALIZAMOS LISTA CADA VEZ QUE LLAMEMOS AL MÉTODO
            int numTransferiblesMax = numMaximo;
            int cont = 0;

            if (conexion != null)                                       //COMPROBAMOS LA CONEXIÓN
            {
                conexion.Close();
                conexion = null;
            }

            try
            {
                if (conexion == null)
                {
                    conexion = new MySqlConnection(connectionString);
                    comando = new MySqlCommand("SELECT * FROM jugadores WHERE idEquipo=-1  ;", conexion);     //CAMBIAR CUANDO EXISTAN MÁS LIGAS

                    conexion.Open();
                    lector = comando.ExecuteReader();
                    if (lector.HasRows)                                                             //BUSCAMOS JUGADOR
                    {
                        while (lector.Read() && cont<=20)                                           //OBTENEMOS DATOS JUGADOR
                        {
                            Jugador jugador = new Jugador(
                                lector.GetInt32(0),
                                lector.GetInt32(1),
                                lector.GetString(2),
                                lector.GetInt32(3),
                                lector.GetInt32(4),
                                lector.GetString(5),
                                lector.GetString(6),
                                lector.GetString(7),
                                lector.GetString(8),
                                lector.GetString(9),
                                lector.GetInt32(10),
                                lector.GetString(11)
                            );


                            listaTransferibles.Add(jugador);                                        //AÑADIMOS JUGADOR A LA LISTA
                            cont++;
                        }
                    }
                    else
                    {                                                                          //SI NO EXISTEN JUGADORES...                                                                     
                        MessageBox.Show("NO SE ENCONTRARON REGISTROS");
                    }

                    lector.Close();
                    conexion.Close();

                    /*var equiposOrdenados = listaEquipos.Cast<Equipo>()                          //ORDENA EQUIPOS SEGÚN SU PUNTUACIÓN
                    .OrderByDescending(e => e.Puntos)
                    .ToList();*/

                    //listaEquipos = new ArrayList(equiposOrdenados);
                }
                else
                {
                    MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN*");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN*\n" + ex.Message);
            }

            return listaTransferibles;
        }







        /// <summary>
        ///     Función Para Borrar Usuario
        /// </summary>
        /// <param name="usuario">
        ///     Recibimos Usuario  Para Hacer Borrado
        /// </param>
        public void BorrarUsuario(string usuario)
        {

            try
            {
                conexion = null;
                if (conexion == null)
                {
                    conexion = new MySqlConnection(connectionString);
                    comando = new MySqlCommand("DELETE from usuarios WHERE Usuario = '" + usuario + "' ; ", conexion);

                    conexion.Open();
                    comando.ExecuteNonQuery().ToString();         
                    conexion.Close();

                    MessageBox.Show(usuario + " , HA SIDO ELIMINADO");
                }
                else
                {
                    MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN COD1*");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN COD2*\n" + ex.Message);
            }

        }





        /// <summary>
        ///     Método para obtener Email de un tercero
        /// </summary>
        /// <returns>
        ///     Devuelve Nombre Email de un tercero
        /// </returns>
        public string DevuelveCorreo(string usuario)
        {
            string correo = null;

            if (conexion != null)       //COMPROBAMOS LA CONEXIÓN
            {
                conexion.Close();
                conexion = null;
            }
            try
            {
                if (conexion == null)
                {
                    conexion = new MySqlConnection(connectionString);
                    comando = new MySqlCommand("SELECT * FROM usuarios WHERE Usuario='" + usuario + "'", conexion);

                    conexion.Open();
                    lector = comando.ExecuteReader();
                    if (lector.HasRows)                                                             //BUSCAMOS USUARIO
                    {
                        if (lector.Read())
                        {                                                         //OBTENEMOS CAMPO DESEADO
                            correo = lector.GetString(3);
                        }
                    }
                    else
                    {                                                                          //SI NO EXISTE USUARIO...s                                                                     
                        MessageBox.Show("NO SE ENCONTRARON REGISTROS");
                    }

                    lector.Close();
                    conexion.Close();

                }
                else
                {
                    MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN*");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN*\n" + ex.Message);
            }
            return correo;
        }





        /// <summary>
        ///     Función Para Devolver Objeto Equipo para su mejor manejo
        /// </summary>
        ///<returns>
        ///     Devuelve un Objeto Equipo con todos los atributos que contiene la BD
        ///     <see cref="Equipo"/>
        /// </returns>
        public Equipo DevuelveEquipo()
        {
            Equipo equipo=null;

            if (conexion != null)                                       //COMPROBAMOS LA CONEXIÓN
            {
                conexion.Close();
                conexion = null;
            }

            try
            {
                if (conexion == null)
                {
                    conexion = new MySqlConnection(connectionString);
                    comando = new MySqlCommand("SELECT * FROM equipos WHERE idEquipo="+idEquipo+";", conexion);     //CAMBIAR CUANDO EXISTAN MÁS LIGAS

                    conexion.Open();
                    lector = comando.ExecuteReader();
                    if (lector.HasRows)                                                             //BUSCAMOS EQUIPO
                    {
                        while (lector.Read())                                                       //OBTENEMOS DATOS EQUIPO
                        {
                            equipo = new Equipo(
                                lector.GetInt32(0),
                                lector.GetString(1),
                                lector.GetString(2),
                                lector.GetString(3),
                                lector.GetInt32(4),
                                lector.GetInt32(5),
                                lector.GetInt32(6),
                                lector.GetInt32(7),
                                lector.GetInt32(8),
                                lector.GetInt32(9)
                            );
                        }
                    }
                    else
                    {                                                                          //SI NO EXISTEN EQUIPOS...                                                                     
                        MessageBox.Show("NO SE ENCONTRARON REGISTROS");
                    }

                    lector.Close();
                    conexion.Close();
                }
                else
                {
                    MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN*");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN*\n" + ex.Message);
            }

            return equipo;
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





        /// <summary>
        ///     Método para generar equipos de forma aleatoria para cada jugador
        /// </summary>
        /// <param name="idEquipo">
        ///     Recibimos idEquipo del Usuario
        /// </param>
        public void GenerarEquipo(string idEquipo, string nomEquipo)
        {


            Random random = new Random();
            int cantJugadores = 25;
            int cont = 0;
            int contPOR = 0, contDFC=0, contMC=0, contDEL=0;
            int idMin = 20;
            int idMax = 386828;
            int idJug=-2;
            int idEq=-2;
            string nomJug = string.Empty, pos=string.Empty;

            int[] jugadores = new int[cantJugadores];
            for (int i = 0; i < jugadores.Length; i++)              //INICIALIZAMOS A 0 EL ARRAY DE JUGADORES
            {
                jugadores[i] = 0;
            }

            while (cont < cantJugadores)
            {

                int id= random.Next(idMin, idMax + 1);                        //GENERAMOS IDJUGADOR RANDOM

                try
                {
                    conexion = null;
                    if (conexion == null)
                    {
                        connectionString = "Server=localhost;Database=11freaks;Uid=root;Pwd=CIFP1;";
                        conexion = new MySqlConnection(connectionString);
                        comando = new MySqlCommand("SELECT idjugador, idEquipo, Nombre, Posicion FROM jugadores WHERE idjugador=" +id, conexion);

                        conexion.Open();
                        lector=comando.ExecuteReader();
                        
                        if (lector.HasRows)                                                              
                        {                                                               //BUSCAMOS IDJUGADOR
                            if (lector.Read())
                            {                                                           //ALMACENAMOS IDJUGADOR
                                idJug = lector.GetInt32(0);
                                idEq = lector.GetInt32(1);
                                nomJug = lector.GetString(2);
                                pos = lector.GetString(3);
                                //MessageBox.Show(idJug+ "\t"+nomJug+"\t"+idEq);
                                if (idEq == (-1))
                                {
                                    bool jugadorRepetido = false;

                                    for (int i = 0; i < jugadores.Length; i++)
                                    {
                                        if (jugadores[i] == idJug)  // COMPROBAMOS QUE EL JUGADOR NO HUBIERA SIDO SELECCIONADO ANTERIORMENTE
                                        {
                                            jugadorRepetido = true;
                                            break;
                                        }
                                    }

                                    if (!jugadorRepetido)
                                    {
                                        switch (pos)
                                        {
                                            case "Portero":
                                                if (contPOR < 3)
                                                {
                                                    contPOR++;
                                                    jugadores[cont] = idJug;  // ASIGNAR PORTERO AL JUGADOR
                                                    cont++;
                                                }
                                                break;
                                            case "Defensor":
                                                if (contDFC < 8)
                                                {
                                                    contDFC++;
                                                    jugadores[cont] = idJug;  // ASIGNAR DEFENSOR AL JUGADOR
                                                    cont++;
                                                }
                                                break;
                                            case "Medio":
                                                if (contMC < 8)
                                                {
                                                    contMC++;
                                                    jugadores[cont] = idJug;  // ASIGNAR MEDIO AL JUGADOR
                                                    cont++;
                                                }
                                                break;
                                            case "Atacante":
                                                if (contDEL < 6)
                                                {
                                                    contDEL++;
                                                    jugadores[cont] = idJug;  // ASIGNAR DELANTERO AL JUGADOR
                                                    cont++;
                                                }
                                                break;
                                        }
                                    }
                                }

                            
                            }

                        }
                        else
                        {                                                                          //SI NO EXISTE JUGADOR...                                                                     
                            //MessageBox.Show("NO SE ENCONTRARON JUGADORES");
                        }

                            conexion.Close();
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


            }//FIN WHILE

            MessageBox.Show("PORTEROS " + contPOR + "\tDEFENSORES " + contDFC + "\tMEDIOS " + contMC + "\tATACANTES " + contDEL);

            for (int i = 0; i < jugadores.Length; i++)
            {
                Traspaso(DevuelveIdEquipo(nomEquipo), jugadores[i]);           //TRASPASAMOS LOS JUGADORES A SU NUEVO EQUIPO

            }

        }




        /// <summary>
        ///     Método para devolver datos jugador dado su nombre (idJugador)
        /// </summary>
        /// <param name="idJugador">
        ///     Recibimos Usuario
        /// </param>
        /// <returns>
        ///     Devuelve nombre del jugador
        ///     <see cref="string"/>
        /// </returns>
        public string BuscarJugadorPorId(string idJugador)
        {

            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                try
                {
                    conexion.Open();
                    comando = new MySqlCommand("SELECT * FROM jugadores WHERE idjugador='" + idJugador + "'; ", conexion);
                    lector = comando.ExecuteReader();

                    if (lector.HasRows)                                                              //BUSCAMOS JUGADOR
                    {
                        if (lector.Read())
                        {                                                         //COMPROBAMOS SI HAY RESULTADOS
                            nomJugador = lector.GetString(2);
                         /*   password = lector["Password"].ToString();
                            permisos = Boolean.Parse(lector["Permisos"].ToString());
                            correo = lector.GetString(3);
                            //ALMACENARÍA IMAGEN FOTO PERFIL
                            equipo = lector["Equipo"].ToString();
                            activo = lector.GetString(6);
                            idEquipo = lector.GetInt32(7);*/
                        }
                    }
                    else
                    {                                                                          //SI NO EXISTE JUGADOR...                                                                     
                        MessageBox.Show("NO SE ENCONTRARON REGISTROS");
                    }

                    lector.Close();
                    conexion.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN " + ex.Message);
                }

            }//FIN USING 
            return nomJugador;
        }



        /// <summary>
        ///     Método para devolver presupuesto de un equipo
        /// </summary>
        /// <returns>
        ///     Devuelve presupuesto de un equipo
        ///     <see cref="int"/>
        /// </returns>
        public int DevuelvePresupuesto()
        {
            int presupuesto=0;
            
            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                try
                {
                    conexion.Open();
                    comando = new MySqlCommand("SELECT Presupuesto FROM equipos WHERE idEquipo='" + idEquipo + "'; ", conexion);
                    lector = comando.ExecuteReader();

                    if (lector.HasRows)                                                              //BUSCAMOS EQUIPO
                    {
                        if (lector.Read())
                        {                                                         //COMPROBAMOS SI HAY RESULTADOS
                            presupuesto = lector.GetInt32(0);
                        }
                    }
                    else
                    {                                                                          //SI NO EXISTE EQUIPO...                                                                     
                        MessageBox.Show("NO SE ENCONTRARON REGISTROS");
                    }

                    lector.Close();
                    conexion.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN " + ex.Message);
                }

            }//FIN USING 
            return presupuesto;
        }








        /// <summary>
        ///     Método para hacer traspaso jugador (CREACIÓN EQUIPO)
        /// </summary>
        /// <param name="idEquipo">
        ///     Recibimos id Equipo Destino
        /// </param>
        /// <param name="idJugador">
        ///     Recibimos id Jugador
        /// </param>
        public void Traspaso (int idEquipo, int idJugador)
        {
            try
            {
                conexion = null;
                if (conexion == null)
                {
                    connectionString = "Server=localhost;Database=11freaks;Uid=root;Pwd=CIFP1;";
                    conexion = new MySqlConnection(connectionString);
                    comando = new MySqlCommand("UPDATE jugadores SET idEquipo="+idEquipo+" WHERE idjugador="+idJugador+"; ", conexion);

                    conexion.Open();
                    comando.ExecuteNonQuery();
                    conexion.Close();

                    MessageBox.Show(BuscarJugadorPorId(idJugador.ToString()) + " HA SIDO TRASPASADO");
                    
                }
                else
                {
                    MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN COD1*");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN COD2*\n" + ex.Message);
            }

        }





        /// <summary>
        ///     Método para hacer traspaso jugador (MERCADO FICHAJES)
        /// </summary>
        /// <param name="precio">
        ///     Recibimos valor del jugador
        /// </param>
        /// <param name="idEquipoActual">
        ///     Recibimos id Equipo Actual
        /// </param>
        /// <param name="idEquipoDestino">
        ///     Recibimos id Equipo Destino
        /// </param>
        /// <param name="idJugador">
        ///     Recibimos id Jugador
        /// </param>
        public void Traspaso(int precio ,int idEquipoActual, int idEquipoDestino, int idJugador)
        {


            if (DevuelvePresupuesto() >precio)              //COMPROBAMOS QUE EL EQUIPO PUEDE HACER FRENTE AL FICHAJE
            {
                try
                {
                    conexion = null;
                    if (conexion == null)
                    {
                        connectionString = "Server=localhost;Database=11freaks;Uid=root;Pwd=CIFP1;";
                        conexion = new MySqlConnection(connectionString);
                        comando = new MySqlCommand("UPDATE equipos SET Presupuesto=Presupuesto-" +precio+ " WHERE idEquipo=" + idEquipoDestino + "; ", conexion);

                        conexion.Open();
                        comando.ExecuteNonQuery();
                        conexion.Close();

                        MessageBox.Show("CLÁSULA PAGADA");
                    }
                    else
                    {
                        MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN PAGO CLÁUSULA*");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN COD2*\n" + ex.Message);
                }



                try
                {
                    conexion = null;
                    if (conexion == null)
                    {
                        connectionString = "Server=localhost;Database=11freaks;Uid=root;Pwd=CIFP1;";
                        conexion = new MySqlConnection(connectionString);
                        comando = new MySqlCommand("UPDATE jugadores SET idEquipo=" + idEquipoDestino + " WHERE idjugador=" + idJugador + " AND idEquipo=" + idEquipoActual + "; ", conexion);

                        conexion.Open();
                        comando.ExecuteNonQuery();
                        conexion.Close();

                        MessageBox.Show(BuscarJugadorPorId(idJugador.ToString()).ToUpper() + " HA SIDO TRASPASADO");
                    }
                    else
                    {
                        MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN COD1*");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN COD2*\n" + ex.Message);
                }


            }
            else
            {
                MessageBox.Show("NO TIENES EL SUFICIENTE PRESUPUESTO PARA AFRONTAR ESTE FICHAJE");
            }





        }




        /// <summary>
        ///     Método para hacer venta jugador
        /// </summary>
        /// <param name="idEquipo">
        ///     Recibimos id Equipo
        /// </param>
        /// <param name="idJugador">
        ///     Recibimos id Jugador
        /// </param>
        /// <param name="precio">
        ///     Recibimos valor de venta del jugador
        /// </param>
        public void VenderJugador(int idEquipo, int idJugador, int precio)
        {
            try
            {
                conexion = null;
                if (conexion == null)
                {
                    connectionString = "Server=localhost;Database=11freaks;Uid=root;Pwd=CIFP1;";
                    conexion = new MySqlConnection(connectionString);
                    comando = new MySqlCommand("UPDATE jugadores SET idEquipo=-1  WHERE idjugador=" + idJugador + " AND idEquipo="+idEquipo+"; ", conexion);

                    conexion.Open();
                    comando.ExecuteNonQuery();
                    conexion.Close();

                    MessageBox.Show(BuscarJugadorPorId(idJugador.ToString()) + " HA QUEDADO LIBRE");
                }
                else
                {
                    MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN COD1*");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN COD2*\n" + ex.Message);
            }




            try
            {                                                       //EQUIPO OBTIENE DINERO POR LA VENTA
                conexion = null;
                if (conexion == null)
                {
                    connectionString = "Server=localhost;Database=11freaks;Uid=root;Pwd=CIFP1;";
                    conexion = new MySqlConnection(connectionString);
                    comando = new MySqlCommand("UPDATE equipos SET Presupuesto+Presupuesto-" + precio + " WHERE idEquipo=" + idEquipo + "; ", conexion);

                    conexion.Open();
                    comando.ExecuteNonQuery();
                    conexion.Close();

                    MessageBox.Show("CLÁSULA RECIBIDA");
                }
                else
                {
                    MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN PAGO CLÁUSULA*");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN COD2*\n" + ex.Message);
            }


        }
    }
}
