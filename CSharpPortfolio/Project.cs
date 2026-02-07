using System;
using System.Collections.Generic;

namespace CSharpPortfolio
{
    public class Project
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Skill> Technologies { get; set; } = new List<Skill>();

        public Project(string title, string description)
        {
            Title = title;
            Description = description;
        }

        public void AddTechnology(Skill skill)
        {
            Technologies.Add(skill);
        }

        public override string ToString()
        {
            string techList = string.Join(", ", Technologies);
            return $"Title: {Title}\nDescription: {Description}\nTechnologies: {techList}";
        }
    }
}
