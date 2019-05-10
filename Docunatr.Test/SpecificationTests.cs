using System;
using System.Linq;
using System.Linq.Expressions;
using Docunatr.Core;
using Docunatr.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Docunatr.Test
{
    [TestClass]
    public class SpecificationTests
    {
        public class IsLowercaseSpecification : Specification<string>
        {
            public override Expression<Func<string, bool>> ToExpression()
            {
                return item => item == item.ToLowerInvariant();
            }
        }

        public class StartsWithFSpecification : Specification<string>
        {
            public override Expression<Func<string, bool>> ToExpression()
            {
                return item => item.Length > 0 && item.ToLowerInvariant()[0] == 'f';
            }
        }

        [TestMethod]
        public void CanFilterBySpecification()
        {
            Assert.IsTrue(new IsLowercaseSpecification().IsSatisfiedBy("foo"));
            Assert.IsFalse(new IsLowercaseSpecification().IsSatisfiedBy("Bar"));

            Assert.IsTrue(new StartsWithFSpecification().IsSatisfiedBy("foo"));
            Assert.IsTrue(new StartsWithFSpecification().IsSatisfiedBy("Foo"));
            Assert.IsFalse(new StartsWithFSpecification().IsSatisfiedBy("bar"));
        }

        [TestMethod]
        public void CanCombineSpecificationsUsingAnd()
        {
            Specification<string> isLowercase = new IsLowercaseSpecification();
            Specification<string> startsWithF = new StartsWithFSpecification();

            Assert.IsTrue(isLowercase.And(startsWithF).IsSatisfiedBy("foo"), "foo (both)");
            Assert.IsFalse(isLowercase.And(startsWithF).IsSatisfiedBy("fl4M1ng0"), "fl4M1ng0 (starts with f)");
            Assert.IsFalse(isLowercase.And(startsWithF).IsSatisfiedBy("bar"), "bar (lowercase)");
            Assert.IsFalse(isLowercase.And(startsWithF).IsSatisfiedBy("Bassoon"), "Bassoon (neither)");
        }

        [TestMethod]
        public void CanCombineSpecificationsUsingOr()
        {
            Specification<string> isLowercase = new IsLowercaseSpecification();
            Specification<string> startsWithF = new StartsWithFSpecification();

            Assert.IsTrue(isLowercase.Or(startsWithF).IsSatisfiedBy("foo"), "foo (both)");
            Assert.IsTrue(isLowercase.Or(startsWithF).IsSatisfiedBy("flaMINGO"), "fl4M1ng0 (starts with f)");
            Assert.IsTrue(isLowercase.Or(startsWithF).IsSatisfiedBy("bar"), "bar (lowercase)");
            Assert.IsFalse(isLowercase.Or(startsWithF).IsSatisfiedBy("Bassoon"), "Bassoon (neither)");
        }

        [TestMethod]
        public void CanFilterByNotSpecification()
        {
            Specification<string> isLowercase = new IsLowercaseSpecification();
            Specification<string> isNotJustLowercase = new NotSpecification<string>(isLowercase);

            Assert.IsTrue(isLowercase.IsSatisfiedBy("foo"));
            Assert.IsFalse(isLowercase.IsSatisfiedBy("Bar"));

            Assert.IsFalse(isNotJustLowercase.IsSatisfiedBy("foo"));
            Assert.IsTrue(isNotJustLowercase.IsSatisfiedBy("Bar"));
        }

        [TestMethod]
        public void CanCombineSpecificationsUsingXor()
        {
            Specification<string> isLowercase = new IsLowercaseSpecification();
            Specification<string> startsWithF = new StartsWithFSpecification();

            Assert.IsFalse(isLowercase.Xor(startsWithF).IsSatisfiedBy("foo"), "foo (both)");
            Assert.IsTrue(isLowercase.Xor(startsWithF).IsSatisfiedBy("fl4M1ng0"), "fl4M1ng0 (starts with f)");
            Assert.IsTrue(isLowercase.Xor(startsWithF).IsSatisfiedBy("bar"), "bar (lowercase)");
            Assert.IsFalse(isLowercase.Xor(startsWithF).IsSatisfiedBy("Bassoon"), "Bassoon (neither)");
        }
    }
}