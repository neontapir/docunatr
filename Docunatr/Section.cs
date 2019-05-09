using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Docunatr.Core;

namespace Docunatr
{
    public class Section : ElementBase
    {
        private readonly List<Subsection> _subsections;

        public Section(Title title, params Subsection[] subsections) 
            : base(title)
        {
            _subsections = new List<Subsection>(new ReadOnlyCollection<Subsection>(subsections));
        }

        public IReadOnlyCollection<Subsection> Subsections => _subsections;

        public void Add(params Subsection[] subsections)
        {
            _subsections.AddRange(subsections);
        }

        public void AddAt(int position, Subsection subsection)
        {
            _subsections.Insert(position - 1, subsection);
        }

        public override string ToString()
        {
            return $"Section {{ Title: \"{Title}\", Subsections: [{ string.Join(", ", Subsections) }] }}";
        }
    }
}