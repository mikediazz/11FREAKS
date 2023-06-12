using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11FREAKS.Datos
{
    class JugadorAux
    {
        public JugadorAux(string nombre, string equipo, int estadistica, int partidosJugados)     //CONSTRUCTOR AMARILLAS
        {
            Nombre = nombre;
            Equipo = equipo;
            Estadistica = estadistica;
            PartidosJugados = partidosJugados;
        }

        public string Nombre { get; set; }
        public string Equipo { get; set; }
        public int Estadistica { get; set; }
        public int PartidosJugados { get; set; }
    }


}
