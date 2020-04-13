using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExamenTecnicoAcciona.Controllers;
using ExamenTecnicoAcciona.Models;
using ExamenTecnicoAcciona.Services;
using ExamenTecnico.DataAccess;
using Unity;
using ExamenTecnicoAcciona.Interfaces;
using System.Configuration;
using Unity.Injection;
using ExamenTecnicoAcciona.Helper;

namespace ExamenTecnicoAcciona.Tests.Controllers
{
    [TestClass]
    public class FullControllerTest
    {
        [TestMethod]
        public void ObtieneUnaProvincia()
        {
            UnityContainer container = RegisterTypes();

            // Disponer
            ProvinciaController controller = container.Resolve<ProvinciaController>();

            // Actuar
            ResultadoBusqueda result = controller.Obtener("Corrientes");

            // Declarar
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Provincias.Length);
        }

        [TestMethod]
        public void NombreProvinciaErroneo()
        {
            UnityContainer container = RegisterTypes();

            // Disponer
            ProvinciaController controller = container.Resolve<ProvinciaController>();

            // Actuar
            ResultadoBusqueda result = controller.Obtener("Andalucia");

            // Declarar
            Assert.IsNotNull(result);
            StringAssert.Contains(result.Mensaje, "Se encontraron 0 provincias.");
            CollectionAssert.AreEqual(result.Provincias, null);
        }

        [TestMethod]
        public void ForzarMalaConfiguracion()
        {
            UnityContainer container = RegisterTypes("POST");

            // Disponer
            ProvinciaController controller = container.Resolve<ProvinciaController>();

            // Actuar
            ResultadoBusqueda result = controller.Obtener("Tierra");

            // Declarar
            Assert.IsNotNull(result);
            StringAssert.Contains(result.Mensaje, "Error al parsear respuesta de servidor");
            CollectionAssert.AreEqual(result.Provincias, null);
        }

        [TestMethod]
        public void LoginCorrecto()
        {
            UnityContainer container = RegisterTypes();

            // Disponer
            LoginController controller = container.Resolve<LoginController>();
            CustomCredentials credentials = new CustomCredentials();
            credentials.UserName = "admin";
            credentials.Password = "admin";

            // Actuar
            ResultadoLogin result = controller.Login(credentials);

            // Declarar
            Assert.IsNotNull(result);
            StringAssert.Contains(result.Mensaje, "Usuario autorizado");
            Assert.IsNotNull(result.Datos, "No se recibieron datos");
        }

        [TestMethod]
        public void LoginIncorrecto()
        {
            UnityContainer container = RegisterTypes();

            // Disponer
            LoginController controller = container.Resolve<LoginController>();
            CustomCredentials credentials = new CustomCredentials();
            credentials.UserName = "user";
            credentials.Password = "pass";

            // Actuar
            ResultadoLogin result = controller.Login(credentials);

            // Declarar
            Assert.IsNotNull(result);
            StringAssert.Contains(result.Mensaje, "Usuario no autorizado");
            Assert.IsNull(result.Datos, "No se debian recibir datos");
        }


        private UnityContainer RegisterTypes(string metodoAlterno = "")
        {
            var container = new UnityContainer();

            container.RegisterType<ILoginService, LoginService>();
            container.RegisterType<IDatosGobArgService, DatosGobArgService>();

            string urlServicio = ConfigurationManager.AppSettings["DatosGobArgUrl"];
            string metodo = ConfigurationManager.AppSettings["DatosGobMethod"];

            if (!string.IsNullOrWhiteSpace(metodoAlterno))
            {
                metodo = metodoAlterno;
            }
            container.RegisterType<ExternalApiAccess>(new InjectionConstructor(urlServicio, metodo));

            container.RegisterType(typeof(ILogHelper<>), typeof(LogHelper<>));
            return container;
        }

    }
}
