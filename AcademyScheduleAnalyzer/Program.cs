using System;
using System.Globalization;
using System.Text;
using BenchmarkDotNet.Running;
using AcademyScheduleAnalyzer.Benchmarks;

namespace AcademyScheduleAnalyzer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0 && args[0].Equals("--benchmark", StringComparison.OrdinalIgnoreCase))
            {
                BenchmarkRunner.Run<StringBenchmark>();
                return;
            }

            string[] sessionNames =
            {
                "C# Basics",
                "Arrays",
                "Functions",
                "Date and Time",
                "Exception Handling"
            };

            DateTime[] sessionDates =
            {
                new DateTime(2026, 9, 10, 18, 0, 0),
                new DateTime(2026, 9, 13, 18, 0, 0),
                new DateTime(2026, 9, 17, 18, 0, 0),
                new DateTime(2026, 9, 20, 18, 0, 0),
                new DateTime(2026, 9, 24, 18, 0, 0)
            };

            int[] sessionDurations =
            {
                180,
                240,
                180,
                240,
                180
            };

            bool running = true;

            while (running)
            {
                DisplayMenu();
                int option = ReadMenuOption();
                Console.WriteLine();

                switch (option)
                {
                    case 1:
                        DisplaySessions(sessionNames, sessionDates, sessionDurations);
                        break;

                    case 2:
                        SearchSessionMenu(sessionNames, sessionDates, sessionDurations);
                        break;

                    case 3:
                        SortSessionNames(sessionNames);
                        break;

                    case 4:
                        ReverseSessionNames(sessionNames);
                        break;

                    case 5:
                        FindSessionIndexMenu(sessionNames);
                        break;

                    case 6:
                        CheckSessionExistsMenu(sessionNames);
                        break;

                    case 7:
                        ShowDurationStatistics(sessionDurations);
                        break;

                    case 8:
                        ShowSessionDateDetails(sessionNames, sessionDates, sessionDurations);
                        break;

                    case 9:
                        ShowPastAndUpcomingSessions(sessionNames, sessionDates);
                        break;

                    case 10:
                        FindNextSession(sessionNames, sessionDates);
                        break;

                    case 11:
                        CompareTwoSessionDates(sessionNames, sessionDates);
                        break;

                    case 12:
                        ReadAndValidateCustomDate();
                        break;

                    case 13:
                        SelectSessionByIndex(sessionNames);
                        break;

                    case 14:
                        ValidateSessionDurationMenu();
                        break;

                    case 15:
                        Console.WriteLine(BuildReportUsingString(sessionNames, sessionDates, sessionDurations));
                        break;

                    case 16:
                        Console.WriteLine(BuildReportUsingStringBuilder(sessionNames, sessionDates, sessionDurations));
                        break;

                    case 0:
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

                if (running)
                {
                    Console.WriteLine("\nPress Enter to continue...");
                    Console.ReadLine();
                    Console.WriteLine();
                }
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("===================================");
            Console.WriteLine("Academy Schedule Analyzer");
            Console.WriteLine("===================================");
            Console.WriteLine("1. Display all sessions");
            Console.WriteLine("2. Search for a session");
            Console.WriteLine("3. Sort session names");
            Console.WriteLine("4. Reverse session names");
            Console.WriteLine("5. Find session index");
            Console.WriteLine("6. Check if session exists");
            Console.WriteLine("7. Show duration statistics");
            Console.WriteLine("8. Show session date details");
            Console.WriteLine("9. Show past and upcoming sessions");
            Console.WriteLine("10. Find next session");
            Console.WriteLine("11. Compare two session dates");
            Console.WriteLine("12. Read and validate a custom date");
            Console.WriteLine("13. Select session by index");
            Console.WriteLine("14. Validate session duration");
            Console.WriteLine("15. Generate report using string");
            Console.WriteLine("16. Generate report using StringBuilder");
            Console.WriteLine("0. Exit");
        }

        static int ReadMenuOption()
        {
            while (true)
            {
                Console.Write("Choose an option: ");
                string? input = Console.ReadLine();

                try
                {
                    return int.Parse(input ?? string.Empty);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid menu option. Enter a number.");
                }
            }
        }

        static void DisplaySessions(string[] names, DateTime[] dates, int[] durations)
        {
            for (int i = 0; i < names.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {names[i]}");
                Console.WriteLine($"Date: {dates[i]:dd MMMM yyyy}");
                Console.WriteLine($"Start Time: {dates[i]:hh:mm tt}");
                Console.WriteLine($"Duration: {durations[i]} minutes");
                Console.WriteLine();
            }
        }

        static void SearchSessionMenu(string[] names, DateTime[] dates, int[] durations)
        {
            Console.Write("Enter session name: ");
            string? name = Console.ReadLine()?.Trim();

            if (TryFindSession(names, durations, name ?? string.Empty, out int index, out int duration))
            {
                Console.WriteLine($"Name: {names[index]}");
                Console.WriteLine($"Date: {dates[index]:dd MMMM yyyy}");
                Console.WriteLine($"Start Time: {dates[index]:hh:mm tt}");
                Console.WriteLine($"Duration: {duration} minutes");
            }
            else
            {
                Console.WriteLine("Session not found.");
            }
        }

        static int SearchSession(string[] names, string targetName)
        {
            return Array.FindIndex(names, s => s.Equals(targetName, StringComparison.OrdinalIgnoreCase));
        }

        static bool TryFindSession(string[] names, int[] durations, string targetName, out int index, out int duration)
        {
            index = SearchSession(names, targetName);
            if (index != -1)
            {
                duration = durations[index];
                return true;
            }

            duration = 0;
            return false;
        }

        static void SortSessionNames(string[] names)
        {
            string[] copy = new string[names.Length];
            Array.Copy(names, copy, names.Length);
            Array.Sort(copy);

            for (int i = 0; i < copy.Length; i++)
            {
                Console.WriteLine(copy[i]);
            }
        }

        static void ReverseSessionNames(string[] names)
        {
            string[] copy = new string[names.Length];
            Array.Copy(names, copy, names.Length);
            Array.Reverse(copy);

            for (int i = 0; i < copy.Length; i++)
            {
                Console.WriteLine(copy[i]);
            }
        }

        static void FindSessionIndexMenu(string[] names)
        {
            Console.Write("Enter session name: ");
            string? name = Console.ReadLine()?.Trim();

            int index = -1;
            for (int i = 0; i < names.Length; i++)
            {
                if (names[i].Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    index = Array.IndexOf(names, names[i]);
                    break;
                }
            }

            Console.WriteLine($"Index: {index}");
        }

        static void CheckSessionExistsMenu(string[] names)
        {
            Console.Write("Enter session name: ");
            string? name = Console.ReadLine()?.Trim();

            bool exists = Array.Exists(names, s => s.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (exists)
            {
                Console.WriteLine("Session exists.");
            }
            else
            {
                Console.WriteLine("Session does not exist.");
            }

            string? found = Array.Find(names, s => s.StartsWith("C#", StringComparison.OrdinalIgnoreCase));
            if (found != null)
            {
                Console.WriteLine($"Find result: {found}");
            }

            int foundIndex = Array.FindIndex(names, s => s.Length > 10);
            if (foundIndex != -1)
            {
                Console.WriteLine($"FindIndex result: {foundIndex}");
            }

            string[] copy = new string[names.Length];
            Array.Copy(names, copy, names.Length);
            copy[0] = "Updated Session";

            Console.WriteLine("Original array:");
            for (int i = 0; i < names.Length; i++)
            {
                Console.WriteLine($" - {names[i]}");
            }

            Console.WriteLine("Copied array:");
            for (int i = 0; i < copy.Length; i++)
            {
                Console.WriteLine($" - {copy[i]}");
            }

            string[] sample = new string[] { "Item 1", "Item 2" };
            ModifyArrayWithoutRef(sample);
            Console.WriteLine($"Modified without ref: {sample[0]}");
        }

        static void ModifyArrayWithoutRef(string[] items)
        {
            if (items.Length > 0)
            {
                items[0] = "Modified Inside Method";
            }
        }

        static void ShowDurationStatistics(int[] durations)
        {
            int total = GetTotalDuration(durations);
            double average = GetAverageDuration(durations);
            int shortest = GetShortestDuration(durations);
            int longest = GetLongestDuration(durations);

            Console.WriteLine($"Total Duration: {total} minutes");
            Console.WriteLine($"Average Duration: {average} minutes");
            Console.WriteLine($"Shortest Duration: {shortest} minutes");
            Console.WriteLine($"Longest Duration: {longest} minutes");

            int[] copy = new int[durations.Length];
            Array.Copy(durations, copy, durations.Length);
            Array.Sort(copy);

            Console.Write("Sorted Durations: ");
            for (int i = 0; i < copy.Length; i++)
            {
                Console.Write($"{copy[i]} ");
            }
            Console.WriteLine("minutes");

            int totalParams = CalculateTotalDuration(120, 180, 240);
            Console.WriteLine($"CalculateTotalDuration(120, 180, 240): {totalParams} minutes");

            int bonus = 100;
            AddBonus(ref bonus, 50);
            Console.WriteLine($"Value modified with ref: {bonus}");
        }

        static int GetTotalDuration(int[] durations)
        {
            int sum = 0;
            for (int i = 0; i < durations.Length; i++)
            {
                sum += durations[i];
            }
            return sum;
        }

        static double GetAverageDuration(int[] durations)
        {
            if (durations.Length == 0) return 0;
            return (double)GetTotalDuration(durations) / durations.Length;
        }

        static int GetShortestDuration(int[] durations)
        {
            if (durations.Length == 0) return 0;
            int min = durations[0];
            for (int i = 1; i < durations.Length; i++)
            {
                if (durations[i] < min)
                {
                    min = durations[i];
                }
            }
            return min;
        }

        static int GetLongestDuration(int[] durations)
        {
            if (durations.Length == 0) return 0;
            int max = durations[0];
            for (int i = 1; i < durations.Length; i++)
            {
                if (durations[i] > max)
                {
                    max = durations[i];
                }
            }
            return max;
        }

        static int CalculateTotalDuration(params int[] durations)
        {
            int total = 0;
            for (int i = 0; i < durations.Length; i++)
            {
                total += durations[i];
            }
            return total;
        }

        static void AddBonus(ref int val, int amount)
        {
            val += amount;
        }

        static void ShowSessionDateDetails(string[] names, DateTime[] dates, int[] durations)
        {
            Console.Write("Enter session name: ");
            string? name = Console.ReadLine()?.Trim();

            int index = SearchSession(names, name ?? string.Empty);
            if (index == -1)
            {
                Console.WriteLine("Session not found.");
                return;
            }

            DisplaySessionDetails(names[index], dates[index], durations[index]);

            Console.WriteLine();
            DateTime dt = dates[index];
            Console.WriteLine(dt.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
            Console.WriteLine(dt.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
            Console.WriteLine(dt.ToString("dd MMMM yyyy", CultureInfo.InvariantCulture));
            Console.WriteLine(dt.ToString("dddd, dd MMMM yyyy", CultureInfo.InvariantCulture));
            Console.WriteLine(dt.ToString("hh:mm tt", CultureInfo.InvariantCulture));
        }

        static void DisplaySessionDetails(string name, DateTime date, int duration)
        {
            DateTime endTime = GetSessionEndTime(date, duration);

            Console.WriteLine($"Session: {name}");
            Console.WriteLine($"Date: {date:dd MMMM yyyy}");
            Console.WriteLine($"Day: {date.DayOfWeek}");
            Console.WriteLine($"Year: {date.Year}");
            Console.WriteLine($"Month: {date.Month}");
            Console.WriteLine($"Day Number: {date.Day}");
            Console.WriteLine($"Start Time: {date:hh:mm tt}");
            Console.WriteLine($"Duration: {duration} minutes");
            Console.WriteLine($"End Time: {endTime:hh:mm tt}");
        }

        static DateTime GetSessionEndTime(DateTime start, int durationMinutes)
        {
            return start.AddMinutes(durationMinutes);
        }

        static void ShowPastAndUpcomingSessions(string[] names, DateTime[] dates)
        {
            DateTime now = DateTime.Now;

            for (int i = 0; i < names.Length; i++)
            {
                string status = dates[i] < now ? "Past" : "Upcoming";
                Console.WriteLine($"{names[i]} {status}");
            }
        }

        static void FindNextSession(string[] names, DateTime[] dates)
        {
            DateTime now = DateTime.Now;
            int nextIndex = -1;
            TimeSpan minDiff = TimeSpan.MaxValue;

            for (int i = 0; i < dates.Length; i++)
            {
                if (dates[i] > now)
                {
                    TimeSpan diff = dates[i] - now;
                    if (diff < minDiff)
                    {
                        minDiff = diff;
                        nextIndex = i;
                    }
                }
            }

            if (nextIndex != -1)
            {
                DateTime nextDate = dates[nextIndex];
                TimeSpan remaining = nextDate - now;

                Console.WriteLine("Next Session:");
                Console.WriteLine(names[nextIndex]);
                Console.WriteLine(nextDate.ToString("dd MMMM yyyy", CultureInfo.InvariantCulture));
                Console.WriteLine(nextDate.ToString("hh:mm tt", CultureInfo.InvariantCulture));
                Console.WriteLine("\nTime Remaining:");
                Console.WriteLine($"{remaining.Days} days");
                Console.WriteLine($"{remaining.Hours} hours");
            }
            else
            {
                Console.WriteLine("No upcoming sessions found.");
            }
        }

        static void CompareTwoSessionDates(string[] names, DateTime[] dates)
        {
            Console.Write("First Session: ");
            string? first = Console.ReadLine()?.Trim();
            int idx1 = SearchSession(names, first ?? string.Empty);

            Console.Write("Second Session: ");
            string? second = Console.ReadLine()?.Trim();
            int idx2 = SearchSession(names, second ?? string.Empty);

            if (idx1 == -1 || idx2 == -1)
            {
                Console.WriteLine("Session not found.");
                return;
            }

            DateTime d1 = dates[idx1];
            DateTime d2 = dates[idx2];
            TimeSpan diff = d2 > d1 ? d2 - d1 : d1 - d2;

            Console.WriteLine("\nDifference:");
            Console.WriteLine($"{(int)diff.TotalDays} days");
            Console.WriteLine($"{(int)diff.TotalHours} hours");
        }

        static void ReadAndValidateCustomDate()
        {
            DateTime dt = ReadSessionDate();
            Console.WriteLine($"Valid date: {dt:yyyy-MM-dd HH:mm}");
        }

        static DateTime ReadSessionDate()
        {
            const string format = "yyyy-MM-dd HH:mm";

            while (true)
            {
                Console.Write($"Enter date ({format}): ");
                string? input = Console.ReadLine();

                if (DateTime.TryParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                {
                    return parsedDate;
                }

                Console.WriteLine("Invalid date format.");
            }
        }

        static void SelectSessionByIndex(string[] names)
        {
            Console.Write("Enter session index: ");
            string? input = Console.ReadLine();

            if (!int.TryParse(input, out int index))
            {
                Console.WriteLine("Invalid number.");
                return;
            }

            try
            {
                Console.WriteLine($"Session: {names[index]}");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("The selected session index is out of range.");
            }
        }

        static void ValidateSessionDurationMenu()
        {
            Console.Write("Enter duration: ");
            string? input = Console.ReadLine();

            if (!int.TryParse(input, out int duration))
            {
                Console.WriteLine("Invalid number.");
                return;
            }

            try
            {
                ValidateDuration(duration);
                Console.WriteLine("Duration accepted.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Input operation finished.");
            }
        }

        static void ValidateDuration(int duration)
        {
            if (duration <= 0)
            {
                throw new ArgumentException("Duration must be greater than zero.");
            }
        }

        static string BuildReportUsingString(string[] names, DateTime[] dates, int[] durations)
        {
            string result = "";
            for (int i = 0; i < names.Length; i++)
            {
                result += $"{names[i]} - {dates[i]:dd/MM/yyyy hh:mm tt} - {durations[i]} minutes\n";
            }
            return result;
        }

        static string BuildReportUsingStringBuilder(string[] names, DateTime[] dates, int[] durations)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < names.Length; i++)
            {
                sb.Append(names[i])
                  .Append(" - ")
                  .Append(dates[i].ToString("dd/MM/yyyy hh:mm tt", CultureInfo.InvariantCulture))
                  .Append(" - ")
                  .Append(durations[i])
                  .Append(" minutes\n");
            }
            return sb.ToString();
        }
    }
}
