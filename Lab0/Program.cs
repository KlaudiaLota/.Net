namespace Lab0
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FizzBuzz test = new FizzBuzz(20);
            test.PrintFizzBuzz();
        }
    }

    class FizzBuzz
    {
        private int number;
        public FizzBuzz(int maxNumber)
        {
            number = maxNumber;
        }

        public void PrintFizzBuzz()
        {
            string toPrint;
            for (int i = 1; i <= number; i++)
            {
                if (i % 3 == 0 && i % 5 == 0)
                {
                    toPrint = "FizzBuzz";
                }
                else if (i % 3 == 0)
                {
                    toPrint = "Fizz";
                }
                else if (i % 5 == 0)
                {
                    toPrint = "Buzz";
                }
                else
                {
                    toPrint = i.ToString();
                }
                Console.WriteLine(toPrint);
            }
        }   
    }
}
