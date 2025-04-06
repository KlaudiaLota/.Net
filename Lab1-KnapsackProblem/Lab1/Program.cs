namespace KnapsackProblem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the seed :");
            int seed = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the number of items :");
            int n = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter the capacity :");
            int capacity = int.Parse(Console.ReadLine());

            Problem problem = new Problem(n, seed);
            Console.WriteLine(problem);

            problem.PrintSortedItems();

            var result = problem.Solve(capacity);
            Console.WriteLine(result);
        }
    }
}
