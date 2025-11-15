import { makeObservable, observable, computed, action } from 'mobx'
import { capitalize } from 'lodash'
import NavItem from "../../Common/NavItem.ts";

export interface Props {
  store: Store
}

export default class Store extends NavItem{
  message = 'hello world'
  count = 0

  name = 'Dashboard';
  icon = 'dashboard-fill';
  url = '/';

  constructor() {
    super();
    makeObservable(this, {
      message: observable
    })
  }

  override isActive(currentLocation: string): boolean {
    return currentLocation === this.url;
  }
}

