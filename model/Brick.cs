using System.Drawing;

namespace ConsoleApplication1.model
{
    public class Brick : Entity
    {

        public bool IsDestroyed { get; set; }
        
        public Brick(Point position, Size size) : base(position, size)
        {
            IsDestroyed = false;
        }
    }
}