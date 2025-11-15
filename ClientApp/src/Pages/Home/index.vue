<script setup lang="ts">
import { Observer } from 'mobx-vue-lite'
import { NumberInput } from '@ark-ui/vue'
import type { Props } from './Store'
const props = defineProps<Props>()
</script>

<template>
  <Observer>
    <div class="flex flex-col items-center justify-center min-h-[calc(100vh-4rem)] bg-gradient-to-br from-blue-500 to-purple-600 p-8">
    <div class="bg-white rounded-lg shadow-2xl p-8 max-w-md w-full">
      <div class="flex items-center justify-center mb-6">
        <i class="ri-home-heart-line text-5xl text-blue-600 mr-3"></i>
        <h1 class="text-4xl font-bold text-gray-800">
          {{ props.store.formattedMessage }}
        </h1>
      </div>

      <div class="mb-6">
        <div class="flex items-center justify-center mb-4">
          <i class="ri-calculator-line text-2xl text-gray-600 mr-2"></i>
          <p class="text-2xl font-semibold text-gray-700">
            Counter: {{ props.store.count }}
          </p>
        </div>
        <div class="flex gap-4 justify-center">
          <button
            @click="props.store.decrement()"
            class="flex items-center gap-2 px-6 py-2 bg-red-500 text-white rounded-md hover:bg-red-600 transition-colors duration-200"
          >
            <i class="ri-subtract-line text-xl"></i>
            Decrement
          </button>
          <button
            @click="props.store.increment()"
            class="flex items-center gap-2 px-6 py-2 bg-green-500 text-white rounded-md hover:bg-green-600 transition-colors duration-200"
          >
            <i class="ri-add-line text-xl"></i>
            Increment
          </button>
        </div>
      </div>

      <div class="border-t pt-6">
        <h2 class="text-lg font-semibold text-gray-700 mb-3">Ark UI Number Input:</h2>
        <NumberInput.Root
          :min="0"
          :max="100"
          :defaultValue="props.store.count.toString()"
          @value-change="(details) => props.store.count = parseInt(details.value) || 0"
        >
          <NumberInput.Label class="block text-sm font-medium text-gray-700 mb-2">
            Set Count:
          </NumberInput.Label>
          <div class="flex gap-2">
            <NumberInput.Input
              class="flex-1 px-4 py-2 border border-gray-300 rounded-md focus:ring-2 focus:ring-blue-500 focus:border-transparent"
            />
            <div class="flex flex-col gap-1">
              <NumberInput.IncrementTrigger
                class="px-3 py-1 bg-blue-500 text-white rounded hover:bg-blue-600 text-sm"
              >
                +
              </NumberInput.IncrementTrigger>
              <NumberInput.DecrementTrigger
                class="px-3 py-1 bg-blue-500 text-white rounded hover:bg-blue-600 text-sm"
              >
                -
              </NumberInput.DecrementTrigger>
            </div>
          </div>
        </NumberInput.Root>
      </div>

      <div class="mt-6 text-center text-sm text-gray-600">
        <p>Built with: Vue 3 + TypeScript + Vite</p>
        <p>Tailwind CSS + Ark UI + MobX</p>
      </div>
    </div>
    </div>
  </Observer>
</template>
