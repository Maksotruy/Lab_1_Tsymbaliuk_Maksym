# FootballMatches

## Тема

Облік футбольних матчів.

## Основний тип предметної області

Основним типом предметної області є `FootballMatch`.

Властивості типу:

- `Id` — ідентифікатор матчу;
- `HomeTeam` — команда господарів;
- `AwayTeam` — команда гостей;
- `HomeScore` — кількість голів команди господарів;
- `AwayScore` — кількість голів команди гостей;
- `MatchDate` — дата проведення матчу;
- `Stadium` — стадіон, на якому проводиться матч;
- `Status` — статус матчу.

## Особливість проєкту

Особливістю проєкту буде можливість переглядати статистику результатів матчів за певний період. Користувач зможе аналізувати кількість зіграних матчів та забитих голів за вибраний період, що спростить перегляд історії футбольних матчів.

# FootballMatches

## Лабораторна робота №7 — Зв'язки між сутностями та довідники

### Опис проєкту

FootballMatches — вебзастосунок для обліку футбольних матчів, розроблений із використанням Blazor, C#, Entity Framework Core та SQLite.

У лабораторній роботі №7 реалізовано зв'язки між футбольними матчами, командами та стадіонами.

Додано Competition, тобто чемпіонат або ліга, де проводиться матч. 
- `Competition` — чемпіонат або ліга, де проводиться матч;

### Використані технології

- C# / .NET 10
- ASP.NET Core Blazor
- Entity Framework Core
- SQLite
- QuickGrid
- Visual Studio Code
- Git / GitHub

### Основні сутності

**FootballMatch**

Основна сутність, що містить інформацію про футбольний матч: команди, рахунок, дату, стадіон та статус.

**Team**

Довідник футбольних команд, який містить назву команди та країну.

**Stadium**

Довідник футбольних стадіонів, який містить назву стадіону та місто.

### Зв'язки між сутностями

- FootballMatch.HomeTeamId → Team.Id
- FootballMatch.AwayTeamId → Team.Id
- FootballMatch.StadiumId → Stadium.Id

Усі три зв'язки мають тип «один до багатьох».

### Mermaid-діаграма сутностей

```mermaid
erDiagram
    TEAM {
        int Id PK
        string Name
        string Country
    }

    STADIUM {
        int Id PK
        string Name
        string City
    }

    FOOTBALL_MATCH {
        int Id PK
        int HomeTeamId FK
        int AwayTeamId FK
        int StadiumId FK
        int HomeScore
        int AwayScore
        datetime MatchDate
        string Status
    }

    TEAM ||--o{ FOOTBALL_MATCH : "Home Team"
    TEAM ||--o{ FOOTBALL_MATCH : "Away Team"
    STADIUM ||--o{ FOOTBALL_MATCH : "Stadium"
```

### Реалізований функціонал

1. Створення та редагування футбольних команд.
2. Перегляд і видалення команд.
3. Створення та редагування стадіонів.
4. Перегляд і видалення стадіонів.
5. Вибір домашньої та гостьової команд у формі матчу.
6. Вибір стадіону у формі матчу.
7. Збереження зовнішніх ключів у базі даних.
8. Відображення назв пов'язаних сутностей.
9. Перевірка правильності вибору команд.
10. Захист пов'язаних записів від випадкового видалення.

### Робота з базою даних

Для зберігання даних використовується SQLite.

Зміни структури бази даних виконуються через міграції Entity Framework Core.

Команда застосування міграцій:

```bash
dotnet ef database update
```

### Результат

У лабораторній роботі №7 створено пов'язані довідники Team і Stadium, налаштовано зв'язки з FootballMatch та реалізовано роботу із зовнішніми ключами.

Завдяки цьому футбольні матчі можна створювати та редагувати, вибираючи команди й стадіони зі списків, без повторного введення їхніх назв.

# FootballMatches — система обліку футбольних матчів

## Опис проєкту

**FootballMatches** — вебзастосунок для обліку футбольних матчів, розроблений на платформі ASP.NET Core Blazor із використанням .NET 10, Entity Framework Core та бази даних SQLite.

Застосунок дозволяє переглядати інформацію про футбольні матчі, керувати командами та стадіонами, додавати й редагувати результати матчів, виконувати пошук, фільтрацію та сортування даних.

## Використані технології

- C# та .NET 10
- ASP.NET Core Blazor Web App
- Entity Framework Core
- SQLite
- LINQ
- QuickGrid
- HTML, CSS, Bootstrap
- Git та GitHub
- Visual Studio Code

## Основні можливості

- Перегляд списку футбольних матчів.
- Додавання нових матчів.
- Редагування та видалення матчів.
- Облік домашніх і гостьових команд.
- Вибір стадіону та футбольного турніру.
- Збереження результатів і статусів матчів.
- Перевірка введених даних.
- Захист від створення дублікатів матчів.
- Пошук матчів за назвою домашньої команди.
- Фільтрація матчів за командою.
- Сортування за датою, результатом і назвою команди.
- Посторінковий перегляд результатів.

## Лабораторна робота №8

**Тема:** Запити LINQ та асинхронний доступ до даних.

