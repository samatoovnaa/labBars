<script setup>
const appTitle = 'СИБУР · Управление рисками и инцидентами ИБ'
</script>

<template>
  <header>
    <h1>{{ appTitle }}</h1>
    <nav>
      <router-link to="/">Список инцидентов</router-link>
      <router-link to="/new">Создать инцидент</router-link>
      <router-link to="/login">Вход</router-link>
    </nav>
  </header>
</template>

<style scoped>
  header {
    margin-bottom: 1rem;
    padding-bottom: 0.5rem;
    border-bottom: 2px solid #007a3d;
  }
  h1 {
    font-size: 1.2rem;
    color: #007a3d;
    margin: 0 0 0.5rem 0;
  }
  nav {
    display: flex;
    gap: 0.5rem;
  }
  nav a {
    padding: 0.4rem 1rem;
    border: 1px solid #007a3d;
    color: #007a3d;
    text-decoration: none;
    border-radius: 3px;
  }
  nav a:hover {
    background: #e6f5ec;
  }
  /* router-link-active — класс, который Vue добавляет активной ссылке */
  nav a.router-link-active {
    background: #007a3d;
    color: white;
  }
</style>