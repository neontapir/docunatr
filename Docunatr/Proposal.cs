using System.Collections.Generic;
using System.Collections.ObjectModel;
using Docunatr.Core;

namespace Docunatr
{
    public class Proposal : ElementBase
    {
        private readonly List<Section> _sections;

        public Proposal(Title title, params Section[] sections) 
            : base(title)
        {
            _sections = new List<Section>(new ReadOnlyCollection<Section>(sections));
        }

        public IReadOnlyCollection<Section> Sections => _sections;

        public void Add(params Section[] section)
        {
            _sections.AddRange(section);
        }

        public void AddAt(int position, Section section)
        {
            _sections.Insert(position - 1, section);
        }

        public override string ToString()
        {
            return $"Proposal {{ Title: \"{Title}\", Sections: [{ string.Join(", ", Sections) }] }}";
        }
    }
}