using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignalProcessing_Lab3
{
    public struct Amplitude
    { 
        public double CosPart { get; init; }
        public double SinPart { get; init; }
    }

    public static class DiscreteFourierTransform
    {
        private static Amplitude[] CalculateAmplitudes(double[] signal)
        {
            int N = signal.Length;
            int M = (N % 2 == 0) ? N / 2 : (N - 1) / 2;

            List<Amplitude> amplitudes = new List<Amplitude>();

            for (int i = 0; i < M; i++)
            {
                double sinPart = 0, cosPart = 0;
                for (int j = 0; j < N; j++)
                {
                    sinPart += signal[j] * Math.Cos(2 * Math.PI * i * j / N);
                    cosPart += signal[j] * Math.Cos(2 * Math.PI * i * j / N);
                }
                amplitudes.Add(new Amplitude { SinPart = sinPart, CosPart = cosPart });
            }

            return amplitudes.ToArray();
        }

        public static double[] GetAmplitudeSpectrum(double[] signal)
        {
            Amplitude[] amplitudes = CalculateAmplitudes(signal);
            List<double> amplitudeSpectrum = new List<double>();

            for (int i = 0; i < amplitudes.Length; i++)
            {
                amplitudeSpectrum.Add(Math.Sqrt(amplitudes[i].CosPart * amplitudes[i].CosPart + amplitudes[i].SinPart * amplitudes[i].SinPart));
            }
            return amplitudeSpectrum.ToArray();
        }

        public static double[] GetPhaseSpectrum(double[] signal)
        {
            Amplitude[] amplitudes = CalculateAmplitudes(signal);
            List<double> phaseSpectrum = new List<double>();

            for (int i = 0; i < amplitudes.Length; i++)
            {
                phaseSpectrum.Add(Math.Atan2(amplitudes[i].SinPart, amplitudes[i].CosPart));
            }
            return phaseSpectrum.ToArray();
        }
    }
}
