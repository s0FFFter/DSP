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
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Wpf;
using DigitalSignalProcessing_Lab2.Helpers;
using DigitalSignalProcessing_Lab2.Generators;
using DigitalSignalProcessing_Lab2.Fourier;

namespace DigitalSignalProcessing_Lab2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly int N = 64;

        private readonly double[] Amplitudes = { 1, 3, 5, 8, 10, 12, 16 };
        private readonly double[] Phases = { Math.PI / 6, Math.PI / 4, Math.PI / 3, Math.PI / 2, 3 * Math.PI / 4, Math.PI };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void menuItem_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;

            switch (element.Name)
            {
                case "dftHarmonicItem":
                    HarmonicSignal();
                    break;
                case "dftPolyharmonicItem":
                    PolyharmonicSignal();
                    break;
                case "fftPolyharmonicItem":
                    FFTPolyharmonicSignal();
                    break;
                case "filtersItem":
                    FilterSignal();
                    break;
            }
            
        }

        private PlotView[] InitializePlots(int numPlots)
        {
            List<PlotView> plots = new List<PlotView>();

            for (int i = 0; i < numPlots; i++)
            {
                plots.Add(new PlotView());
            }

            return plots.ToArray();
        }

        private void HarmonicSignal()
        {
            PlotView[] plotViews = InitializePlots(4);

            GridHelper.SetGrid2x2(gridContainer, plotViews);

            var testSignalModel = new PlotModel { Title = "Тестовый сигнал", TitleColor = OxyColors.Chocolate, TitleFontSize = 20 };
            var amplitudeSpectrumModel = new PlotModel { Title = "Амплитудный спектр", TitleColor = OxyColors.BlueViolet, TitleFontSize = 20 };
            var phaseSpectrumModel = new PlotModel { Title = "Фазовый спектр", TitleColor = OxyColors.Chartreuse, TitleFontSize = 20 } ;
            var restoredSignalModel = new PlotModel { Title = "Восстановленный сигнал", TitleColor = OxyColors.BurlyWood, TitleFontSize = 20 };

            var testSignalSeries = new LineSeries { Color = OxyColors.Red, MarkerType = MarkerType.Circle };
            var amplitudeSpectrumSeries = new StemSeries { Color = OxyColors.Green };
            var phaseSpectrumSeries = new StemSeries { Color = OxyColors.Blue };
            var restoredSignalSeries = new LineSeries { Color = OxyColors.Purple, MarkerType = MarkerType.Circle };

            /* Тестовый сигнал */
            List<DataPoint> points = new List<DataPoint>();

            double[] testSignal = HarmonicGenerator.GenerateSignal(Math.Cos, 10, 1, 0, N);

            for (int i = 0; i < N; i++)
            {
                points.Add(new DataPoint(i, testSignal[i]));
            }

            testSignalSeries.Points.AddRange(points);

            testSignalModel.Series.Add(testSignalSeries);

            plotViews[0].Model = testSignalModel;

            // Амплитудный спектр
            points = new List<DataPoint>();

            double[] amplitudes = DiscreteFourierTransform.GetHarmonicsAmplitudes(testSignal, 0, N, N);

            for (int i = 0; i < N; i++)
            {
                points.Add(new DataPoint(i, amplitudes[i]));
            }

            amplitudeSpectrumSeries.Points.AddRange(points);
            amplitudeSpectrumModel.Series.Add(amplitudeSpectrumSeries);

            // Фазовый спектр
            points = new List<DataPoint>();

            double[] phases = DiscreteFourierTransform.GetHarmonicsPhases(testSignal, 0, N, N);

            for (int i = 0; i < N; i++)
            {
                points.Add(new DataPoint(i, phases[i]));
            }

            phaseSpectrumSeries.Points.AddRange(points);
            phaseSpectrumModel.Series.Add(phaseSpectrumSeries);

            // Восстановленный сигнал
            points = new List<DataPoint>();

            double[] restoredSignal = HarmonicGenerator.GenerateRestoredSignal(amplitudes, phases, N);

            for (int i = 0; i < N; i++)
            {
                points.Add(new DataPoint(i, restoredSignal[i]));
            }

            restoredSignalSeries.Points.AddRange(points);

            restoredSignalModel.Series.Add(restoredSignalSeries);

            plotViews[0].Model = testSignalModel;
            plotViews[1].Model = amplitudeSpectrumModel;
            plotViews[2].Model = phaseSpectrumModel;
            plotViews[3].Model = restoredSignalModel;
        }

        private void PolyharmonicSignal()
        {
            PlotView[] plotViews = InitializePlots(4);

            GridHelper.SetGrid2x2(gridContainer, plotViews);

            var testSignalModel = new PlotModel { Title = "Тестовый сигнал" };
            var amplitudeSpectrumModel = new PlotModel { Title = "Амплитудный спектр" };
            var phaseSpectrumModel = new PlotModel { Title = "Фазовый спектр" };
            var restoredSignalModel = new PlotModel { Title = "Восстановленный сигнал" };

            var testSignalSeries = new LineSeries { Color = OxyColors.Red, MarkerType = MarkerType.Circle };
            var amplitudeSpectrumSeries = new StemSeries { Color = OxyColors.Green };
            var phaseSpectrumSeries = new StemSeries { Color = OxyColors.Blue };
            var restoredSignalSeries = new LineSeries { Color = OxyColors.Yellow };
            var restoredSignalNoPhaseSeries = new LineSeries { Color = OxyColors.Purple, MarkerType = MarkerType.Circle };

            // Тестовый сигнал
            List<DataPoint> points = new List<DataPoint>();

            double[] testSignal = PolyharmonicGenerator.GenerateSignal(Amplitudes, Phases, N);

            for (int i = 0; i < N; i++)
            {
                points.Add(new DataPoint(i, testSignal[i]));
            }

            testSignalSeries.Points.AddRange(points);

            testSignalModel.Series.Add(testSignalSeries);

            plotViews[0].Model = testSignalModel;

            // Амплитудный спектр
            points = new List<DataPoint>();

            double[] amplitudes = DiscreteFourierTransform.GetHarmonicsAmplitudes(testSignal, 0, N, N);

            for (int i = 0; i < N; i++)
            {
                points.Add(new DataPoint(i, amplitudes[i]));
            }

            amplitudeSpectrumSeries.Points.AddRange(points);

            amplitudeSpectrumModel.Series.Add(amplitudeSpectrumSeries);

            plotViews[1].Model = amplitudeSpectrumModel;

            // Фазовый спектр
            points = new List<DataPoint>();

            double[] phases = DiscreteFourierTransform.GetHarmonicsPhases(testSignal, 0, N, N);

            for (int i = 0; i < N; i++)
            {
                points.Add(new DataPoint(i, phases[i]));
            }

            phaseSpectrumSeries.Points.AddRange(points);

            phaseSpectrumModel.Series.Add(phaseSpectrumSeries);

            plotViews[2].Model = phaseSpectrumModel;

            // Восстановленный сигнал
            points = new List<DataPoint>();

            double[] restoredSignal = PolyharmonicGenerator.GenerateRestoredSignal(amplitudes, phases, N, true);
            double[] restoredSignalNoPhase = PolyharmonicGenerator.GenerateRestoredSignal(amplitudes, phases, N, false);

            for (int i = 0; i < N; i++)
            {
                restoredSignalSeries.Points.Add(new DataPoint(i, restoredSignal[i]));
                restoredSignalNoPhaseSeries.Points.Add(new DataPoint(i, restoredSignalNoPhase[i]));
            }

            restoredSignalModel.Series.Add(restoredSignalSeries);
            //restoredSignalModel.Series.Add(restoredSignalNoPhaseSeries);

            plotViews[3].Model = restoredSignalModel;
        }

        private void FFTPolyharmonicSignal()
        {
            var plots = InitializePlots(4);

            GridHelper.SetGrid2x2(gridContainer, plots);

            // Тестовый полигармонический сиганл
            double[] testSignal = PolyharmonicGenerator.GenerateSignal(Amplitudes, Phases, N);

            var testSignalModel = new PlotModel { Title = "Тестовый сигнал" };

            var testSignalSeries = new LineSeries { Color = OxyColor.FromRgb(255, 0, 0) };

            for (int i = 0; i < N; i++)
            {
                testSignalSeries.Points.Add(new DataPoint(i, testSignal[i]));
            }

            testSignalModel.Series.Add(testSignalSeries);

            plots[0].Model = testSignalModel;

            // Амплитудный спектр
            double[] amplitudes = DiscreteFourierTransform.GetHarmonicsAmplitudes(testSignal, 0, N, N);

            var amplitudeSpectrumModel = new PlotModel { Title = "Амплитудный спектр" };

            var amplitudeSpectrumSeries = new StemSeries { Color = OxyColors.Blue };

            for (int i = 0; i < N; i++)
            {
                amplitudeSpectrumSeries.Points.Add(new DataPoint(i, amplitudes[i]));
            }

            amplitudeSpectrumModel.Series.Add(amplitudeSpectrumSeries);

            plots[1].Model = amplitudeSpectrumModel;

            // Фазовый спектр
            double[] phases = DiscreteFourierTransform.GetHarmonicsPhases(testSignal, 0, N, N);

            var phaseSpectrumModel = new PlotModel { Title = "Фазовый спектр" };

            var phaseSpectrumSeries = new StemSeries { Color = OxyColors.Yellow };

            for (int i = 0; i < N; i++)
            {
                phaseSpectrumSeries.Points.Add(new DataPoint(i, phases[i]));
            }

            phaseSpectrumModel.Series.Add(phaseSpectrumSeries);

            plots[2].Model = phaseSpectrumModel;

            // Восстановленный сигнал
            double[] restoredSignal = PolyharmonicGenerator.GenerateRestoredSignal(amplitudes, phases, N, true);
            double[] restoredSignalNoPhase = PolyharmonicGenerator.GenerateRestoredSignal(amplitudes, phases, N, false);

            var restoredSignalSeries = new LineSeries { Color = OxyColors.Green};

            for (int i = 0; i < N; i++)
            {
                restoredSignalSeries.Points.Add(new DataPoint(i, restoredSignal[i]));
            }

            var restoredSignalModel = new PlotModel { Title = "Восстановленный сигнал" };

            restoredSignalModel.Series.Add(restoredSignalSeries);

            plots[3].Model = restoredSignalModel;
        }

        private void FilterSignal()
        {
            var plots = InitializePlots(4);

            GridHelper.SetGrid2x2(gridContainer, plots);

            // Тестовый полигармонический сиганл
            double[] testSignal = PolyharmonicGenerator.GenerateSignal(Amplitudes, Phases, N);

            var testSignalModel = new PlotModel { Title = "Тестовый сигнал" };

            var testSignalSeries = new LineSeries { Color = OxyColor.FromRgb(255, 0, 0) };

            for (int i = 0; i < N; i++)
            {
                testSignalSeries.Points.Add(new DataPoint(i, testSignal[i]));
            }

            testSignalModel.Series.Add(testSignalSeries);

            plots[0].Model = testSignalModel;

            // Низкочастотный фильтр
            double[] amplitudesLF = DiscreteFourierTransform.GetHarmonicsAmplitudes(testSignal, 0, N, N).Where(x => x < 15).ToArray();

            var amplitudesFilterLFSeries = new StemSeries { Color = OxyColors.Green };

            for (int i = 0; i < amplitudesLF.Length; i++)
            {
                amplitudesFilterLFSeries.Points.Add(new DataPoint(i, amplitudesLF[i]));
            }

            var amplitudesFilterLFModel = new PlotModel { Title = "Низкочастотный фильтр" };

            amplitudesFilterLFModel.Series.Add(amplitudesFilterLFSeries);

            plots[1].Model = amplitudesFilterLFModel;

            // Высокочастотный фильтр
            double[] amplitudesHF = DiscreteFourierTransform.GetHarmonicsAmplitudes(testSignal, 0, N, N).Where(x => x > 15).ToArray();

            var amplitudesFilterHFSeries = new StemSeries { Color = OxyColors.Blue };

            for (int i = 0; i < amplitudesHF.Length; i++)
            {
                amplitudesFilterHFSeries.Points.Add(new DataPoint(i, amplitudesHF[i]));
            }

            var amplitudesFilterHFModel = new PlotModel { Title = "Высокочастотный фильтр" };

            amplitudesFilterHFModel.Series.Add(amplitudesFilterHFSeries);

            plots[2].Model = amplitudesFilterHFModel;
        }
    }
}
