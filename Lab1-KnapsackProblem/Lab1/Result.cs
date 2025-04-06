using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnapsackProblem
{
    public class Result
    {
        public int TotalValue { get; }
        public int TotalWeight { get; }
        public List<int> SelectedItems { get; }

        public Result(int totalValue, int totalWeight, List<int> selectedItems)
        {
            TotalValue = totalValue;
            TotalWeight = totalWeight;
            SelectedItems = selectedItems;
        }

        public override string ToString()
        {
            return $"Total value: {TotalValue}, Total weight: {TotalWeight}, Selected items: {string.Join(", ", SelectedItems)}";
        }

    }
}
