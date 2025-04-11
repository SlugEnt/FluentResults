using System;
using System.Collections.Generic;
using System.Linq;
using SlugEnt.FluentResults;
using Microsoft.Extensions.Logging;
using System.Text;

// ReSharper disable once CheckNamespace
namespace SlugEnt.FluentResults
{

    /// <summary>
    /// Provides a base class for Result and Result{TValue}
    /// </summary>
    public abstract partial class ResultBase : IResultBase
    {

        public string ErrorTitle
        {
            get
            {
                if (!IsFailed) return string.Empty;
                if (Reasons.Count == 0) return "No Reason was provided.";
                return Reasons[0].Message;  
            }
        }


        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() => ToStringSimplified();


        /// <summary>
        /// Prints the most readable and simplified Result Information.  
        /// </summary>
        /// <returns></returns>
        public string ToStringSimplified()
        {
            StringBuilder sb = new StringBuilder(400);

            if (IsFailed)
                sb.Append("Fail: ");
            else
                sb.Append("Success: ");

            // Print just the message for each reason.
            foreach (IReason reason in Reasons)
            {
                sb.Append(reason.Message + " | ");
            }
            
            return sb.ToString();
        }

        /// <summary>
        /// Prints a user friendly explanation of the result, whether success or failure.
        /// </summary>
        /// <param name="resultName"></param>
        /// <returns></returns>
        public string ToStringForPrint(string resultName = "")
        {
            StringBuilder sb = new StringBuilder();

            if (resultName != string.Empty)
                sb.Append("Result: " + resultName + "  -->  IsSuccess=" + IsSuccess + Environment.NewLine);

            foreach (var reason in Reasons)
            {
                sb.Append("  | Msg: " + reason.Message);

                if (reason.GetType() == typeof(Error))
                {
                    Error error = (Error)reason;
                    foreach (IError errorReason in error.Reasons)
                    {
                        sb.Append(Environment.NewLine + "    --> " + errorReason.Message);
                    }
                }

                //foreach (var reason1 in (IError)reason) { }
            }

            return sb.ToString();
        }


        /// <summary>
        /// Returns just the error messages from the Result.
        /// </summary>
        /// <returns></returns>
        public string ToStringErrorOnly()
        {
            StringBuilder sb = new StringBuilder();


            foreach (var reason in Reasons)
            {
                sb.Append("  |  " + reason.Message);

                if (reason.GetType() == typeof(Error))
                {
                    Error error = (Error)reason;
                    foreach (IError errorReason in error.Reasons)
                    {
                        sb.Append(Environment.NewLine + "    --> " + errorReason.Message);
                    }
                }
            }

            return sb.ToString();
        }


        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool IsFailed => Reasons.OfType<IError>().Any();

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public bool IsSuccess => !IsFailed;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IReason> Reasons { get; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<IError> Errors => Reasons.OfType<IError>().ToList();

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public List<ISuccess> Successes => Reasons.OfType<ISuccess>().ToList();

        protected ResultBase() { Reasons = new List<IReason>(); }


        /// <summary>
        /// Check if the result object contains an error from a specific type
        /// </summary>
        public bool HasError<TError>() where TError : IError { return HasError<TError>(out _); }


        /// <summary>
        /// Check if the result object contains an error from a specific type
        /// </summary>
        public bool HasError<TError>(out IEnumerable<TError> result) where TError : IError { return HasError<TError>(e => true, out result); }


        /// <summary>
        /// Check if the result object contains an error from a specific type and with a specific condition
        /// </summary>
        public bool HasError<TError>(Func<TError, bool> predicate) where TError : IError { return HasError<TError>(predicate, out _); }


        /// <summary>
        /// Check if the result object contains an error from a specific type and with a specific condition
        /// </summary>
        public bool HasError<TError>(Func<TError, bool> predicate,
                                     out IEnumerable<TError> result) where TError : IError
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return ResultHelper.HasError(Errors, predicate, out result);
        }


        /// <summary>
        /// Check if the result object contains an error with a specific condition
        /// </summary>
        public bool HasError(Func<IError, bool> predicate) { return HasError(predicate, out _); }


        /// <summary>
        /// Check if the result object contains an error with a specific condition
        /// </summary>
        public bool HasError(Func<IError, bool> predicate,
                             out IEnumerable<IError> result)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return ResultHelper.HasError(Errors, predicate, out result);
        }


        /// <summary>
        /// Check if the result object contains an exception from a specific type
        /// </summary>
        public bool HasException<TException>() where TException : Exception { return HasException<TException>(out _); }


        /// <summary>
        /// Check if the result object contains an exception from a specific type
        /// </summary>
        public bool HasException<TException>(out IEnumerable<IError> result) where TException : Exception { return HasException<TException>(error => true, out result); }


        /// <summary>
        /// Check if the result object contains an exception from a specific type and with a specific condition
        /// </summary>
        public bool HasException<TException>(Func<TException, bool> predicate) where TException : Exception { return HasException(predicate, out _); }


