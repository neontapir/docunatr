using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Docunatr.Test
{
    [TestClass]
    public class ProposalTests
    {
        [TestMethod]
        public void Proposal_CanCreate()
        {
            var proposal = new Proposal("new");
            Assert.IsNotNull(proposal);
            Assert.AreEqual(proposal.Title, "new");
        }

        [TestMethod]
        public void Proposal_IsEntity()
        {
            var proposal = new Proposal("new");
            Assert.AreNotEqual(Guid.Empty, proposal.Id);
        }

        [TestMethod]
        public void Proposal_HasSections()
        {
            var proposal = new Proposal("new");
            CollectionAssert.AreEquivalent(new Section[0], proposal.Sections.ToArray());
        }

        [TestMethod]
        public void Proposal_Equality()
        {
            var proposal1 = new Proposal("Proposal");
            var proposal2 = new Proposal("Proposal");
            Assert.AreNotEqual(proposal1.Id, proposal2.Id);
            Assert.AreEqual(proposal1.Title, proposal2.Title);
            Assert.AreEqual(proposal1, proposal2);

            Assert.AreEqual(proposal1, proposal1);
            Assert.AreEqual(proposal2, proposal2);
            
            Assert.IsTrue(proposal1.IsSameAs(proposal1));
            Assert.IsFalse(proposal1.IsSameAs(proposal2));
        }

        [TestMethod]
        public void Proposal_ToString()
        {
            var proposal = new Proposal("Proposal");
            Assert.AreEqual(@"Proposal { Title: ""Proposal"", Sections: [] }", proposal.ToString());

            proposal.Add(new Section("Section"));
            Assert.AreEqual(@"Proposal { Title: ""Proposal"", Sections: [Section { Title: ""Section"", Subsections: [] }] }", proposal.ToString());

            proposal.Add(new Section("セクション2", new Subsection("第三款")));
            Assert.AreEqual(@"Proposal { Title: ""Proposal"", Sections: [Section { Title: ""Section"", Subsections: [] }, Section { Title: ""セクション2"", Subsections: [Subsection { Title: ""第三款"" }] }] }", proposal.ToString());
        }

        [TestMethod]
        public void Proposal_AddSections()
        {
            var proposal = new Proposal("proposal");
            var section = new Section("section");
            proposal.Add(section);
            CollectionAssert.AreEqual(new[] {section}, proposal.Sections.ToArray());
        }

        [TestMethod]
        public void Proposal_AddMultipleSections()
        {
            var proposal = new Proposal("proposal");
            var section1 = new Section("section 1");
            var section2 = new Section("section 2");
            proposal.Add(section1, section2);
            CollectionAssert.AreEqual(new[] {section1, section2}, proposal.Sections.ToArray());
        }

        [TestMethod]
        public void Proposal_AddAtPosition()
        {
            var proposal = new Proposal("セクション");
            var section1 = new Section("セクション1");
            var section2 = new Section("セクション2");
            proposal.Add(section1, section2);
            CollectionAssert.AreEqual(new[] {section1, section2}, proposal.Sections.ToArray());

            var section3 = new Section("セクション3");
            proposal.AddAt(2, section3);
            CollectionAssert.AreEqual(new[] {section1, section3, section2}, proposal.Sections.ToArray());
        }
    }
}