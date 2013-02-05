using FubuCore.Reflection;
using FubuMVC.Media.Projections;
using NUnit.Framework;
using FubuCore;
using System.Linq;
using FubuTestingSupport;

namespace FubuMVC.Media.Testing.Projections
{
    [TestFixture]
    public class SelfProjectingValueProjectorTester
    {
        [Test]
        public void accessors()
        {
            var accessor = ReflectionHelper.GetAccessor<HoldsProjectsItself>(x => x.Itself);
            var projection = new SelfProjectingValueProjector<HoldsProjectsItself, ProjectsItself>(accessor);

            projection.As<IProjection<HoldsProjectsItself>>()
                .Accessors()
                .Single()
                .ShouldEqual(accessor);
        }
    }

    public class ProjectsItself : IProjectMyself
    {
        public void Project(string attributeName, IMediaNode node)
        {
            throw new System.NotImplementedException();
        }
    }

    public class HoldsProjectsItself
    {
        public ProjectsItself Itself { get; set; }
    }
}