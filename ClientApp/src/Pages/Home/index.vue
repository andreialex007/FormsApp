<script setup lang="ts">
import { Observer } from 'mobx-vue-lite'
import type { Props } from './Store'
const props = defineProps<Props>()
</script>

<template>
  <Observer>
    <div class="flex flex-col items-center justify-center min-h-[calc(100vh-4rem)] p-8 bg-gray-50">
      <div class="flex flex-col items-center mb-12">
        <i :class="`ri-${props.store.icon} text-8xl text-blue-600 mb-4`"></i>
        <h1 class="text-5xl font-bold text-gray-800 mb-3">
          {{ props.store.name }}
        </h1>
        <p class="text-xl text-gray-600">Welcome to FormsApp</p>
      </div>

      <!-- Action Cards -->
      <div class="grid grid-cols-1 md:grid-cols-2 gap-8 max-w-4xl w-full">
        <!-- Submit Card -->
        <router-link
          to="/submit"
          class="group relative bg-gradient-to-br from-emerald-500 to-emerald-700 rounded-2xl p-8 shadow-xl hover:shadow-2xl transform hover:scale-105 transition-all duration-300 overflow-hidden"
        >
          <div class="absolute top-0 right-0 w-32 h-32 bg-white opacity-10 rounded-full -mr-16 -mt-16"></div>
          <div class="absolute bottom-0 left-0 w-24 h-24 bg-white opacity-10 rounded-full -ml-12 -mb-12"></div>

          <div class="relative z-10">
            <div class="flex items-center justify-between mb-6">
              <div class="w-16 h-16 bg-white bg-opacity-20 rounded-xl flex items-center justify-center backdrop-blur-sm">
                <i class="ri-file-add-fill text-4xl text-emerald-500"></i>
              </div>
              <i class="ri-arrow-right-line text-3xl text-white opacity-0 group-hover:opacity-100 transform translate-x-0 group-hover:translate-x-2 transition-all duration-300"></i>
            </div>

            <h2 class="text-3xl font-bold text-white mb-3">Submit Form</h2>
            <p class="text-emerald-100 text-lg">
              Create and submit a new form with your information
            </p>

            <div class="mt-6 flex items-center text-white font-semibold">
              <span>Get Started</span>
              <i class="ri-arrow-right-s-line ml-1 text-xl"></i>
            </div>
          </div>
        </router-link>

        <!-- Items Card -->
        <router-link
          to="/items"
          class="group relative bg-gradient-to-br from-blue-500 to-blue-700 rounded-2xl p-8 shadow-xl hover:shadow-2xl transform hover:scale-105 transition-all duration-300 overflow-hidden"
        >
          <div class="absolute top-0 right-0 w-32 h-32 bg-white opacity-10 rounded-full -mr-16 -mt-16"></div>
          <div class="absolute bottom-0 left-0 w-24 h-24 bg-white opacity-10 rounded-full -ml-12 -mb-12"></div>

          <div class="relative z-10">
            <div class="flex items-center justify-between mb-6">
              <div class="w-16 h-16 bg-white bg-opacity-20 rounded-xl flex items-center justify-center backdrop-blur-sm">
                <i class="ri-file-list-3-fill text-4xl text-blue-500"></i>
              </div>
              <i class="ri-arrow-right-line text-3xl text-white opacity-0 group-hover:opacity-100 transform translate-x-0 group-hover:translate-x-2 transition-all duration-300"></i>
            </div>

            <h2 class="text-3xl font-bold text-white mb-3">View Items</h2>
            <p class="text-blue-100 text-lg">
              Browse and search through all submitted forms
            </p>

            <div class="mt-6 flex items-center text-white font-semibold">
              <span>Browse Now</span>
              <i class="ri-arrow-right-s-line ml-1 text-xl"></i>
            </div>
          </div>
        </router-link>
      </div>
    </div>
  </Observer>
</template>
