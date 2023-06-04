using System.Threading;
using System.Threading.Tasks;
using SampleProject.Domain.SeedWork;
using SampleProject.Infrastructure.Database;
using SampleProject.Infrastructure.Processing;

namespace SampleProject.Infrastructure.Domain
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDomainEventsDispatcher _domainEventsDispatcher;

        public UnitOfWork(
            IDomainEventsDispatcher domainEventsDispatcher)
        {
            this._domainEventsDispatcher = domainEventsDispatcher;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await this._domainEventsDispatcher.DispatchEventsAsync();
            //return await this._ordersContext.SaveChangesAsync(cancellationToken);
            return -1;
        }
    }
}