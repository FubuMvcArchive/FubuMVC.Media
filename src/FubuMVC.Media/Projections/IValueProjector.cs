namespace FubuMVC.Media.Projections
{
    public interface IValueProjector<T>
    {
        void Project(string attributeName, T value, IMediaNode node);
    }
}