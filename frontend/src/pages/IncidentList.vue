<script setup>
import { ref, onMounted } from 'vue'
import { getTickets } from '../api/tickets.js'
import IncidentTable from '../components/IncidentTable.vue'

// Одно поле состояния — четыре возможных значения
// 'loading' | 'empty' | 'data' | 'error'
const screen = ref('loading')

// Массив инцидентов (заполняется после ответа)
const items = ref([])

// Текст ошибки (если screen === 'error')
const errorMessage = ref('')

async function loadIncidents() {
  screen.value = 'loading'
  errorMessage.value = ''

  try {
    const data = await getTickets()
    items.value = data

    if (data.length === 0) {
      screen.value = 'empty'
    } else {
      screen.value = 'data'
    }
  } catch (err) {
    screen.value = 'error'
    // Разные тексты для разных типов ошибок
    if (err.status === 400) {
      errorMessage.value = 'Проверьте поля — сервер отклонил запрос.'
    } else if (err.status === 500) {
      errorMessage.value = 'Сервер не смог обработать запрос.'
    } else if (err.status) {
      errorMessage.value = `Ошибка ${err.status}: ${err.message}`
    } else {
      // Нет статуса — это сетевая ошибка (fetch упал)
      errorMessage.value = 'Нет связи. Проверьте, что приложение запущено.'
    }
  }
}

// Загружаем при открытии страницы
onMounted(loadIncidents)
</script>

<template>
  <main>
    <h2>Реестр инцидентов ИБ</h2>

    <!-- ===== Загрузка ===== -->
    <div v-if="screen === 'loading'">
      <p>Загрузка…</p>
    </div>

    <!-- ===== Пусто ===== -->
    <div v-else-if="screen === 'empty'">
      <p>Записей пока нет.</p>
    </div>

    <!-- ===== Данные ===== -->
    <div v-else-if="screen === 'data'">
      <IncidentTable :items="items" />
    </div>

    <!-- ===== Ошибка ===== -->
    <div v-else>
      <p class="error">{{ errorMessage }}</p>
      <button type="button" @click="loadIncidents">Повторить</button>
    </div>
  </main>
</template>

<style scoped>
  .error {
    color: #c00;
    padding: 1rem;
    background: #fee;
    border: 1px solid #c00;
  }
  button {
    padding: 0.4rem 1rem;
    cursor: pointer;
  }
</style>