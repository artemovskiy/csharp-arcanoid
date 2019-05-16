using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.Remoting.Channels;
using ConsoleApplication1.model;

namespace ConsoleApplication1
{
    class MyForm : Form
    {
        public MyForm(GameWorld gameWorld)
        {
            DoubleBuffered = true;

            Size = new Size(240, 400);

            var timer = new Timer();
            timer.Interval = 50;
            timer.Tick += (sender, args) =>
            {
                gameWorld.OnTick();
                Invalidate();
            };
            timer.Start();

            KeyDown += (sender, args) =>
            {
                if (args.KeyCode == Keys.Left)
                    gameWorld.MovePaddle(MoveDirections.Left);
            };

            KeyDown += (sender, args) =>
            {
                if (args.KeyCode == Keys.Right)
                    gameWorld.MovePaddle(MoveDirections.Right);
            };

            Paint += (sender, args) =>
            {
                var g = args.Graphics;

                Action<Entity> drawWall = wall =>
                {
                    g.DrawRectangle(
                        new Pen(Color.Black),
                        wall.GetRectangle()
                    );
                };

                drawWall(gameWorld.RightWall);
                drawWall(gameWorld.TopWall);
                drawWall(gameWorld.LeftWall);

                g.DrawRectangle(
                    new Pen(Color.Red),
                    gameWorld.BottomVoid.GetRectangle()
                );

                g.DrawEllipse(new Pen(Color.Blue), gameWorld.Ball.GetRectangle());

                g.DrawRectangle(
                    new Pen(Color.Green),
                    gameWorld.Paddle.GetRectangle()
                );

                foreach (var brick in gameWorld.Bricks)
                {
                    if (!brick.IsDestroyed)
                        g.FillRectangle(
                            Brushes.Yellow,
                            brick.GetRectangle()
                        );
                }
            };
        }
    }

    internal class Program
    {
        public static void Main()
        {
            var builder = new GameWorldBuilder();
            builder.BuildField(new Size(200, 300));
            builder.BuildWalls();
            builder.BuildVoid();
            builder.BuildBall();
            builder.BuildBricks();
            builder.BuildPaddle();
            
            var state = builder.GetResult();

            state.BrickDestroy += (sender, args) => { Console.WriteLine("destroyed brick"); };

            state.Failure += (IChannelSender, args) => { Console.WriteLine("FAILURE"); };

            Application.Run(new MyForm(state));
        }
    }
}