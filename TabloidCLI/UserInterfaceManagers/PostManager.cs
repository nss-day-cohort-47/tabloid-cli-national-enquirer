using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    class PostManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private PostRepository _postRepository;
        private AuthorRepository _authorRepository;
        //private BlogRepository _blogRepository;
        private string _connectionString;

        public PostManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
            _authorRepository = new AuthorRepository(connectionString);
            _connectionString = connectionString;
            // _blogRepository = new BlogRepository(connectionString);
        }
        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Post Menu");
            Console.WriteLine(" 1) List Posts");
            Console.WriteLine(" 2) Add Post");
            Console.WriteLine(" 3) Edit Post");
            Console.WriteLine(" 4) Remove Post");
            Console.WriteLine(" 5) Note Management");
            Console.WriteLine(" 0) Return to Main Menu");

            Console.Write("> ");
            string choice = Console.ReadLine();
            Console.Clear();
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
                case "5":
                    throw new NotImplementedException();
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void List()
        {
            List<Post> posts = _postRepository.GetAll();
            Console.WriteLine("Posts: ");
            Console.WriteLine("");
            foreach (Post post in posts)
            {
                Console.WriteLine($"Title: {post.Title} - URL: {post.Url}");
            }
            Console.WriteLine("");
        }

        private void Add()
        {
            List<Author> authors = _authorRepository.GetAll();
            //List<Blog> blogs = _blogRepository.GetAll();

            Author postAuthor = null;
            Blog postBlog = null;
            DateTime postDate = new DateTime();
            Console.Clear();
            string postTitle = UserPrompt("Post title: ");
            Console.Clear();
            string postUrl = UserPrompt("Post url: ");

            bool enteringDate = true;
            while (enteringDate)
            {
                Console.Write("When was this post published? (mm/dd/yyyy): ");
                string dateInput = Console.ReadLine();
                try
                {
                    postDate = DateTime.Parse(dateInput);
                    enteringDate = false;
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine("Invalid date");
                }
            }
        }
        private void Edit()
        {

        }
        private void Remove()
        {

        }
        private string UserPrompt(string message)
        {
            Console.Write(message);
            string output = Console.ReadLine();
            return output;
        }
    }
}
