using System.Reflection;
using FluentResults.Reasons;
using SlugEnt.FluentResults;

namespace ConsoleTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            Result A1 = Result.Fail("Something wrong", EnumReasonCode.AlreadyExists);
            if (A1.ReasonCode != EnumReasonCode.AlreadyExists)
                Console.WriteLine("Reason Code Error code is not correct");


            Result r1 = TestException();
            if (r1.IsSuccess)
                return;
            r1.PrintErrorToConsole();
            Console.WriteLine($"^^^^^^^^^^^^ Error Title");
            Console.WriteLine(r1.ErrorTitle);

            Console.WriteLine("*****************************************************************************************");
            Result r2 = TestException2();
            r2.PrintErrorToConsole();
            Console.WriteLine("^^^^^^^^^^^^");
            Console.WriteLine(r2.ErrorTitle);
            Console.WriteLine("*****************************************************************************************");

            Console.WriteLine("*****************************************************************************************");
            Result r3 = TestException3();
            r3.PrintErrorToConsole();
            Console.WriteLine($"^^^^^^^^^^^^ Error Title");
            Console.WriteLine(r3.ErrorTitle);
            Console.WriteLine("---------------------------------------------  Simplified and ToString");
            Console.WriteLine(r3.ToStringSimplified());
            Console.WriteLine(r3.ToString());
            Console.WriteLine("---------------------------------------------");

            // Test adding an error to an existing result.
            Console.WriteLine("*****************************************************************************************");
            Console.WriteLine("*****   Adding an error object to the existing result.  *****");
            Result r4 = TestException3();
            r4.AddError(new Error("Supplier was closed this week"));
            Console.WriteLine($"^^^^^^^^^^^^ Error Title");
            Console.WriteLine(r4.ErrorTitle);

            r4.PrintErrorToConsole();
        }


        internal static Result TestException()
        {

            Result a = TestException_A();
            if (a.IsFailed)

                return Result.Fail("Unable to process bank transfer").WithErrors(a.Errors);
            return Result.Ok();
        }


        internal static Result TestException_A()
        {
            Result result = new Result();
            try
            {
                throw new ArgumentException("Bad Debit Amount.");
            }
            catch (Exception ex)
            {
                return xyz(ex,
                           "TestException_A",
                           "Account",
                           "Transfer has failed");
                result = Result.Fail(new Error("failed").CausedBy(ex));
                return result;
            }
        }


        internal static Result TestException2()
        {

            Result a = TestException_2A();
            if (a.IsFailed)

                return Result.Fail("Unable to process car loan").WithErrors(a.Errors);

            return Result.Ok();
        }


        internal static Result TestException_2A()
        {
            Result result1 = Result.Fail("Invalid Loan Document").
                                    AddMetaData("Document #", "4544346")
                                    .AddMetaData("Loan Officer", "Sam Dickens");
            Result result = Result.Fail("Invalid Loan Document");
            return result;

        }


        internal static Result TestException3()
        {

            Result a = TestException_3A();
            if (a.IsFailed)
                return Result.Fail("Do not have that entree")
                             .WithErrors(a.Errors);
                             

            return Result.Ok();
        }


        internal static Result TestException_3A()
        {
            Result result = Result.Fail(new Error( "Out of Filet Mignon").CausedBy("Manager forgot to order"));
            return result;

        }



        public static Result xyz(Exception e,
                          string methodName,
                          string entityType,
                          string customMsg)
        {
            Dictionary<string, object> meta = new();
            meta.Add("Class","E2EntityData::Company");
            meta.Add("Method", "AddAsync");
            Result x = Result.Fail(new Error("Insufficient Funds for Transfer").CausedBy(e).WithMetadata(meta));
            return x;
        }
    }
}