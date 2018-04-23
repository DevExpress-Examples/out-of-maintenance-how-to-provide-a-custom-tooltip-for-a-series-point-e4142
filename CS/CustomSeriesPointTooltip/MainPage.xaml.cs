using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Resources;
using System.Globalization;
using System.Windows.Controls;
using System.Xml.Linq;

namespace CustomSeriesPointTooltip {
    public partial class MainPage : UserControl {
        public MainPage() {
            InitializeComponent();
            chart.DataSource = GetDataSource();
        }

        List<G8Member> GetDataSource() {
            List<GDP> GDPs = GetGDPs();
            List<G8Member> countries = new List<G8Member>();
            const int yearsInDecade = 10;
            for (int countryCounter = 0; countryCounter < 8; countryCounter++) {
                List<GDP> countryGDPs = new List<GDP>();
                for (int countryValuesCounter = 0; countryValuesCounter < yearsInDecade; countryValuesCounter++) {
                    countryGDPs.Add(GDPs[countryCounter * yearsInDecade + countryValuesCounter]);
                }
                countries.Add(new G8Member(countryGDPs));
            }
            return countries;
        }

        List<GDP> GetGDPs() {
            XDocument document = DataLoader.LoadXmlFromResources("/Data/GDPofG8.xml");
            List<GDP> result = new List<GDP>();
            if (document != null) {
                foreach (XElement element in document.Element("G8GDPs").Elements()) {
                    string country = element.Element("Country").Value;
                    int year = int.Parse(element.Element("Year").Value);
                    decimal product = Convert.ToDecimal(element.Element("Product").Value, CultureInfo.InvariantCulture);
                    result.Add(new GDP(country, year, product));
                }
            }
            return result;
        }
        public static class DataLoader {
            public static XDocument LoadXmlFromResources(string fileName) {
                try {
                    fileName = "/CustomSeriesPointTooltip;component" + fileName;
                    Uri uri = new Uri(fileName, UriKind.RelativeOrAbsolute);
                    StreamResourceInfo info = Application.GetResourceStream(uri);
                    return XDocument.Load(info.Stream);
                }
                catch {
                    return null;
                }
            }
        }
        
        private void ChartToolTipController_ToolTipOpening(object sender, DevExpress.Xpf.Charts.ChartToolTipEventArgs e) {
            ToolTipData toolTipData = e.Hint as ToolTipData;
            int colorNumber = e.Series.Points.IndexOf(e.SeriesPoint);
            Color seriesColor = e.ChartControl.Palette[colorNumber];
            toolTipData.SeriesBrush = new SolidColorBrush(seriesColor);
        }
    }

    public class GDP {
        public string Country { get; private set; }
        public int Year { get; private set; }
        public decimal Product { get; private set; }

        public GDP(string country, int year, decimal product) {
            Country = country;
            Year = year;
            Product = product;
        }
    }

    public class G8Member {
        public decimal GDPin2010 { get; private set; }
        public string CountryName { get; private set; }
        public ToolTipData ToolTipData { get; set; }

        public G8Member(List<GDP> GDPs) {
            ToolTipData = new ToolTipData(GDPs, GDPs[0].Country);
            CountryName = GDPs[0].Country;
            GDPin2010 = GDPs[9].Product;
        }
    }

    public class ToolTipData {
        public List<GDP> GDPs { get; private set; }
        public SolidColorBrush SeriesBrush { get; set; }
        public string Title { get; private set; }

        public ToolTipData(List<GDP> gdps, string countryName) {
            GDPs = gdps;
            Title = countryName + " GDP History";
        }

    }
}
