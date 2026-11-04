# labBars
Хасаншина Лейсан Саматовна 
ТРИС-3-23
«Проектирование цифровой экосистемы предприятия ПАО „Сибур Холдинг“ и разработка программного обеспечения „Управление рисками и инцидентами информационной безопасности“»

## Документация

- [Архитектура](docs/architecture.md)
- [Модель данных](docs/data-model.md)
- [API-контракт](docs/api-contract.md)
- [Матрица требований](docs/requirements-matrix.md)
- [ТЗ](docs/project-proposal.md)
- [Требования ЛР1](docs/lab-01-requirements.md)

## Запуск бэкенда (ЛР7)

```bash
cd backend
dotnet restore
dotnet run

API: http://localhost:5000

Swagger: http://localhost:5000/swagger


## ЛР8 - REST API

- Базовый URL: http://localhost:5000
- Swagger: http://localhost:5000/swagger
- Данные хранятся в памяти, после перезапуска пропадают.
- Коллекция запросов для Postman лежит в `docs/api-collection/incidents.postman_collection.json`.

## ЛР9 - База данных PostgreSQL

Скрипты лежат в папке `db/`:
- `schema.sql` — создание таблиц, связей и индексов.
- `seed.sql` — заполнение тестовыми данными (4 статуса).
- `queries.sql` — 4 обязательных SQL-запроса (фильтр, JOIN, GROUP BY, проверка CHECK).

Пароль от PostgreSQL тот, что задавали при установке, в репозиторий не кладем. 
Для запуска: создайте БД `course_incidents` в pgAdmin и выполните скрипты по очереди через Query Tool.

cd ..
git add .
git commit -m "ЛР10: EF Core, миграции, подключение к PostgreSQL"
git push

## ЛР11 - Авторизация и безопасность
Добавлена JWT-авторизация. Пароли хешируются. 
Добавлена матрица ролей в `docs/role-matrix.md`.
Учетные данные для теста:
- Логин: dispatcher, Пароль: Password!
- Логин: engineer, Пароль: Password!

# Система управления рисками и инцидентами

## Стек
- Frontend: Vue 3 + Vite (порт 5173)
- Backend: ASP.NET Core (порт 5000)
- База данных: PostgreSQL (порт 5432)

## Как запустить (пошагово)

1. Установите PostgreSQL и создайте пустую базу данных `course_incidents`.
2. В папке `backend` настройте пароль к базе (замените `ВАШ_ПАРОЛЬ`):
   ```bash
   cd backend
   dotnet user-secrets init
   dotnet user-secrets set "ConnectionStrings:Default" "Host=localhost;Port=5432;Database=course_incidents;Username=postgres;Password=ВАШ_ПАРОЛЬ"