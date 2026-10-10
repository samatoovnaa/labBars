// ============ API-модуль ============
// Единственное место, где страницы узнают о способе получения данных.
// Сейчас — mock. На ЛР12 переключим USE_MOCK=false и пойдёт настоящий fetch.

import { mockGetTickets, mockCreateTicket } from './mockTickets.js'

// Переключатель: true — используем mock, false — реальный сервер
const USE_MOCK = true

// Адрес API из .env (Vite читает только переменные с префиксом VITE_)
const baseUrl = import.meta.env.VITE_API_BASE_URL || 'http://localhost:5000'

/**
 * GET /api/incidents — список инцидентов
 * Возвращает { ok, status, data } или выбрасывает ошибку.
 */
export async function getTickets() {
  if (USE_MOCK) {
    // Имитация: mock отдаёт объект как Response
    const response = await mockGetTickets()
    return handleResponse(response)
  }

  // Реальный fetch (сработает, когда USE_MOCK = false)
  const response = await fetch(`${baseUrl}/api/incidents`, {
    method: 'GET',
    headers: { Accept: 'application/json' },
  })
  return handleResponse(response)
}

/**
 * POST /api/incidents — создание инцидента
 * @param {Object} body — данные из формы
 */
export async function createTicket(body) {
  if (USE_MOCK) {
    const response = await mockCreateTicket(body)
    return handleResponse(response)
  }

  const response = await fetch(`${baseUrl}/api/incidents`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
      Accept: 'application/json',
    },
    body: JSON.stringify(body),
  })
  return handleResponse(response)
}

// ============ Общая обработка ответа ============
async function handleResponse(response) {
  const data = await response.json().catch(() => ({}))

  if (!response.ok) {
    // HTTP-ошибка: 400, 500 и т.д.
    // Создаём Error с прикреплённым статусом, чтобы UI мог различить 400 и 500
    const error = new Error(data.error || `HTTP ${response.status}`)
    error.status = response.status
    throw error
  }

  return data
}