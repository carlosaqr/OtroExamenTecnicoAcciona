using ExamenTecnico.DataAccess.Model;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;

namespace ExamenTecnico.DataAccess
{
    public class ExternalApiAccess
    {
        private string serviceUrl { get; set; }
        private string methodAccess { get; set; }

        public ExternalApiAccess(string url, string accessMethod = "GET")
        {
            this.serviceUrl = url;
            this.methodAccess = accessMethod;
        }

        public string ObtenerResponse(List<Parametro> parametros)
        {
            string pathExternalApi = ConformUrl(parametros);

            return GetResponse(pathExternalApi);
        }

        private string ConformUrl(List<Parametro> parameters)
        {
            char firstSeparator = '?';
            char followingSep = '&';
            bool first = true;
            string pathApi = this.serviceUrl;

            foreach (Parametro parAux in parameters)
            {
                string valorEscapado = HttpUtility.UrlEncode(parAux.Valor.ToString());
                if (first)
                {
                    pathApi += string.Format("{0}{1}={2}", firstSeparator, parAux.Nombre, valorEscapado);
                    first = false;
                }
                else
                {
                    pathApi += string.Format("{0}{1}={2}", followingSep, parAux.Nombre, valorEscapado);
                }

            }
            return pathApi;
        }

        private string GetResponse(string serviceUrl)
        {
            WebRequest requestObject = WebRequest.Create(serviceUrl);
            requestObject.Method = this.methodAccess;
            HttpWebResponse response = (HttpWebResponse)requestObject.GetResponse();

            string streamResult = null;
            using (Stream stream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream);
                streamResult = reader.ReadToEnd();
                reader.Close();
            }

            return streamResult;
        }

    }
}
