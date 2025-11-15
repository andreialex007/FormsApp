import { makeObservable, observable, action, computed } from 'mobx'
import NavItem from '../../Common/NavItem'
import axios from '@/Common/AxiosConfig'
import { z } from 'zod'


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

const schema = z.object({
  fullName: z.string().min(2, 'Full name must be at least 2 characters').max(100, 'Full name is too long'),
  email: z.string().email('Please enter a valid email address'),
  country: z.string().min(1, 'Please select a country'),
  gender: z.string().min(1, 'Please select your gender'),
  newsletter: z.boolean(),
  birthDate: z.string().min(1, 'Please select your birth date')
      .refine((date) => {
        const selectedDate = new Date(date)
        const today = new Date()
        const age = today.getFullYear() - selectedDate.getFullYear()
        return age >= 13 && age <= 120
      }, 'You must be at least 13 years old'),
})

export default class Store extends NavItem {
  url = '/submit'
  name = 'Submit'
  icon = 'file-add-fill'

  fullName = ''
  email = ''
  country = ''
  birthDate = ''
  gender = ''
  newsletter = false
  isSubmitting = false

  touched = {
    fullName: false,
    email: false,
    country: false,
    birthDate: false,
    gender: false
  }

  constructor() {
    super()
    makeObservable(this, {
      fullName: observable,
      email: observable,
      country: observable,
      birthDate: observable,
      gender: observable,
      newsletter: observable,
      isSubmitting: observable,
      touched: observable,
      errors: computed,
      isValid: computed,
      submitForm: action,
      resetForm: action,
      setTouched: action
    })
  }

  get errors(): Record<string, string> {
    const result = schema.safeParse({
      fullName: this.fullName,
      email: this.email,
      country: this.country,
      birthDate: this.birthDate,
      gender: this.gender,
      newsletter: this.newsletter
    })

    if (result.success) {
      return {}
    }

    const fieldErrors: Record<string, string> = {}
    result.error.issues.forEach((issue) => {
      const field = issue.path[0] as string
      if (fieldErrors[field]) return
      fieldErrors[field] = issue.message
    })

    return fieldErrors
  }

  get isValid() {
    return Object.keys(this.errors).length === 0
  }

  setTouched(field: keyof typeof this.touched, value: boolean) {
    this.touched[field] = value
  }

  async submitForm() {
    Object.keys(this.touched).forEach((key) => {
      this.touched[key as keyof typeof this.touched] = true
    })

    if (!this.isValid) {
      alert('Please fix validation errors before submitting')
      return
    }

    this.isSubmitting = true
    try {
      const formData = {
        fullName: this.fullName,
        email: this.email,
        country: this.country,
        birthDate: this.birthDate,
        gender: this.gender,
        newsletter: this.newsletter
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

  resetForm() {
    this.fullName = ''
    this.email = ''
    this.country = ''
    this.birthDate = ''
    this.gender = ''
    this.newsletter = false

    this.touched = {
      fullName: false,
      email: false,
      country: false,
      birthDate: false,
      gender: false
    }
  }
}
