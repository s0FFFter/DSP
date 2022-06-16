using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DigitalSignalProcessing_Lab3
{
    public enum FourthDegreeType { Seven, Nine, Eleven };
    public static class AntialiasingAlgorithms
    {
        private static double DefinedOrZero(double[] signal, int index)
        {
            return (index < 0 || index >= signal.Length ? 0 : signal[index]);
        }
        private static double FourthDegreeParabola(double[] signal, int i, FourthDegreeType type)
        {
            if (!Enum.IsDefined(typeof(FourthDegreeType), type))
            {
                throw new ArgumentOutOfRangeException("FourthDegreeType doesn't contain such value");
            }
            
            switch (type)
            {
                case FourthDegreeType.Seven:
                    return (double)1 / 231 * (5 * DefinedOrZero(signal, i - 3) - 30 * DefinedOrZero(signal, i - 2) + 75 * DefinedOrZero(signal, i - 1) + 131 * DefinedOrZero(signal, i) + 75 * DefinedOrZero(signal, i + 1) - 30 * DefinedOrZero(signal, i + 2) + 5 * DefinedOrZero(signal, i + 3));
                case FourthDegreeType.Nine:
                    return (double)1 / 429 * (15 * DefinedOrZero(signal, i - 4) - 55 * DefinedOrZero(signal, i - 3) + 30 * DefinedOrZero(signal, i - 2) + 135 * DefinedOrZero(signal, i - 1) + 179 * DefinedOrZero(signal, i) + 135 * DefinedOrZero(signal, i + 1) + 30 * DefinedOrZero(signal, i + 2) - 55 * DefinedOrZero(signal, i + 3) + 15 * DefinedOrZero(signal, i + 4));
                case FourthDegreeType.Eleven:
                    return (double)1 / 429 * (18 * DefinedOrZero(signal, i - 5) - 45 * DefinedOrZero(signal, i - 4) - 10 * DefinedOrZero(signal, i - 3) + 60 * DefinedOrZero(signal, i - 2) + 120 * DefinedOrZero(signal, i - 1) + 143 * DefinedOrZero(signal, i) + 120 * DefinedOrZero(signal, i + 1) + 60 * DefinedOrZero(signal, i + 2) - 10 * DefinedOrZero(signal, i + 3) - 45 * DefinedOrZero(signal, i + 4) + 18 * DefinedOrZero(signal, i + 5));
                default:
                    return 0;
            }
        }
        private static double[] GetTimeWindow(double[] source, int i, int N)
        {
            int lower = i - (N - 1) / 2;
            int upper = i + (N - 1) / 2;

            List<double> elements = new List<double>();
            for (int j = lower; j <= upper; j++)
            {
                elements.Add(j < 0 || j >= source.Length ? 0 : source[j]);
            }
            return elements.ToArray();
        }

        private static double Mean(double[] window, int K)
        {
            Array.Sort(window);

            var segment = new ArraySegment<double>(window, K, window.Length - K);
            double sum = 0;
            for (int i = 0; i < segment.Count; i++)
            {
                sum += segment[i];
            }
            return sum / (double)segment.Count;
        }

        public static double[] SmoothSlidingAveraging(double[] signal, int N, int K)
        {
            List<double> smoothedSignal = new List<double>();
            for (int i = 0; i < signal.Length; i++)
            {
                double[] timeWindow = GetTimeWindow(signal, i, N);
                smoothedSignal.Add(Mean(timeWindow, K));
            }
            return smoothedSignal.ToArray();
        }

        public static double[] SmoothFourthDegreeParabola(double[] signal, FourthDegreeType type)
        {
            List<double> smoothedSignal = new List<double>();
            for (int i = 0; i < signal.Length; i++)
            {
                smoothedSignal.Add(FourthDegreeParabola(signal, i, type));
            }
            return smoothedSignal.ToArray();
        }

        public static double[] SmoothMedianFiltering(double[] signal, int N)
        {
            List<double> smoothedSignal = new List<double>();
            for (int i = 0; i < signal.Length; i++)
            {
                double[] timeWindow = GetTimeWindow(signal, i, N);
                Array.Sort<double>(timeWindow);
                smoothedSignal.Add(timeWindow[(timeWindow.Length - 1) / 2]);
            }
            return smoothedSignal.ToArray();
        }
    }
}
