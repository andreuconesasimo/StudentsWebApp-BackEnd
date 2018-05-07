using System.Web.Http;
using Unity;
using Unity.Lifetime;
using WebApi.Business.Logic;
using WebApi.DataAccess.Dao;
using WebApi.DataAccess.Dao.Interfaces;

namespace WebApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<IStudentBL, StudentBL>(new HierarchicalLifetimeManager());
            container.RegisterType<IFileStudent, StudentJsonFile>(new HierarchicalLifetimeManager());
            container.RegisterType<IFileStudent, StudentSPFile>(new HierarchicalLifetimeManager());
            container.RegisterType<IFileStudent, StudentSqlFile>(new HierarchicalLifetimeManager());
            container.RegisterType<IFileStudent, StudentTxtFile>(new HierarchicalLifetimeManager());
            container.RegisterType<IFileStudent, StudentXmlFile>(new HierarchicalLifetimeManager());

            GlobalConfiguration.Configuration.DependencyResolver = new UnityResolver(container);
        }
    }
}