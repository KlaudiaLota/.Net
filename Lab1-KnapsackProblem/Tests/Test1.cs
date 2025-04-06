using KnapsackProblem;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Tests
{
    [TestClass]
    public sealed class Tests
    {
        // Test: Sprawdzenie, czy jeśli co najmniej jeden przedmiot spełnia ograniczenia, to zwrócono co najmniej jeden element.
        [TestMethod]
        public void Solve_ShouldReturnAtLeastOneItem_WhenAtLeastOneItemFitsTheCapacity()
        {
            var problem = new Problem(10, 20);
            int capacity = 20;

            var result = problem.Solve(capacity);

            Assert.IsTrue(result.SelectedItems.Count > 0, "Should return at least one item.");
        }

        // Test: Sprawdzenie, czy jeśli żaden przedmiot nie spełnia ograniczeń, to zwrócono puste rozwiązanie.
        [TestMethod]
        public void Solve_ShouldReturnEmptySelection_WhenNoItemFitsTheCapacity()
        {
            var problem = new Problem(10, 20);
            int capacity = 0;

            var result = problem.Solve(capacity);

            Assert.AreEqual(0, result.SelectedItems.Count, "Selection should be empty.");
        }

        // Test: Sprawdzenie poprawności wyniku dla konkretnej instancji.
        [TestMethod]
        public void Solve_ShouldReturnCorrectResult_ForSpecificInstance()
        {
            var problem = new Problem(10, 20);
            int capacity = 20;

            var result = problem.Solve(capacity);

            int expectedValue = 29;
            int expectedWeight = 18;
            List<int> expectedSelectedItems = new List<int> { 3, 10, 4, 8, 6, 9 };

            Assert.AreEqual(expectedValue, result.TotalValue, "Total value is incorrect.");
            Assert.AreEqual(expectedWeight, result.TotalWeight, "Total weight is incorrect.");
            CollectionAssert.AreEqual(expectedSelectedItems, result.SelectedItems, "Selected items are incorrect.");
        }

        // Test: Sprawdzenie, czy program bierze pod uwagę wartość po przecinku przy sortowaniu przedmiotów
        [TestMethod]
        public void Solve_ShouldPickItemWithHigherValuePerWeightEvenIfDifferenceIsSmall()
        {
            var problem = new Problem(2, 42);
            problem.items.Clear();
            problem.items.Add(new Item(0, 8, 6));  // ValuePerWeight = 1.33
            problem.items.Add(new Item(1, 9, 7));  // ValuePerWeight ≈ 1.29
            int capacity = 20;

            var result = problem.Solve(capacity);

            Assert.IsTrue(result.SelectedItems.Count > 0, "At least one item should be selected.");
            Assert.AreEqual(0, result.SelectedItems[0], "Item with higher ValuePerWeight should be picked first.");
        }

        // Test: Sprawdzenie, czy program szybko obsługuje duże liczby
        [TestMethod]
        public void Solve_ShouldWorkEfficientlyForLargeInputs()
        {
            int n = 100000;
            var problem = new Problem(n, 42);
            int capacity = 500000;

            var watch = System.Diagnostics.Stopwatch.StartNew();
            var result = problem.Solve(capacity);
            watch.Stop();

            Assert.IsTrue(watch.ElapsedMilliseconds < 2000, "The solution should work quickly (below 2s for 100,000 items).");
        }


    }
}
