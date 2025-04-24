using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace SlugEnt.FluentResults
{
    /// <summary>
    /// Objects from Error class cause a failed result
    /// </summary>
    public class Error : IError
    {
        /// <summary>
        /// Message of the error
        /// </summary>
        public string Message { get; protected set; }

        /// <summary>
        /// Metadata of the error
        /// </summary>
        public Dictionary<string, object> Metadata { get; }


        /// <summary>
        /// A code that can be used to more specifically categorize the error.  Such as "Id not found".  Might not be a hard error, but needs to be handled.
        /// mainly used to eliminate needing to use strings for error codes and then interroperating them to find the real error.
        /// </summary>
        public EnumReasonCode ReasonCode { get; protected set; }


        /// <summary>
        /// Get the reasons of an error
        /// </summary>
        public List<IError> Reasons { get; }


        protected Error()
        {
            Metadata = new Dictionary<string, object>();
            Reasons  = new List<IError>();
        }


        /// <summary>
        /// Creates a new instance of <see cref="Error"/>
        /// </summary>
        /// <param name="message">Discription of the error</param>
        /// <param name="reasonCode">The reason code to return.  Codes 0-999 are reserved for use by the Result Class.  Define your own above this range.  </param>
        public Error(string message, EnumReasonCode reasonCode = 0)
            : this()
        {
            Message = message;
            ReasonCode = reasonCode;
        }


        /// <summary>
        /// Creates a new instance of <see cref="Error"/>
        /// </summary>
        /// <param name="message">Discription of the error</param>
        /// <param name="causedBy">The root cause of the <see cref="Error"/></param>
        public Error(string message,
                     IError causedBy, EnumReasonCode reasonCode = EnumReasonCode.NotSpecified)
            : this(message, reasonCode)
        {
            if (causedBy == null)
                throw new ArgumentNullException(nameof(causedBy));

            Reasons.Add(causedBy);
        }



        /// <summary>
        /// Set the root cause of the error
        /// </summary>
        public Error CausedBy(IError error)
        {
            if (error == null)
                throw new ArgumentNullException(nameof(error));

            Reasons.Add(error);
            return this;
        }


        /// <summary>
        /// Set the root cause of the error
        /// </summary>
        public Error CausedBy(Exception exception)
        {
            if (exception == null)
                throw new ArgumentNullException(nameof(exception));

            Reasons.Add(Result.Settings.ExceptionalErrorFactory(null, exception));
            return this;
        }


        /// <summary>
        /// Set the root cause of the error
        /// </summary>
        public Error CausedBy(string message,
                              Exception exception)
        {
            if (exception == null)
                throw new ArgumentNullException(nameof(exception));

            Reasons.Add(Result.Settings.ExceptionalErrorFactory(message, exception));
            return this;
        }


        /// <summary>
        /// Set the root cause of the error
        /// </summary>
        public Error CausedBy(string message, EnumReasonCode reasonCode = 0)
        {
            Reasons.Add(Result.Settings.ErrorFactory(message,reasonCode));
            return this;
        }


        /// <summary>
        /// Set the root cause of the error
        /// </summary>
        public Error CausedBy(IEnumerable<IError> errors)
        {
            if (errors == null)
                throw new ArgumentNullException(nameof(errors));

            Reasons.AddRange(errors);
            return this;
        }


        /// <summary>
        /// Set the root cause of the error
        /// </summary>
        public Error CausedBy(IEnumerable<string> errors, EnumReasonCode reasonCode = EnumReasonCode.NotSpecified)
        {
            if (errors == null)
                throw new ArgumentNullException(nameof(errors));

            Reasons.AddRange(errors.Select(errorMessage => Result.Settings.ErrorFactory(errorMessage, reasonCode)));
            return this;
        }


        /// <summary>
        /// Set the metadata
        /// </summary>
        public Error WithMetadata(string metadataName,
                                  object metadataValue)
        {
            Metadata.Add(metadataName, metadataValue);
            return this;
        }


        /// <summary>
        /// Set the metadata
        /// </summary>
        public Error WithMetadata(Dictionary<string, object> metadata)
        {
            foreach (var metadataItem in metadata)
            {
                Metadata.Add(metadataItem.Key, metadataItem.Value);
            }

            return this;
        }


        public override string ToString() { return ToStringCustom(false); }


        /// <summary>
        /// Prints to a linefeed separated string.
        /// </summary>
        /// <returns></returns>
        public string ToStringWithLineFeeds() { return ToStringCustom(true); }



        /// <summary>
        /// Prints either with semicolon separators or Linefeeds
        /// </summary>
        /// <returns></returns>
        private string ToStringCustom(bool linefeeds = false)
        {
            string separator = "; ";
            if (linefeeds)
                separator = Environment.NewLine;

            return new ReasonStringBuilder()
                   .WithReasonType(GetType())
                   .WithLineFeeds()
                   .WithInfo(nameof(Message), Message)
                   .WithInfo(nameof(Metadata), string.Join(separator, Metadata))
                   .WithInfo(nameof(Reasons), ReasonFormat.ErrorReasonsToString(Reasons, separator))
                   .Build();
        }
    }


    internal class ReasonFormat
    {
        public static string ErrorReasonsToString(IReadOnlyCollection<IError> errorReasons,
                                                  string separator = "; ")
        {
            return string.Join(separator, errorReasons);
        }


        public static string ReasonsToString(IReadOnlyCollection<IReason> errorReasons,
                                             string separator = "; ")
        {
            return string.Join(separator, errorReasons);
        }
    }
}