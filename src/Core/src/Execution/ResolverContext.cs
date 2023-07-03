namespace Chart.Core
{
    public interface IResolverContext
    {
        TResult Result<TResult>();
    }

    public class ResolverContext : IResolverContext
    {
        public TResult Result<TResult>() => default(TResult)!;
    }
}