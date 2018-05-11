using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
using System.Web.Http;
using WebApi.Business.Logic;
using WebApi.DataAccess.Dao;

namespace WebApi.App_Start
{
    public static class AutofacConfig
    {
        public static void RegisterComponents()
        {
            var builder = new ContainerBuilder();

            // register assemblies en ejecucion
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());                       

            builder.RegisterModule(new BusinessModule());            
            builder.RegisterModule(new DataAccessModule());            

            // construir container
            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }
    }
}