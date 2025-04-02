Celem tych laboratoriów była implementacja rozwiązania problemu plecakowego. W tym celu opracowano aplikację w języku C# z wykorzystaniem Windows Forms, która pozwala użytkownikowi na wprowadzenie ziarna (seed), liczby przedmiotów oraz pojemności plecaka w celu wygenerowania losowych przedmiotów.

![image](https://github.com/user-attachments/assets/f02eb5ea-0812-47ed-9b67-56ffc9933c2e)

Podczas wprowadzania danych do formularza system sprawdza poprawność wpisywanych wartości – dozwolone są wyłącznie liczby. Jeśli użytkownik wprowadzi inne znaki, pole zostanie podświetlone na czerwono, a wyświetlony komunikat poinformuje o konieczności wpisania dodatniej liczby.  

Po kliknięciu przycisku „Solve problem” aplikacja weryfikuje, czy wszystkie pola zostały poprawnie wypełnione i czy żadne z nich nie zawiera błędu. Jeśli dane są prawidłowe, w prawym panelu pojawi się lista wylosowanych przedmiotów wraz z ich wagą i wartością.  

Problem plecakowy rozwiązywany jest algorytmem zachłannym, który sortuje przedmioty malejąco według stosunku wartości do wagi, a następnie dodaje je do plecaka, dopóki pozwala na to jego pojemność. Ostateczny wynik jest wyświetlany w dolnym oknie aplikacji.

Oprócz rozwiązania problemu i stworzenia aplikacji okienkowej zaimplementowano również testy algorytmu, które pozwalają na weryfikację jego poprawności i efektywności.

![image](https://github.com/user-attachments/assets/9a6376e0-5d78-4919-a77a-2560a4141dcd)

Testy weryfikują poprawność działania algorytmu plecakowego w różnych scenariuszach:

1. **Przynajmniej jeden przedmiot w plecaku**
   Jeśli przynajmniej jeden przedmiot mieści się w plecaku, algorytm powinien zwrócić co najmniej jedno rozwiązanie. Sprawdzane jest to przez poniższy test:
     ```csharp
     Assert.IsTrue(result.SelectedItems.Count > 0, "Should return at least one item.");
3. **Pojemność plecaka równa 0**
     ```csharp
     Assert.AreEqual(0, result.SelectedItems.Count, "Selection should be empty.");
5. **Sprawdzanie wyników dla określonego zestawu danych**
     ```csharp
     Assert.AreEqual(expectedValue, result.TotalValue, "Total value is incorrect.");
     Assert.AreEqual(expectedWeight, result.TotalWeight, "Total weight is incorrect.");
     CollectionAssert.AreEqual(expectedSelectedItems, result.SelectedItems, "Selected items are incorrect.");
7. **Preferowanie przedmiotów o wyższej wartości w stosunku do wagi**
     ```csharp
     Assert.IsTrue(result.SelectedItems.Count > 0, "At least one item should be selected.");
     Assert.AreEqual(0, result.SelectedItems[0], "Item with higher ValuePerWeight should be picked first.");
9. **Efektywność algorytmu przy dużej liczbie przedmiotów**
    ```csharp
    Assert.IsTrue(watch.ElapsedMilliseconds < 2000, "The solution should work quickly (below 2s for 100,000 items).");
      

Aplikacja skutecznie rozwiązuje problem plecakowy, oferując intuicyjny interfejs oraz efektywność działania algorytmu. Przeprowadzone testy pozwoliły zweryfikować zarówno poprawność, jak i wydajność rozwiązania, co stanowi solidną podstawę do dalszych modyfikacji i usprawnień.
