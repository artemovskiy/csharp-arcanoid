using System.Collections.Generic;
using System.Drawing;
using ConsoleApplication1.model;
using ConsoleApplication1.model.entities;
using NUnit.Framework;

namespace ConsoleApplication1.Tests
{
    [TestFixture]
    public class GameWorldBuilderTests
    {
        [Test]
        public void TestBuildsField()
        {
            var builder = GetTestBuilder();

            Assert.AreEqual(new Entity(
                new Point(25, 30),
                new Size(30, 40)
            ), builder.GetResult().Field);
        }

        [Test]
        public void TestBuildsWalls()
        {
            var builder = GetTestBuilder();
            builder.BuildWalls();
            var world = builder.GetResult();

            Assert.AreEqual(new Entity(
                new Point(25, 5),
                new Size(30, 10)
            ), world.TopWall);

            Assert.AreEqual(new Entity(
                new Point(5, 30),
                new Size(10, 60)
            ), world.LeftWall);

            Assert.AreEqual(new Entity(
                new Point(45, 30),
                new Size(10, 60)
            ), world.RightWall);
        }

        [Test]
        public void TestBuildsVoid()
        {
            var builder = GetTestBuilder();
            builder.BuildVoid();
            var world = builder.GetResult();

            Assert.AreEqual(new Entity(
                new Point(25, 55),
                new Size(30, 10)
            ), world.BottomVoid);
        }

        [Test]
        public void TestBuildsBall()
        {
            var builder = GetTestBuilder();
            builder.BuildBall();
            var world = builder.GetResult();

            Assert.AreEqual(new Ball(
                new Point(25, 30),
                new Size(20, 20),
                new Vector()
                {
                    Y = 10,
                    X = 1
                }
            ), world.Ball);
        }

        [Test]
        public void TestBuildsPaddle()
        {
            var builder = GetTestBuilder();
            builder.BuildPaddle();
            var world = builder.GetResult();

            Assert.AreEqual(new Entity(
                new Point(25, 30),
                new Size(30, 10)
            ), world.Paddle);
        }

        [Test]
        public void TestBuildsBricks()
        {
            var builder = GetTestBuilder();
            builder.BuildBricks(2);
            var world = builder.GetResult();

            var expected = new HashSet<Brick>()
            {
                new Brick(new Point(25, 15), new Size(30, 10)),
                new Brick(new Point(25, 25), new Size(30, 10)),
            };
            Assert.AreEqual(expected, world.Bricks);
        }

        [Test]
        public void TestBuildsBricks2()
        {
            var builder = new GameWorldBuilder();
            builder.BuildField(new Size(60, 40));
            builder.BuildBricks(2);
            var world = builder.GetResult();

            var expected = new HashSet<Brick>()
            {
                new Brick(new Point(25, 15), new Size(30, 10)),
                new Brick(new Point(55, 15), new Size(30, 10)),
                new Brick(new Point(25, 25), new Size(30, 10)),
                new Brick(new Point(55, 25), new Size(30, 10)),
            };
            Assert.AreEqual(expected, world.Bricks);
        }

        [Test]
        public void TestBuildsBricks3()
        {
            var builder = new GameWorldBuilder();
            builder.BuildField(new Size(70, 40));
            builder.BuildBricks(2);
            var world = builder.GetResult();

            var expected = new HashSet<Brick>()
            {
                new Brick(new Point(25, 15), new Size(30, 10)),
                new Brick(new Point(55, 15), new Size(30, 10)),
                new Brick(new Point(25, 25), new Size(30, 10)),
                new Brick(new Point(55, 25), new Size(30, 10)),
            };
            Assert.AreEqual(expected, world.Bricks);
        }

        private static GameWorldBuilder GetTestBuilder()
        {
            var builder = new GameWorldBuilder();
            builder.BuildField(new Size(30, 40));
            return builder;
        }
    }
}