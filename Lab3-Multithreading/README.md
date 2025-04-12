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

W celu uzyskania wiarygodnych wyników, pomiary czasu wykonania były uśredniane z wielu prób. Dodatkowo porównano rezultaty dla różnych rozmiarów macierzy (100x100, 300x300, 500x500) oraz różnych liczby wątków (1, 2, 4, 8, 16, 32), także przekraczających liczbę logicznych rdzeni CPU. Badania zostały przeprowadzone na komputerze wyposażonym w procesor AMD Ryzen 5 3600, kartę graficzną GTX 1660 VENTUS oraz 16 GB pamięci RAM.

Wyniki są następujące:

#### Rozmiar macierzy: 100x100

| Liczba wątków | Parallel (czas) | Przyspieszenie | Thread (czas) | Przyspieszenie |
|---------------|------------------|---------------|----------------|-------------|
| 1             | 19,08 ms         | 1,00x         | 46,91 ms       | 1,00x       |
| 2             | 9,76 ms          | 1,95x         | 19,57 ms       | 2,40x       |
| 4             | 5,43 ms          | 3,51x         | 20,27 ms       | 2,31x       |
| 8             | 6,27 ms          | 3,04x         | 44,87 ms       | 1,05x       |
| 16            | 5,78 ms          | 3,30x         | 76,09 ms       | 0,62x       |
| 32            | 3,45 ms          | 5,53x         | 151,35 ms      | 0,31x       |

- `Parallel` zdecydowanie lepiej radzi sobie przy większej liczbie wątków — zauważalny jest wzrost przyspieszenia aż do 5,53x (dla 32 wątków).
- W przypadku `Thread`, wydajność spada przy większej liczbie wątków — przy 32 jest znacznie gorzej niż przy 1 (0,31x), co świadczy o **przeciążeniu** systemu zarządzaniem wątkami i wysokim narzucie kontekstowym.

#### Rozmiar macierzy: 200x200

| Liczba wątków | Parallel (czas) | Przyspieszenie | Thread (czas) | Przyspieszenie |
|---------------|------------------|---------------|----------------|-------------|
| 1             | 128,15 ms        | 1,00x         | 139,05 ms      | 1,00x       |
| 2             | 70,13 ms         | 1,83x         | 80,74 ms       | 1,72x       |
| 4             | 40,79 ms         | 3,14x         | 57,97 ms       | 2,40x       |
| 8             | 27,49 ms         | 4,66x         | 68,07 ms       | 2,04x       |
| 16            | 25,05 ms         | 5,11x         | 107,00 ms      | 1,30x       |
| 32            | 21,35 ms         | 6,00x         | 181,92 ms      | 0,76x       |

- `Parallel` dalej pokazuje stabilne i rosnące przyspieszenie wraz z liczbą wątków — do 6x.
- `Thread` początkowo wykazuje poprawę, ale po przekroczeniu 4–8 wątków wydajność spada, a czas obliczeń zaczyna rosnąć.

#### Rozmiar macierzy: 500x500

| Liczba wątków | Parallel (czas) | Przyspieszenie | Thread (czas) | Przyspieszenie |
|---------------|------------------|---------------|----------------|-------------|
| 1             | 2060,06 ms       | 1,00x         | 2153,59 ms     | 1,00x       |
| 2             | 1103,38 ms       | 1,87x         | 1127,01 ms     | 1,91x       |
| 4             | 620,14 ms        | 3,32x         | 658,86 ms      | 3,27x       |
| 8             | 432,89 ms        | 4,76x         | 491,27 ms      | 4,38x       |
| 16            | 352,79 ms        | 5,84x         | 518,47 ms      | 4,15x       |
| 32            | 347,89 ms        | 5,92x         | 586,30 ms      | 3,67x       |

- Obie metody pokazują wzrost przyspieszenia do pewnego momentu.
- `Parallel` ponownie okazuje się bardziej stabilny i efektywny, nawet przy 32 wątkach.
- `Thread` przy większej liczbie wątków nadal działa gorzej niż `Parallel`, mimo zauważalnych przyspieszeń do ok. 4x–5x.

#### Wykresy porównawcze

<img src="https://github.com/user-attachments/assets/6bbfe440-73bb-4ce7-b9aa-abd20f417e2c" alt="image" width="800">

#### **Wnioski**

- **Zastosowanie równoległości znacząco skraca czas obliczeń** – zarówno przy wykorzystaniu wysokopoziomowego API `Parallel`, jak i niskopoziomowego `Thread`, uzyskano istotne skrócenie czasu przetwarzania w porównaniu do wersji sekwencyjnej.
- **Zyski ze zrównoleglenia rosną wraz z rozmiarem macierzy** – dla małych rozmiarów macierzy przyspieszenia były ograniczone, ponieważ narzuty związane z zarządzaniem wątkami mogły przewyższyć zyski z równoległości. Natomiast dla dużych macierzy, wzrost liczby wątków dawał wyraźne skrócenie czasu.
- **Optymalna liczba wątków nie zawsze pokrywa się z fizyczną liczbą rdzeni** – wyniki pokazały, że przy odpowiednio dużych danych nawet liczba wątków większa niż liczba rdzeni mogła przynieść poprawę (w przypadku `Parallel`). Jednak w przypadku `Thread`, przekroczenie tej granicy skutkowało wyraźnym spadkiem efektywności.
- **Parallel działa stabilniej i wydajniej przy większej liczbie wątków** – dla wszystkich rozmiarów macierzy biblioteka `Parallel` lepiej radziła sobie z przydziałem zadań i zarządzaniem zasobami. Szczególnie przy większej liczbie wątków, `Thread` traciło na wydajności, a nawet powodowało pogorszenie czasu względem mniejszej liczby wątków.
- **Wydajność `Thread` spada przy zbyt dużej liczbie wątków** – tworzenie i zarządzanie zbyt wieloma wątkami manualnie powodowało większe narzuty systemowe i przełączenia kontekstu, co prowadziło do spadku efektywności. W skrajnych przypadkach, czas był gorszy niż dla wersji jednoprocesowej.

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

#### Wygląd aplikacji wraz z przykładową grafiką

![image](https://github.com/user-attachments/assets/f57261fa-191d-4337-b67c-391367f571e1)


