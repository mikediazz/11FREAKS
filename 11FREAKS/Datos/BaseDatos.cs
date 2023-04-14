using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Collections;
using System.Security.Cryptography;

namespace _11FREAKS.Datos
{

    public class BaseDatos
    {
        private SQLiteConnection conexion;
        private SQLiteCommand comando;
        private SQLiteConnection conexion2;
        private SQLiteCommand comando2;
        private SQLiteDataReader lector;

        
        private string nomusuario;
        private string password;
        private bool permisos;
        private string idEquipoFav;



        /// <summary>
        ///     Función Para Hacer Conexión a BBDD
        /// </summary>
        /// <param name="usuario">
        ///     Recibimos Usuario del Login o Para Hacer Consulta
        /// </param>
        /// <param name="contraseña"></param>
        ///     Recibimos la Contraseña del Usuario
        /// <returns>
        ///     Devuelve un Booleano dependiendo del Resultado
        /// </returns>

        public bool Conectar(string usuario, string contraseña)                             //MÉTODO CONEXIÓN BBDD SQLITE
        {
            bool conDisponible = false;



            string hash = String.Empty;

            
            using (SHA256 sha256 = SHA256.Create())         // Inicializamos a SHA256 la Contraseña de Login
            {
                byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(contraseña));      //Hasheo

                foreach (byte b in hashValue)       //Convertimos Hash de Byte[] -> String
                {
                    hash += $"{b:X2}";
                }
            }



           



            if (conexion != null)       //COMPROBAMOS LA CONEXIÓN
            {
                conexion.Close();
            }
            try
            {     
                if (conexion == null){
                    conexion = new SQLiteConnection("Data Source= ../../../Resources/freaksBBDD.db; Version = 3; New = False; Compress=True;");
                    comando = new SQLiteCommand("SELECT * FROM Usuarios WHERE Usuario='" + usuario + "' and Password='" + hash + "'", conexion);
                    
                    conexion.Open();
                    lector = comando.ExecuteReader();
                    if(lector.HasRows)                                                             //BUSCAMOS USUARIO
                    {
                        if(lector.Read()){                                                         //COMPROBAMOS USUARIO Y SI ES ADMIN
                            nomusuario = lector.GetString(0);
                            password = lector["Password"].ToString();
                            permisos =  Boolean.Parse(lector["Permisos"].ToString());
                            idEquipoFav= lector.GetString(3);


                            conDisponible = true;
                        }
                    }else{                                                                          //SI NO EXISTE USUARIO...s                                                                     
                        MessageBox.Show("NO SE ENCONTRARON REGISTROS");
                    }

                    lector.Close();
                    conexion.Close();

                }else{
                    MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN*");
                    conDisponible = false;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN*\n" + ex.Message);
            }
         
            return conDisponible;

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
        ///     Método para obtener Usuario actual
        /// </summary>
        /// <returns>
        ///     Devuelve Nombre Usuario actual
        /// </returns>
        public string DevuelveUsuario()
        {
            return nomusuario;
        }



        /// <summary>
        ///     Método para obtener Equipo Favorito del Usuario
        /// </summary>
        /// <returns>
        ///     Devuelve ID Equipo Favorito del Usuario actual
        /// </returns>
        public string DevuelveEquipoFav()
        {
            return idEquipoFav;
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
        public bool CompruebaPassword(String usuario,String password)
        {
            bool validador = false;

            bool validezPass=false;                             //VALIDADOR CONTRASEÑA
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
                }
            }


            ///////////////////COMPROBACIÓN USUARIO///////////////////
            if (usuario.Length > 7)
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
                    }


                }
                   // MessageBox.Show("VALIDEZ USUARIO-> "+validezUser+ "\tPASSWORD-> "+validezPass);

                if(validezUser && validezPass)      //COMPROBAMOS QUE HAYAN PASADO LA PRUEBA USUARIO Y CONTRASEÑA
                {
                    validador = true;
                }

