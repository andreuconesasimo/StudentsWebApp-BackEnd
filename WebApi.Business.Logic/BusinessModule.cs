using Autofac;

namespace WebApi.Business.Logic
{
    public class BusinessModule : Module
    {

        protected override void Load(ContainerBuilder builder)
        {            
            builder.RegisterType<StudentBL>().As<IStudentBL>().InstancePerRequest();
            base.Load(builder);
        }

    }
}
