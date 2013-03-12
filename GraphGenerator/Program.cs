
namespace GraphGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            ChartCreator chartCreator = new ChartCreator(args);
            chartCreator.SaveImage();
        }
    }
}
