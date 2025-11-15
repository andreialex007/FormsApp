import { makeAutoObservable } from 'mobx'
import { HomeStore } from './Pages/Home/Store'
import { AboutStore } from './Pages/About/Store'

export class AppStore {
  homeStore: HomeStore
  aboutStore: AboutStore

  constructor() {
    this.homeStore = new HomeStore()
    this.aboutStore = new AboutStore()
    makeAutoObservable(this)
  }

  // Add global app methods here if needed
  reset() {
    this.homeStore = new HomeStore()
    this.aboutStore = new AboutStore()
  }
}

// Create a singleton instance
export const appStore = new AppStore()
