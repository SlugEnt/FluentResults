namespace SlugEnt.FluentResults;

/// <summary>
/// Reason code definitions.  These are used to categorize reasons (success or error) in results.  Sometimes it will return a failure along with the reason code, the caller then may determine it is not a real failure.
/// </summary>
public enum EnumReasonCode
{
    NotSpecified = 0,
    NotFound = 10,
    AlreadyExists = 11,
    Invalid = 12,
    Unauthorized = 13,
}