using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11FREAKS.Datos
{
    public class Equipo
    {

        public string Id { get; set; }
        public string Nombre { get; set; }
        public string AñoFundacion { get; set; }
        public string Logo { get; set; }
        public string Estadio { get; set; }
        public string Ciudad { get; set; }


        public Equipo(string id, string nombre, string añoFundacion, string logo, string estadio, string ciudad)
        {
            Id = id;
            Nombre = nombre;
            AñoFundacion = añoFundacion;
            Logo = logo;
            Estadio = estadio;
            Ciudad = ciudad;
        }

        public Equipo() { }
    }
}