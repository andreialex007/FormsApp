import { createApp } from 'vue'
import './style.css'
import App from './App.vue'
import Observer from 'mobx-vue-lite'

const app = createApp(App)
app.use(Observer)
app.mount('#app')
