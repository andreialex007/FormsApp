import { makeObservable, observable, computed, action } from 'mobx'
import NavItem from '../../Common/NavItem'

export interface Props {
  store: Store
}

export default class Store extends NavItem {
  url = '/submit'
  name = 'Submit'
  icon = 'file-add-fill'

  constructor() {
    super()
    makeObservable(this, {})
  }
}
