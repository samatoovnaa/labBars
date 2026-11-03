-- Удаляем старые таблицы, если они были (чтобы можно было запускать много раз)
DROP TABLE IF EXISTS incidents CASCADE;
DROP TABLE IF EXISTS categories CASCADE;
DROP TABLE IF EXISTS employees CASCADE;

-- 1. Справочник: Категории
CREATE TABLE categories (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL
);

-- 2. Справочник: Сотрудники
CREATE TABLE employees (
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL
);

-- 3. Главный объект: Инциденты
CREATE TABLE incidents (
    id SERIAL PRIMARY KEY,
    number TEXT NOT NULL UNIQUE,
    title TEXT NOT NULL,
    description TEXT NOT NULL,
    status TEXT NOT NULL CHECK (status IN ('New', 'InProgress', 'Closed', 'Cancelled')),
    category_id INTEGER NOT NULL REFERENCES categories(id),
    created_by_user_id INTEGER NOT NULL REFERENCES employees(id),
    assignee_user_id INTEGER REFERENCES employees(id), -- Может быть NULL
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

-- Индексы (как требует методичка)
-- Ускоряет фильтрацию списка по статусу
CREATE INDEX idx_incidents_status ON incidents(status);
-- Ускоряет соединение (JOIN) с таблицей категорий
CREATE INDEX idx_incidents_category_id ON incidents(category_id);