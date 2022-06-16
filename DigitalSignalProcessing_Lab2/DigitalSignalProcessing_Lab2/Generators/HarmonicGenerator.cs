using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignalProcessing_Lab2.Generators
{
    public static class HarmonicGenerator
    {
        public static double[] GenerateSignal(Func<double, double> func, int amplitude, int frequency, double startPhase, int N)
        {
            List<double> signalValues = new List<double>();

            for (int i = 0; i < N; i++)
            {
                signalValues.Add(amplitude * func(2 * Math.PI * frequency * i / N + startPhase));
            }

            return signalValues.ToArray();
        }

        public static double[] GenerateRestoredSignal(double[] amplitudeHarmonics, double[] phaseHarmonics, int N)
        {
            List<double> restoredSignal = new List<double>();

            for (int i = 0; i < N; i++)
            {
                double sum = 0;

                for (int j = 0; j < N / 2; j++)
                {
                    sum += amplitudeHarmonics[j] * Math.Cos((2 * Math.PI * j * i) / N - phaseHarmonics[j]);
                }

                restoredSignal.Add(sum);
            }

            return restoredSignal.ToArray();
        }
    }
}
