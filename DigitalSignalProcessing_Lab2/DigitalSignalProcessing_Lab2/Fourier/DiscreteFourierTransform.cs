using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DigitalSignalProcessing_Lab2.Fourier
{
    public static class DiscreteFourierTransform
    {
        public static double CalculateHarmonicAmplitude(double[] signal, int harmonicNumber, int N)
        {
            double cosAmplitude = 0, sinAmplitude = 0;

            for (int i = 0; i < N; i++)
            {
                cosAmplitude += signal[i] * Math.Cos((2 * Math.PI * i * harmonicNumber) / N);
                sinAmplitude += signal[i] * Math.Sin((2 * Math.PI * i * harmonicNumber) / N);
            }

            cosAmplitude *= 2 / (double)N;
            sinAmplitude *= 2 / (double)N;

            return Math.Sqrt(cosAmplitude * cosAmplitude + sinAmplitude * sinAmplitude);
        }

        public static double CalculateHarmonicPhase(double[] signal, int harmonicNumber, int N)
        {
            double cosAmplitude = 0, sinAmplitude = 0;

            for (int i = 0; i < N; i++)
            {
                cosAmplitude += signal[i] * Math.Cos((2 * Math.PI * i * harmonicNumber) / N);
                sinAmplitude += signal[i] * Math.Sin((2 * Math.PI * i * harmonicNumber) / N);
            }

            cosAmplitude *= 2 / (double)N;
            sinAmplitude *= 2 / (double)N;

            return Math.Atan2(sinAmplitude, cosAmplitude);
        }

        public static double[] GetHarmonicsAmplitudes(double[] signal, int lower, int upper, int N)
        {
            List<double> amplitudes = new List<double>();

            for (int i = lower; i < upper; i++)
            {
                amplitudes.Add(CalculateHarmonicAmplitude(signal, i, N));
            }

            return amplitudes.ToArray();
        }

        public static double[] GetHarmonicsPhases(double[] signal, int lower, int upper, int N)
        {
            List<double> phases = new List<double>();

            for (int i = lower; i < upper; i++)
            {
                phases.Add(CalculateHarmonicPhase(signal, i, N));
            }

            return phases.ToArray();
        }
    }
}
