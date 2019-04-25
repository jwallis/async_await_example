using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Main: Starting");

            APIInteraction();

            Console.WriteLine("Main: Ok now I am responsive.  Give me some input...");
            string input;

            for (int i = 0; i < 5; i++)
            {
                input = Console.ReadLine();
                Console.WriteLine("Main: you entered \"" + input + "\"");
            }

            Console.WriteLine("Main: Ending");
        }

        static async void APIInteraction()
        {
            Console.WriteLine("    APIInteraction: Starting.  We're still on the main thread, sleeping 3 sec to prove it (UI will be blocked)");
            Thread.Sleep(3000);
            Console.WriteLine("    APIInteraction: Starting API calls...");

            string apiCallAResult = await APICallA();
            Console.WriteLine("    APIInteraction: Result from apiCallA = " + apiCallAResult);

            Console.WriteLine("    APIInteraction: More API calls.  NON-blocking on APICallB, but blocking on APICallC");
            APICallB();
            APICallC().Wait();

            Console.WriteLine("    APIInteraction: Calling APIs is done, but we didn't wait on all of them to complete.  That's ok.");
        }

        static Task<string> APICallA()
        {
            return Task.Factory.StartNew(() =>
            {
                Console.WriteLine("        APICallA: Starting");
                Thread.Sleep(3000);
                Console.WriteLine("        APICallA: Ending");
                return "A";
            });
        }

        static Task APICallB()
        {
            return Task.Factory.StartNew(() =>
            {
                Console.WriteLine("        APICallB: Starting");
                Thread.Sleep(5000);
                Console.WriteLine("        APICallB: Ending");
            });
        }

        static Task APICallC()
        {
            return Task.Factory.StartNew(() =>
            {
                Console.WriteLine("        APICallC: Starting");
                Thread.Sleep(3000);
                Console.WriteLine("        APICallC: Ending");
            });
        }
    }
}
