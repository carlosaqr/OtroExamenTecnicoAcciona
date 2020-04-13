using ExamenTecnico.DataAccess;
using ExamenTecnico.DataAccess.Model;
using ExamenTecnicoAcciona.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamenTecnicoAcciona.Services
{
    public class DatosGobArgService : IDatosGobArgService
    {
        private ExternalApiAccess _dataAccess;
        private ILog _logger;

        public DatosGobArgService(ExternalApiAccess apiAccess, ILogHelper<DatosGobArgService> logHelper)
        {
            _dataAccess = apiAccess;
            _logger = logHelper.Logger;
        }

        public string ObtenerResponse(string provinciaNombre)
        {
            List<Parametro> lista = new List<Parametro>();

            if (!string.IsNullOrWhiteSpace(provinciaNombre))
            {
                Parametro nombre = new Parametro();
                nombre.Nombre = "nombre";
                nombre.Valor = provinciaNombre;
                lista.Add(nombre);
            }

            string result = null;
            try
            {
                result = _dataAccess.ObtenerResponse(lista);
            }
            catch (Exception ex)
            {
                _logger.Error("Se produjo un error al intentar obtener lista de provincias", ex);
            }
            return result;
        }
    }
}