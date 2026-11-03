-- 1. Фильтр и сортировка (найти все новые инциденты, сначала свежие)
SELECT number, title, status, created_at 
FROM incidents 
WHERE status = 'New' 
ORDER BY created_at DESC;

-- 2. JOIN (вывести название категории вместо её ID)
SELECT incidents.number, incidents.title, categories.name AS category_name
FROM incidents
JOIN categories ON categories.id = incidents.category_id
WHERE incidents.status = 'InProgress';

-- 3. GROUP BY (посчитать, сколько инцидентов в каждом статусе)
SELECT status, COUNT(*) AS count
FROM incidents
GROUP BY status;

-- 4. Проверка CHECK (попробуем поставить несуществующий статус "Done")
-- ВНИМАНИЕ: Этот запрос ВЫЗОВЕТ ОШИБКУ. Это нормально! 
-- База должна сказать: "violates check constraint".
UPDATE incidents 
SET status = 'Done' 
WHERE id = 1;