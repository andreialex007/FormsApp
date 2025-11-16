import { makeObservable, observable, action, computed } from 'mobx'
import NavItem from '../../Common/NavItem'
import axios from '@/Common/AxiosConfig'

export interface Submission {
  id: number
  created: string
  content: string
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
  filterId: string = ''

  @observable
  filterContentSearchTerm: string = ''

  constructor() {
    super()
    makeObservable(this)
  }

  @computed
  get hasMore() {
    return this.submissions.length < this.found
  }

  @action
  async loadSubmissions(append = false) {
    this.isLoading = true
    this.skip = append ? (this.skip + this.take) : 0

    try {
      const response = await axios.post('/api/submissions/search', {
        skip: this.skip,
        take: this.take,
        id: this.filterId ? parseInt(this.filterId) : null,
        contentSearchTerm: this.filterContentSearchTerm || null
      })

      const newSubmissions = response.data.items || []
      this.submissions = append ? [...this.submissions, ...newSubmissions] : newSubmissions
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
  async deleteSubmission(id: number) {
    try {
      await axios.delete(`/api/submissions/${id}`)
      this.submissions = this.submissions.filter(s => s.id !== id)
    } catch (error) {
      console.error('Error deleting submission:', error)
    }
  }
}
