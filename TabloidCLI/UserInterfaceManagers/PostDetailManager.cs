using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    internal class PostDetailManager : IUserInterfaceManager
    {
        private IUserInterfaceManager _parentUI;
        private PostRepository _postRepository;
        private int _postId;
        private TagRepository _tagRepository;

        public PostDetailManager(IUserInterfaceManager parentUI, string connectionString, int postId)
        {
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
            _postId = postId;
            _tagRepository = new TagRepository(connectionString);
        }
        //if this looks familiar to AuthorDetailManager that's good, because I copied this from there and bent it to my will
        public IUserInterfaceManager Execute()
        {
            Post post = _postRepository.Get(_postId);
            Console.WriteLine($"{post.Title} Details");
            Console.WriteLine(" 1) View");
            Console.WriteLine(" 2) Add Tag");
            Console.WriteLine(" 3) Remove Tag");
            Console.WriteLine(" 4) Note Management");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    View();
                    return this;
                case "2":
                    AddTag();
                    return this;
                case "3":
                    RemoveTag();
                    return this;
                case "4":
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void View()
        {
            Post post = _postRepository.Get(_postId);
            Console.WriteLine($"Post title: {post.Title}");
            Console.WriteLine($"Post URL: {post.Url}");
            Console.WriteLine($"Post publication date: {post.PublishDateTime.ToString("D")}");
            Console.Write("Tags:");
            foreach (Tag tag in post.Tags)
            {
                Console.Write(" " + tag);
            }
            Console.WriteLine(@"
                                        _.--^^^--,
                                    .'          `\
  .-^^^^^^-.                      .'              |
 /          '.                   /            .-._/
|             `.                |             |
 \              \          .-._ |          _   \
  `^^'-.         \_.-.     \   `          ( \__/
        |             )     '=.       .,   \
       /             (         \     /  \  /
     /`               `\        |   /    `'
     '..-`\        _.-. `\ _.__/   .=.
          |  _    / \  '.-`    `-.'  /
          \_/ |  |   './ _     _  \.'
               '-'    | /       \ |
                      |  .-. .-.  |
                      \ / o| |o \ /
                       |   / \   |    Did you think you could escape me?
                      / `^`   `^` \
                     /             \
                    | '._.'         \
                    |  /             |
                     \ |             |
                      ||    _    _   /
                      /|\  (_\  /_) /
                      \ \'._  ` '_.'
                       `^^` `^^^`");
            Console.Write("Smash your keyboard to move on, but not too hard.");
            Console.ReadKey();
            Console.Clear();
        }
        private void AddTag()
        {
            Post post = _postRepository.Get(_postId);

            Console.WriteLine($"Choose a tag to connect to {post.Title}?");
            List<Tag> tags = _tagRepository.GetAll();

            for (int i = 0; i < tags.Count; i++)
            {
                Tag tag = tags[i];
                Console.WriteLine($" {i + 1}) {tag.Name}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                Tag tag = tags[choice - 1];
                _postRepository.InsertTag(post, tag);
                Console.WriteLine($"{tag.Name} has been successfully paired to {post.Title}");
                Console.WriteLine();
                Console.WriteLine("Press enter to continue.");
                Console.ReadLine();
                Console.Clear();
            }
            catch (Exception)
            {
                Console.WriteLine("Once again you managed to pick an option that doesn't exist, go away.");
                Console.WriteLine();
                Console.WriteLine("Smash your keyboard to move on");
                Console.ReadLine();
                Console.Clear();
            }

        }
        private void RemoveTag()
        {
            Post post = _postRepository.Get(_postId);

            Console.WriteLine($"Which tag would you like to remove from {post.Title}?");
            List<Tag> tags = post.Tags;

            for (int i = 0; i < tags.Count; i++)
            {
                Tag tag = tags[i];
                Console.WriteLine($" {i + 1}) {tag.Name}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                Tag tag = tags[choice - 1];
                _postRepository.DeleteTag(post.Id, tag.Id);
                Console.WriteLine($"Successfully removed ({tag.Name}) tag from {post.Title}");
                Console.WriteLine();
                Console.WriteLine("Press enter to continue.");
                Console.ReadLine();
                Console.Clear();
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid Selection. Won't remove any tags.");
                Console.WriteLine();
                Console.WriteLine("Press enter to continue.");
                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}