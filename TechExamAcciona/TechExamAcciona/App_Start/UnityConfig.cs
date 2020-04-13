using ExamenTecnico.DataAccess;
using ExamenTecnicoAcciona.Helper;
using ExamenTecnicoAcciona.Interfaces;
using ExamenTecnicoAcciona.Services;
using log4net;
using System.Configuration;
using System.Web.Http;
using Unity;
using Unity.Injection;
using Unity.WebApi;

namespace ExamenTecnicoAcciona
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<ILoginService, LoginService>();
            container.RegisterType<IDatosGobArgService, DatosGobArgService>();

            string urlServicio = ConfigurationManager.AppSettings["DatosGobArgUrl"];
            string metodo = ConfigurationManager.AppSettings["DatosGobMethod"];
            container.RegisterType<ExternalApiAccess>(new InjectionConstructor(urlServicio, metodo));

            //container.RegisterFactory<ILog>(x => LogHelper.GetLogger());
            container.RegisterType(typeof(ILogHelper<>), typeof(LogHelper<>));


            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}