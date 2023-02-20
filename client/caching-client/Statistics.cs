using System;
using System.Collections.Generic;
using System.Linq;

public static class Statistics
{
    public static void Stats<T>(string title, List<T> numbers) where T : struct, IComparable, IFormattable, IConvertible
    {
        if (numbers.Count == 0)
        {
            Console.WriteLine("No statistics to compute - list is empty.");
            return;
        }

        Console.WriteLine($"\n{title}\n");

        double[] convertedNumbers = numbers.Select(x => Convert.ToDouble(x)).ToArray();

        double min = convertedNumbers.Min();
        double max = convertedNumbers.Max();
        double mean = convertedNumbers.Average();
        double variance = convertedNumbers.Select(x => Math.Pow(x - mean, 2)).Sum() / (convertedNumbers.Length - 1);
        double standardDeviation = Math.Sqrt(variance);

        Console.WriteLine("Minimum value: {0}", min);
        Console.WriteLine("Maximum value: {0}", max);
        Console.WriteLine("Mean: {0:F2}", mean);
        Console.WriteLine("Variance: {0:F2}", variance);
        Console.WriteLine("Standard deviation: {0:F2}\n", standardDeviation);
    }
}