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

namespace DigitalSignalProcessing_Lab3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly double[] _signal;

        private const int N = 256;
        private const int B1 = 100;
        private const int B2 = 3;
        public MainWindow()
        {
            InitializeComponent();

            _signal = Signal.GenerateSignal(N, B1, B2);

            OriginalSignal();
        }

        private void menuItem_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;

            switch (element.Name)
            {
                case "originalSignalItem":
                    OriginalSignal();
                    break;
                case "slidingAveragingItem":
                    SlidingAveraging();
                    break;
                case "fourDegreeParabolaItem":
                    FourthDegreeParabola();
                    break;
                case "medianFilteringItem":
                    MedianAveraging();
                    break;
            }
        }

        private void OriginalSignal()
        {
            Drawer.DrawOriginalSignal(_signal, plotViewA);
            Drawer.DrawAmplitudeSpectrum(_signal, plotViewB);
            Drawer.DrawPhaseSpectrum(_signal, plotViewC);
        }

        private void SlidingAveraging()
        {
            Drawer.DrawAntialiasedSignal(_signal, AntialiasingAlgorithms.SmoothSlidingAveraging(_signal, 5, 1), plotViewA, OxyColors.Yellow);
            Drawer.DrawAmplitudeSpectrum(AntialiasingAlgorithms.SmoothSlidingAveraging(_signal, 5, 1), plotViewB);
            Drawer.DrawPhaseSpectrum(AntialiasingAlgorithms.SmoothSlidingAveraging(_signal, 5, 1), plotViewC);
        }

        private void FourthDegreeParabola()
        {
            Drawer.DrawAntialiasedSignal(_signal, AntialiasingAlgorithms.SmoothFourthDegreeParabola(_signal, FourthDegreeType.Eleven), plotViewA, OxyColors.Purple);
            Drawer.DrawAmplitudeSpectrum(AntialiasingAlgorithms.SmoothFourthDegreeParabola(_signal, FourthDegreeType.Eleven), plotViewB);
            Drawer.DrawPhaseSpectrum(AntialiasingAlgorithms.SmoothFourthDegreeParabola(_signal, FourthDegreeType.Eleven), plotViewC);
        }

        private void MedianAveraging()
        {
            Drawer.DrawAntialiasedSignal(_signal, AntialiasingAlgorithms.SmoothMedianFiltering(_signal, 7), plotViewA, OxyColors.Aqua);
            Drawer.DrawAmplitudeSpectrum(AntialiasingAlgorithms.SmoothMedianFiltering(_signal, 7), plotViewB);
            Drawer.DrawPhaseSpectrum(AntialiasingAlgorithms.SmoothMedianFiltering(_signal, 7), plotViewC);
        }
    }
}
