using SlugEnt.FluentResults;

namespace ConsoleTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            Result r1 = Test1Exception();
            Console.WriteLine(r1.ToStringWithLineFeeds());

            //Result result = Result.Fail("error line 1").WithError("error line 2").WithError("error 3");
            //Console.WriteLine(result.ToStringWithLineFeeds());
        }


        internal static Result Test1Exception()
        {
            Result result = new Result();
            try
            {
                throw new ArgumentException("bad value");
            }
            catch (Exception ex)
            {
                result = Result.Fail(new Error("failed").CausedBy(ex));
                return result;
            }
        }
    }
}