using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using Microsoft.VisualBasic.FileIO;
namespace MainProgram
{

    public class Program
    {

        // Program For the Timer
        static async Task Main(string[] args)
        {

            // 

            await UpdateEverySecond();
            // using (TextFieldParser parser = new TextFieldParser("sampletime.csv"))
            // {

            //     parser.SetDelimiters(",");

            //     while (!parser.EndOfData)
            //     {
            //         string[]? fields = parser.ReadFields();
            //         if (fields is not null)
            //         {
            //             foreach (var field in fields)
            //             {
            //                 Console.WriteLine(field);
            //             }
            //         }

            //     }
            // }

        }
        // Update A Theoretical Clock Every Second
        static async Task UpdateEverySecond()
        {
            var periodicTimer = new PeriodicTimer(TimeSpan.FromMilliseconds(1000));
            DateTime seconds = DateTime.Now.AddSeconds(5);
            DateTime seconds2 = seconds.AddSeconds(5);
            while (await periodicTimer.WaitForNextTickAsync())
            {
                DateTime now = DateTime.Now;

                Console.Write("Current Date is : ");
                Console.WriteLine(now.ToString("dddd, MMMM dd yyyy"));
                Console.Write("Current Time is : ");
                Console.WriteLine(now.ToString("hh mm ss fffffff"));
                Console.Write("From : ");
                Console.WriteLine(seconds.ToString("hh mm ss fffffff"));
                Console.Write("to : ");
                Console.WriteLine(seconds2.ToString("hh mm ss fffffff"));

                Console.WriteLine("Current Event is ");



                // Just Compares the current to whatever i set
                DateTime current = now.TrimMilliseconds();
                DateTime goal = seconds.TrimMilliseconds();
                DateTime goal2 = seconds2.TrimMilliseconds();

                Console.WriteLine("taskState: ");

                // TODO Detect the time in between and greater than equal to
                if (current < goal)
                {
                    Console.WriteLine("not yet");

                } else if (current >= goal && current <= goal2)
                {
                    Console.WriteLine("ongoing task");
                }
                else{
                    Console.WriteLine("finished task");
                }

                await SomeLongTask();
                Console.Clear();

            }

        }
        static async Task SomeLongTask()
        {
            await Task.Delay(1000);
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
    public static class miliseconds
    {
        public static DateTime TrimMilliseconds(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, 0, dt.Kind);
        }
    }

}




// Current Time : xxxxx
// Current Event : xxxxxx
// Next Event : xxxxx
// Time till next event : xxxx

