using System.Drawing;

namespace ConsoleApplication1.model
{
    public class Entity
    {
        private Size size;

        public Size Size
        {
            get { return size; }
            protected set { size = value; }
        }

        private Point position;

        // Position of entity`s center
        public Point Position
        {
            get { return position; }
            set { position = value; }
        }

        public int TopY
        {
            get { return Position.Y - Size.Height / 2; }
        }

        public int BottomY
        {
            get { return Position.Y + Size.Height / 2; }
        }

        public int LeftX
        {
            get { return Position.X - Size.Width / 2; }
        }

        public int RightX
        {
            get { return Position.X + Size.Width / 2; }
        }

        public Point GetRightLeftCorner()
        {
            return new Point(LeftX, TopY);
        }

        public Rectangle GetRectangle()
        {
            return new Rectangle(GetRightLeftCorner(), Size);
        }

        public Entity(Point position, Size size)
        {
            Position = position;
            Size = size;
        }

        public bool Touches(Entity entity)
        {
            return HasIntersectionOnNumericLine(LeftX, RightX, entity.LeftX, entity.RightX)
                && HasIntersectionOnNumericLine(TopY, BottomY, entity.TopY, entity.BottomY);
        }

        private static bool HasIntersectionOnNumericLine(int left1, int right1, int left2, int right2)
        {
            return left1 <= left2 && left2 <= right1
                   || left2 <= left1 && left1 <= right2;
        }

        public override string ToString()
        {
            return string.Format("Position: {0}\r\n Size: {1}", Position, Size);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            var entityObj = (Entity) obj;
            return entityObj.Position.Equals(Position) && entityObj.Size.Equals(Size);
        }
    }
}