using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using OxyPlot;
using OxyPlot.Wpf;
using OxyPlot.Series;

namespace DigitalSignalProcessing_Lab3
{
    public static class Drawer
    {
        private const int N = 512;
        private static Series OriginalSignal(double[] signal)
        {
            var visualization = new LineSeries { Color = OxyColors.Red };
            for (int i = 0; i < signal.Length; i++)
            {
                visualization.Points.Add(new DataPoint(i, signal[i]));
            }
            return visualization;
        }
        public static void DrawOriginalSignal(double[] signal, PlotView plot)
        {
            var model = new PlotModel { Title = "Исходный сигнал" };
            model.Series.Add(OriginalSignal(signal));
            plot.Model = model;
        }

        public static void DrawAntialiasedSignal(double[] originalSignal, double[] antialiasedSignal, PlotView plot, OxyColor seriesColor)
        {
            var model = new PlotModel { Title = "Сглаженный сигнал" };
            model.Series.Add(OriginalSignal(originalSignal));

            var visualization = new LineSeries { Color = seriesColor };
            for (int i = 0; i < antialiasedSignal.Length; i++)
            {
                visualization.Points.Add(new DataPoint(i, antialiasedSignal[i]));
            }
            model.Series.Add(visualization);
            plot.Model = model;
        }

        public static void DrawAmplitudeSpectrum(double[] signal, PlotView plot)
        {
            double[] amplitudeSpectrum = DiscreteFourierTransform.GetAmplitudeSpectrum(signal);

            var model = new PlotModel { Title = "Амплитудный спектр" };
            var visualization = new StemSeries { Color = OxyColor.FromRgb(0, 255, 0) };

            for (int i = 0; i < amplitudeSpectrum.Length; i++)
            {
                visualization.Points.Add(new DataPoint(i, amplitudeSpectrum[i]));
            }
            model.Series.Add(visualization);
            plot.Model = model;
        }

        public static void DrawPhaseSpectrum(double[] signal, PlotView plot)
        {
            double[] phaseSpectrum = DiscreteFourierTransform.GetPhaseSpectrum(signal);

            var model = new PlotModel { Title = "Фазовый спектр" };
            var visualization = new StemSeries { Color = OxyColor.FromRgb(0, 0, 255) };

            for (int i = 0; i < phaseSpectrum.Length; i++)
            {
                visualization.Points.Add(new DataPoint(i, phaseSpectrum[i]));
            }
            model.Series.Add(visualization);
            plot.Model = model;
        }
    }
}
