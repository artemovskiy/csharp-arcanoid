using System.Drawing;

namespace ConsoleApplication1.model
{
    public class Ball : MovingEntity
    {
        
        public bool IsLost { get; set; }
        
        public Ball(Point position, Size size, Vector speed = null) : base(position, size, speed)
        {
            IsLost = false;
        }
    }
}