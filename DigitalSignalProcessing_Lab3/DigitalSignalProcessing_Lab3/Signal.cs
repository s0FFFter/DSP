using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DigitalSignalProcessing_Lab3
{
    public static class Signal
    {
        public static double[] GenerateSignal(int N, double B1, double B2)
        {
            List<double> signalValues = new List<double>();

            var rnd = new Random();

            for (int i = 0; i < N; i++)
            {
                double value = B1 * Math.Sin(2 * Math.PI * i / N);  
                for (int j = 50; j <= 70; j++)
                {
                    value += (double)rnd.Next(0, 2) * B2 * Math.Sin(2 * Math.PI * i * j / N);
                }
                signalValues.Add(value);
            }
            return signalValues.ToArray();
        }
    }
}
