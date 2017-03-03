using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TDDApp.NUnit.Tests
{
    [TestFixture]
    public class AssertSyntaxTests : AssertionHelper
    {
        #region "Simple constraints tests"

        [Test]
        public void IsNull()
        {
            object item = null;

            //classic syntac
            Assert.IsNull(item);

            //constraint syntax
            Assert.That(item, Is.Null);
        }

        [Test]
        public void IsNotNull()
        {
            var number = 50;

            //classic syntax
            Assert.IsNotNull(number);

            //constraint syntax
            Assert.That(number, Is.Not.Null);
        }

        /// <summary>
        /// Step-1: Arrange
        /// Step-2: Act
        /// Step-3: Assert
        /// </summary>
        [Test]
        public void IsTrue()
        {
            //Arrange
            var expectedResult = 10;
            var result = 5 + 5;

            //Act
            var output = result == expectedResult;

            //Assert
            //classic 
            Assert.IsTrue(output);

            //constraints syntax
            Assert.That(output, Is.True);
        }

        [Test]
        public void IsFalse()
        {
            //Arrange
            var result = 2 + 5;
            var expectedResult = 10;

            //Act
            var output = result == expectedResult;

            //Assert
            //Classic Syntax
            Assert.IsFalse(output);

            //Constraint syntax
            Assert.That(output, Is.False);
        }


        /// <summary>
        /// Is Not a Number
        /// </summary>
        [Test]
        public void IsNan()
        {
            var doubleResult = double.NaN;
            var floatResult = float.NaN;

            //classic syntax
            Assert.IsNaN(doubleResult);
            Assert.IsNaN(floatResult);

            //Constraint syntax
            Assert.That(doubleResult, Is.NaN);
            Assert.That(floatResult, Is.NaN);
        }

        [Test]
        public void EmptyStringTest()
        {
            var emptyValue = string.Empty;
            var notEmptyValue = "Unit Testing is Awesome";

            //Classic
            Assert.IsEmpty(emptyValue);
            Assert.IsNotEmpty(notEmptyValue);

            //Constraint syntax
            Assert.That(emptyValue, Is.Empty);
            Assert.That(notEmptyValue, Is.Not.Empty);
        }

        [Test]
        public void EmptyCollectionTest()
        {
            var emptyList = new List<int>();
            var numberList = new List<int> { 1, 2, 3 };

            //Classic syntax
            Assert.IsEmpty(emptyList);
            Assert.IsNotEmpty(numberList);

            //Constraint syntax
            Assert.That(emptyList, Is.Empty);
            Assert.That(numberList, Is.Not.Empty);
        }
        #endregion

        #region "Type constraints Tests"
        [Test]
        public void ExactTypeTest()
        {
            //classic syntax
            Assert.AreEqual(typeof(string), "Test".GetType());
            Assert.AreEqual("System.String", "Test".GetType().FullName);
            Assert.AreNotEqual(typeof(int), "Test".GetType());

            //Constraint syntax
            Assert.That("Test", Is.TypeOf(typeof(string)));
            //Assert.That("Test", Not.TypeOf(typeof(string)));
            Assert.That(5, Not.TypeOf(typeof(string)));
        }

        [Test]
        public void InstanceOfTest()
        {
            //classic
            Assert.IsInstanceOf(typeof(string), "Test");
            Assert.IsNotInstanceOf(typeof(int), "Test");

            //constraint
            Assert.That("Test", Is.InstanceOf(typeof(string)));
            Assert.That("Test", Not.InstanceOf(typeof(int)));
        }

        public void AssignableFromTypeTest()
        {
            //classic
            Assert.IsAssignableFrom(typeof(string), "Test");
            Assert.IsNotAssignableFrom(typeof(int), "Test");

            //constraints
            Assert.That("Test", Is.AssignableFrom(typeof(string)));
            Assert.That("Test", Not.AssignableFrom(typeof(int)));
        }

        #endregion


        #region "String contraints tests"
        [Test]
        public void SubstringTest()
        {
            var phrase = "Hello Test Driven Development!";
            var array = new string[] { "test", "driven", "development" };

            //classic
            StringAssert.Contains("Test", phrase);

            //contraints
            Assert.That(phrase, Does.Contain("Test"));
            Assert.That(phrase, Does.Contain("TEST").IgnoreCase);
            Assert.That(phrase, Does.Not.Contain("Bye"));
            Assert.That(array, Is.All.Contain("e"));
        }

        [Test]
        public void StartsWithTest()
        {
            var phrase = "Hello Test Driven Development!";
            var array = new string[] { "Hello!", "Hi!", "Hola!" };

            //classic
            StringAssert.StartsWith("Hello", phrase);

            //contraints
            Assert.That(phrase, Does.StartWith("Hello"));
            Assert.That(phrase, Does.StartWith("HELLO").IgnoreCase);
            Assert.That(phrase, Does.Not.StartWith("HI").IgnoreCase);
            Assert.That(array, Is.All.StartWith("h").IgnoreCase);
        }

        [Test]
        public void EndsWithTest()
        {
            var phrase = "Hello Test Driven Development!";
            var array = new string[] { "Hello!", "Hi!", "Hola!" };

            //classic 
            StringAssert.DoesNotEndWith("lo", phrase);
            StringAssert.EndsWith("Development!", phrase);

            //contraints syntax
            Assert.That(phrase, Does.EndWith("Development!"));
            Assert.That(phrase, Does.EndWith("DEVELOPMENT!").IgnoreCase);
            Assert.That(phrase, Does.Not.EndWith("WORLD").IgnoreCase);
            Assert.That(array, Is.All.EndWith("!"));
        }

        [Test]
        public void EqualIgnoreCaseTest()
        {
            //string equal
            var phrase = "Hello Unit Testing!";

            //two arrays or objects are equal
            string[] arr1 = new string[] { "ONE", "Two", "THRee" };
            string[] arr2 = new string[] { "one", "two", "three" };

            //classic
            StringAssert.AreEqualIgnoringCase("Hello UNIT testing!", phrase);

            //constraints syntax
            Assert.That(phrase, Is.EqualTo("HELLO unit TESting!").IgnoreCase);
            Assert.That(phrase, Is.Not.EqualTo("HELLO DEVELOPMENT!"));
            Assert.That(arr1, Is.EqualTo(arr2).IgnoreCase);
            Assert.That(new string[] { "HELLO", "hello", "hELLo" }, All.EqualTo("Hello").IgnoreCase);
        }

        [Test]
        public void RegularExpressionTest()
        {
            var phrase = "Tit for tat!";
            var quotes = new string[] { "Never say never", "It's never too late", "Nevermore!" };

            //classic
            StringAssert.IsMatch("Tit for", phrase);
            StringAssert.IsMatch("Ti.*tat!", phrase);

            //constraint syntax
            Assert.That(phrase, Does.Match("Tit FOR").IgnoreCase);
            Assert.That(phrase, Does.Match("Ti.*tat!"));
            Assert.That(phrase, Does.Not.Match("Its useless to cry over"));
            Assert.That(quotes, All.Matches("never").IgnoreCase);
        }
        #endregion
    }
}

