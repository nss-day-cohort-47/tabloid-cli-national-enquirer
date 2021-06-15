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
        private BlogRepository _blogRepository;
        private string _connectionString;

        public PostManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
            _authorRepository = new AuthorRepository(connectionString);
            _connectionString = connectionString;
            _blogRepository = new BlogRepository(connectionString);
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
                    Console.WriteLine("Pick something else, cause that ain't it chief");
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
                Console.WriteLine($"Title: {post.Title}  URL: {post.Url}");
            }
            Console.WriteLine("");
        }

        private void Add()
        {
            List<Author> authors = _authorRepository.GetAll();
            List<Blog> blogs = _blogRepository.GetAll();

            Author postAuthor = null;
            Blog postBlog = null;
            DateTime postDate = new DateTime();
            Console.Clear();
            string postTitle = userprompt("Input the post's title");
            Console.Clear();
            string postUrl = userprompt("Input the post's url");

            bool enteringDate = true;
            while (enteringDate)
            {
                Console.WriteLine("What is the post's publication date? Cannot be from before 1300 BCE (mm/dd/yyyy)");
                string dateInput = Console.ReadLine();
                try
                {
                    postDate = DateTime.Parse(dateInput);
                    enteringDate = false;
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine("Pick something date, cause that ain't it chief");
                }
            }

            bool enteringAuthor = true;
            while (enteringAuthor)
            {
                Console.WriteLine("Please choose the post's Author:");

                for (int i = 0; i < authors.Count; i++)
                {
                    Author author = authors[i];
                    Console.WriteLine($"{author.Id} - {author.FullName}");
                }
                string input = userprompt("> ");
                try
                {
                    Console.Clear();
                    int choice = int.Parse(input);
                    postAuthor = authors.Find(a => a.Id == choice);
                    enteringAuthor = false;
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine("Pick something else, cause that ain't it chief");
                }
            }

            bool enteringBlog = true;
            while (enteringBlog)
            {
                Console.WriteLine("Please choose the post's Blog:");

                for (int i = 0; i < blogs.Count; i++)
                {
                    Blog blog = blogs[i];
                    Console.WriteLine($"{blog.Id} - {blog.Title}");
                }
                Console.Write("> ");
                string input = Console.ReadLine();
                try
                {
                    Console.Clear();
                    int choice = int.Parse(input);
                    postBlog = blogs.Find(b => b.Id == choice);
                    enteringBlog = false;
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine("Pick something else, cause that ain't it chief");
                }
            }

            Post newPost = new Post()
            {
                Title = postTitle,
                Url = postUrl,
                Blog = postBlog,
                Author = postAuthor,
                PublishDateTime = postDate
            };

            _postRepository.Insert(newPost);

            Console.Clear();
            Console.WriteLine($"\"{newPost.Title}\" has been added succesfully, go celebrate!");
            Console.WriteLine();
        }
        private void Edit()
        {
            Console.Clear();


            Post postToEdit = Choose("Select a post to edit");
            if (postToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            string newTitle = userprompt("Input a new title or leave blank for it to be unchanged");
            if (!string.IsNullOrWhiteSpace(newTitle))
            {
                postToEdit.Title = newTitle;
            }

            string newUrl = userprompt("Input a new URL or leave blank for it to be unchanged");
            if (!string.IsNullOrWhiteSpace(newUrl))
            {
                postToEdit.Url = newUrl;
            }

            bool enteringDate = true;
            while (enteringDate)
            {
                DateTime newDate = new DateTime();
                string dateInput = userprompt("What is the post's publication date? Cannot be from before 1300 BCE (mm/dd/yyyy) - blank to leave unchanged: ");
                if (string.IsNullOrWhiteSpace(dateInput))
                {
                    enteringDate = false;
                }
                else
                {
                    try
                    {
                        newDate = DateTime.Parse(dateInput);
                        postToEdit.PublishDateTime = newDate;
                        enteringDate = false;
                    }
                    catch (Exception ex)
                    {
                        Console.Clear();
                        Console.WriteLine("What did I say about formating the date? Listen next time please");
                    }
                }
            }

            bool enteringAuthor = true;
            List<Author> authors = _authorRepository.GetAll();
            while (enteringAuthor)
            {
                Console.WriteLine("Input the new author or leave blank for it to be unchanged");

                for (int i = 0; i < authors.Count; i++)
                {
                    Author author = authors[i];
                    Console.WriteLine($"{author.Id} - {author.FullName}");
                }

                string input = userprompt("> ");
                if (string.IsNullOrWhiteSpace(input))
                {
                    enteringAuthor = false;
                }
                else
                {
                    try
                    {
                        Console.Clear();
                        int choice = int.Parse(input);
                        postToEdit.Author = authors.Find(a => a.Id == choice);
                        enteringAuthor = false;
                    }
                    catch (Exception ex)
                    {
                        Console.Clear();
                        Console.WriteLine("Apparently that author doesn't exist, don't tell them that though. Might hurt their feelings");
                    }
                }
            }

            bool enteringBlog = true;
            List<Blog> blogs = _blogRepository.GetAll();
            while (enteringBlog)
            {
                Console.WriteLine("Input the new blog or leave blank for it to be unchanged");

                for (int i = 0; i < blogs.Count; i++)
                {
                    Blog blog = blogs[i];
                    Console.WriteLine($"{blog.Id} - {blog.Title}");
                }
                Console.Write("> ");
                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    enteringBlog = false;
                }
                else
                {

                    try
                    {
                        Console.Clear();
                        int choice = int.Parse(input);
                        postToEdit.Blog = blogs.Find(b => b.Id == choice);
                        enteringBlog = false;
                    }
                    catch (Exception ex)
                    {
                        Console.Clear();
                        Console.WriteLine("Invalid Selection");
                    }
                }
            }

            _postRepository.Update(postToEdit);
            Console.Clear();
            Console.WriteLine($"Your edits for \"{postToEdit.Title}\" have gone through. Rejoice.");
            Console.WriteLine();
        }
        private void Remove()
        {
            Console.Clear();
            Post postToDelete = Choose("Which post would you like to not exist?");
            if (postToDelete != null)
            {
                _postRepository.Delete(postToDelete.Id);
            }
            Console.Clear();
            Console.WriteLine($"{postToDelete.Title} has been removed, I hope you didn't just delete someone else's work.");
            Console.WriteLine();
        }
        private Post Choose(string prompt = null)
        {
            Console.Clear();
            if (prompt == null)
            {
                prompt = "Select a post please";
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
                Console.Clear();
                int choice = int.Parse(input);
                return posts[choice - 1];
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine("Try again. That was an invalid selection");
                return null;
            }
        }
        private string userprompt(string message)
        {
            Console.WriteLine(message);
            string output = Console.ReadLine();
            return output;
        }
    }
}