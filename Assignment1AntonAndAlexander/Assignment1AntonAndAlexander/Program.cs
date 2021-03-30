using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Assignment1AntonAndAlexander
{
    public class Movie
    {
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
    public class Program
    {
        private static List<Movie> movies = new List<Movie>
        {
            new Movie { Title = "Mr. & Mrs Smith", ReleaseDate = new DateTime(2005, 6, 10) },
            new Movie { Title = "Shark Tale", ReleaseDate = new DateTime(2004, 9, 10) },
            new Movie { Title = "I Am Legend", ReleaseDate = new DateTime(2007, 12, 14) },
            new Movie { Title = "Troy", ReleaseDate = new DateTime(2004, 5, 14) },
            new Movie { Title = "Wanted", ReleaseDate = new DateTime(2008, 6, 27) },
            new Movie { Title = "Kung Fu Panda 3", ReleaseDate = new DateTime(2016, 1, 29) },
            new Movie { Title = "I, Robot", ReleaseDate = new DateTime(2004, 7, 16) },
            new Movie { Title = "Burn After Reading", ReleaseDate = new DateTime(2008, 9, 12) },
        };

        public static void Main()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            bool done = false;
            // We declare the choice here because we want to remember the previous one on each iteration and pass it as the "defaultOption" parameter to ShowMenu, for improved usability.
            int choice = 0;
            while (!done)
            {
                choice = ShowMenu("What do you want to do?", new[]
                {
                    "List movies A-Z",
                    "List movies by release date",
                    "Find movies by year",
                    "Add movie",
                    "Delete movie",
                    "Exit"
                }, choice);
                Console.Clear();

                if (choice == 0)
                {
                    ListMoviesAlphabetically();
                }
                else if (choice == 1)
                {
                    ListMoviesByReleaseDate();
                }
                else if (choice == 2)
                {
                    FindMoviesByYear();
                }
                else if (choice == 3)
                {
                    AddMovie();
                }
                else if (choice == 4)
                {
                    DeleteMovie();
                }
                else
                {
                    done = true;
                    Console.WriteLine("Goodbye!");
                }

                Console.WriteLine();
            }
        }

        private static void ListMoviesAlphabetically()
        {
            WriteHeading("Movies A-Z");

            var sorted = movies.OrderBy(m => m.Title).ThenByDescending(m => m.ReleaseDate);
            foreach (var movie in sorted)
            {
                Console.WriteLine("- " + movie.Title + " (" + movie.ReleaseDate.Year + ")");
            }
        }

        private static void ListMoviesByReleaseDate()
        {
            WriteHeading("Movies by release date");

            var sorted = movies.OrderByDescending(m => m.ReleaseDate).ThenBy(m => m.Title);
            foreach (var movie in sorted)
            {
                Console.WriteLine("- " + movie.Title + " (" + movie.ReleaseDate.Year + ")");
            }
        }

        private static void FindMoviesByYear()
        {
            WriteHeading("Find movies by year");

            Console.Write("Year: ");
            int year = int.Parse(Console.ReadLine());

            var filtered = movies.Where(m => m.ReleaseDate.Year == year);
            var sorted = filtered.OrderBy(m => m.Title).ThenByDescending(m => m.ReleaseDate);

            Console.WriteLine();
            WriteHeading("Movies from " + year);
            foreach (var movie in sorted)
            {
                Console.WriteLine("- " + movie.Title + " (" + movie.ReleaseDate.Year + ")");
            }
        }

        private static void AddMovie()
        {
            WriteHeading("Add movie");

            Console.Write("Title: ");
            string title = Console.ReadLine();

            Console.WriteLine("Release date:");
            Console.Write("Year: ");
            int year = int.Parse(Console.ReadLine());
            Console.Write("Month (1-12): ");
            int month = int.Parse(Console.ReadLine());
            Console.Write("Day (1-31): ");
            int day = int.Parse(Console.ReadLine());

            movies.Add(new Movie
            {
                Title = title,
                ReleaseDate = new DateTime(year, month, day),
            });

            Console.Clear();
            Console.WriteLine("Movie " + title + " (" + year + ") " + "added!");
        }

        private static void DeleteMovie()
        {
            var sorted = movies.OrderBy(m => m.Title);
            var options = new List<string>();
            foreach (var movie in sorted)
            {
                options.Add(movie.Title + " (" + movie.ReleaseDate.Year + ")");
            }

            if (options.Count == 0)
            {
                Console.WriteLine("There are no movies to delete.");
                return;
            }

            int selectedIndex = ShowMenu("Delete movie", options.ToArray());
            movies.RemoveAt(selectedIndex);

            Console.Clear();
            Console.WriteLine("Movie " + options[selectedIndex] + " deleted!");
        }

        // The third parameter is the default option, which allows us to, for example, "remember" where the user was when they return to the main menu.
        // If we don't provide this parameter, it defaults to zero (i.e. highlight the first option).
        public static int ShowMenu(string prompt, string[] options, int defaultOption = 0)
        {
            if (options == null || options.Length == 0)
            {
                throw new ArgumentException("Cannot show a menu for an empty array of options.");
            }

            WriteHeading(prompt);

            int selected = defaultOption;

            // Hide the cursor that will blink after calling ReadKey.
            Console.CursorVisible = false;

            ConsoleKey? key = null;
            while (key != ConsoleKey.Enter)
            {
                // If this is not the first iteration, move the cursor to the first line of the menu.
                if (key != null)
                {
                    Console.CursorLeft = 0;
                    Console.CursorTop = Console.CursorTop - options.Length;
                }

                // Print all the options, highlighting the selected one.
                for (int i = 0; i < options.Length; i++)
                {
                    var option = options[i];
                    if (i == selected)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.WriteLine("- " + option);
                    Console.ResetColor();
                }

                // Read another key and adjust the selected value before looping to repeat all of this.
                key = Console.ReadKey().Key;
                if (key == ConsoleKey.DownArrow)
                {
                    selected = Math.Min(selected + 1, options.Length - 1);
                }
                else if (key == ConsoleKey.UpArrow)
                {
                    selected = Math.Max(selected - 1, 0);
                }
            }

            // Reset the cursor and return the selected option.
            Console.CursorVisible = true;
            return selected;
        }

        // Write a string with a line below it.
        public static void WriteHeading(string s)
        {
            Console.WriteLine(s);
            // Draw a line by repeating a hyphen as many times as the length of the prompt.
            Console.WriteLine(new string('-', s.Length));
        }
    }
}
