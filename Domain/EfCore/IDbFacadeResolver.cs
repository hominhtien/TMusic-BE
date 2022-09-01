
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Domain.EfCore
{
    public interface IDbFacadeResolver
    {
        DatabaseFacade Database { get; }
    }
}
