<template>
    <nav class = "Navbar">
        
    </nav>
</template>

<script lang="ts">

import { defineComponent } from 'vue';
import logo from '../assets/logo.png';

export default defineComponent({
  name: 'Navbar',
  data() {
    return {
      logo
    };
  }
});

</script>

<style scoped>
    .navbar {
        display: flex;
        justify-content: space-between;
        align-items: center;
        padding: 1rem 2rem;
        background-color:rgb(26, 107, 187);
        box-shadow: 0 2px 4px rgba(173, 28, 28, 0.1);
    }
    .navbar-logo{
        height:100px;
        display: block;
        margin-right: 8px;
    }
</style>