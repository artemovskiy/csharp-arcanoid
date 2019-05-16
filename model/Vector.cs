namespace ConsoleApplication1.model
{
    public class Vector
    {
        public int X { get; set; }

        public int Y { get; set; }

        public void InvertX()
        {
            X *= -1;
        }

        public void InvertY()
        {
            Y *= -1;
        }
        
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            var vectorObj = (Vector) obj;
            return vectorObj.X == X && vectorObj.Y == Y;
        }
    }
}