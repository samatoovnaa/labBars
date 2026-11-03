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
