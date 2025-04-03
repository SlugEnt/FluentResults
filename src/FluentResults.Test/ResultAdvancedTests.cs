using System;
using System.Collections.Generic;
using FluentAssertions;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using SlugEnt.FluentResults;
using Xunit;
using Xunit.Abstractions;

namespace FluentResults.Test
{
    public class ResultAdvancedTests
    {
        private readonly ITestOutputHelper output;

        public ResultAdvancedTests(ITestOutputHelper output) { this.output = output; }


        [Fact]
        public void Result1()
        {
            Result<int> result = Result.Ok(2);


            output.WriteLine(result.ToString());

            Result failResult = Result.Fail("Some error").WithError("error number 2");

            output.WriteLine(failResult.ToString());

            output.WriteLine("");
            output.WriteLine("ToStringForPrint");
            output.WriteLine(failResult.ToStringForPrint("SomethingBad"));

            Result fail2 = Result.Fail(new Error("Bad File System Call").CausedBy(new ApplicationException("some App exception happened")));
            output.WriteLine(fail2.ToStringForPrint("Fail2"));
        }


        [Fact]
        public void Result2()
        {
            Result<string> result1 = Result.Ok("All Good");
            Result<string> result2 = Result.Ok("2nd is good");
            Result<int>    result3 = Result.Fail("Not a number");
            result1.Bind(_ => result2);
            result1.Bind(_ => result3);
        }
    }
}