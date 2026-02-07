using System;

namespace CSharpPortfolio
{
    public class Skill
    {
        public string Name { get; set; }
        public string Proficiency { get; set; } // e.g., Beginner, Intermediate, Advanced

        public Skill(string name, string proficiency)
        {
            Name = name;
            Proficiency = proficiency;
        }

        public override string ToString()
        {
            return $"{Name} ({Proficiency})";
        }
    }
}
