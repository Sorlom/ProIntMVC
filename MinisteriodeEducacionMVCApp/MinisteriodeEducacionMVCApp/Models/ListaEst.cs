using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinisteriodeEducacionMVCApp.Models
{
    public class ListaEst
    {
        public int idListaEstudiante { get; set; }
        public string nombre { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string correo { get; set; }
        public string paralelo { get; set; }
        public double promedio { get; set; }
        public int idGrupoDiploma { get; set; }
        public int idGestion { get; set; }
        public Nullable<int> idDiploma { get; set; }
    }
}