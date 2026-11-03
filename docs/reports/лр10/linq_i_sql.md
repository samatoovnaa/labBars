## Пара LINQ / SQL

Я использовала Entity Framework, который переводит мой C#-код в SQL-запросы для PostgreSQL.

Пример из моего кода (метод GetById в сервисе):
- **C# (LINQ):** `_context.Incidents.FirstOrDefault(i => i.Id == id)`
- **SQL (тот же смысл, который уходит в базу):** `SELECT * FROM incidents WHERE id = id LIMIT 1;`