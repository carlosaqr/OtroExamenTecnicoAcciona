using System;
using System.Web.Http;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using ExamenTecnicoAcciona.Models.ExternalAPI;
using ExamenTecnicoAcciona.Models;
using ExamenTecnicoAcciona.Services;
using ExamenTecnicoAcciona.Interfaces;
using ExamenTecnicoAcciona.App_Start;
using log4net;

namespace ExamenTecnicoAcciona.Controllers
{
    [CustomTrace]
    public class LoginController : ApiController
    {
        private ILoginService _loginService;
        private ILog _logger;

        public LoginController(ILoginService service, ILogHelper<LoginController> logHelper)
        {
            _loginService = service;
            _logger = logHelper.Logger;
        }

        [HttpPost]
        public ResultadoLogin Login(CustomCredentials credentials)
        {
            ResultadoLogin result = _loginService.Login(credentials.UserName, credentials.Password);
            _logger.Info(result.Mensaje);
            return result;
        }
    }
}