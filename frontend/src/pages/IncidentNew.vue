<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { createTicket } from '../api/tickets.js'

const router = useRouter()

const form = ref({
  title: '',
  siteId: null,
  category: '',
  riskLevel: 'Средний',
  description: '',
})

const errors = ref({
  title: '',
  siteId: '',
  category: '',
  description: '',
})

// Флаг: идёт ли запрос. Пока true — кнопка disabled.
const isSubmitting = ref(false)

// Общая ошибка сервера (после отправки)
const serverError = ref('')

// ============ Клиентская валидация ============
function validateForm() {
  errors.value = { title: '', siteId: '', category: '', description: '' }

  const title = form.value.title.trim()
  const siteId = form.value.siteId
  const description = form.value.description.trim()

  let ok = true

  if (title.length < 5 || title.length > 80) {
    errors.value.title = 'Тема: от 5 до 80 символов'
    ok = false
  }
  if (siteId === null || siteId === '' || !Number.isInteger(Number(siteId)) || Number(siteId) <= 0) {
    errors.value.siteId = 'siteId: целое число больше нуля'
    ok = false
  }
  if (!form.value.category) {
    errors.value.category = 'Выберите категорию угрозы'
    ok = false
  }
  if (description.length < 10 || description.length > 500) {
    errors.value.description = 'Описание: от 10 до 500 символов'
    ok = false
  }

  return ok
}

// ============ Отправка ============
async function onCreate() {
  serverError.value = ''

  if (!validateForm()) {
    return
  }

  isSubmitting.value = true

  try {
    // Вызываем API-модуль — он сам решит, mock или fetch
    const created = await createTicket({
      title: form.value.title,
      siteId: Number(form.value.siteId),
      category: form.value.category,
      riskLevel: form.value.riskLevel,
      description: form.value.description,
    })

    // id и number пришли из ответа — не выдумываем сами!
    console.log('Создан инцидент:', created)

    // Переход на список — там он уже будет
    router.push('/')
  } catch (err) {
    // ====== Разные тексты для разных ошибок ======
    if (err.status === 400) {
      serverError.value = 'Проверьте поля — сервер отклонил запрос.'
    } else if (err.status === 500) {
      serverError.value = 'Сервер не смог обработать запрос.'
    } else if (err.status) {
      serverError.value = `Ошибка ${err.status}: ${err.message}`
    } else {
      serverError.value = 'Нет связи. Проверьте, что приложение запущено.'
    }
  } finally {
    // Снимаем блокировку ВСЕГДА — и при успехе, и при ошибке
    isSubmitting.value = false
  }
}
</script>

<template>
  <main>
    <h2>Регистрация инцидента ИБ</h2>

    <p v-if="serverError" class="server-error">{{ serverError }}</p>

    <form @submit.prevent="onCreate">
      <p>
        <label for="title">Краткое описание (5–80 символов)</label><br />
        <input id="title" v-model="form.title" name="title" type="text" autofocus />
        <br />
        <span v-if="errors.title" class="error">{{ errors.title }}</span>
      </p>

      <p>
        <label for="siteId">Подразделение / филиал (siteId)</label><br />
        <input id="siteId" v-model.number="form.siteId" name="siteId" type="number" />
        <br />
        <span v-if="errors.siteId" class="error">{{ errors.siteId }}</span>
      </p>

      <p>
        <label for="category">Категория угрозы</label><br />
        <select id="category" v-model="form.category" name="category">
          <option value="">— выберите —</option>
          <option value="Фишинг">Фишинг</option>
          <option value="Утечка данных">Утечка данных</option>
          <option value="Несанкционированный доступ">Несанкционированный доступ</option>
          <option value="Вредоносное ПО">Вредоносное ПО</option>
          <option value="Отказ в обслуживании">Отказ в обслуживании</option>
        </select>
        <br />
        <span v-if="errors.category" class="error">{{ errors.category }}</span>
      </p>

      <p>
        <label for="riskLevel">Уровень риска</label><br />
        <select id="riskLevel" v-model="form.riskLevel" name="riskLevel">
          <option value="Низкий">Низкий</option>
          <option value="Средний">Средний</option>
          <option value="Высокий">Высокий</option>
          <option value="Критический">Критический</option>
        </select>
      </p>

      <p>
        <label for="description">Подробное описание (10–500 символов)</label><br />
        <textarea id="description" v-model="form.description" name="description" rows="5" cols="50"></textarea>
        <br />
        <span v-if="errors.description" class="error">{{ errors.description }}</span>
      </p>

      <button type="submit" :disabled="isSubmitting">
        {{ isSubmitting ? 'Отправка…' : 'Зарегистрировать инцидент' }}
      </button>
    </form>
  </main>
</template>

<style scoped>
  form p { margin-bottom: 0.8rem; }
  label { font-weight: 600; }
  input, textarea, select { padding: 0.3rem; min-width: 260px; }
  button {
    padding: 0.5rem 1rem;
    background: #007a3d;
    color: white;
    border: none;
    cursor: pointer;
  }
  button:disabled {
    background: #999;
    cursor: not-allowed;
  }
  .error {
    color: #c00;
    font-size: 0.9rem;
  }
  .server-error {
    color: #c00;
    padding: 0.5rem;
    background: #fee;
    border: 1px solid #c00;
  }
</style>