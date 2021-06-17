using TabloidCLI.UserInterfaceManagers;
using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Repositories;

namespace TabloidCLI
{
    class Program 
    {
            private ColorRepository _colorRepository;
            private string _connectionString;

        static void Main(string[] args)
        {
            Console.Clear();
            string choice = Color.ColorChoice;
            switch (choice)
            {
                case "1":
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Clear();
                    return this;
                case "2":
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Clear();
                    return this;
                case "3":
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Clear();
                    return this;
                case "4":
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Clear();
                    return this;
                case "5":
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Clear();
                    return this;
                case "6":
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Clear();
                    return this;
                case "7":
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Clear();
                    return this;
                case "8":
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Clear();
                    return this;
                case "9":
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Clear();
                    return this;
                case "10":
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Clear();
                    return this;
                case "11":
                    Console.ResetColor();
                    Console.Clear();
                    return this;

                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
            Console.WriteLine("Welcome to The National Enquirer, the best tabloid ever created");
            Console.WriteLine("===============================================================");
            // MainMenuManager implements the IUserInterfaceManager interface
            IUserInterfaceManager ui = new MainMenuManager();
            while (ui != null)
            {
                // Each call to Execute will return the next IUserInterfaceManager we should execute
                // When it returns null, we should exit the program;
                ui = ui.Execute();
            }
        }
    }
}
