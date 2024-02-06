﻿using System.Threading;
using System.Threading.Tasks;
using SlugEnt.FluentResults.Samples.MediatR.MediatRLogic.Messages;
using MediatR;

namespace SlugEnt.FluentResults.Samples.MediatR.MediatRLogic.Handlers
{
    public class QueryWithResultHandler : IRequestHandler<QueryWithResult, Result<QueryResponse>>
    {
        public Task<Result<QueryResponse>> Handle(QueryWithResult request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Result.Ok(new QueryResponse
            {
                Content = "Hello"
            }));
        }
    }
}