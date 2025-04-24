using System;

namespace FluentResults.Reasons;

/// <summary>
/// Codes 0-999 are reserved for use by the Result Class.  Define your own above this range.
/// </summary>
[Obsolete]
public static class ReasonCodes
{
    /// <summary>
    /// The object was not found.
    /// </summary>
    public const int NOT_FOUND = 100;

    /// <summary>
    /// The object already exists.
    /// </summary>
    public const int ALREADY_EXISTS = 101;

    /// <summary>
    /// The object is invalid.
    /// </summary>
    public const int INVALID = 102;

    /// <summary>
    /// The user is not authorized to perform this action.
    /// </summary>
    public const int UNAUTHORIZED = 103;
}


