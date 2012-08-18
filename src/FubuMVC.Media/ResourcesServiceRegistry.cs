using FubuMVC.Core.Registration;
using FubuMVC.Core.Runtime.Formatters;
using FubuMVC.Media.Projections;

namespace FubuMVC.Media
{
    public class ResourcesServiceRegistry : ServiceRegistry
    {
        public ResourcesServiceRegistry()
        {
            AddService<IFormatter, JsonFormatter>();
            AddService<IFormatter, XmlFormatter>();

            SetServiceIfNone(typeof(IValues<>), typeof(SimpleValues<>));
            SetServiceIfNone(typeof(IValueSource<>), typeof(ValueSource<>));

            SetServiceIfNone<IProjectionRunner, ProjectionRunner>();
            SetServiceIfNone(typeof(IProjectionRunner<>), typeof(ProjectionRunner<>)); 
            SetServiceIfNone<IProjectionRunner, ProjectionRunner>();
            SetServiceIfNone(typeof(IProjectionRunner<>), typeof(ProjectionRunner<>));

        }
    }
}