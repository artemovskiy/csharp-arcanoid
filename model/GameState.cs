using System.Collections.Generic;
using System.Drawing;

namespace ConsoleApplication1.model
{
    public enum MoveDirections
    {
        Left = -1,
        Right = 1
    }

    public class GameState
    {
        public Size FieldSize { get; set; }

        public MovingEntity Ball { get; set; }

        public Entity Paddle { get; set; }

        public Entity LeftWall { get; set; }
        public Entity RightWall { get; set; }
        public Entity TopWall { get; set; }

        public Entity BottomVoid { get; set; }

        public HashSet<Brick> Bricks { get; set; }

        public GameState(Size size)
        {
            FieldSize = size;
            Initialize();
        }

        private void Initialize()
        {
            InitializeWalls();

            BottomVoid = new Entity(new Point(FieldSize.Width / 2, FieldSize.Height - 5),
                new Size(FieldSize.Width - 20, 10));

            InitializeBall();

            InitializeBricks();

            Paddle = new Entity(
                new Point(FieldSize.Width / 2, FieldSize.Height - 25),
                new Size(30, 10)
            );
        }

        private void InitializeWalls()
        {
            var sideWallsSize = new Size(10, FieldSize.Height);
            LeftWall = new Entity(new Point(5, FieldSize.Height / 2), sideWallsSize);
            RightWall = new Entity(new Point(FieldSize.Width - 5, FieldSize.Height / 2), sideWallsSize);

            TopWall = new Entity(new Point(FieldSize.Width / 2, 5), new Size(FieldSize.Width - 20, 10));
        }

        private void InitializeBall()
        {
            Ball = new MovingEntity(
                new Point(FieldSize.Width / 2, FieldSize.Height / 2),
                new Size(20, 20),
                new Vector()
                {
                    Y = 10,
                    X = 1
                }
            );
        }

        private void InitializeBricks()
        {
            Bricks = new HashSet<Brick>();
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
        }

        public void OnTick()
        {
            Ball.Position = new Point(
                Ball.Position.X + Ball.Speed.X,
                Ball.Position.Y + Ball.Speed.Y
            );

            bool flag = false;
            foreach (var brick in Bricks)
            {
                if (!brick.IsDestroyed && Ball.Touches(brick))
                {
                    flag = true;
                    brick.IsDestroyed = true;
                    OnBounceBrick(brick);
                }
            }

            if (flag)
            {
                Ball.Speed.InvertY();
            }

            if (Ball.Touches(LeftWall) || Ball.Touches(RightWall))
                Ball.Speed.InvertX();

            if (Ball.Touches(Paddle) || Ball.Touches(TopWall))
                Ball.Speed.InvertY();
        }

        private void OnBounceBrick(Brick brick)
        {
        }

        public void MovePaddle(MoveDirections direction)
        {
            if (direction == MoveDirections.Left && Paddle.Touches(LeftWall))
                return;
            if (direction == MoveDirections.Right && Paddle.Touches(RightWall))
                return;
            Paddle.Position = Point.Add(Paddle.Position, new Size((direction == MoveDirections.Left ? -1 : 1) * 10, 0));
        }
    }
}