using ExamenTecnicoAcciona.App_Start;
using ExamenTecnicoAcciona.Interfaces;
using ExamenTecnicoAcciona.Models;
using ExamenTecnicoAcciona.Models.ExternalAPI;
using ExamenTecnicoAcciona.Services;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace ExamenTecnicoAcciona.Controllers
{
    [CustomTrace]
    public class ProvinciaController : ApiController
    {
        private IDatosGobArgService _serviceAccess;
        private ILog _logger;

        public ProvinciaController(IDatosGobArgService service, ILogHelper<ProvinciaController> logHelper)
        {
            _serviceAccess = service;
            _logger = logHelper.Logger;
        }

        [HttpGet]
        public ResultadoBusqueda Obtener()
        {
            return Obtener(string.Empty);
        }

        [HttpGet]
        public ResultadoBusqueda Obtener(string nombre)
        {
            ResultadoBusqueda result = null;
            string response = ObtenerResponse(nombre);
            try
            {
                Root yourObject = JsonConvert.DeserializeObject<Root>(response);
                result = Map(yourObject);
                _logger.Info(result.Mensaje);

                return result;
            }
            catch (Exception ex)
            {
                string mensaje = string.Format("Error al parsear respuesta de servidor");
                result = new ResultadoBusqueda();
                result.Mensaje = mensaje;
                result.Provincias = null;

                _logger.Error(mensaje, ex);
                return result;
            }
        }

        private string ObtenerResponse(string provinciaNombre)
        {
            return _serviceAccess.ObtenerResponse(provinciaNombre);
        }

        private Provincia[] MapLista(Provincias[] lista)
        {
            List<Provincia> result = new List<Provincia>();
            foreach (Provincias prov in lista)
            {
                Provincia aux = new Provincia();
                aux.Nombre = prov.nombre;
                aux.Longitud = prov.centroide.lon;
                aux.Latitud = prov.centroide.lat;

                result.Add(aux);
            }

            return result.ToArray();
        }

        private ResultadoBusqueda Map(Root response)
        {
            ResultadoBusqueda res = new ResultadoBusqueda();
            int total = response.provincias.Count();
            if (total > 0)
            {
                res.Provincias = MapLista(response.provincias);
            }
            else
            {
                _logger.Warn("No se encontraron provincias");
            }
            res.Mensaje = String.Format("Se encontraron {0} provincias.", total);

            return res;
        }
    }
}