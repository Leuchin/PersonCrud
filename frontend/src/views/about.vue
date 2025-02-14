<template>
  <router-view/>
  <navigation/>
    <div>
      <h1>Sobre</h1>
      <p>
        Um Crud Básico criado por mim, Leuchin, um entusiasta em tecnologia<br/>
        Este Crud cotempla a criação no formulário, leitura na Lista, edição e exclusão. <br/>
        Me levou exatos 2 dias no back, e 2 dias no front. <br/>
        Caso deseja entrar em contato comigo, tem meu  
        <a 
          href="mailto:marcio.antonio.s.c.silva@gmail.com" 
          placeholder="marcio.antonio.s.c.silva@gmail.com">email
      </a> 
      e meu 
      <a 
        href="https://wa.me/5511951550559">whatsapp
      </a> 
      </p>
    </div>
</template>

<script setup lang="ts">
import Navigation from '../components/Navigation.vue';
</script>