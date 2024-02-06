using System;
using System.Threading;
using System.Threading.Tasks;
using SlugEnt.FluentResults.Samples.MediatR.MediatRLogic.Messages;
using MediatR;

namespace SlugEnt.FluentResults.Samples.MediatR.MediatRLogic.Handlers
{
    public class CommandWithoutResultHandler : AsyncRequestHandler<CommandWithoutResult>
    {
        protected override Task Handle(CommandWithoutResult request, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Hello {nameof(CommandWithoutResult)}");

            return Task.CompletedTask;
        }
    }
}
