import { makeObservable, observable, action, computed } from 'mobx'
import NavItem from '../../Common/NavItem'
import axios from '@/Common/AxiosConfig'
import { FieldState, FormState } from "formstate";
import _ from "lodash";
import {sleep} from "@/Common/Utilities.ts";

export const countries = [
  'USA',
  'Canada',
  'UK',
  'Australia',
  'Germany',
  'France',
  'Ireland',
  'New Zealand',
  'Sweden',
  'Norway'
]


export interface Props {
  store: Store
}

export default class Store extends NavItem {
  url = '/submit'
  name = 'Submit'
  icon = 'file-add-fill'
  @observable
  isSubmitting = false
  @observable
  message = ''
  @observable
  fields = {
    fullName: new FieldState("").validators(
        v => v.trim().length < 2 && "Full name must be at least 2 characters",
        v => v.length > 100 && "Full name is too long"
    ),
    email: new FieldState("").validators(x => !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(x) && "Please enter a valid email address"),
    country: new FieldState("").validators(v => v.length < 1 && "Please select a country"),
    gender: new FieldState("").validators(v => v.length < 1 && "Please select your gender"),
    birthDate: new FieldState("")
        .validators(
            v => v.length < 1 && "Please select your birth date",
            v => {
              if (!v) return false
              const selectedDate = new Date(v)
              const today = new Date()
              const age = today.getFullYear() - selectedDate.getFullYear()
              return (age < 13 || age > 120) && "You must be at least 13 years old"
            }
        ),
    newsletter: new FieldState(false)
  }
  
  constructor() {
    super()
    makeObservable(this)
  }

  @computed
  get isValid() {
    return !Object.values(this.fields).some(field => field.hasError)
  }

  triggerValidate = (field: FieldState<any>) => {
    field.validate();
    field.dirty = true
  }

  @action
  setMessage(message: string) {
    this.message = message
  }

  @action
  clearMessage() {
    this.message = ''
  }

  @action
  async triggerSubmit() {
    this.clearMessage()
    Object.values(this.fields).forEach(x => this.triggerValidate(x))
    await sleep(1);
    await this.submitForm()
  }
  @action
  async submitForm() {
    if (!this.isValid) {
      this.setMessage('Please fix validation errors before submitting')
      return
    }

    this.isSubmitting = true
    try {
      const formData = _.mapValues(this.fields, field => field.value)
      await axios.post('/api/submissions', {
        content: JSON.stringify(formData)
      })

      this.resetForm()
    } catch (error: any) {
      const errorMessage = error.response?.data?.detail || error.response?.data?.title || error.message || 'Unknown error'
      this.setMessage('Error submitting form: ' + errorMessage)
    } finally {
      this.isSubmitting = false
    }
  }
  
  
  
  
  @action
  resetForm() {
    _.forEach(this.fields, field => field.reset())
  }
}
