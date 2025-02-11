<template>
  <!--Navbar-->
  <Navbar />

  <!--Container-->
  <div id="app">
    <h1>Lista de pessoas</h1>

    <PersonForm
      :personToEdit="personBeingEdited"
     @refresh-list="refreshPersonList"
    />

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
import type { PersonItem } from './models/PersonItem.ts';

export default defineComponent({
  name: 'App',
  components: {
    Navbar,
    PersonForm,
    PersonList
  },
  setup() {
    const personList = ref<InstanceType<typeof PersonList> | null>(null);
    const personBeingEdited = ref<PersonItem | null>(null);

    function onEditPerson(person: PersonItem) {
      personBeingEdited.value = {...person};
    }

    function refreshPersonList() {
      if (personList.value) {
        personList.value.fetchPersons();
      }
      personBeingEdited.value = null;
    }

    return {
      personBeingEdited,
      onEditPerson,
      refreshPersonList,
    };
  },
});
</script>

<style>

</style>