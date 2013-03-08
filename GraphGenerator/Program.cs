using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;


namespace GraphGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (string arg in args)
            {
                SeriesCreator creator = new SeriesCreator(CsvReader.Builder.CreateInstance(arg));
                string filePath = arg.Replace("Data", "Charts");
                GeneratePlot(creator, filePath);
            }
        }

        public static void GeneratePlot(SeriesCreator creator, string filePath)
        {
            IEnumerable<Series> serieses = creator.ToSerieses();

            using (var ch = new Chart())
            {
                ch.Size = new Size(1300, 800);
                ch.AntiAliasing = AntiAliasingStyles.All;
                ch.TextAntiAliasingQuality = TextAntiAliasingQuality.High;
                ch.Palette = ChartColorPalette.BrightPastel;

                ChartArea area = new ChartArea();
                area.AxisX.MajorGrid.Enabled = false;
                area.AxisY.MajorGrid.Enabled = false;
                area.AxisY.Minimum = creator.GetMinimumY();
                ch.ChartAreas.Add(area);

                Legend legend = new Legend();
                legend.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);
                ch.Legends.Add(legend);

                foreach (var s in serieses)
                {
                    ch.Series.Add(s);
                }

                string savePath = filePath + ".png";
                ch.SaveImage(savePath, ChartImageFormat.Png);
            }
        }
    }
}
