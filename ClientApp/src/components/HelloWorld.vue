<script setup lang="ts">
import { Observer } from 'mobx-vue-lite'
import { helloStore } from '../stores/HelloStore'
import { NumberInput } from '@ark-ui/vue'
</script>

<template>
  <Observer>
    <div class="flex flex-col items-center justify-center min-h-screen bg-gradient-to-br from-blue-500 to-purple-600 p-8">
    <div class="bg-white rounded-lg shadow-2xl p-8 max-w-md w-full">
      <h1 class="text-4xl font-bold text-center mb-6 text-gray-800">
        {{ helloStore.formattedMessage }}
      </h1>

      <div class="mb-6">
        <p class="text-center text-2xl font-semibold text-gray-700 mb-4">
          Counter: {{ helloStore.count }}
        </p>
        <div class="flex gap-4 justify-center">
          <button
            @click="helloStore.decrement()"
            class="px-6 py-2 bg-red-500 text-white rounded-md hover:bg-red-600 transition-colors duration-200"
          >
            Decrement
          </button>
          <button
            @click="helloStore.increment()"
            class="px-6 py-2 bg-green-500 text-white rounded-md hover:bg-green-600 transition-colors duration-200"
          >
            Increment
          </button>
        </div>
      </div>

      <div class="border-t pt-6">
        <h2 class="text-lg font-semibold text-gray-700 mb-3">Ark UI Number Input:</h2>
        <NumberInput.Root
          :min="0"
          :max="100"
          :defaultValue="helloStore.count.toString()"
          @value-change="(details) => helloStore.count = parseInt(details.value) || 0"
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
