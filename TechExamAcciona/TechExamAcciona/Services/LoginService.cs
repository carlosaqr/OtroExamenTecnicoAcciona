using ExamenTecnicoAcciona.Interfaces;
using ExamenTecnicoAcciona.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ExamenTecnicoAcciona.Services
{
    public class LoginService : ILoginService
    {
        private ILog _logger;

        public LoginService(ILogHelper<LoginService> logHelper)
        {
            _logger = logHelper.Logger;
        }

        public ResultadoLogin Login(string usuario, string password)
        {
            ResultadoLogin resultado = new ResultadoLogin();
            if (usuario.Equals(ConfigurationManager.AppSettings["ValidUser"])
                 && password.Equals(ConfigurationManager.AppSettings["ValidPass"]))
            {
                resultado.Mensaje = "Usuario autorizado";
                resultado.Datos = new Random(DateTime.Now.Millisecond).Next().ToString();
            }
            else
            {
                resultado.Mensaje = "Usuario no autorizado";
                _logger.Warn(resultado.Mensaje);
            }

            return resultado;
        }
    }
}