import { makeAutoObservable } from 'mobx'
import { capitalize } from 'lodash'

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

export const helloStore = new HelloStore()
