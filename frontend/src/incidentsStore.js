import { ref } from 'vue'

// ============ Единственный массив инцидентов на всё приложение ============
// И список, и форма импортируют его отсюда — никто не создаёт свой.
export const incidents = ref([
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
])

// ============ Счётчик для генерации id и number ============
// Ставим на 3, потому что выше уже 3 инцидента с id 1, 2, 3.
// При каждой новой записи он увеличится.
let nextId = 4

/**
 * Добавляет новый инцидент в общий массив.
 * @param {Object} data — поля, пришедшие из формы
 * @returns {Object} — только что созданный инцидент (с id, number, status)
 */
export function addIncident(data) {
  const incident = {
    id: nextId,
    number: `INC-2026-${String(nextId).padStart(3, '0')}`,
    title: data.title.trim(),
    siteId: data.siteId,
    category: data.category,
    riskLevel: data.riskLevel,
    status: 'New',                        // всегда New
    description: data.description.trim(),
  }

  incidents.value.push(incident)
  nextId += 1

  return incident
}