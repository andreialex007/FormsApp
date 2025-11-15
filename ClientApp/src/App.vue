<script setup lang="ts">
import { RouterView, RouterLink, useRoute } from 'vue-router'
import { appStore } from './AppStore'
import { computed } from 'vue'

const route = useRoute()
const navItems = computed(() => [appStore.homeStore, appStore.aboutStore])
</script>

<template>
  <div class="min-h-screen bg-gray-50">
    <!-- Navigation -->
    <nav class="bg-white shadow-lg">
      <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div class="flex justify-between h-16">
          <div class="flex space-x-8">
            <RouterLink
              v-for="item in navItems"
              :key="item.url"
              :to="item.url"
              class="inline-flex items-center px-4 py-2 text-sm font-medium transition-colors duration-200"
              :class="item.isActive(route.path)
                ? 'border-b-2 border-blue-500 text-blue-600'
                : 'text-gray-600 hover:text-gray-900 hover:border-b-2 hover:border-gray-300'"
            >
              <i :class="`ri-${item.icon} text-xl mr-2`"></i>
              {{ item.name }}
            </RouterLink>
          </div>
        </div>
      </div>
    </nav>

    <!-- Page Content -->
    <RouterView />
  </div>
</template>