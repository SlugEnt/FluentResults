using System.Threading;
using System.Threading.Tasks;
using SlugEnt.FluentResults.Samples.MediatR.MediatRLogic.Messages;
using MediatR;

namespace SlugEnt.FluentResults.Samples.MediatR.MediatRLogic.Handlers
{
    public class QueryWithoutResultHandler : IRequestHandler<QueryWithoutResult, QueryResponse>
    {
        public Task<QueryResponse> Handle(QueryWithoutResult request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new QueryResponse
            {
                Content = "Hello"
            });
        }
    }
}