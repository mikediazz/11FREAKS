using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11FREAKS.Datos
{
    /// <summary>
    ///     Clase para la gestión de Usuarios
    /// </summary>
    public class Usuarios
    {
        public Usuarios(string usuario, string password, string permisos, string email, string imagen, string equipo, string activo, int idEquipo)
        {
            Usuario = usuario;
            Password = password;
            Permisos = permisos;
            Email = email;
            Imagen = imagen;
            Equipo = equipo;
            Activo = activo;
            this.idEquipo = idEquipo;
        }

        public Usuarios() { }                                   //CONSTRUCTOR POR DEFECTO


        //PROPIEDADES
        public string Usuario { get; set; }                    
        public string Password { get; set; }
        public string Permisos { get; set; }
        public string Email { get; set; }
        public string Imagen { get; set; }
        public string Equipo { get; set; }
        public string Activo { get; set; }
        public int idEquipo { get; set; }
    }
}
