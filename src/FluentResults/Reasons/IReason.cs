using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace SlugEnt.FluentResults
{
    public interface IReason
    {
        string Message { get; }

        int ReasonCode { get; }
        
        Dictionary<string, object> Metadata { get; }
    }
}