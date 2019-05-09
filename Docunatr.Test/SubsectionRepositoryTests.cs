using Docunatr.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Docunatr.Test
{
    [TestClass]
    public class SubsectionRepositoryTests
    {
        [TestMethod]
        public void SubsectionRepository_CanStore()
        {
            var repo = new SubsectionRepository();
            var subsection = new Subsection("第一款");
            repo.Store(subsection); // without error
        }

        [TestMethod]
        public void SubsectionRepository_CanRetrieve()
        {
            var repo = new SubsectionRepository();
            var subsection = new Subsection("第一款");
            repo.Store(subsection);

            var actual = repo.Retrieve(subsection.Id);
            Assert.AreEqual(subsection, actual);
        }
    }
}