using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;

namespace MainProgram
{
    public class Program
    {
        private const string CsvFilePath = "sample2.csv";
        private static List<EventData> eventsData;

        static async Task Main(string[] args)
        {
            LoadCSV(CsvFilePath);
            await UpdateEverySecond();
        }

        static void LoadCSV(string filePath)
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

        static async Task UpdateEverySecond()
        {
            var periodicTimer = new PeriodicTimer(TimeSpan.FromMilliseconds(1000));

            while (await periodicTimer.WaitForNextTickAsync())
            {
                DateTime now = DateTime.Now;

                PrintCurrentDateTime(now);
                PrintEventTimesForToday(now);

                await SomeLongTask();
                Console.Clear();
            }
        }

        static void PrintCurrentDateTime(DateTime dateTime)
        {
            Console.Write("Current Date is : ");
            Console.WriteLine(dateTime.ToString("dddd, MMMM dd yyyy"));
            Console.Write("Current Time is : ");
            Console.WriteLine(dateTime.ToString("hh mm ss"));
        }

        static void PrintEventTimesForToday(DateTime now)
        {
            DayOfWeek currentDay = now.DayOfWeek;

            Console.WriteLine("Event Times for Today:");

            foreach (var eventData in eventsData)
            {
                if (currentDay == eventData.DayOfWeek)
                {
                    PrintEventDataDetails(now, eventData);
                }
            }
        }

        static void PrintEventDataDetails(DateTime now, EventData eventData)
        {
            Console.WriteLine($"{eventData.DayOfWeek}, {eventData.StartTime} to {eventData.EndTime} - {eventData.EventName}");

            DateTime current = now.TrimMilliseconds();
            DateTime goal = DateTime.Today.Add(eventData.StartTime).TrimMilliseconds();
            DateTime goal2 = DateTime.Today.Add(eventData.EndTime).TrimMilliseconds();

            Console.WriteLine($"goal is {goal} to {goal2}");

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

            Console.WriteLine("");
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

    public static class Miliseconds
    {
        public static DateTime TrimMilliseconds(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, 0, dt.Kind);
        }
    }
}
