using Docunatr.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Docunatr.Test
{
    [TestClass]
    public class SectionRepositoryTests
    {
        [TestMethod]
        public void SectionRepository_CanStore()
        {
            var repo = new SectionRepository();
            var section = new Section("секция 1");
            var subsection = new Subsection("первый подраздел");
            section.Add(subsection);
            repo.Store(section); // without error
        }

        [TestMethod]
        public void SectionRepository_CanRetrieve()
        {
            var repo = new SectionRepository();
            var section = new Section("секция 1");
            var subsection1 = new Subsection("первый подраздел");
            var subsection2 = new Subsection("второй подраздел");
            section.Add(subsection1, subsection2);
            repo.Store(section);

            var actual = repo.Retrieve(section.Id);
            Assert.AreEqual(section, actual);
        }
    }
}