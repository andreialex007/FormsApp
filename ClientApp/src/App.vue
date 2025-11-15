<script setup lang="ts">
import { RouterView, useRoute, useRouter } from 'vue-router'
import { appStore } from './AppStore'
import { computed } from 'vue'

const route = useRoute()
const router = useRouter()
const navItems = computed(() => [appStore.homeStore, appStore.aboutStore])
</script>

<template>
  <div class="flex min-h-screen flex-col">
    <!-- Navigation -->
    <div class="flex flex-row bg-gray-100 shadow-md" >
      <div
        v-for="item in navItems"
        :key="item.url"
        @click="router.push(item.url)"
        class="flex cursor-pointer items-center gap-1 px-6 p-3 transition-colors"
        :class="item.isActive(route.path)
          ? 'bg-slate-300 text-gray-900 cursor-default'
          : 'hover:bg-gray-200'"
      >
        <i :class="`ri-${item.icon}`"></i>
        {{ item.name }}
      </div>
      <div class="flex-grow"></div>
    </div>

    <!-- Page Content -->
    <RouterView />
  </div>
</template>