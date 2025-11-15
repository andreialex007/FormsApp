<script setup lang="ts">
import { Observer } from 'mobx-vue-lite'
import type { Props } from './Store'

const props = defineProps<Props>()

const features = [
  {
    icon: 'file-list-3-line',
    title: 'Dynamic Forms',
    description: 'Create and submit forms with multiple field types including text, dropdown, date, radio, and checkbox'
  },
  {
    icon: 'database-2-line',
    title: 'Data Management',
    description: 'All submissions are stored securely and can be searched and retrieved easily'
  },
  {
    icon: 'shield-check-line',
    title: 'Validation',
    description: 'Built-in form validation ensures data integrity and proper formatting'
  },
  {
    icon: 'code-box-line',
    title: 'Modern Stack',
    description: 'Built with Vue 3, TypeScript, Tailwind CSS, and ASP.NET Core'
  }
]

const techStack = {
  frontend: [
    { name: 'Vue 3', icon: 'vuejs-fill' },
    { name: 'TypeScript', icon: 'code-s-slash-line' },
    { name: 'Tailwind CSS', icon: 'palette-line' },
    { name: 'MobX', icon: 'database-line' },
    { name: 'Vite', icon: 'flashlight-line' }
  ],
  backend: [
    { name: 'ASP.NET Core', icon: 'server-line' },
    { name: 'Entity Framework', icon: 'database-2-line' },
    { name: 'FluentValidation', icon: 'shield-check-line' },
    { name: 'Swagger', icon: 'book-open-line' }
  ]
}
</script>

<template>
  <Observer>
    <div class="h-[calc(100vh-4rem)] bg-gray-50 py-6 px-6 overflow-hidden">
      <div class="h-full flex flex-col">
        <!-- Header -->
        <div class="text-center mb-6">
          <div class="inline-flex items-center gap-3 mb-3">
            <div class="flex items-center justify-center w-12 h-12 bg-purple-600 rounded-full">
              <i :class="`ri-${props.store.icon} text-2xl text-white`"></i>
            </div>
            <h1 class="text-4xl font-bold text-gray-900">About FormsApp</h1>
          </div>
          <p class="text-sm text-gray-600 max-w-2xl mx-auto">
            A modern form submission application built with cutting-edge technologies
          </p>
        </div>

        <!-- Main Content Grid -->
        <div class="flex-1 grid grid-cols-3 gap-4 min-h-0">
          <!-- Left Column: Features -->
          <div class="bg-white rounded-xl shadow-lg p-4">
            <h2 class="text-xl font-bold text-gray-900 mb-3 flex items-center">
              <i class="ri-star-line text-purple-600 mr-2"></i>
              Key Features
            </h2>
            <div class="space-y-2">
              <div
                v-for="feature in features"
                :key="feature.title"
                class="p-2 bg-purple-50 rounded-lg hover:bg-purple-100 transition-colors"
              >
                <div class="flex items-start gap-2">
                  <i :class="`ri-${feature.icon} text-lg text-purple-600 mt-0.5`"></i>
                  <div>
                    <h3 class="text-sm font-bold text-gray-900">{{ feature.title }}</h3>
                    <p class="text-xs text-gray-600">{{ feature.description }}</p>
                  </div>
                </div>
              </div>
            </div>
          </div>

          <!-- Middle Column: Tech Stack -->
          <div class="bg-white rounded-xl shadow-lg p-4">
            <h2 class="text-xl font-bold text-gray-900 mb-3 flex items-center">
              <i class="ri-stack-line text-purple-600 mr-2"></i>
              Technology Stack
            </h2>

            <!-- Frontend -->
            <div class="mb-3">
              <div class="flex items-center mb-2">
                <i class="ri-terminal-box-line text-lg text-blue-600 mr-2"></i>
                <h3 class="text-sm font-bold text-gray-900">Frontend</h3>
              </div>
              <div class="space-y-1.5">
                <div
                  v-for="tech in techStack.frontend"
                  :key="tech.name"
                  class="flex items-center py-1.5 px-2 bg-blue-50 rounded hover:bg-blue-100 transition-colors"
                >
                  <i :class="`ri-${tech.icon} text-sm text-blue-600 mr-2`"></i>
                  <span class="text-xs font-medium text-gray-800">{{ tech.name }}</span>
                </div>
              </div>
            </div>

            <!-- Backend -->
            <div>
              <div class="flex items-center mb-2">
                <i class="ri-server-line text-lg text-green-600 mr-2"></i>
                <h3 class="text-sm font-bold text-gray-900">Backend</h3>
              </div>
              <div class="space-y-1.5">
                <div
                  v-for="tech in techStack.backend"
                  :key="tech.name"
                  class="flex items-center py-1.5 px-2 bg-green-50 rounded hover:bg-green-100 transition-colors"
                >
                  <i :class="`ri-${tech.icon} text-sm text-green-600 mr-2`"></i>
                  <span class="text-xs font-medium text-gray-800">{{ tech.name }}</span>
                </div>
              </div>
            </div>
          </div>

          <!-- Right Column: Stats & Info -->
          <div class="flex flex-col gap-4">
            <!-- Stats -->
            <div class="bg-gradient-to-br from-purple-500 to-purple-700 rounded-xl p-4 text-white text-center flex-1">
              <i class="ri-code-s-slash-line text-3xl mb-2"></i>
              <div class="text-2xl font-bold mb-1">100%</div>
              <div class="text-sm text-purple-100">TypeScript</div>
            </div>
            <div class="bg-gradient-to-br from-blue-500 to-blue-700 rounded-xl p-4 text-white text-center flex-1">
              <i class="ri-shield-check-line text-3xl mb-2"></i>
              <div class="text-2xl font-bold mb-1">Secure</div>
              <div class="text-sm text-blue-100">Validation</div>
            </div>
            <div class="bg-gradient-to-br from-green-500 to-green-700 rounded-xl p-4 text-white text-center flex-1">
              <i class="ri-rocket-line text-3xl mb-2"></i>
              <div class="text-2xl font-bold mb-1">Fast</div>
              <div class="text-sm text-green-100">Performance</div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </Observer>
</template>
