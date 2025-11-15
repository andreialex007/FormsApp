import { makeObservable, observable, computed, action } from 'mobx'
import NavItem from '../../Common/NavItem'

export interface Props {
  store: Store
}

export default class Store extends NavItem {
  url = '/items'
  name = 'Items'
  icon = 'list-check'

  constructor() {
    super()
    makeObservable(this, {})
  }
}
