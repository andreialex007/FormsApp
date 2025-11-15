import { makeAutoObservable } from 'mobx'
import { capitalize } from 'lodash'

export interface Props {
  store: HelloStore
}

export class HelloStore {
  message = 'hello world'
  count = 0

  constructor() {
    makeAutoObservable(this)
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
}

