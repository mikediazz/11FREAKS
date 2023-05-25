using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _11FREAKS.Datos
{
    public class Equipo
    {

        public int idEquipo { get; set; }                                   //PROPIEDADES
        public string idLiga { get; set; }
        public string Nombre { get; set; }
        public string Abreviatura { get; set; }
        public int Posicion { get; set; }
        public int Puntos { get; set; }
        public int Presupuesto { get; set; }
        public int Victorias { get; set; }
        public int Empates { get; set; }
        public int Derrotas { get; set; }



        public Equipo() { }                                                 //CONTRUCTOR POR DEFECTO

        public Equipo(int idEquipo, string idLiga, string nombre, string abreviatura, int posicion, int puntos, int presupuesto, int victorias, int empates, int derrotas)
        {
            this.idEquipo = idEquipo;
            this.idLiga = idLiga;
            Nombre = nombre;
            Abreviatura = abreviatura;
            Posicion = posicion;
            Puntos = puntos;
            Presupuesto = presupuesto;
            Victorias = victorias;
            Empates = empates;
            Derrotas = derrotas;
        }
    }
}