# API-контракт

Базовый путь: /api/incidents

| Метод и путь                          | Тело                                              | Успех          | Ошибки      |
|---------------------------------------|---------------------------------------------------|----------------|-------------|
| GET /api/incidents                    | —                                                 | 200, список    | —           |
| GET /api/incidents/{id}               | —                                                 | 200, один      | 404         |
| POST /api/incidents                   | title, categoryId, description, probability        | 201, создано   | 400         |
| PATCH /api/incidents/{id}/assignee    | assigneeUserId                                    | 200            | 400, 404    |
| PATCH /api/incidents/{id}/status      | status                                            | 200            | 400, 404    |

## Правила

- Статусы только: New, InProgress, Closed, Cancelled.
- Отмена — статус Cancelled, а не удаление строки. DELETE не используем.
- Путь /create не используем. Действие несёт метод POST.
- Пустой список — 200 и [], не 404. 404 — только когда нет id.
- id и status клиент не присылает. Их ставит сервер.
- Назначение ответственного и смена статуса — два разных PATCH.

## Пример тела создания

POST /api/incidents

{
  "title": "Утечка данных клиентов",
  "categoryId": 2,
  "description": "Обнаружена утечка через API",
  "probability": "High"
}

Ответ: 201

## Пример тела назначения

PATCH /api/incidents/17/assignee

{
  "assigneeUserId": 5
}

Ответ: 200

## Пример тела смены статуса

PATCH /api/incidents/17/status

{
  "status": "InProgress"
}

Ответ: 200
