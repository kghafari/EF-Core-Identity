using System;
using System.Collections.Generic;

namespace Day28_Identity_CodeAlong.Models
{
    public partial class Pets
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool Cute { get; set; }
        public string OwnerId { get; set; }

        public virtual AspNetUsers Owner { get; set; }
    }
}
