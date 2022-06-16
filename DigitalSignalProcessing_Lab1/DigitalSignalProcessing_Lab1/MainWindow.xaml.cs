using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;

namespace DigitalSignalProcessing_Lab1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public enum Param { A, F, Phi };

        private readonly double[,] HarmonicAF =
        {
            {5, 1, Math.PI / 4 },
            {5, 1, Math.PI / 2 },
            {5, 1, 3 * Math.PI / 4 },
            {5, 1, 0 },
            {5, 1, Math.PI }
        };

        private readonly double[,] HarmonicAPhi =
        {
            {1, 1, Math.PI },
            {1, 3, Math.PI },
            {1, 2, Math.PI },
            {1, 4, Math.PI },
            {1, 10, Math.PI }
        };

        private readonly double[,] HarmonicPhiF =
        {
            {3, 4, Math.PI },
            {5, 4, Math.PI },
            {10, 4, Math.PI },
            {4, 4, Math.PI },
            {8, 4, Math.PI }
        };

        private readonly double[,] PolyharmonicAPhiF =
        {
            { 5, 1, Math.PI / 9 },
            { 5, 2, Math.PI / 4 },
            { 5, 3, Math.PI / 3 },
            { 5, 4, Math.PI / 6 },
            { 5, 5, 0 }
        };

        private readonly double[,] PolyharmonicAFConst =
        {
            { 5, 1, Math.PI / 10 },
            { 5, 2, 2 * Math.PI / 10 },
            { 5, 3, 3 * Math.PI / 10 },
            { 5, 4, 4 * Math.PI / 10 },
            { 5, 5, 5 * Math.PI / 10 }
        };

        private readonly double[] StartHarmonic = { 5, 1, Math.PI / 10 };

        private readonly OxyColor[] SeriesColors =
        {
            OxyColor.FromRgb(255, 0, 0),
            OxyColor.FromRgb(0, 255, 0),
            OxyColor.FromRgb(0, 0, 255),
            OxyColor.FromRgb(255, 255, 0),
            OxyColor.FromRgb(255, 0, 255)
        };

        private readonly OxyColor[] MarkerColors =
        {
            OxyColor.FromRgb(100, 0, 0),
            OxyColor.FromRgb(0, 100, 0),
            OxyColor.FromRgb(0, 0, 100),
            OxyColor.FromRgb(100, 100, 0),
            OxyColor.FromRgb(100, 0, 100)
        };

        private readonly string[] Titles =
        {
            "Гармонический сигнал при постоянных A и f",
            "Гармонический сигнал при постоянных A и ф",
            "Гармонический сигнал при постоянных ф и f",
            "Полигармонический сигнал при указанных A, ф, f",
            "Полигармонический сигнал при постоянных Aj, fj",
            "Полигармонический сигнал при изменяющихся A, ф, f по линейному закону"
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void comboBox_Selected(object sender, RoutedEventArgs e)
        {
            var comboBox = sender as ComboBox;

            switch (comboBox.SelectedIndex)
            {
                case 0:
                    DrawHarmonicAFConst();
                    break;
                case 1:
                    DrawHarmonicAPhiConst();
                    break;
                case 2:
                    DrawHarmonicPhiFConst();
                    break;
                case 3:
                    DrawPolyharmonicAFPhiConst();
                    break;
                case 4:
                    DrawPolyharmonicAFConst();
                    break;
                case 5:
                    DrawHarmonicAFPhiLinear();
                    break;
            }
        }

        private void SetAxis(PlotModel model, AxisPosition position, double min, double max)
        {
            var axis = new LinearAxis();
            axis.Minimum = min;
            axis.Maximum = max;
            axis.Position = position;

            model.Axes.Add(axis);
        }

        private void DrawHarmonicAFConst()
        {
            var model = new PlotModel() { Title = Titles[0] };
            SetAxis(model, AxisPosition.Left, -10, 10);

            for (int i = 0; i < 5; i++)
            {
                var line = new LineSeries { Color = SeriesColors[i], MarkerType = MarkerType.Circle, MarkerFill = MarkerColors[i]};
                var points = GenerateHarmonicSignal(HarmonicAF[i, (int)Param.A], HarmonicAF[i, (int)Param.F], HarmonicAF[i, (int)Param.Phi], 512);

                int pointsCount = points.Count;

                for (int j = 0; j < pointsCount; j++)
                {
                    line.Points.Add(new DataPoint(j, points[j]));
                }

                model.Series.Add(line);
            }

            plotView.Model = model;
        }

        private void DrawHarmonicAPhiConst()
        {
            var model = new PlotModel { Title = Titles[1] };
            SetAxis(model, AxisPosition.Left, -5, 5);

            for (int i = 0; i < 5; i++)
            {
                var line = new LineSeries { Color = SeriesColors[i], MarkerType = MarkerType.Circle, MarkerFill = MarkerColors[i] };
                var points = GenerateHarmonicSignal(HarmonicAPhi[i, (int)Param.A], HarmonicAPhi[i, (int)Param.F], HarmonicAPhi[i, (int)Param.Phi], 512);

                int pointsCount = points.Count;

                for (int j = 0; j < pointsCount; j++)
                {
                    line.Points.Add(new DataPoint(j, points[j]));
                }

                model.Series.Add(line);
            }

            plotView.Model = model;
        }

        private void DrawHarmonicPhiFConst()
        {
            var model = new PlotModel { Title = Titles[2] };
            SetAxis(model, AxisPosition.Left, -15, 15);

            for (int i = 0; i < 5; i++)
            {
                var line = new LineSeries { Color = SeriesColors[i], MarkerType = MarkerType.Circle, MarkerFill = MarkerColors[i] };
                var points = GenerateHarmonicSignal(HarmonicPhiF[i, (int)Param.A], HarmonicPhiF[i, (int)Param.F], HarmonicPhiF[i, (int)Param.Phi], 512);

                int pointsCount = points.Count;

                for (int j = 0; j < pointsCount; j++)
                {
                    line.Points.Add(new DataPoint(j, points[j]));
                }

                model.Series.Add(line);
            }

            plotView.Model = model;
        }

        private void DrawPolyharmonicAFPhiConst()
        {
            var model = new PlotModel { Title = Titles[3] };

            var line = new LineSeries { Color = OxyColors.Gray, MarkerType = MarkerType.Circle, MarkerFill = OxyColor.FromRgb(150, 150, 150) };

            List<double> amplitudes = new List<double>();
            List<double> frequences = new List<double>();
            List<double> startPhases = new List<double>();

            for (int i = 0; i < 5; i++)
            {
                amplitudes.Add(PolyharmonicAPhiF[i, 0]);
                frequences.Add(PolyharmonicAPhiF[i, 1]);
                startPhases.Add(PolyharmonicAPhiF[i, 2]);
            }

            var points = GeneratePolyharnomicSignal(amplitudes, frequences, startPhases, 512);

            int pointsCount = points.Count;

            for (int i = 0; i < pointsCount; i++)
            {
                line.Points.Add(new DataPoint(i, points[i]));
            }

            model.Series.Add(line);

            plotView.Model = model;
        }

        private void DrawPolyharmonicAFConst()
        {
            var model = new PlotModel { Title = Titles[4] };
            SetAxis(model, AxisPosition.Left, -25, 25);

            var amplitudes = new List<List<double>>();
            var frequences = new List<List<double>>();
            var startPhases = new List<List<double>>();

            var rand = new Random();

            for (int i = 0; i < 5; i++)
            {
                var amp = new List<double>();
                var freq = new List<double>();
                var phi = new List<double>();

                for (int j = 0; j < 5; j++)
                {
                    amp.Add(PolyharmonicAFConst[i, 0]);
                    freq.Add(PolyharmonicAFConst[i, 1] + j);
                    phi.Add(PolyharmonicAFConst[i, 2] + (Math.PI / 5) * j);
                }

                amplitudes.Add(amp);
                frequences.Add(freq);
                startPhases.Add(phi);
            }

            for (int i = 0; i < 5; i++)
            {
                var line = new LineSeries { Color = SeriesColors[i], MarkerType = MarkerType.Circle, MarkerFill = MarkerColors[i] };
                var points = GeneratePolyharnomicSignal(amplitudes[i], frequences[i], startPhases[i], 512);

                for (int j = 0; j < points.Count; j++)
                {
                    line.Points.Add(new DataPoint(j, points[j]));
                }

                model.Series.Add(line);
            }

            plotView.Model = model;
        }

        private void DrawHarmonicAFPhiLinear()
        {
            var harmonics = new List<List<double>>();

            var multiplier = 1.1;

            // Generate harmonics
            for (int i = 0; i < 5; i++)
            {
                var harmonic = new List<double>();

                harmonic.Add(StartHarmonic[0] * (i + 1) * multiplier);
                harmonic.Add(StartHarmonic[1] * (i + 1) * multiplier);
                harmonic.Add(StartHarmonic[2] * (i + 1) * multiplier);

                harmonics.Add(harmonic);
            }

            var points = new List<double>();

            int N = 512;

            for (int i = 0; i < N; i++)
            {
                double res = 0;
                for (int j = 0; j < 5; j++)
                {
                    res += harmonics[j][0] * Math.Sin((2 * Math.PI * harmonics[j][1] * i) / N + harmonics[j][2]);
                }
                points.Add(res);
            }

            var model = new PlotModel { Title = Titles[5] };

            var line = new LineSeries { Color = OxyColor.FromRgb(50, 50, 50), MarkerType = MarkerType.Circle, MarkerFill = OxyColor.FromRgb(100, 100, 100) };

            for (int i = 0; i < N; i++)
            {
                line.Points.Add(new DataPoint(i, points[i]));
            }

            model.Series.Add(line);

            plotView.Model = model;
        }

        private List<double> GenerateHarmonicSignal(double amplitude, double frequency, double startPhase, int N)
        {
            List<double> points = new List<double>();

            for (int i = 0; i < N; i++)
            {
                points.Add(amplitude * Math.Sin((2 * Math.PI * frequency * i) / N + startPhase));
            }

            return points;
        }

        private List<double> GeneratePolyharnomicSignal(List<double> amplitudes, List<double> frequences, List<double> startPhases, int N)
        {
            List<double> points = new List<double>();

            for (int i = 0; i < N; i++)
            {
                double signal = 0;
                for (int j = 0; j < 5; j++)
                {
                    signal += amplitudes[j] * Math.Sin((2 * Math.PI * frequences[j] * i) / N + startPhases[j]);
                }
                points.Add(signal);
            }

            return points;
        }
    }
}
