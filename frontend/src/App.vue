<template>
  <!-- Navbar fixa no topo (opcional) -->
  <Navbar />

  <!-- Container principal -->
  <div id="app" class="app-container">
    <h1>Lista de Pessoas</h1>

    <!-- Formulário para criar/editar pessoa -->
    <PersonForm
      :personToEdit="personBeingEdited"
      @refresh-list="refreshPersonList"
    />

    <!-- Lista de pessoas -->
    <!-- ref="personList" serve para acessarmos o método fetchPersons() -->
    <PersonList
      ref="personList"
      @edit-person="onEditPerson"
    />
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue';

import Navbar from './components/Navbar.vue';
import PersonForm from './components/PersonForm.vue';
import PersonList from './components/PersonList.vue';
import type { PersonItem } from './models/PersonItem';

export default defineComponent({
  name: 'App',
  components: {
    Navbar,
    PersonForm,
    PersonList,
  },
  setup() {
  
    const personList = ref<InstanceType<typeof PersonList> | null>(null);

    const personBeingEdited = ref<PersonItem | null>(null);

    function onEditPerson(person: PersonItem) {
      personBeingEdited.value = { ...person };
    }

    function refreshPersonList() {
      personList.value?.fetchPersons();
      personBeingEdited.value = null;
    }

    return {
      personBeingEdited,
      onEditPerson,
      refreshPersonList,
      personList,
    };
  },
});
</script>

<style scoped>
/* Ajusta fonte e espaçamentos básicos para a página */
.app-container {
  max-width: 900px;
  margin: 0 auto;
  padding: 24px;
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
  color: #333;
}

/* Título principal */
h1 {
  margin-bottom: 24px;
  text-align: center;
  font-size: 2rem;
  letter-spacing: 0.5px;
}

/* Opcional: se quiser dar cor de fundo global */
#app {
  background-color: #f1f3f5;
  min-height: 100vh;
}

/* Exemplo para deixar a Navbar fixa no topo, se desejar
   (depende de como você estiliza o <Navbar />) */
.navbar {
  position: sticky;
  top: 0;
  z-index: 999;
}
</style>
