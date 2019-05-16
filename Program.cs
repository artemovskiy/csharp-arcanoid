using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using ConsoleApplication1.model;

namespace ConsoleApplication1
{
    class MyForm : Form
    {
        public MyForm(GameState gameState)
        {
            DoubleBuffered = true;

            Size = new Size(240, 400);

            var timer = new Timer();
            timer.Interval = 50;
            timer.Tick += (sender, args) =>
            {
                gameState.OnTick();
                Invalidate();
            };
            timer.Start();

            KeyDown += (sender, args) =>
            {
                if (args.KeyCode == Keys.Left)
                    gameState.MovePaddle(MoveDirections.Left);
            };

            KeyDown += (sender, args) =>
            {
                if (args.KeyCode == Keys.Right)
                    gameState.MovePaddle(MoveDirections.Right);
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

                drawWall(gameState.RightWall);
                drawWall(gameState.TopWall);
                drawWall(gameState.LeftWall);

                g.DrawRectangle(
                    new Pen(Color.Red),
                    gameState.BottomVoid.GetRectangle()
                );

                g.DrawEllipse(new Pen(Color.Blue), gameState.Ball.GetRectangle());

                g.DrawRectangle(
                    new Pen(Color.Green),
                    gameState.Paddle.GetRectangle()
                );

                foreach (var brick in gameState.Bricks)
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
        public static void Main(string[] args)
        {
            var state = new GameState(new Size(200, 300));
            Application.Run(new MyForm(state));
        }
    }
}