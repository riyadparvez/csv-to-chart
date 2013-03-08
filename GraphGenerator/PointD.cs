
namespace GraphGenerator
{
    class PointD
    {
        private double x;
        private double y;

        public double X
        {
            get { return x; }
            set { x = value; }
        }
        public double Y
        {
            get { return y; }
            set { y = value; }
        }

        public PointD(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
