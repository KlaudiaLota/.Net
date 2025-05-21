To repozytorium zostało stworzone w ramach pracy laboratoryjnej. Programy zostały napisane w języku C# przy użyciu platformy .NET 8. Poniżej znajduje się krótki opis zawartości poszczególnych katalogów.

# [Lab0 - FizzBuzz](./Lab0)

Program realizuje rozwiązanie klasycznego zadania FizzBuzz. Składa się z dwóch klas: `Program` i `FizzBuzz`. Klasa `FizzBuzz` przyjmuje liczbę maksymalną, do której ma liczyć, a następnie wypisuje na konsolę liczby od 1 do tej liczby, modyfikując je zgodnie z następującymi zasadami: liczby podzielne przez 3 zastępowane są słowem "Fizz", liczby podzielne przez 5 słowem "Buzz", a liczby podzielne przez 3 i 5 słowem "FizzBuzz". Pozostałe liczby pozostają bez zmian. W metodzie `Main` tworzony jest obiekt klasy `FizzBuzz` z liczbą 20 i wywoływana jest metoda `PrintFizzBuzz()`, która wypisuje odpowiednie wyniki na konsolę.

# [Lab1 - KnapsackProblem](./Lab1-KnapsackProblem)

Program implementuje rozwiązanie problemu plecakowego w aplikacji C# z użyciem Windows Forms. Użytkownik wprowadza dane, takie jak liczba przedmiotów, pojemność plecaka i ziarno (seed), a system generuje losowe przedmioty. Aplikacja sprawdza poprawność danych, a po kliknięciu przycisku "Solve problem" wyświetla rozwiązanie oparte na algorytmie zachłannym. Algorytm sortuje przedmioty według stosunku wartości do wagi, a następnie dodaje je do plecaka. Zaimplementowano również testy, które weryfikują poprawność i wydajność algorytmu w różnych scenariuszach, takich jak liczba przedmiotów, pojemność plecaka i czas działania przy dużej liczbie przedmiotów.

# [Lab2 - Weather App](./Lab2-WeatherApp)

Aplikacja korzysta z API OpenWeather do pobierania danych pogodowych. Gdy brak danych w bazie, wysyła zapytanie do API i zapisuje otrzymane informacje w bazie. Struktura bazy danych opiera się na dwóch głównych tabelach: `Cities` (przechowującej dane o miastach) oraz `WeatherData` (z danymi pogodowymi). Tabele są powiązane relacją jeden-do-wielu, co oznacza, że jedno miasto może mieć wiele rekordów pogodowych. Aplikacja oferuje także funkcje do czyszczenia bazy danych oraz walidacji wprowadzanych współrzędnych geograficznych.

# [Lab3 - Multithreading](./Lab3-Multithreading)

Projekt składał się z dwóch głównych zadań związanych z programowaniem współbieżnym, mających na celu porównanie efektywności różnych technik równoległego przetwarzania. W pierwszej części badano wydajność mnożenia macierzy przy użyciu dwóch podejść do wielowątkowości: `Parallel.For`, reprezentującego rozwiązanie wysokopoziomowe, oraz `Thread`, które jest metodą niskopoziomową. W drugiej części projektu stworzono aplikację okienkową, która umożliwia równoczesne stosowanie kilku filtrów na jednym obrazie. Filtry były przetwarzane w osobnych wątkach, co pozwoliło na efektywne wykorzystanie dostępnych rdzeni procesora i znacząco skróciło czas przetwarzania obrazów. 

# [Lab4 - ASP .Net Core](./Lab4-ASP.NetCore)

Projekt zawiera dwa niezależne komponenty webowe stworzone w technologii Blazor Server (.NET 8): prostą aplikację pogodową oraz rozbudowaną aplikację do oceniania filmów z integracją Google Maps.

**🌦️ Aplikacja pogodowa** :  prezentuje prognozę pogody na 10 dni, generowaną losowo przy każdym uruchomieniu. Użytkownik może filtrować dane po nazwie zjawiska pogodowego oraz wyświetlać jedynie dni z temperaturą powyżej 15°C. Interfejs wykorzystuje mechanizmy `@oninput`, `@onclick` oraz renderowanie strumieniowe, dzięki czemu dane są wczytywane dynamicznie i bez opóźnień w ładowaniu interfejsu. Aplikacja działa offline, nie wymaga API ani dostępu do internetu, dzięki czemu jest całkowicie samodzielna i bezpieczna.

**🎬 Aplikacja do oceniania filmów** :  pozwala użytkownikom na dodawanie, edytowanie, przeglądanie i ocenianie filmów zapisanych w bazie danych. Widok listy filmów umożliwia sortowanie danych, natomiast szczegóły filmu zawierają formularz do dodawania ocen oraz osadzoną mapę Google wskazującą lokalizację związaną z filmem (domyślnie Wrocław). Wbudowany system uwierzytelniania ASP.NET Identity pozwala na ograniczenie dostępu wyłącznie dla zalogowanych użytkowników. **Uwaga:** mapa Google może nie działać bez aktywnego konta rozliczeniowego w Google Cloud – domyślny klucz API nie posiada włączonego rozliczania.
