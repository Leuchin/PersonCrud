<template>
    <nav class = "navbar">
        
    </nav>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import logo from '../assets/logo.png';  // importa a imagem logo.png
    export default defineComponent({
        name: 'Navbar',
        data() {
            return {
                logo
            }
        }
    })
</script>

<style scoped>
    .navbar {
        display: flex;
        justify-content: space-between;
        align-items: center;
        padding: 1rem 2rem;
        background-color: #f8f9fa;
        box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
    }
    .navbar-logo{
        height:100px;
        display: block;
        margin-right: 8px;
    }
</style>