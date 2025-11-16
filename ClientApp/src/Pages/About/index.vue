<script setup lang="ts">
import { Observer } from 'mobx-vue-lite'
import type { Props } from './Store'

const props = defineProps<Props>()
</script>

<template>
  <Observer>
    <div class="min-h-[calc(100vh-4rem)] py-12 px-4 sm:px-6 lg:px-8 bg-gray-50">
      <div class="max-w-2xl mx-auto">
        <div class="bg-white rounded-2xl shadow-xl p-12 text-center">
          <div class="inline-flex items-center justify-center w-20 h-20 bg-gray-600 rounded-full mb-6">
            <i :class="`ri-${props.store.icon} text-4xl text-white`"></i>
          </div>
          <h1 class="text-4xl font-bold text-gray-900 mb-4">About FormsApp</h1>
          <p class="text-lg text-gray-600 mb-8">
            A modern form submission application built with Vue 3, TypeScript, Tailwind CSS, and ASP.NET Core.
          </p>
          <div class="text-gray-500">
            <p>Submit forms, manage submissions, and search through your data with ease.</p>
          </div>
        </div>
      </div>
    </div>
  </Observer>
</template>
