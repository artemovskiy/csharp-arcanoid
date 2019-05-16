using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ConsoleApplication1.model;
using ConsoleApplication1.model.entities;
using NUnit.Framework;

namespace ConsoleApplication1.Tests
{
    [TestFixture]
    public class GameWorldTests
    {
        private GameWorld GetTestingWorld()
        {
            var world = new GameWorld();
            world.Field = new Entity(new Point(20, 30), new Size(20, 40));
            world.TopWall = new Entity(new Point(20, 5), new Size(20, 10));
            world.BottomVoid = new Entity(new Point(20, 55), new Size(20, 10));
            world.LeftWall = new Entity(new Point(5, 30), new Size(10, 60));
            world.RightWall = new Entity(new Point(35, 30), new Size(10, 60));
            world.Ball = new Ball(new Point(20, 35), new Size(10, 10));
            world.Paddle = new Entity(new Point(20, 49), new Size(10, 2));
            world.Bricks = new HashSet<Brick>() {new Brick(new Point(20, 15), new Size(20, 10))};
            return world;
        }

        private Entity GetEmptyEntity()
        {
            return new Entity(new Point(0, 0), new Size(0, 0));
        }

        [Test]
        public void TestMovesPaddle()
        {
            var world = new GameWorld();
            world.Field = new Entity(new Point(0, 0), new Size(40, 10));
            world.Paddle = new Entity(new Point(0, 0), new Size(20, 10));

            Assert.AreEqual(new Point(0, 0), world.Paddle.Position);

            world.MovePaddle(MoveDirections.Left);
            Assert.AreEqual(new Point(-10, 0), world.Paddle.Position);

            world.MovePaddle(MoveDirections.Left);
            Assert.AreEqual(new Point(-10, 0), world.Paddle.Position, "should stuck when reached left edge");

            world.MovePaddle(MoveDirections.Right);
            Assert.AreEqual(new Point(0, 0), world.Paddle.Position);


            world.MovePaddle(MoveDirections.Right);
            world.MovePaddle(MoveDirections.Right);
            Assert.AreEqual(new Point(10, 0), world.Paddle.Position, "should stuck when reached right edge");
        }

        [Test]
        public void TestBallMoves()
        {
            var world = GetTestingWorld();
            world.Ball.Speed = new Vector() {X = 1, Y = 1};
            var previousPosition = world.Ball.Position;
            world.Tick();
            Assert.AreEqual(Point.Add(previousPosition, new Size(1, 1)), world.Ball.Position);
        }

        [Test]
        public void TestBallBouncesFromLeftWall()
        {
            var world = GetTestingWorld();
            world.Ball.Speed.X = 5;
            world.Tick();
            Assert.AreEqual(-5, world.Ball.Speed.X);
        }

        [Test]
        public void TestBallBouncesFromRightWall()
        {
            var world = GetTestingWorld();
            world.Ball.Speed.X = -5;
            world.Tick();
            Assert.AreEqual(5, world.Ball.Speed.X);
        }

        [Test]
        public void TestBallBouncesFromTopWall()
        {
            var world = GetTestingWorld();
            world.Bricks = new HashSet<Brick>();
            world.Ball.Speed.Y = -5;
            world.Ball.Position = new Point(20, 20);
            world.Tick();
            Assert.AreEqual(5, world.Ball.Speed.Y);
        }

        [Test]
        public void TestBallBouncesFromPaddle()
        {
            var world = GetTestingWorld();
            world.Ball.Speed.Y = 1;
            world.Ball.Position = new Point(20, 42);
            world.Tick();
            Assert.AreEqual(-1, world.Ball.Speed.Y);
        }

        [Test]
        public void TestBallFallsIntoVoid()
        {
            var world = GetTestingWorld();
            world.Ball.Speed.Y = 1;
            world.Ball.Position = new Point(20, 44);
            world.Paddle = GetEmptyEntity();

            bool failureEventFired = false;
            world.Failure += (sender, args) => { failureEventFired = true; };

            world.Tick();
            Assert.IsTrue(failureEventFired);
            Assert.IsTrue(world.Ball.IsLost);
            Assert.AreEqual(new Point(20, 45), world.Ball.Position);

            failureEventFired = false;
            world.Tick();
            Assert.IsFalse(failureEventFired);
            Assert.AreEqual(new Point(20, 46), world.Ball.Position);
        }

        [Test]
        public void TestBallDestroysBrick()
        {
            var world = GetTestingWorld();
            world.Ball.Speed.Y = -1;
            world.Ball.Position = new Point(20, 26);

            Brick destroyedBrick = null;
            world.BrickDestroy += (sender, args) => { destroyedBrick = args.Brick; };

            world.Tick();
            Assert.AreEqual(world.Bricks.First(), destroyedBrick);
            Assert.IsTrue(destroyedBrick.IsDestroyed);
            Assert.AreEqual(1, world.Ball.Speed.Y);
        }
    }
}