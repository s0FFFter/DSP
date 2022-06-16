using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignalProcessing_Lab2.Generators
{
    public static class PolyharmonicGenerator
    {
        private static Random _random = new Random();
        public static double[] GenerateSignal(double[] amplitudes, double[] phases, int N)
        {
            List<double> polyharmonicSignal = new List<double>();

            for (int i = 0; i < N; i++)
            {
                double harmonic = 0;

                for (int j = 1; j <= 30; j++)
                {
                    harmonic += amplitudes[_random.Next(amplitudes.Length)] * Math.Cos((2 * Math.PI * j * i) / N - phases[_random.Next(phases.Length)]);
                }

                polyharmonicSignal.Add(harmonic);
            }

            return polyharmonicSignal.ToArray();
        }

        public static double[] GenerateRestoredSignal(double[] amplitudes, double[] phases, int N, bool isPhases)
        {
            List<double> restored = new List<double>();

            for (int i = 0; i < N; i++)
            {
                double harmonic = 0;
                harmonic += amplitudes[0] / 2;

                for (int j = 1; j < N / 2; j++)
                {
                    harmonic += amplitudes[j] * Math.Cos(((2 * Math.PI * j * i) / N) - (isPhases == true ? phases[j] : 0));
                }

                restored.Add(harmonic);
            }

            return restored.ToArray();
        }
    }
}
