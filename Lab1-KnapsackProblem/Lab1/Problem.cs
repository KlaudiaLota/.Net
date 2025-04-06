using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
[assembly: InternalsVisibleTo("Tests"), InternalsVisibleTo("WinFormsApp1")]

namespace KnapsackProblem
{
    public class Problem
    {
        private int n;
        public List<Item> items;
        private List<int> selectedItems;

        public Problem(int n, int seed)
        {   
            this.n = n;
            items = new List<Item>();
            Random random = new Random(seed);

            for (int i = 0; i < n; i++)
            {
                int value = random.Next(1, n+1);
                int weight = random.Next(1, n+1);
                items.Add(new Item(i+1, value, weight));
            }
        }
        public List<int> getValues()
        {
            return items.ConvertAll(item => item.Value);
        }
        public List<int> getWeights()
        {
            return items.ConvertAll(item => item.Weight);
        }
        public List<Item> GetItems()
        {
            return items;
        }

        public Result Solve(int capacity)
        {
            items.Sort((a, b) => b.ValuePerWeight.CompareTo(a.ValuePerWeight));

            selectedItems = new List<int>();
            int totalValue = 0;
            int totalWeight = 0;

            foreach (var item in items)
            {
                if (totalWeight + item.Weight <= capacity)
                {
                    selectedItems.Add(item.Index);
                    totalValue += item.Value;
                    totalWeight += item.Weight;
                }
            }

            return new Result(totalValue, totalWeight, selectedItems);
        }

        public override string ToString()
        {
            string result = "Problem with " + n + " items:";
            foreach (var item in items)
            {
                result += "\n" + item;
            }
            return result;
        }
        public void PrintSortedItems()
        {
            items.Sort((a, b) => b.ValuePerWeight.CompareTo(a.ValuePerWeight));
            Console.WriteLine("Sorted items:");
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
        }
    }
}