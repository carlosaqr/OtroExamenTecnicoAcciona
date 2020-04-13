using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamenTecnicoAcciona.Models
{
    public class ResultadoBusqueda
    {
        public string Mensaje { get; set; }

        public Provincia[] Provincias { get; set; }
    }
}