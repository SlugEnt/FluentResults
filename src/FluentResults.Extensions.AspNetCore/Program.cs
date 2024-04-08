using System;

namespace FluentResults.Extensions.AspNetCore;

public class Program
{
    /// <summary>
    /// See this.  This was the solution to get past a Formfile bug
    /// https://stackoverflow.com/questions/58904234/iformfile-not-load-type-microsoft-aspnetcore-http-internal-formfile-from-assemb
    /// </summary>
    /// <param name="args"></param>
    /// <exception cref="NotImplementedException"></exception>
    public static void Main(string[] args) { throw new NotImplementedException("This class is never meant to be run."); }
}