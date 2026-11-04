<script setup>
import { ref, onMounted } from 'vue'
import IncidentTable from '../components/IncidentTable.vue'

const items = ref([])
const errorMsg = ref('')

const loadIncidents = async () => {
  try {
    // 1. Автоматически логинимся под диспетчером, чтобы получить токен
    const loginRes = await fetch('http://localhost:5000/api/auth/login', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ username: 'dispatcher', password: 'Password!' })
    })
    
    if (!loginRes.ok) throw new Error('Ошибка входа')
    const loginData = await loginRes.json()
    
    // 2. Делаем запрос к базе данных, передавая токен
    const res = await fetch('http://localhost:5000/api/incidents', {
      headers: { 
        'Authorization': `Bearer ${loginData.token}` 
      }
    })
    
    if (!res.ok) throw new Error('Ошибка загрузки данных')
    items.value = await res.json()
    
  } catch (err) {
    errorMsg.value = err.message
  }
}

onMounted(() => {
  loadIncidents()
})
</script>

<template>
  <div>
    <h2>Список инцидентов (из базы данных)</h2>
    
    <p v-if="errorMsg" style="color: red; font-weight: bold;">
      {{ errorMsg }} (Убедитесь, что бэкенд запущен)
    </p>
    
    <IncidentTable :items="items" />
  </div>
</template>