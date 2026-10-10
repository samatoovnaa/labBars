// ============ MOCK: имитация сервера на C# ============
// Тут данных ровно столько, чтобы UI показал все четыре состояния.
// На ЛР12 этот файл выключим и подставим настоящий fetch.

// Учебный массив инцидентов (тот же, что раньше лежал в incidentsStore)
let incidents = [
  {
    id: 1,
    number: 'INC-2026-001',
    siteId: 101,
    title: 'Подозрительный вход в корпоративную почту',
    category: 'Фишинг',
    riskLevel: 'Высокий',
    status: 'New',
    description: 'Несколько неудачных попыток входа с IP 10.24.8.15',
  },
  {
    id: 2,
    number: 'INC-2026-002',
    siteId: 205,
    title: 'Попытка выгрузки данных через USB-носитель',
    category: 'Утечка данных',
    riskLevel: 'Критический',
    status: 'InProgress',
    description: 'Обнаружено подключение незарегистрированного носителя',
  },
  {
    id: 3,
    number: 'INC-2026-003',
    siteId: 101,
    title: 'Несанкционированный доступ к серверу БД',
    category: 'Несанкционированный доступ',
    riskLevel: 'Средний',
    status: 'Closed',
    description: 'Закрыт после смены пароля и блокировки учётной записи',
  },
]

let nextId = 4

// ============ Секретные флаги для отладки ============
// force500 — если true, следующий запрос вернёт 500 (для проверки ветки !response.ok)
export const mockFlags = {
  force500: false,
}

// ============ Внутренние функции ============

// Пауза, чтобы UI успел показать «Загрузка…»
function delay(ms) {
  return new Promise((resolve) => setTimeout(resolve, ms))
}

// Обёртка: имитирует объект Response
function jsonResponse(status, data) {
  return {
    ok: status >= 200 && status < 300,
    status,
    json: async () => data,
  }
}

// ============ Публичные функции (как настоящий сервер) ============

/**
 * GET /api/incidents — список инцидентов
 * @returns {Promise<{ ok: boolean, status: number, json: Function }>}
 */
export async function mockGetTickets() {
  await delay(500) // «полсекунды на ответ сервера»

  // Секретный флаг для проверки ошибки 500
  if (mockFlags.force500) {
    return jsonResponse(500, { error: 'Внутренняя ошибка сервера' })
  }

  return jsonResponse(200, incidents)
}

/**
 * POST /api/incidents — создание инцидента
 * @param {Object} body — данные из формы
 * @returns {Promise<{ ok: boolean, status: number, json: Function }>}
 */
export async function mockCreateTicket(body) {
  await delay(500)

  // Секретный флаг для проверки ошибки 500
  if (mockFlags.force500) {
    return jsonResponse(500, { error: 'Внутренняя ошибка сервера' })
  }

  // Валидация на «сервере»: тема короче 5 символов — 400
  const title = (body.title || '').trim()
  if (title.length < 5) {
    return jsonResponse(400, { error: 'Тема должна быть не короче 5 символов' })
  }

  // Создание — 201
  const newIncident = {
    id: nextId,
    number: `INC-2026-${String(nextId).padStart(3, '0')}`,
    title,
    siteId: Number(body.siteId),
    category: body.category,
    riskLevel: body.riskLevel,
    status: 'New',
    description: (body.description || '').trim(),
  }

  incidents.push(newIncident)
  nextId += 1

  return jsonResponse(201, newIncident)
}