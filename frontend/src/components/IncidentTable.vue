<script setup>
defineProps({
  items: { type: Array, required: true },
})
</script>

<template>
  <table border="1" cellpadding="6">
    <thead>
      <tr>
        <th>ID</th>
        <th>Номер</th>
        <th>Тема</th>
        <th>Описание</th>
        <th>Статус</th>
        <th>ID Категории</th>
        <th>ID Исполнителя</th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="item in items" :key="item.id">
        <td>{{ item.id }}</td>
        <td>{{ item.number }}</td>
        <td>{{ item.title }}</td>
        <td>{{ item.description }}</td>
        <td>{{ item.status }}</td>
        <td>{{ item.categoryId }}</td>
        <td>{{ item.assigneeUserId === null ? 'Не назначен' : item.assigneeUserId }}</td>
      </tr>
    </tbody>
  </table>
</template>

<style scoped>
  table { border-collapse: collapse; background: white; width: 100%; }
  th { background: #007a3d; color: white; }
  th, td { padding: 0.4rem 0.8rem; border: 1px solid #ccc; text-align: left;}
</style>