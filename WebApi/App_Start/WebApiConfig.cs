using System.Web.Http;
using Unity;
using Unity.Lifetime;
using WebApi.Business.Logic;
using WebApi.DataAccess.Dao;
using WebApi.DataAccess.Dao.Interfaces;

namespace WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de API web

            // Rutas de API web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var container = new UnityContainer();
            container.RegisterType<IStudentBL, StudentBL>(new HierarchicalLifetimeManager());
            container.RegisterType<IFileStudent, StudentJsonFile>(new HierarchicalLifetimeManager());
            container.RegisterType<IFileStudent, StudentSPFile>(new HierarchicalLifetimeManager());
            container.RegisterType<IFileStudent, StudentSqlFile>(new HierarchicalLifetimeManager());
            container.RegisterType<IFileStudent, StudentTxtFile>(new HierarchicalLifetimeManager());
            container.RegisterType<IFileStudent, StudentXmlFile>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);
        }
    }
}
