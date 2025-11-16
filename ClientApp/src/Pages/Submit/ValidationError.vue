<script setup lang="ts">
import { Observer } from 'mobx-vue-lite'
import type { FieldState } from 'formstate'

defineProps<{
  field: FieldState<any>
}>()
</script>

<template>
  <Observer>
    <div v-if="field.dirty && field.error" class="h-6 mt-1">
      <div
           class="flex items-center gap-1 text-red-600 text-sm">
        <i class="ri-error-warning-line"></i>
        <span>{{ field.error }}</span>
      </div>
    </div>
  </Observer>
</template>
