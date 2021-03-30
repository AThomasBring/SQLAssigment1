﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

namespace Assignment1
{
    public class Program
    {
        // We make the connection static because we need it in almost every method.
        private static SqlConnection connection;

        public static void Main()
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            // Connect to the database.
            connection = new SqlConnection(@"Data Source=(local)\SQLExpress;Initial Catalog=Assignment1;Integrated Security=SSPI;");
            connection.Open();

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

            // TODO: Get movies from database.
        }

        private static void ListMoviesByReleaseDate()
        {
            WriteHeading("Movies by release date");

            // TODO: Get movies from database.
        }

        private static void FindMoviesByYear()
        {
            WriteHeading("Find movies by year");

            // TODO: Get movies from database.
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

            // TODO: Add movie to database.
        }

        private static void DeleteMovie()
        {
            // TODO: Delete movie from database.
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