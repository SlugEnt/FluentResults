using System.Collections.Generic;


namespace SlugEnt.FluentResults;

public interface IResultBase
{
    /// <summary>
    /// Is true if Reasons contains at least one error
    /// </summary>
    bool IsFailed { get; }

    /// <summary>
    /// Is true if Reasons contains no errors
    /// </summary>
    bool IsSuccess { get; }

    /// <summary>
    /// Get all reasons (errors and successes)
    /// </summary>
    List<IReason> Reasons { get; }

    /// <summary>
    /// Get all errors
    /// </summary>
    List<IError> Errors { get; }

    /// <summary>
    /// Get all successes
    /// </summary>
    List<ISuccess> Successes { get; }

    public string ToStringForPrint(string resultName = "");

    public string ToStringSimplified();

    public string ToStringErrorOnly();

    public string ErrorTitle
    {
        get;
    }

    /// <summary>
    /// Prints the Result that is in error to the console in a pretty format.
    /// </summary>
    public void PrintErrorToConsole();

}