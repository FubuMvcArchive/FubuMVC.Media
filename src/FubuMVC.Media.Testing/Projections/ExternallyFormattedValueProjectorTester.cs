using FubuCore.Reflection;
using FubuMVC.Media.Projections;
using NUnit.Framework;
using FubuCore;
using System.Linq;
using FubuTestingSupport;

namespace FubuMVC.Media.Testing.Projections
{
    [TestFixture]
    public class ExternallyFormattedValueProjectorTester
    {
        [Test]
        public void accessors()
        {
            var accessor = ReflectionHelper.GetAccessor<ExternalFormatTarget>(x => x.Name);
            var formatter = new ExternallyFormattedValueProjector<ExternalFormatTarget, string>(accessor, null);
            formatter.As<IProjection<ExternalFormatTarget>>().Accessors().Single().ShouldEqual(accessor);
        }
    }

    public class ExternalFormatTarget
    {
        public string Name { get; set; }
    }
}