using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace GraphGenerator
{
    public class ChartCreator
    {
        private IEnumerable<string> filePaths;
        private string saveFilePath;

        public ChartCreator(IEnumerable<string> filePaths)
        {
            this.filePaths = filePaths;

        }

        public void SaveImage()
        {
            Parallel.ForEach(filePaths, (dataFilePath) =>
                {
                    SeriesCreator creator = new SeriesCreator(CsvReader.Builder.CreateInstance(dataFilePath));
                    string filePath = dataFilePath.Replace("Data", "Charts");
                    GenerateChart(creator, filePath);
                }
            );
        }

        private void GenerateChart(SeriesCreator creator, string filePath)
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
