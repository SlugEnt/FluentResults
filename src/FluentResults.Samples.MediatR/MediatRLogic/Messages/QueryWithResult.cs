using MediatR;

namespace SlugEnt.FluentResults.Samples.MediatR.MediatRLogic.Messages
{
    public class QueryWithResult : IRequest<Result<QueryResponse>>
    {

    }
}