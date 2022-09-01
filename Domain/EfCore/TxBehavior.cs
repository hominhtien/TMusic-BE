using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.EfCore
{
    public class TxBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IDomainEventContext _domainEventContext;
        private readonly IDbFacadeResolver _dbFacadeResolver;
        private readonly IMediator _mediator;

        public TxBehavior(IDbFacadeResolver dbFacadeResolver, IDomainEventContext domainEventContext,
            IMediator mediator)
        {
            _domainEventContext = domainEventContext ?? throw new ArgumentNullException(nameof(domainEventContext));
            _dbFacadeResolver = dbFacadeResolver ?? throw new ArgumentNullException(nameof(dbFacadeResolver));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            if (!(request is ITxRequest))
            {
                return await next();
            }

            var strategy = _dbFacadeResolver.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                // Achieving atomicity
                await using var transaction = await _dbFacadeResolver.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);

                var response = await next();
                await transaction.CommitAsync(cancellationToken);

                var domainEvents = _domainEventContext.GetDomainEvents().ToList();
                var tasks = domainEvents
                    .Select(async @event =>
                    {
                        // because we have int identity
                        var id = (response as dynamic)?.Id;
                        @event.MetaData.Add("id", id);

                        await _mediator.Publish(@event, cancellationToken);
                    });

                await Task.WhenAll(tasks);

                return response;
            });
        }
    }
}
