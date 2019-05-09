using Docunatr.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Docunatr.Test
{
    [TestClass]
    public class ProposalRepositoryTests
    {
        [TestMethod]
        public void ProposalRepository_CanStore()
        {
            var repo = new ProposalRepository();
            var proposal = new Proposal("мое славное предложение");
            var section = new Section("секция 1", new Subsection("первый подраздел"));
            proposal.Add(section);
            repo.Store(proposal); // without error
        }

        [TestMethod]
        public void ProposalRepository_CanRetrieve()
        {
            var repo = new ProposalRepository();
            var proposal = new Proposal("мое славное предложение");
            var section = new Section("секция 1", new Subsection("первый подраздел"));
            proposal.Add(section);
            repo.Store(proposal); // without error

            var actual = repo.Retrieve(proposal.Id);
            Assert.AreEqual(proposal, actual);
        }
    }
}