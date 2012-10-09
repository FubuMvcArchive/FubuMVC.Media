using FubuCore.Reflection;

namespace FubuMVC.Media.Projections
{
    public class SelfProjectingValueProjector<TParent, T> : ISingleValueProjection<TParent> where T : class, IProjectMyself
    {
        private readonly Accessor _accessor;

        public SelfProjectingValueProjector(Accessor accessor)
        {
            _accessor = accessor;
            AttributeName = accessor.Name;
        }

        public void Write(IProjectionContext<TParent> context, IMediaNode node)
        {
            var value = context.ValueFor(_accessor) as T;
            if (value != null)
            {
                value.Project(AttributeName, node);
            }
        }

        public string AttributeName { get; set; }
    }
}