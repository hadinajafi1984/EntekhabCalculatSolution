using System.Linq.Expressions;

namespace BackendApis.Domain.Contracts.Repositories
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
    }
}