            return validador;
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
        public bool CrearUsuario(string usuario, string contraseña, string idEquipo)
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
                if (conexion2 == null)
                {
                    conexion2 = new SQLiteConnection("Data Source= ../../../Resources/freaksBBDD.db; Version = 3; New = False; Compress=True;");
                    comando2 = new SQLiteCommand("INSERT INTO Usuarios VALUES ( '" +usuario+ "' , '" +hash+ "', 'false', '"+idEquipo+"'); ", conexion2);

                    conexion2.Open();
                    MessageBox.Show(comando2.ExecuteNonQuery().ToString());
                    conexion2.Close();

                    
                    MessageBox.Show("BIENVENIDO " + usuario + ", AHORA FORMAS PARTE DE 11FREAKS!");
                    conDisponible= true;

                }else{
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
        ///     Función Para Crear Usuario (TIPO ADMINISTRADOR)
        /// </summary>
        /// <param name="usuario">
        ///     Recibimos Usuario del Login o Para Hacer Consulta
        /// </param>
        /// <param name="contraseña"></param>
        ///     Recibimos la Contraseña del Usuario
        ///
        /// <param name="admin"></param>
        ///     Recibimos si el Usuario es Administrador
        ///     
        /// <param name="idEquipo"></param>
        ///     Recibimos el Id de su Equipo Favorito (en caso de que tenga)
        /// <returns>
        ///     Devuelve un Booleano con el Resultado de la Operación
        ///     <see cref="bool"/>
        /// </returns>
        public bool CrearUsuario(string usuario, string contraseña, bool admin, string idEquipo)     //MÉTODO PARA CREAR ADMIN
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
                if (conexion2 == null)
                {
                    conexion2 = new SQLiteConnection("Data Source= ../../../Resources/freaksBBDD.db; Version = 3; New = False; Compress=True;");
                    comando2 = new SQLiteCommand("INSERT INTO Usuarios VALUES ( '" + usuario + "' , '" + hash + "', '"+admin+"', '"+idEquipo+"'); ", conexion2);

                    conexion2.Open();
                    MessageBox.Show(comando2.ExecuteNonQuery().ToString());
                    conexion2.Close();

                    MessageBox.Show("AHORA " + usuario + ", ES ADMINISTRADOR DE 11FREAKS!");
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
        ///     Función Para Borrar Usuario
        /// </summary>
        /// <param name="usuario">
        ///     Recibimos Usuario  Para Hacer Borrado
        /// </param>
        /// <returns>
        ///     Devuelve un Booleano con el Resultado de la Operación
        ///     <see cref="bool"/>
        /// </returns>
        public bool BorrarUsuario(string usuario)
        {

            bool conDisponible = false;


            try
            {
                if (conexion2 == null)
                {
                    conexion2 = new SQLiteConnection("Data Source= ../../../Resources/freaksBBDD.db; Version = 3; New = False; Compress=True;");
                    comando2 = new SQLiteCommand("DELETE from Usuarios Where Usuario = '" + usuario + "' ; ", conexion2);

                    conexion2.Open();
                    MessageBox.Show(comando2.ExecuteNonQuery().ToString());
                    conexion2.Close();


                    MessageBox.Show(usuario + " , HA SIDO ELIMINADO");
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
        ///     Función Para Mostrar Todos los Usuarios Registrados
        /// </summary>
        ///<returns>
        ///     Devuelve un ArrayList con todos los Usuario Registrados
        ///     <see cref="ArrayList"/>
        /// </returns>

        public ArrayList ConsultaUsuarios()                             //MÉTODO CONEXIÓN BBDD SQLITE
        {
            ArrayList listaUsuarios=new ArrayList();                    //INICIALIZAMOS LISTA CADA VEZ QUE LLAMEMOS AL MÉTODO
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
                    conexion = new SQLiteConnection("Data Source= ../../../Resources/freaksBBDD.db; Version = 3; New = False; Compress=True;");
                    comando = new SQLiteCommand("SELECT * FROM Usuarios ", conexion);

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
                                listaUsuarios.Add("#"+ userName);              //PONEMOS HASHTAG PARA INDICAR QUE ES ADMIN
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
        ///     Función Para Borrar los Datos Desactualizados de los Equipos
        /// </summary>
        public void BorrarDatosEquipos()
        {
            try
            {
                conexion2 = null;
                if (conexion2 == null)
                {
                    conexion2 = new SQLiteConnection("Data Source= ../../../Resources/freaksBBDD.db; Version = 3; New = False; Compress=True;");
                    comando2 = new SQLiteCommand("DELETE from Equipo", conexion2);                                //BORRAMOS TODOS LOS DATOS 
                    conexion2.Open();
                    comando2.ExecuteNonQuery();
                    conexion2.Close();
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
        ///     Función Para Cargar los Datos Actualizados de los Equipos
        /// </summary>
        /// <param name="id">Recibimos el Identificador del Equipo</param>
        /// <param name="nombre">Recibimos el Nombre del Equipo</param>
        /// <param name="año">Recibimos el Año de Fundación del Equipo</param>
        /// <param name="logo">Recibimos la ruta del Logo del Equipo</param>
        /// <param name="estadio">Recibimos el Estadio del Equipo</param>  
        /// <param name="ciudad">Recibimos la Ciudad del Equipo</param>
     
        public void CargarDatosEquipos(string id, string nombre, string año, string logo, string estadio, string ciudad)  {
            
                try
                {
                    conexion2 = null;
                    if (conexion2 == null)
                    {
                        conexion2 = new SQLiteConnection("Data Source= ../../../Resources/freaksBBDD.db; Version = 3; New = False; Compress=True;");
                        comando2 = new SQLiteCommand("INSERT INTO Equipo VALUES ( '"+id+"' , '"+nombre+"', '" +año+"', '"+logo+"' , '"+estadio+"' , '"+ciudad+"' ) ", conexion2);
                        conexion2.Open();                                                                           //CARGAMOS TODOS LOS DATOS ACTUALIZADOSs
                        comando2.ExecuteNonQuery();
                        conexion2.Close();
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
        ///     Función Para Buscar Datos de un Equipo
        /// </summary>
        /// <param name="nombre">
        ///     Recibimos el Nombre del Equipo
        /// </param>
        public string BuscarEquipo(string nombre)                             //MÉTODO CONSULTAR EQUIPO
        {
            bool conDisponible = false;
            string idEquipo=null;
            string nomEquipo;
            string añoFund;
            string logo;
            string estadio;
            string ciudad;


            if (conexion != null)       //COMPROBAMOS LA CONEXIÓN
            {
                conexion.Close();
                conexion = null;
            }

            try
            {
                if (conexion == null)
                {
                    conexion = new SQLiteConnection("Data Source= ../../../Resources/freaksBBDD.db; Version = 3; New = False; Compress=True;");
                    comando = new SQLiteCommand("SELECT * FROM Equipo WHERE Nombre='" + nombre + "' ", conexion);

                    conexion.Open();
                    lector = comando.ExecuteReader();
                    if (lector.HasRows)                                                             //BUSCAMOS USUARIO
                    {
                        if (lector.Read())
                        {                                                         //COMPROBAMOS USUARIO Y SI ES ADMIN
                            idEquipo = lector.GetString(0);
                            nomEquipo = lector.GetString(1);
                            añoFund = lector.GetString(2);
                            logo = lector.GetString(3); 
                            estadio = lector.GetString(4);
                            ciudad = lector.GetString(5);

                            conDisponible = true;
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
                    conDisponible = false;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("*ERROR AL REALIZAR CONEXIÓN*\n" + ex.Message);
            }

            return idEquipo;

        }



        /// <summary>
        ///     Función Para Obtener información de la BD de un Equipo
        /// </summary>
        /// <param name="id">
        ///     Recibimos Id del Equipo a Consultar
        /// </param>
        /// <returns>
        ///     Devuelve Objeto Equipo, o Nulo en su defecto
        /// </returns>

        public Equipo MiInfoEquipo(string id)                             //MÉTODO CONEXIÓN BBDD SQLITE
        {
            Equipo equipo=null;


            if (conexion != null)       //COMPROBAMOS LA CONEXIÓN
            {
                conexion.Close();
                conexion=null;
            }
            try
            {
                if (conexion == null)
                {
                    conexion = new SQLiteConnection("Data Source= ../../../Resources/freaksBBDD.db; Version = 3; New = False; Compress=True;");
                    comando = new SQLiteCommand("SELECT * FROM Equipo WHERE Id='" + id + "'", conexion);

                    conexion.Open();
                    lector = comando.ExecuteReader();
                    if (lector.HasRows)                                                             //BUSCAMOS USUARIO
                    {
                        if (lector.Read())
                        {                                                         //COMPROBAMOS USUARIO Y SI ES ADMIN
                            string nombre = lector.GetString(1);
                            string añoFundacion = lector.GetString(2);
                            string logo = lector.GetString(3);
                            string estadio = lector.GetString(4);
                            string ciudad = lector.GetString(5);

                            equipo = new Equipo(id, nombre, añoFundacion, logo, estadio, ciudad);     //CREAMOS OBJETO EQUIPO QUE DEVOLVEREMOS
                        }
                    }
                    else
                    {                                                                          //SI NO EXISTE EQUIPO...s                                                                     
                        MessageBox.Show("NO SE ENCONTRÓ NINGÚN EQUIPO CON ESAS CARACTERÍSTICAS |ID EQUIPO->"+id);
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
        ///     Función Para Obtener información de la BD de un Equipo
        /// </summary>
        /// <param name="id">
        ///     Recibimos Id del Equipo a Consultar
        /// </param>
        /// <returns>
        ///     Devuelve Objeto Equipo, o Nulo en su defecto
        /// </returns>

        public void CambiarEquipo(string usuario,string id)                             //MÉTODO PARA MODIFICAR EQUIPO FAVORITO
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
                    conexion = new SQLiteConnection("Data Source= ../../../Resources/freaksBBDD.db; Version = 3; New = False; Compress=True;");
                    comando = new SQLiteCommand("UPDATE Usuarios SET EquipoFav="+id+" WHERE Usuario='" + usuario + "'", conexion);

                    conexion.Open();                                                                         //CARGAMOS TODOS LOS DATOS ACTUALIZADOSs
                    comando.ExecuteNonQuery();
                    conexion.Close();

                    MessageBox.Show("HAS CAMBIADO TU EQUIPO FAVORITO");

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
        ///     Función Para Mostrar Todos los Equipos
        /// </summary>
        ///<returns>
        ///     Devuelve un ArrayList con todos los Equipos
        ///     <see cref="ArrayList"/>
        /// </returns>

        public ArrayList ConsultaEquipos()                             //MÉTODO CONEXIÓN BBDD SQLITE
        {
            ArrayList listaEquipos = new ArrayList();                    //INICIALIZAMOS LISTA CADA VEZ QUE LLAMEMOS AL MÉTODO
            string nomEquipo = string.Empty;

            if (conexion != null)                                       //COMPROBAMOS LA CONEXIÓN
            {
                conexion.Close();
                conexion = null;
            }

            try
            {

                if (conexion == null)
                {
                    conexion = new SQLiteConnection("Data Source= ../../../Resources/freaksBBDD.db; Version = 3; New = False; Compress=True;");
                    comando = new SQLiteCommand("SELECT * FROM Equipo ", conexion);

                    conexion.Open();
                    lector = comando.ExecuteReader();
                    if (lector.HasRows)                                                             //BUSCAMOS EQUIPOS
                    {
                        while (lector.Read())
                        {                                                         
                            nomEquipo = lector.GetString(1);                                        //OBTENEMOS NOMBRE
                            listaEquipos.Add(nomEquipo);                                            //AÑADIMOS AL ARRAYLIST QUE DEVOLVEREMOS
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

            return listaEquipos;

        }










    }
}
