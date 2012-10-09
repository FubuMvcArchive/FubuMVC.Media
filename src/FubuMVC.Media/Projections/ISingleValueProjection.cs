namespace FubuMVC.Media.Projections
{
    public interface ISingleValueProjection<T> : IProjection<T>
    {
        string AttributeName { get; set; }
    }
}