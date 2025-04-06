Celem tego zadania było zapoznanie się z podstawami projektowania aplikacji bazodanowych oraz integracji z interfejsami API. W ramach zajęć stworzyliśmy aplikację w języku C#, która pobierała dane z zewnętrznego API, przetwarzała je, a następnie przechowywała w lokalnej bazie danych.

## 1. Struktura bazy danych

Baza danych aplikacji składa się z dwóch głównych tabel: `Cities` i `WeatherData`, które są powiązane w relacji jeden-do-wielu. Tabela `Cities` przechowuje podstawowe informacje o miastach, takie jak nazwa, współrzędne geograficzne oraz strefa czasowa, natomiast tabela `WeatherData` zawiera szczegółowe dane pogodowe związane z danym miastem, takie jak temperatura, wilgotność, ciśnienie, opis pogody, prędkość wiatru oraz czas wschodu i zachodu słońca.

### Model danych:

```csharp
public class AppDbContext : DbContext
{
    public DbSet<City> Cities { get; set; }
    public DbSet<WeatherData> WeatherRecords { get; set; }
    private static string dbPath;
    public AppDbContext()
    {
        if (string.IsNullOrEmpty(dbPath))
        {
            string folder = FileSystem.AppDataDirectory;
            dbPath = Path.Combine(folder, "weather.db");
        }
        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite($"Filename={dbPath}");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>()
            .HasMany(c => c.WeatherData)
            .WithOne(w => w.City)
            .HasForeignKey(w => w.CityId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<City>()
            .HasIndex(c => c.Name)
            .IsUnique();
    }
}
```

**Tabela `City`**

Tabela `City` zawiera informacje o miastach, takie jak:
- **Id**: Unikalny identyfikator miasta (klucz podstawowy).
- **Name**: Nazwa miasta, która jest wymagana do prawidłowego działania aplikacji.
- **Lat**: Szerokość geograficzna miasta, wymagane pole.
- **Lon**: Długość geograficzna miasta, wymagane pole.
- **Timezone**: Strefa czasowa miasta (opcjonalna, może przyjąć wartość null).
- **WeatherData**: Kolekcja rekordów pogodowych związanych z miastem. Jest to relacja jeden-do-wielu z tabelą `WeatherData`, ponieważ jedno miasto może mieć wiele wpisów pogodowych (np. różne pomiary w różnych chwilach czasu).


```csharp
public class City
{
    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }
    public required double Lat { get; set; }
    public required double Lon { get; set; }
    public int? Timezone { get; set; }
    public ICollection<WeatherData> WeatherData { get; set; }
}
```

**Tabela `WeatherData`**

Tabela `WeatherData` zawiera dane pogodowe, takie jak:
- **Id**: Unikalny identyfikator rekordu pogodowego (klucz podstawowy).
- **CityId**: Identyfikator miasta, do którego rekord pogodowy należy (klucz obcy).
- **City**: Obiekt reprezentujący miasto, do którego należy rekord pogodowy. To powiązanie jest realizowane przez właściwość nawigacyjną `City`.
- **Temperature**: Temperatura w mieście w momencie pomiaru.
- **Humidity**: Wilgotność powietrza w mieście w momencie pomiaru.
- **Pressure**: Ciśnienie atmosferyczne w mieście w momencie pomiaru.
- **WeatherDescription**: Opis warunków pogodowych (np. "słonecznie", "deszczowo").
- **WindSpeed**: Prędkość wiatru w mieście w momencie pomiaru.
- **Sunrise**: Czas wschodu słońca w formacie Unix timestamp.
- **Sunset**: Czas zachodu słońca w formacie Unix timestamp.
- **Timestamp**: Data i godzina, w której dane pogodowe zostały zapisane.

```csharp
public class WeatherData
{
    [Key]
    public int Id { get; set; }
    public int CityId { get; set; }
    public City City { get; set; }
    public required double Temperature { get; set; }
    public required int Humidity { get; set; }
    public required int Pressure { get; set; }
    public required string WeatherDescription { get; set; }
    public required double WindSpeed { get; set; }
    public required long Sunrise { get; set; }
    public required long Sunset { get; set; }
    public required DateTime Timestamp { get; set; }
}
```
**Zależności między tabelami**

Relacja między tabelami `Cities` i `WeatherData` jest zdefiniowana jako **relacja jeden-do-wielu**. Oznacza to, że każde miasto może mieć wiele powiązanych danych pogodowych, ale każdy rekord pogodowy jest przypisany do jednego miasta.

