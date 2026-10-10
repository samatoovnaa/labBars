<script setup>
// Таблица получает items снаружи — обязательный массив
defineProps({
  items: {
    type: Array,
    required: true,
  },
})
</script>

<template>
  <table border="1" cellpadding="6">
    <thead>
      <tr>
        <th>Номер</th>
        <th>Краткое описание</th>
        <th>Подразделение (siteId)</th>
        <th>Категория</th>
        <th>Уровень риска</th>
        <th>Статус</th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="item in items" :key="item.id">
        <td>{{ item.number }}</td>
        <td>{{ item.title }}</td>
        <td>{{ item.siteId }}</td>
        <td>{{ item.category }}</td>
        <td>{{ item.riskLevel }}</td>
        <td>{{ item.status }}</td>
      </tr>
    </tbody>
  </table>
</template>

<style scoped>
  table {
    border-collapse: collapse;
    background: white;
  }
  th {
    background: #007a3d;
    color: white;
  }
  th, td {
    padding: 0.4rem 0.8rem;
    border: 1px solid #ccc;
  }
</style>