import { makeObservable, observable, computed, action } from 'mobx'
import { capitalize } from 'lodash'
import NavItem from "../../Common/NavItem.ts";

export interface Props {
  store: HomeStore
}

export class HomeStore extends NavItem{
  message = 'hello world'
  count = 0

  name = 'Home';
  icon = 'home-2-fill';
  url = '/';

  constructor() {
    super();
    makeObservable(this, {
      message: observable,
      count: observable,
      formattedMessage: computed,
      setMessage: action,
      increment: action,
      decrement: action
    })
  }

  get formattedMessage(): string {
    return capitalize(this.message)
  }

  setMessage(newMessage: string) {
    this.message = newMessage
  }

  increment() {
    this.count++
  }

  decrement() {
    this.count--
  }

  override isActive(currentLocation: string): boolean {
    return currentLocation === this.url;
  }
}

