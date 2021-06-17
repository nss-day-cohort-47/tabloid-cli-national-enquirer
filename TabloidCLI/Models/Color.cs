using System;
using System.Collections.Generic;
using System.Text;

namespace TabloidCLI.Models
{
    public class Color
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}