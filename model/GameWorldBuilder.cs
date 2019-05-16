using System.Collections.Generic;
using System.Drawing;

namespace ConsoleApplication1.model
{
    public class GameWorldBuilder
    {
        private const int WALL_WIDTH = 10;

        private GameWorld world;

        public GameWorldBuilder()
        {
            Reset();
        }

        public void BuildField(Size fieldSize)
        {
            world.FieldSize = fieldSize;
        }

        public void BuildWalls()
        {
            var sideWallsSize = new Size(WALL_WIDTH, world.FieldSize.Height + WALL_WIDTH * 2);

            world.LeftWall = new Entity(
                new Point(WALL_WIDTH / 2, sideWallsSize.Height / 2),
                sideWallsSize
            );
            world.RightWall = new Entity(
                new Point(world.FieldSize.Width + WALL_WIDTH / 2, sideWallsSize.Height / 2),
                sideWallsSize
            );

            world.TopWall = new Entity(
                new Point(GetFieldCenter().X, WALL_WIDTH / 2),
                new Size(world.FieldSize.Width, WALL_WIDTH)
            );
        }

        private Point GetFieldCenter()
        {
            return new Point(
                WALL_WIDTH + world.FieldSize.Width / 2,
                world.FieldSize.Height / 2 + WALL_WIDTH
            );
        }

        public void BuildVoid()
        {
            world.BottomVoid = new Entity(
                new Point(GetFieldCenter().X, world.FieldSize.Height + WALL_WIDTH + WALL_WIDTH / 2),
                new Size(world.FieldSize.Width, WALL_WIDTH)
            );
        }

        public void BuildBall()
        {
            world.Ball = new Ball(
                GetFieldCenter(),
                new Size(20, 20),
                new Vector()
                {
                    Y = 10,
                    X = 1
                }
            );
        }

        public void BuildBricks()
        {
            world.Bricks = new HashSet<Brick>();
            /*
             * Bricks = new HashSet<Brick>();
            var BrickSize = new Size(28, 8);
            for (int y = 12; y < BrickSize.Height * 5; y += BrickSize.Height + 2)
            {
                for (int x = 12; x < FieldSize.Width - BrickSize.Width; x += BrickSize.Width + 2)
                {
                    var brick = new Brick(
                        new Point(x + BrickSize.Width / 2 - 1, y + BrickSize.Height / 2 - 1),
                        BrickSize
                    );
                    Bricks.Add(brick);
                }
            }
             */
        }

        public void BuildPaddle()
        {
            world.Paddle = new Entity(
                new Point(GetFieldCenter().X, world.FieldSize.Height - WALL_WIDTH),
                new Size(30, WALL_WIDTH)
            );
        }

        public void Reset()
        {
            world = new GameWorld();
        }

        public GameWorld GetResult()
        {
            return world;
        }
    }
}