using System.IO;
using MediatR.Pipeline;

namespace YoumaconSecurityOps.Core.Mediatr.Processors;

public class EmptyRequestPreProcessor<TRequest> : IRequestPreProcessor<TRequest>
{
    private readonly TextWriter _writer;

    public EmptyRequestPreProcessor(TextWriter writer)
    {
        _writer = writer;
    }

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        return _writer.WriteLineAsync("- Starting Up");
    }
}

