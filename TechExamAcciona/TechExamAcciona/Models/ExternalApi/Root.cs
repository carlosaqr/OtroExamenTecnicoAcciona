using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamenTecnicoAcciona.Models.ExternalAPI
{
    public class Root
    {
        public int cantidad { get; set; }

        public int inicio { get; set; }

        public Parametros parametros { get; set; }

        public Provincias[] provincias { get; set; }

        public int total { get; set; }
    }
}