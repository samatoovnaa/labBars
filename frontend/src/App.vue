<script setup>
import { ref } from 'vue'

const screen = ref('list')

const items = ref([
  { id: 1, number: 'A-101', siteId: 1, title: 'Насос не работает', status: 'New' },
  { id: 2, number: 'A-102', siteId: 2, title: 'Протечка трубы', status: 'InProgress' },
  { id: 3, number: 'A-103', siteId: 1, title: 'Заменить фильтр', status: 'Closed' },
])

const form = ref({
  title: '',
  siteId: null,
  description: '',
})

const login = ref({ username: '', password: '' })

function submitForm() {
  console.log('Создать заявку:', form.value)
}
</script>

<template>
  <div>
    <header>
      <button type="button" @click="screen = 'list'">Список</button>
      <button type="button" @click="screen = 'create'">Создать</button>
      <button type="button" @click="screen = 'login'">Вход</button>
    </header>

    <main v-if="screen === 'list'">
      <h1>Заявки</h1>
      <table border="1" cellpadding="6">
        <thead>
          <tr>
            <th>Номер</th>
            <th>Тема</th>
            <th>Участок</th>
            <th>Статус</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="item in items" :key="item.id">
            <td>{{ item.number }}</td>
            <td>{{ item.title }}</td>
            <td>{{ item.siteId }}</td>
            <td>{{ item.status }}</td>
          </tr>
        </tbody>
      </table>
    </main>

    <main v-else-if="screen === 'create'">
      <h1>Новая заявка</h1>
      <form @submit.prevent="submitForm">
        <p>
          <label for="title">Тема (5–80 символов)</label><br />
          <input id="title" v-model="form.title" name="title" type="text" autofocus />
        </p>
        <p>
          <label for="siteId">Участок (siteId)</label><br />
          <input id="siteId" v-model.number="form.siteId" name="siteId" type="number" />
        </p>
        <p>
          <label for="description">Описание</label><br />
          <textarea id="description" v-model="form.description" name="description" rows="4"></textarea>
        </p>
        <button type="submit">Создать</button>
      </form>
    </main>

    <main v-else>
      <h1>Вход</h1>
      <form @submit.prevent>
        <p>
          <label for="username">Логин</label><br />
          <input id="username" v-model="login.username" name="username" type="text" />
        </p>
        <p>
          <label for="password">Пароль</label><br />
          <input id="password" v-model="login.password" name="password" type="password" />
        </p>
        <button type="submit">Войти</button>
      </form>
    </main>
  </div>
</template>

<style scoped>
  body {
    font-family: system-ui, sans-serif;
    margin: 0;
    padding: 1rem;
  }
  header {
    margin-bottom: 1rem;
    display: flex;
    gap: 0.5rem;
  }
  header button {
    padding: 0.5rem 1rem;
    cursor: pointer;
  }
  table {
    border-collapse: collapse;
  }
  th, td {
    padding: 0.4rem 0.8rem;
  }
  form p {
    margin-bottom: 0.8rem;
  }
  label {
    font-weight: 600;
  }
  input, textarea {
    padding: 0.3rem;
    min-width: 250px;
  }
</style>