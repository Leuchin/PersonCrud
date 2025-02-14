<template>
    <aside>
        <h3>Opções</h3>
        <router-link id="lk" to="/">Formulário</router-link>
        <router-link id="lk" to="/list">Lista</router-link>
        <router-link id="lk" to="/about">Sobre</router-link>
    </aside>
</template>

<script setup lang="ts">

</script>

<style scoped>
aside{
    display: flex;
    flex-direction: column;
    overflow: hidden;
    padding: 1rem;
    background-color: #333;
    color: white;
    width: 60px;

}
aside{
    width: 100px; 
    height: 100%; 
    background-color: #333; 
    color: #fff; 
    position: fixed; 
    left: 0;
    top: 0;
    padding-top: 2rem; 
    overflow-y: auto; 
    transition: width 0.3s;
}
aside:hover{
  width: 300px;
}
#lk{
    padding: 0.5rem 1rem;
    margin: 0.5rem 0;
    border-radius: 5px;
    color: antiquewhite;
    text-decoration: none;
    transition: background-color 0.3s;
}
#lk:hover{
    background-color: #555;
}
</style>