import { makeObservable, observable, computed, action } from 'mobx'
import type NavItem from './Common/NavItem'
import {discoverAllPages} from './Common/Utilities'
import HomeStore from './Pages/Home/Store'
import AboutStore from './Pages/About/Store'
import SubmitStore from './Pages/Submit/Store'
import ItemsStore from './Pages/Items/Store'
import { createRouter, createWebHistory } from 'vue-router'
import type { RouteRecordRaw } from 'vue-router'

export class AppStore {
  
  @observable
  homeStore = new HomeStore()

  @observable
  submitStore = new SubmitStore()

  @observable
  itemsStore = new ItemsStore()

  @observable
  aboutStore = new AboutStore()

  @observable
  navItems : Array<NavItem> = [
    this.homeStore,
    this.submitStore,
    this.itemsStore,
    this.aboutStore
  ]

  constructor() {
    makeObservable(this)
  }

  get allPages() {
    return discoverAllPages(this.navItems)
  }
}

export const appStore = new AppStore()

const routes: RouteRecordRaw[] = appStore.allPages.map(page => ({
  path: page.store.url,
  name: page.store.name,
  component: page.component,
  props: { store: page.store }
}))

export const router = createRouter({
  history: createWebHistory(),
  routes
})
