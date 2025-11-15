import { createRouter, createWebHistory } from 'vue-router'
import type { RouteRecordRaw } from 'vue-router'
import { appStore } from '../AppStore'

// Import page components
import Home from '../Pages/Home/index.vue'
import About from '../Pages/About/index.vue'

// Create routes from store configuration
const routes: RouteRecordRaw[] = [
  {
    path: appStore.homeStore.url,
    name: appStore.homeStore.name,
    component: Home,
    props: { store: appStore.homeStore }
  },
  {
    path: appStore.aboutStore.url,
    name: appStore.aboutStore.name,
    component: About,
    props: { store: appStore.aboutStore }
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router
