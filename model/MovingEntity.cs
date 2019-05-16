using System.Drawing;

namespace ConsoleApplication1.model
{
    public class MovingEntity : Entity
    {
        private Vector speed;

        public MovingEntity(Point position, Size size, Vector speed = null) : base(position, size)
        {
            this.speed = speed ?? new Vector(){ X =0, Y = 0};
        }

        public Vector Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            var entityObj = (MovingEntity) obj;
            return entityObj.Speed.Equals(Speed) && base.Equals(obj);
        }
    }
}