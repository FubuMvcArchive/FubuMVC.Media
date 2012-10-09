using System;
using System.Linq.Expressions;
using FubuCore.Reflection;
using FubuMVC.Core.Urls;
using FubuCore;

namespace FubuMVC.Media.Projections
{
    public class AccessorProjection<T, TValue> : IProjection<T>
    {
        private readonly Accessor _accessor;
        private ISingleValueProjection<T> _inner;

        public AccessorProjection(Accessor accessor)
        {
            _accessor = accessor;

            if (typeof(TValue).CanBeCastTo<IProjectMyself>())
            {
                _inner = typeof (SelfProjectingValueProjector<,>)
                    .CloseAndBuildAs<ISingleValueProjection<T>>(accessor, typeof (T), typeof (TValue));
            }
            else
            {
                _inner = new SingleValueProjection<T>(_accessor.Name, c => c.ValueFor(_accessor));
            }
        }

        public static AccessorProjection<T, TValue> For(Expression<Func<T, TValue>> expression)
        {
            return new AccessorProjection<T, TValue>(ReflectionHelper.GetAccessor(expression));
        }

        public AccessorProjection<T, TValue> Name(string value)
        {
            _inner.AttributeName = value;
            return this;
        }

        public AccessorProjection<T, TValue> Formatted()
        {
            _inner = new SingleValueProjection<T>(_inner.AttributeName, context => context.FormattedValueOf(_accessor));

            return this;
        }

        public AccessorProjection<T, TValue> FormattedBy(Func<TValue, object> formatting)
        {
            _inner = new SingleValueProjection<T>(_inner.AttributeName, context =>
            {
                var raw = context.ValueFor(_accessor);
                if (raw == null)
                {
                    return null;
                }

                return formatting((TValue)raw);
            });

            return this;
        }

        public AccessorProjection<T, TValue> WriteUrlFor(Func<TValue, object> inputBuilder)
        {
            return WriteUrlFor((urls, value) =>
            {
                var inputModel = inputBuilder(value);
                return urls.UrlFor(inputModel);
            });
        }

        public AccessorProjection<T, TValue> WriteUrlFor(Func<IUrlRegistry, TValue, string> urlFinder)
        {
            _inner = new SingleValueProjection<T>(_inner.AttributeName, context =>
            {
                var raw = context.ValueFor(_accessor);
                if (raw == null)
                {
                    return string.Empty;
                }

                return urlFinder(context.Urls, (TValue)raw);
            });

            return this;
        }

        public string Name()
        {
            return _inner.AttributeName;
        }

        public void Write(IProjectionContext<T> context, IMediaNode node)
        {
            _inner.Write(context, node);
        }
    }
}