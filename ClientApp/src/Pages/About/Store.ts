import { makeObservable, observable, computed, action } from 'mobx'
import NavItem from '../../Common/NavItem';

export interface Props {
  store: AboutStore
}

export class AboutStore extends NavItem{
  version = '1.0.0'
  year = new Date().getFullYear()
  features: string[] = ['Vue 3', 'TypeScript', 'Vite', 'Tailwind CSS', 'Ark UI', 'MobX']

  url = '/about';
  name = 'About';
  icon = 'question-fill';

  constructor() {
    super();
    makeObservable(this, {
      version: observable,
      year: observable,
      features: observable,
      appInfo: computed,
      addFeature: action,
      removeFeature: action
    })
  }

  get appInfo(): string {
    return `FormsApp v${this.version} - ${this.year}`
  }

  addFeature(feature: string) {
    this.features.push(feature)
  }

  removeFeature(index: number) {
    this.features.splice(index, 1)
  }
}
