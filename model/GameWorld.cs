using System.Collections.Generic;
using System.Drawing;
using ConsoleApplication1.model.entities;

namespace ConsoleApplication1.model
{
    public enum MoveDirections
    {
        Left = -1,
        Right = 1
    }

    public class BrickDestroyEventArgs
    {
        private Brick brick;

        public Brick Brick
        {
            get { return brick; }
        }

        public BrickDestroyEventArgs(Brick brick)
        {
            this.brick = brick;
        }
    }

    public delegate void BrickDestroyEventHandler(object sender, BrickDestroyEventArgs args);

    public class FailureEventArgs
    {
    }

    public delegate void FailureEventHandler(object sender, FailureEventArgs args);

    public class GameWorld
    {
        public Entity Field { get; set; }

        public Ball Ball { get; set; }

        public Entity Paddle { get; set; }

        public Entity LeftWall { get; set; }
        public Entity RightWall { get; set; }
        public Entity TopWall { get; set; }

        public Entity BottomVoid { get; set; }

        public List<Brick> Bricks { get; set; }

        public event BrickDestroyEventHandler BrickDestroy;

        public event FailureEventHandler Failure;

        public GameWorld()
        {
            Bricks = new List<Brick>();
        }

        public void Tick()
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
                    OnBrickDestroy(brick);
                }
            }

            if (flag)
            {
                Ball.Speed.InvertY();
                return;
            }

            if (Ball.Touches(LeftWall) || Ball.Touches(RightWall))
            {
                Ball.Speed.InvertX();
                return;
            }

            if (Ball.Touches(Paddle) || Ball.Touches(TopWall))
            {
                Ball.Speed.InvertY();
                return;
            }

            if (!Ball.IsLost && Ball.Touches(BottomVoid))
            {
                Ball.IsLost = true;
                OnFailure();
            }
        }

        protected virtual void OnBrickDestroy(Brick brick)
        {
            if (BrickDestroy != null)
                BrickDestroy(this, new BrickDestroyEventArgs(brick));
        }

        protected virtual void OnFailure()
        {
            if (Failure != null)
                Failure(this, new FailureEventArgs());
        }

        public void MovePaddle(MoveDirections direction)
        {
            if (direction == MoveDirections.Left && Paddle.LeftX <= Field.LeftX)
                return;
            if (direction == MoveDirections.Right && Paddle.RightX >= Field.RightX)
                return;
            Paddle.Position = Point.Add(Paddle.Position, new Size((direction == MoveDirections.Left ? -1 : 1) * 10, 0));
        }
    }
}