import { createRouter, createWebHistory } from 'vue-router'
import IncidentList from './pages/IncidentList.vue'
import IncidentNew from './pages/IncidentNew.vue'
import Login from './pages/Login.vue'

const routes = [
  { path: '/', component: IncidentList },
  { path: '/new', component: IncidentNew },
  { path: '/login', component: Login },
]

export const router = createRouter({
  history: createWebHistory(),
  routes,
})