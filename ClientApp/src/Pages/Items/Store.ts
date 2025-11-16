import { makeObservable, observable, action, computed } from 'mobx'
import NavItem from '@/Common/NavItem'
import axios from '@/Common/AxiosConfig'
import _ from 'lodash'

export interface FormContent {
  fullName: string
  email: string
  country: string
  birthDate: string
  gender: string
  newsletter: boolean
}

export interface Submission {
  id: number
  created: string
  content: string
  parsedContent: FormContent
}

export interface Props {
  store: Store
}

export default class Store extends NavItem {
  url = '/items'
  name = 'Submissions'
  icon = 'list-check'

  @observable
  submissions: Submission[] = []

  @observable
  isLoading = false

  @observable
  skip = 0

  @observable
  take = 20

  @observable
  returned = 0

  @observable
  found = 0

  @observable
  total = 0

  @observable
  filters = {
    id: '',
    dateFrom: '',
    dateTo: '',
    contentSearchTerm: ''
  }

  debouncedApplyFilters = _.debounce(() => this.applyFilters(), 500)

  constructor() {
    super()
    makeObservable(this)
  }

  @computed
  get hasMore() {
    return this.submissions.length < this.found
  }

  private parseContent(content: string): FormContent {
    try {
      return JSON.parse(content) as FormContent
    } catch {
      return {
        fullName: '',
        email: '',
        country: '',
        birthDate: '',
        gender: '',
        newsletter: false
      }
    }
  }

  @action
  async loadSubmissions(append = false) {
    this.isLoading = true
    this.skip = append ? (this.skip + this.take) : 0

    try {
      const response = await axios.post('/api/submissions/search', {
        skip: this.skip,
        take: this.take,
        id: this.filters.id ? parseInt(this.filters.id) : null,
        contentSearchTerm: this.filters.contentSearchTerm || null,
        dateFrom: this.filters.dateFrom || null,
        dateTo: this.filters.dateTo || null
      })

      const rawSubmissions = response.data.items || []
      const parsedSubmissions = rawSubmissions.map((sub: any) => ({
        ...sub,
        parsedContent: this.parseContent(sub.content)
      }))

      this.submissions = append ? [...this.submissions, ...parsedSubmissions] : parsedSubmissions
      this.returned = response.data.returned || 0
      this.found = response.data.found || 0
      this.total = response.data.total || 0
    } catch (error) {
      console.error('Error loading submissions:', error)
    } finally {
      this.isLoading = false
    }
  }

  @action
  applyFilters() {
    this.skip = 0
    this.submissions = []
    this.loadSubmissions(false)
  }

  @action
  onFilterChange() {
    this.debouncedApplyFilters()
  }

  @action
  resetFilters() {
    Object.keys(this.filters).forEach(key => (this.filters as any)[key] = '')
    this.applyFilters()
  }

  @action
  confirmDelete(id: number) {
    if (window.confirm('Are you sure you want to delete this submission?')) {
      this.deleteSubmission(id)
    }
  }

  @action
  async deleteSubmission(id: number) {
    try {
      await axios.delete(`/api/submissions/${id}`)
      this.submissions = this.submissions.filter(s => s.id !== id)
    } catch (error) {
      console.error('Error deleting submission:', error)
    }
  }
}
