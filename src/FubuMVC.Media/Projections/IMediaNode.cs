using System.Collections.Generic;

namespace FubuMVC.Media.Projections
{
    public interface IMediaNode
    {
        IMediaNode AddChild(string name);
        void SetAttribute(string name, object value);

        void WriteLinks(IEnumerable<Link> links);
            
        IMediaNodeList AddList(string nodeName, string leafName);
    }

    public static class MediaNodeExtensions
    {
        public static IMediaNodeList AddList(this IMediaNode node, string name)
        {
            return node.AddList(name, name);
        }
    }

    public interface IMediaNodeList
    {
        IMediaNode Add();
    }
}