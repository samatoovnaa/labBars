<script setup>
import { ref, onMounted } from 'vue'
import IncidentTable from '../components/IncidentTable.vue'

const items = ref([])

const loadIncidents = async () => {
  try {
    const loginRes = await fetch('http://localhost:5000/api/auth/login', {
      method: 'POST', headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ username: 'dispatcher', password: 'Password!' })
    })
    const loginData = await loginRes.json()
    
    const res = await fetch('http://localhost:5000/api/incidents', {
      headers: { 'Authorization': `Bearer ${loginData.token}` }
    })
    items.value = await res.json()
  } catch (err) {
    console.error(err)
  }
}

onMounted(() => loadIncidents())
</script>

<template>
  <div>
    <h2>Список инцидентов</h2>
    <router-link to="/new">➕ Создать новый инцидент</router-link>
    <br><br>
    <IncidentTable :items="items" />
  </div>
</template>