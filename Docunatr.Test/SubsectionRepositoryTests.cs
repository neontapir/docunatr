using System;
using System.Linq;
using System.Linq.Expressions;
using Docunatr.Core;
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

        public class SubsectionHasTitle : Specification<Subsection>
        {
            private readonly Title _title;

            public SubsectionHasTitle(string title)
            {
                _title = new Title(title);
            }

            public override Expression<Func<Subsection, bool>> ToExpression()
            {
                return subsection => subsection.Title == _title;
            }
        }

        [TestMethod]
        public void SubsectionRepository_CanRetrieveBySpecification()
        {
            var repo = new SubsectionRepository();
            repo.Empty();

            var subsection = new Subsection("第一款");
            repo.Store(subsection);

            var specification = new SubsectionHasTitle("第一款");
            var actual = repo.Retrieve(specification);
            Assert.AreEqual(subsection, actual.Single());
        }

        public class SubsectionTitleContains : Specification<Subsection>
        {
            private readonly string _titleFragment;

            public SubsectionTitleContains(string titleFragment)
            {
                _titleFragment = titleFragment;
            }

            public override Expression<Func<Subsection, bool>> ToExpression()
            {
                return subsection => subsection.Title.Value.Contains(_titleFragment);
            }
        }

        [TestMethod]
        public void SubsectionRepository_CanRetrieveMultipleItemsBySpecification()
        {
            var repo = new SubsectionRepository();
            repo.Empty();

            var subsection1 = new Subsection("第一款");
            repo.Store(subsection1);
            var subsection2 = new Subsection("第二款");
            repo.Store(subsection2);

            var specification = new SubsectionTitleContains("第");
            var actual = repo.Retrieve(specification);
            CollectionAssert.AreEquivalent(new[] {subsection1, subsection2}, actual.ToArray());
        }

        [TestMethod]
        public void SubsectionRepository_EmptyIfNoItemsFoundBySpecification()
        {
            var repo = new SubsectionRepository();
            repo.Empty();

            var subsection1 = new Subsection("第一款");
            repo.Store(subsection1);
            var subsection2 = new Subsection("第二款");
            repo.Store(subsection2);

            var specification = new SubsectionTitleContains("Subsection");
            var actual = repo.Retrieve(specification);
            CollectionAssert.AreEquivalent(new Subsection[0] , actual.ToArray());
        }
    }
}