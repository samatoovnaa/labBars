-- Добавляем категории
INSERT INTO categories (name) VALUES 
('Утечка данных'),
('Сбой оборудования');

-- Добавляем сотрудников
INSERT INTO employees (name) VALUES 
('Иванов Иван'),
('Петров Петр'),
('Сидоров Сидор');

-- Добавляем инциденты (все 4 статуса)
INSERT INTO incidents (number, title, description, status, category_id, created_by_user_id, assignee_user_id) VALUES
('INC-001', 'Утечка базы клиентов', 'Обнаружена утечка через API', 'New', 1, 1, NULL),
('INC-002', 'Упал сервер 1С', 'Сервер не отвечает после обновления', 'InProgress', 2, 1, 2),
('INC-003', 'Фишинговая рассылка', 'Сотрудникам пришли письма от имени директора', 'Closed', 1, 2, 1),
('INC-004', 'Забыли пароль от Wi-Fi', 'Нужно восстановить доступ', 'Cancelled', 2, 3, NULL);