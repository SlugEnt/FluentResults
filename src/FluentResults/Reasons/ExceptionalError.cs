using System;

// ReSharper disable once CheckNamespace
namespace SlugEnt.FluentResults
{
    /// <summary>
    /// Error class which stores additionally the exception
    /// </summary>
    public class ExceptionalError : Error, IExceptionalError
    {
        /// <summary>
        /// Exception of the error
        /// </summary>
        public Exception Exception { get; }


        /// <summary>
        /// Creates a new instance of <see cref="ExceptionalError"/>
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="reasonCode">The reason code to return.  Codes 0-999 are reserved for use by the Result Class.  Define your own above this range.  </param>
        public ExceptionalError(Exception exception, int reasonCode = 0)
            : this(exception.Message, exception, reasonCode) { }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <param name="reasonCode">The reason code to return.  Codes 0-999 are reserved for use by the Result Class.  Define your own above this range.  </param>
        public ExceptionalError(string message,
                                Exception exception, int reasonCode = 0)
            : base(message, reasonCode)
        {
            Exception = exception;
        }


        public override string ToString()
        {
            return new ReasonStringBuilder()
                   .WithReasonType(GetType())
                   .WithLineFeeds()
                   .WithInfo(nameof(Message), Message)
                   .WithInfo(nameof(Metadata), string.Join("; ", Metadata))
                   .WithInfo(nameof(Reasons), ReasonFormat.ErrorReasonsToString(Reasons))
                   .WithInfo(nameof(Exception), Exception.ToString())
                   .Build();
        }
    }
}