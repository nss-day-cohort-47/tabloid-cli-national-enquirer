using System;
using TabloidCLI.Models;
using System.Collections.Generic;

namespace TabloidCLI.UserInterfaceManagers
{
    internal class SearchManager : IUserInterfaceManager
    {
        private IUserInterfaceManager _parentUI;
        private TagRepository _tagRepository;

        public SearchManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _tagRepository = new TagRepository(connectionString);
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Search Menu");
            Console.WriteLine(" 1) Search Blogs");
            Console.WriteLine(" 2) Search Authors");
            Console.WriteLine(" 3) Search Posts");
            Console.WriteLine(" 4) Search All");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    SearchBlogs();
                    return this;
                case "2":
                    SearchAuthors();
                    return this;
                case "3":
                    SearchPosts();
                    return this;
                case "4":
                    SearchAll();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void SearchAuthors()
        {
            Console.WriteLine("Please select a tag to search for: ");
            List<Tag> tags = _tagRepository.GetAll();
            for (int i = 0; i < tags.Count; i++)
            {
                Tag tag = tags[i];
                Console.WriteLine($" {i + 1}) {tag.Name}");
            }
            Console.Write("> ");
            int tagId = int.Parse(Console.ReadLine());
            string tagName = tags[tagId - 1].Name;

            SearchResults<Author> results = _tagRepository.SearchAuthors(tagName);

            if (results.NoResultsFound)
            {
                Console.WriteLine($"No results for {tagName}");
            }
            else
            {
                results.Display();
            }
        }

        private void SearchBlogs()
        {
            Console.WriteLine("Please select a tag to search for: ");
            List<Tag> tags = _tagRepository.GetAll();
            for (int i = 0; i < tags.Count; i++)
            {
                Tag tag = tags[i];
                Console.WriteLine($" {i + 1}) {tag.Name}");
            }
                        Console.Write("> ");
            int tagId = int.Parse(Console.ReadLine());
            string tagName = tags[tagId - 1].Name;

            SearchResults<Blog> results = _tagRepository.SearchBlogs(tagName);

            if (results.NoResultsFound)
            {
                Console.WriteLine($"No results for {tagName}");
            }
            else
            {
                results.Display();
            }
        }

        private void SearchPosts()
        {
            Console.WriteLine("Please select a tag to search for: ");
            List<Tag> tags = _tagRepository.GetAll();
            for (int i = 0; i < tags.Count; i++)
            {
                Tag tag = tags[i];
                Console.WriteLine($" {i + 1}) {tag.Name}");
            }
                        Console.Write("> ");
            int tagId = int.Parse(Console.ReadLine());
            string tagName = tags[tagId - 1].Name;
            SearchResults<Post> results = _tagRepository.SearchPosts(tagName);

            if (results.NoResultsFound)
            {
                Console.WriteLine($"No results for {tagName}");
            }
            else
            {
                results.Display();
            }
        }

        // The user woud like to view all objects attached to a specific tag.
        private void SearchAll()
        {
            // This is taken from above again to have a list of tags for a user to choose from 
            Console.WriteLine("Please select a tag to search for: ");
            List<Tag> tags = _tagRepository.GetAll();
            for (int i = 0; i < tags.Count; i++)
            {
                Tag tag = tags[i];
                Console.WriteLine($" {i + 1}) {tag.Name}");
            }
                        Console.Write("> ");
            int tagId = int.Parse(Console.ReadLine());
            string tagName = tags[tagId - 1].Name;

            // We need to call the already defined searches to be triggered in the search. 
            // But we need to give them a unique variable than just results like above.
            // It will look through the tag and find what ever is attached to either a blog, post or author.
            SearchResults<Blog> blogResults = _tagRepository.SearchBlogs(tagName);
            SearchResults<Author> authorResults = _tagRepository.SearchAuthors(tagName);
            SearchResults<Post> postResults = _tagRepository.SearchPosts(tagName);

            // If nothing is attached to that specific tag let the user know.
            if (blogResults.NoResultsFound && authorResults.NoResultsFound && postResults.NoResultsFound)
            {
                Console.WriteLine($"No results for {tagName}");
            }
            else
            {
                // Ternary that will show us that that tag doesnt match anything in our database 
                // Or will show those objects attached to the tag. (May need a little more work)
                // If no results are found then let the user know that it doesnt have one, if there is one
                // let the user know that that tag has a match and it will be displayed underneath the WriteLine.
                Console.WriteLine(blogResults.NoResultsFound ? "No matching tags in Blogs" : $"{tagName}: in Blogs");
                blogResults.Display();

                Console.WriteLine(authorResults.NoResultsFound ? "No matching tags in Authors" : $"{tagName}: in Authors");
                authorResults.Display();

                Console.WriteLine(postResults.NoResultsFound ? "No matching tags in Posts" : $"{tagName}: in Posts");
                postResults.Display();
            }
        }
    }
}