<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()
const title = ref('')
const description = ref('')
const categoryId = ref(1)

const createIncident = async () => {
  try {
    // 1. Получаем токен диспетчера
    const loginRes = await fetch('http://localhost:5000/api/auth/login', {
      method: 'POST', 
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ username: 'dispatcher', password: 'Password!' })
    })
    const loginData = await loginRes.json()

    // 2. Отправляем данные на сервер
    const res = await fetch('http://localhost:5000/api/incidents', {
      method: 'POST',
      headers: { 
        'Content-Type': 'application/json', 
        'Authorization': `Bearer ${loginData.token}` 
      },
      body: JSON.stringify({ 
        title: title.value, 
        description: description.value, 
        categoryId: Number(categoryId.value) 
      })
    })
    
    if (res.ok) {
      alert('Инцидент успешно создан!')
      router.push('/') // Возвращаемся к списку
    } else {
      alert('Ошибка при создании')
    }
  } catch (err) {
    alert('Сервер не отвечает')
  }
}
</script>

<template>
  <div>
    <h2>Создать инцидент</h2>
    <form @submit.prevent="createIncident">
      <div>
        <label>Тема:</label>
        <input v-model="title" required placeholder="Например: Сбой сервера" />
      </div>
      <div>
        <label>Описание:</label>
        <textarea v-model="description" required placeholder="Подробности..."></textarea>
      </div>
      <div>
        <label>ID категории (число):</label>
        <input type="number" v-model="categoryId" required />
      </div>
      <button type="submit">Создать</button>
    </form>
    <br>
    <router-link to="/">← Вернуться к списку</router-link>
  </div>
</template>