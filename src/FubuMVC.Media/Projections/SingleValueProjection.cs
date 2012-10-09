using System;
using FubuCore.Reflection;

namespace FubuMVC.Media.Projections
{
    public interface ISingleValueProjection<T> : IProjection<T>
    {
        string AttributeName { get; set; }
    }

    public class SingleValueProjection<T> : ISingleValueProjection<T>
    {
        public string AttributeName { get; set; }
        public Func<IProjectionContext<T>, object> Source { get; private set; }

        public SingleValueProjection(string attributeName, Func<IProjectionContext<T>, object> source)
        {
            AttributeName = attributeName;
            Source = source;
        }

        public void Write(IProjectionContext<T> context, IMediaNode node)
        {
            var value = Source(context);
            node.SetAttribute(AttributeName, value);
        }
    }

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