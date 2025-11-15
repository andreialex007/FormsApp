import { makeObservable, observable, computed, action } from 'mobx'
import type NavItem from './Common/NavItem'
import { Utilities } from './Common/Utilities'
import HomeStore from './Pages/Home/Store'
import AboutStore from './Pages/About/Store'
import SubmitStore from './Pages/Submit/Store'
import ItemsStore from './Pages/Items/Store'
import { createRouter, createWebHistory } from 'vue-router'
import type { RouteRecordRaw } from 'vue-router'

export class AppStore {
  homeStore = new HomeStore()
  submitStore = new SubmitStore()
  itemsStore = new ItemsStore()
  aboutStore = new AboutStore()

  navItems : Array<NavItem> = [
    this.homeStore,
    this.submitStore,
    this.itemsStore,
    this.aboutStore
  ]

  constructor() {
    makeObservable(this, {
      homeStore: observable,
      submitStore: observable,
      itemsStore: observable,
      aboutStore: observable,
      navItems: observable,
      allPages: computed
    })
  }

  getActivePage(location: string) {
    return this.navItems.find(x => x.isActive(location))
  }

  get allPages() {
    return Utilities.discoverAllPages(this.navItems)
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
