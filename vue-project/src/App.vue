<template>
  <div class="container mt-4">
    <NavBar />
    <router-view />
  </div>
</template>

<script setup lang="ts">
import NavBar from "./components/NavBar.vue";
</script>