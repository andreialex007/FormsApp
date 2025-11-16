<script setup lang="ts">
import { Observer } from 'mobx-vue-lite'
import { onMounted } from 'vue'
import type { Props } from './Store'

const props = defineProps<Props>()

onMounted(() => props.store.loadSubmissions())
</script>

<template>
  <Observer>
    <div class="h-[calc(100vh-4rem)] py-6 px-4 sm:px-6 lg:px-8 bg-gray-50 overflow-hidden">
      <div class="h-full flex flex-col">
        <!-- Header -->
        <div class="mb-8">
          <div class="flex items-center justify-between">
            <div>
              <div class="flex items-center gap-3 mb-2">
                <i :class="`ri-${props.store.icon} text-4xl text-gray-700`"></i>
                <h1 class="text-4xl font-bold text-gray-900">{{ props.store.name }}</h1>
              </div>
              <p class="text-gray-600">View all submitted forms</p>
            </div>
            <div class="flex gap-4 text-sm">
              <div class="text-center px-4 py-2 bg-white rounded-lg shadow">
                <i class="ri-arrow-left-right-line text-lg text-gray-600 mb-1"></i>
                <div class="text-2xl font-bold text-gray-900">{{ props.store.returned }}</div>
                <div class="text-gray-600 text-xs uppercase tracking-wide">Returned</div>
              </div>
              <div class="text-center px-4 py-2 bg-white rounded-lg shadow">
                <i class="ri-search-line text-lg text-emerald-600 mb-1"></i>
                <div class="text-2xl font-bold text-emerald-600">{{ props.store.found }}</div>
                <div class="text-gray-600 text-xs uppercase tracking-wide">Found</div>
              </div>
              <div class="text-center px-4 py-2 bg-white rounded-lg shadow">
                <i class="ri-database-2-line text-lg text-gray-600 mb-1"></i>
                <div class="text-2xl font-bold text-gray-900">{{ props.store.total }}</div>
                <div class="text-gray-600 text-xs uppercase tracking-wide">Total</div>
              </div>
            </div>
          </div>
        </div>

        <!-- Table Card -->
        <div class="bg-white rounded-2xl shadow-xl overflow-hidden flex flex-col flex-1 min-h-0">
          <!-- Header Table with Filters -->
          <div class="overflow-x-auto border-b border-gray-200">
            <table class="w-full table-fixed">
              <thead>
                <!-- Filter Row -->
                <tr class="bg-gray-100 border-b border-gray-200">
                  <th class="px-6 py-3" style="width: 120px;">
                    <input
                      v-model="props.store.filters.id"
                      @input="props.store.onFilterChange()"
                      type="text"
                      placeholder="ID"
                      class="w-full px-2 py-1.5 text-sm border border-gray-300 rounded-lg focus:ring-2 focus:ring-gray-500 focus:border-transparent outline-none"
                    />
                  </th>
                  <th class="px-6 py-3" style="width: 320px;">
                    <div class="flex gap-2">
                      <input
                        v-model="props.store.filters.dateFrom"
                        @input="props.store.onFilterChange()"
                        type="date"
                        placeholder="From"
                        class="flex-1 px-2 py-1.5 text-sm border border-gray-300 rounded-lg focus:ring-2 focus:ring-gray-500 focus:border-transparent outline-none"
                      />
                      <input
                        v-model="props.store.filters.dateTo"
                        @input="props.store.onFilterChange()"
                        type="date"
                        placeholder="To"
                        class="flex-1 px-2 py-1.5 text-sm border border-gray-300 rounded-lg focus:ring-2 focus:ring-gray-500 focus:border-transparent outline-none"
                      />
                    </div>
                  </th>
                  <th class="px-6 py-3">
                    <input
                      v-model="props.store.filters.contentSearchTerm"
                      @input="props.store.onFilterChange()"
                      type="text"
                      placeholder="Search content..."
                      class="w-full px-2 py-1.5 text-sm border border-gray-300 rounded-lg focus:ring-2 focus:ring-gray-500 focus:border-transparent outline-none"
                    />
                  </th>
                  <th class="px-6 py-3" style="width: 140px;">
                    <button
                      @click="props.store.resetFilters()"
                      class="w-full bg-gray-600 text-white py-1.5 px-3 rounded-lg text-sm font-medium hover:bg-gray-700 transition inline-flex items-center justify-center gap-1.5">
                      <i class="ri-refresh-line text-base"></i>
                      Reset
                    </button>
                  </th>
                </tr>
                <!-- Labels Row -->
                <tr class="bg-gray-50 border-b border-gray-200">
                  <th class="px-6 py-4 text-left text-xs font-semibold text-gray-600 uppercase tracking-wider" style="width: 120px;">
                    <i class="ri-hashtag mr-1"></i>ID
                  </th>
                  <th class="px-6 py-4 text-left text-xs font-semibold text-gray-600 uppercase tracking-wider" style="width: 320px;">
                    <i class="ri-calendar-line mr-1"></i>Date
                  </th>
                  <th class="px-6 py-4 text-left text-xs font-semibold text-gray-600 uppercase tracking-wider">
                    <i class="ri-file-text-line mr-1"></i>Content
                  </th>
                  <th class="px-6 py-4 text-left text-xs font-semibold text-gray-600 uppercase tracking-wider" style="width: 140px;">
                    <i class="ri-settings-3-line mr-1"></i>Actions
                  </th>
                </tr>
              </thead>
            </table>
          </div>

          <!-- Content Table with Scroll -->
          <div class="flex-1 overflow-y-overlay" style="overflow-y: overlay;">
            <table class="w-full table-fixed">
              <tbody class="divide-y divide-gray-200">
                <tr v-if="props.store.submissions.length === 0 && !props.store.isLoading">
                  <td colspan="4" class="px-6 py-12 text-center text-gray-500">
                    <i class="ri-inbox-line text-5xl mb-2 block"></i>
                    <p class="text-lg">No submissions yet</p>
                  </td>
                </tr>
                <tr v-for="submission in props.store.submissions" :key="submission.id"
                    class="even:bg-gray-50 hover:bg-gray-100 transition duration-150">
                  <td class="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900" style="width: 120px;">
                    #{{ submission.id }}
                  </td>
                  <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-600" style="width: 320px;">
                    {{ new Date(submission.created).toLocaleString() }}
                  </td>
                  <td class="px-6 py-4 text-sm text-gray-600">
                    <div class="max-w-2xl">
                      <p class="font-medium text-gray-900 flex items-center gap-1">
                        <i class="ri-user-line text-gray-500"></i>
                        {{ submission.parsedContent.fullName || 'N/A' }}
                      </p>
                      <p class="text-gray-500 text-xs mt-1 flex items-center gap-2 flex-wrap">
                        <span v-if="submission.parsedContent.email" class="inline-flex items-center gap-1">
                          <i class="ri-mail-line"></i>
                          {{ submission.parsedContent.email }}
                        </span>
                        <span v-if="submission.parsedContent.country" class="inline-flex items-center gap-1">
                          <i class="ri-global-line"></i>
                          {{ submission.parsedContent.country }}
                        </span>
                        <span v-if="submission.parsedContent.gender" class="inline-flex items-center gap-1">
                          <i class="ri-user-heart-line"></i>
                          {{ submission.parsedContent.gender }}
                        </span>
                      </p>
                    </div>
                  </td>
                  <td class="px-6 py-4 whitespace-nowrap text-sm" style="width: 140px;">
                    <button
                      @click="props.store.confirmDelete(submission.id)"
                      class="bg-red-800 text-white hover:bg-red-900 px-3 py-1.5 rounded-full transition inline-flex items-center gap-1.5 font-medium text-xs shadow-sm">
                      <i class="ri-delete-bin-line text-sm"></i>
                      Delete
                    </button>
                  </td>
                </tr>
              </tbody>
            </table>
          </div>

          <!-- Load More Button -->
          <div v-if="props.store.hasMore || props.store.isLoading" class="border-t border-gray-200 p-6 text-center">
            <button
              @click="props.store.loadSubmissions(true)"
              :disabled="props.store.isLoading"
              class="bg-gray-600 text-white py-3 px-8 rounded-lg text-base font-bold hover:bg-gray-700 focus:ring-4 focus:ring-gray-300 transition duration-200 disabled:opacity-50 disabled:cursor-not-allowed inline-flex items-center gap-2 shadow-lg hover:shadow-xl">
              <i v-if="!props.store.isLoading" class="ri-arrow-down-line text-xl"></i>
              <i v-else class="ri-loader-4-line animate-spin text-xl"></i>
              {{ props.store.isLoading ? 'Loading...' : 'Load More' }}
            </button>
          </div>
        </div>
      </div>
    </div>
  </Observer>
</template>
