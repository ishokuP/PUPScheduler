using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
namespace MainProgram
{
    public class Program
    {

        // Program For the Timer
        static async Task Main(string[] args)
        {

            await UpdateEverySecond();

        }
        // Update A Theoretical Clock Every Second
        static async Task UpdateEverySecond()
        {
            var stopwatch = Stopwatch.StartNew();
            var periodicTimer = new PeriodicTimer(TimeSpan.FromMilliseconds(1000));
            DateTime seconds = DateTime.Now.AddSeconds(5);
            while (await periodicTimer.WaitForNextTickAsync())
            {
                DateTime now = DateTime.Now;


                // Console.Write("Current Date is : ");
                // Console.WriteLine(now.ToString("dddd, MMMM dd yyyy"));
                Console.Write("Current Time is : ");
                Console.WriteLine(now.ToString("hh mm ss fffffff"));
                Console.Write("Target Time is  : ");
                Console.WriteLine(seconds.ToString("hh mm ss fffffff"));
                // await SomeLongTask();
                Console.WriteLine($"Periodic Time: {stopwatch.ElapsedMilliseconds}");

                // Just Compares the current to whatever i set
                int Timeresult = DateTime.Compare(now, seconds);
                if (Timeresult < 0)
                {
                    Console.WriteLine("not right now");
                }
                else if (Timeresult == 0)
                {
                    // TODO Fix this, reason why as to this wont work cause the miliseconds are time apart
                    // TODO just unironically do a if else compare lOL : Implement the ff below
                    // public static DateTime TrimMilliseconds(this DateTime dt)
                    // {
                    //     return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, 0, dt.Kind);
                    // }
                    Console.WriteLine("NOW");


                }
                else
                {
                    Console.WriteLine("Passed");
                    break;
                }

                // TODO: move the current list head to the next when it reaches Timeresult == 0;

            }

        }
        static async Task SomeLongTask()
        {
            await Task.Delay(250);
        }

        public bool Timechecker()
        {
            return true;
        }

        // Store some Var's Here ong
        public void ListItems()
        {
            var DateStored = new List<dynamic>();
        }
    }
}

