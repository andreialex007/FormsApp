<script setup lang="ts">
import { Observer } from 'mobx-vue-lite'
import type { Props } from './Store'

const props = defineProps<Props>()
</script>

<template>
  <Observer>
    <div class="flex flex-col items-center justify-center min-h-[calc(100vh-4rem)] p-8">
      <div class="flex flex-col items-center">
        <i :class="`ri-${props.store.icon} text-9xl text-purple-600 mb-4`"></i>
        <h1 class="text-5xl font-bold text-gray-800">
          {{ props.store.name }}
        </h1>
      </div>
    </div>
  </Observer>
</template>
