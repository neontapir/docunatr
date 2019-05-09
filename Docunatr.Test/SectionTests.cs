using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Docunatr.Test
{
        [TestClass]
    public class SectionTests
    {
        [TestMethod]
        public void Section_CanCreate()
        {
            var section = new Section("new");
            Assert.IsNotNull(section);
            Assert.AreEqual(section.Title, "new");
        }

        [TestMethod]
        public void Section_IsEntity()
        {
            var section = new Section("new");
            Assert.AreNotEqual(Guid.Empty, section.Id);
        }

        [TestMethod]
        public void Section_HasSubsections()
        {
            var section = new Section("new");
            CollectionAssert.AreEquivalent(new Subsection[0], section.Subsections.ToArray());
        }

        [TestMethod]
        public void Section_Equality()
        {
            var section1 = new Section("Section");
            var section2 = new Section("Section");
            Assert.AreNotEqual(section1.Id, section2.Id);
            Assert.AreEqual(section1.Title, section2.Title);
            Assert.AreEqual(section1, section2);

            Assert.AreEqual(section1, section1);
            Assert.AreEqual(section2, section2);
            
            Assert.IsTrue(section1.IsSameAs(section1));
            Assert.IsFalse(section1.IsSameAs(section2));
        }

        [TestMethod]
        public void Section_ToString()
        {
            var subsection = new Subsection("Subsection");
            Assert.AreEqual(@"Subsection { Title: ""Subsection"" }", subsection.ToString());

            var section = new Section("Section");
            Assert.AreEqual(@"Section { Title: ""Section"", Subsections: [] }", section.ToString());

            section.Add(subsection, new Subsection("第二款"));
            Assert.AreEqual(@"Section { Title: ""Section"", Subsections: [Subsection { Title: ""Subsection"" }, Subsection { Title: ""第二款"" }] }", section.ToString());
        }

        [TestMethod]
        public void Section_AddSubsections()
        {
            var section = new Section("section");
            var subsection = new Subsection("subsection");
            section.Add(subsection);
            CollectionAssert.AreEqual(new[] {subsection}, section.Subsections.ToArray());
        }

        [TestMethod]
        public void Section_AddMultipleSubsections()
        {
            var section = new Section("section");
            var subsection1 = new Subsection("subsection 1");
            var subsection2 = new Subsection("subsection 2");
            section.Add(subsection1, subsection2);
            CollectionAssert.AreEqual(new[] {subsection1, subsection2}, section.Subsections.ToArray());
        }

        [TestMethod]
        public void Section_AddAtPosition()
        {
            var section = new Section("セクション");
            var subsection1 = new Subsection("第一款");
            var subsection2 = new Subsection("第二款");
            section.Add(subsection1, subsection2);
            CollectionAssert.AreEqual(new[] {subsection1, subsection2}, section.Subsections.ToArray());

            var subsection3 = new Subsection("第三款");
            section.AddAt(2, subsection3);
            CollectionAssert.AreEqual(new[] {subsection1, subsection3, subsection2}, section.Subsections.ToArray());
        }
    }
}