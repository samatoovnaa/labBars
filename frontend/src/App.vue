<script setup>
import AppHeader from './components/AppHeader.vue'
</script>

<template>
  <AppHeader />
  <router-view />
</template>

<style scoped>
  :global(body) {
    font-family: system-ui, sans-serif;
    margin: 0;
    padding: 1rem;
    background: #f5f7fa;
  }
</style>