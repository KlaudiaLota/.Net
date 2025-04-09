using System.Diagnostics;

namespace MatrixMultiply;

internal class Program
{
    static void Main(string[] args)
    {
        int[] sizes = { 100, 200, 500 };
        int[] threadCount = { 1, 2, 4, 8, 16, 32};
        int trials = 10;

        foreach (int size in sizes)
        {
            Console.WriteLine($"\nRozmiar macierzy: {size}x{size}");
            double baseParallelTime = 0;
            double baseThreadTime = 0;

            foreach (int threads in threadCount)
            {
                double totalParallelTime = 0;
                double totalThreadTime = 0;

                for (int t = 0; t < trials; t++)
                {
                    Matrix a = new Matrix(size, size);
                    Matrix b = new Matrix(size, size);
                    a.FillRandom();
                    b.FillRandom();

                    var sw = Stopwatch.StartNew();
                    var resultParallel = MatrixMultiplier.Multiply(a, b, threads);
                    sw.Stop();
                    totalParallelTime += sw.Elapsed.TotalMilliseconds;

                    sw.Restart();
                    var resultThread = MatrixMultiplier.MultiplyWithThreads(a, b, threads);
                    sw.Stop();
                    totalThreadTime += sw.Elapsed.TotalMilliseconds;
                }

                double avgParallel = totalParallelTime / trials;
                double avgThread = totalThreadTime / trials;

                if (threads == 1)
                {
                    baseParallelTime = avgParallel;
                    baseThreadTime = avgThread;
                }

                double speedupParallel = baseParallelTime / avgParallel;
                double speedupThread = baseThreadTime / avgThread;

                Console.WriteLine($"Wątki: {threads} | Parallel: {avgParallel:F2} ms ({speedupParallel:F2}x) | Thread: {avgThread:F2} ms ({speedupThread:F2}x)");
            }
        }
    }
}

