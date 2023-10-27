using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AndroidTesting
{
    public class Testing
    {
        public static async Task<string> Start(long size = 100, long repeat = 1)
        {
            StringBuilder stringBuilder = new StringBuilder();
            int n = 170;
            Stopwatch stopwatch;
            List<double> Omil = new();
            List<double> Mmil = new();
            List<long> Otik = new();
            List<long> Mtik = new();
            List<double> OP = new();
            List<double> MP = new();

            for (int i = 0; i < repeat; i++)
            {
                stopwatch = Stopwatch.StartNew();
                for (int a = 0; a < size; a++)
                {
                    Factorial(n);
                }
                stopwatch.Stop();
                Omil.Add(stopwatch.ElapsedMilliseconds);
                Otik.Add(stopwatch.ElapsedTicks);
                OP.Add((long)((n - 1) * size / (Omil.Last() / 1000.0)));

                stopwatch = Stopwatch.StartNew();
                Parallel.For(0, size, i => Factorial(n));
                stopwatch.Stop();
                Mmil.Add(stopwatch.ElapsedMilliseconds);
                Mtik.Add(stopwatch.ElapsedTicks);
                MP.Add((long)((n - 1) * size / (Mmil.Last() / 1000.0)));
            }

            stringBuilder.AppendLine("Однопоточный режим:");
            stringBuilder.AppendLine($"Время: {Omil.Average()} мс;");
            stringBuilder.AppendLine($"Кол-во тактов: {Otik.Average()};");
            stringBuilder.AppendLine($"Пропускная способность: {OP.Average()} операций в секунду;");
            stringBuilder.AppendLine("\nМногопоточный режим:");
            stringBuilder.AppendLine($"Время: {Mmil.Average()} мс;");
            stringBuilder.AppendLine($"Кол-во тактов: {Mtik.Average()};");
            stringBuilder.AppendLine($"Пропускная способность: {MP.Average()} операций в секунду;");

            return stringBuilder.ToString();
        }

        // Функция для вычисления факториала
        private static void Factorial(int n)
        {
            double factorial = 1;
            for (int i = 2; i <= n; i++)
            {
                factorial *= i;
            }
        }
    }
}
