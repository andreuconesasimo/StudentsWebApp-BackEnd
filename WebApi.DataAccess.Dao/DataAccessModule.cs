using Autofac;
using WebApi.DataAccess.Dao.Interfaces;

namespace WebApi.DataAccess.Dao
{
    public class DataAccessModule : Module
    {

        protected override void Load(ContainerBuilder builder)
        {            
            builder.RegisterType<StudentJsonFile>().As<IFileStudent>().InstancePerRequest();
            builder.RegisterType<StudentSPFile>().As<IFileStudent>().InstancePerRequest();
            builder.RegisterType<StudentSqlFile>().As<IFileStudent>().InstancePerRequest();
            builder.RegisterType<StudentTxtFile>().As<IFileStudent>().InstancePerRequest();
            builder.RegisterType<StudentXmlFile>().As<IFileStudent>().InstancePerRequest();
            base.Load(builder);
        }

    }
}