        /// <summary>
        /// Check if the result object contains an exception from a specific type and with a specific condition
        /// </summary>
        public bool HasException<TException>(Func<TException, bool> predicate,
                                             out IEnumerable<IError> result) where TException : Exception
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return ResultHelper.HasException(Errors, predicate, out result);
        }



        /// <summary>
        /// Check if the result object contains a success from a specific type
        /// </summary>
        public bool HasSuccess<TSuccess>() where TSuccess : ISuccess { return HasSuccess<TSuccess>(success => true, out _); }


        /// <summary>
        /// Check if the result object contains a success from a specific type
        /// </summary>
        public bool HasSuccess<TSuccess>(out IEnumerable<TSuccess> result) where TSuccess : ISuccess { return HasSuccess<TSuccess>(success => true, out result); }


        /// <summary>
        /// Check if the result object contains a success from a specific type and with a specific condition
        /// </summary>
        public bool HasSuccess<TSuccess>(Func<TSuccess, bool> predicate) where TSuccess : ISuccess { return HasSuccess(predicate, out _); }


        /// <summary>
        /// Check if the result object contains a success from a specific type and with a specific condition
        /// </summary>
        public bool HasSuccess<TSuccess>(Func<TSuccess, bool> predicate,
                                         out IEnumerable<TSuccess> result) where TSuccess : ISuccess
        {
            return ResultHelper.HasSuccess(Successes, predicate, out result);
        }


        /// <summary>
        /// Check if the result object contains a success with a specific condition
        /// </summary>
        public bool HasSuccess(Func<ISuccess, bool> predicate,
                               out IEnumerable<ISuccess> result)
        {
            return ResultHelper.HasSuccess(Successes, predicate, out result);
        }


        /// <summary>
        /// Check if the result object contains a success with a specific condition
        /// </summary>
        public bool HasSuccess(Func<ISuccess, bool> predicate) { return ResultHelper.HasSuccess(Successes, predicate, out _); }


        /// <summary>
        /// Deconstruct Result 
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <param name="isFailed"></param>
        public void Deconstruct(out bool isSuccess,
                                out bool isFailed)
        {
            isSuccess = IsSuccess;
            isFailed  = IsFailed;
        }


        /// <summary>
        /// Deconstruct Result
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <param name="isFailed"></param>
        /// <param name="errors"></param>
        public void Deconstruct(out bool isSuccess,
                                out bool isFailed,
                                out List<IError> errors)
        {
            isSuccess = IsSuccess;
            isFailed  = IsFailed;
            errors    = IsFailed ? Errors : default;
        }



        /// <summary>
        /// Creates a new indent string with the requested number of spaces.
        /// </summary>
        /// <param name="indent"></param>
        /// <returns></returns>
        internal static string CreateIndent(int indent)
        {
            string newLine = "";
            for (int i = 0; i < indent; i++)
                newLine = newLine + " ";
            return newLine;
        }



        /// <summary>
        /// Prints the Failed result to the Console.
        /// </summary>
        public void PrintErrorToConsole()
        {
            if (!IsFailed)
                return;

            int indent    = 0;
            int indentAmt = 2;

            foreach (IError err in Errors)
            {
                Console.WriteLine($"{CreateIndent(indent)}Error Msg:  {err.Message}");
                if (err.Metadata.Count > 0)
                {
                    indent += indentAmt;
                    Console.WriteLine($"{CreateIndent(indent)}Metadata:");


                    if (err.Metadata.Count > 0)
                    {
                        indent += indentAmt;
                        foreach (KeyValuePair<string, object> kv in err.Metadata)
                        {
                            Console.WriteLine($"{CreateIndent(indent)}{kv.Key}    :  {kv.Value}");
                        }

                        indent -= indentAmt;
                    }


                    Console.WriteLine("");
                    indent -= indentAmt;
                }

                if (err.Reasons.Count > 0)
                {
                    indent += indentAmt;
                    foreach (IError reason in err.Reasons)
                    {
                        if (reason is ExceptionalError)
                        {
                            ExceptionalError er = (ExceptionalError)reason;
                            Console.WriteLine($"{CreateIndent(indent)}Reason was an app Exception:  {reason.Message}");
                            indent += indentAmt;
                            Console.WriteLine($"{er.Exception.StackTrace}");
                            Console.WriteLine($"{CreateIndent(indent)}StackTrace:  " + er.Exception.StackTrace);
                            if (er.Exception.InnerException != null)
                                Console.WriteLine($"{CreateIndent(indent)}Inner Except:  " + er.Exception.InnerException);
                        }
                        else
                            Console.WriteLine($"{CreateIndent(indent)}Reason was app Error:  {reason.Message}");


                        if (reason.Metadata.Count > 0)
                        {
                            indent += indentAmt;
                            foreach (KeyValuePair<string, object> kv in reason.Metadata)
                            {
                                Console.WriteLine($"{CreateIndent(indent)}{kv.Key}    :  {kv.Value}");
                            }

                            indent -= indentAmt;
                        }
                    }

                    indent -= indentAmt;
                }
            }
        }
    }
    }

