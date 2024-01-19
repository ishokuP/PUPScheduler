using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using Microsoft.VisualBasic.FileIO;
using System.Threading.Tasks;
namespace MainProgram
{



    public class Program
    {

        private static List<EventData> eventsData;

        // Program For the Timer
        static async Task Main(string[] args)
        {
            loadCSV("sample2.csv");
            await UpdateEverySecond();

        }

        static void loadCSV(string filePath)
        {
            eventsData = new List<EventData>();

            try
            {
                using (TextFieldParser parser = new TextFieldParser(filePath))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");

                    while (!parser.EndOfData)
                    {
                        string[] fields = parser.ReadFields();

                        if (fields.Length >= 4 && Enum.TryParse(fields[0], true, out DayOfWeek dayOfWeek)
                            && TimeSpan.TryParse(fields[1], out TimeSpan startTime) && TimeSpan.TryParse(fields[2], out TimeSpan endTime))
                        {
                            var eventData = new EventData
                            {
                                DayOfWeek = dayOfWeek,
                                StartTime = startTime,
                                EndTime = endTime,
                                EventName = fields[3]
                            };

                            eventsData.Add(eventData);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading CSV file: {ex.Message}");
            }
        }
        // Update A Theoretical Clock Every Second
        static async Task UpdateEverySecond()
        {
            var periodicTimer = new PeriodicTimer(TimeSpan.FromMilliseconds(1000));
            DateTime seconds = DateTime.Now.AddSeconds(5);
            DateTime seconds2 = seconds.AddSeconds(5);
            // Inside the UpdateEverySecond method
            while (await periodicTimer.WaitForNextTickAsync())
            {
                DateTime now = DateTime.Now;

                Console.Write("Current Date is : ");
                Console.WriteLine(now.ToString("dddd, MMMM dd yyyy"));
                Console.Write("Current Time is : ");
                Console.WriteLine(now.ToString("hh mm ss"));

                Console.WriteLine("Event Times for Today:");

                DayOfWeek currentDay = now.DayOfWeek;

                foreach (var eventData in eventsData)
                {
                    // Check if the event day matches the current day
                    if (currentDay == eventData.DayOfWeek)
                    {
                        Console.WriteLine($"{eventData.DayOfWeek}, {eventData.StartTime} to {eventData.EndTime} - {eventData.EventName}");

                        DateTime current = now.TrimMilliseconds();
                        DateTime goal = DateTime.Today.Add(eventData.StartTime);
                        DateTime goal2 = DateTime.Today.Add(eventData.EndTime);

                        Console.WriteLine("taskState: ");

                        if (current < goal)
                        {
                            Console.WriteLine("not yet");
                        }
                        else if (current >= goal && current <= goal2)
                        {
                            Console.WriteLine("ongoing task");
                        }
                        else
                        {
                            Console.WriteLine("finished task");
                        }
                    }
                }

                await SomeLongTask();
                Console.Clear();
            }


        }
        static async Task SomeLongTask()
        {
            await Task.Delay(1000);
        }

    }
    public class EventData
    {
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string EventName { get; set; }
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

