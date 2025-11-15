import { createApp } from 'vue'
import './style.css'
import App from './App.vue'
import Observer from 'mobx-vue-lite'
import { router } from './AppStore'

const app = createApp(App)
app.use(Observer)
app.use(router)
app.mount('#app')
