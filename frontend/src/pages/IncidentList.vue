<script setup>
import { ref } from 'vue'
import IncidentTable from '../components/IncidentTable.vue'

// Временный массив с данными (пока без подключения к бэкенду)
const items = ref([
  {
    id: 1,
    number: 'INC-001',
    title: 'Обычный инцидент',
    description: 'Всё работает штатно, это обычный текст.',
    siteId: 1,
    category: 'Сбой оборудования',
    riskLevel: 'Низкий',
    status: 'New'
  },
  {
    id: 999,
    number: 'XSS-TEST',
    title: 'Проверка безопасности',
    // ВОТ НАШ ОПАСНЫЙ СКРИПТ (разбит на части, чтобы не ломать код):
    description: "<scr" + "ipt>alert('Взлом!')</scr" + "ipt>", 
    siteId: 1,
    category: 'Тест',
    riskLevel: 'Высокий',
    status: 'New'
  }
])
</script>

<template>
  <div>
    <h2>Список инцидентов</h2>
    <!-- Передаем наши данные в таблицу -->
    <IncidentTable :items="items" />
  </div>
</template>

<style scoped>
h2 {
  color: #333;
  margin-bottom: 1rem;
}
</style>