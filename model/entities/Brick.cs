using System.Drawing;

namespace ConsoleApplication1.model.entities
{
    public class Brick : Entity
    {

        public bool IsDestroyed { get; set; }
        
        public Brick(Point position, Size size) : base(position, size)
        {
            IsDestroyed = false;
        }
        
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            var brickObj = (Brick) obj;
            return brickObj.IsDestroyed == IsDestroyed && base.Equals(obj);
        }
    }
}