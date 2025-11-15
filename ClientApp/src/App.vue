<script setup lang="ts">
import Home from './Pages/Home/index.vue'
import { HelloStore } from './Pages/Home/Store'
</script>

<template>
  <Home :store="new HelloStore()" />
</template>
