using System.Drawing;
using ConsoleApplication1.model;
using NUnit.Framework;

namespace ConsoleApplication1.Tests
{
    [TestFixture]
    public class EntityTests
    {
        [Test]
        public void TestTouchNoIntersection()
        {
            var e1 = new Entity(new Point(5, 5), new Size(10, 10));
            var e2 = new Entity(new Point(20, 20), new Size(10, 10));
            Assert.IsFalse(e1.Touches(e2));
            Assert.IsFalse(e2.Touches(e1));
        }

        [Test]
        public void TestTouchHorizontalIntersection()
        {
            var e1 = new Entity(new Point(5, 5), new Size(10, 10));
            var e2 = new Entity(new Point(10, 5), new Size(10, 10));
            Assert.IsTrue(e1.Touches(e2));
            Assert.IsTrue(e2.Touches(e1));
        }

        [Test]
        public void TestTouchHorizontalTouching()
        {
            var e1 = new Entity(new Point(5, 5), new Size(10, 10));
            var e2 = new Entity(new Point(15, 5), new Size(10, 10));
            Assert.IsTrue(e1.Touches(e2));
            Assert.IsTrue(e2.Touches(e1));
        }

        [Test]
        public void TestTouchVerticalIntersection()
        {
            var e1 = new Entity(new Point(5, 5), new Size(10, 10));
            var e2 = new Entity(new Point(5, 10), new Size(10, 10));
            Assert.IsTrue(e1.Touches(e2));
            Assert.IsTrue(e2.Touches(e1));
        }

        [Test]
        public void TestTouchVerticalTouching()
        {
            var e1 = new Entity(new Point(5, 5), new Size(10, 10));
            var e2 = new Entity(new Point(5, 15), new Size(10, 10));
            Assert.IsTrue(e1.Touches(e2));
            Assert.IsTrue(e2.Touches(e1));
        }
    }
}