1. **Klucz obcy `CityId` w tabeli `WeatherData`**: Każdy rekord w tabeli `WeatherData` zawiera identyfikator `CityId`, który wskazuje na rekord w tabeli `Cities`. Dzięki temu możliwe jest przypisanie wielu danych pogodowych do konkretnego miasta.
2. **Usuwanie kaskadowe**: Jeśli rekord miasta w tabeli `Cities` zostanie usunięty, wszystkie powiązane rekordy pogodowe w tabeli `WeatherData` zostaną automatycznie usunięte.
3. **Indeks unikalności na kolumnie `Name` w tabeli `Cities`**: Zapewniono, że każda nazwa miasta w tabeli `Cities` będzie unikalna. Dzięki temu nie będzie możliwości dodania dwóch miast o tej samej nazwie, co gwarantuje spójność danych w aplikacji.


## 2. Integracja z API

Aplikacja korzysta z interfejsu API OpenWeather do pobierania danych pogodowych na podstawie współrzędnych geograficznych lub nazwy miasta. Jeśli dane pogodowe nie są dostępne w bazie danych, aplikacja wysyła zapytanie do API i zapisuje odpowiedź w lokalnej bazie danych. 

Działanie aplikacji:

a) Sprawdzenie, czy wpisane przez użytkownika miasto jest w bazie danych i wyświetlenie dla niego pogody

Aplikacja najpierw sprawdza, czy miasto, które zostało wpisane przez użytkownika w formularzu, znajduje się w bazie danych. Jeśli tak, to pobiera ostatnie dostępne dane pogodowe dla tego miasta, a następnie wyświetla je użytkownikowi. Dzięki temu użytkownicy mają szybki dostęp do danych dla wcześniej dodanych miast.

b) Wyświetlenie pogody dla wybranego miasta z listy 

Jeśli użytkownik wybierze miasto z rozwijanej listy, aplikacja pobiera dane pogodowe przypisane do tego miasta i wyświetla je w interfejsie. Miasta są przechowywane w bazie danych, co pozwala na łatwe zarządzanie i dostęp do informacji o pogodzie w wielu miejscach.

c) Zapytanie do API z podanymi przez użytkownika współrzędnymi aby wyświetlić pogodę

Jeśli użytkownik zdecyduje się podać współrzędne geograficzne (szerokość i długość geograficzną) zamiast nazwy miasta, aplikacja wysyła zapytanie do API OpenWeather. Na podstawie tych współrzędnych, API zwraca aktualne dane pogodowe, które następnie są wyświetlane w aplikacji.

d) Utworzenie nowych rekordów w bazie danych

Jeżeli miasto lub jego dane pogodowe nie istnieją jeszcze w bazie danych, aplikacja automatycznie dodaje nowe rekordy do tabeli Cities (dla miast) oraz WeatherData (dla danych pogodowych). Po zapisaniu danych w bazie, użytkownik otrzymuje pełną informację o pogodzie w wybranym miejscu. Dzięki tej funkcji aplikacja może dynamicznie rozszerzać bazę danych o nowe lokalizacje, które wcześniej nie były uwzględnione.


## 3. Dodatkowe funkcje

W aplikacji zaimplementowano funkcję czyszczenia bazy danych, która umożliwia usunięcie wszystkich zapisanych miast oraz rekordów pogodowych. Funkcjonalność ta jest dostępna poprzez przycisk "Wyczyść bazę danych". Po jego naciśnięciu, aplikacja usuwa wszystkie rekordy z tabel "Cities" oraz "WeatherRecords" w lokalnej bazie danych, a następnie wyświetla komunikat informujący o pomyślnym wyczyszczeniu bazy.

W celu zapewnienia poprawności danych wejściowych, aplikacja posiada mechanizm walidacji wprowadzanych współrzędnych geograficznych. Gdy użytkownik wpisuje współrzędne w polach tekstowych dla szerokości (latitude) i długości geograficznej (longitude), aplikacja sprawdza, czy wpisane wartości są liczbami zmiennoprzecinkowymi. Jeśli dane wejściowe są niepoprawne, tekst w polu zmienia kolor na czerwony, co daje użytkownikowi informację o błędzie.


## 4. Wygląd aplikacji
![ScreenMAUI](https://github.com/user-attachments/assets/077fba1b-ae22-4693-ab81-5e6f4a0fd09c)
