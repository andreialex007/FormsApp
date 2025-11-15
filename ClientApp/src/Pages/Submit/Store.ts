import { makeObservable, observable, action, computed } from 'mobx'
import NavItem from '../../Common/NavItem'
import axios from '@/Common/AxiosConfig'
import { FieldState, FormState } from "formstate";
import { z } from 'zod'
import _, { mapValues, transform } from 'lodash'


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
  fullName = new FieldState("")
    .validators(
      v => v.trim().length < 2 && "Full name must be at least 2 characters",
      v => v.length > 100 && "Full name is too long"
    ).enableAutoValidation()

  email = new FieldState("")
    .validators(
      v => !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(v) && "Please enter a valid email address"
    ).enableAutoValidation()

  country = new FieldState("")
    .validators(
      v => v.length < 1 && "Please select a country"
    ).enableAutoValidation()

  gender = new FieldState("")
    .validators(
      v => v.length < 1 && "Please select your gender"
    ).enableAutoValidation()

  newsletter = new FieldState(false)

  birthDate = new FieldState("")
    .validators(
      v => v.length < 1 && "Please select your birth date",
      v => {
        if (!v) return false
        const selectedDate = new Date(v)
        const today = new Date()
        const age = today.getFullYear() - selectedDate.getFullYear()
        return (age < 13 || age > 120) && "You must be at least 13 years old"
      }
    )

  @observable
  isSubmitting = false

  constructor() {
    super()
    makeObservable(this)
  }

  @computed
  get errors(): Record<string, string> {
    return {};
  }

  @computed
  get isValid() {
    return Object.keys(this.errors).length === 0
  }

  @action
  setTouched(field: string, value: boolean) {
    
  }
  
  onBlur = (field: FieldState<any>) => {
    field.validate();
    field.dirty = true
  }

  @action
  async submitForm() {
    
    
    /* Object.keys(this.touched).forEach((key) => {
      this.touched[key as keyof typeof this.touched] = true
    }) */

    if (!this.isValid) {
      alert('Please fix validation errors before submitting')
      return
    }

    this.isSubmitting = true
    try {
      const formData = {
       /* fullName: this.fullName,
        email: this.email,
        country: this.country,
        birthDate: this.birthDate,
        gender: this.gender,
        newsletter: this.newsletter */
      }

      await axios.post('/api/submissions', {
        content: JSON.stringify(formData)
      })

      alert('Form submitted successfully!')
      this.resetForm()
    } catch (error: any) {
      const errorMessage = error.response?.data?.detail || error.response?.data?.title || error.message || 'Unknown error'
      alert('Error submitting form: ' + errorMessage)
    } finally {
      this.isSubmitting = false
    }
  }
  
  
  
  @action
  resetForm() {
    /* this.fullName = ''
    this.email = ''
    this.country = ''
    this.birthDate = ''
    this.gender = ''
    this.newsletter = false */

    /* this.touched = {
      fullName: false,
      email: false,
      country: false,
      birthDate: false,
      gender: false
    } */
  }
}
