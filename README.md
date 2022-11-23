# Wprowadzenie do ASP.NET Core 6
## Podstawy

### Komendy CLI
#### Środowisko
- ``` dotnet --version ``` - wyświetlenie wersji SDK
- ``` dotnet --list-sdks ``` - wyświetlenie listy zainstalowanych SDK
- ``` dotnet new globaljson ``` - utworzenie pliku _global.json_
- ``` dotnet new globaljson --sdk-version {version} ``` - utworzenie pliku _global.json_ i ustawienie wersji SDK

#### Projekt
- ``` dotnet new --list ``` - wyświetlenie listy dostępnych szablonów
- ``` dotnet new {template} ``` - utworzenie nowego projektu na podstawie wybranego szablonu
- ``` dotnet new {template} -o {output} ``` - utworzenie nowego projektu w podanym katalogu
- ``` dotnet build ``` - kompilacja projektu
- ``` dotnet run ``` - uruchomienie projektu
- ``` dotnet watch run ``` - uruchomienie projektu w trybie śledzenia zmian
- ``` dotnet run {app.dll}``` - uruchomienie aplikacji
- ``` dotnet test ``` - uruchomienie testów jednostkowych
- ``` dotnet watch test ``` - uruchomienie testów jednostkowych w trybie śledzenia zmian
- ``` dotnet add {project.csproj} reference {library.csproj} ``` - dodanie odwołania do biblioteki
- ``` dotnet remove {project.csproj} reference {library.csproj} ``` - usunięcie odwołania do biblioteki
- ``` dotnet clean ``` - wyczyszczenie wyniku kompilacji, czyli zawartości folderu pośredniego _obj_ oraz folderu końcowego _bin_

#### Rozwiązanie
- ``` dotnet new sln ``` - utworzenie nowego rozwiązania
- ``` dotnet new sln --name {name} ``` - utworzenie nowego rozwiązania o określonej nazwie
- ``` dotnet sln add {folder}``` - dodanie projektu z folderu do rozwiązania
- ``` dotnet sln remove {folder}``` - usunięcie projektu z folderu z rozwiązania
- ``` dotnet sln add {project.csproj}``` - dodanie projektu do rozwiązania
- ``` dotnet sln remove {project.csproj}``` - usunięcie projektu z rozwiązania

### Pobieranie konfiguracji

#### Secret Keys 
- ``` dotnet user-secrets init ``` - utworzenie sekretów
- ``` dotnet user-secrets set "{key}" "{value}" ``` - ustawienie wartości klucza
- ``` dotnet user-secrets list ``` - wyświetlenie listy kluczy i wartości
- ``` dotnet user-secrets remove "{key}" ``` - usunięcie wskazanego klucza
- ``` dotnet user-secrets clear ``` - usunięcie wszystkich kluczy


### Środowiska (deweloperskie, przejściowe, produkcyjne)
### Rejestrowanie logów
## Wstrzykiwanie zależności
### Zasada odwróconej zależności
### Rejestrowanie usług w kontenerze
### Wstrzykiwanie poprzez konstruktor
## Budowa aplikacji ASP.NET Core
### WebApplicationBuilder (ASP.NET Core 6)
## Middleware
### Filozofia middleware
### Utworzenie własnej warstwy pośredniej
### Mapowanie żądań
### Punkty końcowe (Endpoints)
## Bezpieczeństwo
### Uwierzytelnianie i autoryzacja
### Role (Roles)
### Poświadczenia (Claims)
### Zasady (Policies)
