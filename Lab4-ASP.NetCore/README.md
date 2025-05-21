# 🌦️ BlazorApp - aplikacja pogodowa

Aplikacja pogodowa to prosty interaktywny komponent stworzony w technologii **Blazor Server**, który umożliwia użytkownikowi wyświetlanie, filtrowanie i analizę prognozy pogody na najbliższe 10 dni.

Dane pogodowe są **generowane losowo** przy każdym uruchomieniu komponentu (symulacja działania prawdziwego API). Dzięki temu użytkownik może testować różne scenariusze filtrowania bez połączenia z zewnętrznym serwisem.

---

### 🔧 Główne funkcjonalności

* **Wyświetlanie prognozy**: Użytkownik widzi tabelę z datą, temperaturą w °C i °F oraz krótkim podsumowaniem (np. "Hot", "Cool").
* **Filtrowanie po nazwie zjawiska pogodowego**: Dzięki polu tekstowemu możliwe jest filtrowanie wierszy po zawartości kolumny `Summary`.
* **Filtrowanie dni cieplejszych niż 15°C**: Przyciskiem `Filtruj dni > 15°C` można wyświetlić jedynie dni z wyższą temperaturą.
* **Przywracanie danych**: Przycisk `Przywróć` pozwala zresetować filtr i wrócić do pełnej listy prognoz.
* **Licznik ciepłych dni**: Na dole komponentu widnieje licznik pokazujący ile z 10 wygenerowanych dni przekracza temperaturę 15°C.

---

### 🧪 Technologie i techniki

* **Blazor Server** z użyciem `@rendermode InteractiveServerRenderMode`
* Interaktywne zdarzenia (`@onclick`, `@oninput`)
* Prosty komponent `WeatherForecast` wbudowany bezpośrednio w `.razor`
* Symulacja danych losowych (brak zewnętrznego API)
* Streaming rendering (`[StreamRendering]`) – komponent wyświetla się natychmiast, a dane ładują się asynchronicznie po chwili

---

### 🔒 Brak zewnętrznego API

Dzięki czemu:

* Jest **bezpieczna** – nie wymaga przechowywania ani ochrony żadnych danych wrażliwych,
* Jest **samodzielna** – działa offline, bez internetu,

---

### 📸 Zrzut ekranu

![image](https://github.com/user-attachments/assets/7a877d53-5f01-4c9d-9ea0-c7c477fece2c)


# 🎬BlazorApp2 – aplikacja do oceniania filmów

Aplikacja webowa stworzona w technologii **Blazor Server (.NET 8)**, umożliwiająca przeglądanie, dodawanie, edytowanie oraz ocenianie filmów. Projekt został wykonany w ramach zajęć dydaktycznych i zawiera wszystkie wymagane funkcjonalności wraz z dodatkowym rozszerzeniem – integracją z Google Maps.

---

## 📌 Opis działania aplikacji

### 🏠 Strona główna /movies

- Wyświetla listę wszystkich filmów zapisanych w bazie danych.
- Użytkownik może sortować listę klikając przyciski nagłówków kolumn:
  - **Title** – sortowanie po tytule filmu (rosnąco/malejąco),
  - **Release Date** – sortowanie po dacie premiery,
  - **Rating** – sortowanie po średniej ocenie filmu.
- Kolumna z opisem filmu jest ukryta – widoczna tylko w szczegółach filmu.
- Tylko zalogowany użytkownik ma dostęp do tej strony – wykorzystano atrybut `[Authorize]`.

![image](https://github.com/user-attachments/assets/9a320bd5-3b56-439f-8608-66919a15dfca)

### ➕ Dodawanie filmów

- Po kliknięciu przycisku **Create New** można dodać nowy film.
- W formularzu podaje się m.in. tytuł, datę premiery, opis, ocenę oraz **link do obrazka** (np. URL z IMDb lub z Google Drive).
- Po zapisaniu, film trafia do bazy danych i jest widoczny w widoku listy.

![image](https://github.com/user-attachments/assets/2394dc5f-582a-49b1-bf27-3f84aed489d4)

### 📝 Edycja i usuwanie

- W widoku listy każdy film ma opcje: **Edit**, **Details**, **Delete**.
- Edycja pozwala zmienić wszystkie dane filmu.
- Usunięcie filmu jest potwierdzane i trwałe (z bazy danych).

![image](https://github.com/user-attachments/assets/6ff0aeca-0bda-43ce-a5e2-815bdf711b98) ![image](https://github.com/user-attachments/assets/d4c19bd4-47f1-475f-8d9f-14fb88f7878c)

### ⭐ Ocena filmu (widok Details)

- W szczegółach filmu znajduje się formularz do **dodania nowej oceny**.
- Nowa ocena **nie zastępuje poprzedniej**, lecz jest **uśredniana** ze wszystkimi dotychczasowymi ocenami.
- W szczegółach widoczny jest również:
  - Obrazek filmu (na podstawie wprowadzonego URL-a),
  - Wbudowana mapa Google.

![image](https://github.com/user-attachments/assets/044ca771-2c8c-49d5-99b0-642925f8459e)


### 🗺️ Mapa Google (widok Details)

- W dolnej części widoku `Details` osadzona jest mapa Google.
- Mapa pokazuje lokalizację związaną z filmem (domyślnie: **Wrocław**).
- Technicznie wykorzystano **Google Maps Embed API** z iframe.

![image](https://github.com/user-attachments/assets/93371705-320a-4331-82d8-c6dd9ede71bb)


**Uwaga**:  
> Mapa nie będzie działać domyślnie, ponieważ darmowy dostęp do Google Maps Embed API wymaga rejestracji i podpięcia aktywnego konta rozliczeniowego.  
> W obecnej wersji używany klucz API nie ma aktywnego rozliczenia, dlatego mapa może wyświetlać błąd:  
> _"Google Maps Platform rejected your request. This API project is not authorized to use this API."_  
> Aby uruchomić mapę, należy:
> - Włączyć Maps Embed API w Google Cloud Console,
> - Podać własny **klucz API** z włączonym rozliczaniem.

---

## 🔐 Uwierzytelnianie

- Aplikacja wykorzystuje wbudowane uwierzytelnianie ASP.NET Core Identity.
- Tylko zalogowani użytkownicy mogą:
  - Przeglądać listę filmów,
  - Dodawać, edytować i usuwać filmy,
  - Dodawać nowe oceny.
- Nowi użytkownicy mogą zarejestrować się samodzielnie przez formularz rejestracji.

![image](https://github.com/user-attachments/assets/fd6eb429-2565-490b-8bb8-6253b24c4e59)

