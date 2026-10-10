<script setup>
import { ref } from 'vue'

const login = ref({ username: '', password: '' })

function submitLogin() {
  console.log('Вход:', login.value)
  alert(`Попытка входа: ${login.value.username}`)
}
</script>

<template>
  <main>
    <h2>Вход в систему</h2>
    <p>Доступ только для сотрудников Департамента ИБ ПАО «СИБУР Холдинг».</p>

    <form @submit.prevent="submitLogin">
      <p>
        <label for="username">Логин (корпоративный)</label><br />
        <input
          id="username"
          v-model="login.username"
          name="username"
          type="text"
          required
          autofocus
        />
      </p>
      <p>
        <label for="password">Пароль</label><br />
        <input
          id="password"
          v-model="login.password"
          name="password"
          type="password"
          required
        />
      </p>
      <button type="submit">Войти</button>
    </form>
  </main>
</template>

<style scoped>
  form p {
    margin-bottom: 0.8rem;
  }
  label {
    font-weight: 600;
  }
  input {
    padding: 0.3rem;
    min-width: 260px;
  }
  button {
    padding: 0.5rem 1rem;
    background: #007a3d;
    color: white;
    border: none;
    cursor: pointer;
  }
</style>