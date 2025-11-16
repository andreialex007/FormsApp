<script setup lang="ts">
import { Observer } from 'mobx-vue-lite'
import { onMounted } from 'vue'
import type { Props } from './Store'

const props = defineProps<Props>()

onMounted(() => props.store.loadSubmissions())
</script>

<template>
  <Observer>
    <div class="min-h-[calc(100vh-4rem)] py-12 px-4 sm:px-6 lg:px-8 bg-gray-50">
      <div class="max-w-7xl mx-auto">
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
                <div class="text-2xl font-bold text-gray-900">{{ props.store.returned }}</div>
                <div class="text-gray-600 text-xs uppercase tracking-wide">Returned</div>
              </div>
              <div class="text-center px-4 py-2 bg-white rounded-lg shadow">
                <div class="text-2xl font-bold text-emerald-600">{{ props.store.found }}</div>
                <div class="text-gray-600 text-xs uppercase tracking-wide">Found</div>
              </div>
              <div class="text-center px-4 py-2 bg-white rounded-lg shadow">
                <div class="text-2xl font-bold text-gray-900">{{ props.store.total }}</div>
                <div class="text-gray-600 text-xs uppercase tracking-wide">Total</div>
              </div>
            </div>
          </div>
        </div>

        <!-- Table Card -->
        <div class="bg-white rounded-2xl shadow-xl overflow-hidden flex flex-col h-[calc(100vh-16rem)]">
          <!-- Header Table with Filters -->
          <div class="overflow-x-auto border-b border-gray-200">
            <table class="w-full table-fixed">
              <thead>
                <!-- Filter Row -->
                <tr class="bg-gray-100 border-b border-gray-200">
                  <th class="px-6 py-3" style="width: 120px;">
                    <input
                      type="text"
                      placeholder="ID"
                      class="w-full px-2 py-1.5 text-sm border border-gray-300 rounded-lg focus:ring-2 focus:ring-gray-500 focus:border-transparent outline-none"
                    />
                  </th>
                  <th class="px-6 py-3" style="width: 320px;">
                    <div class="flex gap-2">
                      <input
                        type="date"
                        placeholder="From"
                        class="flex-1 px-2 py-1.5 text-sm border border-gray-300 rounded-lg focus:ring-2 focus:ring-gray-500 focus:border-transparent outline-none"
                      />
                      <input
                        type="date"
                        placeholder="To"
                        class="flex-1 px-2 py-1.5 text-sm border border-gray-300 rounded-lg focus:ring-2 focus:ring-gray-500 focus:border-transparent outline-none"
                      />
                    </div>
                  </th>
                  <th class="px-6 py-3">
                    <input
                      type="text"
                      placeholder="Search content..."
                      class="w-full px-2 py-1.5 text-sm border border-gray-300 rounded-lg focus:ring-2 focus:ring-gray-500 focus:border-transparent outline-none"
                    />
                  </th>
                  <th class="px-6 py-3" style="width: 140px;"></th>
                </tr>
                <!-- Labels Row -->
                <tr class="bg-gray-50 border-b border-gray-200">
                  <th class="px-6 py-4 text-left text-xs font-semibold text-gray-600 uppercase tracking-wider" style="width: 120px;">ID</th>
                  <th class="px-6 py-4 text-left text-xs font-semibold text-gray-600 uppercase tracking-wider" style="width: 320px;">Date</th>
                  <th class="px-6 py-4 text-left text-xs font-semibold text-gray-600 uppercase tracking-wider">Content</th>
                  <th class="px-6 py-4 text-left text-xs font-semibold text-gray-600 uppercase tracking-wider" style="width: 140px;">Actions</th>
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
                    class="hover:bg-gray-50 transition duration-150">
                  <td class="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900" style="width: 120px;">
                    #{{ submission.id }}
                  </td>
                  <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-600" style="width: 320px;">
                    {{ new Date(submission.created).toLocaleString() }}
                  </td>
                  <td class="px-6 py-4 text-sm text-gray-600">
                    <div class="max-w-2xl">
                      <p class="font-medium text-gray-900">{{ submission.parsedContent.fullName || 'N/A' }}</p>
                      <p class="text-gray-500 text-xs mt-1">
                        {{ submission.parsedContent.email || '' }}
                        <span v-if="submission.parsedContent.country"> • {{ submission.parsedContent.country }}</span>
                        <span v-if="submission.parsedContent.gender"> • {{ submission.parsedContent.gender }}</span>
                      </p>
                    </div>
                  </td>
                  <td class="px-6 py-4 whitespace-nowrap text-sm" style="width: 140px;">
                    <button
                      @click="props.store.deleteSubmission(submission.id)"
                      class="text-red-600 hover:text-red-800 hover:bg-red-50 px-3 py-1.5 rounded-lg transition inline-flex items-center gap-1.5 font-medium">
                      <i class="ri-delete-bin-line text-base"></i>
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
