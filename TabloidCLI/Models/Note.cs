using System;
using System.Collections.Generic;


namespace TabloidCLI.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateDateTime { get; set; }

        public List<Note> Notes { get; set; } = new List<Note>();

        public Post PostId { get; set; }

        public override string ToString()
        {
            return $" {CreateDateTime} {Title} ({Content})";
        }



    }
}