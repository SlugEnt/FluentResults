using System;

// ReSharper disable once CheckNamespace
namespace SlugEnt.FluentResults
{
    public interface IExceptionalError : IError
    {
        Exception Exception { get; }

    }
}