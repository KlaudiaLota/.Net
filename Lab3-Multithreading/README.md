## Porównanie metod równoległego przetwarzania z użyciem Parallel i Thread

Celem zadania było przeanalizowanie przyspieszenia obliczeń równoległych względem sekwencyjnych na przykładzie mnożenia macierzy, z wykorzystaniem dwóch podejść: wysokopoziomowej biblioteki `Parallel` oraz niskopoziomowej klasy `Thread`.

Użytkownik może na początku programu określić rozmiar macierzy, liczbę wątków oraz liczbę powtórzeń obliczeń, które zostaną użyte do przetwarzania. Macierze są generowane losowo, a ich mnożenie odbywa się w dwóch trybach:

1. **Z użyciem `Parallel.For`** – wersja wysokopoziomowa, wykorzystująca mechanizmy dostępne w .NET do zarządzania zadaniami i ich podziałem na dostępne rdzenie CPU. Obliczenia równoległe były skalowane w zależności od podanej liczby wątków, co pozwoliło uzyskać widoczny wzrost wydajności wraz ze wzrostem liczby wątków, aż do pewnego momentu nasycenia zasobów CPU.
      ```csharp
      Parallel.For(0, rows, new ParallelOptions { MaxDegreeOfParallelism = threadCount }, i =>
    {
        for (int j = 0; j < cols; j++)
        {
            double sum = 0;
            for (int k = 0; k < common; k++)
                sum += A[i, k] * B[k, j];
            result[i, j] = sum;
        }
    });
      ```

3. **Z użyciem `Thread`** – wersja niskopoziomowa, w której tworzenie i zarządzanie wątkami realizowane było ręcznie. Każdy wątek odpowiedzialny był za obliczenie pewnej części macierzy wynikowej. Choć podejście to daje dużą kontrolę nad równoległością, wiąże się z większą złożonością implementacyjną i potencjalnie większym narzutem czasowym wynikającym z kosztów zarządzania wątkami.
      ```csharp
      for (int t = 0; t < threadCount; t++)
      {
          int start = t * rows / threadCount;
          int end = (t + 1) * rows / threadCount;
          threads[t] = new Thread(() => MultiplyRange(A, B, result, start, end));
          threads[t].Start();
      }
      foreach (var thread in threads)
          thread.Join();
      ```

W celu uzyskania wiarygodnych wyników, pomiary czasu wykonania były uśredniane z wielu prób. Dodatkowo porównano rezultaty dla różnych rozmiarów macierzy (100x100, 300x300, 500x500) oraz różnych liczby wątków (1, 2, 4, 8, 16, 32), także przekraczających liczbę logicznych rdzeni CPU. 

Wyniki są następujące:
...

## Aplikacja okienkowa do równoległego przetwarzania obrazów

Celem było stworzenie aplikacji okienkowej (Windows Forms), która pozwala użytkownikowi wybrać obraz z dysku, a następnie zastosować kilka filtrów przetwarzających ten obraz – każdy z nich w osobnym wątku. Zgodnie z założeniami, przetwarzanie każdego filtru odbywało się równolegle, natomiast sam filtr działał sekwencyjnie.

Po załadowaniu obrazu aplikacja tworzyła jego kopie, po jednej dla każdego filtra. Następnie każda kopia była przetwarzana przez osobną funkcję filtrującą, działającą w osobnym wątku. 

Zaimplementowane filtry to:
- **Odcienie szarości** – przekształcenie koloru każdego piksela na wartość szarości.
```csharp
int avg = (pixel.R + pixel.G + pixel.B) / 3;
```
- **Negatyw** – odwrócenie wartości RGB.
```csharp
Color.FromArgb(255 - r, 255 - g, 255 - b);
```
- **Lustrzane odbicie** – odbicie obrazu w poziomie.
```csharp
result.SetPixel(width - x - 1, y, color);
```
- **Progowanie** – uproszczenie obrazu do czerni i bieli na podstawie jasności piksela.
```csharp
avg > threshold ? Color.White : Color.Black;
```

Po zakończeniu przetwarzania, wszystkie przefiltrowane obrazy były wyświetlane w oknie aplikacji w układzie 2x2, z podpisami pod obrazami. Użytkownik mógł łatwo porównać efekty działania każdego filtra z oryginałem. Aby poprawić estetykę interfejsu, obrazy były skalowane do stałego rozmiaru i ustawione w trybie `Zoom`, co umożliwiało ich pełne wyświetlenie bez zniekształceń.

**Wygląd aplikacji wraz z przykładową grafiką**

![image](https://github.com/user-attachments/assets/f57261fa-191d-4337-b67c-391367f571e1)


