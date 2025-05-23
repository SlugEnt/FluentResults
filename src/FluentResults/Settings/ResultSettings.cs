﻿using System;

// ReSharper disable once CheckNamespace
namespace SlugEnt.FluentResults
{
    public class ResultSettings
    {
        public IResultLogger Logger { get; set; }

        public Func<Exception, IError> DefaultTryCatchHandler { get; set; }

        public Func<string, ISuccess> SuccessFactory { get; set; }

        public Func<string, int, IError> ErrorFactory { get; set; }

        public Func<string, Exception, IExceptionalError> ExceptionalErrorFactory { get; set; }
    }
}