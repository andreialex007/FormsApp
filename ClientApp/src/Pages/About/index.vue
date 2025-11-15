<script setup lang="ts">
import { Observer } from 'mobx-vue-lite'
import type { Props } from './Store'

const props = defineProps<Props>()
</script>

<template>
  <Observer>
    <div class="flex flex-col items-center justify-center min-h-[calc(100vh-4rem)] bg-gradient-to-br from-purple-500 to-pink-600 p-8">
      <div class="bg-white rounded-lg shadow-2xl p-8 max-w-2xl w-full">
        <div class="flex items-center justify-center mb-6">
          <i class="ri-information-line text-5xl text-purple-600 mr-3"></i>
          <h1 class="text-4xl font-bold text-gray-800">
            About
          </h1>
        </div>

        <div class="mb-6">
          <div class="flex items-center justify-center mb-4">
            <i class="ri-code-box-line text-xl text-purple-600 mr-2"></i>
            <p class="text-xl font-semibold text-gray-700">
              {{ props.store.appInfo }}
            </p>
          </div>
          <p class="text-center text-gray-600 mb-4">
            <i class="ri-rocket-line mr-1"></i>
            A modern web application built with cutting-edge technologies.
          </p>
        </div>

        <div class="border-t pt-6">
          <div class="flex items-center mb-4">
            <i class="ri-stack-line text-2xl text-purple-600 mr-2"></i>
            <h2 class="text-2xl font-semibold text-gray-700">Tech Stack</h2>
          </div>
          <ul class="space-y-2">
            <li
              v-for="(feature, index) in props.store.features"
              :key="index"
              class="flex items-center justify-between bg-gray-50 px-4 py-3 rounded-md hover:bg-gray-100 transition-colors"
            >
              <div class="flex items-center gap-2">
                <i class="ri-checkbox-circle-line text-green-500"></i>
                <span class="text-gray-700 font-medium">{{ feature }}</span>
              </div>
              <button
                @click="props.store.removeFeature(index)"
                class="flex items-center gap-1 px-3 py-1 bg-red-500 text-white text-sm rounded hover:bg-red-600 transition-colors duration-200"
              >
                <i class="ri-delete-bin-line"></i>
                Remove
              </button>
            </li>
          </ul>
        </div>

        <div class="mt-6 flex gap-2">
          <i class="ri-add-circle-line text-2xl text-purple-600 self-center"></i>
          <input
            type="text"
            placeholder="Add a feature..."
            @keyup.enter="(e) => {
              const input = e.target as HTMLInputElement
              if (input.value.trim()) {
                props.store.addFeature(input.value.trim())
                input.value = ''
              }
            }"
            class="flex-1 px-4 py-2 border border-gray-300 rounded-md focus:ring-2 focus:ring-purple-500 focus:border-transparent"
          />
        </div>

        <div class="mt-6 text-center text-sm text-gray-600">
          <p>Built with: Vue 3 + TypeScript + Vite</p>
          <p>Tailwind CSS + Ark UI + MobX</p>
        </div>
      </div>
    </div>
  </Observer>
</template>
