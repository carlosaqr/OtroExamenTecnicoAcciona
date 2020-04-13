using ExamenTecnicoAcciona.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamenTecnicoAcciona.Interfaces
{
    public interface ILoginService
    {
        ResultadoLogin Login(string usuario, string password);
    }
}