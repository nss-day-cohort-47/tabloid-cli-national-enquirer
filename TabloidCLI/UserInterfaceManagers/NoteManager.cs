using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;


namespace TabloidCLI.UserInterfaceManagers
{
    public class NoteManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private NoteRepository _noteRepository;
        private PostRepository _postRepository;
        private string _connectionString;
        private string connectionString;

        public NoteManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _noteRepository = new NoteRepository(connectionString);
            _postRepository = new PostRepository(connectionString);
            _connectionString = connectionString;
        }

        public NoteManager(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
                
                Console.WriteLine("Note Menu");
                Console.WriteLine(" 1) List Notes");
                Console.WriteLine(" 2) Add Note");
                Console.WriteLine(" 3) Remove Note");
                Console.WriteLine(" 0) Go Back");

                Console.Write("> ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                    Console.Clear();
                    List();
                        return this;
                    case "2":
                    Console.Clear();
                    Add();
                        return this;
                    case "3":
                    Console.Clear();
                    Remove();
                        return this;
                    case "0":
                    Console.Clear();
                    return _parentUI;
                    default:
                        Console.WriteLine("Invalid Selection");
                        return this;
                }
            
        }

        private void List()
        {
            List<Note> notes = _noteRepository.GetAll();
            foreach (Note note in notes)
            {
                Console.WriteLine(note);
            }
        }
        private Note Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose an Note:";
            }

            Console.WriteLine(prompt);

            List<Note> notes = _noteRepository.GetAll();

            for (int i = 0; i < notes.Count; i++)
            {
                Note note = notes[i];
                Console.WriteLine($" {i + 1}) {note.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return notes[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private void Add()
        {
            Console.WriteLine("New Note");
            Note note = new Note();

            Console.Write("Title: ");
            note.Title = Console.ReadLine();

            Console.Write("Content: ");
            note.Content = Console.ReadLine();

            note.CreateDateTime = System.DateTime.Now;

            Console.WriteLine($"Which post would you like to add to {note.Title}?");

            Post Choose(string prompt = null)
            {
                if (prompt == null)
                {
                    prompt = "Please choose a post:";
                }

                Console.WriteLine(prompt);

                List<Post> posts = _postRepository.GetAll();

                for (int i = 0; i < posts.Count; i++)
                {
                    Post post = posts[i];
                    Console.WriteLine($" {i + 1}) {post.Title}");
                }
                Console.Write("> ");

                string input = Console.ReadLine();
                try
                {
                    int choice = int.Parse(input);
                    return posts[choice - 1];
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Invalid Selection");
                    return null;
                }
            }
            note.PostId = Choose();


            _noteRepository.Insert(note);


        }



        private void Remove()
        {
            Note noteToDelete = Choose("Which note would you like to remove?");
            if (noteToDelete != null)
            {
                _noteRepository.Delete(noteToDelete.Id);
            }
        }
    }
}