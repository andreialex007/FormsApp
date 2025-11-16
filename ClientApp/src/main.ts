import { createApp } from 'vue'
import './style.css'
import 'sweetalert2/dist/sweetalert2.min.css'
import 'toastr/build/toastr.min.css'
import App from './App.vue'
import Observer from 'mobx-vue-lite'
import { router, appStore } from './AppStore'

const app = createApp(App);
app.use(Observer);
app.use(router);
(window as any).appStore = appStore;
app.mount('#app');