**Мета:** реалізувати пошук, фільтрацію та сортування даних із використанням LINQ і асинхронних методів Entity Framework Core.

### Реалізовані завдання

1. До моделі `FootballMatch` додано властивість `HomeTeamSearch` для нормалізованого пошуку.
2. Створено міграцію `AddHomeTeamSearch`.
3. Реалізовано оновлення пошукового поля під час створення та редагування матчів.
4. На сторінці `Index.razor` реалізовано LINQ-запити для пошуку, фільтрації та сортування.
5. Додано пагінацію зі збереженням параметрів пошуку.
6. Використано асинхронні методи `ToListAsync()`, `CountAsync()`, `FirstOrDefaultAsync()` і `SaveChangesAsync()`.
7. Налаштовано зв'язки між матчами, командами та стадіонами.

## Структура проєкту

```text
FootballMatches/
├── Components/
│   └── Pages/
│       └── FootballMatchesPages/
├── Data/
│   └── FootballMatchesContext.cs
├── Models/
│   ├── FootballMatch.cs
│   ├── Team.cs
│   └── Stadium.cs
├── Migrations/
├── Program.cs
├── appsettings.json
├── NOTES.md
└── README.md
```

## Запуск проєкту

### 1. Вимоги

Необхідно встановити .NET SDK 10 та Git.

### 2. Відкриття проєкту

Відкрийте папку FootballMatches у Visual Studio Code.

### 3. Відновлення залежностей

```powershell
dotnet restore
```

### 4. Оновлення бази даних

```powershell
dotnet ef database update
```

Для виконання команди потрібен інструмент `dotnet-ef` і налаштоване підключення до SQLite.

### 5. Запуск застосунку

```powershell
dotnet run
```

Після запуску відкрийте адресу, вказану в терміналі, та перейдіть на сторінку `/footballmatches`.

## Робота з базою даних

Для зберігання інформації використовується SQLite.

Основні сутності:

- `FootballMatch` — інформація про матч.
- `Team` — футбольна команда.
- `Stadium` — стадіон.

Доступ до бази даних реалізовано через Entity Framework Core з використанням асинхронних операцій.

## Висновок

У процесі розробки FootballMatches було створено вебзастосунок для обліку футбольних матчів. У межах лабораторної роботи №8 реалізовано пошук, фільтрацію, сортування та пагінацію з використанням LINQ. Асинхронні методи Entity Framework Core забезпечують неблокувальний доступ до бази даних.


---

## Match Statistics (Lab 9)

The FootballMatches application includes a statistics dashboard
available at `/summary`.

### Features

- Total number of football matches
- Total goals scored by home teams
- Number of home teams
- Number of months with recorded matches
- Match statistics grouped by team
- Match statistics grouped by month
- Responsive statistics dashboard
- Empty-state handling

### Implementation

The `SummaryCalculator` service uses LINQ and Entity Framework Core
to calculate aggregated results directly in SQLite.

The `SummaryRow` record stores the calculated values.

The statistics page uses asynchronous queries to retrieve
and display the results.


## Лабораторна робота №10

### Удосконалення інтерфейсу FootballMatches

У межах лабораторної роботи було вдосконалено
користувацький інтерфейс вебзастосунку FootballMatches.

Реалізовано:
- Навігаційне меню для переходу між розділами.
- Україномовні заголовки та підписи.
- Порожній стан для списків.
- Повідомлення про успішне збереження.
- Підтвердження видалення записів.
- Адаптивний інтерфейс для мобільних пристроїв.
- Відображення статистики футбольних матчів.

### Знімки екрана

#### 1. Список футбольних матчів

![Список футбольних матчів](docs/matches-desktop.png)

#### 2. Мобільна версія (360 px)

![Мобільна версія списку команд](docs/teams-mobile.png)

#### 3. Статистика футбольних матчів

![Статистика матчів](docs/statistics.png)



## Лабораторна робота №11 — Web API та OpenAPI

У межах лабораторної роботи розроблено мінімальний
вебсервіс для застосунку FootballMatches на ASP.NET Core 10.

Вебсервіс забезпечує отримання інформації про футбольні
матчі та статистику у форматі JSON.

### Кінцеві точки API

| Метод | Адреса | Опис |
|---|---|---|
| GET | `/api/footballmatches` | Отримання списку футбольних матчів |
| GET | `/api/footballmatches/{id}` | Отримання одного матчу за ідентифікатором |
| GET | `/api/summary` | Отримання статистики матчів за командами |

Якщо матч із зазначеним ідентифікатором відсутній,
вебсервіс повертає HTTP 404 Not Found.

### Документація OpenAPI

Опис API доступний у середовищі розробки:

- OpenAPI JSON: `http://localhost:5291/openapi/v1.json`
- Scalar: `http://localhost:5291/scalar`

### Перевірка вебсервісу

Для тестування використано розширення REST Client
у Visual Studio Code.

Приклади HTTP-запитів збережено у файлі
`FootballMatches/FootballMatches.http`.

Результати перевірки:

- Список матчів — 200 OK.
- Один матч за ID — 200 OK.
- Неіснуючий матч — 404 Not Found.
- Статистика матчів — 200 OK.
- Опис OpenAPI — 200 OK.
