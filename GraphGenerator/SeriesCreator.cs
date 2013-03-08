using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;


namespace GraphGenerator
{
    class SeriesCreator
    {
        private CsvReader reader;
        private List<List<PointD>> pointLists;
        public Legend Legend { get; set; }

        public SeriesCreator(CsvReader reader)
        {
            this.reader = reader;
            Legend = new Legend();
        }

        private IEnumerable<Series> AddPoints(List<Series> serieses)
        {
            int rowNumber = 0;
            foreach (List<string> tokens in reader)
            {
                int i = 0;

                foreach (var token in tokens)
                {
                    PointD p = new PointD(rowNumber, double.Parse(token.StripDoubleQuotes()));
                    pointLists[i].Add(p);
                    serieses[i].Points.AddXY(p.X, p.Y);
                    i++;
                }
                rowNumber++;
            }
            return serieses;
        }

        public IEnumerable<Series> ToSerieses()
        {
            List<Series> serieses = new List<Series>();
            pointLists = new List<List<PointD>>();

            foreach (var name in reader.Header)
            {
                pointLists.Add(new List<PointD>());
                Series s = new Series()
                            {
                                Name = name.StripDoubleQuotes(),
                                BorderWidth = 3,
                                IsVisibleInLegend = true,
                                IsXValueIndexed = true,
                                ChartType = SeriesChartType.Spline
                            };
                serieses.Add(s);
            }
            return AddPoints(serieses);
        }

        public double GetMinimumY()
        {
            double maxY = pointLists.SelectMany(l => l.Select(p => p.Y)).Max();
            double minY = pointLists.SelectMany(l => l.Select(p => p.Y)).Min();
            double range = Math.Abs(maxY - minY);

            return Math.Floor(minY - (range) * .001);
        }
    }
}
