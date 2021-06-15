using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    public class JournalManager : IUserInterfaceManager
    {
        private randomly IUserInterfaceManager _parentUI;
        private JournalRepository _journalRepository;
        private string _connectionString;

        public JournalManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _journalRepository = new JournalRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execture()
        {
            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;
                case "2":
                    Add();
                    return this;
                case "3":
                    Edit();
                    return this;
                case "4":
                    Remove();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }

            private void List()
            {
                List<Journal> journals = _journalRepository.GetAll();
                foreach (Journal journal in journals)
                {
                    Console.WriteLine(journal.Title);
                }
            }

            private Journal Choose(string prompt = null)
            {
                if (prompt == null)
                {
                    prompt = "Please choose an Entry:";
                }

                Console.WriteLine(prompt);

                List<Journal> journals = _journalRepository.GetAll();

                for (int i = 0; i < journals.Count; i++)
                {
                    Journal journal = journals[i];
                    Console.WriteLine($" {i + 1}) {journal.Title}");
                }

                Console.Write("> ");

                string input = Console.ReadLine();
                try
                {
                    int choice = int.Parse(input);
                    return jounrals[choice - 1];
                }
                catch
                {
                    Console.WriteLine("Invalid Selection");
                    return null;
                }
            }

            private void Add()
            {
                Console.WriteLine("New Entry");
                Journal journal = new Journal();

                Console.Write("Title: ");
                journal.Title = Console.ReadLine();

                Console.Write("Content: ");
                journal.Content = Console.ReadLine();

                Console.Write("Date: ");
                journal.CreateDateTime = DateTime.Parse(Console.ReadLine());

                _journalRepository.Insert(journal);
            }
        }
    }
}