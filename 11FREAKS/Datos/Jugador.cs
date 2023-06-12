using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11FREAKS.Datos
{
    /// <summary>
    ///     Método para gestión de Jugadores
    /// </summary>
    public class Jugador
    {
        public Jugador(int idJugador, int idEquipo, string nombre, int valor, int edad, string foto, string convocable, string dorsal, string escudo, string nacionalidad, int idEquipoOriginal, string posicion)
        {
            this.idJugador = idJugador;
            this.idEquipo = idEquipo;
            Nombre = nombre;
            Valor = valor;
            Edad = edad;
            Foto = foto;
            Convocable = convocable;
            Dorsal = dorsal;
            Escudo = escudo;
            Nacionalidad = nacionalidad;
            this.idEquipoOriginal = idEquipoOriginal;
            Posicion = posicion;
        }

        public Jugador(){}                                  //CONSTRUCTOR POR DEFECTO

        //PROPIEDADES
        public int idJugador { get; set; }               
        public int idEquipo { get; set; }
        public string Nombre { get; set; }
        public int Valor { get; set; }
        public int Edad { get; set; }
        public string Foto { get; set; }
        public string Convocable { get; set; }
        public string Dorsal { get; set; }
        public string Escudo { get; set; }
        public string Nacionalidad { get; set; }
        public int idEquipoOriginal { get; set; }
        public string Posicion { get; set; }


    }
}
